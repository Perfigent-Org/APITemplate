using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace APICoreTemplate.Core.Helpers
{
    public static class ModelHelper
    {
        public static string GetSafeColumnName<T>(string columnName)
        {
            var prop = typeof(T).GetProperty(columnName);
            return prop?.Name;
        }

        public static TableAttribute GetTableAttribute<T>()
        {
            var tAttribute = (TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();

            if (tAttribute != null && !string.IsNullOrWhiteSpace(tAttribute.Name))
            {
                return tAttribute;
            }

            throw new TypeInitializationException($"TableName not fould in {typeof(T).Name}.", new Exception($"Please add TableAttribute like [Table('TABLE_NAME', Schema = 'TABLE_SCHEMA')] in {typeof(T).FullName}"));
        }
    }
}
