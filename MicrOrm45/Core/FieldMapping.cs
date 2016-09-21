﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MicrOrm.Core
{
    internal static class FieldMapping
    {
        internal static IDictionary<string,object> MapRowToDictionary(IDataReader rdr)
        {
            var row = new Dictionary<string, object>();
            for (var i = 0; i < rdr.FieldCount; i++)
            {
                row.Add(MapFieldNameToFriendlyName(rdr.GetName(i)), rdr.IsDBNull(i) ? null : rdr[i]);// Convert.ChangeType(rdr[i], rdr.GetFieldType(i)));
            }
            return row;
        }

        internal static string MapFieldNameToFriendlyName(string fieldName)
        {
            if (fieldName == null) throw new ArgumentNullException("fieldName");

            var parts = fieldName.Split(new[] {'_'}, StringSplitOptions.RemoveEmptyEntries);

            return parts.Select(part => System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(part.ToLower())).Aggregate(String.Concat);
        }
    }
}