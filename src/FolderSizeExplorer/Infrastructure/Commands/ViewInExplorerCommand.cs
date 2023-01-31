using System.Windows.Controls;
using FolderSizeExplorer.Infrastructure.Commands.Base;
using FolderSizeExplorer.Models.Base;
using FolderSizeExplorer.Services.Helpers;

namespace FolderSizeExplorer.Infrastructure.Commands
{
    internal class ViewInExplorerCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var row = parameter as DataGridRow;
            var item = row?.Item as File;
            ExplorerService.OpenWithSelect(item?.Path);
        }
    }
}