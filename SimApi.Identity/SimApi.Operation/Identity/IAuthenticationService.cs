using SimApi.Base;
using SimApi.Schema;
using System.Security.Claims;

namespace SimApi.Operation;

public interface IAuthenticationService
{
    public Task<ApiResponse<TokenResponse>> SignIn(TokenRequest request);
    public Task<ApiResponse> SignOut();
    public Task<ApiResponse> ChangePassword(ClaimsPrincipal User, ChangePasswordRequest request);

}
