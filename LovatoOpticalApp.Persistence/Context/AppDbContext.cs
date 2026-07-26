using LovatoOpticalApp.Core;
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
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CrystalOrderWork> CrystalOrderWorks { get; set; }
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
            //options.UseNpgsql("Server=localhost; Database=lovato; Port=5432; User Id=postgres; Password=12345678");
            options.UseSqlServer("Server=localhost;Database=LovatoOptical;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasMany(c => c.Recipes)
                      .WithOne(r => r.Customer)
                      .HasForeignKey(r => r.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

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

                entity.HasOne(o => o.CrystalOrderWork)
                      .WithOne(c => c.Order)
                      .HasForeignKey<CrystalOrderWork>(c => c.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CrystalOrderWork>(entity =>
            {
                entity.HasOne(c => c.CrystalRight)
                      .WithMany()
                      .HasForeignKey(c => c.CrystalRightId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.CrystalLeft)
                      .WithMany()
                      .HasForeignKey(c => c.CrystalLeftId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
