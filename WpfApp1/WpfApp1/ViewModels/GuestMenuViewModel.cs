using EasyFoodManager.Models;
using EasyFoodManager.DAL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EasyFoodManager.Services;
using System.Windows.Input;

namespace EasyFoodManager.ViewModels
{
    public class GuestMenuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Meniu> Meniuri { get; set; } = new();
        public ObservableCollection<Preparat> PreparateleMeniului { get; set; } = new();

        public string Keyword { get; set; }
        public string Alergen { get; set; }
        public bool Contine { get; set; }

        public ObservableCollection<PreparatMeniuDTO> Rezultate { get; set; } = new();
        public PreparatMeniuDTO PreparatSelectatDinCautare { get; set; }
        public ICommand CautaCommand => new RelayCommand(_ => Cauta());

        private void Cauta()
        {
            Rezultate.Clear();
            var rezultate = PreparatDAL.CautaPreparateMeniu(Keyword ?? "", Alergen, Contine);
            foreach (var r in rezultate)
                Rezultate.Add(r);
        }


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
        public void ReloadMeniuri()
        {
            Meniuri.Clear();
            foreach (var m in MeniuDAL.GetAllMeniuri())
                Meniuri.Add(m);
        }


        public GuestMenuViewModel()
        {
            Meniuri.Clear(); 
            var toate = MeniuDAL.GetAllMeniuri();
            foreach (var m in toate)
                Meniuri.Add(m);
        }


        private void LoadPreparateleMeniului()
        {
            PreparateleMeniului.Clear();
            if (SelectedMeniu != null)
            {
                var preparate = MeniuDAL.GetPreparateleDinMeniu(SelectedMeniu.Id);
                foreach (var p in preparate)
                    PreparateleMeniului.Add(p);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
