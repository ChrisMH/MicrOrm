using System.IO;
using MicrOrm.Core;

namespace MicrOrm.Test.Utility
{
  public class SqLiteDatabaseUtility : DatabaseUtility
  {
    public const string ConnectionStringName = "System.Data.SQLite.Connection";

    public SqLiteDatabaseUtility()
    {
      Provider = new MoConnectionProvider(ConnectionStringName);
      Provider.ConnectionString["Data Source"] = Path.Combine(Path.GetTempPath(), (string) Provider.ConnectionString["Data Source"]);
    }

    public override IMoConnectionProvider ConnectionProvider
    {
      get { return Provider; }
    }

    public override void CreateDatabase(string initializationSql)
    {
      File.Create((string) Provider.ConnectionString["Data Source"]).Close();
    }

    public override void DestroyDatabase()
    {
      if (File.Exists((string) Provider.ConnectionString["Data Source"]))
      {
        File.Delete((string) Provider.ConnectionString["Data Source"]);
      }
    }

    protected IMoConnectionProvider Provider { get; private set; }
  }
}