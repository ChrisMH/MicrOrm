using System.Data.SqlServerCe;
using System.IO;
using MicrOrm.Core;

namespace MicrOrm.Test.Utility
{
  public class SqlServerCeDatabaseUtility : IDatabaseUtility
  {
    static SqlServerCeDatabaseUtility()
    {
      Provider = new MoConnectionProvider("SqlServerCe.Connection");
      Provider.ConnectionString["Data Source"] = Path.Combine(Path.GetTempPath(), ( string )Provider.ConnectionString["Data Source"]);
    }

    public IMoConnectionProvider ConnectionProvider { get { return Provider; } }
    
    public void CreateDatabase(string initializationSql)
    {
      using( var engine = new SqlCeEngine( Provider.ConnectionString.ConnectionString) )
      {
        engine.CreateDatabase();
      }
    }

    public void DestroyDatabase()
    {
      if( File.Exists( (string)Provider.ConnectionString["Data Source"] ) )
      {
        File.Delete((string)Provider.ConnectionString["Data Source"]);
      }
    }

    private static readonly IMoConnectionProvider Provider;
  }
}
