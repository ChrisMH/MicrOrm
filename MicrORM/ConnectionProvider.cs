using System.Configuration;
using System.Data.Common;

namespace MicrORM
{
  public class ConnectionProvider : IConnectionProvider
  {
    /// <summary>
    /// Create using a named connection string setting from the application configuration file
    /// </summary>
    /// <param name="connectionName">The name of the connection in the application configuration file</param>
    public ConnectionProvider(string connectionName)
    {
      ConnectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
      ProviderFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings[connectionName].ProviderName);
    }

    /// <summary>
    /// Create using a connection string and provider name
    /// </summary>
    /// <param name="connectionString">The connection string</param>
    /// <param name="providerName">The database provider name</param>
    public ConnectionProvider(string connectionString, string providerName)
    {
      ConnectionString = connectionString;
      ProviderFactory = DbProviderFactories.GetFactory(providerName);
    }

    /// <summary>
    /// Creates a connection using the class settings.
    /// </summary>
    /// <returns>A (closed) connection to the database.</returns>
    public DbConnection CreateConnection()
    {
      var connection = ProviderFactory.CreateConnection();
      connection.ConnectionString = ConnectionString;
      return connection;
    }

    public string ConnectionString { get; private set; }
    public DbProviderFactory ProviderFactory { get; private set; }
  }
}