using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.PE2.Socket.Core.Entities
{
    public class FTFolder
    {
        public string FolderName { get; set; }
        public string FullPath { get; set; }
        public string FullParentPath { get; set; }
        public ICollection<FTFile> FilesInFolder { get; set; }

        public FTFolder(string folderName, string fullPath, string fullParentPath, ICollection<FTFile> filesInFolder)
        {
            FolderName = folderName;
            FullPath = fullPath;
            FullParentPath = fullParentPath;
            FilesInFolder = filesInFolder;
        }
    }
}
