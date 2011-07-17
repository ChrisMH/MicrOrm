using System;
using System.Data.Common;

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

    public IMoConnectionProvider ConnectionProvider { get; private set; }
    public DbConnection Connection { get; private set; }
  }
}