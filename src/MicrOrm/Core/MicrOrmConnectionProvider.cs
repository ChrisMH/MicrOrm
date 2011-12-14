using System;
using System.Data;
using Utility.Database;

namespace MicrOrm.Core
{
  public class MicrOrmConnectionProvider : IConnectionProvider
  {
    /// <summary>
    /// Create using existing connection information
    /// </summary>
    /// <param name="connectionInfo">Connection information</param>
    public MicrOrmConnectionProvider(IDbConnectionInfo connectionInfo)
    {
      ConnectionInfo = connectionInfo;
    }

    /// <summary>
    /// Create using a named connection string setting from the application configuration file
    /// </summary>
    /// <param name="connectionStringName">The name of the connection string in the application configuration file</param>
    public MicrOrmConnectionProvider(string connectionStringName)
    {
      ConnectionInfo = new DbConnectionInfo {ConnectionStringName = connectionStringName};
    }

    /// <summary>
    /// Create using a connection string and provider name
    /// </summary>
    /// <param name="connectionString">The connection string</param>
    /// <param name="provider">The database provider name</param>
    public MicrOrmConnectionProvider(string connectionString, string provider)
    {
      if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("Connection string not supplied", "connectionString");
      if (string.IsNullOrEmpty(provider)) throw new ArgumentException("Provider name not not supplied", "provider");
      ConnectionInfo = new DbConnectionInfo { ConnectionString = connectionString, Provider = provider };
    }

    /// <summary>
    /// Creates a connection using the class settings.
    /// </summary>
    /// <returns>A (closed) connection to the database.</returns>
    public IDbConnection CreateConnection()
    {
      var connection = ConnectionInfo.ProviderFactory.CreateConnection();
      if (connection == null)
      {
        throw new MicrOrmException("Failed to create connection with provider factory");
      }
      connection.ConnectionString = ConnectionInfo.ConnectionString;
      return connection;
    }

    public IDbConnectionInfo ConnectionInfo { get; private set; }


    public IDatabase Database
    {
      get { return new MicrOrmDatabase(this); }
    }

    public ITransaction Transaction
    {
      get { return new MicrOrmTransaction(this); }
    }
  }
}