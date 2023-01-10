using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.ViewModels.Base;

namespace FolderSizeExplorer.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Folder> Folders { get; set; }

        public MainWindowViewModel()
        {
            Folders = new ObservableCollection<Folder>();

            var f = new Folder()
            {
                Name = "1"
            };

            for (var i = 0; i < 5; i++)
            {
                var folder = new Folder()
                {
                    Name = $"folder {i + 1}"
                };
                f.Subfolders.Add(folder);
            }
            Folders.Add(f);
        }
    }
}