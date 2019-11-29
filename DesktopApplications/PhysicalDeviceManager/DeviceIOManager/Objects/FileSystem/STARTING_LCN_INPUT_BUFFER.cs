using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.FileSystem
{
    [StructLayout(LayoutKind.Sequential)]
    public struct STARTING_LCN_INPUT_BUFFER
    {
        public ulong StartingLcn;
    }
}