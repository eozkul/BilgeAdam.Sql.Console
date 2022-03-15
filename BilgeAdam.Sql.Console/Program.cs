using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using System.Data.SqlClient;
using CS = System.Console;
namespace BilgeAdam.Sql.Console
{
    class Program
    {
        private static string connectionString = "Server=localhost,12000;Database=Northwind;User Id=sa;Password=1q2w3e4R!";
        private static string query = "SELECT CategoryID,CategoryName FROM Categories";

        static void Main(string[] args)
        {
            ConnectV4();

            //var connectionString = Environment.GetEnvironmentVariable("NorthwindConnectionString");
            //using (var connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    var command = new SqlCommand(query, connection);
            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            CS.Write($"{reader[0]}\t");
            //            CS.WriteLine(reader["CategoryName"]);
            //        }
            //    }

            //}

            //SOR!!
            // var configuration = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build();           
            //using (var connection = new SqlConnection(configuration.GetSection("Configuration:Data:NorthwindAdress").Value))
            //{
            //    connection.Open();
            //    var command = new SqlCommand(query, connection);
            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            CS.Write($"{reader[0]}\t");
            //            CS.WriteLine(reader["CategoryName"]);
            //        }
            //    }
            //}
        }

        private static void ConnectV4()
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
            using (var connection = new SqlConnection(cs))
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CS.Write($"{reader[0]}\t");
                        CS.WriteLine(reader["CategoryName"]);
                    }
                }

            }
        }

        private static void ConnectV2()
        {
            //Disposable olarak belirlenen nesnenin scope bitiminde Dispose methodunu çağırır.Bu da close u gereksiz kılar.
            using (var connection = new SqlConnection(connectionString))//IDisposable interface ile implement edilmiş
            {
                connection.Open();
                var command = new SqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CS.Write($"{reader[0]}\t");
                        CS.WriteLine(reader["CategoryName"]);
                    }
                }

            }
        }

        private static void ConnectV1()
        {
            var connectionString = "Server=localhost,12000;Database=Northwind;User Id=sa;Password=1q2w3e4R!";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var query = "SELECT CategoryID,CategoryName FROM Categories";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                CS.Write($"{reader[0]}\t");
                CS.WriteLine(reader["CategoryName"]);
            }
            reader.Close();
            connection.Close();
            connection.Dispose();//GC üzerinden sil
        }
    }
}
//Connected mimnaride(connection'ı kendimizin yönettiği mimari)
//bağlantıyı oluştur
//Sorguyu oluştur
//bağlantıyı aç
//Sorguyu çalıştır
//Bağlantıyı kapat
