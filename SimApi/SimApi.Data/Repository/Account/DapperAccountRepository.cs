using Dapper;
using SimApi.Data.Context;
using System.Data;

namespace SimApi.Data.Repository;

public class DapperAccountRepository : IDapperRepository<Account>
{

    private readonly SimDapperDbContext context;

    public DapperAccountRepository(SimDapperDbContext context)
    {
        this.context = context;
    }

    public void DeleteById(int Id)
    {
        var sqlAccount = "DELETE FROM dbo.\"Account\" WHERE \"Id\"=@Id";
        var sqlTransaction = "DELETE FROM dbo.\"Transaction\" WHERE \"AccountId\"=@Id";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            connection.Execute(sqlAccount, new { Id });
            connection.Execute(sqlTransaction, new { Id });
            connection.Close();
        }
    }

    public List<Account> Filter(string sql)
    {
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Account>(sql);
            connection.Close();
            return result.ToList();
        }
    }

    public List<Account> GetAll()
    {
        var sql = "SELECT * FROM dbo.\"Account\"";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Account>(sql);
            connection.Close();
            return result.ToList();
        }
    }

    public Account GetById(int Id)
    {
        var sql = "SELECT * FROM dbo.\"Account\" WHERE \"Id\"=@Id";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.QueryFirst<Account>(sql, new { Id});
            connection.Close();
            return result;
        }
    }

    public void Insert(Account entity)
    {
        var sql = "INSERT INTO dbo.\"Account\" (\"CreatedAt\",\"CreatedBy\",\"CustomerId\",\"AccountNumber\",\"Name\",\"OpenDate\",\"IsValid\",\"Balance\")" +
            "VALUES (@CreatedAt,@CreatedBy,@CustomerId,@AccountNumber,@Name,@OpenDate,@IsValid,@Balance)";

        entity.CreatedAt = DateTime.UtcNow;
        entity.OpenDate = DateTime.UtcNow;
        entity.CreatedBy = "sim@sim.com";

        var parameters = new DynamicParameters();
        parameters.Add("CreatedAt", entity.CreatedAt, DbType.DateTime);
        parameters.Add("CreatedBy", entity.CreatedBy, DbType.String);
        parameters.Add("CustomerId", entity.CustomerId, DbType.Int32);
        parameters.Add("AccountNumber", entity.AccountNumber, DbType.Int32);
        parameters.Add("Name", entity.Name, DbType.String);
        parameters.Add("OpenDate", entity.OpenDate, DbType.DateTime);
        parameters.Add("IsValid", entity.IsValid, DbType.Boolean);
        parameters.Add("Balance", entity.Balance, DbType.Decimal);

        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Execute(sql, parameters);
            connection.Close();      
        }
    }

    public void Update(Account entity)
    {
        throw new NotImplementedException();
    }
}
