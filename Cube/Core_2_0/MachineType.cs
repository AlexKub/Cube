namespace Merlion.ECR.Update.Core
{
    /// <summary>
    /// Флаги Целевых платформ для файлов Windows
    /// </summary>
    public enum MachineType
    {
        //взято из https://stackoverflow.com/questions/1001404/check-if-unmanaged-dll-is-32-bit-or-64-bit
        //доки о текущем перечислении: https://docs.microsoft.com/en-us/windows/desktop/sysinfo/image-file-machine-constants

        /// <summary>
        /// Не определено
        /// </summary>
        IMAGE_FILE_MACHINE_UNKNOWN = 0x0,
        /// <summary>
        /// TAM33BD
        /// </summary>
        IMAGE_FILE_MACHINE_AM33 = 0x1d3,
        /// <summary>
        /// AMD64 (K8)
        /// </summary>
        IMAGE_FILE_MACHINE_AMD64 = 0x8664,
        /// <summary>
        /// ARM Little-Endian
        /// </summary>
        IMAGE_FILE_MACHINE_ARM = 0x1c0,
        /// <summary>
        /// EFI Byte Code
        /// </summary>
        IMAGE_FILE_MACHINE_EBC = 0xebc,
        /// <summary>
        /// Intel 386
        /// </summary>
        IMAGE_FILE_MACHINE_I386 = 0x14c,
        /// <summary>
        /// Intel 64
        /// </summary>
        IMAGE_FILE_MACHINE_IA64 = 0x200,
        /// <summary>
        /// M32R little-endian
        /// </summary>
        IMAGE_FILE_MACHINE_M32R = 0x9041,
        /// <summary>
        /// MIPS
        /// </summary>
        IMAGE_FILE_MACHINE_MIPS16 = 0x266,
        /// <summary>
        /// MIPS
        /// </summary>
        IMAGE_FILE_MACHINE_MIPSFPU = 0x366,
        /// <summary>
        /// MIPS
        /// </summary>
        IMAGE_FILE_MACHINE_MIPSFPU16 = 0x466,
        /// <summary>
        /// IBM PowerPC Little-Endian
        /// </summary>
        IMAGE_FILE_MACHINE_POWERPC = 0x1f0,
        /// <summary>
        /// POWERPCFP
        /// </summary>
        IMAGE_FILE_MACHINE_POWERPCFP = 0x1f1,
        /// <summary>
        /// MIPS little-endian
        /// </summary>
        IMAGE_FILE_MACHINE_R4000 = 0x166,
        /// <summary>
        /// SH3 little-endian
        /// </summary>
        IMAGE_FILE_MACHINE_SH3 = 0x1a2,
        /// <summary>
        /// SH3DSP
        /// </summary>
        IMAGE_FILE_MACHINE_SH3DSP = 0x1a3,
        /// <summary>
        /// SH4 little-endian
        /// </summary>
        IMAGE_FILE_MACHINE_SH4 = 0x1a6,
        /// <summary>
        /// SH5
        /// </summary>
        IMAGE_FILE_MACHINE_SH5 = 0x1a8,
        /// <summary>
        /// ARM Thumb/Thumb-2 Little-Endian
        /// </summary>
        IMAGE_FILE_MACHINE_THUMB = 0x1c2,
        /// <summary>
        /// MIPS little-endian WCE v2
        /// </summary>
        IMAGE_FILE_MACHINE_WCEMIPSV2 = 0x169
    }
}
