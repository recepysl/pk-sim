using SimApi.Base;

namespace SimApi.Schema;

public class CategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public int Order { get; set; }
}
