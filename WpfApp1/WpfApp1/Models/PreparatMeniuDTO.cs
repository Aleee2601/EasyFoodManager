using System.Collections.Generic;

namespace EasyFoodManager.Models
{
    public class PreparatMeniuDTO
    {
        public string Tip { get; set; } // "Preparat" sau "Meniu"
        public int Id { get; set; }
        public string Denumire { get; set; }
        public decimal Pret { get; set; }
        public string Cantitate { get; set; }
        public string Categorie { get; set; }

        // ✅ Lista preparatelor componente, doar pentru meniuri
        public List<PreparatInMeniuDTO> PreparateDinMeniu { get; set; } = new();
    }
}
