using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FolderSizeExplorer.Events;
using FolderSizeExplorer.Infrastructure.Commands;
using FolderSizeExplorer.Infrastructure.Events;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.Services;
using FolderSizeExplorer.ViewModels.Base;

namespace FolderSizeExplorer.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Fields

        private LinkedList<string> _history;
        private LinkedListNode<string> _currentHistoryNode;
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
                    IsStartupPage = _currentDirectory == string.Empty;
                    
                    _history.AddAfter(_history.Last, value);
                    
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
            _currentHistoryNode = _history.Last;
        }

        private void OnHistoryChanged(object sender, HistoryChangeEvent<string> e)
        {
            if (e.Previous)
            {
                for (var node = _history.First; node != null; node = node.Next)
                {
                    if (node == _currentHistoryNode)
                    {
                        _currentHistoryNode = node.Previous;
                        CurrentDirectory = _currentHistoryNode?.Value;
                        break;
                    }
                }
            }

            if (e.Next)
            {
                for (var node = _history.First; node != null; node = node.Next)
                {
                    if (node == _currentHistoryNode)
                    {
                        _currentHistoryNode = node.Next;
                        CurrentDirectory = _currentHistoryNode?.Value;
                        break;
                    }
                }
            }

            if (e.Up)
            {
                CurrentDirectory = new DirectoryInfo(_currentDirectory).Parent?.FullName;
                _currentHistoryNode = _history.Last;
            }
        }
        
        #endregion

        #region Commands

        #endregion
        
        public MainWindowViewModel()
        {
            Folder.SelectedPathChanged += OnDirectoryChanged;
            ExplorerDoubleClickCommand.SelectedPathChanged += OnDirectoryChanged;
            ChangeDirectoryCommand.HistoryChangedEvent += OnHistoryChanged;
            Folders = TreeViewService.GetBase();
            SpecialFileDetailsService.GetBase(SpecialFileDetailsCollection);
            _history = new LinkedList<string>(new [] {_currentDirectory});
            _currentHistoryNode = _history.First;
        }
    }
}