using System;
using System.IO;
using System.Reflection;

namespace AppDomainResolver
{
    /// <summary>
    /// Логика разрешения ссылок на файлы библиотек
    /// </summary>
    public class AppDomainResolver : MarshalByRefObject
    {
        /*
         * при тестировании обёртки над AppDomain не получается найти сборку Core_2_0:
         * тесты запускаются из системной папки
         * dll компилиеруется в ECR/Build
         * 
         * Поскольку AppDomain не умеет(!О_о) загружать DLL по удалённому пути
         * приходится делать отдельный класс-загрузчик по известному пути
         * (AppDomain умеет Create)
         * уже через который подгружать нужные сборки с помощью Assembly.LoadFrom
         *
         * гайд найден здесь https://stackoverflow.com/a/23689445/5454565
         * 
         * эта логика нужна только для автотестов
         * потому этот проект имеет смысл только в конфигурации 'Test'
         */

        private string ApplicationBase { get; set; }
     
        public AppDomainResolver()
        {
            ApplicationBase = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            AppDomain.CurrentDomain.AssemblyResolve += Resolve;
        }
     
        private Assembly Resolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            string fileName = string.Format("{0}.dll", assemblyName.Name);
            return Assembly.LoadFile(Path.Combine(ApplicationBase, fileName));
        }
    }
}
