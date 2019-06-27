using System;
using System.IO;
using Merlion.ECR.Update.Core.Flags.Integer;
using System.Diagnostics;
using Merlion.ECR.Update.Core.Log;

namespace Merlion.ECR.Update.Core.Environment
{

    /// <summary>
    /// Управление регистрацией/разрегистрацией сборок
    /// </summary>
    public static class AssemblyManager
    {
        readonly static string RegAsmPath = @"C:\Windows\Microsoft.NET\Framework\v2.0.50727\RegAsm.exe";
        readonly static LogSet m_loger = LogManager.GetDefaultLogSet(nameof(AssemblyManager));


        /// <summary>
        /// Массовая регистрация сборок
        /// </summary>
        /// <param name="assembly">Ссылка на файл сборки</param>
        /// <param name="context">Флаги регистрации</param>
        /// <param name="force">ФЛаг принудительной регистрации (true по умолчанию)</param>
        /// <returns>Возвращает результаты выполненных операций</returns>
        public static Result Install(FileInfo assembly, AssemblyRegContext context, bool force = true)
        {

            RegistrationResult gacResult = RegistrationResult.UnKnown;
            if (context.ContainsFlag(AssemblyRegContext.GAC))
                gacResult = Install_GAC(assembly);

            var regAsmResult = RegistrationResult.UnKnown;
            if (context.ContainsFlag(AssemblyRegContext.RegAsm))
                regAsmResult = Install_ByRegAsm(assembly);

            var regSvrResult = RegistrationResult.UnKnown;
            if (context.ContainsFlag(AssemblyRegContext.RegSvr32))
                //regSvrResult = ResultFromExitcode(Install_ByRegSvr32(assembly));
                regSvrResult = Install_ByRegSvr32(assembly);

            return new Result(gacResult, regAsmResult, regSvrResult);
        }

        /// <summary>
        /// Массовая разрегистрация сборок (для RegSvr32 не реализовано)
        /// </summary>
        /// <param name="assembly">Ссылка на файл сборки</param>
        /// <param name="context">Флаги регистрации</param>
        /// <param name="force">ФЛаг принудительной регистрации (true по умолчанию)</param>
        /// <returns>Возвращает результаты выполненных операций</returns>
        public static Result UnInstall(FileInfo assembly, AssemblyRegContext context, bool force = true)
        {

            RegistrationResult gacResult = RegistrationResult.UnKnown;
            if (context.ContainsFlag(AssemblyRegContext.GAC))
                gacResult = UnInstall_GAC(assembly);

            var regAsmResult = RegistrationResult.UnKnown;
            if (context.ContainsFlag(AssemblyRegContext.RegAsm))
                regAsmResult = UnInstall_ByRegAsm(assembly);

            var regSvrResult = RegistrationResult.UnKnown; //не реализовано

            return new Result(gacResult, regAsmResult, regSvrResult);
        }

        /// <summary>
        /// Массовая перерегистрация сборок (разрегистрация и регистрация заново) (Разрегистрация для RegSvr32 е реализовано)
        /// </summary>
        /// <param name="assembly">Ссылка на файл сборки</param>
        /// <param name="context">Флаги регистрации</param>
        /// <param name="force">ФЛаг принудительной регистрации (true по умолчанию)</param>
        /// <returns>Возвращает результаты выполненных операций</returns>
        public static Result ReInstall(FileInfo assembly, AssemblyRegContext context, bool force = true)
        {

            RegistrationResult gacResult = RegistrationResult.UnKnown;
            if (context.ContainsFlag(AssemblyRegContext.GAC))
            {
                UnInstall_GAC(assembly);
                gacResult = Install_GAC(assembly);
            }

            var regAsmResult = RegistrationResult.UnKnown;
            if (context.ContainsFlag(AssemblyRegContext.RegAsm))
            {
                UnInstall_ByRegAsm(assembly);
                regAsmResult = Install_ByRegAsm(assembly);
            }

            var regSvrResult = RegistrationResult.UnKnown;
            if (context.ContainsFlag(AssemblyRegContext.RegSvr32))
                //regSvrResult = ResultFromExitcode(Install_ByRegSvr32(assembly));
                regSvrResult = Install_ByRegSvr32(assembly);

            return new Result(gacResult, regAsmResult, regSvrResult);
        }

        /// <summary>
        /// Регистрация сборки через RegAsm
        /// </summary>
        /// <param name="asm">Ссылка на файл сборки</param>
        /// <param name="force">Флаг принудительной установки</param>
        /// <returns>Возвращает результат операции</returns>
        public static RegistrationResult Install_GAC(FileInfo asm)
        {
            return GAC.ReInstallAssembly(asm);
            //string command = "/i " + asm.FullName + (force ? " /f" : "");
            //return RunHiddenProcess(RegAsmPath, command);
        }
        /// <summary>
        /// Регистрация сборки через RegAsm
        /// </summary>
        /// <param name="asm">Ссылка на файл сборки</param>
        /// <param name="force">Флаг принудительной операции</param>
        /// <returns>Возвращает результат операции</returns>
        public static RegistrationResult UnInstall_GAC(FileInfo asm)
        {
            return GAC.UnInstallAssembly(asm);

            //string command = $"/u" 
            //    + (force ? "f" : "") 
            //    + " " 
            //    + Path.GetFileNameWithoutExtension(asm.FullName);
            ////string command = "/i " + asm.FullName + (force ? " /f" : "");
            //return RunHiddenProcess(RegAsmPath, command);
        }
        /// <summary>
        /// Регистрация сборки через RegAsm
        /// </summary>
        /// <param name="asm">Ссылка на файл сборки</param>
        /// <param name="force">Флаг принудительной установки</param>
        /// <returns>Возвращает результат операции</returns>
        public static RegistrationResult Install_ByRegAsm(FileInfo asm, bool codebase = false, bool tlb = false)
        {
            //return RegAsm.Install(asm);
            //string command = "/i " + asm.FullName + (force ? " /f" : "");
            //return RunHiddenProcess(RegAsmPath, command);

            var args = codebase ? "/codebase " : "";
            args += asm.FullName;
            args += tlb ? (" /tlb:" + Path.Combine(asm.DirectoryName, Path.GetFileNameWithoutExtension(asm.Name)) + ".tlb") : "";

            var toolsFolder = Path.Combine(Constants.RootFolder, Constants.Files.ToolsDirectoryName);
            //для 2 и 4 версии .NET RegAsm отличается.
            //2ой RegAsm не будет работать с четвёртыми сборками
            var regasmPath = GetRegAsmPath(asm);

            var exitCode = Core_2_0.Utilites.RunProcess(args, regasmPath);

            return exitCode == 0 ? RegistrationResult.Installed : RegistrationResult.Fail;
        }
        /// <summary>
        /// Регистрация сборки через RegAsm
        /// </summary>
        /// <param name="asm">Ссылка на файл сборки</param>
        /// <param name="force">Флаг принудительной операции</param>
        /// <returns>Возвращает результат операции</returns>
        public static RegistrationResult UnInstall_ByRegAsm(FileInfo asm)
        {
            //return RegAsm.UnInstall(asm);

            //string command = $"/u" 
            //    + (force ? "f" : "") 
            //    + " " 
            //    + Path.GetFileNameWithoutExtension(asm.FullName);
            ////string command = "/i " + asm.FullName + (force ? " /f" : "");
            //return RunHiddenProcess(RegAsmPath, command);

            var args = "/u " + asm.FullName;
            var toolsFolder = Path.Combine(Constants.RootFolder, Constants.Files.ToolsDirectoryName);
            //для 2 и 4 версии .NET RegAsm отличается.
            //2ой RegAsm не будет работать с четвёртыми сборками
            var regasmPath = GetRegAsmPath(asm);

            var exitCode = Core_2_0.Utilites.RunProcess(args, regasmPath);

            return exitCode == 0 ? RegistrationResult.Uninstalled : RegistrationResult.Fail;
        }
        /// <summary>
        /// Регистрация сборки через RegSvr32
        /// </summary>
        /// <param name="asm">Ссылка на файл сборки</param>
        /// <returns>Возвращает результат операции</returns>
        public static RegistrationResult Install_ByRegSvr32(FileInfo asm)
        {
            var args = "/s " + asm.FullName;

            return RunRegsvr32(asm, args, true);
        }

        /// <summary>
        /// Разрегистрация сборки через RegSvr32
        /// </summary>
        /// <param name="asm">Ссылка на файл сборки</param>
        /// <returns>Возвращает результат операции</returns>
        public static RegistrationResult UnInstall_ByRegSvr32(FileInfo asm)
        {
            var args = "/s /u " + asm.FullName;

            return RunRegsvr32(asm, args, false);
        }

        static int RunHiddenProcess(string FileName, string Arguments)
        {
            int exitCode;
            using (var process = new Process())
            {
                var startInfo = process.StartInfo;
                startInfo.FileName = FileName;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = Arguments;
                process.Start();
                process.WaitForExit();
                exitCode = process.ExitCode;
            }
            return exitCode;
        }

        public static RegistrationResult ResultFromExitcode(int exitCode)
        {
            return (exitCode == 0) ? RegistrationResult.Installed : RegistrationResult.Fail;
        }

        static string GetRegAsmPath(FileInfo asm)
        {
            var toolsFolder = Core_4_0.Utilites.GetDotNetFolder(asm);

            return Path.Combine(toolsFolder, "RegAsm.exe");
        }

        static string GetRegsvr32Path(FileInfo asm)
        {
            var isX64 = false;

            //получаем целевую платформу сборки
            //для x64 и x32 разные папки у Regsvr32.exe
            if (Core_2_0.Utilites.Is_CLR_Assembly(asm))
                isX64 = Core_2_0.Utilites.Is_X64(asm.FullName);
            else
                Core_2_0.Utilites.UnmanagedDllIs64Bit(asm.FullName);

            var windowsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
            var systemFolder = Path.Combine(windowsFolder, isX64 ? "SysWOW64" : "System32");

            //regsvr32 зависит от разрядности библиотеки
            return Path.Combine(systemFolder, "Regsvr32.exe");
        }

        static RegistrationResult RunRegsvr32(FileInfo asm, string args, bool install)
        {
            string regsvrPath = string.Empty;
            int exitCode = -1;
            try
            {
                //RegSvr различается для 32 64 разрядных библиотек
                regsvrPath = GetRegsvr32Path(asm);

                if(!File.Exists(regsvrPath))
                    throw new FileNotFoundException("Не найден файл Regsvr32.exe для регистрации сборки");

                exitCode = Core_2_0.Utilites.RunProcess(args, regsvrPath);
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при регистрации вызове Regsvr32", ex
                    , new LogParameter("Путь к файлу", asm == null ? "NULL" : asm.FullName)
                    , new LogParameter("Путь к RegAsm32", LogMessageBuilder.GetStringLogVal(regsvrPath)));

                return RegistrationResult.Fail;
            }

            //взято из https://stackoverflow.com/questions/22094309/regsvr32-exit-codes-documentation/22095500
            switch (exitCode)
            {
                case 0:
                    return install ? RegistrationResult.Installed : RegistrationResult.Uninstalled;
                case 1:
                    return RegistrationResult.WrongArgs;
                case 2:
                    return RegistrationResult.OleFailed;
                case 3:
                    return RegistrationResult.LoadFailed;
                case 4:
                    return RegistrationResult.WrongEntryPoint;
                case 5:
                    return RegistrationResult.Fail;
                default:
                    return RegistrationResult.UnKnown;
            }
        }

        /// <summary>
        /// Результат регистрации сборки
        /// </summary>
        [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
        public class Result
        {
            /// <summary>
            /// Результат регистрации в GAC
            /// </summary>
            public RegistrationResult GAC { get; private set; }
            /// <summary>
            /// Результат регистрации в RegAsm
            /// </summary>
            public RegistrationResult RegAsm { get; private set; }
            /// <summary>
            /// Результат регистрации в RegSvr32
            /// </summary>
            public RegistrationResult RegSvr32 { get; private set; }

            public Result(RegistrationResult gac = RegistrationResult.UnKnown
                , RegistrationResult regAsm = RegistrationResult.UnKnown
                , RegistrationResult regSvr32 = RegistrationResult.UnKnown)
            {
                GAC = gac;
                RegAsm = regAsm;
                RegSvr32 = regSvr32;
            }

            string DebugDisplay()
            {
                return "GAC: " + GAC.ToString() + " | RegAsm: " + RegAsm.ToString() + " | RegSvr: " + RegSvr32.ToString();
            }
        }
    }

}