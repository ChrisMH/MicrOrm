using System.Data;
using System.Data.Common;
using Moq;

namespace MicrOrm.Test.Utility
{
  public static class MockIDbConnection
  {
    public static IDbConnection Create()
    {
      var conn = new Mock<IDbConnection>();

      conn.Setup(c => c.CreateCommand())
        .Returns(MockIDbCommand.Create());

      return conn.Object;
    }
  }
}