using UglyToad.PdfPig;

namespace Utilities
{
    public class PdfManager
    {
        public static string ReadFromPdf(string pathToPdf)
        {
            string content = string.Empty;
            using (var pdf = PdfDocument.Open(pathToPdf))
            {
                foreach (var page in pdf.GetPages())
                {
                    content += page.Text;
                }
            }

            return content;
        }
    }
}
