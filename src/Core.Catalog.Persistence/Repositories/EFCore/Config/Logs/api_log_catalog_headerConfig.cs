using Core.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Catalog.Persistence.Repositories.EFCore.Config.Logs
{
    public class api_log_catalog_headerConfig : IEntityTypeConfiguration<api_log_catalog_header>
    {
        public void Configure(EntityTypeBuilder<api_log_catalog_header> builder)
        {
            builder.ToTable("api_log_catalog_header");
            builder.HasKey(p => p.api_log_catalog_header_id);

            builder.Property(e => e.api_log_catalog_header_id)
               .HasColumnName("api_log_catalog_header_id")
               .UseIdentityAlwaysColumn();            
        }
    }
}
