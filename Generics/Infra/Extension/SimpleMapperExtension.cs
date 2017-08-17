using Infra.Attr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mapper
{
    public static class SimpleMapperExtension
    {  
        #region public_interface  
         
        public static List<T> MapWhere<T>(this DataTable table, Func<T, bool> sortExpression) where T : new()
        {
            var result = table.Select().Select(row => ConvertRow<T>(row, table.Columns, 
                typeof(T).GetProperties())).Where((t) => sortExpression(t));
            return result.ToList();
        }
        public static List<T> Map<T>(this DataTable table) where T : new()
        {
            var result = table.Select().Select(row => ConvertRow<T>(row, table.Columns, typeof(T).GetProperties()));
            return result.ToList();
        }
        #endregion

        #region implementation_details
        private static T ConvertRow<T>(DataRow row, DataColumnCollection columns, PropertyInfo[] p_info) where T : new()
        {
            var instance = new T();
            foreach (var info in p_info)
            {
                if (columns.Contains(GetMappingName(info))) SetProperty(row, instance, info);
            }
            return instance;
        }

        private static void SetProperty<T>(DataRow row, T instance,  PropertyInfo info) where T : new()
        {
            string mp_name = GetMappingName(info);
            object value = row[mp_name];
            value = Convert.ChangeType(value, info.PropertyType);
            info.SetValue(instance, value);
        }

        private static string GetMappingName( PropertyInfo info)
        {
            SimpleMapperAttribute attribute = info.GetCustomAttributes(typeof(SimpleMapperAttribute), true)
                .Select((o) => o as SimpleMapperAttribute).FirstOrDefault();
            return attribute == null ? info.Name : attribute.HeaderName;
        }
        #endregion
    }
}
