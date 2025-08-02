using MenuVoting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuVoting.Persistence.Configurations;
public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
	public void Configure(EntityTypeBuilder<Vote> builder)
	{
		builder.HasKey(x => x.Id);

		builder.HasOne(x => x.User)
			.WithMany(x => x.Votes);

		builder.HasOne(x => x.Menu)
			.WithMany(x => x.Votes);
	}
}
