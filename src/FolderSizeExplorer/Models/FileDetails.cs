﻿using System.Windows.Controls;
using System.Windows.Media;
using FolderSizeExplorer.Models.Base;

namespace FolderSizeExplorer.Models
{
    /// <summary>
    /// Explorer Item
    /// </summary>
    internal class FileDetails : File
    {
        public string FileExtension { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
        public int FilesCount { get; set; }
        public int FoldersCount { get; set; }
        public long Size { get; set; }
        private bool _isDirectory;
        public string Type => _isDirectory ? "Folder" : "File";
    }
}