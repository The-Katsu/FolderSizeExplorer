using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FolderSizeExplorer.Models;
using File = FolderSizeExplorer.Models.Base.File;

namespace FolderSizeExplorer.Services
{
    internal class CsvService
    {
        public static void ExportData(string path, List<File> files)
        {
            var now = DateTime.Now.ToString("dddd, dd MMMM yyyy HH-mm-ss");
            var filePath = $"{path}\\{now}.csv";
            var text = new StringBuilder();
            using (var writer = new StreamWriter(filePath))
            {
                switch (files.First())
                {
                    case FileDetails _:
                        text.AppendLine("Name," +
                                        "Size," +
                                        "Files Count," +
                                        "Folders Count," +
                                        "Date Modified," +
                                        "Date Created," +
                                        "Type");
                        break;
                    case SpecialFileDetails _:
                        text.AppendLine("Name," +
                                        "Total Space," +
                                        "Free Space," +
                                        "Type");
                        break;
                }
                foreach (var file in files)
                {
                    switch (file)
                    {
                        case FileDetails details:
                            text.AppendLine($"{details.Name}," +
                                            $"{details.HumanReadSize}," +
                                            $"{details.FilesCount}," +
                                            $"{details.FoldersCount}," +
                                            $"{details.ModifiedAt}," +
                                            $"{details.CreatedAt}," +
                                            $"{details.Type}");
                            break;
                        case SpecialFileDetails specialDetails:
                            text.AppendLine($"{specialDetails.Name}," +
                                            $"{specialDetails.HumanReadSize}," +
                                            $"{specialDetails.FreeSpaceHumanRead}," +
                                            $"{specialDetails.Type}");
                            break;
                    }
                }
                writer.Write(text);
            }
        }
    }
}