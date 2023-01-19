using System.Windows;
using FolderSizeExplorer.Infrastructure.Commands.Base;

namespace FolderSizeExplorer.Infrastructure.Commands
{
    internal class ExplorerDoubleClickCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}