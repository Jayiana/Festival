using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShowTime.DataAccess.Configurations;
using ShowTime.DataAccess.Models.ArtistInfo;
using ShowTime.DataAccess.Models.BookingInfo;
using ShowTime.DataAccess.Models.FestivalInfo;
using ShowTime.DataAccess.Models.LineupInfo;
using ShowTime.DataAccess.Models.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess
{
    public class ShowTimeDbContext : DbContext
    {
        public ShowTimeDbContext(DbContextOptions<ShowTimeDbContext> options) : base(options) { }
       
        public DbSet<Festival> Festivals { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Lineup> Lineups { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ArtistConfiguration().Configure(modelBuilder.Entity<Artist>());
            new BookingConfiguration().Configure(modelBuilder.Entity<Booking>());
            new LineupConfigurations().Configure(modelBuilder.Entity<Lineup>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new FestivalConfiguration().Configure(modelBuilder.Entity<Festival>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-05UPJ1O\\SQLEXPRESS;Initial Catalog=ShowTimeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
    }
}
