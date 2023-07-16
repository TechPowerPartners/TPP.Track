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
		builder.HasIndex(a => a.Email).IsUnique();
		builder.Property(a => a.PasswordHash).IsRequired();
		builder.Property(a => a.Role).IsRequired();
	}
}
