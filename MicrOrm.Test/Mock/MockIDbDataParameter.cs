using System.Data;
using Moq;

namespace MicrOrm.Test.Mock
{
  public static class MockIDbDataParameter
  {
    public static IDbDataParameter Create()
    {
      var mockParam = new Mock<IDbDataParameter>();

      mockParam.SetupProperty(param => param.ParameterName, null);
      mockParam.SetupProperty(param => param.Value, null);

      return mockParam.Object;
    }
  }
}
