using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Dynamic;

namespace MicrOrm.Core
{
  internal static class FieldMapping
  {
    internal static dynamic MapRowToDynamic(IDataReader rdr)
    {
      var expando = new ExpandoObject() as IDictionary<string, object>;
      for(var i = 0 ; i < rdr.FieldCount ; i++)
      {
        expando.Add(MapFieldNameToFriendlyName(rdr.GetName(i)), rdr.IsDBNull(i) ? null : Convert.ChangeType(rdr[i], rdr.GetFieldType(i)));
      }
      return expando;
    }

    internal static string MapFieldNameToFriendlyName(string fieldName)
    {
      if(fieldName == null) throw new ArgumentNullException("fieldName");

      var parts = fieldName.Split(new [] {'_'}, StringSplitOptions.RemoveEmptyEntries);
      
      return parts.Select(part => System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(part.ToLower())).Aggregate(String.Concat);
    }
  }
}