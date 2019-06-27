using System;
using System.Runtime.InteropServices;

namespace Merlion.ECR.Update.Core.DllImport
{
    /// <summary>
    /// Внешние методы из 'advapi32.dll'
    /// </summary>
    public static class advapi32
    {
        const string DLL_Name = "advapi32.dll";

        [DllImport(DLL_Name, SetLastError = true)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);


        public const UInt32 Infinite = 0xffffffff;
        public const Int32 Startf_UseStdHandles = 0x00000100;
        public const Int32 StdOutputHandle = -11;
        public const Int32 StdErrorHandle = -12;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct StartupInfo
        {
            public int cb;
            public String reserved;
            public String desktop;
            public String title;
            public int x;
            public int y;
            public int xSize;
            public int ySize;
            public int xCountChars;
            public int yCountChars;
            public int fillAttribute;
            public int flags;
            public UInt16 showWindow;
            public UInt16 reserved2;
            public byte reserved3;
            public IntPtr stdInput;
            public IntPtr stdOutput;
            public IntPtr stdError;
        }

        public struct ProcessInformation
        {
            public IntPtr process;
            public IntPtr thread;
            public int processId;
            public int threadId;
        }

        [DllImport(DLL_Name, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CreateProcessWithLogonW(
            String userName,
            String domain,
            String password,
            UInt32 logonFlags,
            String applicationName,
            String commandLine,
            UInt32 creationFlags,
            UInt32 environment,
            String currentDirectory,
            ref StartupInfo startupInfo,
            out ProcessInformation processInformation);

        [DllImport(DLL_Name, EntryPoint = "LogonUserW", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool LogOnUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogOnType, int dwLogOnProvider, ref IntPtr phToken);

    }
}
