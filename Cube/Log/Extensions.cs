namespace Cube.Log
{
    /// <summary>
    /// Расширения логирования
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Получение не пустого значения из строки для логирования
        /// </summary>
        /// <param name="val">Логируемое значение</param>
        /// <returns>Возвращает переданное значение, если оно не пустое. В противном случае, возвращает NULL или EMPTY</returns>
        public static string GetStringLogVal(string val)
        {
            return val == null ? "NULL" : string.IsNullOrEmpty(val) ? "EMPTY" : val;
        }
    }
}
