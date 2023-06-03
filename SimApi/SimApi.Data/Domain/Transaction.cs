using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data;

[Table("Transaction", Schema = "dbo")]
public class Transaction : BaseModel
{
    public int AccountId { get; set; }
    public virtual Account Account { get; set; } 


    public decimal Amount { get; set; }
    public byte Direction { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public string ReferenceNumber { get; set; }
    public string TransactionCode { get; set; }
}

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.AccountId).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Direction).IsRequired(true);
        builder.Property(x => x.TransactionDate).IsRequired(true);
        builder.Property(x => x.ReferenceNumber).HasMaxLength(50).IsRequired(true);
        builder.Property(x => x.TransactionCode).HasMaxLength(50).IsRequired(true);

        builder.HasIndex(x => x.ReferenceNumber);
        builder.HasIndex(x => x.AccountId);
    }
}