using System;
using System.Reflection;

namespace Cube.Log
{
    /// <summary>
    /// Логируемые члены класса
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class LogMembersAttribute : Attribute
    {
        /// <summary>
        /// Логируемые члены класса
        /// </summary>
        public LogMembers LogMembers { get; private set; }
        /// <summary>
        /// Флаги поиска членов
        /// </summary>
        public BindingFlags BindingFlags { get; private set; }

        /// <summary>
        /// Логируемые члены класса
        /// </summary>
        /// <param name="logMembers">Флаги членов</param>
        /// <param name="bindingFlags">Флаги поиска</param>
        public LogMembersAttribute(LogMembers logMembers, BindingFlags bindingFlags)
        {
            LogMembers = logMembers;
            BindingFlags = bindingFlags;
        }

        string DebugDisplay() => $"Members: {(LogMembers.ToString())} -- Flags: {(BindingFlags.ToString())}";
    }
}
