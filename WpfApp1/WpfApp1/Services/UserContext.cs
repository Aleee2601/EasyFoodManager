using EasyFoodManager.Models;

namespace EasyFoodManager.Services
{
    public static class UserContext
    {
        public static Utilizator? CurrentUser { get; set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static bool IsClient => CurrentUser?.Tip?.ToLower() == "client";
        public static bool IsAngajat => CurrentUser?.Tip?.ToLower() == "angajat";

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
