using LovatoOpticalApp.Core.Interfaces;

namespace LovatoOpticalApp.Core.Entities
{
    public class OrderBuilder
    {
        private readonly Order _order = new();

        public OrderBuilder ForCustomer(Customer customer)
        {
            _order.Customer = customer;
            return this;
        }

        public OrderBuilder WithFrame(Frame frame)
        {
            _order.Frame = frame;
            return this;
        }

        public OrderBuilder WithRightCrystal(Crystal crystal)
        {
            _order.CrystalRight = crystal;
            return this;
        }

        public OrderBuilder WithLeftCrystal(Crystal crystal)
        {
            _order.CrystalLeft = crystal;
            return this;
        }

        public OrderBuilder WithSameCrystals(Crystal crystal)
        {
            _order.CrystalRight = crystal;
            _order.CrystalLeft = crystal;
            return this;
        }

        public OrderBuilder WithGlassesCase(Accessory glassesCase)
        {
            _order.GlassesCase = glassesCase;
            return this;
        }

        public OrderBuilder AddAccessory(Accessory accessory)
        {
            _order.Accessories.Add(accessory);
            return this;
        }

        public OrderBuilder WithObservations(string obs)
        {
            _order.Observations = obs;
            return this;
        }

        public OrderBuilder WithCrystalOrderWork(CrystalOrderWork crystalOrderWork)
        {
            _order.CrystalOrderWork = crystalOrderWork;
            return this;
        }

        public Order Build()
        {
            var (isValid, errors) = _order.Validate();
            if (!isValid)
                throw new InvalidOperationException(
                    $"Order inválido:\n{string.Join("\n", errors)}");

            return _order;
        }
    }
}

