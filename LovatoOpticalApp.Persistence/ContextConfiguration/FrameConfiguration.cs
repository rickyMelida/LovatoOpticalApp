using LovatoOpticalApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LovatoOpticalApp.Persistence
{
	public class FrameConfiguration: IEntityTypeConfiguration<Frame>
	{
		public void Configure(EntityTypeBuilder<Frame> builder)
		{
			builder.ToTable("FRAME", "LOVATO");

			builder.Property(f => f.Id).HasColumnName("FRAME_ID");
			builder.Property(f => f.Name).HasColumnName("NAME");
			builder.Property(f => f.Code).HasColumnName("CODE");
			builder.Property(f => f.PurchasePrice).HasColumnName("PURCHASE_PRICE");
			builder.Property(f => f.SalePrice).HasColumnName("SALE_PRICE");
			builder.Property(f => f.MinimumQuantity).HasColumnName("MINIMUM_QUANTITY");
			builder.Property(f => f.Color).HasColumnName("COLOR");
			builder.Property(f => f.Shape).HasColumnName("SHAPE_ID");
			builder.Property(f => f.Material).HasColumnName("MATERIAL_ID");
			builder.Property(f => f.CreatedBy).HasColumnName("CREATED_BY");
		}
	}
}