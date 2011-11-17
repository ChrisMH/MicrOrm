using NUnit.Framework;

namespace MicrOrm.PostgreSql.Test
{
  public class ExecuteNonQueryTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.TestDb.Seed();
    }

    [Test]
    public void CanInsertRecord()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        db.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES (:p0,:p1)", "Billy", "billy@gmail.com");

        var result = db.ExecuteScalar("SELECT COUNT(*) FROM test.user WHERE name=:p0", "Billy");
        Assert.NotNull(result);
        Assert.AreEqual(1, result);
      }
    }
  }
}