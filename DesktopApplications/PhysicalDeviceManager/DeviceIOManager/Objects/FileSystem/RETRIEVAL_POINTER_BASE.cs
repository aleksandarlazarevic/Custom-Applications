using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.FileSystem
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RETRIEVAL_POINTER_BASE
    {
        public ulong FileAreaOffset;
    }
}
