using LovatoOpticalApp.Core;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Entities.Discounts;
using LovatoOpticalApp.Core.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LovatoOpticalApp.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private const string DefaultSchema = "lovato";

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Frame> Frames { get; set; }
        public DbSet<Crystal> Crystals { get; set; }
        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CrystalOrderWork> CrystalOrderWorks { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<CreditCardPayment> CreditCardPayments { get; set; }
        public DbSet<DebitCardPayment> DebitCardPayments { get; set; }
        public DbSet<TransferPayment> TransferPayments { get; set; }
        public DbSet<PaymentProof> PaymentProofs { get; set; }

        public DbSet<DiscountByFixedAmount> DiscountByFixedAmounts { get; set; }
        public DbSet<DiscountByPercentage> DiscountByPercentages { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasMany(c => c.Recipes)
                      .WithOne(r => r.Customer)
                      .HasForeignKey(r => r.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Crystal>();
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

            modelBuilder.Entity<CrystalOrderWork>(entity =>
            {
               
            });
        }
    }
}