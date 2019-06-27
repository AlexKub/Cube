namespace RTCManifestGenerator
{
    /// <summary>
    /// Константы прилоежния
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Имя RTC манифеста по умолчанию из Core_2_0
        /// </summary>
        public static string RTCManifestFileName { get { return Merlion.ECR.Update.Core.Manifest.RTCManifest.DefaultFileName; } }

        /// <summary>
        /// Имя файла с инструкцией пользователя
        /// </summary>
        public const string InstructionFileName = "Инструкция_пользователя.docx";
    }
}
