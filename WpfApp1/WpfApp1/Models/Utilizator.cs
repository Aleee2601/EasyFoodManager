namespace EasyFoodManager.Models
{
    public class Utilizator
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string Parola { get; set; }
        public string Tip { get; set; } // "Client" sau "Angajat"
    }
}
