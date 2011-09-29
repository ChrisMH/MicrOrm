using System.IO;
using MicrOrm.Core;

namespace MicrOrm.Test.Utility
{
  public class SqLiteDatabaseUtility : IDatabaseUtility
  {
    public SqLiteDatabaseUtility(string connectionStringName)
    {
      Provider = new MoConnectionProvider(connectionStringName);
      Provider.ConnectionString["Data Source"] = Path.Combine(Path.GetTempPath(), (string) Provider.ConnectionString["Data Source"]);
    }

    public IMoConnectionProvider ConnectionProvider
    {
      get { return Provider; }
    }

    public void CreateDatabase(string initializationSql)
    {
      File.Create((string) Provider.ConnectionString["Data Source"]).Close();
    }

    public void DestroyDatabase()
    {
      if (File.Exists((string) Provider.ConnectionString["Data Source"]))
      {
        File.Delete((string) Provider.ConnectionString["Data Source"]);
      }
    }

    protected IMoConnectionProvider Provider { get; private set; }
  }
}