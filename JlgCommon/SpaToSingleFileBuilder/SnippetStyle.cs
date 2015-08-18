using System.Text;

namespace JlgCommon.SpaToSingleFileBuilder
{
    public class SnippetStyle : Snippet
    {
       public override string BuildExtendedText()
        {
            if (string.IsNullOrEmpty(Path))
            {
                return Text;
            }
           
            var extendedText = new StringBuilder();
            extendedText.Append("<style>");
            var fileContent = FileManager.Read(Path);
            fileContent = Minifier.MinifyStyleSheet(fileContent);
            extendedText.Append(fileContent);
            
            extendedText.Append("</style>");
            return extendedText.ToString();
        }
    }
}
