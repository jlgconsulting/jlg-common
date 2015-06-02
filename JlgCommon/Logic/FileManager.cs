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

            if (includeAllChildDirectories)
            {
                foreach (var dir in directoryRes.GetDirectories())
                {
                    dir.Delete(true);
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
        
        public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = true)
        {            
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();
                       
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }
           
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    
    }
}
