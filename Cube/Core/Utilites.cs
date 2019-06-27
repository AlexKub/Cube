using Merlion.ECR.Update.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ProcArc = Merlion.ECR.Update.Core.ProcessorArchitecture;

namespace Merlion.ECR.Update.Core_4_0
{
    /// <summary>
    /// Вспомогательные методы
    /// </summary>
    public static class Utilites
    {


        /// <summary>
        /// Проверка на наличие процесса
        /// </summary>
        /// <param name="processID">Идентификатор процесса</param>
        /// <returns>Возвращает флаг наличия активного процесса на текущей машине с указанным ID</returns>
        public static bool ProcessExist(int processID)
        {
            if (processID < 1)
                return false;

            foreach (var process in Process.GetProcesses())
                using (process) //лучше вызвать. Подробнее - https://stackoverflow.com/questions/16957320/what-does-process-dispose-actually-do
                    if (process.Id == processID)
                        return true;

            return false;
        }

        /// <summary>
        /// Пробрасывает исключение, связзанное с переданным кодом ошибки от неуправляемого метода
        /// </summary>
        /// <param name="hresult">Код ошибки из неуправляемого метода</param>
        public static void ThrowExceptionFrom_HRESULT(int errorCode)
        {
            Marshal.ThrowExceptionForHR(errorCode);
        }
        /// <summary>
        /// Возвращает исключение, связзанное с переданным кодом ошибки от неуправляемого метода
        /// </summary>
        /// <param name="hresult">Код ошибки из неуправляемого метода</param>
        public static Exception GetExceptionFrom_HRESULT(int errorCode)
        {
            return Marshal.GetExceptionForHR(errorCode);
        }

        /// <summary>
        /// Получение списка с анонимным типом
        /// </summary>
        /// <typeparam name="T">Анонимный тип</typeparam>
        /// <param name="anonymous">Фейковый экземпляр нужного типа (с нужной структутрой) (будет удалён при создании)</param>
        /// <returns>Возвращает список переданного анонимного типа</returns>
        public static List<T> GetAnonymousList<T>(T anonymous)
        {
            var list = new[] { anonymous }.ToList();

            list.Clear();

            return list;
        }

        public static string GetDotNetFolder(FileInfo assembly)
        {
            var procArc = ProcArc.X86;
            var netVersion = RuntimeVersions.v4;
            if (Core_2_0.Utilites.Is_CLR_Assembly(assembly))
            {
                procArc = Core_2_0.Utilites.GetProcArchitecture(assembly.FullName);

                using (var asmbly = Core.SeparatedAssemblyLoad.SeparatedAssembly.Load(assembly))
                    netVersion = asmbly.GetDotNetVersion();
            }

            return GetDotNetFolder(netVersion, procArc);
        }

        public static string GetDotNetFolder(RuntimeVersions netVersion, ProcArc procArc)
        {
            var winDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);

            var dotNetDir = Path.Combine(winDir, "Microsoft.NET");

            if (!Directory.Exists(dotNetDir))
                throw new DirectoryNotFoundException("Каталог не найден: " + dotNetDir);

            var procDir = string.Empty;
            switch (procArc)
            {
                case ProcArc.IA64:
                case ProcArc.Amd64:
                case ProcArc.MSIL:
                    procDir = "Framework64";
                    break;
                default:
                    procDir = "Framework";
                    break;

            }

            string frameworkDir = Path.Combine(dotNetDir, procDir);

            string versPrefix = string.Empty;
            switch (netVersion)
            {
                case RuntimeVersions.v4:
                    versPrefix = "v4.";
                    break;
                default:
                    versPrefix = "v2.";
                    break;
            }

            var versFolder = string.Empty;
            var frameworkDirectories = new DirectoryInfo(frameworkDir).EnumerateDirectories().Select(di => di.Name);
            foreach (var dir in frameworkDirectories)
                if (dir.StartsWith(versPrefix))
                    versFolder = dir;

            if (string.IsNullOrEmpty(versFolder))
                throw new DirectoryNotFoundException("Каталог с версией .NET не найден по пути: " + frameworkDir);

            return Path.Combine(frameworkDir, versFolder);
        }
    }
}
