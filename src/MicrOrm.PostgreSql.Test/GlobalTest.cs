using System;
using System.Xml.Linq;
using MicrOrm.Core;
using NUnit.Framework;
using Utility.Database;
using Utility.Database.PostgreSql;
using Utility.Logging;
using Utility.Logging.NLog;

namespace MicrOrm.PostgreSql.Test
{
  /// <summary>
  /// Sets up and tears down for the test assembly.
  /// </summary>
  [SetUpFixture]
  public class GlobalTest
  {
    public static IDbManager TestDb { get; private set; }
    public static IConnectionProvider ConnectionProvider { get; private set; }
    public static ILogger Logger { get; private set; }

    [SetUp]
    public void SetUp()
    {
      try
      {
        Logger = new NLogLoggerFactory().GetCurrentClassLogger();
        MicrOrmLogger.Logger = Logger.GetLogger("MicrOrm.PostgreSql.Test");
        
        MicrOrmLogger.Enabled = true;

        TestDb = new PgDbManager(new PgDbDescription(XElement.Parse(Resources.Test)));
        TestDb.Create();

        ConnectionProvider = new MicrOrmConnectionProvider(TestDb.ConnectionInfo);

      }
      catch (Exception e)
      {
        Logger.Fatal(e, "SetUp : {0} : {1}", e.GetType(), e.Message);
        throw;
      }
    }

    [TearDown]
    public void TearDown()
    {
      try
      {
        MicrOrmLogger.Enabled = false;
        TestDb.Destroy();
      }
      catch (Exception e)
      {
        Logger.Fatal(e, "SetUp : {0} : {1}", e.GetType(), e.Message);
        throw;
      }
    }
  }
}