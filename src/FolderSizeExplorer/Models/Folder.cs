using System.Collections.ObjectModel;
using FolderSizeExplorer.Services;

namespace FolderSizeExplorer.Models
{
    /// <summary>
    /// Tree View Item
    /// </summary>
    internal class Folder
    {
        public Folder()
        {
            Subfolders = new ObservableCollection<Folder> {null};
        }
        
        public string Name { get; set; }
        public string Image { get; set; }
        public string Path { get; set; }
        
        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                // Exclude This PC
                if (_isExpanded && Path != "")
                {
                    Subfolders.Clear();
                    foreach (var subfolder in FolderService.GetSubfolders(Path)) 
                        Subfolders.Add(subfolder);
                }
                
            }
        }

        public ObservableCollection<Folder> Subfolders { get; set; }
    }
}