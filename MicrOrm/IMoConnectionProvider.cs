using System.Data.Common;

namespace MicrOrm
{
  public interface IMoConnectionProvider
  {
    DbConnection CreateConnection();

    DbConnectionStringBuilder ConnectionString { get; }
    DbProviderFactory ProviderFactory { get; }
  }
}