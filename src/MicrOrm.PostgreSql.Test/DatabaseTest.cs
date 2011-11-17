using System.Data;
using NUnit.Framework;

namespace MicrOrm.PostgreSql.Test
{
  public class DatabaseTest
  {
    [Test]
    public void DatabaseInstanceOpensAndClosesTheDatabase()
    {
      IDbConnection connection;
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        Assert.NotNull(db);
        Assert.NotNull(db.ConnectionProvider);
        Assert.NotNull(db.Connection);
        Assert.AreEqual(ConnectionState.Open, db.Connection.State);
        connection = db.Connection;
      }

      Assert.AreEqual(ConnectionState.Closed, connection.State);
    }
  }
}