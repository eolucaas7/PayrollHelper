using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PayrollHelper
{
    public static class ValidationHelper
    {
        public static bool IsValidFullName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            name = name.Trim();
            return name.Length >= 5 && name.Length <= 100 && Regex.IsMatch(name, @"^[a-zA-Zа-яА-ЯёЁ\s-]+$");
        }

        public static bool IsValidAmount(string? amountStr)
        {
            if (string.IsNullOrWhiteSpace(amountStr)) return false;
            string normalized = amountStr.Replace(',', '.');
            if (double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
            {
                return amount > 0;
            }
            return false;
        }

        public static bool IsValidTaxRate(string? rateStr)
        {
            if (string.IsNullOrWhiteSpace(rateStr)) return false;
            string normalized = rateStr.Replace(',', '.');
            if (double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out double rate))
            {
                return rate > 0 && rate <= 100;
            }
            return false;
        }
    }
}
