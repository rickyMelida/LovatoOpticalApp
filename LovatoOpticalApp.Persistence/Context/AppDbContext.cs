using LovatoOpticalApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{ }

		public DbSet<User> Users { get; set; }
		public DbSet<Frame> Frames { get; set; }
		public DbSet<FrameInventory> FrameInventories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new FrameConfiguration());
			modelBuilder.ApplyConfiguration(new FrameInventoryConfiguration());
		}
	}
}
