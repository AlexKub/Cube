using Merlion.ECR.Update.Core.Log;
using System;
using System.Management;

namespace Merlion.ECR.Update.Core
{
    /// <summary>
    /// Обёртка над WMI class Win32_Service
    /// </summary>
    public partial class WinService
    {
        readonly string m_name;

        /// <summary>
        /// Возвращает или задаёт Тип авторизации Службы
        /// </summary>
        /// <exception cref="NotSupportedException">Передан на вставку не поддерживаемый параметр</exception>
        public AccountType AccountType
        {
            get
            {
                var acType = AccountType.UnKnown;

                var typeName = AccountTypeName;
                if (string.IsNullOrEmpty(typeName))
                    return acType;

                //получение известных значений WMI из аттрибутов перечисления
                var common = acType.GetCommonValues();
                foreach(var kvp in common)
                {
                    if (kvp.Value.Equals(typeName, StringComparison.InvariantCultureIgnoreCase))
                        return kvp.Key;
                }

                return acType; //UnKnown
            }
            set
            {
                var common = value.GetCommonValues();

                if (common.ContainsKey(value))
                    AccountTypeName = common[value];
                else
                    throw new NotSupportedException($"Проставление типа авторизации '{value.ToString()}' не поддерживается");
            }
        }

        /// <summary>
        /// Тип авторизации Службы (LocalSystem / Network System и пр.)
        /// </summary>
        public string AccountTypeName
        {
            get { return GetPropertyValue("StartName", "", (obj) => (string)obj); }
            set
            {
                try
                {
                    using (var service = GetServiceObject())
                    {

                        using (var inParams =
                            service.GetMethodParameters("Change"))
                        {

                            inParams["StartName"] = value;

                            ManagementBaseObject outParams = service.InvokeMethod("Change", inParams, null);
                            outParams.Dispose();
                        }

                    }
                }
                catch (ManagementException err)
                {
                    m_loger.Log("Возникло исключение при изменении Типа авторизации для службы", err);
                }
            }
        }


        public WinService(string name)
        {
            m_name = name;
        }

        /// <summary>
        /// Получение значение свойства объекта WMI
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="propertyName">Имя свойсвта</param>
        /// <param name="defaultVal">Значение по умолчанию</param>
        /// <param name="converter">Конвертация получаемого значения</param>
        /// <returns></returns>
        T GetPropertyValue<T>(string propertyName, T defaultVal, Func<object, T> converter)
        {
            try
            {
                using (var mObj = GetServiceObject())
                {
                    return converter.Invoke(mObj[propertyName]);
                }
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при попытке получения Свойства через WMI", ex
                    , new LogParameter("Имя свойства", propertyName)
                    , new LogParameter("Имя сервиса", m_name));
            }

            return defaultVal;
        }

        ManagementObject GetServiceObject() => new ManagementObject($"Win32_Service.Name='{m_name}'");
    }
}
