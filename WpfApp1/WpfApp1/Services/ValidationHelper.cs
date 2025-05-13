using System.Text.RegularExpressions;

namespace EasyFoodManager.Services
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        public static bool IsPositiveDecimal(string text)
        {
            return decimal.TryParse(text, out var value) && value >= 0;
        }

        public static bool IsNotEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^\+?\d{10,15}$");
        }

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
        }
    }
}
