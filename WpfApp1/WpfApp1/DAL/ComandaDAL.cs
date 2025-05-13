using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class ComandaDAL
    {
        public static void AddComanda(Comanda comanda)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ClientId", comanda.ClientId),
                new SqlParameter("@Data", comanda.Data),
                new SqlParameter("@Stare", comanda.Stare),
                new SqlParameter("@CostTotal", comanda.CostTotal),
                new SqlParameter("@CostLivrare", comanda.CostLivrare),
                new SqlParameter("@Discount", comanda.Discount),
                new SqlParameter("@OraEstimataLivrare", comanda.OraEstimataLivrare)
            };

            // Returneaza Id-ul comenzii pentru inserarea produselor
            object result = DatabaseHelper.ExecuteScalar("AddComanda", parameters);
            int comandaId = Convert.ToInt32(result);

            // Adauga produsele in comanda
            foreach (var item in comanda.Produse)
            {
                var paramProdus = new List<SqlParameter>
                {
                    new SqlParameter("@ComandaId", comandaId),
                    new SqlParameter("@PreparatId", item.PreparatId),
                    new SqlParameter("@NrBucati", item.NrBucati)
                };

                DatabaseHelper.ExecuteNonQuery("AddPreparatLaComanda", paramProdus);
            }
        }

        public static List<Comanda> GetComenziByClientId(int clientId)
        {
            var comenzi = new List<Comanda>();
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ClientId", clientId)
            };

            using (var reader = DatabaseHelper.ExecuteReader("GetComenziByClientId", parameters))
            {
                while (reader.Read())
                {
                    comenzi.Add(new Comanda
                    {
                        Id = (int)reader["Id"],
                        Data = Convert.ToDateTime(reader["Data"]),
                        Cod = reader["Cod"].ToString(),
                        Stare = reader["Stare"].ToString(),
                        CostTotal = Convert.ToDecimal(reader["CostTotal"]),
                        CostLivrare = Convert.ToDecimal(reader["CostLivrare"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        OraEstimataLivrare = reader["OraEstimataLivrare"].ToString()
                    });
                }
            }

            return comenzi;
        }

        public static void UpdateStareComanda(int comandaId, string stareNoua)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ComandaId", comandaId),
                new SqlParameter("@Stare", stareNoua)
            };

            DatabaseHelper.ExecuteNonQuery("UpdateStareComanda", parameters);
        }

        public static List<Comanda> GetComenziActive()
        {
            var comenzi = new List<Comanda>();

            using (var reader = DatabaseHelper.ExecuteReader("GetComenziActive"))
            {
                while (reader.Read())
                {
                    comenzi.Add(new Comanda
                    {
                        Id = (int)reader["Id"],
                        Data = Convert.ToDateTime(reader["Data"]),
                        Cod = reader["Cod"].ToString(),
                        Stare = reader["Stare"].ToString(),
                        CostTotal = Convert.ToDecimal(reader["CostTotal"]),
                        CostLivrare = Convert.ToDecimal(reader["CostLivrare"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        OraEstimataLivrare = reader["OraEstimataLivrare"].ToString()
                    });
                }
            }

            return comenzi;
        }

        public static List<Comanda> GetComenziToate()
        {
            var comenzi = new List<Comanda>();

            using (var reader = DatabaseHelper.ExecuteReader("GetComenziToate"))
            {
                while (reader.Read())
                {
                    comenzi.Add(new Comanda
                    {
                        Id = (int)reader["Id"],
                        Data = Convert.ToDateTime(reader["Data"]),
                        Cod = reader["Cod"].ToString(),
                        Stare = reader["Stare"].ToString(),
                        CostTotal = Convert.ToDecimal(reader["CostTotal"]),
                        CostLivrare = Convert.ToDecimal(reader["CostLivrare"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        OraEstimataLivrare = reader["OraEstimataLivrare"].ToString()
                    });
                }
            }

            return comenzi;
        }

        public static void AnuleazaComanda(int comandaId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ComandaId", comandaId)
            };

            DatabaseHelper.ExecuteNonQuery("AnuleazaComanda", parameters);
        }
    }
}
