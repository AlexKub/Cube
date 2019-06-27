using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LogOperators.Models.MVC.OZON
{
    /// <summary>
    /// Базовый контекст для View-контроллеров
    /// </summary>
    public abstract class ControllersViewContext
    {
        /// <summary>
        /// Информация обо всеъ контроллерах и их действиях
        /// </summary>
        readonly static ConcurrentDictionary<string, ControllerActionsInfo> m_controllersActions =
            new ConcurrentDictionary<string, ControllerActionsInfo>();

        /// <summary>
        /// Получение информации о контроллерах указанной Области
        /// </summary>
        /// <param name="area">Область Контроллера</param>
        /// <returns></returns>
        public static IReadOnlyCollection<ControllerActionsInfo> GetAreaControllersInfo(string area)
        {
            if (area == null)
                throw new ArgumentNullException("Пустое имя Области при инициализации Контекста контролллера не предусмотрено");

            if (string.IsNullOrEmpty(area))
                return new List<ControllerActionsInfo>();

            var allControllersInfo = m_controllersActions?.Values?.ToList() ?? new List<ControllerActionsInfo>();

            return allControllersInfo.Where(ai => area.Equals(ai.Area)).ToList();
        }

        /// <summary>
        /// Действия контроллера в текущем контексте
        /// </summary>
        public ControllerActionsInfo ControllerActions { get; private set; }

        /// <summary>
        /// Имя области
        /// </summary>
        public string Area { get; private set; }

        public ControllersViewContext(string area = AreaNames.OZON)
        {
            Area = area;
            ControllerActions = GetOrCreateControllerInfo(area);
        }

        protected abstract ControllerActionsInfo GetOrCreateControllerInfo(string area);

        protected static ControllerActionsInfo CreateControllerInfo<TController>(string area) where TController : System.Web.Mvc.Controller => new ControllerActionsInfo(typeof(TController), area);

        protected static string GetControllerFullName<TController>() where TController : System.Web.Mvc.Controller => typeof(TController).FullName;

        protected static ControllerActionsInfo GetOrCreateControllerInfo<TController>(string area) where TController : System.Web.Mvc.Controller
        {
            string controllerKeyName = GetControllerFullName<TController>();
            ControllerActionsInfo controllerInfo = null;

            if (m_controllersActions.TryGetValue(controllerKeyName, out controllerInfo))
                return controllerInfo;
            else
            {
                controllerInfo = CreateControllerInfo<TController>(area);

                InitControllerInfo<TController>(area, controllerInfo);

                return controllerInfo;
            }
        }

        /// <summary>
        /// Инициализация данных о контроллере
        /// </summary>
        /// <typeparam name="TController">Тип контроллера</typeparam>
        /// <param name="area">Область контроллера</param>
        public static void InitControllerInfo<TController>(string area, ControllerActionsInfo info = null) where TController : System.Web.Mvc.Controller
        {
            if(info == null)
                info = CreateControllerInfo<TController>(area);

            string controllerKeyName = GetControllerFullName<TController>();

            m_controllersActions.AddOrUpdate(controllerKeyName, info, (a, ai_cur) => info);
        }
    }

    /// <summary>
    /// Базовый контекст View-контроллеров
    /// </summary>
    /// <typeparam name="TController">Тип наследника</typeparam>
    public abstract class ControllersViewContext<TController> : ControllersViewContext
        where TController : System.Web.Mvc.Controller
    {

        public ControllersViewContext(string area) : base(area) { }

        protected sealed override ControllerActionsInfo GetOrCreateControllerInfo(string area) => GetOrCreateControllerInfo<TController>(area);
    }
}