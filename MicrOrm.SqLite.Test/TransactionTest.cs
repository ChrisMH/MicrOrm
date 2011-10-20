using System.Linq;
using NUnit.Framework;

namespace MicrOrm.SqLite.Test
{
  public class TransactionTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.DbUtility.Seed();
    }

    [Test]
    public void TransactionCommit()
    {
      using (var trans = GlobalTest.DbUtility.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteNonQuery("INSERT INTO user (name, email) VALUES(:p0, :p1)", "Bobby", "bobby@gmail.com");
        trans.Commit();
      }

      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM user WHERE name=:p0 OR name=:p1", "Billy", "Bobby").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);
      }
    }

    [Test]
    public void TransactionRollback()
    {
      using (var trans = GlobalTest.DbUtility.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteNonQuery("INSERT INTO user (name, email) VALUES(:p0, :p1)", "Bobby", "bobby@gmail.com");

        trans.Rollback();
      }

      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM user WHERE name=:p0 OR name=:p1", "Billy", "Bobby").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
      }
    }

    [Test]
    public void TransactionAutoRollback()
    {
      using (var trans = GlobalTest.DbUtility.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteNonQuery("INSERT INTO user (name, email) VALUES(:p0, :p1)", "Bobby", "bobby@gmail.com");
      }

      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM user WHERE name=:p0 OR name=:p1", "Billy", "Bobby").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
      }
    }
  }
}