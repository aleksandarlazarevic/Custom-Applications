using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebActions
{
    public class Utilities
    {
        public static void WriteToLog(string valueToWrite, string fileName)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
            using (StreamWriter outputFile = new StreamWriter(filePath, true))
            {
                outputFile.WriteLine(valueToWrite);
            }
        }

    }
}
