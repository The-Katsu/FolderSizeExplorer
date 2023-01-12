using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderSizeExplorer.Test.Console.IO
{
    public class Drvs
    {
        public Drvs()
        {

        }

        public List<DriveInfo> Get()
        {
            return DriveInfo.GetDrives().ToList();
        }

        public long CalculateSize(DriveInfo drive)
        {
            return drive.TotalSize - drive.TotalFreeSpace;
        }
    }
}