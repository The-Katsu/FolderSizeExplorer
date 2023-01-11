using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.Services;
using FolderSizeExplorer.ViewModels.Base;

namespace FolderSizeExplorer.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Folder> Folders { get; set; }

        public MainWindowViewModel()
        {
            Folders = FolderService.Base();
        }
    }
}