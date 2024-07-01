using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
using TrainAPI.Models;

namespace TrainAPI.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext>options) : base(options) { 
            
        }
        public DbSet<SeatChart> SeatCharts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeatChart>(entity =>
            {
                entity.Property(x => x.SeatStatus).HasConversion(new EnumToNumberConverter<Status,int>());

            });
        }

    }
}
