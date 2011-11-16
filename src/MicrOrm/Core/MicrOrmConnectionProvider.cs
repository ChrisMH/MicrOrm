using System;
using System.Configuration;
using System.Data.Common;

namespace MicrOrm.Core
{
  public class MicrOrmConnectionProvider : IConnectionProvider
  {
    /// <summary>
    /// Create using a named connection string setting from the application configuration file
    /// </summary>
    /// <param name="connectionStringName">The name of the connection string in the application configuration file</param>
    public MicrOrmConnectionProvider(string connectionStringName)
    {
      if(ConfigurationManager.ConnectionStrings[connectionStringName] == null) throw new ArgumentException(string.Format("Connection string name '{0}' not found in configuration", connectionStringName), "connectionStringName");

      ConnectionString = new DbConnectionStringBuilder { ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString };
      ProviderName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
      ProviderFactory = DbProviderFactories.GetFactory(ProviderName);
    }

    /// <summary>
    /// Create using a connection string builder and provider name
    /// </summary>
    /// <param name="connectionString">The connection string</param>
    /// <param name="providerName">The database provider name</param>
    public MicrOrmConnectionProvider(DbConnectionStringBuilder connectionString, string providerName)
    {
      ConnectionString = connectionString;
      ProviderName = providerName;
      ProviderFactory = DbProviderFactories.GetFactory(providerName);
    }

    /// <summary>
    /// Create using a connection string and provider name
    /// </summary>
    /// <param name="connectionString">The connection string</param>
    /// <param name="providerName">The database provider name</param>
    public MicrOrmConnectionProvider(string connectionString, string providerName)
    {
      ConnectionString = new DbConnectionStringBuilder { ConnectionString = connectionString };
      ProviderName = providerName;
      ProviderFactory = DbProviderFactories.GetFactory(providerName);
    }
    
    /// <summary>
    /// Creates a connection using the class settings.
    /// </summary>
    /// <returns>A (closed) connection to the database.</returns>
    public DbConnection CreateConnection()
    {
      var connection = ProviderFactory.CreateConnection();
      if(connection == null)
      {
        throw new MicrOrmException("Failed to create connection with provider factory");
      }
      connection.ConnectionString = ConnectionString.ConnectionString;
      return connection;
    }

    public DbConnectionStringBuilder ConnectionString { get; private set; }
    public string ProviderName { get; private set; }
    public DbProviderFactory ProviderFactory { get; private set; }

    
    public IDatabase Database
    {
      get
      {
        return new MicrOrmDatabase(this);
      }
    }

    public ITransaction Transaction
    {
      get
      {
        return new MicrOrmTransaction(this);
      }
    }
  }
}