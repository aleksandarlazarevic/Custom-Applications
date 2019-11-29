using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DRIVE_LAYOUT_INFORMATION_MBR
    {
        public ulong Signature { get; set; }
    }
}