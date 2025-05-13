using EasyFoodManager.DAL;
using EasyFoodManager.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using EasyFoodManager.Services;

namespace EasyFoodManager.ViewModels
{
    public class ComandaClientViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PreparatSelectat> MeniuPreparate { get; set; } = new();
        public ObservableCollection<Comanda> ComenziClient { get; set; } = new();

        public Utilizator ClientCurent { get; set; }

        private Comanda _comandaSelectata;
        public Comanda ComandaSelectata
        {
            get => _comandaSelectata;
            set
            {
                _comandaSelectata = value;
                OnPropertyChanged(nameof(ComandaSelectata));
            }
        }

        public ICommand PlaseazaComandaCommand { get; }
        public ICommand AnuleazaComandaCommand { get; }
        public ICommand RefreshComenziCommand { get; }

        public ComandaClientViewModel(Utilizator client)
        {
            ClientCurent = client;

            var toatePreparate = PreparatDAL.GetAllPreparate();
            foreach (var p in toatePreparate)
                MeniuPreparate.Add(new PreparatSelectat { Preparat = p, Cantitate = 0 });

            LoadComenzi();

            PlaseazaComandaCommand = new RelayCommand(_ => PlaseazaComanda());
            AnuleazaComandaCommand = new RelayCommand(_ => AnuleazaComanda(),
                _ => ComandaSelectata != null && ComandaSelectata.Stare != "Livrata" && ComandaSelectata.Stare != "Anulata");
            RefreshComenziCommand = new RelayCommand(_ => LoadComenzi());
        }

        private void LoadComenzi()
        {
            ComenziClient.Clear();
            var comenzi = ComandaDAL.GetComenziByClientId(ClientCurent.Id);
            foreach (var c in comenzi)
                ComenziClient.Add(c);
        }

        private void PlaseazaComanda()
        {
            var produseComandate = MeniuPreparate
                .Where(p => p.Cantitate > 0)
                .Select(p => new ComandaPreparat
                {
                    PreparatId = p.Preparat.Id,
                    NrBucati = (int)p.Cantitate
                }).ToList();

            if (produseComandate.Count == 1)
                return;

            var total = produseComandate.Sum(p => p.NrBucati * MeniuPreparate
                .First(x => x.Preparat.Id == p.PreparatId).Preparat.Pret);

            var livrare = total >= 100 ? 0 : 10; // Exemplu fix – poate veni din Setari
            var discount = 0m;

            var comandaNoua = new Comanda
            {
                ClientId = ClientCurent.Id,
                Data = DateTime.Now,
                Cod = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                Stare = "Înregistrată",
                CostTotal = total + livrare - discount,
                CostLivrare = livrare,
                Discount = discount,
                OraEstimataLivrare = DateTime.Now.AddMinutes(45).ToShortTimeString(),
                Produse = produseComandate
            };

            ComandaDAL.AddComanda(comandaNoua);
            LoadComenzi();

            foreach (var item in MeniuPreparate)
                item.Cantitate = 0;
        }

        private void AnuleazaComanda()
        {
            if (ComandaSelectata == null) return;

            ComandaDAL.AnuleazaComanda(ComandaSelectata.Id);
            LoadComenzi();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
