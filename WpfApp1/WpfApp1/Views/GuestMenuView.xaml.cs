using EasyFoodManager.ViewModels;
using System.Windows;

namespace EasyFoodManager.Views
{
    public partial class GuestMenuView : Window
    {
        public GuestMenuView()
        {
            InitializeComponent();
            DataContext = new GuestMenuViewModel();
        }
    }
}
