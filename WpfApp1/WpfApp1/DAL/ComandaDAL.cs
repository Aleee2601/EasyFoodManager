using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class ComandaDAL
    {
        public static void AddComanda(Comanda comanda)
        {
            var comandaIdParam = new SqlParameter("@ComandaId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var parametri = new List<SqlParameter>
            {
                new SqlParameter("@ClientId", comanda.ClientId),
                new SqlParameter("@Data", comanda.Data),
                new SqlParameter("@Cod", comanda.Cod),
                new SqlParameter("@Stare", comanda.Stare),
                new SqlParameter("@CostTotal", comanda.CostTotal),
                new SqlParameter("@CostLivrare", comanda.CostLivrare),
                new SqlParameter("@Discount", comanda.Discount),
                new SqlParameter("@OraEstimataLivrare", comanda.OraEstimataLivrare),
                comandaIdParam // OUTPUT
            };

            DatabaseHelper.ExecuteNonQuery("AddComanda", parametri);

            // salvam ID-ul generat inapoi in obiectul comanda
            comanda.Id = (int)comandaIdParam.Value;

            // daca vrei, aici inserezi si in ComandaPreparat:
            foreach (var p in comanda.Produse)
            {
                var paramPrep = new List<SqlParameter>
        {
            new SqlParameter("@ComandaId", comanda.Id),
            new SqlParameter("@PreparatId", p.PreparatId),
            new SqlParameter("@NrBucati", p.NrBucati)
        };

                DatabaseHelper.ExecuteNonQuery("AddComandaPreparat", paramPrep);
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
            var parametri = new List<SqlParameter>
        {
            new SqlParameter("@ComandaId", comandaId),
            new SqlParameter("@Stare", stareNoua)
        };

            DatabaseHelper.ExecuteNonQuery("UpdateStareComanda", parametri);
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
                        OraEstimataLivrare = reader["OraEstimataLivrare"].ToString(),
                        ClientId = (int)reader["ClientId"] // ⬅️ Asigură-te că ai AICI
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
                        OraEstimataLivrare = reader["OraEstimataLivrare"].ToString(),
                        ClientId = (int)reader["ClientId"] // ⬅️ Asigură-te că ai AICI
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
        public static List<ComandaPreparat> GetProduseDinComanda(int comandaId)
        {
            var lista = new List<ComandaPreparat>();
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@ComandaId", comandaId)
    };

            using (var reader = DatabaseHelper.ExecuteReader("GetProduseDinComanda", parameters))
            {
                while (reader.Read())
                {
                    lista.Add(new ComandaPreparat
                    {
                        PreparatId = (int)reader["PreparatId"],
                        NrBucati = (int)reader["NrBucati"]
                    });
                }
            }

            return lista;
        }

    }
}
