using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class CategorieDAL
    {
        public static List<Categorie> GetAllCategorii()
        {
            var categorii = new List<Categorie>();

            using (var reader = DatabaseHelper.ExecuteReader("GetAllCategorii"))
            {
                while (reader.Read())
                {
                    categorii.Add(new Categorie
                    {
                        Id = (int)reader["Id"],
                        Nume = reader["Nume"].ToString()
                    });
                }
            }

            return categorii;
        }

        public static void AddCategorie(string nume)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nume", nume)
            };

            DatabaseHelper.ExecuteNonQuery("AddCategorie", parameters);
        }

        public static void UpdateCategorie(int id, string nume)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Nume", nume)
            };

            DatabaseHelper.ExecuteNonQuery("UpdateCategorie", parameters);
        }

        public static void DeleteCategorie(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            DatabaseHelper.ExecuteNonQuery("DeleteCategorie", parameters);
        }
    }
}
