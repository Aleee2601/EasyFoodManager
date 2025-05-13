namespace EasyFoodManager.Models
{
    public class Meniu
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public int CategorieId { get; set; }
        public string Descriere { get; set; }
        public decimal Pret { get; set; }
    }
}
