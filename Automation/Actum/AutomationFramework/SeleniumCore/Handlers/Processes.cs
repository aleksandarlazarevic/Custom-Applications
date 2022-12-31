using System.Diagnostics;
using System.Management;

namespace SeleniumCore.Handlers
{
    public class Processes
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

                foreach (var p in activeProcesses)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
                while (Process.GetProcesses().Where(pr => pr.ProcessName == processName).Count() > 0)
                {
                    Thread.Sleep(2000);
                    if (--numberOfRetrive == 0)
                    {
                        throw new Exception($"Unable to kill {processName} process.");
                    }
                }
            }
        }

        public uint ParentProcessID(int processID)
        {
            var query = String.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", processID);
            var search = new ManagementObjectSearcher("root\\CIMV2", query);
            var results = search.Get().GetEnumerator();

            if (!results.MoveNext())
            {
                throw new Exception("Unable to retrieve id of the parent process.");
            }

            var queryObj = results.Current;
            return (uint)queryObj["ParentProcessId"];
        }
    }
}