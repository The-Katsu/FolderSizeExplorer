using System;
using System.Runtime.InteropServices;

namespace FolderSizeExplorer.Services.Helpers
{
    internal static class BinService
    {
        private enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        public static void Empty() => SHEmptyRecycleBin(IntPtr.Zero, null, 0);
    }
}