using SimApi.Base;

namespace SimApi.Data.Domain;

public class Product : BaseModel
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Tag { get; set; }

}
