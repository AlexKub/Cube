using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merlion.ECR.Update.Core.Environment
{
    /// <summary>
    /// Операции с коммандной строкой
    /// </summary>
    public static class Cmd
    {
        public static int Reg_Regsvr32(FileInfo assembly)
        {
            var args = assembly.FullName + " /s";

            return Run(args, "regsvr32");
        }

        public static int UnReg_Regsvr32(FileInfo assembly)
        {
            var args = "/s /u" + assembly.FullName;

            return Run(args, "regsvr32");
        }

        //public static int Reg_RegAsm(FileInfo assembly, bool codebase = true, bool tlb = true)
        //{
        //
        //}
        //
        //public static int UnReg_RegAsm(FileInfo assembly, bool codebase = true, bool tlb = true)
        //{
        //
        //}

        public static int Run(string args, string processName = null, ProcessWindowStyle windowStile = ProcessWindowStyle.Hidden, bool throwEx = false)
        {
            /*
            параметры, используемые по умочланию
            */

            int exitCode = -1;
            using (var process = new Process())
            {
                var startInfo = process.StartInfo;
                startInfo.FileName = string.IsNullOrEmpty(processName) ?  "cmd.exe" : processName;
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
    }
}
