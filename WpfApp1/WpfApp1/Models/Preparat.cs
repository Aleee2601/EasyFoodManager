using System.Collections.Generic;

namespace EasyFoodManager.Models
{
    public class Preparat
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public decimal Pret { get; set; }
        public double CantitatePortie { get; set; }
        public double CantitateTotala { get; set; }
        public int CategorieId { get; set; }

        public List<Alergen> Alergeni { get; set; } = new List<Alergen>();
        public List<string> Imagini { get; set; } = new List<string>();
    }
}
