using System;
using System.Collections.Generic;
using System.Data;

namespace MicrOrm
{
  public interface IMoTransaction : IDisposable
  {
    IEnumerable<dynamic> ExecuteReader(string sql, params object[] parameters);
    dynamic ExecuteScalar(string sql, params object[] parameters);
    void ExecuteNonQuery(string sql, params object[] parameters);
    
    IMoConnectionProvider ConnectionProvider { get; }
    IDbConnection Connection { get; }

    void Commit();
    void Rollback();

    IDbTransaction Transaction { get; }

  }
}