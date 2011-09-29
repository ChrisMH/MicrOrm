using System;

namespace MicrOrm.Core
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
  public class MoColumnAttribute : Attribute
  {
    public MoColumnAttribute()
    {
    }

    public MoColumnAttribute(string columnName)
    {
      ColumnName = columnName;
    }

    public string ColumnName { get; set; }
  }
}