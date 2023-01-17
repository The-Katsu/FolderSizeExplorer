using System.Windows.Controls;

namespace FolderSizeExplorer.Models.Base
{
    internal abstract class File
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Image Icon { get; set; }
        public string IconSource { get; set; }
        public virtual bool IsSelected { get; set; }
    }
}