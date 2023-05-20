namespace SimApi.Base;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Response { get; set; }

    public BaseResponse(bool isSuccess)
    {
        Success = isSuccess;
        Response = default;
        Message = isSuccess ? "Success" : "Error";
    }
    public BaseResponse(T data)
    {
        Success = true;
        Response = data;
        Message = "Success";
    }
    public BaseResponse(string message)
    {
        Success = false;
        Response = default;
        Message = message;
    }
}
