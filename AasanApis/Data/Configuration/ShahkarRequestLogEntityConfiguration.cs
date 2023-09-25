using AasanApis.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AasanApis.Data.Configuration
{
    public class ShahkarRequestLogEntityConfiguration : IEntityTypeConfiguration<ShahkarRequestsLogEntity>
    {
        public void Configure(EntityTypeBuilder<ShahkarRequestsLogEntity> builder)
        {

            builder.ToTable("ShahkarRequestsLog");
            builder.HasKey(entity => entity.Id);
            builder.HasIndex(entity => entity.Id).IsUnique(true);
            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
            builder.Property(entity => entity.Scope).IsRequired();
            builder.Property(entity => entity.TokenType).IsRequired();
            builder.Property(entity => entity.AccessToken).IsRequired();
            builder.Property(entity => entity.ExpireTimeInSecond).IsRequired();
            builder.Property(entity => entity.RefreshToken).IsRequired(false);
            //builder.HasOne<PromissoryNoteRecipientEntity>(entity => entity.PromissoryNoteRecipient)
            //  .WithMany()
            //  .HasForeignKey(entity => entity.RecipientId);
        }
    }
}
