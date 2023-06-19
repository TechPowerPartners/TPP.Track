using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dlbb.Track.Persistence.EntityTypeConfigurations;
public class GlobalActivityTypeConfiguration : IEntityTypeConfiguration<GlobalActivity>
{
	public void Configure(EntityTypeBuilder<GlobalActivity> builder)
	{
		builder.HasKey(g => g.Id);
		builder.HasIndex(g => g.Id).IsUnique();
		builder.HasIndex(g => g.Name).IsUnique();
	}
}
