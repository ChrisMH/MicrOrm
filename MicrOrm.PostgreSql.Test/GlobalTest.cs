﻿using Common.Logging.NLog;
using MicrOrm.Core;
using MicrOrm.PostgreSql.Test.Utility;
using NUnit.Framework;

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

    MoLogger.Logger = new NLogLoggerFactory().GetLogger("MicrOrm.PostgreSql.Test");
    MoLogger.Enabled = true;

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