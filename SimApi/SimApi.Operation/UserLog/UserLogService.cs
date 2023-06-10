using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation;

public class UserLogService : BaseService<UserLog, UserLogRequest, UserLogResponse>, IUserLogService
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IMemoryCache memoryCache;
    private readonly IMapper mapper;
    public UserLogService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.memoryCache = memoryCache;
    }

    public ApiResponse<List<UserLogResponse>> GetByFilter(string username, string logType)
    {
        List<UserLogResponse> entityList;
        var checkMemory = memoryCache.TryGetValue(username, out List<UserLogResponse> response);
        if (checkMemory)
        {
            entityList = response;
        }
        else
        {
            entityList = GetByUserName(username).Response;
        }

        entityList = entityList.Where(x=> x.LogType == logType).ToList();
        return new ApiResponse<List<UserLogResponse>>(entityList);
    }

    public ApiResponse<List<UserLogResponse>> GetByUserName(string username)
    {
        var list = unitOfWork.Repository<UserLog>().Where(x => x.UserName == username).ToList();
        var mapped = mapper.Map<List<UserLogResponse>>(list);
        return new ApiResponse<List<UserLogResponse>>(mapped);
    }

    public void Log(string username, string logType)
    {
        UserLog log = new();
        log.LogType = logType;
        log.CreatedAt = DateTime.UtcNow;
        log.TransactionDate = DateTime.UtcNow;
        log.UserName = username;

        unitOfWork.Repository<UserLog>().Insert(log);
        unitOfWork.Complete();
    }
}
