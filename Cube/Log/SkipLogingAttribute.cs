using System;

namespace Cube.Log
{
    /// <summary>
    /// Флаг пропуска логирования для данного элемента
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class SkipLogingAttribute : Attribute { }
}
