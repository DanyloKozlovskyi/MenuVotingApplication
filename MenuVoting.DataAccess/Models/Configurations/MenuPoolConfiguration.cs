using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Models.Configurations
{
	public class MenuPoolConfiguration : IEntityTypeConfiguration<MenuPool>
	{
		public void Configure(EntityTypeBuilder<MenuPool> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Restaurant)
				.WithMany(x => x.MenuPools);

			builder.HasMany(x => x.Menus)
				.WithOne(x => x.MenuPool);
		}
	}
}
