using System;
using System.IO;
using Devart.Data.SQLite;
using MicrOrm.Core;

namespace MicrOrm.Test.Utility
{
  public class DevartDataSqLiteDatabaseUtility : SqLiteDatabaseUtility
  {
    public DevartDataSqLiteDatabaseUtility()
      : base("Devart.Data.SQLite.Connection")
    {
    }
  }
}
