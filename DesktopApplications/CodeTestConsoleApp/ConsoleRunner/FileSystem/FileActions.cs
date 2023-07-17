using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleRunner.FileSystem
{
    public class FileActions
    {
        public static void CopyFiles(string sourcePath, string destinationPath, string filter = "*.*")
        {
            // Creating all of the directories
            foreach (string dirPath in Directory.GetDirectories(
                sourcePath,
                "*",
                SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                }
                catch (Exception exception)
                {
                    throw new Exception(string.Format("Failed copying files from {0} to {1}: {2}",
                                                      sourcePath,
                                                      destinationPath,
                                                      exception.Message));
                }
            }

            // Copying all files & replacing if any files with the same name are found
            foreach (string newPath in Directory.GetFiles(
                sourcePath,
                filter,
                SearchOption.AllDirectories))
            {
                string destination = newPath.Replace(sourcePath, destinationPath);
                File.Copy(newPath, destination, true);
            }

            // Remove read only flag
            RemoveReadOnlyFlag(destinationPath);
        }

        private static void RemoveReadOnlyFlag(string rootDirectory)
        {
            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(rootDirectory);

            if (rootDirectoryInfo != null)
            {
                rootDirectoryInfo.Attributes = FileAttributes.Normal;
                foreach (FileInfo fileInfo in rootDirectoryInfo.GetFiles())
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                }

                foreach (DirectoryInfo directoryInfo in rootDirectoryInfo.GetDirectories())
                {
                    RemoveReadOnlyFlag(directoryInfo.FullName);
                }
            }
        }

        public static string OpenBrowseFolderDialog()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.ShowDialog();
                return dialog.SelectedPath;
            }
        }

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
