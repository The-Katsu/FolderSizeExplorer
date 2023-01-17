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

        public ObservableCollection<FileDetails> FileDetailsCollection { get; set; } =
            new ObservableCollection<FileDetails>();
        public ObservableCollection<SpecialFileDetails> SpecialFileDetailsCollection { get; set; } = 
            new ObservableCollection<SpecialFileDetails>();
        
        private string _currentDirectory = string.Empty;
        public string CurrentDirectory
        {
            get => _currentDirectory;
            set
            {
                if (value != _currentDirectory)
                {
                    _currentDirectory = value;
                    if (_currentDirectory == string.Empty) 
                        SpecialFileDetailsService.GetBase(SpecialFileDetailsCollection);
                    else
                        FileDetailsService.GetFiles(FileDetailsCollection, _currentDirectory);
                    OnPropertyChanged();
                }
            }
        }

        private bool _isStartupPage = true;
        public bool IsStartupPage
        {
            get => _isStartupPage;
            set
            {
                if (value != _isStartupPage)
                {
                    _isStartupPage = value;
                    OnPropertyChanged();
                }   
            }
        }

        #endregion

        #region Methods

        private void OnDirectoryChanged(object sender, ValueChangedEvent<string> e)
        {
            CurrentDirectory = e.NewValue;
            IsStartupPage = _currentDirectory == string.Empty;
        }

        #endregion
        
        public MainWindowViewModel()
        {
            Folder.SelectedPathChanged += OnDirectoryChanged;
            Folders = TreeViewService.GetBase();
            SpecialFileDetailsService.GetBase(SpecialFileDetailsCollection);
        }
    }
}