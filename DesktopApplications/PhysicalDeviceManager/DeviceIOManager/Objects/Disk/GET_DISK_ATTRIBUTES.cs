using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GET_DISK_ATTRIBUTES
    {
        public int Version;
        public int Reserved1;
        public long Attributes;
    }
}