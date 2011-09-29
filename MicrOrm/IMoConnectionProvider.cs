using System.Data.Common;

namespace MicrOrm
{
  public interface IMoConnectionProvider
  {
    DbConnection CreateConnection();

    DbConnectionStringBuilder ConnectionString { get; }
    string ProviderName { get; }
    DbProviderFactory ProviderFactory { get; }

    IMoDatabase Database { get; }
  }
}