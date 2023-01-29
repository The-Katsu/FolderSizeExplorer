using FolderSizeExplorer.Models.Base;

namespace FolderSizeExplorer.Models
{
    /// <summary>
    /// Explorer item
    /// </summary>
    internal class SpecialFileDetails : File
    {
        public long TotalSpace { get; set; }
        public long FreeSpace { get; set; }
        public string FreeSpaceHumanRead { get; set; }
    }
}