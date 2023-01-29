using System;
using System.Windows.Controls;
using FolderSizeExplorer.Infrastructure.Commands.Base;
using FolderSizeExplorer.Infrastructure.Events;

namespace FolderSizeExplorer.Infrastructure.Commands
{
    internal class ChangeSizeUnitCommand : Command
    {
        public static event EventHandler<ValueChangedEvent<string>> SizeUnitChanged;
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var btn = parameter as RadioButton;
            SizeUnitChanged?.Invoke(this, new ValueChangedEvent<string>(btn?.Name));
        }
    }
}