using System.Data;

namespace MicrOrm.Test.Utility
{
  public class NpgsqlPostgreSqlDatabaseUtility : PostgreSqlDatabaseUtility
  {
    public NpgsqlPostgreSqlDatabaseUtility()
      : base("Npgsql.Connection")
    {
    }

    protected override void Initialize(IDbConnection conn, string initializationSql)
    {
      base.Initialize(conn, initializationSql);

      using(var cmd = conn.CreateCommand())
      {
        cmd.CommandText = initializationSql;
        cmd.ExecuteNonQuery();
      }
    }
  }
}