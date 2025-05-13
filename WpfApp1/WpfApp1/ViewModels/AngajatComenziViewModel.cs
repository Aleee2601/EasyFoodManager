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
                ComenziActive.Add(c);

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
