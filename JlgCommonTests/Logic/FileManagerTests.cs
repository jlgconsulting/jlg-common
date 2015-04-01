using JlgCommon.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommonTests.Logic
{
    [TestClass]
    public class FileManagerTests
    {
        private FileManager _fileManager = new FileManager();
        private string authorFileName = "author";
        private string testDirectoryName = "testfileManager";

        [TestMethod]
        public void Write_Read_Delete()
        {
            _fileManager.Write(authorFileName, "Dan Misailescu");

            var authorFileContent = _fileManager.Read(authorFileName);
            Assert.AreEqual(authorFileContent, "Dan Misailescu");

            _fileManager.Delete(authorFileName);
            Assert.IsFalse(File.Exists(authorFileName));
        }
       

        [TestMethod]
        public void CreateDirectory_GetAllFilePathsFromDirectory_DeleteAllFilesFromDirectory()
        {
            _fileManager.CreateDirectory(testDirectoryName);
            Assert.IsTrue(Directory.Exists(testDirectoryName));
            _fileManager.CreateDirectory(testDirectoryName);
            _fileManager.CreateDirectory(testDirectoryName);

            for (int i = 1; i <= 10; i++)
            {
                _fileManager.Write(string.Format("{0}\\{1}{2}.txt", testDirectoryName,authorFileName, i), "Dan Misailescu");
            }

            for (int i = 1; i <= 7; i++)
            {
                _fileManager.Write(string.Format("{0}\\{1}{2}.png", testDirectoryName, authorFileName, i), "Dan Misailescu");
            }

            var childDirectoryName = string.Format("{0}\\Child{{0}}", testDirectoryName);
            _fileManager.CreateDirectory(childDirectoryName);
            for (int i = 1; i <= 5; i++)
            {
                _fileManager.Write(string.Format("{0}\\{1}{2}.mp3", childDirectoryName, authorFileName, i), "Dan Misailescu");
            }

            Assert.AreEqual(17, _fileManager.GetAllFilesFromDirectory(testDirectoryName).Count);
            Assert.AreEqual(22, _fileManager.GetAllFilesFromDirectory(testDirectoryName,null,true).Count);
            Assert.AreEqual(0, _fileManager.GetAllFilesFromDirectory(testDirectoryName, new List<string>{".jpg"}).Count);
            Assert.AreEqual(7, _fileManager.GetAllFilesFromDirectory(testDirectoryName, new List<string> { ".pNG"}).Count);

            _fileManager.DeleteAllFilesFromDirectory(testDirectoryName, new List<string> { ".mp3" });
            Assert.AreEqual(22, _fileManager.GetAllFilesFromDirectory(testDirectoryName, null, true).Count);
            _fileManager.DeleteAllFilesFromDirectory(testDirectoryName, new List<string> { ".mp3" }, true);
            Assert.AreEqual(17, _fileManager.GetAllFilesFromDirectory(testDirectoryName, null, true).Count);

            _fileManager.DeleteAllFilesFromDirectory(testDirectoryName, new List<string> { ".txt" });
            Assert.AreEqual(7, _fileManager.GetAllFilesFromDirectory(testDirectoryName, null, true).Count);

            _fileManager.DeleteAllFilesFromDirectory(testDirectoryName);
            Assert.AreEqual(0, _fileManager.GetAllFilesFromDirectory(testDirectoryName).Count);
            
            Directory.Delete(testDirectoryName,true);
        }        

    }
}
