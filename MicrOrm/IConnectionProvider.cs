using System.Data.Common;

namespace MicrOrm
{
  public interface IConnectionProvider
  {
    DbConnection CreateConnection();
  }
}