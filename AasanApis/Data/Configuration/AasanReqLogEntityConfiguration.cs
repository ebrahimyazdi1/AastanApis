using AasanApis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AasanApis.Data.Configuration
{
    public class AasanReqLogEntityConfiguration : IEntityTypeConfiguration<AasanReqLog>
    {
        public void Configure(EntityTypeBuilder<AasanReqLog> builder)
        {

            builder.ToTable("Aasan_LOG_REQ");
            builder.HasKey(entity => entity.Id);
            builder.HasIndex(entity => entity.Id).IsUnique(true);
            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
            builder.Property(entity => entity.UserId).IsRequired();
            builder.Property(entity => entity.ServiceId).IsRequired();
            builder.Property(entity => entity.PublicReqId).IsRequired();
            builder.Property(entity => entity.LogDateTime).IsRequired();
            builder.Property(entity => entity.PublicAppId).IsRequired();
            builder.Property(entity => entity.JsonReq).IsRequired();
        }
    }
}
