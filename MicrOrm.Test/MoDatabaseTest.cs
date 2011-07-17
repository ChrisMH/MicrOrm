using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using MicrOrm.Core;
using MicrOrm.Test.Utility;
using Xunit;
using Xunit.Extensions;

namespace MicrOrm.Test
{
  public class MoDatabaseTest : IDisposable
  {
    public MoDatabaseTest()
    {
    }

    public void Dispose()
    {
    }

    [Theory]
    [InlineData(typeof(SqlServerCeDatabaseUtility))]
    public void database_instance_opens_and_closes_database(Type databaseUtilityType)
    {
      var databaseUtility = ( IDatabaseUtility )databaseUtilityType.Assembly.CreateInstance(databaseUtilityType.FullName, false, BindingFlags.CreateInstance, null, null, null, null);
      databaseUtility.CreateDatabase();

      DbConnection connection = null;
      using( var db = new MoDatabase( databaseUtility.ConnectionProvider))
      {
        Assert.NotNull(db);
        Assert.NotNull(db.ConnectionProvider);
        Assert.NotNull(db.Connection);
        Assert.Equal(ConnectionState.Open, db.Connection.State);
        connection = db.Connection;
      }

      Assert.Equal(ConnectionState.Closed, connection.State);

      databaseUtility.DestroyDatabase();
    }

    //public IDatabaseUtility DatabaseUtility { get; set; }
  }
}