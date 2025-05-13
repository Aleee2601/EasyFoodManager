using EasyFoodManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EasyFoodManager.Views
{
    public partial class RegisterView : Window
    {
        private RegisterViewModel _viewModel;

        public RegisterView()
        {
            InitializeComponent();
            _viewModel = new RegisterViewModel();
            _viewModel.OnRegisterSuccess = () =>
            {
                MessageBox.Show("Cont creat cu succes!");
                var login = new LoginView();
                login.Show();
                this.Close();
            };
            DataContext = _viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel vm)
                vm.Parola = ((PasswordBox)sender).Password;
        }
    }
}
