using System.Configuration;
using System.Data;
using System.Data.Common;
using MicrOrm.Core;
using MicrOrm.PostgreSql.Test.Utility;
using NUnit.Framework;

namespace MicrOrm.PostgreSql.Test
{
  public class ConnectionProviderTest
  {
    [Test]
    public void CanCreateWithConnectionName()
    {
      var provider = (IMoConnectionProvider) new MoConnectionProvider(DbUtility.ConnectionStringName);

      Assert.NotNull(provider);

      Assert.NotNull(provider.ProviderFactory);
      Assert.AreEqual(ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ProviderName, provider.ProviderName);
      Assert.AreEqual(new DbConnectionStringBuilder {ConnectionString = ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ConnectionString},
                      provider.ConnectionString);
    }

    [Test]
    public void CanCreateWithConnectionStringAndProviderName()
    {
      var provider = (IMoConnectionProvider) new MoConnectionProvider(ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ConnectionString,
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
        (IMoConnectionProvider)
        new MoConnectionProvider(new DbConnectionStringBuilder {ConnectionString = ConfigurationManager.ConnectionStrings[DbUtility.ConnectionStringName].ConnectionString},
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
      var provider = new MoConnectionProvider(DbUtility.ConnectionStringName);

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