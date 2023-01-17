using System;
using System.Collections.ObjectModel;
using FolderSizeExplorer.Events;
using FolderSizeExplorer.Models.Base;
using FolderSizeExplorer.Services;
using FolderSizeExplorer.Services.Helpers;

namespace FolderSizeExplorer.Models
{
    /// <summary>
    /// Tree View Item
    /// </summary>
    internal class Folder : File
    {
        public Folder()
        {
            Subfolders = new ObservableCollection<Folder> {null};
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                switch (_isExpanded)
                {
                    // This PC case
                    case true when Path == "":
                    {
                        Subfolders.Clear();
                        foreach (var subfolder in TreeViewService.GetThisPcSubfolders()) 
                            Subfolders.Add(subfolder);
                        break;
                    }
                    case true when Path != "":
                    {
                        Subfolders.Clear();
                        foreach (var subfolder in FolderService.GetSubfolders(Path)) 
                            Subfolders.Add(subfolder);
                        break;
                    }
                }
            }
        }
        
        private bool _isSelected;
        public override bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnSelectedPathChanged(Path);
            }
        }
        public ObservableCollection<Folder> Subfolders { get; set; }
        public static event EventHandler<ValueChangedEvent<string>> SelectedPathChanged; 
        private void OnSelectedPathChanged(string path) => SelectedPathChanged?.Invoke(this, new ValueChangedEvent<string>(path));
    }
}