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


			builder.HasData(
				new Restaurant() { Id = Guid.Parse("A08AFF22-1F11-4913-8296-6D96AC706321"), Name = "McDonalds", Address = "Sydney 10" },
				new Restaurant() { Id = Guid.Parse("C5739BD7-0375-4B80-9BEF-C76406D3FB39"), Name = "KFC", Address = "Toronto 7" },
				new Restaurant() { Id = Guid.Parse("2C578A19-98DC-48C0-A460-3AF245CE8D4E"), Name = "KFC", Address = "Washinghton 7" });
		}
	}
}
