using System.Windows.Controls;
using FolderSizeExplorer.Models.Base;

namespace FolderSizeExplorer.Models
{
    /// <summary>
    /// Explorer Item
    /// </summary>
    internal class FileDetails : File
    {
        public string FileExtension { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
        public bool IsHidden { get; set; }
        private bool _isDirectory;
        public string Type => _isDirectory ? "Folder" : "File";
    }
}