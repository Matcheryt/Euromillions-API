using EuromilhoesAPI.Context.Models;
using Microsoft.EntityFrameworkCore;

namespace EuromilhoesAPI.Context
{
    public class ApiContext : DbContext
    {
        public DbSet<PrizeDraw> PrizeDraws => Set<PrizeDraw>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrizeDraw>(pd =>
            {
                pd.HasKey(x => x.Id);
                pd.Property(x => x.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
