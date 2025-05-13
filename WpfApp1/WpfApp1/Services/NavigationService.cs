using System.Windows;
using EasyFoodManager.Views;

namespace EasyFoodManager.Services
{
    public static class NavigationService
    {
        public static void NavigateTo(string userType)
        {
            Window? nextWindow = null;

            switch (userType.ToLower())
            {
                case "client":
                    nextWindow = new ClientDashboardView();
                    break;
                case "angajat":
                    nextWindow = new AngajatDashboardView();
                    break;
                default:
                    MessageBox.Show("Rol necunoscut.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }

            nextWindow?.Show();

            // Închide fereastra Login (dacă e deschisă)
            foreach (Window win in Application.Current.Windows)
            {
                if (win is LoginView)
                {
                    win.Close();
                    break;
                }
            }
        }
    }
}
