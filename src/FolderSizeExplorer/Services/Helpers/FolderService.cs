using System;
using System.Collections.ObjectModel;
using System.IO;
using FolderSizeExplorer.Models;

namespace FolderSizeExplorer.Services.Helpers
{
    internal static class FolderService
    {
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
                    IconSource = "/Resources/Icons/Special/folder.png",
                    Path = directory.FullName
                };
                subfolders.Add(folder);
            }
            
            return subfolders;
        }

        public static long CalculateSize(string path)
        {
            long size = 0;   
            var folder = new DirectoryInfo(path);
            
            try
            {
                var fis = folder.GetFiles();
                foreach (var fi in fis) 
                {      
                    size += fi.Length;    
                }
            
                var dis = folder.GetDirectories();
                foreach (var di in dis) 
                {
                    size += CalculateSize(di.FullName);   
                }
            }
            catch (Exception e)
            {
                size = 0;
            }
            
            return size; 
        }
    }
}