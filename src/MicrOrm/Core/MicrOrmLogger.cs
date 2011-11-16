using System;
using System.Data;
using Utility.Logging;

namespace MicrOrm.Core
{
  public static class MicrOrmLogger
  {
    public static ILogger Logger { get; set; }
    public static bool Enabled { get; set; }

    public static void LogCommand(IDbCommand cmd)
    {
      if (!Enabled || Logger == null) return;

      Logger.Debug(cmd.CommandText);

      foreach (IDataParameter parameter in cmd.Parameters)
      {
        var value = parameter.Value == null ? "NULL" : parameter.Value.ToString();
        if (parameter.Value != null && parameter.Value.GetType().IsArray)
        {
          var arrayValues = (Array) Convert.ChangeType(parameter.Value, parameter.Value.GetType());
          var enumerator = arrayValues.GetEnumerator();
          value = "{";
          while (enumerator.MoveNext())
          {
            value += enumerator.Current.ToString() + ",";
          }
          value = value.TrimEnd(new[] {','}) + "}";
        }
        Logger.Debug("      :{0} = {1}", parameter.ParameterName, value);
      }
    }
  }
}