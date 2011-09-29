using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MicrOrm.Core
{
  public class MoDatabase : IMoDatabase
  {
    public MoDatabase( IMoConnectionProvider connectionProvider )
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

    //public TClass SingleOrDefault<TClass>(string sql, params object[] parameters) where TClass : new()
    //{
    //  return Query<TClass>(sql, parameters).SingleOrDefault();
    //}

    //public TClass Single<TClass>(string sql, params object[] parameters) where TClass : new()
    //{
    //  return Query<TClass>(sql, parameters).Single();
    //}

    //public TClass FirstOrDefault<TClass>(string sql, params object[] parameters) where TClass : new()
    //{
    //  return Query<TClass>(sql, parameters).FirstOrDefault();
    //}

    //public TClass First<TClass>(string sql, params object[] parameters) where TClass : new()
    //{
    //  return Query<TClass>(sql, parameters).First();
    //}

    //public IEnumerable<TClass> Query<TClass>(string sql, params object[] parameters) where TClass : new()
    //{
    //  throw new NotImplementedException();
    //}

    public IMoConnectionProvider ConnectionProvider { get; private set; }
    public DbConnection Connection { get; private set; }
  }
}