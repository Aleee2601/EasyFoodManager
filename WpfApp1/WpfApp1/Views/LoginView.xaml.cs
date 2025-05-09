using System.Windows;
using System.Windows.Controls;
using EasyFoodManager.ViewModels;

namespace EasyFoodManager.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Parola = ((PasswordBox)sender).Password;
            }
        }
    }
}