using Merlion.ECR.Update.Core;
using Merlion.ECR.Update.Core.DllImport;
using Merlion.ECR.Update.Core.Log;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using ProcArc = Merlion.ECR.Update.Core.ProcessorArchitecture;

namespace Merlion.ECR.Update.Core_2_0
{
    /// <summary>
    /// Общие вспомогательные методы
    /// </summary>
    public static class Utilites
    {
        internal static readonly ILoger m_loger = LogManager.GetDefaultLogSet("Utilites");

        /// <summary>
        /// Удаление символов \r и \n из строки
        /// </summary>
        /// <param name="str">Входная строка</param>
        /// <returns>Возвращает входную строку с удалёнными \r и \n</returns>
        public static string Clean_CRLF(string str)
        {
            return str.Replace("\r", "").Replace("\n", "");
        }

        /// <summary>
        /// Получение контрольной суммы файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Возвращает контрольную сумму файла</returns>
        /// <exception cref="T:ArgumentNullException">При передачи пустой ссылки на файл</exception>
        /// <exception cref="T:System.ArgumentException">
        ///   <paramref name="path" /> представляет собой строку нулевой длины, содержащую только пробелы или один или несколько недопустимых символов, как указано <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="path" /> имеет значение null; </exception>
        /// <exception cref="T:System.IO.PathTooLongException">Длина указанного пути, имени файла или обоих параметров превышает установленное в системе максимальное значение.Например, для платформ на основе Windows длина пути не должна превышать 248 знаков, а имена файлов не должны содержать более 260 знаков.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">Указанный путь недопустим (например, он соответствует неподключенному диску). </exception>
        /// <exception cref="T:System.IO.IOException">При открытии файла возникла ошибка ввода-вывода. </exception>
        /// <exception cref="T:System.UnauthorizedAccessException">Эта операция не поддерживается на текущей платформе.— или — <paramref name="path" /> определяет каталог.— или — Вызывающий оператор не имеет необходимого разрешения. </exception>
        /// <exception cref="T:System.IO.FileNotFoundException">Файл, заданный в <paramref name="path" />, не найден. </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///   <paramref name="path" /> имеет недопустимый формат. </exception>
        /// <exception cref="T:System.Security.SecurityException">Вызывающий оператор не имеет необходимого разрешения. </exception>
        public static string GetFileHash(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("Не задан путь к файлу при генерации контрольной суммы");

            using (var ms = new MemoryStream(File.ReadAllBytes(path)))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(ms);
                return BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower();
            }
        }

        /// <summary>
        /// Получение каталога исполняемого файла
        /// </summary>
        /// <returns>Возвращает каталог, из которого был вызван исполняемый файл, загрузивший сборку</returns>
        public static string GetExeDirectory()
        {
            try
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при попытке получить путь исполняемого файла. Возвращена пустая строка.", ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Проверка наличия каталога (и создание, если папки нет) (включая иерархию)
        /// </summary>
        /// <param name="path">Путь к папке / файлу</param>
        public static void CheckDirectory(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    m_loger.Log("На проверку наличия папки передан пустой путь", MessageType.Info);

                path = Path.GetDirectoryName(path);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при проверке / создании каталога", ex
                    , new LogParameter("Путь", path));
            }
        }

        /// <summary>
        /// Проверка разрядности сборки
        /// </summary>
        /// <param name="pathName">Сброка</param>
        /// <returns>Возвращает true для x64 сборок</returns>
        public static bool Is_X64(string pathName)
        {
            switch (GetMachineType(pathName))
            {
                case MachineType.IMAGE_FILE_MACHINE_IA64:
                case MachineType.IMAGE_FILE_MACHINE_AMD64:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Получение токена публичного ключа сборки
        /// </summary>
        /// <param name="pathName">Полный путь к файлу сборки</param>
        /// <exception cref="Exception">Если не найден PE header в библиотеке</exception>
        /// <returns>Возвращает токен публичного ключа или пустую строку</returns>
        public static MachineType GetMachineType(string pathName)
        {
            try
            {
                //взято из https://stackoverflow.com/questions/13079129/how-do-i-get-the-architecture-of-a-dll-in-c

                //see http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
                //offset to PE header is always at 0x3C
                //PE header starts with "PE\0\0" =  0x50 0x45 0x00 0x00
                //followed by 2-byte machine type field (see document above for enum)
                MachineType machineType = MachineType.IMAGE_FILE_MACHINE_UNKNOWN;
                using (FileStream fs = new FileStream(pathName, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        fs.Seek(0x3c, SeekOrigin.Begin);
                        Int32 peOffset = br.ReadInt32();
                        fs.Seek(peOffset, SeekOrigin.Begin);
                        UInt32 peHead = br.ReadUInt32();
                        if (peHead != 0x00004550) // "PE\0\0", little-endian
                            throw new Exception("Не найден PE header для определения архитектуры машины для библиотеки");
                        machineType = (MachineType)br.ReadUInt16();
                    }
                }
                return machineType;
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при попытке определить архитектуру сборки", ex
                    , new LogParameter("Путь к файлу сборки", pathName));

                return MachineType.IMAGE_FILE_MACHINE_UNKNOWN;
            }
        }

        /// <summary>
        /// Проверка разрядности сборки
        /// </summary>
        /// <param name="pathName">Сброка</param>
        /// <returns>Возвращает true для x64 сборок</returns>
        public static ProcArc GetProcArchitecture(string pathName)
        {
            try
            {
                switch (GetMachineType(pathName))
                {
                    case MachineType.IMAGE_FILE_MACHINE_IA64:
                        return ProcArc.IA64;
                    case MachineType.IMAGE_FILE_MACHINE_AMD64:
                        return ProcArc.Amd64;
                    case MachineType.IMAGE_FILE_MACHINE_ARM:
                        return ProcArc.Arm;
                    default:
                        //наврятли у нас будут какие-то специфические сборки... считаем остальные x86'ыми
                        return ProcArc.X86;
                }
            }
            catch { return ProcArc.None; }
        }

        /// <summary>
        /// Получение пути к исполняему файлу службы через реестр
        /// </summary>
        /// <param name="serviceName">Имя сервиса</param>
        /// <exception cref="T:ArgumentNullException">При передачи пустой ссылки на имя Службы</exception>
        /// <returns>Возвращает путь из реестра к исполняемому файлу службы</returns>
        public static string GetServiceExePath(string serviceName)
        {
            /*
            чтобы не хардкодить путь, при обновлении, необходимо получить путь к текущему каталогу
            поскольку обновление проводится службой, необходимо получить путь к exe'шнику службы,
            который, на данный момент, должен наодится в корне основного каталога

            ранее работал AppDomain.CurrentDomain.BaseDirectory, но он не всегда корректно отрабатывает
            подробности про получение пути https://stackoverflow.com/questions/2833959/how-to-find-windows-service-exe-path

            в результате, на мой взгляд, самый надежный способ - поиск через реестр.
            он даст сбой, если по какой-то причине, библиотека адаптера вызывается при отсутствии установленной службы на машине, 
            что, в рамках текущего использования, есть кейс не возможный
            */

            try
            {
                if (string.IsNullOrEmpty(serviceName))
                    throw new ArgumentNullException("Не задано имя службы при запросе пути к исполняемому файлу");

                using (var regKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\" + serviceName))
                {
                    string exePath = (string)regKey.GetValue("ImagePath");

                    exePath = exePath.Replace("\"", ""); //путь в реестре содержит кавычки

                    return exePath;
                }
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при попытке получения адреса исполняемого файла для службы из реестра", ex
                    , new LogParameter("Имя сервиса", LogMessageBuilder.GetStringLogVal(serviceName)));

                throw;
            }
        }

        /// <summary>
        /// Перезагрузка приложения через cmd
        /// </summary>
        /// <param name="timeWaitSec">Отложенный запуск через N секунд</param>
        public static void RestartApplication(TimeSpan timeWaitSec)
        {
            CmdExec("choice /C Y /N /D Y /T " + ((int)timeWaitSec.TotalSeconds).ToString() + " & START \"\" \"" + Assembly.GetExecutingAssembly().Location + "\"");
        }

        /// <summary>
        /// Выполнение комманды через коммандную строку
        /// </summary>
        /// <param name="cmdCommand">Комманда для выполнения</param>
        /// <param name="waitForExit">Флаг ожидания закрытия</param>
        public static void CmdExec(string cmdCommand, bool waitForExit = false)
        {
            /*
            взято из https://stackoverflow.com/questions/4773632/how-do-i-restart-a-wpf-application
            */

            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C " + cmdCommand;
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";

            using (var p = Process.Start(Info))
            {
                if (waitForExit)
                    p.WaitForExit();
            }
        }

        /// <summary>
        /// Проверка корректности пути
        /// </summary>
        /// <param name="path">Path to validate</param>
        /// <param name="RelativePath">Relative path</param>
        /// <param name="Extension">If want to check for File Path</param>
        /// <returns></returns>
        public static bool ValidatePathName(ref string path, string RelativePath = "", string Extension = "")
        {
            //из https://stackoverflow.com/questions/3137097/check-if-a-string-is-a-valid-windows-directory-folder-path
            // Check if it contains any Invalid Characters.
            if (path.IndexOfAny(Path.GetInvalidPathChars()) == -1)
            {
                try
                {
                    // If path is relative take %IGXLROOT% as the base directory
                    if (!Path.IsPathRooted(path))
                    {
                        if (string.IsNullOrEmpty(RelativePath))
                        {
                            // Exceptions handled by Path.GetFullPath
                            // ArgumentException path is a zero-length string, contains only white space, or contains one or more of the invalid characters defined in GetInvalidPathChars. -or- The system could not retrieve the absolute path.
                            // 
                            // SecurityException The caller does not have the required permissions.
                            // 
                            // ArgumentNullException path is null.
                            // 
                            // NotSupportedException path contains a colon (":") that is not part of a volume identifier (for example, "c:\"). 
                            // PathTooLongException The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.

                            // RelativePath is not passed so we would take the project path 
                            path = Path.GetFullPath(RelativePath);

                        }
                        else
                        {
                            // Make sure the path is relative to the RelativePath and not our project directory
                            path = Path.Combine(RelativePath, path);
                        }
                    }

                    // Exceptions from FileInfo Constructor:
                    //   System.ArgumentNullException:
                    //     fileName is null.
                    //
                    //   System.Security.SecurityException:
                    //     The caller does not have the required permission.
                    //
                    //   System.ArgumentException:
                    //     The file name is empty, contains only white spaces, or contains invalid characters.
                    //
                    //   System.IO.PathTooLongException:
                    //     The specified path, file name, or both exceed the system-defined maximum
                    //     length. For example, on Windows-based platforms, paths must be less than
                    //     248 characters, and file names must be less than 260 characters.
                    //
                    //   System.NotSupportedException:
                    //     fileName contains a colon (:) in the middle of the string.
                    FileInfo fileInfo = new FileInfo(path);

                    // Exceptions using FileInfo.Length:
                    //   System.IO.IOException:
                    //     System.IO.FileSystemInfo.Refresh() cannot update the state of the file or
                    //     directory.
                    //
                    //   System.IO.FileNotFoundException:
                    //     The file does not exist.-or- The Length property is called for a directory.
                    bool throwEx = fileInfo.Length == -1;

                    // Exceptions using FileInfo.IsReadOnly:
                    //   System.UnauthorizedAccessException:
                    //     Access to fileName is denied.
                    //     The file described by the current System.IO.FileInfo object is read-only.-or-
                    //     This operation is not supported on the current platform.-or- The caller does
                    //     not have the required permission.
                    throwEx = fileInfo.IsReadOnly;

                    if (!string.IsNullOrEmpty(Extension))
                    {
                        // Validate the Extension of the file.
                        if (Path.GetExtension(path).Equals(Extension, StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Trim the Library Path
                            path = path.Trim();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;

                    }
                }
                catch (ArgumentNullException)
                {
                    //   System.ArgumentNullException:
                    //     fileName is null.
                }
                catch (System.Security.SecurityException)
                {
                    //   System.Security.SecurityException:
                    //     The caller does not have the required permission.
                }
                catch (ArgumentException)
                {
                    //   System.ArgumentException:
                    //     The file name is empty, contains only white spaces, or contains invalid characters.
                }
                catch (UnauthorizedAccessException)
                {
                    //   System.UnauthorizedAccessException:
                    //     Access to fileName is denied.
                }
                catch (PathTooLongException)
                {
                    //   System.IO.PathTooLongException:
                    //     The specified path, file name, or both exceed the system-defined maximum
                    //     length. For example, on Windows-based platforms, paths must be less than
                    //     248 characters, and file names must be less than 260 characters.
                }
                catch (NotSupportedException)
                {
                    //   System.NotSupportedException:
                    //     fileName contains a colon (:) in the middle of the string.
                }
                catch (FileNotFoundException)
                {
                    // System.FileNotFoundException
                    //  The exception that is thrown when an attempt to access a file that does not
                    //  exist on disk fails.
                }
                catch (IOException)
                {
                    //   System.IO.IOException:
                    //     An I/O error occurred while opening the file.
                }
                catch (Exception)
                {
                    // Unknown Exception. Might be due to wrong case or nulll checks.
                }
            }
            else
            {
                // Path contains invalid characters
            }
            return false;
        }

        /// <summary>
        /// Получение токена открытого ключа у сборки
        /// </summary>
        /// <param name="pathName">Полный путь к файлу сборки</param>
        /// <returns>Возвращает токен публичного ключа или null</returns>
        public static string GetPublicKeyToken(string pathName)
        {
            const string defultValue = "";

            try
            {

                var bytes = AssemblyName.GetAssemblyName(pathName).GetPublicKeyToken();

                if (bytes == null || bytes.Length == 0)
                    return defultValue;

                var publicKeyToken = string.Empty;
                for (int i = 0; i < bytes.GetLength(0); i++)
                    publicKeyToken += string.Format("{0:x2}", bytes[i]);

                return publicKeyToken;
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при попытке получения публичного ключа сборки", ex
                    , new LogParameter("Сборка", pathName == null ? "NULL" : pathName));

                return defultValue;
            }
        }

        /// <summary>
        /// Проверка сборки на возможность загрузки в CLR
        /// </summary>
        /// <param name="asmFile">Имя файла сборки</param>
        /// <returns></returns>
        public static bool Is_CLR_Assembly(FileInfo asmFile)
        {
            return Is_CLR_Assembly(asmFile.FullName);
        }
        /// <summary>
        /// Проверка сборки на возможность загрузки в CLR
        /// </summary>
        /// <param name="asmFile">Имя файла сборки</param>
        /// <returns></returns>
        public static bool Is_CLR_Assembly(string asmFile)
        {
            //https://stackoverflow.com/questions/1366503/best-way-to-check-if-a-dll-file-is-a-clr-assembly-in-c-sharp

            uint peHeader;
            uint peHeaderSignature;
            ushort machine;
            ushort sections;
            uint timestamp;
            uint pSymbolTable;
            uint noOfSymbol;
            ushort optionalHeaderSize;
            ushort characteristics;
            ushort dataDictionaryStart;
            uint[] dataDictionaryRVA = new uint[16];
            uint[] dataDictionarySize = new uint[16];


            using (Stream fs = new FileStream(asmFile, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(fs);

                //PE Header starts @ 0x3C (60). Its a 4 byte header.
                fs.Position = 0x3C;

                peHeader = reader.ReadUInt32();

                //Moving to PE Header start location...
                fs.Position = peHeader;
                peHeaderSignature = reader.ReadUInt32();

                //We can also show all these value, but we will be       
                //limiting to the CLI header test.

                machine = reader.ReadUInt16();
                sections = reader.ReadUInt16();
                timestamp = reader.ReadUInt32();
                pSymbolTable = reader.ReadUInt32();
                noOfSymbol = reader.ReadUInt32();
                optionalHeaderSize = reader.ReadUInt16();
                characteristics = reader.ReadUInt16();

                /*
                    Now we are at the end of the PE Header and from here, the
                                PE Optional Headers starts...
                        To go directly to the datadictionary, we'll increase the      
                        stream’s current position to with 96 (0x60). 96 because,
                                28 for Standard fields
                                68 for NT-specific fields
                    From here DataDictionary starts...and its of total 128 bytes. DataDictionay has 16 directories in total,
                    doing simple maths 128/16 = 8.
                    So each directory is of 8 bytes.
                                In this 8 bytes, 4 bytes is of RVA and 4 bytes of Size.

                    btw, the 15th directory consist of CLR header! if its 0, its not a CLR file :)
             */
                dataDictionaryStart = Convert.ToUInt16(Convert.ToUInt16(fs.Position) + 0x60);
                fs.Position = dataDictionaryStart;
                for (int i = 0; i < 15; i++)
                {
                    dataDictionaryRVA[i] = reader.ReadUInt32();
                    dataDictionarySize[i] = reader.ReadUInt32();
                }
                if (dataDictionaryRVA[14] == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Проверка наличия у файла расширения *.dll
        /// </summary>
        /// <param name="asmFileName">Полное имя файла</param>
        /// <returns>Возвращает флаг наличия расширения *.dll у переданного файла</returns>
        public static bool IsAssemblyFile(string asmFileName)
        {
            if (!string.IsNullOrEmpty(asmFileName))
            {
                var fileExtension = Path.GetExtension(asmFileName);
                if (Constants.FileExtensions.DLL.Equals(fileExtension, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка запуска приложения от имени администратора
        /// </summary>
        /// <returns>Фозвращает флаг, что приложение была запущено от имени администратора</returns>
        public static bool IsAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// Проверка наличия файла
        /// </summary>
        /// <param name="pathName">Полный путь к файлу</param>
        /// <param name="throwEx">Проброс исключения, если файл не найден</param>
        /// <exception cref="FileNotFoundException">Если файл не найден и стоит флаг проброса исключения</exception>
        public static bool CheckFileExist(string pathName, bool throwEx = true)
        {
            if (!File.Exists(pathName))
            {
                m_loger.Log("Файл не найден при проверке", MessageType.Warning
                    , new LogParameter("Искомый файл", LogMessageBuilder.GetStringLogVal(pathName)));

                if (throwEx)
                    throw new FileNotFoundException("Файл " + pathName + " не найден");

                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Подсчёт количества процессов с указанным именем
        /// </summary>
        /// <param name="processName">Имя процесса</param>
        /// <param name="sessionID">ID сессии. -1 - поиск во всех сессиях</param>
        /// <returns>Возвращает количество процессов</returns>
        public static int GetProcessesCount(string processName, int sessionID = -1)
        {
            var count = 0;

            foreach (var proc in Process.GetProcessesByName(processName))
            {
                using (proc)
                {
                    if (sessionID >= 0)
                        //считаем только для текущего юзера
                        if (sessionID != proc.SessionId)
                            continue;

                    count++;
                }
            }

            return count;
        }

        public static int RunProcess(string args, string processName, ProcessWindowStyle windowStile = ProcessWindowStyle.Hidden)
        {
            /*
            параметры, используемые по умочланию
            */

            int exitCode = -1;
            using (var process = new Process())
            {
                var startInfo = process.StartInfo;
                startInfo.FileName = processName;
                startInfo.Arguments = args;
                startInfo.WindowStyle = windowStile;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                exitCode = process.ExitCode;
            }

            return exitCode;
        }

        /// <summary>
        /// Получение ID текущей сессии
        /// </summary>
        /// <returns>Возвращает ID сессии текущего процесса</returns>
        public static int GetCurentSessionID()
        {
            using (var curent = Process.GetCurrentProcess())
            {
                return curent.SessionId;
            }
        }

        /// <summary>
        /// Проверка на разрадность библиотеки
        /// </summary>
        /// <param name="dllPath">Полный путь к файлу библиотеки</param>
        /// <returns>Возвращает true, если библиотека признана 64-разрядной</returns>
        public static bool UnmanagedDllIs64Bit(string dllPath)
        {
            //взято из https://stackoverflow.com/questions/1001404/check-if-unmanaged-dll-is-32-bit-or-64-bit
            switch (GetDllMachineType(dllPath))
            {
                case MachineType.IMAGE_FILE_MACHINE_AMD64:
                case MachineType.IMAGE_FILE_MACHINE_IA64:
                    return true;
                case MachineType.IMAGE_FILE_MACHINE_I386:
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Получение целевой платформы, под которую была собрана библиотека
        /// </summary>
        /// <param name="dllPath">Полный путь к файлу библиотеки</param>
        /// <returns>Возвращает платформу, под которую собрана библиотека</returns>
        public static MachineType GetDllMachineType(string dllPath)
        {
            // See http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
            // Offset to PE header is always at 0x3C.
            // The PE header starts with "PE\0\0" =  0x50 0x45 0x00 0x00,
            // followed by a 2-byte machine type field (see the document above for the enum).
            //
            FileStream fs = new FileStream(dllPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            fs.Seek(0x3c, SeekOrigin.Begin);
            Int32 peOffset = br.ReadInt32();
            fs.Seek(peOffset, SeekOrigin.Begin);
            UInt32 peHead = br.ReadUInt32();

            if (peHead != 0x00004550) // "PE\0\0", little-endian
                throw new Exception("Can't find PE header");

            MachineType machineType = (MachineType)br.ReadUInt16();
            br.Close();
            fs.Close();
            return machineType;
        }

        /// <summary>
        /// Проверка на наличие аттрибута
        /// </summary>
        /// <typeparam name="TAttribute">Тип аттрибута</typeparam>
        /// <param name="member">Член объекта</param>
        /// <param name="inherite">Флаг просмотра родителей</param>
        /// <returns>Возвращает флаг наличия аттрибута указанного типа в коллекции аттрибутаов переданного члена</returns>
        public static bool HasAttribute<TAttribute>(MemberInfo member, bool inherite = false) where TAttribute : Attribute
        {
            if (member == null)
                return false;

            var attrs = member.GetCustomAttributes(inherite);

            if (attrs == null || attrs.Length == 0)
                return false;

            var attrType = typeof(TAttribute);

            for (int i = 0; i < attrs.Length; i++)
            {
                var a = attrs[i];

                if (attrType.Equals(a.GetType()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Получение битности ОС
        /// </summary>
        /// <returns>Возвращает битность ОС (x64 в )</returns>
        public static OSBitness GetOSBit()
        {
            //взято из https://ru.stackoverflow.com/questions/127037/%D0%9A%D0%B0%D0%BA-%D0%BE%D0%BF%D1%80%D0%B5%D0%B4%D0%B5%D0%BB%D0%B8%D1%82%D1%8C-%D1%80%D0%B0%D0%B7%D1%80%D1%8F%D0%B4%D0%BD%D0%BE%D1%81%D1%82%D1%8C-%D0%BE%D0%BF%D0%B5%D1%80%D0%B0%D1%86%D0%B8%D0%BE%D0%BD%D0%BD%D0%BE%D0%B9-%D1%81%D0%B8%D1%81%D1%82%D0%B5%D0%BC%D1%8B-%D1%81%D1%80%D0%B5%D0%B4%D1%81%D1%82%D0%B2%D0%B0%D0%BC%D0%B8-net
            using (var p = Process.GetCurrentProcess())
            {
                bool is64bit;
                kernel32.IsWow64Process(p.Handle, out is64bit);

                return is64bit ? OSBitness.x64 : OSBitness.x32;
            }
        }

        /// <summary>
        /// Принудительное удаление файла
        /// </summary>
        /// <param name="pathName">Полный путь к файлу</param>
        public static void ForceDeleteFile(string pathName)
        {
            if (!File.Exists(pathName))
                return;

            //файлы с флагом "Только для чтения" нельзя удалить обычным File.Delete
            //необходимо снять флаг перед удалением, чтобы избавиться от этой проблемы
            File.SetAttributes(pathName, FileAttributes.Normal);
            File.Delete(pathName);
        }

        /// <summary>
        /// Принудительное удаление папки
        /// </summary>
        /// <param name="path">Полный путь папки</param>
        public static void ForceDeleteFolder(string path)
        {
            if (!Directory.Exists(path))
                return;

            //решение взято отсюда https://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true

            //чистим все подпапки вручную
            //чтобы пройти через try / catch для каждой
            foreach (var file in Directory.GetFiles(path))
            {
                ForceDeleteFile(file);
            }

            //чистим все файлы вручную, чтобы не было проблем с readonly-файлами
            foreach (string directory in Directory.GetDirectories(path))
            {
                ForceDeleteFolder(directory);
            }

            try
            {
                //попытка удаления папки
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Thread.Sleep(50);

                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Thread.Sleep(50);

                Directory.Delete(path, true);
            }
        }
    }
}
