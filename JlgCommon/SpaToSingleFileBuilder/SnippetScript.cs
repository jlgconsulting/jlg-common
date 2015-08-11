using System.Text;

namespace SpaToSingleFileBuilder
{
    public class SnippetScript : Snippet
    {
        public override string BuildExtendedText()
        {
            if (string.IsNullOrEmpty(Path))
            {
                return Text;
            }
            
           var extendedText = new StringBuilder();
            extendedText.Append("<script type=\"text/javascript\">");
            var fileContent = FileManager.Read(Path);
            fileContent = Minifier.MinifyJavaScript(fileContent);
            extendedText.Append(fileContent);
    
            extendedText.Append("</script>");
            return extendedText.ToString();
        }
    }
}
