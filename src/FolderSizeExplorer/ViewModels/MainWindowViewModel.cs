using System.Collections.ObjectModel;
using FolderSizeExplorer.Events;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.Services;
using FolderSizeExplorer.ViewModels.Base;

namespace FolderSizeExplorer.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Fields
        public ObservableCollection<Folder> Folders { get; }
        private string _currentDirectory = string.Empty;
        public string CurrentDirectory
        {
            get => _currentDirectory;
            set
            {
                if (value != _currentDirectory)
                {
                    _currentDirectory = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Methods

        private void OnDirectoryChanged(object sender, ValueChangedEvent<string> e)
        {
            if (e.NewValue != _currentDirectory)
            {
                _currentDirectory = e.NewValue;
                OnPropertyChanged(nameof(CurrentDirectory));
            }
        }

        #endregion
        
        public MainWindowViewModel()
        {
            Folder.SelectedPathChanged += OnDirectoryChanged;
            Folders = FolderService.Base();
        }
    }
}