using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Models.Configurations
{
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
}
