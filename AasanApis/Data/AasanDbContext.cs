using AasanApis.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AasanApis.Data
{
    public class AasanDbContext : DbContext
    {
        public DbSet<AasanReqLog> SatnaReqLogs { get; set; }
        public DbSet<AasanResLog> SatnaResLogs { get; set; }
        public DbSet<AccessTokenEntity> AccessTokens { get; set; }
        public AasanDbContext(DbContextOptions<AasanDbContext> dbContext) : base(dbContext)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CarTollsBillsLog>()
            //       .Property(e => e.PayStatus)
            //       .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AasanDbContext).Assembly);
        }

    }
}
