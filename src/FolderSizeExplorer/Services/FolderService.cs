using System.Collections.ObjectModel;
using System.IO;
using FolderSizeExplorer.Models;

namespace FolderSizeExplorer.Services
{
    internal static class FolderService
    {
        public static ObservableCollection<Folder> Base()
        {
            var folders = new ObservableCollection<Folder>();

            var thisPc = new Folder
            {
                Image = "/Resources/Icons/TreeView/computer.png",
                Name = "This PC",
                Path = ""
            };

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
    }
}