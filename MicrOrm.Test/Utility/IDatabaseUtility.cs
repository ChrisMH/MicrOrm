using System;

namespace MicrOrm.Test.Utility
{
  public interface IDatabaseUtility 
  {
    IMoConnectionProvider ConnectionProvider { get; }
    void CreateDatabase(string initializationSql);
    void DestroyDatabase();
  }
}