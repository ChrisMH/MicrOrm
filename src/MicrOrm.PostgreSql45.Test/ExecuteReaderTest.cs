using System;
using System.Linq;
using MicrOrm.Core;
using NUnit.Framework;

namespace MicrOrm.PostgreSql.Test
{
  public class ExecuteReaderTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.TestDb.Seed();
    }

    [Test]
    public void CanExecuteReaderForSimpleSelect()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.user").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(7, result.Count);
        foreach (var row in result)
        {
          Assert.NotNull(row["Id"]);
          Assert.NotNull(row["Name"]);
          Console.WriteLine("{0}, {1}, {2}", row["Id"], row["Name"], row["Email"] ?? "NULL");
        }
      }
    }

    [Test]
    public void CanExecuteReaderForParameterizedSelect()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.user WHERE name=:p0", "Bob").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);

        foreach (var row in result)
        {
          Assert.NotNull(row["Id"]);
          Assert.NotNull(row["Name"]);
          Console.WriteLine("{0}, {1}, {2}", row["Id"], row["Name"], row["Email"] ?? "NULL");
        }
      }
    }

    [Test]
    public void CanExecuteReaderForFunction()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.get_users()").ToList();

        Assert.NotNull(result);
        Assert.AreEqual(7, result.Count);

        foreach (var row in result)
        {
          Assert.NotNull(row["Name"]);
          Console.WriteLine("{0}, {1}", row["Name"], row["Email"] ?? "NULL");
        }
      }
    }

    [Test]
    public void CanExecuteReaderForParameterizedFunction()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM test.get_users(:p0)", (object)new string[] {"Fred", "Ed"}).ToList();

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);

        foreach (var row in result)
        {
          Assert.NotNull(row["Name"]);
          Console.WriteLine("{0}, {1}", row["Name"], row["Email"] ?? "NULL");
        }
      }
    }

    [Test]
    public void ExecuteReaderWithNullParameterThrows()
    {
      using (var db = GlobalTest.ConnectionProvider.Database)
      {
        Assert.Throws<MicrOrmException>(() => db.ExecuteReader("SELECT * FROM test.user WHERE email=:p0", null).ToList());
      }
    }
  }
}