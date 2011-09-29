using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace MicrOrm.Core
{
  internal static class ObjectMapper
  {
    internal static TClass MapRowToObject<TClass>(DbDataReader rdr)
    {
      // Get public properties & fields
      var memberInfos = new List<MemberInfo>(typeof(TClass).GetProperties());
      memberInfos.AddRange(typeof(TClass).GetFields());

      // Create a mapping of mapping name to member.
      // Mapping name comes from MoColumnAttribute if it exists, else it is the member name
      var mappableMembers = new Dictionary<string, MemberInfo>();
      foreach (var memberInfo in memberInfos)
      {
        var mappingName = memberInfo.Name;
        var attributes = memberInfo.GetCustomAttributes(typeof(MoColumnAttribute), false);
        if( attributes.Length == 1 )
        {
          mappingName = ((MoColumnAttribute) attributes[0]).ColumnName;
        }

        if( mappableMembers.ContainsKey(mappingName))
        {
          throw new MoException(string.Format("Duplicate mapping name '{0}' in mapped type '{1}'", mappingName, typeof(TClass).FullName));
        }

        mappableMembers[mappingName] = memberInfo;
      }

      return default(TClass);
    }
  }
}