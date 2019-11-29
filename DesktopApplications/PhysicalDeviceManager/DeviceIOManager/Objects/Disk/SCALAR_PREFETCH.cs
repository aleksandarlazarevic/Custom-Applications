using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SCALAR_PREFETCH
    {
        public short Minimum;
        public short Maximum;
        public short MaximumBlocks;
    }
}