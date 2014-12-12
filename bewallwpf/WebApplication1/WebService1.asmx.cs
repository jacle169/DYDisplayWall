using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        static Random ran = new Random();

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void GetWeather()
        {
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(
                new weather() { type = (em_weather)ran.Next(0, 4), temp = ran.Next(-30, 50), hum = ran.Next(0, 100) }));
            Context.Response.End();
        }

        static int ni = 0;
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void GetNews()
        {
            Context.Response.ContentType = "application/json";
            if (ni == 0)
            {
                ni = 1;
                Context.Response.Write(JsonConvert.SerializeObject(CreateNewsHasImage()));
            }
            else
            {
                ni = 0;
                Context.Response.Write(JsonConvert.SerializeObject(CreateNewsNoImage()));
            }
            Context.Response.End();
        }

        static int ad = 0;
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void GetAd()
        {
            Context.Response.ContentType = "application/json";
            if (ad == 0)
            {
                ad = 1;
                Context.Response.Write(JsonConvert.SerializeObject(
                new imgobj() { url = "http://d.lanrentuku.com/down/png/0904/18Boxes/Zip.png" }));
            }
            else
            {
                ad = 0;
                Context.Response.Write(JsonConvert.SerializeObject(
                new imgobj() { url = "http://img.sj33.cn/uploads/allimg/201012/2010122009080699.png" }));

            }
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void GetPies()
        {
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(randerPie()));
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void GetBar()
        {
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(randerCoollection()));
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void GetLine()
        {
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(randerCoollection()));
            Context.Response.End();
        }

        List<barOrLineData> randerCoollection()
        {
            var data = new List<barOrLineData>();
            data.Add(randerBarData("IE", true));
            data.Add(randerBarData("FF", true));
            data.Add(randerBarData("谷歌", true));
            return data;
        }

        barOrLineData randerBarData(string title, bool showlable)
        {
            var obj = new barOrLineData();
            obj.title = title;
            obj.showlable = showlable;
            obj.data = randerBar();
            return obj;
        }

        List<jacData> randerBar()
        {
            var list = new List<jacData>();
            for (int i = 1; i <= 12; i++)
            {
                list.Add(new jacData(i.ToString() + "月", ran.Next(0, 200)));
            }
            return list;
        }

        List<jacData> randerPie()
        {
            var list = new List<jacData>();
            list.Add(new jacData("谷歌", 20));
            list.Add(new jacData("FF", 30));
            list.Add(new jacData("IE", 40));
            list.Add(new jacData("其他", 10));
            return list;
        }

        public News CreateNewsHasImage()
        {
            var n = new News();
            n.hasImage = true;
            n.Title = "国家领导人将出席南京大屠杀公祭日仪式";
            n.Context = "新华网北京12月11日电 今年12月13日是首个南京大屠杀死难者国家公祭日。当天上午，党和国家领导人将出席在侵华日军南京大屠杀遇难同胞纪念馆举行的国家公祭仪式。";
            n.Date = DateTime.Now.ToShortDateString();
            n.imageUrl = "http://p7.qhimg.com/dmt/235_350_/t0107612c53b59c30e7.jpg";
            return n;
        }

        public News CreateNewsNoImage()
        {
            var n = new News();
            n.hasImage = false;
            n.Title = "李克强周日启程出访欧亚三国";
            n.Context = "本台消息：应哈萨克斯坦共和国总理马西莫夫、塞尔维亚共和国总理武契奇、泰王国总理巴育邀请，国务院总理李克强将于12月14日至20日对哈萨克斯坦进行正式访问并举行中哈总理第二次定期会晤、出席上海合作组织成员国政府首脑理事会第十三次会议，出席在塞尔维亚举行的第三次中国—中东欧国家领导人会晤并对塞尔维亚进行正式访问，赴泰国出席大湄公河次区域经济合作第五次领导人会议。";
            n.Date = DateTime.Now.ToShortDateString();
            return n;
        }
    }

    public class barOrLineData
    {
        public string title { get; set; }
        public bool showlable { get; set; }
        public List<jacData> data { get; set; }
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

    public enum em_weather
    {
        阳光明媚, 有雪, 阴天, 雨, 雷雨
    }

    public class imgobj
    {
        public string url { get; set; }
    }
}
