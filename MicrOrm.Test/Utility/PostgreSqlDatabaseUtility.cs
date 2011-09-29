using System.Data;
using MicrOrm.Core;

namespace MicrOrm.Test.Utility
{
  public abstract class PostgreSqlDatabaseUtility : IDatabaseUtility
  {
    public PostgreSqlDatabaseUtility(string connectionStringName)
    {
      Provider = new MoConnectionProvider(connectionStringName);

      CreateProvider = new MoConnectionProvider(connectionStringName);
      CreateProvider.ConnectionString["database"] = "postgres";
    }

    public IMoConnectionProvider ConnectionProvider
    {
      get { return Provider; }
    }

    public virtual void CreateDatabase(string initializationSql)
    {
      DestroyDatabase();
      using (var conn = CreateProvider.CreateConnection())
      {
        conn.Open();
        using (var cmd = conn.CreateCommand())
        {
          cmd.CommandText = string.Format("CREATE DATABASE {0}", Provider.ConnectionString["database"]);
          cmd.ExecuteNonQuery();
        }
      }

      if (initializationSql != null)
      {
        using (var conn = Provider.CreateConnection())
        {
          conn.Open();
          Initialize(conn, initializationSql);
        }
      }
    }

    public void DestroyDatabase()
    {
      using (var conn = CreateProvider.CreateConnection())
      {
        conn.Open();
        using (var cmd = conn.CreateCommand())
        {
          cmd.CommandText = string.Format("DROP DATABASE IF EXISTS {0}", Provider.ConnectionString["database"]);
          cmd.ExecuteNonQuery();
        }
      }
    }

    protected virtual void Initialize(IDbConnection conn, string initializationSql)
    {
    }


    protected IMoConnectionProvider Provider { get; private set; }
    protected IMoConnectionProvider CreateProvider { get; private set; }
  }
}