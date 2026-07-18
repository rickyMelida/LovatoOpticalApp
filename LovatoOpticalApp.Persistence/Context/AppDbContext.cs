using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Entities.Discounts;
using LovatoOpticalApp.Core.Entities.Payments;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence
{
    public class AppDbContext: DbContext
    {
        // Products
        public DbSet<Frame> Frames { get; set; }
        public DbSet<Crystal> Crystals { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<GlassesCase> GlassesCases { get; set; }

        // Customers & Orders
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        // Payments
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<CreditCardPayment> CreditCardPayments { get; set; }
        public DbSet<DebitCardPayment> DebitCardPayments { get; set; }
        public DbSet<TransferPayment> TransferPayments { get; set; }
        public DbSet<PaymentProof> PaymentProofs { get; set; }

        // Discounts
        public DbSet<DiscountByFixedAmount> DiscountByFixedAmounts { get; set; }
        public DbSet<DiscountByPercentage> DiscountByPercentages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Server=localhost; Database=lovato; Port=5432; User Id=postgres; Password=12345678");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crystal>(entity =>
            {
                entity.OwnsOne(c => c.Prescription);
                entity.OwnsMany(c => c.Treatments);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.CrystalLeft)
                      .WithMany()
                      .HasForeignKey("CrystalLeftId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.CrystalRight)
                      .WithMany()
                      .HasForeignKey("CrystalRightId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Frame)
                      .WithMany()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Customer)
                      .WithMany()
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
