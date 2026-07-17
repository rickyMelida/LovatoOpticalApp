using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core.Entities
{
    public class Invoice
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public Order Order { get; private set; }
        public InvoiceStateEnum State { get; private set; } = InvoiceStateEnum.Pending;

        private readonly List<IDiscount> _discounts = new();
        private readonly List<IPayment> _payments = new();

        [NotMapped]
        public IReadOnlyList<IDiscount> Discounts => _discounts.AsReadOnly();
        [NotMapped]
        public IReadOnlyList<IPayment> Payments => _payments.AsReadOnly();

        // Fase 3: Facturación
        [NotMapped]
        public decimal SubTotal => Order.TotalPrice;
        [NotMapped]
        public decimal TotalDiscount => _discounts.Sum(d => d.Calculate(SubTotal));
        [NotMapped]
        public decimal TotalWithDiscount => Math.Max(0, SubTotal - TotalDiscount);

        // Fase 4: Pagos
        [NotMapped]
        public decimal TotalPaid => _payments.Sum(p => p.Amount);
        [NotMapped]
        public decimal Balance => Math.Max(0, TotalWithDiscount - TotalPaid);

        private Invoice() { }

        public Invoice(Order order)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public void AddDiscount(IDiscount discount)
        {
            if (State != InvoiceStateEnum.Pending)
                throw new InvalidOperationException("No se pueden agregar descuentos a una factura con pagos registrados.");

            _discounts.Add(discount);
        }

        public void RegisterPayment(IPayment payment)
        {
            if (payment.Amount <= 0)
                throw new ArgumentException("El monto del pago debe ser mayor a cero.");

            if (payment.Amount > Balance)
                throw new InvalidOperationException(
                    $"El monto ${payment.Amount:0.00} supera el saldo pendiente ${Balance:0.00}.");

            _payments.Add(payment);

            State = Balance == 0
                ? InvoiceStateEnum.FullyPaid
                : InvoiceStateEnum.PartiallyPaid;

            if (State == InvoiceStateEnum.FullyPaid)
            {
                if (Order.State == StateEnum.Drafts || Order.State == StateEnum.Confirmed)
                    Order.State = StateEnum.InProduction;
            }
        }

        public string GenerateSummary()
        {
            var lines = new List<string>
            {
                $"=== FACTURA {Id} ===",
                $"Fecha       : {CreatedAt:dd/MM/yyyy HH:mm}",
                $"Cliente     : {Order.Customer?.Name ?? "-"}",
                $"",
                $"Sub-Total   : ${SubTotal,10:0.00}",
                $"Descuentos  : -${TotalDiscount,9:0.00}"
            };

            foreach (var d in _discounts)
                lines.Add($"  · {d.Description,-30} -${d.Calculate(SubTotal):0.00}");

            lines.Add($"TOTAL       : ${TotalWithDiscount,10:0.00}");
            lines.Add($"");

            foreach (var p in _payments)
                lines.Add($"  Pago {p.Method,-15} ${p.Amount,8:0.00}  [{p.PaidAt:dd/MM/yyyy}]");

            lines.Add($"Saldo       : ${Balance,10:0.00}");
            lines.Add($"Estado      : {State}");

            return string.Join(Environment.NewLine, lines);
        }
    }
}
