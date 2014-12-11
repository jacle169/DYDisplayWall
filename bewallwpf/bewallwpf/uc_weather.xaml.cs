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
    /// Interaction logic for uc_weather.xaml
    /// </summary>
    public partial class uc_weather : UserControl
    {
        private string[] weatherStatus = new string[] { "阳光明媚", "有雪", "阴天", "雨", "雷雨" };
        private string[] weatherIcons = new string[] { "SunnyWeather", "SnowyWeather", "PartlyCloudyWeather", "RainyWeather", "ThunderstormWeather" };
        public uc_weather()
        {
            InitializeComponent();
            this.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("/bewallwpf;component/Weather/SunnyWeatherTemplate.xaml", UriKind.RelativeOrAbsolute) });
            this.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("/bewallwpf;component/Weather/SnowyWeatherTemplate.xaml", UriKind.RelativeOrAbsolute) });
            this.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("/bewallwpf;component/Weather/PartlyCloudyWeatherTemplate.xaml", UriKind.RelativeOrAbsolute) });
            this.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("/bewallwpf;component/Weather/ThunderstormWeatherTemplate.xaml", UriKind.RelativeOrAbsolute) });
            this.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("/bewallwpf;component/Weather/RainyWeatherTemplate.xaml", UriKind.RelativeOrAbsolute) });
        }

        public void SetWeather(em_weather weathertype, double temp, int hum)
        {
            int ran = (int)weathertype;
            ContentControl icon = new ContentControl();
            icon.Template = this.Resources[weatherIcons[ran]] as ControlTemplate;
            icon.Height = icon.Width = 180;
            this.weather.Content = new Viewbox() { Child = icon };
            this.temptext.Text = string.Format("温度 {0}°C", temp);
            this.statustext.Text = weatherStatus[ran];
            this.humtext.Text = string.Format("相对湿度 {0}%", hum);
        }

    }
}
