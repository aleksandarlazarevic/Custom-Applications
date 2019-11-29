using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.FileSystem
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FILE_ZERO_DATA_INFORMATION
    {
        public long FileOffset;
        public long BeyondFinalZero;
    }
}