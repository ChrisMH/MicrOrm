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
  public static class GlobalTest
  {
    static GlobalTest()
    {
      try
      {
        MoLogger.Logger = new NLogLoggerFactory().GetLogger("MicrOrm.PostgreSql.Test");
        MoLogger.Enabled = true;

        DbUtility = new DbUtility();

      }
      catch (Exception e)
      {
        if(MoLogger.Logger != null)
        {
          MoLogger.Logger.Fatal(e, "GlobalTest : {0} : {1}", e.GetType(), e.Message);
        }
        throw;
      }

    }

    [SetUp]
    public static void SetUp()
    {
      try
      {
        DbUtility.Create();
      }
      catch (Exception e)
      {
        MoLogger.Logger.Fatal(e, "SetUp : {0} : {1}", e.GetType(), e.Message);
        
        throw;
      }
    }

    [TearDown]
    public static void TearDown()
    {
      DbUtility.Destroy();
    }

    public static DbUtility DbUtility { get; private set; }
  }
}