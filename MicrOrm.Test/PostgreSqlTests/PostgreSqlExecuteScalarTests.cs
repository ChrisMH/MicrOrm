using MicrOrm.Test.Utility;
using NUnit.Framework;

namespace MicrOrm.Test.PostgreSqlTests
{
  [TestFixture]
  public class PostgreSqlExecuteScalarTests
  {
    [SetUp]
    public void SetUp()
    {
      databaseUtility.CreateDatabase(Properties.Resource.PostgreSql);
    }

    [TearDown]
    public void TearDown()
    {
      databaseUtility.DestroyDatabase();
    }

    [Test]
    public void CanGetSimpleCount()
    {
      using (var db = databaseUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT COUNT(*) FROM test.user");

        Assert.NotNull(result);
        Assert.AreEqual(7, result);
      }
    }

    [Test]
    public void CanGetParameterizedCount()
    {
      using (var db = databaseUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT COUNT(*) FROM test.user WHERE name=:p0", "Bob");

        Assert.NotNull(result);
        Assert.AreEqual(2, result);
      }
    }

    [Test]
    public void CanGetNullValue()
    {
      using (var db = databaseUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT id FROM test.user WHERE name=:p0", "InvalidName");

        Assert.Null(result);
      }
    }

    private readonly IDatabaseUtility databaseUtility = new PostgreSqlDatabaseUtility();
  }
}