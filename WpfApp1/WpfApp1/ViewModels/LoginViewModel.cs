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
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email = string.Empty;
        private string _parola = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Parola
        {
            get => _parola;
            set { _parola = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            var db = new DbHelper();
            string hashedPassword = HashHelper.ComputeSha256Hash(Parola);

            using (SqlConnection con = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("LoginUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Parola", hashedPassword);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string tip = reader["TipUtilizator"].ToString() ?? "";
                    string nume = reader["Nume"].ToString() ?? "";

                    MessageBox.Show($"Bine ai venit, {nume} ({tip})!");
                    // Navigare in functie de tip (client / angajat)
                }
                else
                {
                    MessageBox.Show("Email sau parolă incorectă.");
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
