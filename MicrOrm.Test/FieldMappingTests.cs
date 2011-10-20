using System;
using MicrOrm.Core;
using MicrOrm.Test.Mock;
using NUnit.Framework;

namespace MicrOrm.Test
{
  public class FieldMappingTests
  {
    [Test]
    public void MapFieldNameToFriendlyNameThrowsIfInputIsNull()
    {
      Assert.Throws<ArgumentNullException>(() => FieldMapping.MapFieldNameToFriendlyName(null));
    }

    [TestCase("field", "Field")]
    [TestCase("Field", "Field")]
    [TestCase("fieldName", "Fieldname")]
    [TestCase("fieldname", "Fieldname")]
    [TestCase("FIELDNAME", "Fieldname")]
    [TestCase("field_name", "FieldName")]
    [TestCase("FIELD_NAME", "FieldName")]
    [TestCase("Field_name", "FieldName")]
    [TestCase("field_Name", "FieldName")]
    [TestCase("Field_Name", "FieldName")]
    public void CanMapFieldNameToFriendlyName(string input, string expected)
    {
      var result = FieldMapping.MapFieldNameToFriendlyName(input);

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void MapRowToDynamic()
    { 
      var rdr = MoqIDataReader.Create(mockTable);
      rdr.Read();

      var result = FieldMapping.MapRowToDynamic(rdr);

      Assert.NotNull(result);
      
      Assert.AreEqual(1, result.ShortValue);
      Assert.IsInstanceOf(typeof(Int16), result.ShortValue);

      Assert.AreEqual(1, result.IntValue);
      Assert.IsInstanceOf(typeof(Int32), result.IntValue);
      
      Assert.AreEqual(1, result.LongValue);
      Assert.IsInstanceOf(typeof(Int64), result.LongValue);
      
      Assert.AreEqual(1f, result.SingleValue);
      Assert.IsInstanceOf(typeof(Single), result.SingleValue);
      
      Assert.AreEqual(1.0, result.DoubleValue);
      Assert.IsInstanceOf(typeof(Double), result.DoubleValue);

      Assert.AreEqual("one", result.StringValue);
      Assert.IsInstanceOf(typeof(String), result.StringValue);
      
      Assert.AreEqual(true, result.BoolValue);
      Assert.IsInstanceOf(typeof(String), result.StringValue);
      
      Assert.AreEqual(DateTime.Parse("2011.01.01 12:01:01"), result.DateTimeValue);
      Assert.IsInstanceOf(typeof(DateTime), result.DateTimeValue);
    }

    private readonly MockRow[] mockTable =
      new[]
        {
          new MockRow
            {
              Fields = new[]
                         {
                           new MockField {Name = "short_value", Type = typeof (Int16), Value = 1},
                           new MockField {Name = "int_value", Type = typeof (Int32), Value = 1},
                           new MockField {Name = "long_value", Type = typeof (Int64), Value = 1},
                           new MockField {Name = "single_value", Type = typeof (Single), Value = 1f},
                           new MockField {Name = "double_value", Type = typeof (Double), Value = 1.0},
                           new MockField {Name = "string_value", Type = typeof (String), Value = "one"},
                           new MockField {Name = "bool_value", Type = typeof (Boolean), Value = true},
                           new MockField {Name = "date_time_value", Type = typeof (DateTime), Value = DateTime.Parse("2011.01.01 12:01:01")}
                         }
            },
          new MockRow
            {
              Fields = new[]
                         {
                           new MockField {Name = "short_value", Type = typeof (Int16), Value = 2},
                           new MockField {Name = "int_value", Type = typeof (Int32), Value = 2},
                           new MockField {Name = "long_value", Type = typeof (Int64), Value = 2},
                           new MockField {Name = "single_value", Type = typeof (Single), Value = 2f},
                           new MockField {Name = "double_value", Type = typeof (Double), Value = 2.0},
                           new MockField {Name = "string_value", Type = typeof (String), Value = "two"},
                           new MockField {Name = "bool_value", Type = typeof (Boolean), Value = false},
                           new MockField {Name = "date_time_value", Type = typeof (DateTime), Value = DateTime.Parse("2011.02.02 12:02:02")}
                         }
            }
        };
  }
}