using System;

namespace FolderSizeExplorer.Infrastructure.Events
{
    internal class HistoryChangeEvent<T> : EventArgs
    {
        public HistoryChangeEvent(bool previous, bool next, bool up)
        {
            Previous = previous;
            Next = next;
            Up = up;
        }
        
        public bool Previous { get; }
        public bool Next { get; }
        public bool Up { get; }
    }
}