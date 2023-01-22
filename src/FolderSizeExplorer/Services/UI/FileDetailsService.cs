﻿using System.Collections.ObjectModel;
using System.IO;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.Services.Helpers;

namespace FolderSizeExplorer.Services.UI
{
    internal static class FileDetailsService
    {
        public static void GetFiles(ObservableCollection<FileDetails> fileDetailsCollection, string path)
        {
            fileDetailsCollection.Clear();
            var folder = new DirectoryInfo(path);
            foreach (var directory in folder.GetDirectories())
            {
                var dir = new FileDetails
                {
                    IconSource = "/Resources/Icons/Special/folder.png",
                    Name = directory.Name,
                    Path = directory.FullName,
                    Size = FolderService.CalculateSize(directory.FullName),
                    Type = directory.Extension,
                    IsDirectory = true
                };
                fileDetailsCollection.Add(dir);
            }

            foreach (var file in folder.GetFiles())
            {
                var f = new FileDetails
                {
                    IconSource = "/Resources/Icons/Special/file.png",
                    Name = file.Name,
                    Path = file.FullName,
                    Size = file.Length,
                    Type = file.Extension,
                    IsDirectory = false
                };
                fileDetailsCollection.Add(f);
            }
        }
    }
}