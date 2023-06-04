using Dapper;
using SimApi.Data.Context;

namespace SimApi.Data.Repository;

public class DapperTransactionRepository : IDapperTransactionRepository
{

    private readonly SimDapperDbContext context;

    public DapperTransactionRepository(SimDapperDbContext context)
    {
        this.context = context;
    }

    public List<Transaction> GetAll()
    {
        var sql = "SELECT * FROM dbo.Transaction";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Transaction>(sql);
            connection.Close();
            return result.ToList();
        }
    }

    public List<Transaction> GetByAccountId(int AccountId)
    {
        var sql = "SELECT * FROM dbo.Transaction WHERE AccountId=@AccountId";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Transaction>(sql, new { AccountId });
            connection.Close();
            return result.ToList();
        }
    }

    public List<Transaction> GetByCustomerId(int CustomerId)
    {
        var sql = "SELECT * FROM dbo.Transaction WHERE CustomerId=@CustomerId";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Transaction>(sql, new { CustomerId });
            connection.Close();
            return result.ToList();
        }
    }

    public Transaction GetById(int Id)
    {
        var sql = "SELECT * FROM dbo.Transaction WHERE Id=@Id";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.QueryFirst<Transaction>(sql, new { Id });
            connection.Close();
            return result;
        }
    }

    public List<Transaction> GetByReferenceNumber(string ReferenceNumber)
    {
        var sql = "SELECT * FROM dbo.Transaction WHERE ReferenceNumber=@ReferenceNumber";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<Transaction>(sql, new { ReferenceNumber });
            connection.Close();
            return result.ToList();
        }
    }
}
