using System;
using System.Data;

namespace MicrOrm.Core
{
  public static class MicrOrmLogger
  {
    public static bool Enabled
    {
      get 
      {
        return logger != null;
      }
      set
      {
        if(value)
        {
          if(logger == null)
          {
            logger = NLog.LogManager.GetLogger("MicrOrm");
          }
        }
        else
        {
          logger = null;
        }
      }
    }

    static NLog.Logger logger;

    public static void LogCommand(IDbCommand cmd)
    {
      if (logger == null) return;

      logger.Debug(cmd.CommandText);

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
        logger.Debug("      :{0} = {1}", parameter.ParameterName, value);
      }
    }
  }
}