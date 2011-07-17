using System.Data.Common;

namespace MicrOrm
{
  public interface IConnectionProvider
  {
    DbConnection CreateConnection();

    DbConnectionStringBuilder ConnectionString { get; }
    DbProviderFactory ProviderFactory { get; }
  }
}