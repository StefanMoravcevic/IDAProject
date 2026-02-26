using NPOI.XWPF.UserModel;

namespace IDAProject.Web.Word
{
    public class OfficeMiddleware
    {

        public void ReplaceTextInDocument(XWPFDocument document, string oldText, string newText)
        {
            foreach (var paragraph in document.Paragraphs)
            {
                if (paragraph.Text.Contains(oldText))
                {
                    paragraph.ReplaceText(oldText, newText);
                }
            }
        }
    }
}