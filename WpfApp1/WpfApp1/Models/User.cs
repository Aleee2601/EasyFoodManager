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
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string TipUtilizator { get; set; } // "client" sau "angajat"
    }
}
