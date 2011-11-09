using System;
using System.Collections.Generic;
using System.Data.Common;

namespace MicrOrm.Core
{
  public class MoDatabase : MoDataStrategy, IMoDatabase
  {
    public MoDatabase(IMoConnectionProvider connectionProvider)
    : base(connectionProvider)
    {
      
    }
  }
}