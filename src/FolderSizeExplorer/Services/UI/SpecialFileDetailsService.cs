﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FolderSizeExplorer.Infrastructure.Events;
using FolderSizeExplorer.Models;
using FolderSizeExplorer.Services.Helpers;
using Microsoft.WindowsAPICodePack.Shell;

namespace FolderSizeExplorer.Services.UI
{
    internal static class SpecialFileDetailsService
    {
        public static event EventHandler<ValueChangedEvent<int>> ProgressBarUpdate;
        public static void GetBase(ObservableCollection<SpecialFileDetails> specialFileDetailsCollection, string unit = "MB")
        {
            specialFileDetailsCollection.Clear();

            foreach (var specialFile in GetSpecialFiles())
            {
                specialFileDetailsCollection.Add(specialFile);
            }

            foreach (var driverFile in GetDriverFiles())
            {
                driverFile.HumanReadSize = SizeUnitConverter.ToHumanReadSize(driverFile.TotalSpace, unit);
                driverFile.FreeSpaceHumanRead = SizeUnitConverter.ToHumanReadSize(driverFile.FreeSpace, unit);
                specialFileDetailsCollection.Add(driverFile);
            }
            
            ProgressBarUpdate?.Invoke(null, new ValueChangedEvent<int>(100));
        }

        private static List<SpecialFileDetails> GetSpecialFiles()
        {
            var documents = new SpecialFileDetails
            {
                IconSource = "/Resources/Icons/Special/documents.png",
                Name = KnownFolders.Documents.CanonicalName,
                Path = KnownFolders.Documents.Path,
                Type = "Specific Folder",
                //TotalSpace = FolderService.CalculateSize(KnownFolders.Documents.Path),
                IsDirectory = true
            };

            var downloads = new SpecialFileDetails
            {
                IconSource = "/Resources/Icons/Special/downloads.png",
                Name = KnownFolders.Downloads.CanonicalName,
                Path = KnownFolders.Downloads.Path,
                Type = "Specific Folder",
                //TotalSpace = FolderService.CalculateSize(KnownFolders.Downloads.Path),
                IsDirectory = true
            };

            var desktop = new SpecialFileDetails
            {
                IconSource = "/Resources/Icons/Special/desktop.png",
                Name = KnownFolders.Desktop.CanonicalName,
                Path = KnownFolders.Desktop.Path,
                Type = "Specific Folder",
                //TotalSpace = FolderService.CalculateSize(KnownFolders.Desktop.Path),
                IsDirectory = true
            };

            var music = new SpecialFileDetails
            {
                IconSource = "/Resources/Icons/Special/music.png",
                Name = KnownFolders.Music.CanonicalName,
                Path = KnownFolders.Music.Path,
                Type = "Specific Folder",
                //TotalSpace = FolderService.CalculateSize(KnownFolders.Music.Path),
                IsDirectory = true
            };

            var pictures = new SpecialFileDetails
            {
                IconSource = "/Resources/Icons/Special/pictures.png",
                Name = KnownFolders.Pictures.CanonicalName,
                Path = KnownFolders.Pictures.Path,
                Type = "Specific Folder",
                //TotalSpace = FolderService.CalculateSize(KnownFolders.Pictures.Path),
                IsDirectory = true
            };
            
            var videos = new SpecialFileDetails
            {
                IconSource = "/Resources/Icons/Special/video.png",
                Name = KnownFolders.Videos.CanonicalName,
                Path = KnownFolders.Videos.Path,
                Type = "Specific Folder",
                //TotalSpace = FolderService.CalculateSize(KnownFolders.Videos.Path),
                IsDirectory = true
            };

            return new List<SpecialFileDetails>
            {
                desktop,
                downloads,
                documents,
                pictures,
                music,
                videos
            };
        }

        private static List<SpecialFileDetails> GetDriverFiles()
        {
            var drivers = DriveInfo.GetDrives();
            return drivers.Select(driver => new SpecialFileDetails
                {
                    IconSource = "/Resources/Icons/Special/driver.png",
                    Name = driver.Name,
                    Path = driver.RootDirectory.Name,
                    TotalSpace = driver.TotalSize,
                    FreeSpace = driver.TotalFreeSpace,
                    Type = "Driver",
                    IsDirectory = true
                })
                .ToList();
        }
    }
}