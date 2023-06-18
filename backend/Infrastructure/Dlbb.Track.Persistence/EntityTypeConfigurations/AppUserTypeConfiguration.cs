using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dlbb.Track.Persistence.EntityTypeConfigurations;
public class AppUserTypeConfiguration: IEntityTypeConfiguration<AppUser>
{
	public void Configure(EntityTypeBuilder<AppUser> builder)
	{
		builder.HasKey(a => a.Id);
		builder.HasIndex(a => a.Id).IsUnique();
		builder.Property(a => a.Email).IsRequired(true);
		builder.Property(a => a.PassworHash).IsRequired(false);
		builder.Property(a => a.Role).HasDefaultValue(RoleEnum.User);
	}
}
