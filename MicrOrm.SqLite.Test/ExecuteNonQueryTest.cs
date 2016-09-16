using NUnit.Framework;

namespace MicrOrm.SqLite.Test
{
  public class ExecuteNonQueryTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.DbUtility.Seed();
    }

    [Test]
    public void CanInsertRecord()
    {
      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        db.ExecuteNonQuery("INSERT INTO user (name, email) VALUES (:p0,:p1)", "Billy", "billy@gmail.com");

        var result = db.ExecuteScalar("SELECT COUNT(*) FROM user WHERE name=:p0", "Billy");
        Assert.NotNull(result);
        Assert.AreEqual(1, result);
      }
    }
  }
}