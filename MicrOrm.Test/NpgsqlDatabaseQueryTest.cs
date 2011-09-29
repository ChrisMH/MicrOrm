using System;
using System.Linq;
using MicrOrm.Core;
using MicrOrm.Test.Utility;
using NUnit.Framework;

namespace MicrOrm.Test
{
  [TestFixture]
  public class NpgsqlDatabaseQueryTest
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
    public void CanExecuteReaderForSimpleSelect()
    {
      using(var db = new MoDatabase(databaseUtility.ConnectionProvider))
      {
        var result = db.ExecuteReader("SELECT * FROM test.user").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(7, result.Count);
        foreach (var row in result)
        {
          Assert.NotNull(row.Id);
          Assert.NotNull(row.Name);
          Console.WriteLine("{0}, {1}, {2}", row.Id, row.Name, row.Email ?? "NULL");
        }
      }
    }

    [Test]
    public void CanExecuteReaderForParameterizedSelect()
    {
      using(var db = new MoDatabase(databaseUtility.ConnectionProvider))
      {
        var result = db.ExecuteReader("SELECT * FROM test.user WHERE name=:p0", "Bob").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);

        foreach (var row in result)
        {
          Assert.NotNull(row.Id);
          Assert.NotNull(row.Name);
          Console.WriteLine("{0}, {1}, {2}", row.Id, row.Name, row.Email ?? "NULL");
        }
      }
    }

    [Test]
    public void CanExecuteReaderForFunction()
    {
      using(var db = new MoDatabase(databaseUtility.ConnectionProvider))
      {
        var result = db.ExecuteReader("SELECT * FROM test.get_users()").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(7, result.Count);

        foreach (var row in result)
        {
          Assert.NotNull(row.Name);
          Console.WriteLine("{0}, {1}", row.Name, row.Email ?? "NULL");
        }
      }
    }

    [Test]
    public void CanExecuteReaderForParameterizedFunction()
    {
      using(var db = new MoDatabase(databaseUtility.ConnectionProvider))
      {
        var result = db.ExecuteReader("SELECT * FROM test.get_users(:p0)", new int[] {1, 3}).ToList();

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);

        foreach (var row in result)
        {
          Assert.NotNull(row.Name);
          Console.WriteLine("{0}, {1}", row.Name, row.Email ?? "NULL");
        }
      }
    }
    private IDatabaseUtility databaseUtility = new NpgsqlPostgreSqlDatabaseUtility();
  }
}