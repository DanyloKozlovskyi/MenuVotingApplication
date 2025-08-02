using MenuVoting.Domain.Entities;
using MenuVoting.Domain.Entities.Identity;
using MenuVoting.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MenuVoting.Persistence;
// I was getting build failed after Update-Database because I didn't install Npgsql.EntityFrameworkCore.PostgreSQL
public class MenuVotingDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
	public DbSet<Menu> Menus { get; set; }
	public DbSet<MenuPool> MenuPools { get; set; }
	public DbSet<Restaurant> Restaurants { get; set; }
	public DbSet<Vote> Votes { get; set; }

	public MenuVotingDbContext(DbContextOptions options) : base(options)
	{

	}

	public MenuVotingDbContext()
	{

	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new MenuConfiguration());
		modelBuilder.ApplyConfiguration(new MenuPoolConfiguration());
		modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
		modelBuilder.ApplyConfiguration(new VoteConfiguration());
	}
}
