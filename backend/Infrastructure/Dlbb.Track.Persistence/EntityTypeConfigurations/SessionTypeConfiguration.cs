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
				.WithMany(s => s.Sessions)
				.HasForeignKey(a => a.ActivityId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
