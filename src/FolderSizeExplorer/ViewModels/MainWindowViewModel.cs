using System.Collections.ObjectModel;
using FolderSizeExplorer.Events;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.Services;
using FolderSizeExplorer.ViewModels.Base;

namespace FolderSizeExplorer.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Folder> Folders { get; }
        private string _currentDirectory = string.Empty;
        private void OnDirectoryChanged(object sender, ValueChangedEvent<string> e)
        {
            if (e.NewValue != _currentDirectory)
            {
                _currentDirectory = e.NewValue;
                OnPropertyChanged(nameof(CurrentDirectory));
            }
        }
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

        public MainWindowViewModel()
        {
            Folder.SelectedPathChanged += OnDirectoryChanged;
            Folders = FolderService.Base();
        }
    }
}