using System;
using System.Collections.Generic;
using System.Linq;

namespace Merlion.ECR.Update.Core.Flags
{
    /// <summary>
    /// Внутренний помощник для работы с битовыми перечислениями
    /// </summary>
    /// <typeparam name="TEnum">Тип перечисления</typeparam>
    /// <typeparam name="TValue">Тип значения перечисления</typeparam>
    internal static class BitEnumHelper<TEnum, TValue>
        where TEnum : struct
        where TValue : struct
    {

        /// <summary>
        /// Пары число | значение перечисления
        /// </summary>
        private static readonly Dictionary<TEnum, TValue> _enumMap = new Dictionary<TEnum, TValue>();

        /// <summary>
        /// Числовые значения перечисления
        /// </summary>
        internal static readonly IEnumerable<TValue> Numbers;
        
        static BitEnumHelper()
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
                throw new ArrayTypeMismatchException("Type " + type.FullName + " is not enum");

            if (!HasFlagsAttribute(type))
                throw new NotSupportedException("Enumeration " + type.FullName + " has no [Flags] attribute so not supported");

            var enumValues = Enum.GetValues(type).OfType<TEnum>().ToList();

            var enumNumberics = enumValues.Cast<TValue>().ToList();

            for (int i = 0; i < enumValues.Count; i++)
                _enumMap.Add(enumValues[i], enumNumberics[i]);

            Numbers = enumNumberics;
        }

        /// <summary>
        /// Получение числового значения перечисления
        /// </summary>
        /// <param name="value">Значение битового пересчисления</param>
        /// <returns>Возвращает соответствующее числовое значения битового флага/флагов</returns>
        internal static TValue GetNumber(TEnum value)
        {
            TValue result;

            if (!_enumMap.TryGetValue(value, out result))
            {
                result = (TValue)(object)value;

                _enumMap.Add(value, result);
            }

            return result;
        }

        private static bool HasFlagsAttribute(Type type)
        {
            var attrs = type.GetCustomAttributes(false)?.OfType<FlagsAttribute>();

            return attrs != null && attrs.Count() > 0;
        }
    }
}
