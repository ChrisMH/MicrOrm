using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Moq;

namespace MicrOrm.Test.Utility
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
