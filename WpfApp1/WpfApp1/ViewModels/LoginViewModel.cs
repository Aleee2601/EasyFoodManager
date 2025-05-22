using EasyFoodManager.Models;
using EasyFoodManager.DAL;
using EasyFoodManager.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Input;
using EasyFoodManager.Services;


/*
 * TODO:
 *  - Adaugă validare pentru email (ex: regex)
 *  - Adaugă validare pentru parolă (ex: minim 8 caractere, 1 majusculă, 1 cifră)
 *  - Adaugă un mesaj de succes după autentificare
 *  - Adaugă un mesaj de eroare în cazul în care utilizatorul nu există
 *  - Adaugă un mesaj de eroare în cazul în care parola este greșită
 *  - Adaugă un mesaj de eroare în cazul în care email-ul nu este valid
 *  - Adaugă un mesaj de eroare în cazul în care parola nu este validă
 *  
 *  
 *  
 *  
 *  
 *  - lista de preparate si de meniuri - pt comanda
 *  - admin - modificare/stergere/adaugare meniuri si preparate, categorii, alergeni
 *  - adaugat imagini
 *  
 *  
 *  - adaugat un meniu de navigare
 *  - adaugat un meniu de navigare pentru admin
 *  - adaugat un meniu de navigare pentru utilizator
 *  - adaugat un meniu de navigare pentru guest
 *  - Modificat UI-ul pentru a fi mai prietenos
 *  
 *  
 *  
 *  - MODIFICAT COMENTARIILE !!!!
 */




namespace EasyFoodManager.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _parola;
        public string Parola
        {
            get => _parola;
            set
            {
                _parola = value;
                OnPropertyChanged(nameof(Parola));
            }
        }

        private string _mesajEroare;
        public string MesajEroare
        {
            get => _mesajEroare;
            set
            {
                _mesajEroare = value;
                OnPropertyChanged(nameof(MesajEroare));
            }
        }

        public ICommand LoginCommand { get; }

        // Eveniment (delegate) folosit pentru navigare în UI
        public Action<Utilizator> OnLoginSuccess;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => Login());
        }

        private void Login()
        {
            MesajEroare = "";

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Parola))
            {
                MesajEroare = "Email și parolă sunt obligatorii.";
                return;
            }

            // 👉 Parola este transformată în hash
            string parolaHash = HashHelper.ComputeSha256Hash(Parola);

            // 👉 Căutăm utilizatorul cu acel email și hash
            Utilizator user = UtilizatorDAL.Login(Email, parolaHash);

            if (user == null)
            {
                MesajEroare = "Autentificare eșuată.";
                return;
            }

            OnLoginSuccess?.Invoke(user);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
