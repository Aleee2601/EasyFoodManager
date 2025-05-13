using Microsoft.Win32;
using System.Windows;

namespace EasyFoodManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerView = new RegisterView();
            registerView.Show();
            this.Close();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            var guestView = new GuestMenuView(); // trebuie creat separat
            guestView.Show();
            this.Close();
        }
    }
}
