using EasyFoodManager.ViewModels;
using EasyFoodManager.Services;
using System.Windows;

namespace EasyFoodManager.Views
{
    public partial class ClientDashboardView : Window
    {
        public ClientDashboardView()
        {
            InitializeComponent();

            if (UserContext.CurrentUser == null || !UserContext.IsClient)
            {
                MessageBox.Show("Acces neautorizat.");
                Close();
                return;
            }

            DataContext = new ComandaClientViewModel(UserContext.CurrentUser);
        }
    }
}
