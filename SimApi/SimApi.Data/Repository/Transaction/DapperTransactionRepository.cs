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

    public List<TransactionView> GetAll()
    {
        var sql = "SELECT * FROM dbo.\"vTransactionReport\"";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<TransactionView>(sql);
            connection.Close();
            return result.ToList();
        }
    }

    public List<TransactionView> GetByAccountId(int AccountId)
    {
        var sql = "SELECT * FROM dbo.\"vTransactionReport\" WHERE \"AccountId\"=@AccountId";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<TransactionView>(sql, new { AccountId });
            connection.Close();
            return result.ToList();
        }
    }

    public List<TransactionView> GetByCustomerId(int CustomerId)
    {
        var sql = "SELECT * FROM dbo.\"vTransactionReport\" WHERE \"CustomerId\"=@CustomerId";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<TransactionView>(sql, new { CustomerId });
            connection.Close();
            return result.ToList();
        }
    }

    public TransactionView GetById(int Id)
    {
        var sql = "SELECT * FROM dbo.\"vTransactionReport\" WHERE \"Id\"=@Id";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.QueryFirst<TransactionView>(sql, new { Id });
            connection.Close();
            return result;
        }
    }

    public List<TransactionView> GetByReferenceNumber(string ReferenceNumber)
    {
        var sql = "SELECT * FROM dbo.\"vTransactionReport\" WHERE \"ReferenceNumber\"=@ReferenceNumber";
        using (var connection = context.CreateConnection())
        {
            connection.Open();
            var result = connection.Query<TransactionView>(sql, new { ReferenceNumber });
            connection.Close();
            return result.ToList();
        }
    }
}
