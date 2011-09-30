using System.Linq;
using MicrOrm.Test.Utility;
using NUnit.Framework;

namespace MicrOrm.Test.PostgreSqlTests
{
  [TestFixture]
  public class PostgreSqlTransactionTests
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
    public void TransactionCommit()
    {
      using(var trans = databaseUtility.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Bobby", "bobby@gmail.com");

        trans.Commit();
      }

      using(var db = databaseUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.user WHERE name=ANY(:p0)", (object)new[] { "Billy", "Bobby" }).ToList();

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);
      }
    }

    [Test]
    public void TransactionRollback()
    {
      using (var trans = databaseUtility.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Bobby", "bobby@gmail.com");

        trans.Rollback();
      }

      using (var db = databaseUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.user WHERE name=ANY(:p0)", (object)new[] { "Billy", "Bobby" }).ToList();

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
      }
    }

    [Test]
    public void TransactionAutoRollback()
    {
      using (var trans = databaseUtility.ConnectionProvider.Transaction)
      {
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Billy", "billy@gmail.com");
        trans.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES(:p0, :p1)", "Bobby", "bobby@gmail.com");
      }

      using (var db = databaseUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.user WHERE name=ANY(:p0)", (object)new [] { "Billy", "Bobby" }).ToList();

        Assert.NotNull(result);
        Assert.AreEqual(0, result.Count);
      }
    }

    private readonly IDatabaseUtility databaseUtility = new PostgreSqlDatabaseUtility(); 
  }
}