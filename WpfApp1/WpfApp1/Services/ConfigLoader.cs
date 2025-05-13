using System;
using System.Configuration;

namespace EasyFoodManager.Services
{
    public static class ConfigLoader
    {
        public static string GetString(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        public static int GetInt(string key)
        {
            string? val = ConfigurationManager.AppSettings[key];
            return int.TryParse(val, out int result) ? result : 0;
        }

        public static decimal GetDecimal(string key)
        {
            string? val = ConfigurationManager.AppSettings[key];
            return decimal.TryParse(val, out decimal result) ? result : 0;
        }

        public static double GetDouble(string key)
        {
            string? val = ConfigurationManager.AppSettings[key];
            return double.TryParse(val, out double result) ? result : 0;
        }
    }
}
