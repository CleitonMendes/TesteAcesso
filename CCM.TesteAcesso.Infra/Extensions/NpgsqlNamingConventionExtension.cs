using System.Text.RegularExpressions;

namespace MiraeDigital.FixedIncome.Infrastructure.Extensions
{
    public static class NpgsqlNamingConventionExtension
    {
        public static string AsNpgsqlConvention(this string? value)
        {
            var result = Regex.Split(value, @"(?<!^)(?=[A-Z])");

            return string.Join('_', result).Replace("__", "_").ToLower();
        }
    }
}
