using MenuVoting.DataAccess.Models;
using MenuVoting.DataAccess.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess
{
	// I was getting build failed after Update-Database because I didn't install Npgsql.EntityFrameworkCore.PostgreSQL
	public class MenuVotingDbContext : DbContext
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
}
