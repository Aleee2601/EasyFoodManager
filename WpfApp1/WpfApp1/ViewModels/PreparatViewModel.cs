using EasyFoodManager.Models;
using EasyFoodManager.DAL;
using EasyFoodManager.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using EasyFoodManager.Services;

namespace EasyFoodManager.ViewModels
{
    public class PreparatViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Preparat> Preparate { get; set; }
        public ObservableCollection<Alergen> TotiAlergenii { get; set; }
        public ObservableCollection<Alergen> AlergeniSelectati { get; set; }

        private Preparat _selectedPreparat;
        public Preparat SelectedPreparat
        {
            get => _selectedPreparat;
            set
            {
                _selectedPreparat = value;
                OnPropertyChanged(nameof(SelectedPreparat));
                LoadAlergeniPreparat();
            }
        }

        public string CuvantCautat { get; set; }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ResetSearchCommand { get; }
        public ICommand SaveAlergeniCommand { get; }

        public PreparatViewModel()
        {
            Preparate = new ObservableCollection<Preparat>(PreparatDAL.GetAllPreparate());
            TotiAlergenii = new ObservableCollection<Alergen>(AlergenDAL.GetAllAlergeni());
            AlergeniSelectati = new ObservableCollection<Alergen>();

            AddCommand = new RelayCommand(_ => AddPreparat());
            UpdateCommand = new RelayCommand(_ => UpdatePreparat(), _ => SelectedPreparat != null);
            DeleteCommand = new RelayCommand(_ => DeletePreparat(), _ => SelectedPreparat != null);
            SearchCommand = new RelayCommand(_ => SearchPreparate());
            ResetSearchCommand = new RelayCommand(_ => ResetSearch());
            SaveAlergeniCommand = new RelayCommand(_ => SalveazaAlergeniPreparat(), _ => SelectedPreparat != null);
        }

        private void LoadAlergeniPreparat()
        {
            AlergeniSelectati.Clear();
            if (SelectedPreparat != null)
            {
                var alergeni = PreparatDAL.GetAlergeniByPreparatId(SelectedPreparat.Id);
                foreach (var alergen in alergeni)
                    AlergeniSelectati.Add(alergen);
            }
        }

        private void SalveazaAlergeniPreparat()
        {
            if (SelectedPreparat == null) return;

            var alergeniActuali = PreparatDAL.GetAlergeniByPreparatId(SelectedPreparat.Id);

            // stergere alergenii vechi
            foreach (var alergen in alergeniActuali)
            {
                if (!AlergeniSelectati.Any(a => a.Id == alergen.Id))
                    PreparatDAL.RemoveAlergenDeLaPreparat(SelectedPreparat.Id, alergen.Id);
            }

            // adaugare alergenii noi
            foreach (var alergen in AlergeniSelectati)
            {
                if (!alergeniActuali.Any(a => a.Id == alergen.Id))
                    PreparatDAL.AddAlergenLaPreparat(SelectedPreparat.Id, alergen.Id);
            }
        }

        private void AddPreparat()
        {
            var nou = new Preparat
            {
                Denumire = "Nou",
                Pret = 0,
                CantitatePortie = 100,
                CantitateTotala = 1000,
                CategorieId = 1 // test
            };

            PreparatDAL.AddPreparat(nou);
            Preparate.Clear();
            foreach (var p in PreparatDAL.GetAllPreparate())
                Preparate.Add(p);
        }

        private void UpdatePreparat()
        {
            if (SelectedPreparat == null) return;

            PreparatDAL.UpdatePreparat(SelectedPreparat);
        }

        private void DeletePreparat()
        {
            if (SelectedPreparat == null) return;

            PreparatDAL.DeletePreparat(SelectedPreparat.Id);
            Preparate.Remove(SelectedPreparat);
            SelectedPreparat = null;
        }

        private void SearchPreparate()
        {
            if (string.IsNullOrWhiteSpace(CuvantCautat))
                return;

            var rezultat = PreparatDAL.GetAllPreparate()
                .Where(p => p.Denumire.ToLower().Contains(CuvantCautat.ToLower()));

            Preparate.Clear();
            foreach (var p in rezultat)
                Preparate.Add(p);
        }

        private void ResetSearch()
        {
            CuvantCautat = string.Empty;
            Preparate.Clear();
            foreach (var p in PreparatDAL.GetAllPreparate())
                Preparate.Add(p);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
