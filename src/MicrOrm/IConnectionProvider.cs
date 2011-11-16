using System.Data.Common;
using MicrOrm.Core;

namespace MicrOrm
{
  public interface IConnectionProvider
  {
    DbConnection CreateConnection();

    DbConnectionStringBuilder ConnectionString { get; }
    string ProviderName { get; }
    DbProviderFactory ProviderFactory { get; }

    IDatabase Database { get; }
    ITransaction Transaction { get; }
  }
}