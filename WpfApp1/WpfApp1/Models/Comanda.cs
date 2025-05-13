using System;
using System.Collections.Generic;

namespace EasyFoodManager.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Data { get; set; }
        public string Cod { get; set; }
        public string Stare { get; set; }
        public decimal CostTotal { get; set; }
        public decimal CostLivrare { get; set; }
        public decimal Discount { get; set; }
        public string OraEstimataLivrare { get; set; }

        public List<ComandaPreparat> Produse { get; set; } = new List<ComandaPreparat>();
    }

    public class ComandaPreparat
    {
        public int PreparatId { get; set; }
        public int NrBucati { get; set; }
    }
}
