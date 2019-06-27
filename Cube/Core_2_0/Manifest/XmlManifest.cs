using Merlion.ECR.Update.Core.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// XML-сериализуемый класс
    /// </summary>
    [Serializable]
    public abstract class XmlManifest
    {
        /// <summary>
        /// Кеширование xml-сериализаторов
        /// </summary>
        static Dictionary<Type, XmlSerializer> m_serializers = new Dictionary<Type, XmlSerializer>();

        /// <summary>
        /// Экземпляр логера
        /// </summary>
        [NonSerialized]
        protected static readonly ILoger m_loger = LogManager.GetDefaultLogSet<XmlManifest>();

        /// <summary>
        /// Флаг логирования исключений при сериализации
        /// </summary>
        [XmlIgnore]
        public static bool LogExceptions { get; set; } = true;

        /// <summary>
        /// Флаг проброса исключений при сериализации
        /// </summary>
        [XmlIgnore]
        public static bool ThrowSerializeExceptions { get; set; } = false;

        /// <summary>
        /// Информация о файле Манифеста
        /// </summary>
        [XmlIgnore]
        public FileInfo FileInfo { get; protected set; }

        /// <summary>
        /// Флаг наличия файла для данного манифеста
        /// </summary>
        [XmlIgnore]
        public bool FileExist { get { return FileInfo != null && FileInfo.Exists; } }

        /// <summary>
        /// Получение манифеста по умочланию для данного типа
        /// </summary>
        /// <returns>Возвращает манифест по умочланию текущего типа</returns>
        protected abstract XmlManifest GetDefault();

        /// <summary>
        /// Получение имени файла по умолчанию для манфиеста текущего типа
        /// </summary>
        /// <returns>Имя файла по умочланию для текущего манфиеста</returns>
        protected abstract string GetDefaultFileName();

        /// <summary>
        /// Логика сохранения изменений для текущего манифеста
        /// </summary>
        /// <param name="pathName">Полный путь для сохранения файла</param>
        public abstract void Save(string pathName = null);

        /// <summary>
        /// Десериализация манифеста из файла
        /// </summary>
        /// <typeparam name="TManifest">Тип манифеста</typeparam>
        /// <param name="pathName">Полный путь к десериализуемому файлу</param>
        /// <param name="rebuildOnEx">Перезапись существующего файла файлом по умолчанию в случае ошибки десериализации</param>
        /// <returns>Возвращает десериализованный экземпляр или экземпляр по умолчанию</returns>
        protected internal static TManifest Deserialize<TManifest>(string pathName, bool rebuildOnEx = false)
            where TManifest : XmlManifest, new()
        {
            try
            {
                TManifest manifest = null;
                if (string.IsNullOrEmpty(pathName))
                {
                    manifest = (TManifest)new TManifest().GetDefault();
                    pathName = manifest.GetDefaultFileName();
                }
                if (!File.Exists(pathName))
                {
                    manifest = (TManifest)new TManifest().GetDefault();

                    manifest.FileInfo = new FileInfo(pathName);

                    try
                    {
                        using (MutexLocker.Lock(typeof(TManifest).FullName))
                            manifest.Save(pathName);
                    }
                    catch (Exception ex)
                    {
                        m_loger.Log("Возникло исключение при генерации манифеста по умолчанию", ex, MessageType.Warning, new LogParameter("Файл", LogMessageBuilder.GetStringLogVal(pathName)));
                    }

                    return manifest;
                }
                using (var sr = new StringReader(File.ReadAllText(pathName)))
                {
                    XmlSerializer xs = GetSerializer<TManifest>();

                    manifest = xs.Deserialize(sr) as TManifest;
                    manifest.FileInfo = new FileInfo(pathName);
                    return manifest;
                }
            }
            catch (Exception ex)
            {
                if (ThrowSerializeExceptions)
                    throw;

                if (LogExceptions)
                    m_loger.Log("Возникло исключение при десериализации манифеста. Манифест будет удалён и перегенерирован заново.", ex
                        , new LogParameter("Тип манифеста", typeof(TManifest).FullName)
                        , new LogParameter("Путь к файлу", pathName));

                TManifest newMon = null;

                if (!rebuildOnEx)
                    return newMon;
                else
                    try
                    {
                        //пересоздаём манифест по умолчанию
                        newMon = new TManifest();

                        if (File.Exists(pathName))
                        {
                            File.Delete(pathName);
                        }

                        using (MutexLocker.Lock(typeof(TManifest).FullName))
                            Serialize(newMon, pathName);

                        return newMon;
                    }
                    catch (Exception subEx)
                    {
                        if (LogExceptions)
                            m_loger.Log("Возникло исключение при попытке сериализации манифеста по умолчанию. Возвращена пустая ссылка на манифест", subEx
                            , new LogParameter("Тип манифеста", typeof(TManifest).FullName)
                            , new LogParameter("Путь к файлу", pathName));

                        return null;
                    }
            }
        }

        /// <summary>
        /// Сериализация (сохранение) передаваемого экземпляра
        /// </summary>
        /// <typeparam name="TManifest">Тип сохраняемого манфиеста</typeparam>
        /// <param name="manifest">Сохраянемый экземпляр</param>
        /// <param name="pathName">Полный путь сохраняемого файла</param>
        /// <returns>Возвращает флаг успешного сохранения экземпляра</returns>
        protected internal static bool Serialize<TManifest>(TManifest manifest, string pathName) where TManifest : XmlManifest
        {
            try
            {
                XmlSerializer xs = GetSerializer<TManifest>();

                using (MutexLocker.Lock(typeof(TManifest).FullName))
                {
                    using (var fw = File.Create(pathName))
                        xs.Serialize(fw, manifest);
                }

                manifest.FileInfo = new FileInfo(pathName);

                return true;
            }
            catch (Exception ex)
            {
                if (ThrowSerializeExceptions)
                    throw;

                if (LogExceptions)
                    m_loger.Log("Возникло исключение при сериализации манифеста", ex
                        , new LogParameter("Тип манифеста", typeof(TManifest).FullName)
                        , new LogParameter("Путь к файлу", pathName));

                return false;
            }
        }

        /// <summary>
        /// Получение первого реального (не абстрактоного) родителя
        /// </summary>
        /// <typeparam name="TManifest">Текущий тип, унаследованный от XmlManifest</typeparam>
        /// <returns>Возвращает первого не абстрактного родителя (или текущий тип, если он таковым является)</returns>
        private static Type GetBaseType<TManifest>() where TManifest : XmlManifest
        {
            /*
             * поиск первого родителя
             */

            //сериализцемый класс
            var curent = typeof(TManifest);

            if (curent.IsAbstract)
                throw new XmlSerializationException("Сериализуемый класс не может быть абстрактным", curent);

            var baseType = curent.BaseType;

            while (!baseType.IsAbstract)
            {
                curent = baseType;
                baseType = baseType.BaseType;
            }

            return curent;
        }

        /// <summary>
        /// Получение цепи наследования (кроме абстрактных классов)
        /// </summary>
        /// <typeparam name="TManifest">Текущий тип</typeparam>
        /// <returns>Возвращает цепь наследников для текущего типа</returns>
        private static Type[] GetDerivedTypes<TManifest>() where TManifest : XmlManifest
        {
            var curent = typeof(TManifest);
            var xmlManifestName = typeof(XmlManifest).FullName;

            List<Type> _derivedTypes = new List<Type>();

            if (curent.IsAbstract)
                throw new XmlSerializationException("Сериализуемый класс не может быть абстрактным", curent);

            var baseType = curent.BaseType;

            while (!baseType.IsAbstract)
            {
                _derivedTypes.Add(curent);
                curent = baseType;
                baseType = baseType.BaseType;
            }

            return _derivedTypes.ToArray();
        }

        /// <summary>
        /// Получение сериализатора для чтения/записи XML
        /// </summary>
        /// <typeparam name="TManifest">Текущий тип сериализуемого/десериализуемого манифеста</typeparam>
        /// <returns>Возвращает сериализатор из кеша или новый, кешируя его</returns>
        private static XmlSerializer GetSerializer<TManifest>() where TManifest : XmlManifest
        {
            var mtype = typeof(TManifest);
            XmlSerializer xs = null;
            if (m_serializers.ContainsKey(mtype))
                xs = m_serializers[mtype];
            else
            {
                xs = new XmlSerializer(GetBaseType<TManifest>(), GetDerivedTypes<TManifest>());
                m_serializers.Add(mtype, xs);
            }

            return xs;
        }
    }
}
