
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SimApi.Base;
using SimApi.Data.Domain;
using SimApi.Schema;
using System.Security.Claims;

namespace SimApi.Operation;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;


    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<ApplicationUserResponse>> GetUser(ClaimsPrincipal User)
    {
        var user = await userManager.GetUserAsync(User);
        var mapped = mapper.Map<ApplicationUserResponse>(user);
        return new ApiResponse<ApplicationUserResponse>(mapped);
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
        var list = userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        var mapped = mapper.Map<ApplicationUserResponse>(list);
        return new ApiResponse<ApplicationUserResponse>(mapped);
    }
    public async Task<ApiResponse<string>> GetUserId(ClaimsPrincipal User)
    {
        var id = userManager.GetUserId(User);
        return new ApiResponse<string>(id);
    }
}
