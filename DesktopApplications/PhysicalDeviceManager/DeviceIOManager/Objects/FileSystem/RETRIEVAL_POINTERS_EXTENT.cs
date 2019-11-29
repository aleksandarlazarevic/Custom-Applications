using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.FileSystem
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RETRIEVAL_POINTERS_EXTENT
    {
        public ulong NextVcn;
        public ulong Lcn;
    }
}