using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Interop;

namespace FolderSizeExplorer.Models
{
    /// <summary>
    /// Tree View Item
    /// </summary>
    internal class Folder
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Path { get; set; }

        public ObservableCollection<Folder> Subfolders { get; set; } = new ObservableCollection<Folder>();
    }
}