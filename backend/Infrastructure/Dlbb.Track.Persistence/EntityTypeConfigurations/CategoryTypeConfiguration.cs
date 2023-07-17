using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dlbb.Track.Persistence.EntityTypeConfigurations;
public class CategoryTypeConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.HasKey(c => c.Id);
		builder.HasIndex(c => c.Id).IsUnique();
		builder.Property(c => c.Name).IsRequired(true);
		builder.Property(c => c.Description).IsRequired(false);
		builder.Property(c => c.IsGlobal).IsRequired(true);

		builder.HasOne(c => c.AppUser)
			.WithMany(u => u.Categories)
			.HasForeignKey(c => c.AppUserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(c => c.Activities)
			.WithMany(a => a.Categories);
	}
}
