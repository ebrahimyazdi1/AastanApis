using AasanApis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AasanApis.Data.Configuration
{
    public class AasanResLogEntityConfiguration : IEntityTypeConfiguration<AasanResLog>
    {
        public void Configure(EntityTypeBuilder<AasanResLog> builder)
        {
            builder.ToTable("Aasan_LOG_RES");
            builder.HasKey(entity => entity.Id);
            builder.HasIndex(entity => entity.Id).IsUnique(true);
            builder.HasIndex(entity => entity.ReqLogId).IsUnique(true);
            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
            builder.Property(entity => entity.ResCode).IsRequired();
            builder.Property(entity => entity.PublicReqId).IsRequired();
            builder.Property(entity => entity.HTTPStatusCode).IsRequired();
            builder.Property(entity => entity.ReqLogId).IsRequired();
            builder.Property(entity => entity.JsonRes).IsRequired();
            builder.HasOne(entity => entity.PayaReqLog).WithMany(entity => entity.AasanResLogs)
                .HasForeignKey(p => p.ReqLogId);
        }
    }
}
