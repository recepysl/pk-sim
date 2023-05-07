using SimApi.Base;

namespace SimApi.Schema;

public class ProductRequest : BaseRequest
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Tag { get; set; }
}
