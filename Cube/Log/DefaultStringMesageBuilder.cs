﻿using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Cube.Log
{
    /// <summary>
    /// Построитель строки логирования по умолчанию
    /// </summary>
    public class DefaultStringMesageBuilder : IMeassageBuilder<string, DefaultStringMessageData>
    {
        private static Type ExceptionType = typeof(Exception);

        /// <summary>
        /// Отступ по умолчанию
        /// </summary>
        public const string DefaultIndent = "    ";

        /// <summary>
        /// Делегаты логирования конкретных типов исключений
        /// </summary>
        readonly Dictionary<Type, Action<StringBuilder, string, Exception>> m_ExceptionBuilders = new Dictionary<Type, Action<StringBuilder, string, Exception>>();

        string m_indent = DefaultIndent;
        /// <summary>
        /// Отступ
        /// </summary>
        public string Indent { get => m_indent; set => m_indent = string.IsNullOrEmpty(value) ? DefaultIndent : value; }

        /// <summary>
        /// Построитель строки логирования по умолчанию
        /// </summary>
        public DefaultStringMesageBuilder()
        {
            AddDefaultExceptionBuilders();
        }

        void AddDefaultExceptionBuilders()
        {
            AddExceptionBuildHandler<System.Reflection.ReflectionTypeLoadException>(ReflectionTypeLoadExceptionBuilding);
        }

        /// <summary>
        /// Действие при исключениях во время построения сообщения
        /// </summary>
        public Action<DefaultStringMessageData> BuildExceptionsHandler { get; set; }

        /// <summary>
        /// Добавление логики построения развёрнутого сообщения для исключений конкретного типа
        /// </summary>
        /// <typeparam name="T">Тип исключения</typeparam>
        /// <param name="handler">Делегат логики построения сообщения (builder, indent, exception)</param>
        public void AddExceptionBuildHandler<T>(Action<StringBuilder, string, T> handler) where T : Exception
        {
            if (handler == null)
                return;

            var t = typeof(T);

            if (m_ExceptionBuilders.ContainsKey(t))
                m_ExceptionBuilders[t] = (Action<StringBuilder, string, Exception>)handler;
            else
                m_ExceptionBuilders.Add(t, (Action<StringBuilder, string, Exception>)handler);
        }

        /// <summary>
        /// Удаление логики построения развёрнутого сообщения для исключений конкретного типа
        /// </summary>
        /// <typeparam name="T">Тип исключения</typeparam>
        public void RemoveExceptionBuildHandler<T>() where T : Exception
        {
            var t = typeof(T);

            m_ExceptionBuilders.Remove(t);
        }

        #region построение сообщения

        /// <summary>
        /// Построение сообщения логирования
        /// </summary>
        /// <param name="data">Логируемые данные</param>
        /// <returns>Возвращает готовую строку для логирования</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Метод используется только внутри DefaultStringMesageBuilder. Публичный в виду наследования IMeassageBuilder")]
        public string Build(DefaultStringMessageData data) => BuildInternal(ref data).ToString();

        /// <summary>
        /// Построение сообщения об исключении
        /// </summary>
        /// <param name="sb">Текущий экземпляр построителя</param>
        /// <param name="ex">Экземмпляр исключения</param>
        /// <param name="indent">Отступ от левого края</param>
        /// <returns>ВОзвращает текущий построитель сообщения</returns>
        public StringBuilder AppendException(StringBuilder sb, Exception ex, string indent = null)
        {
            if (sb == null)
                sb = new StringBuilder();

            if (ex == null)
                return sb;

            string curIndent = indent == null ? string.Empty : indent;

            var t = ex.GetType();

            if (m_ExceptionBuilders.ContainsKey(t))
                //строим сообщение имеющимся делегатом для данного типа исключений
                m_ExceptionBuilders[t](sb, curIndent, ex);
            else
                //строим сообщение стандартной логикой
                DefaultExceptionBuildHandler(sb, curIndent, ex);

            if (ex.InnerException != null)
            {
                sb.Append(curIndent).AppendLine("Вложенное исключение: ---------------------");
                AppendException(sb, ex.InnerException, curIndent + Indent);
            }

            return sb;
        }

        /// <summary>
        /// Конкатинация параметров логирования
        /// </summary>
        /// <param name="sb">Экземпляр StringBuilder для конкатинации</param>
        /// <param name="parameters">Коллекция параметров</param>
        /// <param name="paramsHeader">Заголовок секции с параметрами</param>
        /// <param name="indent">Отступ для строк</param>
        /// <returns>Возвращает использованный для конкатинации экземпляр StringBuilder</returns>
        public StringBuilder AppendParameters(StringBuilder sb, ICollection<IStringLogParameter> parameters, string indent = "")
        {
            if (sb == null)
                sb = new StringBuilder();

            var paramsHeader = "Параметры: -----------------";

            if (parameters == null || parameters.Count == 0)
                return sb;

            sb.Append(indent).AppendLine(paramsHeader);

            foreach (var p in parameters)
            {
                if (p == null)
                    continue;

                try
                {
                    p.Append(sb, indent);
                }
                catch (Exception ex)
                {
                    AppendLogParameterException(sb, p, ex, indent);
                }
            }

            return sb;
        }

        /// <summary>
        /// Логирование исключения при логировании параметра
        /// </summary>
        /// <param name="sb">Текущий построитель сообщения</param>
        /// <param name="p">Логируемый параметр</param>
        /// <param name="ex">Экземпляр исключения</param>
        /// <param name="indent">Отступ текущей строки</param>
        void AppendLogParameterException(StringBuilder sb, IStringLogParameter p, Exception ex, string indent)
        {
            /*
            логируем исключение при добавлении параметра в сообщение

            возможно получется невротбольшой лог - надеемся, что всё будет ок

            параметр ожидается НЕ нулевым
            */

            string exCase = "<getException>";
            string pName = exCase;
            string pValue = exCase;

            //чуть отступаем для вложенных исключений
            var subExIndent = indent + Indent;
            var nameValueIndent = subExIndent + Indent;
            var data = new DefaultStringMessageData();

            try
            {
                pName = p.Name;
            }
            catch (Exception nameEx)
            {
                data = new DefaultStringMessageData() {
                    Message = "Возникло исключение при получении имени параметра логирования",
                    Parameters = new StringLogParameter[] { new StringLogParameter("Тип параметра", p.GetType().FullName) },
                    Exception = nameEx
                };

                AppendInternal(sb, ref data, nameValueIndent);
            }

            try
            {
                pValue = p.Value;
            }
            catch (Exception valueEx)
            {
                data = new DefaultStringMessageData()
                {
                    Message = "Возникло исключение при получении имени параметра логирования",
                    Parameters = pName == exCase //если с именем проблем не было, логируем ещё и имя
                        ? new StringLogParameter[] { new StringLogParameter("Тип параметра", p.GetType().FullName) }
                        : new StringLogParameter[] { new StringLogParameter("Тип параметра", p.GetType().FullName), new StringLogParameter("Имя параметра", p.Name) },
                    Exception = valueEx
                };

                AppendInternal(sb, ref data, nameValueIndent);
            }

            //если с именем и значением проблем не было, логируем ещё их
            var subExParams = new List<IStringLogParameter>();
            subExParams.Add(new StringLogParameter("Тип параметра", p.GetType().FullName));

            if (pName != exCase)
                subExParams.Add(new StringLogParameter("Имя параметра", pName));
            if (pValue != exCase)
                subExParams.Add(new StringLogParameter("Значение параметра", pValue));

            data = new DefaultStringMessageData()
            {
                Message = "Возникло исключение при построении сообщения логирования для параметра",
                Parameters = subExParams,
                Exception = ex
            };

            AppendInternal(sb, ref data, subExIndent);
        }

        /// <summary>
        /// Построение сообщений для исключения загрузки типа
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="indent"></param>
        /// <param name="ex"></param>
        void ReflectionTypeLoadExceptionBuilding(StringBuilder sb, string indent, Exception ex)
        {
            //начало стандартное
            DefaultExceptionBuildHandler(sb, indent, ex);

            //исключение возникает при ошибке загрузке типа в сборке
            //причина специфического логирования - у этих исключений подробная информация в отдельном свойстве
            //подробнее о типе - https://msdn.microsoft.com/ru-ru/library/system.reflection.reflectiontypeloadexception(v=vs.110).aspx
            var refTypeLoadEx = ex as System.Reflection.ReflectionTypeLoadException;
            if (refTypeLoadEx != null)
            {
                sb.Append(indent).AppendLine("Исключение загрузчика: -----------------");
                var subIndent = indent + Indent;
                sb.Append(subIndent).Append("Количество исключений: ");
                var loaderExceptions = refTypeLoadEx.LoaderExceptions;
                if (loaderExceptions == null || loaderExceptions.Length == 0)
                    sb.AppendLine("0");
                else
                {
                    var exCount = loaderExceptions.Length;
                    sb.AppendLine(exCount.ToString());

                    for (int i = 0; i < exCount; i++)
                    {
                        sb.Append(subIndent).Append("Исключение ").Append(i.ToString()).AppendLine(": -----------");
                        AppendException(sb, loaderExceptions[i], subIndent + Indent);
                    }
                }
            }
        }

        /// <summary>
        /// Построение сообщения логирования для исключения по умолчанию
        /// </summary>
        /// <param name="sb">Построитель сообщения</param>
        /// <param name="indent">Текущий отступ</param>
        /// <param name="ex">Экземпляр исключения</param>
        static void DefaultExceptionBuildHandler(StringBuilder sb, string indent, Exception ex)
        {
            sb.Append(indent).Append("Тип исключения | ").AppendLine(ex.GetType().FullName);
            sb.Append(indent).Append("Сообщение исключения: ").AppendLine(ex.Message);

            sb.Append(indent).Append("Стек вызова:"); //строчку НЕ завершаем

            //стек может быть пуст
            if (string.IsNullOrEmpty(ex.StackTrace))
                sb.AppendLine(ex.StackTrace.LogValue()); //закрываем на той же строке
            else
            {
                sb.AppendLine(); //при последнем Append'е не было конца строки - начинаем со следующей
                using (var stringReader = new StringReader(ex.StackTrace))
                {
                    string _LineText = stringReader.ReadLine();

                    while (_LineText != null)
                    {
                        //добавляем отступ к каждой строчке StackTrace
                        sb.Append(indent).AppendLine(_LineText.Trim());
                        _LineText = stringReader.ReadLine();
                    }
                }
            }
        }

        /// <summary>
        /// Основной метод построения сообщения логирования
        /// </summary>
        /// <param name="data">Логируемые данные</param>
        /// <returns>Возвращает готовую строку для логирования</returns>
        StringBuilder BuildInternal(ref DefaultStringMessageData data)
        {
            var sb = new StringBuilder();

            return AppendInternal(sb, ref data);
        }

        /// <summary>
        /// Добавление данных к логируемой строке
        /// </summary>
        /// <param name="sb">Построитель логируемой строки</param>
        /// <param name="data">Логируемые данные</param>
        /// <param name="indent">Отступ строки</param>
        /// <returns>Возвращает экземпляр текущего построителя</returns>
        StringBuilder AppendInternal(StringBuilder sb, ref DefaultStringMessageData data, string indent = null)
        {
            try
            {
                if (indent == null)
                    indent = string.Empty;

                sb.Append(indent).Append(data.Message.LogValue());

                if (data.HasParameters)
                    AppendParameters(sb, data.Parameters, indent);

                if (data.HasException)
                    AppendException(sb, data.Exception, indent);

                return sb;
            }
            catch (Exception ex)
            {

                BuildExceptionsHandler?.Invoke(new DefaultStringMessageData()
                {
                    Message = "Возникло исключение при построении сообщения логирования",
                    Exception = ex,
                    Parameters = data.Parameters
                });

                return new StringBuilder("Возникло исключение при построении сообщения логирования: ").Append(ex.Message);
            }
        }

        #endregion
    }
}
