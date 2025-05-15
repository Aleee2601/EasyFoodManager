using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class PreparatDAL
    {

        public static List<Preparat> GetAllPreparate()
        {
            var preparate = new List<Preparat>();
            using (var reader = DatabaseHelper.ExecuteReader("GetAllPreparate"))
            {
                while (reader.Read())
                {
                    preparate.Add(new Preparat
                    {
                        Id = (int)reader["Id"],
                        Denumire = reader["Denumire"].ToString(),
                        Pret = Convert.ToDecimal(reader["Pret"]),
                        CantitatePortie = Convert.ToDouble(reader["CantitatePortie"]),
                        CantitateTotala = Convert.ToDouble(reader["CantitateTotala"]),
                        CategorieId = (int)reader["CategorieId"]
                    });
                }
            }
            return preparate;
        }

        public static Preparat GetPreparatById(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            using (var reader = DatabaseHelper.ExecuteReader("GetPreparatById", parameters))
            {
                if (reader.Read())
                {
                    return new Preparat
                    {
                        Id = (int)reader["Id"],
                        Denumire = reader["Denumire"].ToString(),
                        Pret = Convert.ToDecimal(reader["Pret"]),
                        CantitatePortie = Convert.ToDouble(reader["CantitatePortie"]),
                        CantitateTotala = Convert.ToDouble(reader["CantitateTotala"]),
                        CategorieId = (int)reader["CategorieId"]
                    };
                }
            }

            return null;
        }

        public static void AddPreparat(Preparat p)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Denumire", p.Denumire),
                new SqlParameter("@Pret", p.Pret),
                new SqlParameter("@CantitatePortie", p.CantitatePortie),
                new SqlParameter("@CantitateTotala", p.CantitateTotala),
                new SqlParameter("@CategorieId", p.CategorieId)
            };

            DatabaseHelper.ExecuteNonQuery("AddPreparat", parameters);
        }

        public static void UpdatePreparat(Preparat p)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", p.Id),
                new SqlParameter("@Denumire", p.Denumire),
                new SqlParameter("@Pret", p.Pret),
                new SqlParameter("@CantitatePortie", p.CantitatePortie),
                new SqlParameter("@CantitateTotala", p.CantitateTotala),
                new SqlParameter("@CategorieId", p.CategorieId)
            };

            DatabaseHelper.ExecuteNonQuery("UpdatePreparat", parameters);
        }

        public static void DeletePreparat(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            DatabaseHelper.ExecuteNonQuery("DeletePreparat", parameters);
        }

        public static List<Alergen> GetAlergeniByPreparatId(int preparatId)
        {
            var alergeni = new List<Alergen>();
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@PreparatId", preparatId)
            };

            using (var reader = DatabaseHelper.ExecuteReader("GetAlergeniByPreparatId", parameters))
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


        public static void AddAlergenLaPreparat(int preparatId, int alergenId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@PreparatId", preparatId),
                new SqlParameter("@AlergenId", alergenId)
            };

            DatabaseHelper.ExecuteNonQuery("AddAlergenLaPreparat", parameters);
        }

        public static void RemoveAlergenDeLaPreparat(int preparatId, int alergenId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@PreparatId", preparatId),
                new SqlParameter("@AlergenId", alergenId)
            };

            DatabaseHelper.ExecuteNonQuery("RemoveAlergenDeLaPreparat", parameters);
        }

        public static List<PreparatMeniuDTO> CautaPreparateMeniu(string keyword, string alergen, bool contine)
        {
            var lista = new List<PreparatMeniuDTO>();
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@Keyword", keyword),
        new SqlParameter("@Alergen", string.IsNullOrEmpty(alergen) ? (object)DBNull.Value : alergen),
        new SqlParameter("@Contine", contine)
    };

            using (var reader = DatabaseHelper.ExecuteReader("SearchPreparateMeniu", parameters))
            {
                while (reader.Read())
                {
                    lista.Add(new PreparatMeniuDTO
                    {
                        Tip = reader["Tip"].ToString(),
                        Id = (int)reader["Id"],
                        Denumire = reader["Denumire"].ToString(),
                        Pret = Convert.ToDecimal(reader["Pret"]),
                        Cantitate = reader["CantitatePortie"] == DBNull.Value ? null : reader["CantitatePortie"].ToString(),
                        Categorie = reader["Categorie"].ToString()
                    });
                }
            }
            return lista;
        }


    }
}
