using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace bewallwpf
{
    /// <summary>
    /// Interaction logic for Tile.xaml
    /// </summary>
    public partial class Tile : UserControl, IDisposable
    {
        internal int state = 0;
        internal int aniType = 0;

        DispatcherTimer timer;
        public Tile()
        {
            InitializeComponent();
            state = 0;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            timer.Tick += timer_Tick;
        }

        int totalseconds = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            totalseconds += 1;
            if (totalseconds == int.MaxValue)
            {
                totalseconds = 0;
                return;
            }

            if (totalseconds % refreshSeconds == 0)
            {
                if (this.tileType == em_skin.ad)
                {
                    this.SetImage();
                }
                else if (this.tileType == em_skin.bar)
                {
                    this.SetBar();
                }
                else if (this.tileType == em_skin.line) 
                {
                    this.SetLinear();
                }
                else if (this.tileType == em_skin.news)
                {
                    this.SetNews();
                }
                else if (this.tileType == em_skin.pie)
                {
                    this.SetPie();
                }
                else if (this.tileType == em_skin.weather)
                {
                    this.SetWeather();
                }
                else if (this.tileType == em_skin.datetime)
                {
                    this.SetDatetime();
                    timer.Stop();
                    timer.Tick -= timer_Tick;
                }
            }
        }

        public string TileName { get; set; }
        public string refreshUrl { get; set; }
        public em_skin tileType { get; set; }
        public int refreshSeconds { get; set; }

        internal void SetAni(int ani)
        {
            aniType = ani;
        }

        internal void SetFirstTime(UIElement ele)
        {
            item1.Content = ele;
        }

        public string httpget(Uri url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(myResponseStream);
                return sr.ReadToEnd();
            }
            catch 
            {
                return string.Empty;
            }

        }

        internal void SetWeather()
        {
            string strdata = httpget(new Uri(refreshUrl, UriKind.Absolute));
            if (!string.IsNullOrEmpty(strdata))
            {
                var data = JsonConvert.DeserializeObject<weather>(strdata);

                var weather = new uc_weather();
                weather.SetWeather(data.type, data.temp, data.hum);
                weather.MinHeight = 150;
                weather.MinWidth = 310;

                var pa = this.Parent as RadTransitionControl;
                if (pa != null)
                {
                    var anitype = (em_TileAni)pa.Tag;
                    if (anitype == em_TileAni.t_Tile动画)
                    {
                        var tile = tileCn();
                        tile.Content = new Viewbox() { Child = weather };
                        pa.Content = tile;
                        this.Dispose();
                    }
                    else
                    {
                        if (state == 0)
                        {
                            item2.Content = new Viewbox() { Child = weather };
                        }
                        else
                        {
                            state = 1;
                            item1.Content = new Viewbox() { Child = weather };
                        }
                        anigo();
                    }
                }
            }


        }

        internal void SetDatetime()
        {
            var pa = this.Parent as RadTransitionControl;
            if (pa != null)
            {
                 var anitype = (em_TileAni)pa.Tag;
                if (anitype == em_TileAni.t_Tile动画)
                {
                    aniType = 11;
                    //var tile = tileCn();
                    //tile.Content =new Viewbox() { Child= new uc_datetime() { MinHeight = 150, MinWidth = 310 }};
                    //pa.Content = tile;
                    //this.Dispose();
                }
                if (state == 0)
                {
                    item2.Content = new Viewbox() { Child = new uc_datetime() { MinHeight = 150, MinWidth = 310 } };
                }
                else
                {
                    state = 1;
                    item1.Content = new Viewbox() { Child = new uc_datetime() { MinHeight = 150, MinWidth = 310 } };
                }
                anigo();
            }
        }

        internal void SetNews()
        {
            string strdata = httpget(new Uri(refreshUrl, UriKind.Absolute));
            if (!string.IsNullOrEmpty(strdata))
            {
                var data = JsonConvert.DeserializeObject<News>(strdata);

                var pa = this.Parent as RadTransitionControl;
                if (pa != null)
                {
                    var anitype = (em_TileAni)pa.Tag;
                    if (anitype == em_TileAni.t_Tile动画)
                    {
                        var tile = tileCn();
                        tile.Content = new Viewbox() { Child = CreateNews(data) };
                        pa.Content = tile;
                        this.Dispose();
                    }
                    else
                    {
                        if (state == 0)
                        {
                            item2.Content = new Viewbox() { Child = CreateNews(data) };
                        }
                        else
                        {
                            state = 1;
                            item1.Content = new Viewbox() { Child = CreateNews(data) };
                        }
                        anigo();
                    }
                }
            }
        }

        
        UserControl CreateNews(News ns)
        {
            if (ns.hasImage)
            {
                var uc = new uc_news1();
                uc.ct_context.Text = ns.Context;
                uc.ct_date.Text = ns.Date;
                uc.ct_title.Text = ns.Title;
                uc.SetImage(ns.imageUrl);
                uc.MinHeight = 150;
                uc.MinWidth = 300;
                return uc;
            }
            else
            {
                var uc = new uc_news2();
                uc.ct_context.Text = ns.Context;
                uc.ct_date.Text = ns.Date;
                uc.ct_title.Text = ns.Title;
                uc.MinHeight = 150;
                uc.MinWidth = 300;
                return uc;
            }
        }

        BitmapImage srcimg;
        internal void SetImage()
        {
            string strdata = httpget(new Uri(refreshUrl, UriKind.Absolute));
            if (!string.IsNullOrEmpty(strdata))
            {
                var data = JsonConvert.DeserializeObject<imgobj>(strdata);

                var pa = this.Parent as RadTransitionControl;
                if (pa != null)
                {
                    //string path = @"http://g.hiphotos.baidu.com/image/pic/item/2e2eb9389b504fc2c3a8e804e7dde71190ef6d3f.jpg";
                    if (srcimg != null && srcimg.IsDownloading)
                    {
                        return;
                    }
                    BackgroundWorker worker = new BackgroundWorker();
                    DoWorkEventHandler whandler = null;
                    whandler = (ss, ee) => {
                        worker.DoWork -= whandler;
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {

                            var i = new System.Windows.Controls.Image();
                            srcimg = new BitmapImage();
                            srcimg.BeginInit();
                            srcimg.UriSource = new Uri(data.url, UriKind.Absolute);
                            srcimg.CacheOption = BitmapCacheOption.OnLoad;
                            srcimg.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            srcimg.EndInit();
                            i.Source = srcimg;
                            i.Stretch = Stretch.UniformToFill;

                            EventHandler handler = null;
                            handler = (s, e) =>
                            {
                                srcimg.DownloadCompleted -= handler;

                                var anitype = (em_TileAni)pa.Tag;
                                if (anitype == em_TileAni.t_Tile动画)
                                {
                                    var tile = tileCn();
                                    tile.Content = new Viewbox() { Child = i };
                                    pa.Content = tile;
                                    this.Dispose();
                                }
                                else
                                {
                                    if (state == 0)
                                    {
                                        item2.Content = new Viewbox() { Child = i };
                                    }
                                    else
                                    {
                                        state = 1;
                                        item1.Content = new Viewbox() { Child = i };
                                    }
                                    anigo();
                                }

                            };
                            srcimg.DownloadCompleted += handler;

                        });
                       
                    };
                    worker.RunWorkerAsync();
                    worker.DoWork += whandler;
                }
            }


        }

        internal void SetPie()
        {
            string strdata = httpget(new Uri(refreshUrl, UriKind.Absolute));
            if (!string.IsNullOrEmpty(strdata))
            {
                var data = JsonConvert.DeserializeObject<List<jacData>>(strdata);

                var pa = this.Parent as RadTransitionControl;
                if (pa != null)
                {
                    RadPieChart cartChart = new RadPieChart();
                    cartChart.SmartLabelsStrategy = new PieChartSmartLabelsStrategy()
                    {
                        DisplayMode = PieChartLabelsDisplayMode.SpiderAlignedOutwards
                    };
                    cartChart.Palette = ChartPalettes.Windows8;
                    DoughnutSeries Serie = new DoughnutSeries() { ShowLabels = true, RadiusFactor = 0.7 };
                    cartChart.Series.Add(Serie);
                    Serie.AngleRange = new Telerik.Charting.AngleRange(270, 360);
                    cartChart.MinHeight = 200;
                    cartChart.MinWidth = 310;
                    cartChart.Foreground = new SolidColorBrush(Colors.White);
                    Serie.ValueBinding = new PropertyNameDataPointBinding("Val");

                    Serie.ItemsSource = data;

                    Serie.LegendSettings = new DataPointLegendSettings() { TitleBinding = new PropertyNameDataPointBinding("Key") };
                    RadLegend legend = new RadLegend();
                    legend.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    legend.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    Binding binding = new Binding
                    {
                        Source = cartChart,
                        Path = new PropertyPath("LegendItems"),
                    };
                    legend.SetBinding(RadLegend.ItemsProperty, binding);
                    var ng = new Grid();
                    ng.Children.Add(cartChart);
                    ng.Children.Add(legend);

                    var anitype = (em_TileAni)pa.Tag;
                    if (anitype == em_TileAni.t_Tile动画)
                    {
                        var tile = tileCn();
                        tile.Content = new Viewbox() { Child = ng };
                        pa.Content = tile;
                        this.Dispose();
                    }
                    else
                    {
                        if (state == 0)
                        {
                            item2.Content = new Viewbox() { Child = ng };
                        }
                        else
                        {
                            state = 1;
                            item1.Content = new Viewbox() { Child = ng };
                        }
                        anigo();
                    }
                }
            }
            
        }

        internal void SetBar()
        {
             string strdata = httpget(new Uri(refreshUrl, UriKind.Absolute));
             if (!string.IsNullOrEmpty(strdata))
             {
                 var data = JsonConvert.DeserializeObject<List<barOrLineData>>(strdata);
                 var pa = this.Parent as RadTransitionControl;
                 if (pa != null)
                 {
                     RadCartesianChart cartChart = new RadCartesianChart();
                     cartChart.SmartLabelsStrategy = new ChartSmartLabelsStrategy();
                     cartChart.HorizontalAxis = new CategoricalAxis() { ElementBrush = new SolidColorBrush(Colors.White) };
                     cartChart.VerticalAxis = new LinearAxis() { ElementBrush = new SolidColorBrush(Colors.White) };
                     cartChart.Palette = ChartPalettes.Windows8;

                     foreach (var item in data)
                     {
                         cartChart.Series.Add(CreateBarSeries(item.title, item.showlable, item.data));
                     }

                     cartChart.MinHeight = 300;
                     cartChart.MinWidth = 620;
                     cartChart.Foreground = new SolidColorBrush(Colors.White);
                     RadLegend legend = new RadLegend();
                     legend.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                     legend.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                     Binding binding = new Binding
                     {
                         Source = cartChart,
                         Path = new PropertyPath("LegendItems"),
                     };
                     legend.SetBinding(RadLegend.ItemsProperty, binding);
                     var ng = new Grid();
                     ng.Children.Add(cartChart);
                     ng.Children.Add(legend);

                     var anitype = (em_TileAni)pa.Tag;
                     if (anitype == em_TileAni.t_Tile动画)
                     {
                         var tile = tileCn();
                         tile.Content = new Viewbox() { Child = ng };
                         pa.Content = tile;
                         this.Dispose();
                     }
                     else
                     {
                         if (state == 0)
                         {
                             item2.Content = new Viewbox() { Child = ng };
                         }
                         else
                         {
                             state = 1;
                             item1.Content = new Viewbox() { Child = ng };
                         }
                         anigo();
                     }
                 }
             
             }

           
        }

        internal void SetLinear()
        {
            string strdata = httpget(new Uri(refreshUrl, UriKind.Absolute));
            if (!string.IsNullOrEmpty(strdata))
            {
                var data = JsonConvert.DeserializeObject<List<barOrLineData>>(strdata);

                var pa = this.Parent as RadTransitionControl;
                if (pa != null)
                {
                    RadCartesianChart cartChart = new RadCartesianChart();
                    cartChart.SmartLabelsStrategy = new ChartSmartLabelsStrategy();
                    cartChart.HorizontalAxis = new CategoricalAxis() { ElementBrush = new SolidColorBrush(Colors.White) };
                    cartChart.VerticalAxis = new LinearAxis() { ElementBrush = new SolidColorBrush(Colors.White) };
                    cartChart.Palette = ChartPalettes.Windows8;

                    foreach (var item in data)
                    {
                        cartChart.Series.Add(CreateLineSeries(item.title, item.showlable, item.data));
                    }

                    cartChart.MinHeight = 300;
                    cartChart.MinWidth = 620;
                    cartChart.Foreground = new SolidColorBrush(Colors.White);
                    RadLegend legend = new RadLegend();
                    legend.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    legend.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    Binding binding = new Binding
                    {
                        Source = cartChart,
                        Path = new PropertyPath("LegendItems"),
                    };
                    legend.SetBinding(RadLegend.ItemsProperty, binding);
                    var ng = new Grid();
                    ng.Children.Add(cartChart);
                    ng.Children.Add(legend);

                    var anitype = (em_TileAni)pa.Tag;
                    if (anitype == em_TileAni.t_Tile动画)
                    {
                        var tile = tileCn();
                        tile.Content = new Viewbox() { Child = ng };
                        pa.Content = tile;
                        this.Dispose();
                    }
                    else
                    {
                        if (state == 0)
                        {
                            item2.Content = new Viewbox() { Child = ng };
                        }
                        else
                        {
                            state = 1;
                            item1.Content = new Viewbox() { Child = ng };
                        }
                        anigo();
                    }
                }
            }

        }

        public BarSeries CreateBarSeries(string title, bool ShowLabels, List<jacData> coll)
        {
            BarSeries Serie = new BarSeries() { ShowLabels = ShowLabels };
            Serie.LegendSettings = new SeriesLegendSettings() { Title = title };
            Serie.CategoryBinding = new PropertyNameDataPointBinding("Key");
            Serie.ValueBinding = new PropertyNameDataPointBinding("Val");
            Serie.ItemsSource = coll;
            return Serie;
        }

        public LineSeries CreateLineSeries(string title, bool ShowLabels, List<jacData> coll)
        {
            LineSeries Serie = new LineSeries() { ShowLabels = ShowLabels };
            Serie.LegendSettings = new SeriesLegendSettings() { Title = title };
            Serie.CategoryBinding = new PropertyNameDataPointBinding("Key");
            Serie.ValueBinding = new PropertyNameDataPointBinding("Val");
            Serie.ItemsSource = coll;
            return Serie;
        }

        List<jacData> randerPie()
        {
            var list = new List<jacData>();
            list.Add(new jacData("aa", 20));
            list.Add(new jacData("bb", 30));
            list.Add(new jacData("cc", 40));
            list.Add(new jacData("dd", 10));
            return list;
        }

        List<jacData> randerCoollection()
        {
            var list = new List<jacData>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(new jacData(i.ToString() +"月", other.getOther().ran.Next(0, 200)));
            }
            return list;
        }

        //internal void SetContent(string TileName)
        //{
        //    var pa = this.Parent as RadTransitionControl;
        //    if (pa != null)
        //    {
        //        var anitype = (em_TileAni)pa.Tag;
        //        if (anitype == em_TileAni.t_Tile动画)
        //        {
        //            pa.Content = CreateTileCore(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, TileName, 0);
        //           this.Dispose();
        //        }
        //        else
        //        {
        //            TextBlock tb = new TextBlock();
        //            tb.Foreground = System.Windows.Media.Brushes.White;
        //            tb.Margin = new Thickness(5);
        //            tb.FontSize = 26;
        //            tb.Text = "这是测试";
        //            if (state == 0)
        //            {
        //                item2.Content = tb;
        //            }
        //            else
        //            {
        //                state = 0;
        //                item1.Content = tb;
        //            }
        //            anigo();
        //        }
        //    }
        //}

        Tile CreateTileCore(em_TileType type, System.Windows.Media.Brush bg, int margin, string name, int ani)
        {
            var tile = new Tile();
            tile.h = this.h;
            tile.Background = this.Background;
            tile.Margin = this.Margin;
            tile.TileName = this.TileName;
            tile.SetAni(ani);
            return tile;
        }

        Tile tileCn()
        {
            this.timer.Stop();
            var tile = new Tile();
            tile.h = this.h;
            tile.w = this.w;
            tile.Background = this.Background;
            tile.Margin = this.Margin;
            tile.TileName = this.TileName;
            tile.refreshUrl = this.refreshUrl;
            tile.refreshSeconds = this.refreshSeconds;
            tile.tileType = this.tileType;
            tile.SetAni(this.aniType);
            return tile;
        }

        void anigo()
        {
            switch (aniType)
            {
                case 11:
                    ani_righttoleft();
                    break;
                case 12:
                    ani_lefttoright();
                    break;
                case 13:
                    ani_lefttorightf_mazing();
                    break;
                case 14:
                    ani_downtoup();
                    break;
                case 15:
                    ani_uptodown();
                    break;
                case 16:
                    ani_downtoup_mazing();
                    break;
                default:
                    break;
            }
        }

        //internal void loadurl()
        //{
        //    Uri uri = new Uri("http://222.161.197.93/bcshgl/Charts/bingtu.aspx", UriKind.Absolute);
        //    var wb = new WebBrowser();
        //    wb.Navigate(uri);
        //    item1.LayoutRoot.Children.Add(wb);
        //}



        internal void ani_righttoleft()
        {
            double pos = w + 10;
            anigoX(-pos, pos);
        }

        internal void ani_lefttoright()
        {
            double pos = w + 10;
            anigoX(pos, -pos);
        }

        internal void ani_lefttorightf_mazing()
        {
            double pos = w + 10;
            anigoX(pos, pos);
        }

        internal void ani_downtoup()
        {
            double pos = h + 10;
            anigoY(-pos, pos);
        }

        internal void ani_uptodown()
        {
            double pos = h + 10;
            anigoY(pos, -pos);
        }

        internal void ani_downtoup_mazing()
        {
            double pos = h + 10;
            anigoY(pos, pos);
        }

        //var easing = new QuarticEase();
        //var easing = new BackEase();
        //var easing = new BounceEase();
        //var easing = new CircleEase();
        //var easing = new CubicEase();
        //var easing = new ElasticEase();
        //var easing = new ExponentialEase();
        //var easing = new PowerEase();
        //var easing = new QuadraticEase();
        //var easing = new QuarticEase();
        //var easing = new QuinticEase();
        ////var easing = new SineEase();

        //easing.EasingMode = EasingMode.EaseInOut;

        void anigoX(double start, double end)
        {
            var dr = new Duration(TimeSpan.FromSeconds(0.8));

            var easing = new QuinticEase();
            easing.EasingMode = EasingMode.EaseInOut;
            var da1 = new DoubleAnimation(0, start, dr);
            var da2 = new DoubleAnimation(end, 0, dr);

            da1.EasingFunction = easing;
            da2.EasingFunction = easing;
            var rt1 = new TranslateTransform();
            var rt2 = new TranslateTransform();
            item1.RenderTransform = rt1;
            item2.RenderTransform = rt2;

            if (state == 0)
            {
                state = 1;
                rt1.BeginAnimation(TranslateTransform.XProperty, da1);
                rt2.BeginAnimation(TranslateTransform.XProperty, da2);
            }
            else
            {
                state = 0;
                rt1.BeginAnimation(TranslateTransform.XProperty, da2);
                rt2.BeginAnimation(TranslateTransform.XProperty, da1);
            }
        }

        void anigoY(double start, double end)
        {
            var dr = new Duration(TimeSpan.FromSeconds(0.8));

            var easing = new QuinticEase();
            easing.EasingMode = EasingMode.EaseInOut;
            var da1 = new DoubleAnimation(0, start, dr);
            var da2 = new DoubleAnimation(end, 0, dr);

            da1.EasingFunction = easing;
            da2.EasingFunction = easing;
            var rt1 = new TranslateTransform();
            var rt2 = new TranslateTransform();
            item1.RenderTransform = rt1;
            item2.RenderTransform = rt2;

            if (state == 0)
            {
                state = 1;
                rt1.BeginAnimation(TranslateTransform.YProperty, da1);
                rt2.BeginAnimation(TranslateTransform.YProperty, da2);
            }
            else
            {
                state = 0;
                rt1.BeginAnimation(TranslateTransform.YProperty, da2);
                rt2.BeginAnimation(TranslateTransform.YProperty, da1);
            }
        }

        public System.Windows.Media.Brush gb
        {
            get
            {
                return this.Background;
            }
            set
            {
                this.Background = value;
            }
        }



        public double h { 
            get {
                return this.ActualHeight; 
            }
            set {
                this.Height = value;
            }
        }
        public double w
        {
            get
            {
                return this.ActualWidth;
            }
            set
            {
                this.Width = value;
            }
        }

        public void Dispose()
        {
            if (timer != null) { 
            timer.Tick -= timer_Tick;
            }
        }
    }
}
