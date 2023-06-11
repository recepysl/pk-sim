using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data;

[Table("Currency", Schema = "dbo")]
public class Currency : BaseModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool IsActive { get; set; }

    public virtual List<Account> Accounts { get; set; }
}


public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);


        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Code).IsRequired(true).HasMaxLength(3);
        builder.Property(x => x.Symbol).IsRequired(true).HasMaxLength(5);

        builder.HasIndex(x => x.Code).IsUnique(true);

        builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Currency)
                .HasForeignKey(x => x.CurrencyId)
                .IsRequired(true);
    }
}