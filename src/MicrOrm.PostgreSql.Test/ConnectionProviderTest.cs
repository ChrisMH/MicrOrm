using System;
using MicrOrm.Core;
using NUnit.Framework;
using Utility.Database;

namespace MicrOrm.PostgreSql.Test
{
  public class ConnectionProviderTest
  {
    private readonly IDbConnectionInfo connectionInfo = new GenericDbConnectionInfo {ConnectionStringName = "Test"};

    [Test]
    public void CanCreateWithConnectionInfo()
    {
      var provider = (IConnectionProvider) new MicrOrmConnectionProvider(connectionInfo);

      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionInfo);
      Assert.AreEqual(connectionInfo.ConnectionString, provider.ConnectionInfo.ConnectionString);
      Assert.AreEqual(((IDbProviderInfo)connectionInfo).Provider, ((IDbProviderInfo)provider.ConnectionInfo).Provider);
      Assert.NotNull(((IDbProviderInfo)provider.ConnectionInfo).ProviderFactory);
    }

    [Test]
    public void CanCreateWithConnectionName()
    {
      var provider = (IConnectionProvider) new MicrOrmConnectionProvider(connectionInfo.ConnectionStringName);

      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionInfo);
      Assert.AreEqual(connectionInfo.ConnectionString, provider.ConnectionInfo.ConnectionString);
      Assert.AreEqual(((IDbProviderInfo)connectionInfo).Provider, ((IDbProviderInfo)provider.ConnectionInfo).Provider);
      Assert.NotNull(((IDbProviderInfo)provider.ConnectionInfo).ProviderFactory);
    }

    [Test]
    public void CanCreateWithConnectionStringAndProviderName()
    {
      var provider = (IConnectionProvider) new MicrOrmConnectionProvider(connectionInfo.ConnectionString, ((IDbProviderInfo)connectionInfo).Provider);

      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionInfo);
      Assert.AreEqual(connectionInfo.ConnectionString, provider.ConnectionInfo.ConnectionString);
      Assert.AreEqual(((IDbProviderInfo)connectionInfo).Provider, ((IDbProviderInfo)provider.ConnectionInfo).Provider);
      Assert.NotNull(((IDbProviderInfo)provider.ConnectionInfo).ProviderFactory);
    }

    [Test]
    public void InvalidConnectionStringNameThrows()
    {
      var e = Assert.Throws<ArgumentException>(() => new MicrOrmConnectionProvider("InvalidConnectionStringName"));
      Assert.AreEqual("ConnectionStringName", e.ParamName);
    }
  }
}