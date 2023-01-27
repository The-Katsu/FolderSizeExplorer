using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FolderSizeExplorer.Models;
using Microsoft.WindowsAPICodePack.Shell;

namespace FolderSizeExplorer.Services.UI
{
    internal static class TreeViewService
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
        public static void GetBase(ObservableCollection<Folder> folders)
        {
            folders.Clear();
            var thisPc = new Folder
            {
                IconSource = "/Resources/Icons/Special/computer.png",
                Name = "This PC",
                Path = "",
                IsExpanded = true,
                Subfolders = new ObservableCollection<Folder>()
            };
            GetThisPcSubfolders(thisPc.Subfolders);
            folders.Add(thisPc);
        }
        public static void GetThisPcSubfolders(ObservableCollection<Folder> folders)
        {
            folders.Clear();
            foreach (var specialFolder in GetSpecialFolders()) folders.Add(specialFolder);
            foreach (var driver in GetDrivers()) folders.Add(driver);
        }
        private static List<Folder> GetSpecialFolders()
        {
            var documents = new Folder
            {
                IconSource = "/Resources/Icons/Special/documents.png",
                Name = KnownFolders.Documents.CanonicalName,
                Path = KnownFolders.Documents.Path
            };

            var downloads = new Folder
            {
                IconSource = "/Resources/Icons/Special/downloads.png",
                Name = KnownFolders.Downloads.CanonicalName,
                Path = KnownFolders.Downloads.Path
            };

            var desktop = new Folder
            {
                IconSource = "/Resources/Icons/Special/desktop.png",
                Name = KnownFolders.Desktop.CanonicalName,
                Path = KnownFolders.Desktop.Path
            };

            var music = new Folder
            {
                IconSource = "/Resources/Icons/Special/music.png",
                Name = KnownFolders.Music.CanonicalName,
                Path = KnownFolders.Music.Path
            };

            var pictures = new Folder
            {
                IconSource = "/Resources/Icons/Special/pictures.png",
                Name = KnownFolders.Pictures.CanonicalName,
                Path = KnownFolders.Pictures.Path
            };
            
            var videos = new Folder
            {
                IconSource = "/Resources/Icons/Special/video.png",
                Name = KnownFolders.Videos.CanonicalName,
                Path = KnownFolders.Videos.Path
            };
            
            return new List<Folder>
            {
                desktop,
                downloads,
                documents,
                pictures,
                music,
                videos
            };
        }
        private static List<Folder> GetDrivers()
        {
            var drivers = DriveInfo.GetDrives();
            return drivers.Select(driver => new Folder
                {
                    IconSource = "/Resources/Icons/Special/driver.png", 
                    Name = driver.Name, 
                    Path = driver.RootDirectory.Name
                })
                .ToList();
        }
    }
}