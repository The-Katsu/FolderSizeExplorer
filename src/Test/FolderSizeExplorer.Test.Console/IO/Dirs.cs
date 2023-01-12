using System.Collections.Generic;
using System.IO;

namespace FolderSizeExplorer.Test.Console.IO
{
    public class Dirs
    {
        public Dirs()
        {
            
        }

        public List<string> Get(string path)
        {
            var result = new List<string>();
            var directory = new DirectoryInfo(path);
            var directories = directory.GetDirectories();
            foreach (var dir in directories)
                result.Add(dir.Name);

            return result;
        }

        public long CalculateSize(DirectoryInfo path)
        {
            long size = 0;
            
            var files = path.GetFiles();
            foreach (var file in files)
            {
                size += file.Length;
            }

            var dirs = path.GetDirectories();
            foreach (var dir in dirs)
            {
                size += CalculateSize(dir);
            }

            return size;
        }
    }
}