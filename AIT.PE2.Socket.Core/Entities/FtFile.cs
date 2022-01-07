using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.PE2.Socket.Core.Entities
{
    public class FTFile
    {
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public byte FileSize { get; set; }
        public DateTime CreationDate { get; set; }

        public FTFile(string fileName, string fullPath, byte fileSize, DateTime creationDate)
        {
            FileName = fileName;
            FullPath = fullPath;
            FileSize = fileSize;
            CreationDate = creationDate;
        }
    }
}
