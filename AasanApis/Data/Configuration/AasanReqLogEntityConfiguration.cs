using AastanApis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AastanApis.Data.Configuration
{
    public class AastanReqLogEntityConfiguration : IEntityTypeConfiguration<AastanReqLog>
    {
        public void Configure(EntityTypeBuilder<AastanReqLog> builder)
        {
            builder.ToTable("Aastan_LOG_REQ");
            builder.HasKey(entity => entity.Id);
            //builder.HasIndex(entity => entity.Id).IsUnique(true);
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
