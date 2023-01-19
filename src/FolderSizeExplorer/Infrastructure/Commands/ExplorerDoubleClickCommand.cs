using System;
using FolderSizeExplorer.Infrastructure.Commands.Base;
using FolderSizeExplorer.Infrastructure.Events;
using FolderSizeExplorer.Models.Base;

namespace FolderSizeExplorer.Infrastructure.Commands
{
    internal class ExplorerDoubleClickCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var file = parameter as File;
            if (file.IsDirectory)
                SelectedPathChanged?.Invoke(this, new ValueChangedEvent<string>((parameter as File)?.Path));
            
            
        }

        public static event EventHandler<ValueChangedEvent<string>> SelectedPathChanged;
    }
}