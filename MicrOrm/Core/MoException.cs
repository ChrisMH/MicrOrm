using System;

namespace MicrOrm.Core
{
  public class MoException : Exception
  {
    public MoException()
    {
    }

    public MoException(string message)
      : base(message)
    {
    }
    public MoException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}