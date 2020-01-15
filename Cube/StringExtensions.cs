namespace Cube
{
    /// <summary>
    /// Расширения для строк
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Троеточие
        /// </summary>
        const string DOTS = "...";
        /// <summary>
        /// Возврат при пустой ссылке
        /// </summary>
        public const string NULL = "NULL";
        /// <summary>
        /// Возврат при пустой строке
        /// </summary>
        public const string EMPTY = "EMPTY";

        /// <summary>
        /// Замена пустых значений строки на 'NULL' или 'EMPTY'
        /// </summary>
        /// <param name="value">Текущее значение строки</param>
        /// <returns>Возвращает текущее значение или строки-заместители, если оно пустое</returns>
        public static string LogValue(this string value)
        {
            return value == null
                ? NULL
                : string.IsNullOrWhiteSpace(value)
                    ? EMPTY
                    : value;
        }

        /// <summary>
        /// Обрезание строки до указанного количества символов. Если лимит превышен - крайние 3 символа заменяются на троеточие
        /// </summary>
        /// <param name="str">Проверяемая строка</param>
        /// <param name="maxLength">Максимальное число символов</param>
        /// <returns>Возвращает переданную строку или обрезанную с вставленным троеточием</returns>
        public static string TrimLength(this string str, int maxLength)
        {
            if (str == null)
                return str;

            if (str.Length > maxLength)
                return str.Substring(str.Length - 3, 3) + DOTS;

            return str;
        }
    }
}
