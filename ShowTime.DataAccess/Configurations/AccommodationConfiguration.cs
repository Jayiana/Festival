using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models.AccommodationInfo;

namespace ShowTime.DataAccess.Configurations
{
    public class AccommodationConfiguration : IEntityTypeConfiguration<Accommodation>
    {
        public void Configure(EntityTypeBuilder<Accommodation> builder)
        {
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(a => a.AccommodationType)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(a => a.RoomType)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(a => a.Capacity)
                .IsRequired();
            
            builder.Property(a => a.PricePerNight)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            builder.Property(a => a.NumberOfNights)
                .IsRequired();
            
            builder.Property(a => a.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            builder.Property(a => a.Location)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(a => a.CustomerName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(a => a.CustomerEmail)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(a => a.CustomerPhone)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Property(a => a.SpecialRequests)
                .HasMaxLength(500);
            
            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Property(a => a.PaymentStatus)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Property(a => a.TransactionId)
                .HasMaxLength(100);
            
            builder.Property(a => a.CheckInDate)
                .IsRequired();
            
            builder.Property(a => a.CheckOutDate)
                .IsRequired();
            
            builder.Property(a => a.BookingDate)
                .IsRequired();
            
            // Relationships
            builder.HasOne(a => a.Festival)
                .WithMany()
                .HasForeignKey(a => a.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
            
            // Indexes
            builder.HasIndex(a => a.FestivalId);
            builder.HasIndex(a => a.UserId);
            builder.HasIndex(a => a.Status);
            builder.HasIndex(a => a.CheckInDate);
            builder.HasIndex(a => a.CheckOutDate);
        }
    }
} 