﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        #region History

        private LinkedList<string> _history;
        private LinkedListNode<string> _currentHistoryNode;

        #endregion

        #region Collections

        public ObservableCollection<Folder> Folders { get; }
        public ObservableCollection<FileDetails> FileDetailsCollection { get; set; } =
            new ObservableCollection<FileDetails>();
        public ObservableCollection<SpecialFileDetails> SpecialFileDetailsCollection { get; set; } = 
            new ObservableCollection<SpecialFileDetails>();

        #endregion
        
        #region Properties

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
        
        #endregion

        #region Methods

        #region EventsCathcer

        private void OnDirectoryChanged(object sender, ValueChangedEvent<string> e)
        {
            CurrentDirectory = e.NewValue;
            _currentHistoryNode = _history.Last;
        }

        private void OnHistoryChanged(object sender, HistoryChangeEvent e)
        {
            if (e.Previous)
            {
                for (var node = _history.First; node != null; node = node.Next)
                {
                    if (node == _currentHistoryNode && node.Previous != null)
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
                    if (node == _currentHistoryNode && node.Next != null)
                    {
                        _currentHistoryNode = node.Next;
                        CurrentDirectory = _currentHistoryNode?.Value;
                        break;
                    }
                }
            }

            if (e.Up)
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