using Citilink.Log;
using System.Collections.Generic;
using System.Reflection;

namespace LogOperators.Models.MVC
{
    /// <summary>
    /// Информация о реализованных действиях на контроллере
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class ControllerActionsInfo
    {
        readonly string m_hashString;

        readonly HashSet<ControllerViewMethodInfo> m_actions = new HashSet<ControllerViewMethodInfo>();

        /// <summary>
        /// Местоположение контроллера
        /// </summary>
        public string Area { get; private set; }
        /// <summary>
        /// Имя контроллера (короткое)
        /// </summary>
        public string ControllerName { get; private set; }
        /// <summary>
        /// Полное имя класса контроллера
        /// </summary>
        public string ControllerFullName { get; private set; }
        /// <summary>
        /// Отображаемое имя
        /// </summary>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Имя действия по умолчанию
        /// </summary>
        public ControllerViewMethodInfo DefaultActionInfo { get; private set; } = new ControllerViewMethodInfo("Index", "Действие по умолчанию");
        /// <summary>
        /// Имена View-методов
        /// </summary>
        public IReadOnlyCollection<ControllerViewMethodInfo> Actions => m_actions;
        /// <summary>
        /// Флаг наличия зарегистрированных действий
        /// </summary>
        public bool HasActions => m_actions.Count > 0;
        /// <summary>
        /// Информация о реализованных действиях на контроллере
        /// </summary>
        /// <param name="controllerType">Тип контроллера</param>
        /// <param name="areaName">Область контроллера</param>
        public ControllerActionsInfo(System.Type controllerType, string areaName = null)
        {
            var controllerClassName = controllerType.Name;

            m_hashString = areaName == null
                ? controllerClassName
                : areaName + controllerClassName;

            Area = areaName;
            ControllerName = GetControllerName(controllerClassName);
            ControllerFullName = controllerClassName;
            DisplayName = controllerType.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>()?.DisplayName ?? controllerClassName;

            //собираем инфу об отмеченных методах
            GetControllerViewActionsInfo(controllerType);
        }

        void GetControllerViewActionsInfo(System.Type controllerType)
        {
            var controllerMethods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            if (controllerMethods.IsNullOrEmpty())
                ErrorLoger.Log("Контроллер не содержит методов, пригодных для отображения View",
                    parameters: new LogParameter[] {
                        new LogParameter("Имя типа контроллера", controllerType.FullName)
                    });
            else
            {
                foreach (var method in controllerMethods)
                {
                    //аттрибут методов, которые возвращают View
                    var viewMethodAttribute = method.GetCustomAttribute<AvailableViewActionAttribute>();

                    if (viewMethodAttribute == null)
                        continue;

                    //информация для ссылки на метод и его отображения
                    var cvmi = new ControllerViewMethodInfo(method.Name, viewMethodAttribute.DisplayName);

                    m_actions.Add(cvmi);

                    if (viewMethodAttribute.IsDefault)
                        DefaultActionInfo = cvmi;
                }

                if (!HasActions)
                    ErrorLoger.Log("Контроллер не содержит методов, помеченных аттрибутом AvailableViewActionAttribute",
                    parameters: new LogParameter[] {
                        new LogParameter("Имя типа контроллера", controllerType.FullName)
                    });
            }
        }

        string GetControllerName(string fullName) => fullName.Replace("Controller", "");

        public override int GetHashCode() => m_hashString == null ? 0 : m_hashString.GetHashCode();

        //Area | ControllerClassName [m_actions.Count] if(actions > 0) : <action>[, <action>]
        string DebugDisplay() => $"{(Area == null ? string.Empty : Area)} | {ControllerFullName} [{m_actions.Count.ToString()}]{(m_actions.Count > 0 ? (": " + string.Join(", ", m_actions)) : string.Empty )}";
    }
}