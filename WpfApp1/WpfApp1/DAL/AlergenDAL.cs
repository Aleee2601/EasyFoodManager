using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class AlergenDAL
    {
        public static List<Alergen> GetAllAlergeni()
        {
            var alergeni = new List<Alergen>();

            using (var reader = DatabaseHelper.ExecuteReader("GetAllAlergeni"))
            {
                while (reader.Read())
                {
                    alergeni.Add(new Alergen
                    {
                        Id = (int)reader["Id"],
                        Nume = reader["Nume"].ToString()
                    });
                }
            }

            return alergeni;
        }

        public static void AddAlergen(string nume)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nume", nume)
            };

            DatabaseHelper.ExecuteNonQuery("AddAlergen", parameters);
        }

        public static void DeleteAlergen(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            DatabaseHelper.ExecuteNonQuery("DeleteAlergen", parameters);
        }
    }
}
