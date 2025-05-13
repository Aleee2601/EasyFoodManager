using EasyFoodManager.Services;
using EasyFoodManager.ViewModels;
using System.Windows;

namespace EasyFoodManager.Views
{
    public partial class AngajatDashboardView : Window
    {
        public AngajatDashboardView()
        {
            InitializeComponent();

            if (UserContext.CurrentUser == null || !UserContext.IsAngajat)
            {
                MessageBox.Show("Acces neautorizat.");
                Close();
                return;
            }

            DataContext = new AngajatComenziViewModel();
        }
    }
}
