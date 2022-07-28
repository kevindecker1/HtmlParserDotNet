using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParserDotNet
{
    internal static class Utilities
    {
        internal static bool HasValue(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return true;
        }

        internal static string ToSafeString(this object value)
        {
            return ToSafeString(value, string.Empty);
        }

        internal static string ToSafeString(object value, string defaultValue)
        {
            try
            {
                if (value == null)
                {
                    return defaultValue;
                }
                else
                {
                    return value.ToString();
                }
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }

        internal static string SafeTrim(this object value)
        {
            return ToSafeString(value, string.Empty).Trim();
        }
    }
}
