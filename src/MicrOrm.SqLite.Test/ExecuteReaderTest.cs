using System;
using System.Linq;
using MicrOrm.Core;
using NUnit.Framework;

namespace MicrOrm.SqLite.Test
{
  public class ExecuteReaderTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.DbUtility.Seed();
    }

    [Test]
    public void CanExecuteReaderForSimpleSelect()
    {
      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM user").ToList();

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
      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        var result = db.ExecuteReader("SELECT * FROM user WHERE name=:p0", "Bob").ToList();

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
    public void ExecuteReaderWithNullParameterThrows()
    {
      using (var db = GlobalTest.DbUtility.ConnectionProvider.Database)
      {
        Assert.Throws<MicrOrmException>(() => db.ExecuteReader("SELECT * FROM user WHERE email=:p0", null).ToList());
      }
    }
  }
}