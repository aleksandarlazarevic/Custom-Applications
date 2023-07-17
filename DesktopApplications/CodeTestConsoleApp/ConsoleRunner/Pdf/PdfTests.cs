using ConsoleRunner.FileSystem;

namespace ConsoleRunner.Pdf
{
    public class PdfTests
    {
        public static void TestConversions()
        {
            string pdfFile = FileActions.OpenBrowseFileDialog();
            string content = PdfHelper.ReadFromPdf(pdfFile);

        }
    }
}
