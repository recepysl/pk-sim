using SimApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data.Domain;


[Table("Product", Schema = "dbo")]
public class Product : BaseModel
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Tag { get; set; }

}
