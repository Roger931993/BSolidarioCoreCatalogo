using Core.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Catalog.Persistence.Repositories.EFCore.Config
{
    public class parametrosConfig : IEntityTypeConfiguration<parametros>
    {
        public void Configure(EntityTypeBuilder<parametros> builder)
        {
            builder.ToTable("parametros");
            builder.HasKey(p => p.parametros_id);
        }
    }
}
