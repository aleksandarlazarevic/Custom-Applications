using System.Runtime.InteropServices;

namespace DeviceIOManager.Objects.Usn
{
    [StructLayout(LayoutKind.Sequential)]
    public struct READ_USN_JOURNAL_DATA_V0
    {
        public USN StartUsn;
        public UsnJournalReasonMask ReasonMask;
        public int ReturnOnlyOnClose;
        public long Timeout;
        public long BytesToWaitFor;
        public long UsnJournalId;
    }
}