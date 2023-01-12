using System;
using System.Collections.ObjectModel;
using System.IO;
using FolderSizeExplorer.Models;
using Microsoft.WindowsAPICodePack.Shell;

namespace FolderSizeExplorer.Services
{
    internal static class FolderService
    {
        /// <summary>
        /// Create collection for base tree view like
        /// This pc
        /// - Documents
        /// - Videos
        /// - Disk C
        /// etc.
        /// </summary>
        /// <returns>Collection of specific folders and drivers</returns>
        public static ObservableCollection<Folder> Base()
        {
            return new ObservableCollection<Folder>
            {
                new Folder
                {
                    Image = "/Resources/Icons/TreeView/computer.png",
                    Name = "This PC",
                    Path = "",
                    IsExpanded = true,
                    Subfolders = GetThisPcSubfolders()
                }
            };
        }
        public static ObservableCollection<Folder> GetThisPcSubfolders()
        {
            var thisPcSubfolders = GetSpecialFolders();
            foreach (var driver in GetDrivers()) 
                thisPcSubfolders.Add(driver);
            return thisPcSubfolders;
        }
        private static ObservableCollection<Folder> GetSpecialFolders()
        {
            var documents = new Folder
            {
                Image = "/Resources/Icons/TreeView/documents.png",
                Name = KnownFolders.Documents.CanonicalName,
                Path = KnownFolders.Documents.Path
            };

            var downloads = new Folder
            {
                Image = "/Resources/Icons/TreeView/downloads.png",
                Name = KnownFolders.Downloads.CanonicalName,
                Path = KnownFolders.Downloads.Path
            };

            var desktop = new Folder
            {
                Image = "/Resources/Icons/TreeView/desktop.png",
                Name = KnownFolders.Desktop.CanonicalName,
                Path = KnownFolders.Desktop.Path
            };

            var music = new Folder
            {
                Image = "/Resources/Icons/TreeView/music.png",
                Name = KnownFolders.Music.CanonicalName,
                Path = KnownFolders.Music.Path
            };

            var pictures = new Folder
            {
                Image = "/Resources/Icons/TreeView/pictures.png",
                Name = KnownFolders.Pictures.CanonicalName,
                Path = KnownFolders.Pictures.Path
            };
            
            var videos = new Folder
            {
                Image = "/Resources/Icons/TreeView/video.png",
                Name = KnownFolders.Videos.CanonicalName,
                Path = KnownFolders.Videos.Path
            };
            
            return new ObservableCollection<Folder>
            {
                desktop,
                downloads,
                documents,
                pictures,
                music,
                videos
            };
        }
        private static ObservableCollection<Folder> GetDrivers()
        {
            var drivers = DriveInfo.GetDrives();
            var driverFolders = new ObservableCollection<Folder>();
            foreach (var driver in drivers)
            {
                var d = new Folder
                {
                    Image = "/Resources/Icons/TreeView/driver.png",
                    Name = driver.Name,
                    Path = driver.RootDirectory.Name
                };
                driverFolders.Add(d);
            }
            return driverFolders;
        }
        
        public static ObservableCollection<Folder> GetSubfolders(string source)
        {
            var subfolders = new ObservableCollection<Folder>();

            var paths = Directory.GetDirectories(source, "*", SearchOption.TopDirectoryOnly);

            foreach (var path in paths)
            {
                var directory = new DirectoryInfo(path);
                var folder = new Folder
                {
                    Name = directory.Name,
                    Image = "/Resources/Icons/TreeView/folder.png",
                    Path = directory.FullName
                };
                subfolders.Add(folder);
            }
            
            return subfolders;
        }
    }
}