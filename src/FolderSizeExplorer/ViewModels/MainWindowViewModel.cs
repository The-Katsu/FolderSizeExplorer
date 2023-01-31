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

        #region Events

        public static event EventHandler<EmptyArgsEvent> CancelFilesUploading;
    
        #endregion
        
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

        private string _unit = "MB";
        
        private int _percent = 0;

        public int Percent
        {
            get => _percent;
            set
            {
                _percent = value;
                OnPropertyChanged();
            }
        }
        
        // used for csv data export
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
                SpecialFileDetailsService.GetBase(SpecialFileDetailsCollection, _unit);
                CurrentFiles = SpecialFileDetailsCollection.Cast<File>().ToList();
            }
            else
            {
                CurrentFiles.Clear();
                FileDetailsService.GetFiles(FileDetailsCollection, _currentDirectory, CurrentFiles, _unit);
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

        private void OnExplorerUpdate(object sender, EmptyArgsEvent e)
        {
            CancelFilesUploading?.Invoke(this, new EmptyArgsEvent());
            UpdateExplorer();
        }

        private void OnDirectoryChanged(object sender, ValueChangedEvent<string> e)
        {
            CancelFilesUploading?.Invoke(this, new EmptyArgsEvent());
            CurrentDirectory = e.NewValue;
            _currentHistoryNode = _history.Last;
        }

        private void OnProgressBarUpdate(object sender, ValueChangedEvent<int> e)
        {
            Percent = e.NewValue;
        }

        private void OnHistoryChanged(object sender, HistoryChangeEvent e)
        {
            CancelFilesUploading?.Invoke(this, new EmptyArgsEvent());
            if (e.Previous) HistoryMove(true, false, false);
            if (e.Next) HistoryMove(false, true, false);
            if (e.Up) HistoryMove(false, false, true);
        }

        private void OnSizeUnitChanged(object sender, ValueChangedEvent<string> e)
        {
            if (!string.Equals(e.NewValue, _unit))
            {
                _unit = e.NewValue;
                UpdateExplorer();
            }
        }
        
        #endregion
        
        #endregion

        public MainWindowViewModel()
        {
            Folder.SelectedPathChanged += OnDirectoryChanged;
            ExplorerDoubleClickCommand.SelectedPathChanged += OnDirectoryChanged;
            ChangeDirectoryCommand.HistoryChangedEvent += OnHistoryChanged;
            UpdateExplorerCommand.UpdateExplorerEvent += OnExplorerUpdate;
            FileDetailsService.ProgressBarUpdate += OnProgressBarUpdate;
            SpecialFileDetailsService.ProgressBarUpdate += OnProgressBarUpdate;
            ChangeSizeUnitCommand.SizeUnitChanged += OnSizeUnitChanged; 
            TreeViewService.GetBase(Folders);
            SpecialFileDetailsService.GetBase(SpecialFileDetailsCollection);
            CurrentFiles = SpecialFileDetailsCollection.Cast<File>().ToList();
            _history = new LinkedList<string>(new [] {_currentDirectory});
            _currentHistoryNode = _history.First;
        }
    }
}