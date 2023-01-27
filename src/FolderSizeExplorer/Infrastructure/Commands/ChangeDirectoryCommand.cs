using System;
using System.Windows.Controls;
using FolderSizeExplorer.Infrastructure.Commands.Base;
using FolderSizeExplorer.Infrastructure.Events;

namespace FolderSizeExplorer.Infrastructure.Commands
{
    internal class ChangeDirectoryCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var btn = parameter as Button;
            switch (btn.Name)
            {
                case "HistoryPreviousButton":
                    HistoryChangedEvent?.Invoke(this, new HistoryChangeEvent(true, false, false));
                    break;
                case "HistoryNextButton":
                    HistoryChangedEvent?.Invoke(this, new HistoryChangeEvent(false, true, false));
                    break;
                case "HistoryUpButton":
                    HistoryChangedEvent?.Invoke(this, new HistoryChangeEvent(false, false, true));
                    break;
            }
        }
        
        public static event EventHandler<HistoryChangeEvent> HistoryChangedEvent;
    }
}