using AastanApis.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AastanApis.Data
{
    public class AastanDbContext : DbContext
    {
        public DbSet<AastanReqLog> SatnaReqLogs { get; set; }
        public DbSet<AastanResLog> SatnaResLogs { get; set; }
        public DbSet<AccessTokenEntity> AccessTokens { get; set; }
        public AastanDbContext(DbContextOptions<AastanDbContext> dbContext) : base(dbContext)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CarTollsBillsLog>()
            //       .Property(e => e.PayStatus)
            //       .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AastanDbContext).Assembly);
        }

    }
}
