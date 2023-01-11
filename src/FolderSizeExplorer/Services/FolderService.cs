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
            var folders = new ObservableCollection<Folder>();

            var thisPc = new Folder
            {
                Image = "/Resources/Icons/TreeView/computer.png",
                Name = "This PC",
                Path = "",
                IsExpanded = true
            };

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

            var specialFolders = new ObservableCollection<Folder>
            {
                desktop,
                downloads,
                documents,
                pictures,
                music,
                videos
            };

            thisPc.Subfolders = specialFolders;
            
            var drivers = DriveInfo.GetDrives();
            foreach (var driver in drivers)
            {
                var d = new Folder
                {
                    Image = "/Resources/Icons/TreeView/driver.png",
                    Name = driver.Name,
                    Path = driver.RootDirectory.Name
                };
                thisPc.Subfolders.Add(d);
            }
            
            folders.Add(thisPc);
            return folders;
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