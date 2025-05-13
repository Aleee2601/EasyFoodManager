using System;
using System.Data;

namespace EasyFoodManager.Helpers
{
    public static class DataReaderExtensions
    {
        public static int GetInt(this IDataRecord record, string columnName)
        {
            int ordinal = record.GetOrdinal(columnName);
            return record.IsDBNull(ordinal) ? 0 : record.GetInt32(ordinal);
        }

        public static decimal GetDecimal(this IDataRecord record, string columnName)
        {
            int ordinal = record.GetOrdinal(columnName);
            return record.IsDBNull(ordinal) ? 0 : record.GetDecimal(ordinal);
        }

        public static double GetDouble(this IDataRecord record, string columnName)
        {
            int ordinal = record.GetOrdinal(columnName);
            return record.IsDBNull(ordinal) ? 0.0 : Convert.ToDouble(record.GetValue(ordinal));
        }

        public static string GetString(this IDataRecord record, string columnName)
        {
            int ordinal = record.GetOrdinal(columnName);
            return record.IsDBNull(ordinal) ? string.Empty : record.GetString(ordinal);
        }

        public static DateTime GetDateTime(this IDataRecord record, string columnName)
        {
            int ordinal = record.GetOrdinal(columnName);
            return record.IsDBNull(ordinal) ? DateTime.MinValue : record.GetDateTime(ordinal);
        }

        public static bool GetBool(this IDataRecord record, string columnName)
        {
            int ordinal = record.GetOrdinal(columnName);
            return !record.IsDBNull(ordinal) && record.GetBoolean(ordinal);
        }

        public static T GetValue<T>(this IDataRecord record, string columnName)
        {
            int ordinal = record.GetOrdinal(columnName);
            return record.IsDBNull(ordinal) ? default(T) : (T)record.GetValue(ordinal);
        }
    }
}
