using MicrOrm.Core;
using Utility.Logging.NLog;

namespace MicrOrm.PostgreSql.Test.Utility
{
  public class DbUtility
  {
    public const string ConnectionStringName = "TestConnection";

    public DbUtility()
    {
      Provider = new MoConnectionProvider(ConnectionStringName);

      CreateProvider = new MoConnectionProvider(ConnectionStringName);
      CreateProvider.ConnectionString["database"] = "postgres";
    }

    public IMoConnectionProvider ConnectionProvider
    {
      get { return Provider; }
    }

    public void Create()
    {
      Destroy();
      using (var conn = CreateProvider.CreateConnection())
      {
        conn.Open();
        using (var cmd = conn.CreateCommand())
        {
          cmd.CommandText = string.Format("CREATE DATABASE {0}", Provider.ConnectionString["database"]);
          cmd.ExecuteNonQuery();
        }
      }

      using (var conn = Provider.CreateConnection())
      {
        conn.Open();
        using (var cmd = conn.CreateCommand())
        {
          cmd.CommandText = Properties.Resources.Schema;
          cmd.ExecuteNonQuery();
        }
      }
    }

    public void Destroy()
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

    public void Seed()
    {
      using (var conn = Provider.CreateConnection())
      {
        conn.Open();
        using (var cmd = conn.CreateCommand())
        {
          cmd.CommandText = Properties.Resources.DeleteData;
          cmd.ExecuteNonQuery();

          cmd.CommandText = Properties.Resources.InsertData;
          cmd.ExecuteNonQuery();
        }
      }
    }

    protected IMoConnectionProvider Provider { get; private set; }
    protected IMoConnectionProvider CreateProvider { get; private set; }
  }
}