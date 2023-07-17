using System;
using System.Threading;
using System.Windows.Forms;

namespace ActionManagement.HelperActions
{
    public class FileSystemActions
    {
        [STAThread]
        public static string OpenBrowseFileDialog()
        {
            string retVal = string.Empty;
            Thread STAThread = new Thread(
                delegate ()
                {
                    using (var dialog = new OpenFileDialog())
                    {
                        dialog.ShowDialog();
                        retVal = dialog.FileName;
                    }
                });
            STAThread.SetApartmentState(ApartmentState.STA);
            STAThread.Start();
            STAThread.Join();

            return retVal;
        }
    }
}