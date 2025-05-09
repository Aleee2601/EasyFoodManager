using System.Windows;
using System.Windows.Controls;
using EasyFoodManager.ViewModels;

namespace EasyFoodManager.Views
{
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel vm)
            {
                vm.Parola = ((PasswordBox)sender).Password;
            }
        }
    }
}