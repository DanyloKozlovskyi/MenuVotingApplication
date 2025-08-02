using MenuVoting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuVoting.Persistence.Configurations;
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
