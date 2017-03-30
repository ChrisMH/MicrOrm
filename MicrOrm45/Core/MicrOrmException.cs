using System;

namespace MicrOrm.Core
{
    public class MicrOrmException : Exception
    {
        public MicrOrmException()
        {
        }

        public MicrOrmException(string message)
            : base(message)
        {
        }

        public MicrOrmException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}