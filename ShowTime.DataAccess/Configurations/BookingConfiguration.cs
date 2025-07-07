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
            builder.HasKey(x => x.Id);
            
            // Festival and User relationships
            builder.Property(x => x.FestivalId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            
            // Ticket Information
            builder.Property(x => x.TicketType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.TicketNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            
            // Customer Information
            builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CustomerEmail).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CustomerPhone).IsRequired().HasMaxLength(20);
            builder.Property(x => x.SpecialRequests).HasMaxLength(1000);
            
            // Additional Services
            builder.Property(x => x.IncludeCamping).IsRequired();
            builder.Property(x => x.IncludeFoodPackage).IsRequired();
            builder.Property(x => x.IncludeTransportation).IsRequired();
            
            // Status and Timestamps
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BookingDate).IsRequired();
            builder.Property(x => x.ConfirmationDate);
            builder.Property(x => x.CancellationDate);
            
            // Payment Information
            builder.Property(x => x.PaymentMethod).HasMaxLength(50);
            builder.Property(x => x.PaymentStatus).IsRequired().HasMaxLength(50);
            builder.Property(x => x.TransactionId).HasMaxLength(100);
            
            // Relationships
            builder.HasOne(x => x.Festival)
                .WithMany()
                .HasForeignKey(x => x.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
