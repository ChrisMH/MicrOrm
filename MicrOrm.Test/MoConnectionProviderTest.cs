using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using MicrOrm.Core;
using MicrOrm.Test.Utility;
using System.Reflection;
using NUnit.Framework;

namespace MicrOrm.Test
{
  [TestFixture]
  public class MoConnectionProviderTest
  {
    [TestCase(PostgreSqlDatabaseUtility.ConnectionStringName)]
    //[TestCase(SqLiteDatabaseUtility.ConnectionStringName)]
    //[TestCase(SqlServerCeDatabaseUtility.ConnectionStringName)]
    public void CanCreateWithConnectionName(string connectionStringName)
    {
      Console.WriteLine(connectionStringName);

      var provider = (IMoConnectionProvider) new MoConnectionProvider(connectionStringName);

      Assert.NotNull(provider);

      Assert.NotNull(provider.ProviderFactory);
      Assert.AreEqual(ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName, provider.ProviderName);
      Assert.AreEqual(new DbConnectionStringBuilder { ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString },
                   provider.ConnectionString);
    }

    [TestCase(PostgreSqlDatabaseUtility.ConnectionStringName)]
    //[TestCase(SqLiteDatabaseUtility.ConnectionStringName)]
    //[TestCase(SqlServerCeDatabaseUtility.ConnectionStringName)]
    public void CanCreateWithConnectionStringAndProviderName(string connectionStringName)
    {
      Console.WriteLine(connectionStringName);

      var provider = (IMoConnectionProvider) new MoConnectionProvider(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString,
                                                                      ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName);

      Assert.NotNull(provider);

      Assert.NotNull(provider.ProviderFactory);
      Assert.AreEqual(ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName, provider.ProviderName);
      Assert.AreEqual(new DbConnectionStringBuilder { ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString },
                   provider.ConnectionString);
    }

    [TestCase(PostgreSqlDatabaseUtility.ConnectionStringName)]
    //[TestCase(SqLiteDatabaseUtility.ConnectionStringName)]
    //[TestCase(SqlServerCeDatabaseUtility.ConnectionStringName)]
    public void CanCreateWithConnectionStringBuilderAndProviderName(string connectionStringName)
    {
      Console.WriteLine(connectionStringName);

      var provider =
      (IMoConnectionProvider)
      new MoConnectionProvider(new DbConnectionStringBuilder { ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString },
                               ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName);

      Assert.NotNull(provider);

      Assert.NotNull(provider.ProviderFactory);
      Assert.AreEqual(ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName, provider.ProviderName);
      Assert.AreEqual(new DbConnectionStringBuilder { ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString },
                   provider.ConnectionString);
    }

    [TestCase(PostgreSqlDatabaseUtility.ConnectionStringName)]
    //[TestCase(SqLiteDatabaseUtility.ConnectionStringName)]
    //[TestCase(SqlServerCeDatabaseUtility.ConnectionStringName)]
    public void CanCreateConnection(string connectionStringName)
    {
      Console.WriteLine(connectionStringName);

      var provider = new MoConnectionProvider(connectionStringName);

      var connection = provider.CreateConnection();

      Assert.NotNull(connection);
      Assert.AreEqual(ConnectionState.Closed, connection.State);

      var connectionStringActual = new DbConnectionStringBuilder { ConnectionString = connection.ConnectionString };

      foreach (string key in provider.ConnectionString.Keys)
      {
        Assert.True(connectionStringActual.ContainsKey(key));
        Assert.AreEqual(provider.ConnectionString[key], connectionStringActual[key]);
      }
    }

    [TestCase(typeof(PostgreSqlDatabaseUtility))]
    //[TestCase(typeof(SqLiteDatabaseUtility))]
    //[TestCase(typeof(SqlServerCeDatabaseUtility))]
    public void CanCreateDatabase(Type databaseUtilityType)
    {       
      Console.WriteLine(databaseUtilityType.FullName);

      var databaseUtility = ( IDatabaseUtility )databaseUtilityType.Assembly.CreateInstance(databaseUtilityType.FullName, false, BindingFlags.CreateInstance, null, null, null, null);
      databaseUtility.CreateDatabase(null);

      using (var db = databaseUtility.ConnectionProvider.Database)
      {
        Assert.NotNull(db);
        Assert.True(db.Connection.State == ConnectionState.Open);
      }

      databaseUtility.DestroyDatabase();
    }
  }
}