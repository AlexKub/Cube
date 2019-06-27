using System;

namespace Merlion.ECR.Update.Core.Manifest
{
    [Serializable]
    public class ControlAddInFileItem : ManifestFileItem, IControlAddinFileItem
    {
        /// <summary>
        /// Имя контрола
        /// </summary>
        public new string AddinName { get; set; }
    }
}
