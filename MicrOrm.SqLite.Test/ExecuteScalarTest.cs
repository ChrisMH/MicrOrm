using NUnit.Framework;

namespace MicrOrm.SqLite.Test
{
  public class ExecuteScalarTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.DbUtility.Seed();
    }

    [Test]
    public void CanGetSimpleCount()
    {
      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT COUNT(*) FROM user");

        Assert.NotNull(result);
        Assert.AreEqual(7, result);
      }
    }

    [Test]
    public void CanGetParameterizedCount()
    {
      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT COUNT(*) FROM user WHERE name=:p0", "Bob");

        Assert.NotNull(result);
        Assert.AreEqual(2, result);
      }
    }

    [Test]
    public void CanGetNullValue()
    {
      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteScalar("SELECT id FROM user WHERE name=:p0", "InvalidName");

        Assert.Null(result);
      }
    }

  }
}