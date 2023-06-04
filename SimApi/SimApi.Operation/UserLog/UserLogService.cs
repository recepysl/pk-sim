using SimApi.Data;
using SimApi.Data.Uow;

namespace SimApi.Operation;

public class UserLogService : IUserLogService
{

    private readonly IUnitOfWork unitOfWork;

    public UserLogService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
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
