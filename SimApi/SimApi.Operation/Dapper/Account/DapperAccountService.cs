using AutoMapper;
using Serilog;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation;

public class DapperAccountService : BaseService<Account, AccountRequest, AccountResponse>, IDapperAccountService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public DapperAccountService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public ApiResponse<List<AccountResponse>> ByCustomerId(int customerId)
    {
        try
        {
            var query = "SELECT * FROM dbo.Account WHERE CustomerId = " + customerId;
            var entityList = unitOfWork.DapperAccountRepository.Filter(query);
            var mapped = mapper.Map<List<Account>, List<AccountResponse>>(entityList);
            return new ApiResponse<List<AccountResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetAll Exception");
            return new ApiResponse<List<AccountResponse>>(ex.Message);
        }
    }

    
    public virtual ApiResponse Delete(int Id)
    {
        try
        {
            unitOfWork.DapperAccountRepository.DeleteById(Id);
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Delete Exception");
            return new ApiResponse(ex.Message);
        }
    }

    public virtual ApiResponse<List<AccountResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.DapperAccountRepository.GetAll();
            var mapped = mapper.Map<List<Account>, List<AccountResponse>>(entityList);
            return new ApiResponse<List<AccountResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetAll Exception");
            return new ApiResponse<List<AccountResponse>>(ex.Message);
        }
    }

    public virtual  ApiResponse<AccountResponse> GetById(int id)
    {
        try
        {
            var entity = unitOfWork.DapperAccountRepository.GetById(id);
            if (entity is null)
            {
                Log.Warning("Record not found for Id " + id);
                return new ApiResponse<AccountResponse>("Record not found");
            }

            var mapped = mapper.Map<Account, AccountResponse>(entity);
            return new ApiResponse<AccountResponse>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetById Exception");
            return new ApiResponse<AccountResponse>(ex.Message);
        }
    }

    public virtual ApiResponse Insert(AccountRequest request)
    {
        try
        {
            var entity = mapper.Map<AccountRequest, Account>(request);
            unitOfWork.DapperAccountRepository.Insert(entity);
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Insert Exception");
            return new ApiResponse(ex.Message);
        }
    }

    public virtual ApiResponse Update(int Id, AccountRequest request)
    {
        try
        {
            var entity = mapper.Map<AccountRequest, Account>(request);
            var exist = unitOfWork.DapperAccountRepository.GetById(Id);
            if (exist is null)
            {
                Log.Warning("Record not found for Id " + Id);
                return new ApiResponse("Record not found");
            }

            entity.Id = Id;
            entity.UpdatedAt = DateTime.UtcNow;

            unitOfWork.DapperAccountRepository.Update(entity);
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Update Exception");
            return new ApiResponse(ex.Message);
        }
    }
}
