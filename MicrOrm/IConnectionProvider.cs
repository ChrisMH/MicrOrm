using System.Data.Common;

namespace MicrORM
{
  public interface IConnectionProvider
  {
    DbConnection CreateConnection();
  }
}