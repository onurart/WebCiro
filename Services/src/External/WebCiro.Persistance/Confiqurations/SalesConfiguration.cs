using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCiro.Domain.CompanyEntities;
using WebCiro.Persistance.Constans;

namespace WebCiro.Persistance.Confiqurations
{
    public sealed class SalesConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable(TableNames.Sales);
        }
    }
}
