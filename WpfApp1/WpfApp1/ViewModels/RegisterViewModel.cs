using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using EasyFoodManager.DAL;
using EasyFoodManager.Services;

namespace EasyFoodManager.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _nume = string.Empty;
        private string _prenume = string.Empty;
        private string _email = string.Empty;
        private string _parola = string.Empty;
        private string _telefon = string.Empty;
        private string _adresa = string.Empty;
        private string _tipUtilizator = "client"; // default

        public string Nume { get => _nume; set { _nume = value; OnPropertyChanged(); } }
        public string Prenume { get => _prenume; set { _prenume = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public string Parola { get => _parola; set { _parola = value; OnPropertyChanged(); } }
        public string Telefon { get => _telefon; set { _telefon = value; OnPropertyChanged(); } }
        public string Adresa { get => _adresa; set { _adresa = value; OnPropertyChanged(); } }
        public string TipUtilizator { get => _tipUtilizator; set { _tipUtilizator = value; OnPropertyChanged(); } }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register()
        {
            var db = new DbHelper();
            string hashedPassword = HashHelper.ComputeSha256Hash(Parola);

            using (SqlConnection con = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("RegisterUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Nume", Nume);
                cmd.Parameters.AddWithValue("@Prenume", Prenume);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Parola", hashedPassword);
                cmd.Parameters.AddWithValue("@Telefon", Telefon);
                cmd.Parameters.AddWithValue("@Adresa", Adresa);
                cmd.Parameters.AddWithValue("@TipUtilizator", TipUtilizator);

                con.Open();
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Cont creat cu succes!");
                }
                else
                {
                    MessageBox.Show("Eroare la înregistrare.");
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

