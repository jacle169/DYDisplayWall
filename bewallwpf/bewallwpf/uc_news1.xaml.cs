using System;
using System.Collections.Generic;
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

namespace bewallwpf
{
    /// <summary>
    /// Interaction logic for uc_news1.xaml
    /// </summary>
    public partial class uc_news1 : UserControl
    {
        public uc_news1()
        {
            InitializeComponent();
        }

        public void SetImage(string url)
        {
            var i = new System.Windows.Controls.Image();
            var srcimg = new BitmapImage();
            srcimg.BeginInit();
            srcimg.UriSource = new Uri(url, UriKind.Absolute);
            srcimg.CacheOption = BitmapCacheOption.OnLoad;
            srcimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            srcimg.EndInit();
            ct_img.Source = srcimg;
            ct_img.Stretch = Stretch.UniformToFill;
        }

    }
}
