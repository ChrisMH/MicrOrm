using System.Data;
using Utility.Database;

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