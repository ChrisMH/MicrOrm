using Common.Logging.NLog;
using MicrOrm.Core;

namespace MicrOrm.Test.Utility
{
  public abstract class DatabaseUtility : IDatabaseUtility
  {
    static DatabaseUtility()
    {
      MoLogger.Logger = new NLogLoggerFactory().GetLogger("MicrOrm");
    }

    public abstract IMoConnectionProvider ConnectionProvider { get; }
    public abstract void CreateDatabase(string initializationSql);
    public abstract void DestroyDatabase();
  }
}