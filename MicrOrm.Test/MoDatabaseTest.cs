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

    [TestCase(typeof(SqlServerCeDatabaseUtility))]
    [TestCase(typeof(SystemDataSqLiteDatabaseUtility))]
    [TestCase(typeof(DevartDataSqLiteDatabaseUtility))]
    [TestCase(typeof(NpgsqlPostgreSqlDatabaseUtility))]
    [TestCase(typeof(DevartDataPostgreSqlDatabaseUtility))]
    public void database_instance_opens_and_closes_database(Type databaseUtilityType)
    {
      Console.WriteLine(databaseUtilityType.FullName);

      var databaseUtility = ( IDatabaseUtility )databaseUtilityType.Assembly.CreateInstance(databaseUtilityType.FullName, false, BindingFlags.CreateInstance, null, null, null, null);
      databaseUtility.CreateDatabase(null);

      DbConnection connection = null;
      using( var db = new MoDatabase( databaseUtility.ConnectionProvider))
      {
        Assert.NotNull(db);
        Assert.NotNull(db.ConnectionProvider);
        Assert.NotNull(db.Connection);
        Assert.AreEqual(ConnectionState.Open, db.Connection.State);
        connection = db.Connection;
      }

      Assert.AreEqual(ConnectionState.Closed, connection.State);

      databaseUtility.DestroyDatabase();
    }

    //public IDatabaseUtility DatabaseUtility { get; set; }
  }
}