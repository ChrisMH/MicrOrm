using System;
using MicrOrm.Core;
using MicrOrm.SqLite.Test.Utility;
using NUnit.Framework;
using Utility.Logging.NLog;

/// <summary>
/// Sets up and tears down for the test assembly.
/// Leave this class outside of any namespace so that it applies to any namespace in the assembly
/// </summary>
[SetUpFixture]
public static class GlobalTest
{
  static GlobalTest()
  {
    DbUtility = new DbUtility();

    MicrOrmLogger.Logger = new NLogLoggerFactory().GetLogger("MicrOrm.SqLite.Test");
    MicrOrmLogger.Enabled = true;
  }

  [SetUp]
  public static void SetUp()
  {
    DbUtility.Create();
  }

  [TearDown]
  public static void TearDown()
  {
    DbUtility.Destroy();
  }

  public static DbUtility DbUtility { get; private set; }
}