using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace bewallwpf
{
    public class appInfo
    {
        public double mainHeigth { get; set; }
        public double mianWidth { get; set; }
        public double groupHeigth { get; set; }
        public double groupWidth { get; set; }

        public List<tilegroup> tileGroups { get; set; }
    }

    public class tilegroup
    {
        public string groupName { get; set; }
        public List<tileInfo> tiles { get; set; }
    }

    public class tileInfo
    {
        public double height { get; set; }
        public double width { get; set; }
        public string backColor { get; set; }
        public int margin { get; set; }
        public string tileName { get; set; }
        public int animation { get; set; }
        public int refreshSeconds { get; set; }
        public em_skin skin { get; set; }
        public string refreshUrl { get; set; }
    }

    public enum em_weather
    {
        阳光明媚, 有雪, 阴天, 雨, 雷雨
    }

    public class barOrLineData
    {
        public string title { get; set; }
        public bool showlable { get; set; }
        public List<jacData> data { get; set; }
    }

    public class News
    {
        public bool hasImage { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string Date { get; set; }
        public string imageUrl { get; set; }
    }

    public class weather
    {
        //天气类型
        public em_weather type { get; set; }
        //温度
        public double temp { get; set; }
        //湿度
        public int hum { get; set; }
    }

    public class jacData
    {
        public jacData(string key, double val)
        {
            this._key = key;
            this._val = val;
        }

        private string _key;
        private double _val;

        public string Key
        {
            get
            {
                return this._key;
            }
        }

        public double Val
        {
            get
            {
                return this._val;
            }
        }
    }

    public enum em_TileAni
    {
        t_Tile动画, t_Tile内部动画
    }

    public enum em_TileType
    { 
       big,sm
    }

    public class imgobj
    {
        public string url { get; set; }
    }

    public enum em_skin
    {
        weather, news, ad, datetime, pie, bar, line
    }

    public class other
    {
        static other myother;
        public static other getOther()
        {
            if (myother == null)
            {
                myother = new other();
            }
            return myother;
        }

        internal Random ran = new Random();
        internal List<Brush> GetBrushs()
        {
            return new List<Brush>(){
                         (Brush)new BrushConverter().ConvertFromString("#FFF3B200"),
        (Brush)new BrushConverter().ConvertFromString("#FF77B900"),
         (Brush)new BrushConverter().ConvertFromString("#FF2572EB"),
        (Brush)new BrushConverter().ConvertFromString("#FFAD103C"),
         (Brush)new BrushConverter().ConvertFromString("#FF632F00"),
         (Brush)new BrushConverter().ConvertFromString("#FFB01E00"),
         (Brush)new BrushConverter().ConvertFromString("#FFC1004F"),
         
          (Brush)new BrushConverter().ConvertFromString("#FF7200AC"),
           (Brush)new BrushConverter().ConvertFromString("#FF4617B4"),
            (Brush)new BrushConverter().ConvertFromString("#FF006AC1"),
             (Brush)new BrushConverter().ConvertFromString("#FF008287"),
              (Brush)new BrushConverter().ConvertFromString("#FF199900"),
               (Brush)new BrushConverter().ConvertFromString("#FF00C13F"),
                (Brush)new BrushConverter().ConvertFromString("#FFFF981D"),
                 (Brush)new BrushConverter().ConvertFromString("#FFFF2E12"),
                  (Brush)new BrushConverter().ConvertFromString("#FFFF1D77"),
                   (Brush)new BrushConverter().ConvertFromString("#FFAA40FF"),
                    (Brush)new BrushConverter().ConvertFromString("#FF1FAEFF"),
                     (Brush)new BrushConverter().ConvertFromString("#FF56C5FF"),
                      (Brush)new BrushConverter().ConvertFromString("#FF00D8CC"),
                       (Brush)new BrushConverter().ConvertFromString("#FF91D100"),
                        (Brush)new BrushConverter().ConvertFromString("#FFE1B700"),
                         (Brush)new BrushConverter().ConvertFromString("#FFFF76BC"),
                          (Brush)new BrushConverter().ConvertFromString("#FF00A3A3"),
                           (Brush)new BrushConverter().ConvertFromString("#FFFE7C22")

            };
        }

        public string GetCurrentPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +@"\";
        }

        //多屏处理
        //int totaly = 0;
        //int totalx = 0;
        //foreach (var item in other.getOther().GetDisplays())
        //{
        //    totalx += int.Parse(item.ScreenWidth);
        //    totaly += int.Parse(item.ScreenHeight);
        //}

        //this.Height = totaly;
        //this.Width = totalx;

        delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);

        [DllImport("user32.dll")]
        static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);
        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hmon, ref MonitorInfo mi);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MonitorInfo
        {
            public uint size;
            public Rect monitor;
            public Rect work;
            public uint flags;
        }

        /// <summary>
        /// The struct that contains the display information
        /// </summary>
        public class DisplayInfo
        {
            public string Availability { get; set; }
            public string ScreenHeight { get; set; }
            public string ScreenWidth { get; set; }
            public Rect MonitorArea { get; set; }
            public Rect WorkArea { get; set; }
        }

        /// <summary>
        /// Collection of display information
        /// </summary>
        public class DisplayInfoCollection : List<DisplayInfo>
        {
        }

        /// <summary>
        /// Returns the number of Displays using the Win32 functions
        /// </summary>
        /// <returns>collection of Display Info</returns>
        public DisplayInfoCollection GetDisplays()
        {
            DisplayInfoCollection col = new DisplayInfoCollection();

            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                delegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData)
                {
                    MonitorInfo mi = new MonitorInfo();
                    mi.size = (uint)Marshal.SizeOf(mi);
                    bool success = GetMonitorInfo(hMonitor, ref mi);
                    if (success)
                    {
                        DisplayInfo di = new DisplayInfo();
                        di.ScreenWidth = (mi.monitor.right - mi.monitor.left).ToString();
                        di.ScreenHeight = (mi.monitor.bottom - mi.monitor.top).ToString();
                        di.MonitorArea = mi.monitor;
                        di.WorkArea = mi.work;
                        di.Availability = mi.flags.ToString();
                        col.Add(di);
                    }
                    return true;
                }, IntPtr.Zero);
            return col;
        }

    }

    public class c_背景图
    {
        public List<string> c_文件名 { get; set; }
        public int c_显示时间 { get; set; }
    }

}
