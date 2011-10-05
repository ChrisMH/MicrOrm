using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using MicrOrm.Core;
using MicrOrm.Test.Utility;
using NUnit.Framework;

namespace MicrOrm.Test
{
  [TestFixture]
  public class MoDatabaseTest
  {
    public MoDatabaseTest()
    {
    }

    [TestCase(typeof(PostgreSqlDatabaseUtility))]
    //[TestCase(typeof(SqLiteDatabaseUtility))]
    //[TestCase(typeof(SqlServerCeDatabaseUtility))]
    public void DatabaseInstanceOpensAndClosesTheDatabase(Type databaseUtilityType)
    {
      Console.WriteLine(databaseUtilityType.FullName);

      var databaseUtility = ( IDatabaseUtility )databaseUtilityType.Assembly.CreateInstance(databaseUtilityType.FullName, false, BindingFlags.CreateInstance, null, null, null, null);
      databaseUtility.CreateDatabase(null);

      try
      {

        IDbConnection connection = null;
        using (var db = new MoDatabase(databaseUtility.ConnectionProvider))
        {
          Assert.NotNull(db);
          Assert.NotNull(db.ConnectionProvider);
          Assert.NotNull(db.Connection);
          Assert.AreEqual(ConnectionState.Open, db.Connection.State);
          connection = db.Connection;
        }

        Assert.AreEqual(ConnectionState.Closed, connection.State);
      }
      finally
      {
        databaseUtility.DestroyDatabase();
      }
    }

  }
}