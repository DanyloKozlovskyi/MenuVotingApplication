using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Models.Configurations
{
	internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
	{
		public void Configure(EntityTypeBuilder<Restaurant> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasMany(x => x.Users)
				.WithOne(x => x.Restaurant);

			builder.HasMany(x => x.MenuPools)
				.WithOne(x => x.Restaurant);
		}
	}
}
