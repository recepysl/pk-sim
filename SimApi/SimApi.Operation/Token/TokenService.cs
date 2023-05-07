using SimApi.Data.Repository;
using SimApi.Schema;

namespace SimApi.Operation.Token;

public class TokenService : ITokenService
{
    private readonly IUserRepository repository;

    public TokenService(IUserRepository repository)
    {
        this.repository = repository;   
    }

    public TokenResponse GetToken(TokenRequest request)
    {
        if (request is null)
        {

        }
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
        {

        }

        request.UserName = request.UserName.Trim().ToLower();
        var user = repository.GetByUsername(request.UserName);
        if (user is null)
        {

        }

        if (user.Status != 1 )
        {

        }
        if (user.PasswordRetryCount > 3)
        {

        }

        if (user.Password != CreateMD5(request.Password))
        {

        }



        user.LastActivity = DateTime.UtcNow;
        user.Status = 1;
        repository.Update(user);
        repository.Complete();

        TokenResponse response = new();
        response.UserName = request.UserName;
        response.ExpireTime = DateTime.MinValue;



        return null;
    }


    private string CreateMD5(string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);

        }
    }
}
