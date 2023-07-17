using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace ConsoleRunner.Pdf
{
    public class PdfHelper
    {
        public static string ReadFromPdf(string pathToPdf)
        {
            string content = string.Empty;
            using (var pdf = PdfDocument.Open(pathToPdf))
            {
                foreach (var page in pdf.GetPages())
                {
                    // Either extract based on order in the underlying document with newlines and spaces.
                    var text = ContentOrderTextExtractor.GetText(page);

                    // Or based on grouping letters into words.
                    var otherText = string.Join(" ", page.GetWords());

                    // Or the raw text of the page's content stream.
                    content += page.Text;
                }
            }

            return content;
        }
    }
}
