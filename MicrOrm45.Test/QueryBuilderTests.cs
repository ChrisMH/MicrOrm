using System;
using System.Data;
using MicrOrm.Core;
using MicrOrm.Test.Mock;
using NUnit.Framework;

namespace MicrOrm.Test
{
  public class QueryBuilderTests
  {
    [Test]
    public void Temp()
    {
      var paramColl = MockIDataParameterCollection.Create();

      paramColl.Add(1);
      paramColl.Add(2);

      Assert.AreEqual(2, paramColl.Count);
      Assert.AreEqual(1, paramColl[0]);
      Assert.AreEqual(2, paramColl[1]);
    }

    [Test]
    public void SimpleSelect()
    {
      var conn = MockIDbConnection.Create();
      var sql = "SELECT * FROM table";

      var cmd = conn.CreateCommand();
      QueryBuilder.BuildCommand(cmd, sql);

      Assert.NotNull(cmd);
      Assert.AreEqual(sql, cmd.CommandText);
      Assert.AreEqual(0, cmd.Parameters.Count);
    }

    [Test]
    public void ParameterizedSelect()
    {
      var conn = MockIDbConnection.Create();
      var sql = "SELECT * FROM table WHERE field1=:p0 AND field2=:p1";

      var cmd = conn.CreateCommand();
      QueryBuilder.BuildCommand(cmd, sql, 1, "two");

      Assert.NotNull(cmd);
      Assert.AreEqual(sql, cmd.CommandText);
      Assert.AreEqual(2, cmd.Parameters.Count);

      Assert.AreEqual("p0", ((IDbDataParameter) cmd.Parameters[0]).ParameterName);
      Assert.AreEqual(1, ((IDbDataParameter) cmd.Parameters[0]).Value);

      Assert.AreEqual("p1", ((IDbDataParameter) cmd.Parameters[1]).ParameterName);
      Assert.AreEqual("two", ((IDbDataParameter) cmd.Parameters[1]).Value);
    }

    [TestCase(":p0", new object[] {1, "two"})]
    [TestCase(":p0 :p1", new object[] {1})]
    public void BuildCommandThrowsIfParameterCountMismatch(string sql, object[] parameters)
    {
      var conn = MockIDbConnection.Create();
      var cmd = conn.CreateCommand();
      Console.WriteLine(Assert.Throws<MicrOrmException>(() => QueryBuilder.BuildCommand(cmd, sql, parameters)).Message);
    }


    [TestCase(":p23 :p0", new object[] {1, "two"})]
    [TestCase(":p1", new object[] {1})]
    [TestCase(":p1 :p3 :p0", new object[] {1, 2, 3})]
    public void BuildCommandThrowsIfParameterOrdinalMismatch(string sql, object[] parameters)
    {
      var conn = MockIDbConnection.Create();
      var cmd = conn.CreateCommand();
      Console.WriteLine(Assert.Throws<MicrOrmException>(() => QueryBuilder.BuildCommand(cmd, sql, parameters)).Message);
    }

    [TestCase("", 0)]
    [TestCase("p0", 0)]
    [TestCase("p0 :p1", 1)]
    [TestCase(":p0 :p1", 2)]
    [TestCase(" :p0 :p1", 2)]
    [TestCase(":p0 :p1 ", 2)]
    [TestCase(" :p0 :p1 ", 2)]
    [TestCase(" :p0 :p1)", 2)]
    [TestCase(":p0 :p0", 1)]
    [TestCase(" :p0 :p0", 1)]
    [TestCase(":p0 :p0 ", 1)]
    [TestCase(" :p0 :p0 ", 1)]
    public void FindUniqueParametersFindsTheCorrectNumberOfParameters(string sql, int expectedCount)
    {
      var result = QueryBuilder.FindUniqueParameters(sql);

      Assert.AreEqual(expectedCount, result.Count);
    }

    [TestCase(":pa")]
    [TestCase(" :pa")]
    [TestCase(":pa ")]
    [TestCase(" :pa ")]
    public void FindUniqueParametersThrowsIfParameterFormatIsInvalid(string sql)
    {
      Console.WriteLine(Assert.Throws<MicrOrmException>(() => QueryBuilder.FindUniqueParameters(sql)).Message);
    }
  }
}