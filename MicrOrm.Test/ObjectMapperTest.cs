using System;
using System.Data.Common;
using MicrOrm.Core;
using Moq;
using Xunit;

namespace MicrOrm.Test
{

  public class ObjectMapperTest
  {
    private class MockField
    {
      public string Name { get; set; }
      public Type Type { get; set; }
      public object Value { get; set; }
    }

    private class MockRow
    {
      public MockField[] Fields { get; set; }
    }

    private MockRow[] mockTable1 = new[]
                                     {
                                       new MockRow
                                         {
                                           Fields = new[]
                                                      {
                                                        new MockField {Name = "IntValue", Type = typeof (Int32), Value = 1},
                                                        new MockField {Name = "StringValue", Type = typeof (String), Value = "one"},
                                                        new MockField {Name = "double_value", Type = typeof (Double), Value = 1.0 },
                                                        new MockField {Name = "real_value", Type = typeof (Single), Value = 1.0F }
                                                      }
                                         },
                                       new MockRow
                                         {
                                           Fields = new[]
                                                      {
                                                        new MockField {Name = "IntValue", Type = typeof (Int32), Value = 2},
                                                        new MockField {Name = "StringValue", Type = typeof (String), Value = "two"},
                                                        new MockField {Name = "double_value", Type = typeof (Double), Value = 2.0 },
                                                        new MockField {Name = "real_value", Type = typeof (Single), Value = 2.0F }
                                                      }
                                         }
                                     };
    
    private DbDataReader MoqDbDataReader( MockRow[] mockTable )
    {
      var mockRdr = new Mock<DbDataReader>();
      var row = -1;

      mockRdr.Setup(e => e.Read())
        .Returns(() => row < mockTable.Length - 1 )
        .Callback(() => row++ );

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


    [Fact]
    public void simple_types_are_mapped()
    {
      var rdr = MoqDbDataReader(mockTable1);
      
      var result = ObjectMapper.MapRowToObject<TestObject>(rdr);

      Assert.NotNull(result);
    }
  }

  public class TestObject
  {
    public int IntValue { get; set; }
    public string StringValue;

    [MoColumn("double_value")]
    public double DoubleValue { get; set; }

    [MoColumn("real_value")] public float RealValue;
  }
}