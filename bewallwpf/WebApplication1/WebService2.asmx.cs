using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService2 : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void GetAppInfo()
        {
            appInfo app = new appInfo();
            app.mainHeigth = 1080;
            app.mianWidth = 1920;
            app.groupHeigth = 1080;
            app.groupWidth = 500;
            var gps = new tilegroup();
            gps.tiles =CreateTile1();
            app.tileGroups.Add(gps);
            var gps1 = new tilegroup();
            gps1.tiles = CreateTile2();
            app.tileGroups.Add(gps1);
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(app));
            Context.Response.End();
        }

        List<tileInfo> CreateTile1()
        {
            var tiles = new List<tileInfo>();
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FFF3B200", margin = 5, tileName = "gt1", refreshSeconds=5, animation = 1, skin= em_skin.line, refreshUrl ="http://localhost:37265/WebService1.asmx/GetLine" });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FF77B900", margin = 5, tileName = "gt2", refreshSeconds = 3, animation =2, skin = em_skin.datetime });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FF2572EB", margin = 5, tileName = "gt3", refreshSeconds = 15, animation = 13, skin = em_skin.ad, refreshUrl = "http://localhost:37265/WebService1.asmx/GetAd" });
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FFAD103C", margin = 5, tileName = "gt4", refreshSeconds = 7, animation = 4, skin = em_skin.datetime });
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FF632F00", margin = 5, tileName = "gt5", refreshSeconds = 25, animation = 5, skin = em_skin.bar, refreshUrl = "http://localhost:37265/WebService1.asmx/GetBar" });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FFB01E00", margin = 5, tileName = "gt6", refreshSeconds = 30, animation = 6, skin = em_skin.news, refreshUrl = "http://localhost:37265/WebService1.asmx/GetNews" });
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FFC1004F", margin = 5, tileName = "gt7", refreshSeconds = 35, animation = 7, skin = em_skin.pie, refreshUrl = "http://localhost:37265/WebService1.asmx/GetPies" });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FF7200AC", margin = 5, tileName = "gt8", refreshSeconds = 40, animation = 8, skin = em_skin.weather, refreshUrl = "http://localhost:37265/WebService1.asmx/GetWeather" });
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FF4617B4", margin = 5, tileName = "gt9", refreshSeconds = 50, animation = 9, skin = em_skin.news, refreshUrl = "http://localhost:37265/WebService1.asmx/GetNews" });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FF006AC1", margin = 5, tileName = "gt10", refreshSeconds = 55, animation = 10, skin = em_skin.pie, refreshUrl = "http://localhost:37265/WebService1.asmx/GetPies" });
            return tiles;
        }

        List<tileInfo> CreateTile2()
        {
            var tiles = new List<tileInfo>();
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FF008287", margin = 5, tileName = "gt11", refreshSeconds = 5, animation = 11, skin = em_skin.line, refreshUrl = "http://localhost:37265/WebService1.asmx/GetLine" });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FF199900", margin = 5, tileName = "gt12", refreshSeconds = 3, animation = 12, skin = em_skin.datetime });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FF00C13F", margin = 5, tileName = "gt13", refreshSeconds = 15, animation = 13, skin = em_skin.ad, refreshUrl = "http://localhost:37265/WebService1.asmx/GetAd" });
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FFFF981D", margin = 5, tileName = "gt14", refreshSeconds = 7, animation = 14, skin = em_skin.datetime });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FFFF2E12", margin = 5, tileName = "gt15", refreshSeconds = 25, animation = 15, skin = em_skin.bar, refreshUrl = "http://localhost:37265/WebService1.asmx/GetBar" });
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FFFF1D77", margin = 5, tileName = "gt16", refreshSeconds = 30, animation = 16, skin = em_skin.news, refreshUrl = "http://localhost:37265/WebService1.asmx/GetNews" });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FFAA40FF", margin = 5, tileName = "gt17", refreshSeconds = 35, animation = 7, skin = em_skin.pie, refreshUrl = "http://localhost:37265/WebService1.asmx/GetPies" });
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FF1FAEFF", margin = 5, tileName = "gt18", refreshSeconds = 40, animation = 8, skin = em_skin.weather, refreshUrl = "http://localhost:37265/WebService1.asmx/GetWeather" });
            tiles.Add(new tileInfo() { height = 150, width = 310, backColor = "#FF56C5FF", margin = 5, tileName = "gt19", refreshSeconds = 50, animation = 9, skin = em_skin.news, refreshUrl = "http://localhost:37265/WebService1.asmx/GetNews" });
            tiles.Add(new tileInfo() { height = 150, width = 150, backColor = "#FF00D8CC", margin = 5, tileName = "gt20", refreshSeconds = 55, animation = 10, skin = em_skin.pie, refreshUrl = "http://localhost:37265/WebService1.asmx/GetPies" });
            return tiles;
        }
    }

    public class appInfo
    {
        public appInfo()
        {
            tileGroups = new List<tilegroup>();
        }
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

    //天气，新闻，图片或幻灯，日期，饼形统计图， 柱形统计图，线形统计图
    public enum em_skin
    {
        weather, news, ad, datetime, pie, bar, line
    }

}
