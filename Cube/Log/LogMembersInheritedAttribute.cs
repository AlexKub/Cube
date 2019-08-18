using System;
using System.Reflection;

namespace Cube.Log
{
    /// <summary>
    /// Атрибут логирования членов текущего класса и его наследников
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true)]
    public class LogMembersInheritedAttribute : LogMembersAttribute
    {
        /// <summary>
        /// Атрибут логирования членов текущего класса и его наследников
        /// </summary>
        public LogMembersInheritedAttribute(LogMembers logMembers, BindingFlags bindingFlags) : base(logMembers, bindingFlags)
        {
        }
    }
}
