using EasyFoodManager.ViewModels;
using System.Windows;

namespace EasyFoodManager.Views
{
    public partial class MeniuView : Window
    {
        public MeniuView()
        {
            InitializeComponent();
            DataContext = new MeniuViewModel();
        }
    }
}
