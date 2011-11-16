using System.IO;
using MicrOrm.Core;

namespace MicrOrm.SqLite.Test.Utility
{
  public class DbUtility
  {
    public const string ConnectionStringName = "TestConnection";

    public DbUtility()
    {
      Provider = new MicrOrmConnectionProvider(ConnectionStringName);
    }

    public IConnectionProvider ConnectionProvider
    {
      get { return Provider; }
    }

    public void Create()
    {
      Destroy();
      File.Create((string) Provider.ConnectionString["data source"]).Close();

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
      if (File.Exists((string) Provider.ConnectionString["data source"]))
      {
        File.Delete((string) Provider.ConnectionString["data source"]);
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

    protected IConnectionProvider Provider { get; private set; }
    protected IConnectionProvider CreateProvider { get; private set; }
  }
}