using System.Data.Common;
using MicrOrm.Core;

namespace MicrOrm
{
  public interface IMoConnectionProvider
  {
    DbConnection CreateConnection();

    DbConnectionStringBuilder ConnectionString { get; }
    string ProviderName { get; }
    DbProviderFactory ProviderFactory { get; }

    IMoDatabase Database { get; }
    IMoTransaction Transaction { get; }
  }
}