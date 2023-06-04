using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimApi.Operation;

public class TokenService : ITokenService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserLogService userLogService;
    private readonly JwtConfig jwtConfig;

    public TokenService(IUnitOfWork unitOfWork, IUserLogService userLogService,IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.unitOfWork = unitOfWork;   
        this.userLogService = userLogService;
        this.jwtConfig = jwtConfig.CurrentValue;
    }

    public ApiResponse<TokenResponse> GetToken(TokenRequest request)
    {
        if (request is null)
        {
            return new ApiResponse<TokenResponse>("Request was null");
        }
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
        {
            return new ApiResponse<TokenResponse>("Request props was null");
        }

        request.UserName = request.UserName.Trim().ToLower();
        request.Password = request.Password.Trim();
        
        var user = unitOfWork.Repository<User>().Where(x => x.UserName.Equals(request.UserName)).FirstOrDefault();
        if (user is null)
        {
            Log(request.UserName, LogType.InValidUserName);
            return new ApiResponse<TokenResponse>("Invalid user informations");
        }
        if (user.Password.ToLower() != CreateMD5(request.Password))
        {
            user.PasswordRetryCount++;
            user.LastActivity = DateTime.UtcNow;

            if (user.PasswordRetryCount > 3)
                user.Status = 2;

            unitOfWork.Repository<User>().Update(user);
            unitOfWork.Complete();

            Log(request.UserName, LogType.WrongPassword);
            return new ApiResponse<TokenResponse>("Invalid user informations");
        }

        if (user.Status != 1)
        {
            Log(request.UserName, LogType.InValidUserStatus);
            return new ApiResponse<TokenResponse>("Invalid user status");
        }
        if (user.PasswordRetryCount > 3)
        {
            Log(request.UserName, LogType.PasswordRetryCountExceded);
            return new ApiResponse<TokenResponse>("Password retry count exceded");
        }

        user.LastActivity = DateTime.UtcNow;
        user.Status = 1;


        unitOfWork.Repository<User>().Update(user);
        unitOfWork.Complete();

        TokenResponse response = new();
        response.UserName = request.UserName;
        response.AccessToken = Token(user);
        response.ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration);

        return new ApiResponse<TokenResponse>(response);
    }

    private string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials : new SigningCredentials(new SymmetricSecurityKey(secret),SecurityAlgorithms.HmacSha256Signature)
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
     }

    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim("UserName",user.UserName),
            new Claim("UserId",user.Id.ToString()),
            new Claim("FirstName",user.FirstName),
            new Claim("LastName",user.LastName),
            new Claim("UserName",user.UserName),
            new Claim("LastActivity",user.LastActivity.ToString()),
            new Claim("Role",user.Role),
            new Claim("Status",user.Status.ToString()),
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")
        };

        return claims;
    }

    private void Log(string username , string logType)
    {
        userLogService.Log(username, logType);
    }

    private string CreateMD5(string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();

        }
    }
}
