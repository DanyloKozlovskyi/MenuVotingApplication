using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Models.Configurations
{
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
}
