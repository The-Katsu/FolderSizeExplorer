using FolderSizeExplorer.Infrastructure.Commands.Base;
using FolderSizeExplorer.Services;
using FolderSizeExplorer.Services.Helpers;

namespace FolderSizeExplorer.Infrastructure.Commands
{
    internal class EmptyRecycleBinCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => BinService.Empty();
    }
}