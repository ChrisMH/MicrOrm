using MicrOrm.Test.Utility;
using NUnit.Framework;

namespace MicrOrm.Test.PostgreSqlTests
{
  [TestFixture]
  public class PostgreSqlExecuteNonQueryTests
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
    public void CanInsertRecord()
    {
      using (var db = databaseUtility.ConnectionProvider.Database)
      {
        db.ExecuteNonQuery("INSERT INTO test.user (name, email) VALUES (:p0,:p1)", "Billy", "billy@gmail.com");

        var result = db.ExecuteScalar("SELECT COUNT(*) FROM test.user WHERE name=:p0", "Billy");
        Assert.NotNull(result);
        Assert.AreEqual(1, result);
      }
    }


    private readonly IDatabaseUtility databaseUtility = new PostgreSqlDatabaseUtility(); 
  }
}