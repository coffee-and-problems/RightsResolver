using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;

namespace RightsResolver.Implementation.Extensions
{
    public static class EnumExtension
    {
        public static T Max<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }

        public static T? SafeParseNullableEnum<T>(this string value) where T : struct
        {
            return Enum.TryParse(value, true, out T result) ? result : (T?) null;
        }

        public static string GetDescription<T>([NotNull] this T value) where T : Enum
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
