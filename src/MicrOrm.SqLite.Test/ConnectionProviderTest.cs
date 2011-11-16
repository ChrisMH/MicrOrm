using System.Configuration;
using System.Data;
using System.Data.Common;
using MicrOrm.Core;
using MicrOrm.SqLite.Test.Utility;
using NUnit.Framework;

namespace MicrOrm.SqLite.Test
{
  public class ConnectionProviderTest
  {
    [Test]
    public void CanCreateWithConnectionName()
    {
      var provider = (IConnectionProvider) new MicrOrmConnectionProvider(DbUtility.ConnectionStringName);

      Assert.NotNull(provider);

      Assert.NotNull(provider.ProviderFactory);
      Assert.AreEqual(ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ProviderName, provider.ProviderName);
      Assert.AreEqual(new DbConnectionStringBuilder {ConnectionString = ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ConnectionString},
                      provider.ConnectionString);
    }

    [Test]
    public void CanCreateWithConnectionStringAndProviderName()
    {
      var provider = (IConnectionProvider) new MicrOrmConnectionProvider(ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ConnectionString,
                                                                      ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ProviderName);

      Assert.NotNull(provider);

      Assert.NotNull(provider.ProviderFactory);
      Assert.AreEqual(ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ProviderName, provider.ProviderName);
      Assert.AreEqual(new DbConnectionStringBuilder {ConnectionString = ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ConnectionString},
                      provider.ConnectionString);
    }

    [Test]
    public void CanCreateWithConnectionStringBuilderAndProviderName()
    {
      var provider =
        (IConnectionProvider)
        new MicrOrmConnectionProvider(new DbConnectionStringBuilder {ConnectionString = ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ConnectionString},
                                 ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ProviderName);

      Assert.NotNull(provider);

      Assert.NotNull(provider.ProviderFactory);
      Assert.AreEqual(ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ProviderName, provider.ProviderName);
      Assert.AreEqual(new DbConnectionStringBuilder {ConnectionString = ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ConnectionString},
                      provider.ConnectionString);
    }

    [Test]
    public void CanCreateConnection()
    {
      var provider = new MicrOrmConnectionProvider(DbUtility.ConnectionStringName);

      var connection = provider.CreateConnection();

      Assert.NotNull(connection);
      Assert.AreEqual(ConnectionState.Closed, connection.State);

      var connectionStringActual = new DbConnectionStringBuilder {ConnectionString = connection.ConnectionString};

      foreach (string key in provider.ConnectionString.Keys)
      {
        Assert.True(connectionStringActual.ContainsKey(key));
        Assert.AreEqual(provider.ConnectionString[key], connectionStringActual[key]);
      }
    }
  }
}