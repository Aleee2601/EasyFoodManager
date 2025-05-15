using EasyFoodManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EasyFoodManager.Views
{
    public partial class LoginView : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;

            // setează parola la fiecare modificare
            _viewModel.OnLoginSuccess = NavigheazaInFunctieDeRol;

        }
        private void NavigheazaInFunctieDeRol(Models.Utilizator user)
        {
            Services.UserContext.CurrentUser = user;

            Window urmatoareaFereastra = null;

            if (user.Tip == "Client")
                urmatoareaFereastra = new ClientDashboardView();
            else if (user.Tip == "Angajat")
                urmatoareaFereastra = new AngajatDashboardView();

            urmatoareaFereastra?.Show();
            this.Close();
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
