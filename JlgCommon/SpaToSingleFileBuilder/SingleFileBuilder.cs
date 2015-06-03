using JlgCommon.Logic;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpaToSingleFileBuilder
{
    public class SingleFileBuilder
    {
        private string _spaFilePath;
        private string _spaFileDirectoryPath;
        private FileManager _fileManager;

        public SingleFileBuilder()
        {
            _fileManager = new FileManager();
        }

        public void BuildSingleSpaFile(string spaFilePath, string newFilePath=null, bool includeStyles = true)
        {           

            _spaFilePath = spaFilePath;
            _spaFileDirectoryPath = Path.GetDirectoryName(_spaFilePath);
            var spaFileName = Path.GetFileNameWithoutExtension(_spaFilePath);
            var spaFileExtension = Path.GetExtension(_spaFilePath);

            string html = _fileManager.Read(_spaFilePath);
            html = RemoveUnwantedWhiteSpaces(html);
            var originalHtml = html;
            //the snippets will be unique because the file path will be unique for all of them 
            var styleSnippets = new List<SnippetStyle>();
            if (includeStyles)
            {
                styleSnippets = ExtractStyleSnippets(ref html, originalHtml);
            }
            
            var scriptSnippets = ExtractScriptSnippets(ref html, originalHtml);
                     
            var allSnippets = new List<Snippet>();
            allSnippets.AddRange(styleSnippets);
            allSnippets.AddRange(scriptSnippets);

            var htmlExtended = InsertExtendedSnippets(html, allSnippets);

            if (string.IsNullOrEmpty(newFilePath))
            {
                newFilePath =_spaFileDirectoryPath + "\\" + spaFileName + "_DmSingleFile" + spaFileExtension;
            }

            var htmlExtendedFileName = newFilePath;
            _fileManager.Write(htmlExtendedFileName, htmlExtended);

        }
        
        private string RemoveUnwantedWhiteSpaces(string html)
        {
            html = html.Replace("\n", string.Empty);
            html = html.Replace("\r", string.Empty);
            html = html.Replace("\t", string.Empty);

            while (html.IndexOf("  ") != -1)
            {
                html = html.Replace("  ", " ");
            }

            html = html.Replace("> <", "><");

            html = html.Replace("<link >", "<link>");
            html = html.Replace("< link>", "<link>");
            html = html.Replace("< link >", "<link>");
            html = html.Replace("</link >", "</link>");
            html = html.Replace("< /link >", "</link>");
            html = html.Replace("< / link >", "</link>");

            html = html.Replace("<script >", "<script>");
            html = html.Replace("< script>", "<script>");
            html = html.Replace("< script >", "<script>");
            html = html.Replace("</script >", "</script>");
            html = html.Replace("< /script >", "</script>");
            html = html.Replace("< / script >", "</script>");

            return html;
        }

        private List<SnippetStyle> ExtractStyleSnippets(ref string html, string originalHtml)
        {
            var styleSnippets = new List<SnippetStyle>();
            while (html.IndexOf("<link") != -1)
            {
                var indexStart = html.IndexOf("<link");
                var indexEnd = html.IndexOf(">", indexStart);


                if (indexEnd == -1)
                {
                    throw new Exception("link tag not closed!");
                }

                var indexEndTag = html.IndexOf("</link>", indexStart);
                if (indexEndTag == indexEnd + 1)
                {
                    indexEnd = indexEndTag + 7;
                }
                else
                {
                    indexEnd++;
                }

                var styleSnippetText = html.Substring(indexStart, indexEnd - indexStart);

                string path;
                if (styleSnippetText.Contains("image/png")
                    || (styleSnippetText.Contains("x-icon")
                        && styleSnippetText.Contains("base64"))
                    || styleSnippetText.Contains("image/x-icon"))
                {
                    path = string.Empty;
                }
                else
                {
                    path = GetSnippetPath(styleSnippetText, "href");
                }

                var styleSnippet = new SnippetStyle()
                {
                    IndexPosition = originalHtml.IndexOf(styleSnippetText),
                    Text = styleSnippetText,
                    Path = path
                };

                styleSnippets.Add(styleSnippet);

                html = html.Replace(styleSnippetText, string.Empty);
            }

            return styleSnippets;
        }

        private List<SnippetScript> ExtractScriptSnippets(ref string html, string originalHtml)
        {
            var scriptSnippets = new List<SnippetScript>();

            while (html.IndexOf("<script") != -1)
            {
                var indexStart = html.IndexOf("<script");
                var indexClosingFirstTag = html.IndexOf(">", indexStart);

                var indexEnd = indexClosingFirstTag;
                if (indexEnd == -1)
                {
                    throw new Exception("script tag not closed!");
                }

                var indexEndTag = html.IndexOf("</script>", indexStart);
                if (indexEndTag !=-1)
                {
                    indexEnd = indexEndTag + 9;
                }
                else
                {
                    indexEnd++;
                }

                var scriptSnippetText = html.Substring(indexStart, indexEnd - indexStart);

                string path;
                if (html.IndexOf("src", indexStart) < indexClosingFirstTag)
                {
                    path = GetSnippetPath(scriptSnippetText, "src");
                }
                else
                {
                    path = string.Empty;
                }

                var scriptSnippet = new SnippetScript()
                {
                    IndexPosition = originalHtml.IndexOf(scriptSnippetText),
                    Text = scriptSnippetText,
                    Path = path
                };

                scriptSnippets.Add(scriptSnippet);
                html = html.Replace(scriptSnippetText, string.Empty);
            }

            return scriptSnippets;
        }

        private string GetAttributeValueOfHtmlElement(string htmlElement, string attributeName)
        {
            if (!htmlElement.Contains(attributeName))
            {
                return string.Empty;
            }
            htmlElement = htmlElement.Replace(" ", string.Empty);
            htmlElement = htmlElement.Replace("'", "\"");

            var indexOfAttribute = htmlElement.IndexOf(attributeName);
            var indexOfOpenQuote = htmlElement.IndexOf("\"", indexOfAttribute);
            var indexOfCloseQuote = htmlElement.IndexOf("\"", indexOfOpenQuote + 1);

            if (indexOfOpenQuote == -1
                || indexOfCloseQuote == -1)
            {
                return string.Empty;
            }

            var attributeValue = htmlElement.Substring(indexOfOpenQuote + 1, indexOfCloseQuote - indexOfOpenQuote - 1);
            return attributeValue;
        }

        private string GetSnippetPath(string snippetHtml, string attributePath)
        {
            var scriptSnippetPath = GetAttributeValueOfHtmlElement(snippetHtml, attributePath);
            if (scriptSnippetPath.IndexOf("~") == 0)
            {
                scriptSnippetPath = scriptSnippetPath.Remove(0, 1);
            }
            if (scriptSnippetPath.IndexOf("/") == 0)
            {
                scriptSnippetPath = scriptSnippetPath.Remove(0, 1);
            }
            scriptSnippetPath = scriptSnippetPath.Replace("/", "\\");
            

            scriptSnippetPath = _spaFileDirectoryPath + "\\" + scriptSnippetPath;

            return scriptSnippetPath;
        }

        private string InsertExtendedSnippets(string html, List<Snippet> allSnippets)
        {
            for (int i = 0; i < allSnippets.Count; i++)
            {
                var snippet = allSnippets[i];

                html = html.Insert(snippet.IndexPosition, snippet.ExtendedText);
                var textLengthVariation = snippet.ExtendedText.Length - snippet.Text.Length;
                for (int j = i + 1; j < allSnippets.Count; j++)
                {
                    allSnippets[j].IndexPosition += textLengthVariation;
                }
            }

            return html;
        }
    }
}
