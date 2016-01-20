using System;
using System.Data;

namespace MicrOrm
{
  public interface ITransaction : IDataStrategy
  {
    void Commit();
    void Rollback();

    IDbTransaction Transaction { get; }

  }
}