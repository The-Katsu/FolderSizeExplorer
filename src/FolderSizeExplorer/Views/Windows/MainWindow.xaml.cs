using System;
using System.Collections.Generic;
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
            CreateTreeView();
        }

        private void CreateTreeView()
        {
            var pcHeaderStackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            var computerBitmapImage = new BitmapImage();
            computerBitmapImage.BeginInit();
            computerBitmapImage.UriSource = new Uri(@"/Resources/Icons/computer.png", UriKind.RelativeOrAbsolute);
            computerBitmapImage.EndInit();
            var computerImage = new Image {Source = computerBitmapImage, Width = 18, Height = 18};
            var computerText = new TextBlock {Text = "This PC", Margin = new Thickness(10, 0, 0, 0)};
            pcHeaderStackPanel.Children.Add(computerImage);
            pcHeaderStackPanel.Children.Add(computerText);
            
            var pcTreeViewItem = new TreeViewItem {Header = pcHeaderStackPanel, IsExpanded = true};

            var drivers = DriveInfo.GetDrives();

            foreach (var driver in drivers)
            {
                var driverStackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                var driverBitmapImage = new BitmapImage();
                driverBitmapImage.BeginInit();
                driverBitmapImage.UriSource = new Uri(@"/Resources/Icons/driver.png", UriKind.RelativeOrAbsolute);
                driverBitmapImage.EndInit();
                var driverImage = new Image {Source = driverBitmapImage, Width = 18, Height = 18};
                var driverName = new TextBlock {Text = driver.Name, Margin = new Thickness(10, 0, 0, 0)};
                driverStackPanel.Children.Add(driverImage);
                driverStackPanel.Children.Add(driverName);
                
                var driverTreeViewItem = new TreeViewItem { Header = driverStackPanel };
                pcTreeViewItem.Items.Add(driverTreeViewItem);
            }

            TreeView.Items.Add(pcTreeViewItem);
        }
    }
}
