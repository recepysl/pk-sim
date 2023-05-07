using SimApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data.Domain;


[Table("Category", Schema = "dbo")]
public class Category : BaseModel
{
    public string Name { get; set; }
    public int Order { get; set; }

}
