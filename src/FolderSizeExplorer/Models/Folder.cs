using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace FolderSizeExplorer.Models
{
    /// <summary>
    /// Tree View Item
    /// </summary>
    internal class Folder
    {
        public StackPanel Header { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public ObservableCollection<Folder> Subfolders { get; set; } = new ObservableCollection<Folder>();
    }
}