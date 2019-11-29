using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.FileSystem
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NTFS_FILE_RECORD_INPUT_BUFFER
    {
        public ulong FileReferenceNumber;
    }
}