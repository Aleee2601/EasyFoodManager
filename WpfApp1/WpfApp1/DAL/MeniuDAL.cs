using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Controls;

namespace EasyFoodManager.DAL
{
    public static class MeniuDAL
    {
        public static List<Meniu> GetAllMeniuri()
        {
            var meniuri = new List<Meniu>();

            using (var reader = DatabaseHelper.ExecuteReader("GetAllMeniuri"))
            {
                while (reader.Read())
                {
                    meniuri.Add(new Meniu
                    {
                        Id = (int)reader["Id"],
                        Denumire = reader["Denumire"].ToString(),
                        CategorieId = (int)reader["CategorieId"],
                        //Descriere = reader["Descriere"].ToString(),
                        Pret = Convert.ToDecimal(reader["Pret"])
                    });
                }
            }

            return meniuri;
        }

        public static List<Preparat> GetPreparateleDinMeniu(int meniuId)
        {
            var preparate = new List<Preparat>();

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@MeniuId", meniuId)
            };

            using (var reader = DatabaseHelper.ExecuteReader("GetPreparateleDinMeniu", parameters))
            {
                while (reader.Read())
                {
                    preparate.Add(new Preparat
                    {
                        Id = (int)reader["Id"],
                        Denumire = reader["Denumire"].ToString(),
                        CantitatePortie = Convert.ToDouble(reader["CantitatePortie"]),
                        Pret = Convert.ToDecimal(reader["Pret"])
                        // se pot adauga mai multe daca procedura returneaza
                    });
                }
            }

            return preparate;
        }

        public static void AddMeniu(Meniu m, List<(int preparatId, double cantitate)> preparate)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Denumire", m.Denumire),
                new SqlParameter("@CategorieId", m.CategorieId),
                new SqlParameter("@Descriere", m.Descriere),
                new SqlParameter("@Pret", m.Pret)
            };

            // Adauga meniul si primeste Id-ul generat
            object result = DatabaseHelper.ExecuteScalar("AddMeniu", parameters);
            int meniuId = Convert.ToInt32(result);

            // Adauga fiecare preparat asociat
            foreach (var (preparatId, cantitate) in preparate)
            {
                var param2 = new List<SqlParameter>
                {
                    new SqlParameter("@MeniuId", meniuId),
                    new SqlParameter("@PreparatId", preparatId),
                    new SqlParameter("@Cantitate", cantitate)
                };

                DatabaseHelper.ExecuteNonQuery("AddPreparatLaMeniu", param2);
            }
        }

        public static void UpdateMeniu(Meniu m)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", m.Id),
                new SqlParameter("@Denumire", m.Denumire),
                new SqlParameter("@CategorieId", m.CategorieId),
                new SqlParameter("@Descriere", m.Descriere),
                new SqlParameter("@Pret", m.Pret)
            };

            DatabaseHelper.ExecuteNonQuery("UpdateMeniu", parameters);
        }

        public static void DeleteMeniu(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            DatabaseHelper.ExecuteNonQuery("DeleteMeniu", parameters);
        }
    }
}
