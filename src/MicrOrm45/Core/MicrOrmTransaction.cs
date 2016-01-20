using System.Data;

namespace MicrOrm.Core
{
  public class MicrOrmTransaction : MicrOrmDataStrategy, ITransaction
  {
    public MicrOrmTransaction(IConnectionProvider connectionProvider)
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


    protected override IDbCommand CreateCommand()
    {
      var cmd = Connection.CreateCommand();
      cmd.Transaction = Transaction;
      return cmd;
    }

    public IDbTransaction Transaction { get; private set; }
  }
}