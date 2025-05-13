namespace EasyFoodManager.Models
{
    public class Setari
    {
        public decimal DiscountMeniu { get; set; }           // x%
        public decimal PragDiscountSuma { get; set; }        // y lei
        public int NrComenziPentruDiscount { get; set; }     // z
        public int NrZilePentruDiscount { get; set; }        // t
        public decimal DiscountClientFidel { get; set; }     // w%
        public decimal PragLivrareGratuita { get; set; }     // a lei
        public decimal CostLivrareSubPrag { get; set; }      // b lei
        public decimal PragAproapeEpuizat { get; set; }      // c g
    }
}
