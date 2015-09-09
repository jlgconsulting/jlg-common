using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JlgCommon.Logic
{
    public class FileManager
    {       
        public string Read(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                return sr.ReadToEnd();
            }
        }

        public void Write(string filePath, string content)
        {
            using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(content);
            }
        }

        public void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        
        public void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public void DeleteAllFilesFromDirectory(string directoryPath, List<string> extensions = null, bool includeAllChildDirectories = false)
        {
            var directoryRes = new DirectoryInfo(directoryPath);
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

            if (includeAllChildDirectories)
            {
                foreach (var dir in directoryRes.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

        }

        public List<FileInfo> GetAllFilesFromDirectory(string directoryPath, List<string> extensions = null, bool includeAllChildDirectories=false)
        {
            var filePaths = new List<FileInfo>();
            var directoryRes = new DirectoryInfo(directoryPath);
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

        public void CopyDirectory(string sourceDirectoryPath, string directoryPath, bool copySubDirs = true)
        {            
            var dir = new DirectoryInfo(sourceDirectoryPath);
            var dirs = dir.GetDirectories();
                       
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(directoryPath, file.Name);
                file.CopyTo(temppath, false);
            }
           
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(directoryPath, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    
    }
}
