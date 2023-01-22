namespace FolderSizeExplorer.Services.Helpers
{
    internal static class FileService
    {
        internal static string GetImageByExtension(string extension)
        {
            switch (extension)
            {
                case ".png":
                case ".jpg":
                    return "/Resources/Icons/Extension/image.png";
                case ".mkv":
                case ".mp4":
                    return "/Resources/Icons/Extension/video.png";
                case ".doc":
                case ".docx":
                case ".dot":
                case ".dotx":
                case ".rtf":
                case ".odt":
                    return "/Resources/Icons/Extension/doc.png";
                case ".xls":
                case ".xlsx":
                    return "/Resources/Icons/Extension/xls.png";      
                case ".pdf":
                    return "/Resources/Icons/Extension/pdf.png";   
                default:
                    return "/Resources/Icons/Extension/base.png";
            }
        }
    }
}