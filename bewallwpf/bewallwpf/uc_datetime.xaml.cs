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
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Gauge;

namespace bewallwpf
{
    /// <summary>
    /// Interaction logic for uc_datetime.xaml
    /// </summary>
    public partial class uc_datetime : UserControl, IDisposable
    {
        public uc_datetime()
        {
            InitializeComponent();
            Loaded += uc_datetime_Loaded;
        }

        DispatcherTimer timer;
        void uc_datetime_Loaded(object sender, RoutedEventArgs e)
        {
            SecondsNeedle.Background = point.Background = new SolidColorBrush(Windows8Palette.Palette.StrongColor);
            timer_Tick(null, null);
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var mydate = DateTime.Now;

            if (mydate.Hour <= 12)
            {
                radis.SweepAngle = -360;
            }
            else
            {
                radis.SweepAngle = 360;
            }

            this.SecondsNeedle.Value = mydate.Second;
            this.MinutesNeedle.Value = mydate.Minute;
            this.HoursNeedle.Value = Math.Abs(12 - mydate.Hour);
            this.timeText.Text = mydate.ToString("HH:mm:ss");
            this.dateText.Text = mydate.ToString("MM月 dd日, yyyy年").ToUpper();
            this.dayText.Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(mydate.DayOfWeek);
        }

        public void Dispose()
        {
            timer.Tick -= timer_Tick;
        }
    }
}
