using EasyFoodManager.DAL;
using EasyFoodManager.Models;
using EasyFoodManager.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace EasyFoodManager.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }

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

        public ICommand RegisterCommand { get; }
        public Action OnRegisterSuccess;

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(_ => Register());
        }

        private void Register()
        {
            MesajEroare = "";

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Parola))
            {
                MesajEroare = "Email și parolă sunt obligatorii.";
                return;
            }

            var utilizator = new Utilizator
            {
                Nume = Nume,
                Prenume = Prenume,
                Email = Email,
                Parola = HashHelper.ComputeSha256Hash(Parola),
                Telefon = Telefon,
                Adresa = Adresa,
                Tip = "Client"
            };

            bool succes = UtilizatorDAL.Register(utilizator);
            if (succes)
                OnRegisterSuccess?.Invoke();
            else
                MesajEroare = "Acest email există deja.";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
