using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString;

        static DatabaseHelper()
        {
            var configEntry = ConfigurationManager.ConnectionStrings["RestaurantDb"];
            if (configEntry != null)
            {
                connectionString = configEntry.ConnectionString;
            }
            else
            {
                // fallback – conexiune hardcoded pentru situații de urgență
                connectionString = "Data Source=localhost\\SQL2022;Initial Catalog=RestaurantDB;Integrated Security=True;TrustServerCertificate=True";
                Console.WriteLine("⚠️ [WARNING] App.config missing or RestaurantDb not found. Using fallback connection string.");
            }
        }

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(connectionString);
            Console.WriteLine("🔌 Connecting to: " + conn.ConnectionString);
            Console.WriteLine("Loaded connection string: " + connectionString);

            conn.Open();
            return conn;
        }

        public static int ExecuteNonQuery(string storedProcedureName, List<SqlParameter> parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters.ToArray());

                return command.ExecuteNonQuery();
            }
        }
       
        public static object ExecuteScalar(string storedProcedureName, List<SqlParameter> parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters.ToArray());

                return command.ExecuteScalar();
            }
        }

        public static SqlDataReader ExecuteReader(string storedProcedureName, List<SqlParameter> parameters = null)
        {
            var connection = GetConnection(); // nu folosim using aici pentru că reader-ul are ownership pe conexiune
            var command = new SqlCommand(storedProcedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                command.Parameters.AddRange(parameters.ToArray());

            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
