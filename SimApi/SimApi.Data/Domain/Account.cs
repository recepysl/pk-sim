using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data;

[Table("Account", Schema = "dbo")]
public class Account : BaseModel
{
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public int AccountNumber { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }
    public decimal Balance { get; set; }
    public bool IsValid { get; set; }


    public virtual List<Transaction> Transactions { get; set; }
}

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.AccountNumber).IsRequired(true);
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.OpenDate).IsRequired(true);
        builder.Property(x => x.Balance).IsRequired(true).HasPrecision(15,2).HasDefaultValue(0);
        builder.Property(x => x.IsValid).IsRequired(true).HasDefaultValue(true);

        builder.HasIndex(x => x.AccountNumber).IsUnique(true);
        builder.HasIndex(x => x.CustomerId);

        builder.HasMany(x=> x.Transactions)
            .WithOne(x=> x.Account)
            .HasForeignKey(x=> x.AccountId)
            .IsRequired(true);


    }   
}