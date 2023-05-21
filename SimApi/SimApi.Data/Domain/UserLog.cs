using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data;

[Table("UserLog", Schema = "dbo")]
public class UserLog : BaseModel
{
    public string UserName { get; set; }   
    public DateTime TransactionDate { get; set; }
    public string LogType { get; set; }
}



public class UserLogConfiguration : IEntityTypeConfiguration<UserLog>
{
    public void Configure(EntityTypeBuilder<UserLog> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.TransactionDate).IsRequired(true);
        builder.Property(x => x.LogType).IsRequired(true).HasMaxLength(20);
    }
}