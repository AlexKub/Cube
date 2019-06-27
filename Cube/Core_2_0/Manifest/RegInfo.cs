using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Информация о регистрации сборки
    /// </summary>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("{DebugInfo()}")]
    public class RegInfo : IRegInfo
    {
        /*
        поскольку с одной стороны хотелось бы типизированного значения Action
        а с другой стороны доверять десериализации аттрибута к перечислению нельзя
        поскольку при не соответствии вся десериализация вкрячится,
        то делаем два свойства: одно для сериализатора, второе для людей
        */

        /*
        новая версия будет использовать аттрибут Action
        старая версия будет смотреть на Value
        */

        string m_stringAction;
        RegistrationActions m_Action;
        bool m_value;

        /// <summary>
        /// Action для людей
        /// </summary>
        [XmlIgnore]
        public RegistrationActions Action
        {
            get { return m_Action; }
            set
            {
                m_Action = value;
                m_stringAction = m_Action.ToString();
                m_value = m_Action == RegistrationActions.Reg;
            }
        }

        /// <summary>
        /// Action для сериализатора
        /// </summary>
        [XmlAttribute(AttributeName = "Action")]
        public string StringAction
        {
            get { return m_stringAction; }
            set
            {
                m_stringAction = value;

                if (!string.IsNullOrEmpty(value))
                {
                    var type = typeof(RegistrationActions);
                    foreach (string name in Enum.GetNames(type))
                    {
                        if (name.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                        {
                            m_Action = (RegistrationActions)Enum.Parse(type, name);
                            m_value = m_Action == RegistrationActions.Reg;
                            return;
                        }
                    }
                }

                //если ни одно значение не соответствует
                m_Action = RegistrationActions.Ignore;
            }
        }

        [XmlText(Type = typeof(bool))]
        public bool Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;

                if (m_value)
                {
                    m_Action = RegistrationActions.Reg;
                    m_stringAction = m_Action.ToString();
                }
            }
        }

        /// <summary>
        /// Проверка на наличие значимого действия
        /// </summary>
        /// <returns>Флаг наличия значимого действия</returns>
        [XmlIgnore]
        public bool HasAction { get { return m_Action != RegistrationActions.Ignore; } }

        public RegInfo() { }

        public RegInfo(RegistrationActions action) { Action = action; }

        public RegInfo(IRegInfo regInfo)
        {
            Action = regInfo == null ? RegistrationActions.Ignore : regInfo.Action;
        }

        public static implicit operator bool(RegInfo regInfo)
        {
            if (regInfo == null)
                return false;

            return regInfo.Value;
        }

        public static implicit operator RegInfo(bool value)
        {
            return new RegInfo() { Value = value };
        }

        public static bool operator ==(RegInfo first, RegInfo second)
        {
            if (object.ReferenceEquals(first, null) || object.ReferenceEquals(second, null))
                return false;

            return first.Action == second.Action;
        }

        public static bool operator !=(RegInfo first, RegInfo second)
        {
            if (first == null || second == null)
                return true;

            return first.Action != second.Action;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null))
                return false;

            return object.ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return Action.GetHashCode();
        }

        public bool Equals(IRegInfo regInfo)
        {
            if (regInfo == null)
                return false;

            return regInfo.Action == Action;
        }

        string DebugInfo()
        {
            return Action.ToString() + " | " + Value.ToString();
        }
    }
}
