
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

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly JwtConfig jwtConfig;


    public AuthenticationService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtConfig = jwtConfig.CurrentValue;
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
