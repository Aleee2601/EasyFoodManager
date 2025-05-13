using EasyFoodManager.Models;
using EasyFoodManager.DAL;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EasyFoodManager.ViewModels
{
    public class GuestMenuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Meniu> Meniuri { get; set; } = new();
        public ObservableCollection<Preparat> PreparateleMeniului { get; set; } = new();

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

        public GuestMenuViewModel()
        {
            foreach (var m in MeniuDAL.GetAllMeniuri())
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
