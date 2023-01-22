using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FolderSizeExplorer.Test.Console.IO;

namespace FolderSizeExplorer.Test.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var w = new System.Diagnostics.Stopwatch();
            
            w.Start();

            var l1 = GetZabilNazvanie("C:\\Program Files");
            
            w.Stop();

            System.Console.WriteLine($"Execution time : {w.ElapsedMilliseconds}");

            var n = new List<string>();
            
            foreach (var i in l1)
            {
                var k = 0;
                foreach (var j in l1)
                {
                    if (string.Equals(i, j))
                    {
                        k++;
                        if (k >=3) break;
                    }
                    
                }

                if (k > 3)
                {
                    n.Add(i);
                }
            }

            var s = string.Join("\n", l1);            
            System.Console.WriteLine(s);
        }

        public static long GetDirectorySize(string parentDirectory)
        {
            long size = 0;   
            var folder = new DirectoryInfo(parentDirectory);
            
            try
            {
                foreach (var fi in folder.GetFiles()) 
                    Interlocked.Add(ref size, fi.Length);

                Parallel.ForEach(folder.GetDirectories(), (subdir) =>
                    Interlocked.Add(ref size, GetDirectorySize(subdir.FullName)));
            }
            catch (Exception e)
            {
                size = 0;
            }
            
            return size; 
        }
        
        public static IEnumerable<string> GetFiles(string root, string searchPattern)
        {
            Stack<string> pending = new Stack<string>();
            pending.Push(root);
            while (pending.Count != 0)
            {
                var path = pending.Pop();
                string[] next = null;
                try
                {
                    next = Directory.GetFiles(path, searchPattern);          
                }
                catch { }
                if(next != null && next.Length != 0)
                    foreach (var file in next) yield return file;
                try
                {
                    next = Directory.GetDirectories(path);
                    foreach (var subdir in next) pending.Push(subdir);
                }
                catch { }
            }
        }

        static long Cal(string path)
        {
            try
            {
                var fso = new Scripting.FileSystemObject();
                var folder = fso.GetFolder(path);
                return (long)folder.Size;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static List<string> GetZabilNazvanie(string path)
        {
            var dir = new DirectoryInfo(path);
            var result = new List<string>();
            try
            {
                var files = dir.GetFiles();
                foreach (var f in files)
                {
                    result.Add(f.Extension);
                }

                var dirs = dir.GetDirectories();
                foreach (var d in dirs)
                {
                    result.AddRange(GetZabilNazvanie(d.FullName));
                }
            }
            catch (Exception e)
            {
                // ignored
            }

            return result;
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
        
        public static long CalculateSize2(string sourceDir, bool recurse)
        {
            long size = 0;
            string[] fileEntries = Directory.GetFiles(sourceDir);

            foreach (string fileName in fileEntries)
            {
                Interlocked.Add(ref size, (new FileInfo(fileName)).Length);
            }

            if (recurse)
            {
                string[] subdirEntries = Directory.GetDirectories(sourceDir);

                Parallel.For<long>(0, subdirEntries.Length, () => 0, (i, loop, subtotal) =>
                    {
                        if ((File.GetAttributes(subdirEntries[i]) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                        {
                            subtotal += CalculateSize2(subdirEntries[i], true);
                            return subtotal;
                        }
                        return 0;
                    },
                    (x) => Interlocked.Add(ref size, x)
                );
            }
            return size;
        }
    }
}
