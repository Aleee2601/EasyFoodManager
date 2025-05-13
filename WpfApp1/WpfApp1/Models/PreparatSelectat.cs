using System.ComponentModel;

namespace EasyFoodManager.Models
{
    public class PreparatSelectat : INotifyPropertyChanged
    {
        public Preparat Preparat { get; set; }

        private int _cantitate;
        public int Cantitate
        {
            get => _cantitate;
            set
            {
                if (_cantitate != value)
                {
                    _cantitate = value;
                    OnPropertyChanged(nameof(Cantitate));
                }
            }
        }

        public bool Selectat { get; set; } // pentru meniuri, dacă e cazul

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
