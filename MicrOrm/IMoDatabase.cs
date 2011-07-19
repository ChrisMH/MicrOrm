using System;
using System.Collections.Generic;
using System.Data.Common;

namespace MicrOrm
{
  public interface IMoDatabase : IDisposable
  {
    TClass SingleOrDefault<TClass>(string sql, params object[] parameters) where TClass : new();
    TClass Single<TClass>(string sql, params object[] parameters) where TClass : new();

    TClass FirstOrDefault<TClass>(string sql, params object[] parameters) where TClass : new();
    TClass First<TClass>(string sql, params object[] parameters) where TClass : new();

    IEnumerable<TClass> Query<TClass>(string sql, params object[] parameters) where TClass : new();

    IMoConnectionProvider ConnectionProvider { get; }
    DbConnection Connection { get; }
  }
}