using System.Data.Common;

namespace MicrOrm.Core
{
  internal static class ObjectMapper
  {
    internal static TClass MapRowToObject<TClass>(DbDataReader rdr) where TClass : new()
    {
      //var propertyInfos = typeof(TClass).GetProperties();
      //for( var i = 0 ; i < rdr.FieldCount ; i++ )
      //{
      //  var fieldName = rdr.GetName(i);
      //  var fieldType = rdr.GetFieldType(i);
      //  var propertyInfo = propertyInfos.Where(p => p.Name == fieldName).SingleOrDefault();
      //  Convert.
      return default(TClass);
    }
  }
}