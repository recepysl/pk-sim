
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimApi.Base;
using SimApi.Data.Domain;
using SimApi.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimApi.Operation;

public class IdentityUserService : IIdentityUserService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IMapper mapper;
    private readonly JwtConfig jwtConfig;


    public IdentityUserService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptionsMonitor<JwtConfig> jwtConfig, IMapper mapper)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtConfig = jwtConfig.CurrentValue;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<TokenResponse>> SignIn(TokenRequest request)
    {
        if (request is null)
        {
            return new ApiResponse<TokenResponse>("Request was null");
        }
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
        {
            return new ApiResponse<TokenResponse>("Request was null");
        }

        var loginResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);
        if (!loginResult.Succeeded)
        {
            return new ApiResponse<TokenResponse>("Invalid user");
        }

        var user = await userManager.FindByNameAsync(request.UserName);

        string token = Token(user);
        TokenResponse tokenResponse = new TokenResponse
        {
            AccessToken = token,
            ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            UserName = user.UserName
        };

        return new ApiResponse<TokenResponse>(tokenResponse);
    }

    public async Task<ApiResponse> SignOut()
    {
        await signInManager.SignOutAsync();
        return new ApiResponse();
    }

    public async Task<ApiResponse<ApplicationUserResponse>> GetUser(ClaimsPrincipal User)
    {
        var user = await userManager.GetUserAsync(User);
        var mapped = mapper.Map<ApplicationUserResponse>(user);
        return new ApiResponse<ApplicationUserResponse>(mapped);
    }

    public async Task<ApiResponse> ChangePassword(ClaimsPrincipal User, ChangePasswordRequest request)
    {
        if (request is null)
        {
            return new ApiResponse("Request was null");
        }
        if (string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.OldPassword))
        {
            return new ApiResponse("Request was null");
        }

        var user = await userManager.GetUserAsync(User);
        var response = await userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
        if (!response.Succeeded)
        {
            return new ApiResponse("Change password error");
        }
        return new ApiResponse();
    }


    public async Task<ApiResponse> Insert(ApplicationUserRequest request)
    {
        if (request is null)
        {
            return new ApiResponse("Request was null");
        }
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email))
        {
            return new ApiResponse("Request was null");
        }

        var entity = mapper.Map<ApplicationUser>(request);
        entity.EmailConfirmed = true;
        entity.TwoFactorEnabled = false;

        var response = await userManager.CreateAsync(entity, request.Password);
        if (!response.Succeeded)
        {
            return new ApiResponse(response.Errors.FirstOrDefault()?.Description);
        }

        return new ApiResponse();
    }
    public async Task<ApiResponse> Update(ApplicationUserRequest request)
    {
        if (request is null)
        {
            return new ApiResponse("Request was null");
        }
        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email))
        {
            return new ApiResponse("Request was null");
        }

        var mapped = mapper.Map<ApplicationUser>(request);
        await userManager.UpdateAsync(mapped);
      
        return new ApiResponse();
    }

    public async Task<ApiResponse> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return new ApiResponse("Request was null");
        }

        var user = userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        await userManager.DeleteAsync(user);
        
        return new ApiResponse();
    }


    public async Task<ApiResponse<List<ApplicationUserResponse>>> GetAll()
    {
        var list = userManager.Users.ToList();
        var mapped = mapper.Map<List<ApplicationUserResponse>>(list);
        return new ApiResponse<List<ApplicationUserResponse>>(mapped);
    }
    public async Task<ApiResponse<ApplicationUserResponse>> GetById(string id)
    {
        var list = userManager.Users.Where(x=> x.Id == id).FirstOrDefault();
        var mapped = mapper.Map<ApplicationUserResponse>(list);
        return new ApiResponse<ApplicationUserResponse>(mapped);
    }
    public async Task<ApiResponse<string>> GetUserId(ClaimsPrincipal User)
    {
        var id = userManager.GetUserId(User);
        return new ApiResponse<string>(id);
    }

    private string Token(ApplicationUser user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }

    private Claim[] GetClaims(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim("UserName",user.UserName),
            new Claim("UserId",user.Id.ToString()),
            new Claim("FirstName",user.FirstName),
            new Claim("LastName",user.LastName),
            new Claim("UserName",user.UserName)
        };

        return claims;
    }

}
