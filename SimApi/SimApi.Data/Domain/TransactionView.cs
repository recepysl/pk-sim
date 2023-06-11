using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using SimApi.Base;

namespace SimApi.Data;


[Table("vTransactionReport", Schema = "dbo")]
public class TransactionView : BaseModel
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public byte Direction { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public string ReferenceNumber { get; set; }
    public string TransactionCode { get; set; }

    public string CurrencyCode { get; set; }

    public int CustomerId { get; set; }
    public int AccountNumber { get; set; }
    public string AccountName { get; set; }

    public int CustomerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


public class TransactionViewConfiguration : IEntityTypeConfiguration<TransactionView>
{
    public void Configure(EntityTypeBuilder<TransactionView> builder)
    {
        builder.ToView("vTransactionReport","dbo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.CurrencyCode).IsRequired(false).HasMaxLength(3);
        builder.Property(x => x.AccountId).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(15, 2).HasDefaultValue(0);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Direction).IsRequired(true);
        builder.Property(x => x.TransactionDate).IsRequired(true);
        builder.Property(x => x.ReferenceNumber).HasMaxLength(50).IsRequired(true);
        builder.Property(x => x.TransactionCode).HasMaxLength(50).IsRequired(true);

        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.AccountNumber).IsRequired(true);
        builder.Property(x => x.AccountName).IsRequired(true).HasMaxLength(30);

        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.CustomerNumber).IsRequired(true).HasDefaultValue(0);
    }
}