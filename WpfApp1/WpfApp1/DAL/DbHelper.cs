using System.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public class DbHelper
    {
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=RestaurantDB;Integrated Security=True";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
