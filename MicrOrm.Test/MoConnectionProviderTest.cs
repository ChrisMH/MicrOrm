using System.Configuration;
using System.Data;
using System.Data.Common;
using MicrOrm.Core;
using Xunit;
using Xunit.Extensions;

namespace MicrOrm.Test
{
  public class MoConnectionProviderTest
  {
    [Theory]
    [InlineData("SqlServerCe.Connection")]
    [InlineData("SQLite.dotConnect.Connection")]
    [InlineData("PostgreSQL.dotConnect.Connection")]
    [InlineData("PostgreSQL.Npgsql.Connection")]
    public void can_create_with_connection_name(string connectionStringName)
    {
      var provider = new MoConnectionProvider(connectionStringName);

      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionString);
      Assert.NotNull(provider.ProviderFactory);
      Assert.Equal(new DbConnectionStringBuilder {ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString},
                   provider.ConnectionString);
    }

    [Theory]
    [InlineData("SqlServerCe.Connection")]
    [InlineData("SQLite.dotConnect.Connection")]
    [InlineData("PostgreSQL.dotConnect.Connection")]
    [InlineData("PostgreSQL.Npgsql.Connection")]
    public void can_create_with_connection_string_and_provider_name(string connectionStringName)
    {
      var provider = new MoConnectionProvider(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString,
                                            ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName);

      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionString);
      Assert.NotNull(provider.ProviderFactory);
      Assert.Equal(new DbConnectionStringBuilder { ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString },
                   provider.ConnectionString);
    }

    [Theory]
    [InlineData("SqlServerCe.Connection")]
    [InlineData("SQLite.dotConnect.Connection")]
    [InlineData("PostgreSQL.dotConnect.Connection")]
    [InlineData("PostgreSQL.Npgsql.Connection")]
    public void can_create_with_connection_string_builder_and_provider_name(string connectionStringName)
    {
      var provider = new MoConnectionProvider(new DbConnectionStringBuilder { ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString},
                                            ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName);

      Assert.NotNull(provider);
      Assert.NotNull(provider.ConnectionString);
      Assert.NotNull(provider.ProviderFactory);
      Assert.Equal(new DbConnectionStringBuilder { ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString },
                   provider.ConnectionString);
    }
    [Theory]
    [InlineData("SqlServerCe.Connection")]
    [InlineData("SQLite.dotConnect.Connection")]
    [InlineData("PostgreSQL.dotConnect.Connection")]
    [InlineData("PostgreSQL.Npgsql.Connection")]
    public void can_create_connection(string connectionStringName)
    {
      var provider = new MoConnectionProvider(connectionStringName);

      var connection = provider.CreateConnection();

      Assert.NotNull(connection);
      Assert.Equal(ConnectionState.Closed, connection.State);

      var connectionStringActual = new DbConnectionStringBuilder {ConnectionString = connection.ConnectionString};
      
      foreach (string key in provider.ConnectionString.Keys)
      {
        Assert.True(connectionStringActual.ContainsKey(key));
        Assert.Equal(provider.ConnectionString[key], connectionStringActual[key]);
      }
    }
  }
}