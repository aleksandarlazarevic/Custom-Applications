using System.Runtime.InteropServices;
using DeviceIOManager.Objects.Enums;

namespace DeviceIOManager.Objects.Disk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PARTITION_INFORMATION_EX
    {
        [MarshalAs(UnmanagedType.U4)]
        public PartitionStyle PartitionStyle;
        public long StartingOffset;
        public long PartitionLength;
        public int PartitionNumber;
        public bool RewritePartition;
        public PARTITION_INFORMATION_UNION DriveLayoutInformaiton;
    }
}