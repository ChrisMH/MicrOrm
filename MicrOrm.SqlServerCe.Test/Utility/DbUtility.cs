using System.IO;
using MicrOrm.Core;

namespace MicrOrm.SqlServerCe.Test.Utility
{
  public class DbUtility
  {
    public const string ConnectionStringName = "SqlServerCe.Connection";

    static DbUtility()
    {
      Provider = new MoConnectionProvider(ConnectionStringName);
      Provider.ConnectionString["Data Source"] = Path.Combine(Path.GetTempPath(), ( string )Provider.ConnectionString["Data Source"]);
    }

    public override IMoConnectionProvider ConnectionProvider { get { return Provider; } }

    public override void CreateDatabase(string initializationSql)
    {
      using( var engine = new SqlCeEngine( Provider.ConnectionString.ConnectionString) )
      {
        engine.CreateDatabase();
      }
    }

    public override void DestroyDatabase()
    {
      if( File.Exists( (string)Provider.ConnectionString["Data Source"] ) )
      {
        File.Delete((string)Provider.ConnectionString["Data Source"]);
      }
    }

    private static readonly IMoConnectionProvider Provider;
  }
}
