using System.Data;
using Buddy.Database;

namespace MicrOrm
{
    public interface IConnectionProvider
    {
        IDbConnectionInfo ConnectionInfo { get; }
        IDbConnection CreateConnection();
        IDatabase Database { get; }
        ITransaction Transaction { get; }
    }
}