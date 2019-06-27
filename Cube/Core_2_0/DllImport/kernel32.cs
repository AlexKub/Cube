using System;
using System.Runtime.InteropServices;

namespace Merlion.ECR.Update.Core.DllImport
{
    /// <summary>
    /// Методы, импортируемые из Kernel32
    /// </summary>
    public static class kernel32
    {
        const string Kernel32_dll = "kernel32.dll";

        /// <summary>
        /// Проверка разрядности системы
        /// </summary>
        /// <param name="hProcess">Указатель на процесс</param>
        /// <param name="lpSystemInfo">Информация о системе</param>
        /// <returns>Возвращает флаг соответствия 64 разрядной ОС</returns>
        [DllImport(Kernel32_dll, SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

        [DllImport(Kernel32_dll, SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport(Kernel32_dll, SetLastError = true)]
        public static extern bool GetExitCodeProcess(IntPtr process, ref UInt32 exitCode);

        [DllImport(Kernel32_dll, SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr handle, UInt32 milliseconds);

        [DllImport(Kernel32_dll, SetLastError = true)]
        public static extern IntPtr GetStdHandle(IntPtr handle);
    }
}
