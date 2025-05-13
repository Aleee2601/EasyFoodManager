using EasyFoodManager.ViewModels;
using System.Windows;

namespace EasyFoodManager.Views
{
    public partial class PreparatView : Window
    {
        public PreparatView()
        {
            InitializeComponent();
            DataContext = new PreparatViewModel();
        }
    }
}
