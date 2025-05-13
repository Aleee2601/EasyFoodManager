using EasyFoodManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EasyFoodManager.DAL
{
    public static class SetariDAL
    {
        public static Setari GetSetari()
        {
            using (var reader = DatabaseHelper.ExecuteReader("GetSetari"))
            {
                if (reader.Read())
                {
                    return new Setari
                    {
                        DiscountMeniu = Convert.ToDecimal(reader["DiscountMeniu"]), // x
                        PragDiscountSuma = Convert.ToDecimal(reader["PragDiscountSuma"]), // y
                        NrComenziPentruDiscount = Convert.ToInt32(reader["NrComenziPentruDiscount"]), // z
                        NrZilePentruDiscount = Convert.ToInt32(reader["NrZilePentruDiscount"]), // t
                        DiscountClientFidel = Convert.ToDecimal(reader["DiscountClientFidel"]), // w
                        PragLivrareGratuita = Convert.ToDecimal(reader["PragLivrareGratuita"]), // a
                        CostLivrareSubPrag = Convert.ToDecimal(reader["CostLivrareSubPrag"]), // b
                        PragAproapeEpuizat = Convert.ToDecimal(reader["PragAproapeEpuizat"]) // c
                    };
                }
            }

            return null;
        }
    }
}
