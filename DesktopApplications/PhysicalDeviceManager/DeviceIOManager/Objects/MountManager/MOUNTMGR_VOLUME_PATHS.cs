using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.MountManager
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUNTMGR_VOLUME_PATHS
    {
        public ushort MultiSzLength;
        public char[] MultiSz;
    }
}