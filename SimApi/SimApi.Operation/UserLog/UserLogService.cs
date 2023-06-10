using AutoMapper;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation;

public class UserLogService : BaseService<UserLog, UserLogRequest, UserLogResponse>, IUserLogService
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public UserLogService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
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
