using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Helper
{
    public static class ApplicationState
    {
        private static Dictionary<string, object> _values =new Dictionary<string, object>();

        public static void SetValue(string key, object value)
        {

            _values.Add(key, value);
        }

        public static T GetValue<T>(string key)
        {

            return (T)_values[key];
        }
        public static bool RemoveKey(string key)
        {
            return _values.Remove(key);
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
                dataTable.Columns.Add(prop.Name);
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                    values[i] = Props[i].GetValue(item, null);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
