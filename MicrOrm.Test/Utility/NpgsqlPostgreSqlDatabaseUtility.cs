
namespace MicrOrm.Test.Utility
{
  public class NpgsqlPostgreSqlDatabaseUtility : PostgreSqlDatabaseUtility
  {
    public NpgsqlPostgreSqlDatabaseUtility()
      : base("Npgsql.Connection")
    {
    }
  }
}
