using System;
using System.Collections.Generic;
using System.Data.Common;

namespace MicrOrm.Core
{
  public class MoDatabase : IMoDatabase
  {
    public MoDatabase(IMoConnectionProvider connectionProvider)
    {
      ConnectionProvider = connectionProvider;
      Connection = connectionProvider.CreateConnection();
      Connection.Open();
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
      Connection.Close();
    }

    public IEnumerable<dynamic> ExecuteReader(string sql, params object[] parameters)
    {
      using(var cmd = QueryBuilder.Build(Connection, sql, parameters))
      {
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
      throw new NotImplementedException();
    }

    public void ExecuteNonQuery(string sql, params object[] parameters)
    {
      throw new NotImplementedException();
    }

    public IMoConnectionProvider ConnectionProvider { get; private set; }
    public DbConnection Connection { get; private set; }
  }
}