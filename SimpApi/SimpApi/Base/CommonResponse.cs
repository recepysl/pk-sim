namespace SimpApi.Base;

public class CommonResponse<T>
{
    public T Data { get; set; }
    public bool Status { get; set; } = true;
    public string Message { get; set; } = "Success";


    public CommonResponse(T data)
    {
        Data = data;
        Status = true;
    }
    public CommonResponse(string error)
    {
        Message = error;
        Status = false;
    }
}
