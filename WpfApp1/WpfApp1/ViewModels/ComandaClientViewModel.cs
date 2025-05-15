using EasyFoodManager.DAL;
using EasyFoodManager.Models;
using EasyFoodManager.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using EasyFoodManager.Services;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;



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


        public string Keyword { get; set; }
        public string Alergen { get; set; }
        public bool Contine { get; set; }

        public ObservableCollection<PreparatMeniuDTO> Rezultate { get; set; } = new();
        public ObservableCollection<Preparat> PreparateMeniu { get; set; } = new();


        public PreparatMeniuDTO PreparatSelectatDinCautare { get; set; }

        public ICommand CautaCommand => new RelayCommand(_ => Cauta());
        public ICommand AdaugaLaComandaDinCautareCommand => new RelayCommand(_ => AdaugaLaComandaDinCautare());

        private void Cauta()
        {
            Rezultate.Clear();
            var rezultate = PreparatDAL.CautaPreparateMeniu(Keyword ?? "", Alergen, Contine);
            foreach (var r in rezultate)
                Rezultate.Add(r);
        }

        private void AdaugaLaComandaDinCautare()
        {
            if (PreparatSelectatDinCautare == null)
                return;

            var preparat = new Preparat
            {
                Id = PreparatSelectatDinCautare.Id,
                Denumire = PreparatSelectatDinCautare.Denumire,
                Pret = PreparatSelectatDinCautare.Pret
                // Poți adăuga și CantitatePortie, etc.
            };

            var existent = MeniuPreparate.FirstOrDefault(x => x.Preparat.Id == preparat.Id);
            if (existent != null)
            {
                existent.Cantitate += 1;
            }
            else
            {
                MeniuPreparate.Add(new PreparatSelectat
                {
                    Preparat = preparat,
                    Cantitate = 1
                });
            }

        }

        public ComandaClientViewModel(Utilizator client)
        {
            ClientCurent = client;

            var toatePreparate = PreparatDAL.GetAllPreparate();
            foreach (var p in toatePreparate)
                MeniuPreparate.Add(new PreparatSelectat { Preparat = p, Cantitate = 0 });

            LoadComenzi();
            LoadPreparateMeniu();


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
        private void LoadPreparateMeniu()
        {
            PreparateMeniu.Clear();
            var toate = PreparatDAL.GetAllPreparate(); // poți filtra dacă vrei doar active
            foreach (var p in toate)
                PreparateMeniu.Add(p);
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

            if (produseComandate.Count == 0)
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

            // Salvare in baza de date
            ComandaDAL.AddComanda(comandaNoua);

            // 🔻 Reducere stoc
            foreach (var produs in comandaNoua.Produse)
            {
                var preparat = MeniuPreparate.FirstOrDefault(p => p.Preparat.Id == produs.PreparatId);
                if (preparat != null)
                {
                    double cantitateScazuta = preparat.Preparat.CantitateTotala -
                                               (preparat.Preparat.CantitatePortie * produs.NrBucati);

                    var parametriUpdate = new List<SqlParameter>
                    {
                        new SqlParameter("@Id", preparat.Preparat.Id),
                        new SqlParameter("@NouaCantitate", cantitateScazuta)
                    };

                    DatabaseHelper.ExecuteNonQuery("UpdateCantitateTotalaPreparat", parametriUpdate);
                }
            }

            // Reincarca comenzile si reseteaza cantitati
            LoadComenzi();
            foreach (var item in MeniuPreparate)
                item.Cantitate = 0;
            MessageBox.Show("Comanda a fost plasată cu succes!", "Comandă plasată", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void AnuleazaComanda()
        {
            if (ComandaSelectata == null) return;

            ComandaDAL.AnuleazaComanda(ComandaSelectata.Id);
            LoadComenzi();
            MessageBox.Show("Comanda a fost anulată.", "Comandă anulată", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
