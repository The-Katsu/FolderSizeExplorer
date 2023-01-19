using System;

namespace FolderSizeExplorer.Events
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