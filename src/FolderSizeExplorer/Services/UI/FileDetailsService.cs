﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using FolderSizeExplorer.Infrastructure.Events;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.Services.Helpers;
using FolderSizeExplorer.ViewModels;
using File = FolderSizeExplorer.Models.Base.File;

namespace FolderSizeExplorer.Services.UI
{
    internal static class FileDetailsService
    {
        private static CancellationTokenSource _tokenSource;
        public static event EventHandler<ValueChangedEvent<int>> ProgressBarUpdate;

        private static void OnCancellingEvent(object sender, EmptyArgsEvent e)
        {
            _tokenSource.Cancel();
        }
        public static void GetFiles(ObservableCollection<FileDetails> fileDetailsCollection, string path, List<File> currentFiles, string unit = "MB")
        {
            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;
            MainWindowViewModel.CancelFilesUploading += OnCancellingEvent;
            fileDetailsCollection.Clear();
            var folder = new DirectoryInfo(path);

            var directories = folder.GetDirectories();
            var files = folder.GetFiles();
            var current = 0;
            var complete = directories.Length + files.Length;
            
            var foldersTask = Task.Run(() =>
            {
                foreach (var directory in directories)
                {
                    if (token.IsCancellationRequested) break;
                    var dir = new FileDetails
                    {
                        IconSource = "/Resources/Icons/Special/folder.png",
                        Name = directory.Name,
                        Path = directory.FullName,
                        Size = FolderService.CalculateSize(directory.FullName),
                        Type = directory.Extension == string.Empty ? "File folder" : directory.Extension,
                        IsDirectory = true,
                        FoldersCount = FolderService.GetFoldersCount(directory),
                        FilesCount = FolderService.GetFilesCount(directory)
                    };
                    dir.HumanReadSize = SizeUnitConverter.ToHumanReadSize(dir.Size, unit);
                    if (token.IsCancellationRequested) break;
                    Application.Current.Dispatcher.Invoke(() => fileDetailsCollection.Add(dir));
                    currentFiles.Add(dir);
                    current++;
                    ProgressBarUpdate?.Invoke(null, new ValueChangedEvent<int>(100 * current / complete));
                }
            }, token);
            
            var filesTask = Task.Run(() =>
            {
                foreach (var file in files)
                {
                    if (token.IsCancellationRequested) break;
                    var f = new FileDetails
                    {
                        IconSource = FileService.GetImageByExtension(file.Extension),
                        Name = file.Name,
                        Path = file.FullName,
                        Size = file.Length,
                        Type = file.Extension,
                        IsDirectory = false
                    };
                    f.HumanReadSize = SizeUnitConverter.ToHumanReadSize(f.Size, unit);
                    if (token.IsCancellationRequested) break;
                    Application.Current.Dispatcher.Invoke(() => fileDetailsCollection.Add(f));
                    currentFiles.Add(f);
                    current++;
                    ProgressBarUpdate?.Invoke(null, new ValueChangedEvent<int>(100 * current/complete));
                }
            }, token);
            Task.WhenAll(foldersTask, filesTask);
        }

        
    }
}