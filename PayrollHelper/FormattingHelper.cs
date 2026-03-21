using System;
using System.Text.RegularExpressions;

namespace PayrollHelper
{
    public static class FormattingHelper
    {
        public static string FormatPhoneNumber(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            string allDigits = Regex.Replace(input, @"\D", "");
            string cleanDigits = allDigits;

            if (allDigits.Length > 10 && allDigits.StartsWith("7"))
            {
                cleanDigits = allDigits.Substring(1);
            }
            else if (allDigits.Length == 11 && allDigits.StartsWith("8"))
            {
                cleanDigits = allDigits.Substring(1);
            }

            return "+7" + cleanDigits;
        }
    }
}
