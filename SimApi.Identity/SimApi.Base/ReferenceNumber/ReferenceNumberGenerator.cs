namespace SimApi.Base;

public class ReferenceNumberGenerator
{
    public static string Get()
    {
        return Guid.NewGuid().ToString().ToUpper().Replace("-","").Substring(0,10);
    }
}
