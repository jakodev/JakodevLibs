using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace JakodevLibs.Extensions
{
    public static class Enum
    {
        /// <summary>
        /// Restituisce il valore di una Custom DataAnnotation
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this System.Enum value) where TAttribute : System.Attribute
        {
            var enumType = value.GetType();
            var name = System.Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }


        public static string Name(this System.Enum value)
        {
            return System.Enum.GetName(value.GetType(), value);
        }

        /// <summary>
        /// Converte una stringa in un Enum di un certo tipo cercando il valore la DescriptionAttribute e/o il Name dell'Enum
        /// </summary>
        /// <typeparam name="TEnum">Tipo dell'Enum</typeparam>
        /// <param name="value">Stringa di testo da cercare</param>
        /// <returns>Valore dell'Enum</returns>
        public static TEnum? ConvertToEnum<TEnum>(this string value) where TEnum : struct, IConvertible
        {

            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an enumerated type");

            Type enumType = typeof(TEnum);
            foreach (TEnum e in System.Enum.GetValues(enumType))
            {
                string ename = System.Enum.GetName(enumType, e);
                var descriptionAttribute = enumType.GetField(ename).GetCustomAttributes(false).OfType<DescriptionAttribute>().SingleOrDefault();

                if (ename == value || descriptionAttribute?.Description == value)
                {
                    return e;
                }
            }

            return null;

        }

        /// <summary>
        /// Converte una stringa in un Enum di un certo tipo cercando il valore tra i CustomAttribute
        /// </summary>
        /// <typeparam name="TEnum">Tipo dell'Enum</typeparam>
        /// <typeparam name="TAttribute">Tipo del CustomAttribute</typeparam>
        /// <param name="value">Stringa di testo da cercare</param>
        /// <returns>Valore dell'Enum</returns>
        public static TEnum? ConvertToEnum<TEnum, TAttribute>(this string value) where TEnum : struct, IConvertible where TAttribute : IConvertibleAttribute
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an enumerated type");

            foreach (TEnum e in System.Enum.GetValues(typeof(TEnum)))
            {

                var enumType = e.GetType();
                var name = System.Enum.GetName(enumType, e);
                TAttribute attribute = enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();

                if (attribute != null && attribute.Value == value)
                {
                    return e;
                }

            }

            return null;

        }

        public interface IConvertibleAttribute
        {
            string Value { get; }
        }
    }
}
