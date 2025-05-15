using EasyFoodManager.DAL;
using EasyFoodManager.Models;
using EasyFoodManager.Services;
using EasyFoodManager.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EasyFoodManager.Views
{
    public partial class GuestMenuView : Window
    {
        public string Keyword { get; set; }
        public string Alergen { get; set; }
        public bool Contine { get; set; }

        public ObservableCollection<PreparatMeniuDTO> Rezultate { get; set; } = new();

        public ICommand CautaCommand => new RelayCommand(_ => Cauta());

        private void Cauta()
        {
            Rezultate.Clear();
            var rezultate = PreparatDAL.CautaPreparateMeniu(Keyword ?? "", Alergen, Contine);
            foreach (var r in rezultate)
                Rezultate.Add(r);
        }

        public PreparatMeniuDTO PreparatSelectatDinCautare { get; set; }

        public ICommand AfiseazaDetaliiPreparatCommand => new RelayCommand(_ => AfiseazaDetaliiPreparat());

        private void AfiseazaDetaliiPreparat()
        {
            if (PreparatSelectatDinCautare == null)
                return;

            MessageBox.Show($"Denumire: {PreparatSelectatDinCautare.Denumire}\n" +
                            $"Categorie: {PreparatSelectatDinCautare.Categorie}\n" +
                            $"Preț: {PreparatSelectatDinCautare.Pret} lei\n" +
                            $"Cantitate: {PreparatSelectatDinCautare.Cantitate}",
                            "Detalii preparat");
        }


        public GuestMenuView()
        {
            InitializeComponent();
            var vm = new GuestMenuViewModel();
            DataContext = vm;

            // Reîncarcă meniuri dacă vrei după o acțiune
            vm.ReloadMeniuri();
        }

    }
}
