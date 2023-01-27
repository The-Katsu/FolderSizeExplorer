using System.Collections.Generic;
using FolderSizeExplorer.Infrastructure.Commands.Base;
using FolderSizeExplorer.Models.Base;
using FolderSizeExplorer.Services;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FolderSizeExplorer.Infrastructure.Commands
{
    internal class ExportToCsvCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                CsvService.ExportData(dialog.FileName, parameter as List<File>);
            }
        }
    }
}