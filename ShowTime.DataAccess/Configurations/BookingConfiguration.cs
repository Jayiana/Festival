using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models.BookingInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(x => new { x.FestivalId, x.UserId });

            builder.Property(x => x.Type).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired();

            builder.HasOne(x => x.Festival)
                   .WithMany()
                   .HasForeignKey(x => x.FestivalId);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(x => x.UserId);
        }
    }

}
