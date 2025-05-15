using EasyFoodManager.DAL;
using EasyFoodManager.Models;
using EasyFoodManager.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using EasyFoodManager.Services;

namespace EasyFoodManager.ViewModels
{
    public class AngajatComenziViewModel : INotifyPropertyChanged
    {
        private string _clientNume;
        public string ClientNume
        {
            get => _clientNume;
            set { _clientNume = value; OnPropertyChanged(nameof(ClientNume)); }
        }

        private string _clientTelefon;
        public string ClientTelefon
        {
            get => _clientTelefon;
            set { _clientTelefon = value; OnPropertyChanged(nameof(ClientTelefon)); }
        }

        private string _clientAdresa;
        public string ClientAdresa
        {
            get => _clientAdresa;
            set { _clientAdresa = value; OnPropertyChanged(nameof(ClientAdresa)); }
        }

        public ObservableCollection<ComandaPreparat> Produse { get; set; } = new();
        public ObservableCollection<Comanda> ComenziActive { get; set; } = new();
        public ObservableCollection<Comanda> ToateComenzile { get; set; } = new();
        public ObservableCollection<Preparat> PreparateCuStocScazut { get; set; } = new();

        private Comanda _selectedComanda;
        public Comanda SelectedComanda
        {
            get => _selectedComanda;
            set
            {
                _selectedComanda = value;
                OnPropertyChanged(nameof(SelectedComanda));

                if (value != null)
                {
                    var client = UtilizatorDAL.GetUtilizatorById(value.ClientId);
                    if (client != null)
                    {
                        ClientNume = $"{client.Nume} {client.Prenume}";
                        ClientTelefon = client.Telefon;
                        ClientAdresa = client.Adresa;
                        OnPropertyChanged(nameof(ClientNume));
                        OnPropertyChanged(nameof(ClientTelefon));
                        OnPropertyChanged(nameof(ClientAdresa));
                    }

                    Produse.Clear();
                    foreach (var p in ComandaDAL.GetProduseDinComanda(value.Id))
                        Produse.Add(p);

                    OnPropertyChanged(nameof(Produse));
                }
                Console.WriteLine("Comanda selectată: " + value?.Id);
                Console.WriteLine("Client: " + ClientNume);

            }

        }




        private string _stareNoua;
        public string StareNoua
        {
            get => _stareNoua;
            set
            {
                _stareNoua = value;
                OnPropertyChanged(nameof(StareNoua));
            }
        }

        public ICommand SchimbaStareCommand { get; }
        public ICommand RefreshComenziCommand { get; }

        public AngajatComenziViewModel()
        {
            RefreshComenzi();

            SchimbaStareCommand = new RelayCommand(_ => SchimbaStare(), _ => SelectedComanda != null && !string.IsNullOrWhiteSpace(StareNoua));
            RefreshComenziCommand = new RelayCommand(_ => RefreshComenzi());
        }

        private void RefreshComenzi()
        {
            ComenziActive.Clear();
            foreach (var c in ComandaDAL.GetComenziActive())
            {
                Console.WriteLine($"Comanda {c.Id} - ClientId: {c.ClientId}");
                ComenziActive.Add(c);
            }


            ToateComenzile.Clear();
            foreach (var c in ComandaDAL.GetComenziToate())
                ToateComenzile.Add(c);

            LoadPreparateCuStocScazut();
        }

        private void LoadPreparateCuStocScazut()
        {
            PreparateCuStocScazut.Clear();

            var setari = SetariDAL.GetSetari();
            double prag = (double)setari.PragAproapeEpuizat;

            var toate = PreparatDAL.GetAllPreparate();
            foreach (var p in toate)
            {
                if (p.CantitateTotala <= prag)
                    PreparateCuStocScazut.Add(p);
            }
        }

        private void SchimbaStare()
        {
            if (SelectedComanda == null || string.IsNullOrWhiteSpace(StareNoua)) return;

            ComandaDAL.UpdateStareComanda(SelectedComanda.Id, StareNoua);
            RefreshComenzi();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
