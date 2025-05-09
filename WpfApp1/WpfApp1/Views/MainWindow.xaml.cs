using System.Windows;
using EasyFoodManager.Views;

namespace EasyFoodManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnContinueWithoutAccount(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigare ca utilizator guest. Se va deschide meniul general.");
            // Deschide pagina cu meniul public
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            var login = new LoginView();
            login.Show();
            this.Close();
        }

        private void OnRegister(object sender, RoutedEventArgs e)
        {
            var register = new RegisterView();
            register.Show();
            this.Close();
        }

    }
}