using System.Linq;
using NUnit.Framework;

namespace MicrOrm.PostgreSql.Test
{
  public class TransactionTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.TestDb.Seed();
    }

    [Test]
    public void TransactionCommit()
    {
      using (var trans = GlobalTest.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteReader("INSERT INTO test.user (name, email) VALUES(:p0, :p1) RETURNING id", "Bobby", "bobby@gmail.com").Single();
        trans.Commit();
      }

      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.user WHERE name=ANY(:p0)", (object) new[] {"Billy", "Bobby"}).ToList();

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);
      }
    }

    [Test]
    public void TransactionRollback()
    {
      using (var trans = GlobalTest.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Bobby", "bobby@gmail.com");

        trans.Rollback();
      }

      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.user WHERE name=ANY(:p0)", (object) new[] {"Billy", "Bobby"}).ToList();

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
      }
    }

    [Test]
    public void TransactionAutoRollback()
    {
      using (var trans = GlobalTest.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Bobby", "bobby@gmail.com");
      }

      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.user WHERE name=ANY(:p0)", (object) new[] {"Billy", "Bobby"}).ToList();

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
      }
    }
  }
}