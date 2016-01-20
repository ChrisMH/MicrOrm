using NUnit.Framework;

namespace MicrOrm.PostgreSql.Test
{
  public class ExecuteScalarTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.TestDb.Seed();
    }

    [Test]
    public void CanGetSimpleCount()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT COUNT(*) FROM test.user");

        Assert.NotNull(result);
        Assert.AreEqual(7, result);
      }
    }

    [Test]
    public void CanGetParameterizedCount()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT COUNT(*) FROM test.user WHERE name=:p0", "Bob");

        Assert.NotNull(result);
        Assert.AreEqual(2, result);
      }
    }

    [Test]
    public void CanGetNullValue()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT id FROM test.user WHERE name=:p0", "InvalidName");

        Assert.Null(result);
      }
    }

    [Test]
    public void CanGetValueFromFunction()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT test.get_user_id(:p0)", "Bob");

        Assert.True(result > 0);
      }
    }

    [Test]
    public void CanGetNullValueFromFunction()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT test.get_user_id(:p0)", "InvalidName");

        Assert.Null(result);
      }
    }
  }
}