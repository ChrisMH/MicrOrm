using System.Data;

namespace MicrOrm.Core
{
    public class MicrOrmDatabase : MicrOrmDataStrategy, IDatabase
    {
        public MicrOrmDatabase(IConnectionProvider connectionProvider)
            : base(connectionProvider)
        {
        }

        protected override IDbCommand CreateCommand()
        {
            return Connection.CreateCommand();
        }
    }
}