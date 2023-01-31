using System.Diagnostics;

namespace FolderSizeExplorer.Services.Helpers
{
    public static class ExplorerService
    {
        public static void OpenWithSelect(string path)
        {
            Process.Start("explorer.exe", $"/select, {path}");
        }
    }
}