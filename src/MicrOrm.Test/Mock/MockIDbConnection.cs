using System.Data;
using Moq;

namespace MicrOrm.Test.Mock
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