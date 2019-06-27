using System;

namespace Merlion.ECR.Update.Core.Flags
{
    namespace Integer
    {
        /// <summary>
        /// Расширения для битовых перечислений (флагов) типа int
        /// </summary>
        public static class BitEnumExtensions
        {
            /// <summary>
            /// Проверка пересечения битового набора - наличие хотябы одного флага в обоих наборах
            /// </summary>
            /// <typeparam name="TEnum">Тип битового перечисления</typeparam>
            /// <param name="baseFlags">Проверяемая коллекция битовых флагов</param>
            /// <param name="searchFlags">Искомая коллекция битовых флагов</param>
            /// <returns>Возвращает наличие хотябы одного флага в обоих наборах</returns>
            public static bool ContainsFlag<TEnum>(this TEnum baseFlags, TEnum searchFlags) where TEnum : struct
            {
                var en = baseFlags as Enum;

                int baseConverted = BitEnumHelper<TEnum, int>.GetNumber(baseFlags);
                int searchConverted = BitEnumHelper<TEnum, int>.GetNumber(searchFlags);

                if (baseConverted == searchConverted)
                    return true;

                return Contains(baseConverted, searchConverted);
            }

            /// <summary>
            /// Получить общие флаги для битовых перечислений
            /// </summary>
            /// <typeparam name="TEnum">Тип перечисления</typeparam>
            /// <param name="baseFlags">Базовая коллекция</param>
            /// <param name="searchFlags">Искомые флаги</param>
            /// <returns>Возвращает схожие флаги в двух наборах</returns>
            public static TEnum GetSameFlags<TEnum>(this TEnum baseFlags, TEnum searchFlags) where TEnum : struct
            {
                int baseConverted = BitEnumHelper<TEnum, int>.GetNumber(baseFlags);
                int searchConverted = BitEnumHelper<TEnum, int>.GetNumber(searchFlags);

                if (baseConverted == searchConverted)
                    return searchFlags;

                int resultValue = 0;
                foreach (var val in BitEnumHelper<TEnum, int>.Numbers)
                    if (HasFlag(searchConverted, val) && HasFlag(baseConverted, val))
                        resultValue = resultValue | val;

                return (TEnum)(object)resultValue;
            }

            /// <summary>
            /// Проверка наличия только одного флага в наборе (int)
            /// </summary>
            /// <typeparam name="TEnum">Тип битового перечисления</typeparam>
            /// <param name="flag">Проверяемый набор флагов</param>
            /// <returns>Возвращает true если в наборе только один флаг</returns>
            public static bool HasSingleFlag<TEnum>(this TEnum flag) where TEnum : struct
            {
                int integer = BitEnumHelper<TEnum, int>.GetNumber(flag);

                return HasSingleFlag(integer);
            }

            private static bool HasSingleFlag(int value)
            {
                if (value > 2)
                {
                    return (value & (value - 1)) == 0;
                }
                else
                    switch (value)
                    {
                        case 0:
                        case 1:
                        case 2:
                            return true;
                        default:
                            return false;
                    }
            }

            private static bool Contains(int flags, int flag)
            {
                return (flags & flag) != 0;
            }

            private static bool HasFlag(int flags, int flag)
            {
                return (flags & flag) == flag;
            }

            /// <summary>
            /// Проверка вхождения битового набора - искомый набор полностью содержится в базовом
            /// </summary>
            /// <typeparam name="TEnum">Тип перечисления</typeparam>
            /// <param name="baseFlags">Базовый набор флагов</param>
            /// <param name="searchFlags">Искомый набор флагов</param>
            /// <returns>Возвращает true, если искомый набор полностью содержится в базовом</returns>
            public static bool HasFlag<TEnum>(this TEnum baseFlags, TEnum searchFlags) where TEnum : struct
            {
                int baseConverted = BitEnumHelper<TEnum, int>.GetNumber(baseFlags);
                int searchConverted = BitEnumHelper<TEnum, int>.GetNumber(searchFlags);

                return HasFlag(baseConverted, searchConverted);
            }

        }
    }


}
