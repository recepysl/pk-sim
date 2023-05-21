using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data;


[Table("Product", Schema = "dbo")]
public class Product : BaseModel
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Tag { get; set; }

}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Url).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Tag).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.CategoryId).IsRequired(true);

        builder.HasIndex(x => x.Name).IsUnique(true);
    }
}