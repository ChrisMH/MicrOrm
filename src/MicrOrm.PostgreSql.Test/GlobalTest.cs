using System;
using MicrOrm.Core;
using NUnit.Framework;
using Utility.Database;
using Utility.Database.PostgreSql;

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
    public static NLog.Logger Logger { get; private set; }

    [SetUp]
    public void SetUp()
    {
      try
      {
        Logger = NLog.LogManager.GetCurrentClassLogger();
        
        MicrOrmLogger.Enabled = true;

        TestDb = new PgDbManager {Description = new PgDbDescription { XmlRoot = Resources.Test }};
        TestDb.Create();

        ConnectionProvider = new MicrOrmConnectionProvider(TestDb.Description.ConnectionInfo);

      }
      catch (Exception e)
      {
        Logger.FatalException(string.Format("SetUp : {0} : {1}", e.GetType(), e.Message), e);
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
        Logger.FatalException(string.Format("SetUp : {0} : {1}", e.GetType(), e.Message), e);
        throw;
      }
    }
  }
}