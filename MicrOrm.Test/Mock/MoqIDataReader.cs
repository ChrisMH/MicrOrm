using System;
using System.Data;
using Moq;

namespace MicrOrm.Test.Mock
{
  public class MockField
  {
    public string Name { get; set; }
    public Type Type { get; set; }
    public object Value { get; set; }
  }

  public class MockRow
  {
    public MockField[] Fields { get; set; }
  }

  public static class MoqIDataReader
  {
    public static IDataReader Create(MockRow[] mockTable)
    {
      var mockRdr = new Mock<IDataReader>();
      var row = -1;

      mockRdr.Setup(e => e.Read())
        .Returns(() => row < mockTable.Length - 1)
        .Callback(() => row++);

      mockRdr.Setup(e => e[It.IsAny<int>()])
        .Returns((int ordinal) => mockTable[row].Fields[ordinal].Value);

      mockRdr.Setup(e => e.FieldCount)
        .Returns(() => mockTable[row].Fields.Length);

      mockRdr.Setup(e => e.GetName(It.IsAny<int>()))
        .Returns((int ordinal) => mockTable[row].Fields[ordinal].Name);

      mockRdr.Setup(e => e.GetFieldType(It.IsAny<int>()))
        .Returns((int ordinal) => mockTable[row].Fields[ordinal].Type);

      return mockRdr.Object;
    }
  }
}