using System.Windows;
using EasyFoodManager.Services;
using EasyFoodManager.ViewModels;

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

        private void PreparatList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
