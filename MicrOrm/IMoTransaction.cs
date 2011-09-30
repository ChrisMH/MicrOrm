namespace MicrOrm
{
  public interface IMoTransaction : IMoDatabase
  {
    void Commit();
    void Rollback();
  }
}