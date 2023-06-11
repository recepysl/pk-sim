using SimApi.Base;
using SimApi.Data;
using SimApi.Schema;

namespace SimApi.Operation;

public interface IUserLogService : IBaseService<UserLog,UserLogRequest,UserLogResponse>
{
    void Log(string username,string logType);
    ApiResponse<List<UserLogResponse>> GetByUserName(string username);
}
