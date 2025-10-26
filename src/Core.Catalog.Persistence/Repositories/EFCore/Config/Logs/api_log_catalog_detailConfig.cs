using Core.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Catalog.Persistence.Repositories.EFCore.Config.Logs
{
    public class api_log_catalog_detailConfig : IEntityTypeConfiguration<api_log_catalog_detail>
    {
        public void Configure(EntityTypeBuilder<api_log_catalog_detail> builder)
        {
            builder.ToTable("api_log_catalog_detail");            
            builder.HasKey(p => p.api_log_catalog_detail_id);
            builder.Property(x => x.data_message).HasColumnType("text");

            builder.Property(e => e.api_log_catalog_header_id).HasColumnName("api_log_catalog_header_id");

            builder.Property(e => e.api_log_catalog_detail_id)
              .HasColumnName("api_log_catalog_detail_id")
              .UseIdentityAlwaysColumn();

            builder.HasOne(x => x.api_log_catalog_header)
               .WithMany(h => h.api_log_catalog_detail)
               .HasForeignKey(x => x.api_log_catalog_header_id)
               .HasConstraintName("fk_catalog_header_detail");           
        }
    }
}
