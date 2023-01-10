using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FolderSizeExplorer.Models;

namespace FolderSizeExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Image LoadImage(string source, int width, int height)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(source, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            return new Image
            {
                Source = bitmap,
                Width = width,
                Height = height
            };
        }

        private StackPanel MakeHeader(Image image, TextBlock textBlock)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(textBlock);
            return stackPanel;
        }
        
        private void CreateTreeView()
        {
            var computerImage = LoadImage(@"/Resources/Icons/TreeView/computer.png",18,18);
            var computerText = new TextBlock {Text = "This PC", Margin = new Thickness(5, 0, 0, 0)};
            var pcHeader = MakeHeader(computerImage, computerText);
            
            var pcTreeViewItem = new TreeViewItem {Header = pcHeader, IsExpanded = true};

            var drivers = DriveInfo.GetDrives();

            var desktopImage = LoadImage(@"/Resources/Icons/TreeView/desktop.png",18,18);
            var desktopText = new TextBlock {Text = "Desktop", Margin = new Thickness(5, 0, 0, 0)};
            var desktopHeader = MakeHeader(desktopImage, desktopText);
            var desktopTreeViewItem = new TreeViewItem{Header = desktopHeader};
            
            var documentsImage = LoadImage(@"/Resources/Icons/TreeView/documents.png",18,18);
            var documentsText = new TextBlock {Text = "Documents", Margin = new Thickness(5, 0, 0, 0)};
            var documentsHeader = MakeHeader(documentsImage, documentsText);
            var documentsTreeViewItem = new TreeViewItem{Header = documentsHeader};
            
            var downloadImage = LoadImage(@"/Resources/Icons/TreeView/downloads.png",18,18);
            var downloadsText = new TextBlock {Text = "Downloads", Margin = new Thickness(5, 0, 0, 0)};
            var downloadsHeader = MakeHeader(downloadImage, downloadsText);
            var downloadsTreeViewItem = new TreeViewItem{Header = downloadsHeader};

            var picturesImage = LoadImage(@"/Resources/Icons/TreeView/pictures.png",18,18);
            var picturesText = new TextBlock {Text = "Pictures", Margin = new Thickness(5, 0, 0, 0)};
            var picturesHeader = MakeHeader(picturesImage, picturesText);
            var picturesTreeViewItem = new TreeViewItem{Header = picturesHeader};

            var videosImage = LoadImage(@"/Resources/Icons/TreeView/video.png",18,18);
            var videosText = new TextBlock {Text = "Videos", Margin = new Thickness(5, 0, 0, 0)};
            var videosHeader = MakeHeader(videosImage, videosText);
            var videosTreeViewItem = new TreeViewItem{Header = videosHeader};
            
            var musicImage = LoadImage(@"/Resources/Icons/TreeView/music.png",18,18);
            var musicText = new TextBlock {Text = "Music", Margin = new Thickness(5, 0, 0, 0)};
            var musicHeader = MakeHeader(musicImage, musicText);
            var musicTreeViewItem = new TreeViewItem{Header = musicHeader};

            pcTreeViewItem.Items.Add(desktopTreeViewItem);
            pcTreeViewItem.Items.Add(documentsTreeViewItem);
            pcTreeViewItem.Items.Add(downloadsTreeViewItem);
            pcTreeViewItem.Items.Add(picturesTreeViewItem);
            pcTreeViewItem.Items.Add(videosTreeViewItem);
            pcTreeViewItem.Items.Add(musicTreeViewItem);
            
            foreach (var driver in drivers)
            {
                var driverImage = LoadImage(@"/Resources/Icons/TreeView/driver.png", 18, 18);
                var driverText = new TextBlock {Text = driver.Name, Margin = new Thickness(5, 0, 0, 0)};
                var driverHeader = MakeHeader(driverImage, driverText);
                
                var driverTreeViewItem = new TreeViewItem { Header = driverHeader };
                pcTreeViewItem.Items.Add(driverTreeViewItem);
            }

            TreeView.Items.Add(pcTreeViewItem);
        }
    }
}
