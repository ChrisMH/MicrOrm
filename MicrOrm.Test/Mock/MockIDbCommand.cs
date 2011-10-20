using System.Data;
using Moq;

namespace MicrOrm.Test.Mock
{
  public static class MockIDbCommand
  {
    public static IDbCommand Create()
    {
      var mockCmd = new Mock<IDbCommand>();
      var paramColl = MockIDataParameterCollection.Create();

      mockCmd.SetupProperty(cmd => cmd.CommandText, null);
        
      mockCmd.Setup(cmd => cmd.CreateParameter())
        .Returns(() => MockIDbDataParameter.Create());

      mockCmd.SetupGet(cmd => cmd.Parameters)
        .Returns(() => paramColl);

      return mockCmd.Object;
    }
  }
}