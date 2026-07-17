using LovatoOpticalApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LovatoOpticalApp.Persistence
{
	public class FrameInventoryConfiguration: IEntityTypeConfiguration<FrameInventory>
	{
		public void Configure(EntityTypeBuilder<FrameInventory> builder)
		{
			builder.ToTable("FRAME_INVENTORY", "LOVATO");

			builder.Property(fi => fi.Id).HasColumnName("FRAME_INVENTORY_ID");
			builder.Property(fi => fi.Frame).HasColumnName("FRAME_ID");
			builder.Property(fi => fi.Quantity).HasColumnName("CURRENT_STOCK");
			builder.Property(fi => fi.UpdatedAt).HasColumnName("LAST_MODIFICATION");
			builder.Property(fi => fi.UpdatedBy).HasColumnName("UPDATED_BY");
		}
	}
}
		