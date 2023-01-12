using System.Windows.Controls;

namespace FolderSizeExplorer.Models
{
    public class FileDetailsModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Image FileIcon { get; set; }
        public string FileExtension { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public bool IsDirectory { get; set; }
        public bool IsHidden { get; set; }
        public bool IsSelected { get; set; }

        public string Type => IsDirectory ? "Folder" : "File";
    }
}