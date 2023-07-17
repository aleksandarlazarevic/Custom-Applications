using System.Diagnostics;
using System.Management;

namespace Utilities
{
    public class WindowsProcessManager
    {
        public int CurrentProcessID()
        {
            return Process.GetCurrentProcess().Id;
        }

        public static void KillProcess(params string[] processes)
        {
            int numberOfRetrive = 32;

            foreach (string processName in processes)
            {
                Process[] activeProcesses = Process.GetProcesses().Where(pr => pr.ProcessName == processName).ToArray();

                foreach (Process? process in activeProcesses)
                {
                    try
                    {
                        process.Kill();
                    }
                    catch { }
                }
                while (Process.GetProcesses().Where(pr => pr.ProcessName == processName).Count() > 0)
                {
                    Thread.Sleep(2000);
                    if (--numberOfRetrive == 0)
                    {
                        throw new Exception(string.Format("Failed terminating process: {0}", processName));
                    }
                }
            }
        }

        public uint ParentProcessID(int processID)
        {
            string query = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", processID);
            ManagementObjectSearcher search = new ManagementObjectSearcher("root\\CIMV2", query);
            var results = search.Get().GetEnumerator();

            if (!results.MoveNext())
            {
                throw new Exception("Unable to retrieve id of the parent process.");
            }

            ManagementBaseObject queryObj = results.Current;
            return (uint)queryObj["ParentProcessId"];
        }
    }
}