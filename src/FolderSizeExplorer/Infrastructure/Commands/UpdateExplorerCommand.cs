using System;
using FolderSizeExplorer.Infrastructure.Commands.Base;
using FolderSizeExplorer.Infrastructure.Events;

namespace FolderSizeExplorer.Infrastructure.Commands
{
    internal class UpdateExplorerCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => UpdateExplorerEvent?.Invoke(this, null);

        public static event EventHandler<UpdateExplorerEvent> UpdateExplorerEvent;
    }
}