using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Persistence.Contexts;
public class AppDbContext : DbContext
{
	public DbSet<AppUser> AppUsers { get; set; }
	public DbSet<Session> Sessions { get; set; }
	public DbSet<Activity> Activities { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new AppUserTypeConfiguration());
		builder.ApplyConfiguration(new ActivityTypeConfiguration());
		builder.ApplyConfiguration(new SessionTypeConfiguration());

		base.OnModelCreating(builder);
	}
}
