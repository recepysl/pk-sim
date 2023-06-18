namespace SimApi.Base;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}
