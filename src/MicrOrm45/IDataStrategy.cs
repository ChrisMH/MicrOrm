using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MicrOrm
{
  public interface IDataStrategy : IDisposable
  {
    IEnumerable<IDictionary<string,object>> ExecuteReader(string sql, params object[] parameters);
    object ExecuteScalar(string sql, params object[] parameters);
    void ExecuteNonQuery(string sql, params object[] parameters);

    IConnectionProvider ConnectionProvider { get; }
    IDbConnection Connection { get; }
  }
}
