using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FolderSizeExplorer.Infrastructure.Commands;
using FolderSizeExplorer.Infrastructure.Events;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.Services.UI;
using FolderSizeExplorer.ViewModels.Base;
using File = FolderSizeExplorer.Models.Base.File;

namespace FolderSizeExplorer.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Fields

        #region History

        private LinkedList<string> _history;
        private LinkedListNode<string> _currentHistoryNode;

        #endregion

        #region Collections

        public ObservableCollection<Folder> Folders { get; } = 
            new ObservableCollection<Folder>();
        public ObservableCollection<FileDetails> FileDetailsCollection { get; set; } =
            new ObservableCollection<FileDetails>();
        public ObservableCollection<SpecialFileDetails> SpecialFileDetailsCollection { get; set; } = 
            new ObservableCollection<SpecialFileDetails>();

        #endregion
        
        #region Properties

        private List<File> _currentFiles;
        public List<File> CurrentFiles
        {
            get => _currentFiles;
            private set
            {
                _currentFiles = value;
                OnPropertyChanged();
            }
        }
        
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
                    UpdateExplorer();
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
        
        #endregion

        #region Methods

        #region Private Helpers

        private void UpdateExplorer()
        {
            if (_currentDirectory == string.Empty)
            {
                SpecialFileDetailsService.GetBase(SpecialFileDetailsCollection);
                CurrentFiles = SpecialFileDetailsCollection.Cast<File>().ToList();
            }
            else
            {
                FileDetailsService.GetFiles(FileDetailsCollection, _currentDirectory);
                CurrentFiles = FileDetailsCollection.Cast<File>().ToList();
            } 
        }
        private void HistoryMove(bool prev, bool next, bool up)
        {
            if (prev || next)
            {
                for (var node = _history.First; node != null; node = node.Next)
                {
                    if (node == _currentHistoryNode)
                    {
                        if (prev && node.Previous != null)
                        {
                            _currentHistoryNode = node.Previous;
                            CurrentDirectory = _currentHistoryNode.Value;
                            break;
                        }

                        if (next && node.Next != null)
                        {
                            _currentHistoryNode = node.Next;
                            CurrentDirectory = _currentHistoryNode.Value;
                            break;
                        }
                    }
                }
            }

            if (up)
            {
                if (_currentDirectory != string.Empty)
                {
                    var directoryInfo = new DirectoryInfo(_currentDirectory).Parent;
                    if (directoryInfo != null)
                    {
                        CurrentDirectory = directoryInfo.FullName;
                        _currentHistoryNode = _history.Last;
                    }
                }
            }
        }

        #endregion

        #region EventsCathcer

        private void OnExplorerUpdate(object sender, UpdateExplorerEvent e) => UpdateExplorer();
        
        private void OnDirectoryChanged(object sender, ValueChangedEvent<string> e)
        {
            CurrentDirectory = e.NewValue;
            _currentHistoryNode = _history.Last;
        }

        private void OnHistoryChanged(object sender, HistoryChangeEvent e)
        {
            if (e.Previous) HistoryMove(true, false, false);
            if (e.Next) HistoryMove(false, true, false);
            if (e.Up) HistoryMove(false, false, true);
        }

        #endregion
        
        #endregion

        public MainWindowViewModel()
        {
            Folder.SelectedPathChanged += OnDirectoryChanged;
            ExplorerDoubleClickCommand.SelectedPathChanged += OnDirectoryChanged;
            ChangeDirectoryCommand.HistoryChangedEvent += OnHistoryChanged;
            UpdateExplorerCommand.UpdateExplorerEvent += OnExplorerUpdate;
            TreeViewService.GetBase(Folders);
            SpecialFileDetailsService.GetBase(SpecialFileDetailsCollection);
            CurrentFiles = SpecialFileDetailsCollection.Cast<File>().ToList();
            _history = new LinkedList<string>(new [] {_currentDirectory});
            _currentHistoryNode = _history.First;
        }
    }
}