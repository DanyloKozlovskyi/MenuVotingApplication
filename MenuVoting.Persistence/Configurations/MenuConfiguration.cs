using MenuVoting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuVoting.Persistence.Configurations;
public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
	public void Configure(EntityTypeBuilder<Menu> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(m => m.Dishes).HasConversion(
			v => string.Join(',', v), // Convert List to CSV for storage
			v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert CSV to List
		);

		builder.HasOne(x => x.MenuPool)
			.WithMany(x => x.Menus);

		builder.HasMany(x => x.Votes)
			.WithOne(x => x.Menu);
	}
}
