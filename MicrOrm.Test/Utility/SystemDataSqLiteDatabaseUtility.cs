using System.Data.SQLite;
using System.IO;
using MicrOrm.Core;

namespace MicrOrm.Test.Utility
{
  public class SystemDataSqLiteDatabaseUtility : SqLiteDatabaseUtility
  {
    public SystemDataSqLiteDatabaseUtility()
    : base("System.Data.SQLite.Connection")
    {
    }
  }
}