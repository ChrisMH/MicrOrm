using System;
using System.Collections.Generic;
using System.Data;

namespace MicrOrm.Core
{
  internal static class QueryBuilder
  {
    private const string ParameterPrefix = ":p";

    public static IDbCommand Build(IDbConnection conn, string sql, params object[] parameters)
    {
      if (conn == null) throw new ArgumentNullException("conn");
      if (String.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql");

      if (parameters == null)
      {
        parameters = new object[0];
      }

      var cmd = conn.CreateCommand();
      cmd.CommandText = sql;

      var parameterNames = FindUniqueParameters(sql);
      if(parameterNames.Count != parameters.Length)
      {
        throw new MicrOrmException(String.Format("Parameter count mismatch.  {0} in the SQL string, {1} supplied", parameterNames.Count, parameters.Length));
      }

      for(var i = 0 ; i < parameters.Length ; i++)
      {
        if(!parameterNames.ContainsKey(i))
        {
          throw new MicrOrmException(String.Format("Parameter ordinal mismatch.  Parameter with ordinal {0} is missing in the SQL stirng", i));
        }
        var dbParameter = cmd.CreateParameter();
        dbParameter.ParameterName = parameterNames[i];
        dbParameter.Value = parameters[i];
        cmd.Parameters.Add(dbParameter);
      }
      
      return cmd;
    }

    internal static Dictionary<int, string> FindUniqueParameters(string sql)
    {
      var result = new Dictionary<int, string>();

      var startIndex = 0;
      while (startIndex < sql.Length)
      {
        startIndex = sql.IndexOf(ParameterPrefix, startIndex);
        if (startIndex < 0)
        {
          break;
        }

        var endIndex = startIndex + 2;
        for(; endIndex < sql.Length ; endIndex++)
        {
          if(!Char.IsNumber(sql[endIndex]))
          {
            break;
          }
        }

        var parameterName = sql.Substring(startIndex + 1, endIndex - (startIndex + 1));
        var parameterIndex = -1;
        if (!Int32.TryParse(parameterName.Substring(1), out parameterIndex))
        {
          throw new MicrOrmException(String.Format("Parameter in SQL string has an improper format: '{0}'", parameterName));
        }

        if (!result.ContainsKey(parameterIndex))
        {
          result.Add(parameterIndex, parameterName);
        }

        startIndex = endIndex;
      }

      return result;
    }
  }
}