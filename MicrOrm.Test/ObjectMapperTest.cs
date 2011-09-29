using System;
using System.Data.Common;
using MicrOrm.Core;
using MicrOrm.Test.Utility;
using Moq;
using NUnit.Framework;

namespace MicrOrm.Test
{
  [TestFixture]
  public class ObjectMapperTest
  {


    private readonly MockRow[] mockTable1 =
      new[]
        {
          new MockRow
            {
              Fields = new[]
                         {
                           new MockField {Name = "IntValue", Type = typeof (Int32), Value = 1},
                           new MockField {Name = "StringValue", Type = typeof (String), Value = "one"}
                         }
            },
          new MockRow
            {
              Fields = new[]
                         {
                           new MockField {Name = "IntValue", Type = typeof (Int32), Value = 2},
                           new MockField {Name = "StringValue", Type = typeof (String), Value = "two"}
                         }
            }
        };


        }

  public class TestObject
  {
    public int IntValue { get; set; }
    public string StringValue { get; set; }
  }
}