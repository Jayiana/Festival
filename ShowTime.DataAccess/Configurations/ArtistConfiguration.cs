using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models.ArtistInfo;
using ShowTime.DataAccess.Models.LineupInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artists");
            builder.HasKey(a => a.Id);
            builder.Property(a=>a.Name).IsRequired().HasMaxLength(255);
            builder.HasMany(a => a.Festivals).WithMany(f => f.Artists).UsingEntity<Lineup>();
        }
    }
}
