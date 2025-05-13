using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["RestaurantDb"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(connectionString);
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
            var connection = GetConnection(); // not using 'using' here because we return reader
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
