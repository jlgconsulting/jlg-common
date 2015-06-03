using JlgCommon.Logic;
using Microsoft.Ajax.Utilities;

namespace SpaToSingleFileBuilder
{
    public abstract class Snippet
    {
        protected Minifier Minifier { get; private set; }
        protected FileManager FileManager { get; private set; }

        public int IndexPosition { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string ExtendedText
        {
            get
            {
                return BuildExtendedText();
            }
        }
        public abstract string BuildExtendedText();      
        
        public Snippet()
        {
            Minifier = new Minifier();
            FileManager = new FileManager();
        }
    }
}
