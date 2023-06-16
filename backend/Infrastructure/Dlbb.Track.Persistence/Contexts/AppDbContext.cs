using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Persistence.Contexts;
public class AppDbContext : DbContext
{
	public DbSet<Session> Sessions { get; set; }
	public DbSet<Activity> Activities { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new ActivityTypeConfiguration());
		builder.ApplyConfiguration(new SessionTypeConfiguration());
		base.OnModelCreating(builder);
	}
}
