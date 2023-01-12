using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderSizeExplorer.Test.Console.IO;

namespace FolderSizeExplorer.Test.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo info = new DirectoryInfo(@"../..");
            var drivers = new Drvs().Get();
            System.Console.WriteLine();
        }
    }
}
