using SimApi.Base;
using SimApi.Schema;
using System.Security.Claims;

namespace SimApi.Operation;

public interface IUserService
{
    public Task<ApiResponse<ApplicationUserResponse>> GetUser(ClaimsPrincipal User);
    public Task<ApiResponse> Insert(ApplicationUserRequest request);
    public Task<ApiResponse> Update(ApplicationUserRequest request);
    public Task<ApiResponse> Delete(string id);
    public Task<ApiResponse<List<ApplicationUserResponse>>> GetAll();
    public Task<ApiResponse<ApplicationUserResponse>> GetById(string id);
    public Task<ApiResponse<string>> GetUserId(ClaimsPrincipal User);
}
