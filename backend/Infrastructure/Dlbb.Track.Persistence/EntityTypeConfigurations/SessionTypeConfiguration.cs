using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dlbb.Track.Persistence.EntityTypeConfigurations
{
    public class SessionTypeConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Id).IsUnique();
            builder.Property(s => s.Duration).IsRequired(false);
            builder.Property(s => s.StartTime).IsRequired(true);

			builder.HasOne(s => s.Activity)
				.WithMany(a => a.Sessions)
				.HasForeignKey(s => s.ActivityId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(s => s.AppUser)
				.WithMany(u => u.Sessions)
				.HasForeignKey(s => s.AppUserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
