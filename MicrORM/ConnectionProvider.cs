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

    }

    /// <summary>
    /// Create using a connection string and provider name
    /// </summary>
    /// <param name="connectionString">The connection string</param>
    /// <param name="providerName">The database provider name</param>
    public ConnectionProvider( string connectionString, string providerName )
    {
      
    }

    protected string ConnectionString { get; private set; }
    protected DbProviderFacory ProviderFactory { get; private set; }
  }
}