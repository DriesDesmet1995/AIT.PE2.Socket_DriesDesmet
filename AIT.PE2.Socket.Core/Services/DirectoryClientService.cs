using AIT.PE2.Socket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIT.PE2.Socket.Core.Services
{
    public class DirectoryClientService
    {
        public List<FTFolder> Folders { get; set; }
        public DirectoryClientService()
        {
            Folders = new List<FTFolder>();
        }
    }
}
