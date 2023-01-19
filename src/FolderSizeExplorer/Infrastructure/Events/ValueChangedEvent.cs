using System;

namespace FolderSizeExplorer.Infrastructure.Events
{
    public class ValueChangedEvent<T> : EventArgs
    {
        public ValueChangedEvent(T newValue)
        {
            NewValue = newValue;
        }
        
        public T NewValue { get; }
    }
}