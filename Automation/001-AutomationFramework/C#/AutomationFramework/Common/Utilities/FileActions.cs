using System.IO;

namespace Utilities
{
    public class FileActions
    {
        public static void CopyFiles(string sourcePath, string destinationPath, string filter = "*.*")
        {
            // Creating all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                }
                catch (Exception exception)
                {
                    throw new Exception(string.Format("Failed copying files from {0} to {1}: {2}", sourcePath, destinationPath, exception.Message));
                }
            }

            // Copying all files & replacing if any files with the same name are found
            foreach (string newPath in Directory.GetFiles(sourcePath, filter, SearchOption.AllDirectories))
            {
                string destination = newPath.Replace(sourcePath, destinationPath);
                File.Copy(newPath, destination, true);
            }

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
    }
}