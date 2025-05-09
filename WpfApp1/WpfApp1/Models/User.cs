using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFoodManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nume { get; set; } = string.Empty;
        public string Prenume { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Parola { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Adresa { get; set; } = string.Empty;
        public string TipUtilizator { get; set; } = string.Empty;
    }
}
