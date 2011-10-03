using System;
using System.Collections;
using System.Data;
using Common.Logging;
using System.Linq;

namespace MicrOrm.Core
{
  public static class MoLogger
  {
    public static ILogger Logger { get; set; }

    public static void LogCommand(IDbCommand cmd)
    {
      if(Logger == null) return;

      Logger.Debug(cmd.CommandText);

      foreach (IDataParameter parameter in cmd.Parameters)
      {
        var value = parameter.Value.ToString();
        if(parameter.Value.GetType().IsArray)
        {
        }
        Logger.Debug("{0} = {1}", parameter.ParameterName, value);
      }
    }
  }
}