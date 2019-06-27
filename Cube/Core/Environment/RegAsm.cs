using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Merlion.ECR.Update.Core.Environment
{
    /// <summary>
    /// Основная логика регистрации в RegAsm.exe, вытянутая через ILSpy (параметр RegFile не реализован)
    /// </summary>
    public static class RegAsm
    {
        #region COM Implements
        internal enum REGKIND
        {
            REGKIND_DEFAULT,
            REGKIND_REGISTER,
            REGKIND_NONE
        }

        [ComVisible(false), Guid("00020406-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        interface ICreateITypeLib
        {
            void CreateTypeInfo();

            void SetName();

            void SetVersion();

            void SetGuid();

            void SetDocString();

            void SetHelpFileName();

            void SetHelpContext();

            void SetLcid();

            void SetLibFlags();

            void SaveAllChanges();
        }

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void LoadTypeLibEx(string strTypeLibName, REGKIND regKind, out ITypeLib TypeLib);

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void RegisterTypeLib(ITypeLib TypeLib, string szFullPath, string szHelpDirs);

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void UnRegisterTypeLib(ref Guid libID, short wVerMajor, short wVerMinor, int lcid, System.Runtime.InteropServices.ComTypes.SYSKIND syskind);

        #endregion

        /// <summary>
        /// Регистрация сборки
        /// </summary>
        /// <param name="asmFile">Ссылка на файл сборки</param>
        /// <param name="codebase">Флаг регистрации codebase</param>
        /// <returns>Возвращает результат регистрации: Installed или Fail</returns>
        public static RegistrationResult Install(FileInfo asmFile, bool codebase = false)
        {
            var assembly = ResolveAssembly(asmFile);

            var services = new RegistrationServices();

            bool regResult = services.RegisterAssembly(assembly, codebase ? AssemblyRegistrationFlags.SetCodeBase : AssemblyRegistrationFlags.None);

            return regResult ? RegistrationResult.Installed : RegistrationResult.Fail;
        }
        /// <summary>
        /// Регистрация библиотеки типов
        /// </summary>
        /// <param name="asmFile">Ссылка на файл библиотеки</param>
        /// <returns>Возвращает результат регистрации: Installed или Fail</returns>
        public static RegistrationResult InstallTypeLib(FileInfo asmFile)
        {
            var assembly = ResolveAssembly(asmFile);

            try
            {
                RegisterMainTypeLib(assembly, asmFile);
                return RegistrationResult.Installed;
            }
            catch
            {
                return RegistrationResult.Fail;
            }
        }

        /// <summary>
        /// Разрегистрация сборки
        /// </summary>
        /// <param name="asmFile">Ссылка на файл сборки</param>
        /// <param name="typeLib">Флаг разрегистрации библиотеки типов *.tlb, зарегистрированной ранее вместе со сборкой</param>
        /// <returns>Возвращает результат операции: Uninstalled или Fail</returns>
        public static RegistrationResult UnInstall(FileInfo asmFile, bool typeLib = true)
        {
            var assembly = ResolveAssembly(asmFile);
            var services = new RegistrationServices();
            bool unregResult = services.UnregisterAssembly(assembly);

            if (!IsAssemblyImportedFromCom(assembly))
            {
                UnRegisterMainTypeLib(asmFile);
            }

            return unregResult ? RegistrationResult.Uninstalled : RegistrationResult.Fail;
        }
        /// <summary>
        /// Разрегистрация библиотеки типов
        /// </summary>
        /// <param name="asmFile">Ссылка на файл сборки</param>
        /// <returns>Возвращает результат операции: Uninstalled или Fail</returns>
        public static RegistrationResult UnInstallTypeLib(FileInfo asmFile)
        {
            var assembly = ResolveAssembly(asmFile);

            try
            {
                if (!IsAssemblyImportedFromCom(assembly))
                {
                    UnRegisterMainTypeLib(asmFile);
                }

                return RegistrationResult.Uninstalled;
            }
            catch
            {
                return RegistrationResult.Fail;
            }
        }

        public static bool HasInstalled_TypeLib(FileInfo asmFile)
        {
            return RegistryKeyOpened(asmFile, "TypeLib");
        }

        public static bool HasInstalled_COM(FileInfo asmFile)
        {
            return RegistryKeyOpened(asmFile, "CLSID");
        }

        #region Private methods

        private static bool RegistryKeyOpened(FileInfo asmFile, string regKey)
        {
            var asm = ResolveAssembly(asmFile);

            using (var classesRootKey = Microsoft.Win32.RegistryKey.OpenBaseKey(
                    Microsoft.Win32.RegistryHive.ClassesRoot, Microsoft.Win32.RegistryView.Default))
            {
                string clsid = asm.GetGUID(); //получаем GUID из атрибута сборки

                clsid = "{" + clsid + "}"; //в ключах реестра - guid'ы, обрамлённые в скобочки

                //поиск в реестре регистронезависимый - можно не переводить в верхний
                using (var clsIdKey = classesRootKey.OpenSubKey(@"Wow6432Node\" + regKey + @"\" + clsid) ??
                                classesRootKey.OpenSubKey(regKey + @"\" + clsid))
                    return clsIdKey != null; //GUID в реестре есть - регистрация была
            }
        }

        private static void UnRegisterMainTypeLib(FileInfo asmFile)
        {
            ITypeLib typeLib = null;
            IntPtr intPtr = (IntPtr)0;
            try
            {
                LoadTypeLibEx(asmFile.FullName, REGKIND.REGKIND_REGISTER, out typeLib);
                typeLib.GetLibAttr(out intPtr);
                System.Runtime.InteropServices.ComTypes.TYPELIBATTR tYPELIBATTR = (System.Runtime.InteropServices.ComTypes.TYPELIBATTR)Marshal.PtrToStructure(intPtr, typeof(System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
                UnRegisterTypeLib(ref tYPELIBATTR.guid, tYPELIBATTR.wMajorVerNum, tYPELIBATTR.wMinorVerNum, tYPELIBATTR.lcid, tYPELIBATTR.syskind);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (intPtr != (IntPtr)0)
                {
                    typeLib.ReleaseTLibAttr(intPtr);
                }
            }
        }

        private static bool IsAssemblyImportedFromCom(Assembly asm)
        {
            IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(asm);
            for (int i = 0; i < customAttributes.Count; i++)
            {
                if (customAttributes[i].Constructor.DeclaringType == typeof(ImportedFromTypeLibAttribute))
                {
                    return true;
                }
            }
            return false;
        }

        private static void RegisterMainTypeLib(Assembly asm, FileInfo asmFile)
        {
            ITypeLib typeLib = null;

            try
            {
                LoadTypeLibEx(asmFile.FullName, REGKIND.REGKIND_REGISTER, out typeLib);
                if (typeLib != null)
                {
                    RegisterTypeLib(typeLib, asmFile.FullName, Path.GetDirectoryName(asmFile.FullName));
                }
            }
            catch (Exception)
            {
            }

            if (typeLib == null)
            {
                typeLib = DoExportAndRegister(asm, Path.ChangeExtension(asmFile.FullName, ".tlb"));
            }

            Marshal.FinalReleaseComObject(typeLib);
        }

        private static Assembly ResolveAssembly(FileInfo asmFile)
        {
            Assembly assembly = null;

            //assembly = Assembly.ReflectionOnlyLoadFrom(asmFile.FullName);
            //assembly = Assembly.ReflectionOnlyLoadFrom(asmFile.FullName);
            assembly = Assembly.LoadFrom(asmFile.FullName);

            if (assembly == null)
            {
                throw new FileNotFoundException("Не удалось загрузить библиотеку при регистрации по пути: " + asmFile.FullName);
            }
            return assembly;

        }

        private static ITypeLib DoExportAndRegister(Assembly asm, string strTypeLibName)
        {
            ITypeLibConverter typeLibConverter = new TypeLibConverter();
            ExporterCallback notifySink = new ExporterCallback();

            //RegAsm'ом используется только флаг OnlyReferenceRegistered если установлен registered
            //поскольку у нас это нигде не использовалось, параметр опущен
            ITypeLib typeLib = (ITypeLib)typeLibConverter.ConvertAssemblyToTypeLib(asm, strTypeLibName, TypeLibExporterFlags.None, notifySink);
            ICreateITypeLib createITypeLib = null;
            try
            {
                createITypeLib = (ICreateITypeLib)typeLib;
                createITypeLib.SaveAllChanges();
            }
            catch
            {
                throw;
            }
            finally
            {
                Marshal.FinalReleaseComObject(createITypeLib);
            }

            return typeLib;
        }

        public static string GetFilePathName(RuntimeVersions netVersion, ProcessorArchitecture procArc)
        {
            var dotNetToolsFolder = Core_4_0.Utilites.GetDotNetFolder(netVersion, procArc);

            var filePathName = Path.Combine(dotNetToolsFolder, "RegAsm.exe");

            if (File.Exists(filePathName))
                throw new FileNotFoundException("Файл RegAsm не найден по пути " + filePathName);

            return filePathName;
        }

        #endregion

        private class ExporterCallback : ITypeLibExporterNotifySink
        {
            public void ReportEvent(ExporterEventKind EventKind, int EventCode, string EventMsg)
            {
                System.Diagnostics.Debug.WriteLine("Регистрация типов в сборке через RegAsm. Сообщение: " + EventMsg);
            }

            public object ResolveRef(Assembly asm)
            {
                string directoryName = Path.GetDirectoryName(asm.Location);
                string name = asm.GetName().Name;
                string text = Path.Combine(directoryName, name) + ".tlb";

                return DoExportAndRegister(asm, text);
            }
        }

    }

}