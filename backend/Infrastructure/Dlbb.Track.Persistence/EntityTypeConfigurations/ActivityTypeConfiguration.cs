using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dlbb.Track.Persistence.EntityTypeConfigurations
{
	public class ActivityTypeConfiguration : IEntityTypeConfiguration<Activity>
	{
		public void Configure(EntityTypeBuilder<Activity> builder)
		{
			builder.HasKey(a => a.Id);
			builder.HasIndex(a => a.Id).IsUnique();
			builder.Property(a => a.Name).IsRequired(true);
			builder.Property(a => a.Description).IsRequired(false);
		}
	}
}
