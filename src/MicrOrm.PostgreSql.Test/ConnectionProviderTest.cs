using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using MicrOrm.Core;
using NUnit.Framework;
using Utility.Database;

namespace MicrOrm.PostgreSql.Test
{
  public class ConnectionProviderTest
  {
    private readonly IDbConnectionInfo connectionInfo = new DbConnectionInfo("Test");

    [Test]
    public void CanCreateWithConnectionInfo()
    {
      var provider = (IConnectionProvider) new MicrOrmConnectionProvider(connectionInfo);

      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionInfo);
      Assert.AreEqual(connectionInfo.ConnectionString, provider.ConnectionInfo.ConnectionString);
      Assert.AreEqual(connectionInfo.Provider, provider.ConnectionInfo.Provider);
      Assert.NotNull(provider.ConnectionInfo.ProviderFactory);
    }

    [Test]
    public void CanCreateWithConnectionName()
    {
      var provider = (IConnectionProvider) new MicrOrmConnectionProvider(connectionInfo.Name);
      
      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionInfo);
      Assert.AreEqual(connectionInfo.ConnectionString, provider.ConnectionInfo.ConnectionString);
      Assert.AreEqual(connectionInfo.Provider, provider.ConnectionInfo.Provider);
      Assert.NotNull(provider.ConnectionInfo.ProviderFactory);
    }

    [Test]
    public void CanCreateWithConnectionStringAndProviderName()
    {
      var provider = (IConnectionProvider) new MicrOrmConnectionProvider(connectionInfo.ConnectionString, connectionInfo.Provider);
      
      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionInfo);
      Assert.AreEqual(connectionInfo.ConnectionString, provider.ConnectionInfo.ConnectionString);
      Assert.AreEqual(connectionInfo.Provider, provider.ConnectionInfo.Provider);
      Assert.NotNull(provider.ConnectionInfo.ProviderFactory);
    }
    
    [Test]
    public void InvalidConnectionStringNameThrows()
    {
      var e = Assert.Throws<ArgumentException>(() => new MicrOrmConnectionProvider("InvalidConnectionStringName"));
      Assert.AreEqual("connectionStringName", e.ParamName);
    }
  }
}