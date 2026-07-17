using LovatoOpticalApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LovatoOpticalApp.Persistence
{
	public class UserConfiguration: IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("USERS", "LOVATO");

			builder.Property(f => f.Id).HasColumnName("USER_ID");
			builder.Property(f => f.Name).HasColumnName("NAME");
			builder.Property(f => f.Role).HasColumnName("ROLE");
			builder.Property(f => f.CreatedAt).HasColumnName("CREATED_AT");
		}
	}
}