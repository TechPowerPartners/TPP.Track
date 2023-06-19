using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dlbb.Track.Persistence.EntityTypeConfigurations;
public class GlobalSessionTypeConfiguration : IEntityTypeConfiguration<GlobalSessions>
{
	public void Configure(EntityTypeBuilder<GlobalSessions> builder)
	{
		builder.HasKey(s => s.Id);
		builder.HasIndex(s => s.Id).IsUnique();
		builder.Property(s => s.Duration).IsRequired(false);
		builder.Property(s => s.StartTime).IsRequired(true);
		builder.Property(s => s.AppUser).IsRequired(false);

		builder.HasOne(s => s.AppUser)
			.WithMany(s => s.GlobalSessions)
			.HasForeignKey(a => a.AppUserId)
			.OnDelete(DeleteBehavior.SetNull);

		builder.HasOne(s => s.GlobalActivity)
			.WithMany(s => s.GlobalSessions)
			.HasForeignKey(a => a.GlobalActivityId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}
