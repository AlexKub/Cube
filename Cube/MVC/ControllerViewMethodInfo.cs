namespace LogOperators.Models.MVC
{
    /// <summary>
    /// Информация о View-методе контроллера
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class ControllerViewMethodInfo
    {
        /// <summary>
        /// Имя метода контроллера
        /// </summary>
        public string MethodName { get; private set; }
        /// <summary>
        /// Отображаемое имя на панели действий
        /// </summary>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Информация о View-методе контроллера
        /// </summary>
        /// <param name="methodName">Имя метода контроллера</param>
        /// <param name="displayName">Отображаемое имя на панели действий</param>
        public ControllerViewMethodInfo(string methodName, string displayName)
        {
            MethodName = methodName;
            DisplayName = displayName;
        }

        string DebugDisplay()
        {
            return (string.IsNullOrWhiteSpace(MethodName) ? "NO_NAME" : MethodName)
                + " | "
                + (string.IsNullOrWhiteSpace(DisplayName) ? "NO_DisplayName" : DisplayName);
        }

        public override int GetHashCode() => string.IsNullOrEmpty(MethodName) ? 0 : MethodName.GetHashCode();


    }
}