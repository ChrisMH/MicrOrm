using System;
using System.Data.Common;

namespace MicrOrm
{
  public interface IMoDatabase : IDisposable
  {
    IMoConnectionProvider ConnectionProvider { get; }
    DbConnection Connection { get; }
  }
}