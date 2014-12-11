using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TransitionControl;
using Telerik.Windows.Controls.TransitionEffects;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace bewallwpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //internal static WebServicePoxy.WebService1SoapClient ws;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            KeyDown += MainWindow_KeyDown;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            timer.Tick += timer_Tick;
        }

        int dd = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            dd += 1;
            if (dd == int.MaxValue)
            {
                dd = 0;
                return;
            }

            //加载背影图
            if (dd % bgs.c_显示时间 == 0)
            {
                BGAni();
            }

            //if (dd % 4 == 0) {
            //    (((wp_main.Children[0] as TileGroup).wp.Children[5] as RadTransitionControl).Content as Tile).ani_uptodown();
            //}
            //if (dd % 8 == 0)
            //{
            //    ((wp_main.Children[0] as TileGroup).wp.Children[0] as RadTransitionControl).Content
            //        = CreateTile(em_TileType.big, GetBrush()[ran.Next(0, 6)], 5, Guid.NewGuid().ToString(), 6);
            //    //((wp_main.Children[0] as TileGroup).wp.Children[6] as Tile).ani_downtoup();
            //}

            //if (dd % 12 == 0)
            //{
            //    ((wp_main.Children[0] as TileGroup).wp.Children[0] as Tile).ani_lefttoright();
            //}

            //if (dd % 8 == 0)
            //{
            //    ((wp_main.Children[1] as TileGroup).wp.Children[7] as Tile).ani_lefttorightf_mazing();
            //}

            //if (dd % 4 == 0)
            //{
            //    ((wp_main.Children[1] as TileGroup).wp.Children[0] as Tile).ani_downtoup_mazing();
            //}

            //if (dd % 12 == 0)
            //{
            //    ((wp_main.Children[2] as TileGroup).wp.Children[0] as Tile).ani_righttoleft();
            //}
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) { 
                App.Current.MainWindow.Close();
            }
            //else if (e.Key == Key.Z)
            //{
            //    var tile = loadcontext("test");
            //    if (tile != null)
            //    {
            //        tile.SetImage(ws.GetAd());
            //    }
            //}
            //else if (e.Key == Key.X)
            //{
            //    var tile = loadcontext("tt");
            //    if (tile != null)
            //    {
            //        tile.SetLinear(JsonConvert.DeserializeObject<List<barOrLineData>>(ws.GetBarOrLines()));
            //    }
            //    var tile1 = loadcontext("xx");
            //    if (tile1 != null)
            //    {
            //        tile1.SetBar(JsonConvert.DeserializeObject<List<barOrLineData>>(ws.GetBarOrLines()));
            //    }
            //    var tile2 = loadcontext("yy");
            //    if (tile2!= null)
            //    {
            //        //tile2.SetPie(JsonConvert.DeserializeObject<List<jacData>>(ws.GetPies()));
            //        tile2.SetPie();
            //    }
            //    var tile3 = loadcontext("kk");
            //    if (tile3 != null)
            //    {
            //        var obj = JsonConvert.DeserializeObject<News>(ws.GetNews());
            //        tile3.SetNews(obj);
            //    }
            //    var tile4 = loadcontext("jj");
            //    if (tile4 != null)
            //    {
            //        tile4.SetDatetime();
            //    }
            //    var tile5 = loadcontext("cc");
            //    if (tile5 != null)
            //    {
            //        var obj = JsonConvert.DeserializeObject<weather>(ws.GetWeather());
            //        tile5.SetWeather(obj.type, obj.temp, obj.hum);
            //    }
            //}
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            Windows8Palette.Palette.FontSizeXS = 10;
            Windows8Palette.Palette.FontSizeS = 11;
            Windows8Palette.Palette.FontSize = 12;
            Windows8Palette.Palette.FontSizeL = 14;
            Windows8Palette.Palette.FontSizeXL = 16;
            Windows8Palette.Palette.FontSizeXXL = 19;
            Windows8Palette.Palette.FontSizeXXXL = 24;
            Windows8Palette.Palette.FontFamily = new FontFamily("Microsoft YaHei");
            Windows8Palette.Palette.FontFamilyLight = new FontFamily("Microsoft YaHei Light");
            Windows8Palette.Palette.FontFamilyStrong = new FontFamily("Microsoft YaHei Semibold");

            #region 背景操作
            //测试时使用
            //写一个示例图影加载文件
            //writebgs();
            //初始化背影图
            bgs = loadbgs();
            bg2.Background = new ImageBrush(new BitmapImage(new Uri(
                other.getOther().GetCurrentPath() + bgs.c_文件名[bgindex], UriKind.Absolute)));
            #endregion

            //ws = new WebServicePoxy.WebService1SoapClient();

             string strdata = httpget(new Uri("http://localhost:37265/WebService2.asmx/GetAppInfo", UriKind.Absolute));
             if (!string.IsNullOrEmpty(strdata))
             {
                 var data = JsonConvert.DeserializeObject<appInfo>(strdata);

                 wp_main.Height = data.mainHeigth;
                 wp_main.Width = data.mianWidth;

                 foreach (var gp in data.tileGroups)
                 {
                     TileGroup tg = new TileGroup();
                     tg.wp.Height = data.groupHeigth;
                     tg.wp.Width = data.groupWidth;
                     var tiles = gettiles(gp.tiles);
                     foreach (var item in tiles)
                     {
                         tg.wp.Children.Add(item);
                     }
                     wp_main.Children.Add(tg);
                 }

                 //foreach (var item in wp_main.Children)
                 //{
                 //    var group = item as TileGroup;
                 //    foreach (var control in group.wp.Children)
                 //    {
                 //        var rad = control as RadTransitionControl;
                 //        var tile = rad.Content as Tile;
                 //        if (tile.tileType ==  em_skin.datetime)
                 //        {
                 //            tile.SetDatetime();
                 //        }
                 //    }
                 //}

                 //TileGroup tg1 = new TileGroup();
                 //foreach (var item in gettiles2())
                 //{
                 //    tg1.wp.Children.Add(item);
                 //}
                 //tg1.Width = groupWidth;
                 //wp_main.Children.Add(tg1);
                 //mainWpWidth += groupWidth;

                 //TileGroup tg2 = new TileGroup();
                 //foreach (var item in gettiles3())
                 //{
                 //    tg2.wp.Children.Add(item);
                 //}
                 //tg2.Width = groupWidth;
                 //wp_main.Children.Add(tg2);
                 //mainWpWidth += groupWidth;

                 //wp_main.Width = mainWpWidth;
             }

            //((wp_main.Children[2] as TileGroup).wp.Children[0] as Tile).item1.LayoutRoot.Children.RemoveAt(0);
            //((wp_main.Children[2] as TileGroup).wp.Children[0] as Tile).loadurl();
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

        #region 背景操作
        c_背景图 bgs;
        bool firstbg = true;
        int bgindex = 0;
        void BGAni()
        {
            if (bgindex < bgs.c_文件名.Count)
            {
                bgindex++;
            }
            if (bgindex == bgs.c_文件名.Count)
            {
                bgindex = 0;
            }
            if (firstbg)
            {
                firstbg = false;
                bg1.Background = new ImageBrush(new BitmapImage(new Uri(other.getOther().GetCurrentPath() + bgs.c_文件名[bgindex], UriKind.Absolute)));
                var da = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(2)));
                da.EasingFunction = new QuinticEase() { EasingMode = EasingMode.EaseIn };
                bg2.BeginAnimation(OpacityProperty, da);
            }
            else
            {
                firstbg = true;
                bg2.Background = new ImageBrush(new BitmapImage(new Uri(other.getOther().GetCurrentPath() + bgs.c_文件名[bgindex], UriKind.Absolute)));
                var da = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(2)));
                da.EasingFunction = new QuinticEase() { EasingMode = EasingMode.EaseIn };
                bg2.BeginAnimation(OpacityProperty, da);
            }
        }
        c_背景图 loadbgs()
        {
            return JsonConvert.DeserializeObject<c_背景图>(
                System.IO.File.ReadAllText(other.getOther().GetCurrentPath() + "bgs.json"));
        }
        void writebgs()
        {
            var c = new c_背景图();
            c.c_显示时间 = 30;
            var list = new List<string>(){
               @"images\1.png",
               @"images\2.png",
               @"images\3.png",
               @"images\4.png",
               @"images\5.png",
               @"images\6.png",
               @"images\7.png",
               @"images\8.png",
               @"images\9.png"
            };
            c.c_文件名 = list;
            System.IO.File.WriteAllText(other.getOther().GetCurrentPath() + "bgs.json", JsonConvert.SerializeObject(c));
        }
        #endregion



        Tile loadcontext(string TileName)
        {
            foreach (var item in wp_main.Children)
            {
                var group = item as TileGroup;
                foreach (var control in group.wp.Children)
                {
                    var rad = control as RadTransitionControl;
                    var tile = rad.Content as Tile;
                    if (tile.TileName == TileName)
                    {
                        //tile.SetContent(TileName);
                        return tile;
                    }
                }
            }
            return null;
        }

        List<RadTransitionControl> gettiles(List<tileInfo> data)
        {
            var tiles = new List<RadTransitionControl>();
            foreach (var item in data)
            {
                tiles.Add(CreateTile1(item.height, item.width, (Brush)new BrushConverter().ConvertFromString(item.backColor),
                    item.margin, item.tileName, item.animation, item.refreshUrl, item.refreshSeconds, item.skin));
            }
            return tiles;
        }

        //List<RadTransitionControl> gettiles2()
        //{
        //    var tiles = new List<RadTransitionControl>();
        //    tiles.Add(CreateTile(em_TileType.big, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 11));
        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 12));
        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 13));
        //    tiles.Add(CreateTile(em_TileType.big, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 14));
        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 15));
        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 16));

        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, "test", 16));

        //    return tiles;
        //}

        //List<RadTransitionControl> gettiles3()
        //{
        //    var tiles = new List<RadTransitionControl>();
        //    tiles.Add(CreateTile(em_TileType.big, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, "xx", 11));
        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 6));
        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 7));
        //    tiles.Add(CreateTile1(150,310, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, "yy", 10, "http://localhost:37265/WebService1.asmx/GetPies"));
        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 15));
        //    tiles.Add(CreateTile(em_TileType.sm, other.getOther().GetBrushs()[other.getOther().ran.Next(0, 24)], 5, Guid.NewGuid().ToString(), 16));

        //    //tiles.Add(CreateTile(em_TileType.sm, GetBrush()[ran.Next(0, 6)], 5, "test", 16));

        //    return tiles;
        //}

        internal RadTransitionControl CreateTile1(double ht, double wd, Brush bg, int margin, string tileName, int ani, string refreshUrl, int refresh, em_skin skin)
        {
            var rad = new RadTransitionControl();
            var tile = CreateTileCore1(ht, wd, bg, margin);
            tile.TileName = tileName;
            tile.refreshUrl = refreshUrl;
            tile.refreshSeconds = refresh;
            tile.tileType = skin;
            rad.Content = tile;
            if (ani <= 10)
            {
                rad.Tag = em_TileAni.t_Tile动画;
                rad.Transition = getEffes(ani);
            }
            else if (ani <= 16)
            {
                rad.Tag = em_TileAni.t_Tile内部动画;
                rad.Transition = null;
                tile.SetAni(ani);
            }
            else
            {
                MessageBox.Show("读出文件出错>>>(组件名：" + tileName + "，动画参数：" + ani.ToString() + ")不是合法的动画参数（1-16）");
            }
            return rad;
        }

        internal RadTransitionControl CreateTile(em_TileType type, Brush bg,int margin, string tileName, int ani)
        {
            var rad = new RadTransitionControl();
            var tile = CreateTileCore(type, bg, margin);
            tile.TileName = tileName;
            rad.Content = tile;
            if (ani <= 10) {
                rad.Tag = em_TileAni.t_Tile动画;
                rad.Transition = getEffes(ani);
            }
            else if (ani <= 16)
            {
                rad.Tag = em_TileAni.t_Tile内部动画;
                rad.Transition = null;
                tile.SetAni(ani);
            }
            else
            {
                MessageBox.Show("读出文件出错>>>(组件名：" + tileName + "，动画参数：" + ani.ToString() + ")不是合法的动画参数（1-16）");
            }
            return rad;
        }

        Tile CreateTileCore1(double ht, double wd, Brush bg, int margin)
        {
            var tile = new Tile();
            tile.h = ht;
            tile.w = wd;
            tile.Background = bg;
            tile.Margin = new Thickness(margin);
            return tile;
        }

        Tile CreateTileCore(em_TileType type, Brush bg, int margin)
        {
            var tile = new Tile();
            tile.h = 150;
            tile.Background = bg;
            tile.Margin = new Thickness(margin);
            if (type == em_TileType.big)
            {
                tile.w = 310;
            }
            else { tile.w = 150; }
            return tile;
        }

        TransitionProvider getEffes(int ani)
        {
            switch (ani)
            {
                case 1:
                    return effes1();
                case 2:
                    return effes2();
                case 3:
                    return effes3();
                case 4:
                    return effes4();
                case 5:
                    return effes5();
                case 6:
                    return effes6();
                case 7:
                    return effes7();
                case 8:
                    return effes8();
                case 9:
                    return effes9();
                case 10:
                    return effes10();
                default:
                    return null;
            }
        }

        TransitionProvider effes1()
        {
            var eff = new PerspectiveRotationTransition();
            eff.Direction = RotationDirection.Right;
            var es = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            eff.NewPlaneEasing = es;
            eff.OldPlaneEasing = es;
            return eff;
        }

        TransitionProvider effes2()
        {
            var eff = new PerspectiveRotationTransition();
            eff.Direction = RotationDirection.Left;
            var es = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            eff.NewPlaneCenterOfRotationX =0;
            eff.OldPlaneCenterOfRotationY=1;
            eff.NewPlaneEasing = es;
            eff.OldPlaneEasing = es;
            return eff;
        }

        TransitionProvider effes3()
        {
            var eff = new PerspectiveRotationTransition();
            eff.Direction = RotationDirection.Left;
            var es = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            eff.NewPlaneCenterOfRotationZ = 0.4;
            eff.OldPlaneCenterOfRotationZ = 0.4;
            eff.RotationLength=90;
            eff.NewPlaneEasing = es;
            eff.OldPlaneEasing = es;
            return eff;
        }

        TransitionProvider effes4()
        {
            var eff = new PerspectiveRotationTransition();
            eff.Direction = RotationDirection.Left;
            var es = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            eff.NewPlaneCenterOfRotationZ = -0.15;
            eff.OldPlaneCenterOfRotationZ = -0.15;
            eff.NewPlaneEasing = es;
            eff.OldPlaneEasing = es;
            return eff;
        }

        TransitionProvider effes5()
        {
            var eff = new PerspectiveRotationTransition();
            eff.Direction = RotationDirection.Right;
            var es = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            eff.NewPlaneCenterOfRotationZ = -0.25;
            eff.OldPlaneCenterOfRotationZ = -0.25;
            eff.NewPlaneEasing = es;
            eff.OldPlaneEasing = es;
            return eff;
        }

        TransitionProvider effes6()
        {
            return new FlipWarpTransition();
        }

        TransitionProvider effes7()
        {
            return new MotionBlurredZoomTransition();
        }

        TransitionProvider effes8()
        {
            return new PixelateTransition();
        }

        TransitionProvider effes9()
        {
            return new RollTransition();
        }

        TransitionProvider effes10()
        {
            return new WaveTransition();
        }



    }
}
