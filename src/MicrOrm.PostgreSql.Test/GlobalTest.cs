using System;
using MicrOrm.Core;
using MicrOrm.PostgreSql.Test.Utility;
using NUnit.Framework;
using Utility.Logging.NLog;

namespace MicrOrm.PostgreSql.Test
{
  /// <summary>
  /// Sets up and tears down for the test assembly.
  /// </summary>
  [SetUpFixture]
  public class GlobalTest
  {
    [SetUp]
    public void SetUp()
    {
      try
      {
        MicrOrmLogger.Logger = new NLogLoggerFactory().GetLogger("MicrOrm.PostgreSql.Test");
        MicrOrmLogger.Enabled = true;

        DbUtility = new DbUtility();
        DbUtility.Create();
      }
      catch (Exception e)
      {
        MicrOrmLogger.Logger.Fatal(e, "SetUp : {0} : {1}", e.GetType(), e.Message);
        
        throw;
      }
    }

    [TearDown]
    public void TearDown()
    {
      DbUtility.Destroy();
    }

    public static DbUtility DbUtility { get; private set; }
  }
}