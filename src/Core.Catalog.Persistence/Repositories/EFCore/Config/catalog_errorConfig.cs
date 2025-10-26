using Core.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Catalog.Persistence.Repositories.EFCore.Config
{
    public class catalog_errorConfig : IEntityTypeConfiguration<catalog_error>
    {
        public void Configure(EntityTypeBuilder<catalog_error> builder)
        {
            builder.ToTable("catalog_error");
            builder.HasKey(p => p.catalog_error_id);
        }
    }
}
