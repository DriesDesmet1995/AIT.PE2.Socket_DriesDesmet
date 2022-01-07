using AIT.PE2.Socket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.PE2.Socket.Core.Services
{
    public class DirectoryService
    {
        public List<FTFolder> Folders { get; private set; }
        public DirectoryService()
        {
            Folders = new List<FTFolder>();
        }
        public void AddFolder(FTFolder fTFolder)
        {
            bool found = false;
            foreach (FTFolder fol in Folders)
            {
                if (fol.FolderName == fTFolder.FolderName)
                {
                    found = true;
                    fol.FolderName = fTFolder.FolderName;
                    fol.FullPath = fTFolder.FullPath;
                    fol.FullParentPath = fTFolder.FullParentPath;
                    fol.FilesInFolder = fTFolder.FilesInFolder;
                    break;
                }
            }
            if (!found)
            {
                Folders.Add(fTFolder);
            }
        }
        public void RemoveFolder(string folderName)
        {
            foreach (FTFolder fTFolder in Folders)
            {
                if (fTFolder.FolderName == folderName)
                {
                    Folders.Remove(fTFolder);
                }
            }

        }

    }
}