using System.Collections.ObjectModel;
using System.IO;
using FolderSizeExplorer.Models;
using Microsoft.WindowsAPICodePack.Shell;

namespace FolderSizeExplorer.Services
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
        public static ObservableCollection<Folder> GetBase()
        {
            return new ObservableCollection<Folder>
            {
                new Folder
                {
                    IconSource = "/Resources/Icons/Special/computer.png",
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
                    IconSource = "/Resources/Icons/Special/driver.png",
                    Name = driver.Name,
                    Path = driver.RootDirectory.Name
                };
                driverFolders.Add(d);
            }
            return driverFolders;
        }
    }
}