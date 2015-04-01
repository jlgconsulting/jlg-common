using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommon.Logic
{
    public class FileManager
    {
        public string Read(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd();
            }
        }

        public void Write(string fileName, string content)
        {
            using (var sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                sw.Write(content);
            }
        }

        public void Delete(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
        
        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void DeleteAllFilesFromDirectory(string directoryName, List<string> extensions = null, bool includeAllChildDirectories = false)
        {
            var directoryRes = new System.IO.DirectoryInfo(directoryName);
            FileInfo[] files;
            if (includeAllChildDirectories)
            {
                files = directoryRes.GetFiles("*.*", SearchOption.AllDirectories);
            }
            else
            {
                files = directoryRes.GetFiles("*.*");
            }

            foreach (var file in files)
            {
                if (extensions != null && extensions.Count > 0)
                {
                    foreach (var extension in extensions)
	                {
                        if (file.Extension.ToLower() == extension.ToLower())
                        {
                            file.Delete();
                            break;
                        }
	                }
                    
                }
                else
                {
                    file.Delete();
                }
            }
        }

        public List<FileInfo> GetAllFilesFromDirectory(string directoryName, List<string> extensions = null, bool includeAllChildDirectories=false)
        {
            var filePaths = new List<FileInfo>();
            var directoryRes = new System.IO.DirectoryInfo(directoryName);
            FileInfo[] files;
            if (includeAllChildDirectories)
            {
                files = directoryRes.GetFiles("*.*", SearchOption.AllDirectories);
            }
            else
            {
                files = directoryRes.GetFiles("*.*");
            }

            foreach (var file in files)
            {
                if (extensions != null && extensions.Count > 0)
                {
                    foreach (var extension in extensions)
	                {
                        if (file.Extension.ToLower() == extension.ToLower())
                        {
                            filePaths.Add(file);
                            break;
                        }
	                }
                    
                }
                else
                {
                    filePaths.Add(file);
                }
                
            }
            return filePaths;
        }        
    }
}
