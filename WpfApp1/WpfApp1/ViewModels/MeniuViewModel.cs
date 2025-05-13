using EasyFoodManager.Models;
using EasyFoodManager.DAL;
using EasyFoodManager.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using EasyFoodManager.Services;

namespace EasyFoodManager.ViewModels
{
    public class MeniuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Meniu> Meniuri { get; set; } = new ObservableCollection<Meniu>();
        public ObservableCollection<Preparat> ToatePreparate { get; set; } = new ObservableCollection<Preparat>();
        public ObservableCollection<PreparatSelectat> PreparatSelectatLista { get; set; } = new ObservableCollection<PreparatSelectat>();
        public ObservableCollection<Preparat> PreparateleMeniului { get; set; } = new ObservableCollection<Preparat>();

        private Meniu _selectedMeniu;
        public Meniu SelectedMeniu
        {
            get => _selectedMeniu;
            set
            {
                _selectedMeniu = value;
                OnPropertyChanged(nameof(SelectedMeniu));
                LoadPreparateleMeniului();
            }
        }

        public ICommand AddMeniuCommand { get; }
        public ICommand UpdateMeniuCommand { get; }
        public ICommand DeleteMeniuCommand { get; }

        public MeniuViewModel()
        {
            RefreshMeniuri();
            ToatePreparate = new ObservableCollection<Preparat>(PreparatDAL.GetAllPreparate());

            AddMeniuCommand = new RelayCommand(_ => AddMeniu());
            UpdateMeniuCommand = new RelayCommand(_ => UpdateMeniu(), _ => SelectedMeniu != null);
            DeleteMeniuCommand = new RelayCommand(_ => DeleteMeniu(), _ => SelectedMeniu != null);
        }

        private void RefreshMeniuri()
        {
            Meniuri.Clear();
            foreach (var meniu in MeniuDAL.GetAllMeniuri())
                Meniuri.Add(meniu);
        }

        private void LoadPreparateleMeniului()
        {
            PreparateleMeniului.Clear();
            if (SelectedMeniu == null) return;

            var preparate = MeniuDAL.GetPreparateleDinMeniu(SelectedMeniu.Id);
            foreach (var p in preparate)
                PreparateleMeniului.Add(p);
        }

        private void AddMeniu()
        {
            var meniuNou = new Meniu
            {
                Denumire = "Meniu nou",
                CategorieId = 1, // test default
                Descriere = "Descriere...",
                Pret = 0
            };

            // Selectează preparate și cantități
            List<(int preparatId, double cantitate)> preparateAdaugate = new();
            foreach (var item in PreparatSelectatLista.Where(p => p.Selectat))
            {
                preparateAdaugate.Add((item.Preparat.Id, item.Cantitate));
            }

            MeniuDAL.AddMeniu(meniuNou, preparateAdaugate);
            RefreshMeniuri();
        }

        private void UpdateMeniu()
        {
            if (SelectedMeniu == null) return;

            MeniuDAL.UpdateMeniu(SelectedMeniu);
            RefreshMeniuri();
        }

        private void DeleteMeniu()
        {
            if (SelectedMeniu == null) return;

            MeniuDAL.DeleteMeniu(SelectedMeniu.Id);
            Meniuri.Remove(SelectedMeniu);
            SelectedMeniu = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class PreparatSelectat : INotifyPropertyChanged
    {
        public Preparat Preparat { get; set; }

        private bool _selectat;
        public bool Selectat
        {
            get => _selectat;
            set
            {
                _selectat = value;
                OnPropertyChanged(nameof(Selectat));
            }
        }

        private double _cantitate;
        public double Cantitate
        {
            get => _cantitate;
            set
            {
                _cantitate = value;
                OnPropertyChanged(nameof(Cantitate));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
