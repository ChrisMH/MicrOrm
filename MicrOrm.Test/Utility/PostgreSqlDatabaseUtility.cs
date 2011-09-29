using MicrOrm.Core;

namespace MicrOrm.Test.Utility
{
  public class PostgreSqlDatabaseUtility : IDatabaseUtility
  {
    public PostgreSqlDatabaseUtility( string connectionStringName )
    {
      Provider = new MoConnectionProvider(connectionStringName);

      CreateProvider = new MoConnectionProvider(connectionStringName);
      CreateProvider.ConnectionString["database"] = "postgres";
    }

    public IMoConnectionProvider ConnectionProvider { get { return Provider; } }
    
    public void CreateDatabase()
    {
      using( var conn = CreateProvider.CreateConnection())
      {
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = string.Format("CREATE DATABASE {0}", Provider.ConnectionString["database"]);
        cmd.ExecuteNonQuery();
      }
    }

    public void DestroyDatabase()
    {
      using (var conn = CreateProvider.CreateConnection())
      {
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = string.Format("DROP DATABASE IF EXISTS {0}", Provider.ConnectionString["database"]);
        cmd.ExecuteNonQuery();
      }
    }

    
    protected IMoConnectionProvider Provider { get; private set; }
    protected IMoConnectionProvider CreateProvider { get; private set; }
  }
}
