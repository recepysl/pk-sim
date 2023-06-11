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
    public UserLogService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
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
