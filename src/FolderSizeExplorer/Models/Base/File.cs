namespace FolderSizeExplorer.Models.Base
{
    internal abstract class File
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string IconSource { get; set; }
        public string Type { get; set; }
        public virtual bool IsSelected { get; set; }
        public virtual bool IsDirectory { get; set; }
    }
}