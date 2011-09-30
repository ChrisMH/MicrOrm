using System.Data;

namespace MicrOrm.Core
{
  public class MoTransaction : MoDatabase, IMoTransaction
  {
    public MoTransaction(IMoConnectionProvider connectionProvider)
    : base(connectionProvider)
    {
      Transaction = Connection.BeginTransaction();
    }

    public override void Dispose()
    {
      if (Transaction != null)
      {
        Rollback();
      }
      base.Dispose();
    }

    public void Commit()
    {
      Transaction.Commit();
      Transaction.Dispose();
      Transaction = null;
    }

    public void Rollback()
    {
      Transaction.Rollback();
      Transaction.Dispose();
      Transaction = null;
    }

    public IDbTransaction Transaction { get; private set; }
  }
}