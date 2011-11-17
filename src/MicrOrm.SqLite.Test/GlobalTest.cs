using System;
using MicrOrm.Core;
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
    MicrOrmLogger.Logger = new NLogLoggerFactory().GetLogger("MicrOrm.SqLite.Test");
    MicrOrmLogger.Enabled = true;
  }

  [SetUp]
  public static void SetUp()
  {
  }

  [TearDown]
  public static void TearDown()
  {
  }

}