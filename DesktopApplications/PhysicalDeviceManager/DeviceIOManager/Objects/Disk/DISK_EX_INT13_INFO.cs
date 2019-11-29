using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DISK_EX_INT13_INFO
    {
        public ushort ExBufferSize;
        public ushort ExFlags;
        public uint ExCylinders;
        public uint ExHeads;
        public uint ExSectorsPerTrack;
        public ulong ExSectorsPerDrive;
        public ushort ExSectorSize;
        public ushort ExReserved;
    }
}