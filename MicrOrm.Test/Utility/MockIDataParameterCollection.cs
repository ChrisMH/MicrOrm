using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Moq;

namespace MicrOrm.Test.Utility
{
  public static class MockIDataParameterCollection
  {
    public static IDataParameterCollection Create()
    {
      var mockParams = new Mock<IDataParameterCollection>();
      var paramColl = new List<object>();

      mockParams.SetupGet(pc => pc.Count)
        .Returns(() => paramColl.Count);
      mockParams.Setup(pc => pc.Add(It.IsAny<object>()))
        .Callback((object param) => paramColl.Add(param));
      mockParams.Setup(pc => pc[It.IsAny<int>()])
        .Returns((int index) => paramColl[index]);

      return mockParams.Object;
    }
  }
}