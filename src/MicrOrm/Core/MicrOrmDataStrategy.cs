using System;
using System.Collections.Generic;
using System.Data;

namespace MicrOrm.Core
{
  public abstract class MicrOrmDataStrategy
  {
    protected MicrOrmDataStrategy(IConnectionProvider connectionProvider)
    {
      ConnectionProvider = connectionProvider;
      Connection = connectionProvider.CreateConnection();
      Connection.Open();
    }

    public virtual void Dispose()
    {
      GC.SuppressFinalize(this);
      Connection.Close();
    }

    public IEnumerable<dynamic> ExecuteReader(string sql, params object[] parameters)
    {
      using(var cmd = CreateCommand())
      {
        QueryBuilder.BuildCommand(cmd, sql, parameters);
        MicrOrmLogger.LogCommand(cmd);
        using(var rdr = cmd.ExecuteReader())
        {
          while(rdr.Read())
          {
            yield return FieldMapping.MapRowToDynamic(rdr);
          }
        }
      }
    }

    public dynamic ExecuteScalar(string sql, params object[] parameters)
    {
      using (var cmd = CreateCommand())
      {
        QueryBuilder.BuildCommand(cmd, sql, parameters);
        MicrOrmLogger.LogCommand(cmd);
        using (var rdr = cmd.ExecuteReader())
        {
          if(!rdr.Read())
          {
            return null;
          }

          if(Convert.IsDBNull(0))
          {
            return null;
          }

          try
          {
            return Convert.ChangeType(rdr[0], rdr.GetFieldType(0));
          }
          catch (InvalidCastException)
          {
            return null;
          }
        }
      }
    }

    public void ExecuteNonQuery(string sql, params object[] parameters)
    {
      using (var cmd = CreateCommand())
      {
        QueryBuilder.BuildCommand(cmd, sql, parameters);
        MicrOrmLogger.LogCommand(cmd);
        cmd.ExecuteNonQuery();
      }
    }

    protected abstract IDbCommand CreateCommand();

    public IConnectionProvider ConnectionProvider { get; private set; }
    public IDbConnection Connection { get; private set; } 
  }
}