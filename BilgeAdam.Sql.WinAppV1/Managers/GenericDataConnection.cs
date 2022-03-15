using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdam.Sql.WinApp.Managers
{
    internal class GenericDataConnection
    {
        private SqlConnection connection;
        private void Connect()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
            if (connection == null)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
        }
        public List<T> Load<T>(string query) where T:class
        {
            Connect();
            var actualType = typeof(T);
            var result = new List<T>();
            var command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var instance = Activator.CreateInstance<T>();
                    
                    var properties = actualType.GetProperties(System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.Instance);
                    foreach (var property in properties)
                    {
                        var readerValue = reader[property.Name];
                        //var propertyType = Nullable.GetUnderlyingType(property.PropertyType) != null ? Nullable.GetUnderlyingType(property.PropertyType): property.PropertyType;
                        //ya da coalesce ile çözersin.
                        var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                        var safevalue = readerValue == null ? null : Convert.ChangeType(readerValue,propertyType);
                        property.SetValue(instance,safevalue);
                    }
                    result.Add(instance);
                }
            }
            Disconnect();
            return result;
        }
        private void Disconnect()
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }
    }
}
