using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.Json;

namespace project.Presentation.Base
{
    public partial class ChooseMeter : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
        Business.Sys.BusinessUserInfo user = new project.Business.Sys.BusinessUserInfo();
        Data obj = new Data();
        protected string userid = "";
        protected string id = "";
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie hc = getCookie("1");
                if (hc != null)
                {
                    string str = hc.Value.Replace("%3D", "=");
                    userid = Encrypt.DecryptDES(str, "1");
                    user.load(userid);
                    id = Request.QueryString["id"].ToString();

                    if (!Page.IsCallback)
                    {
                        RMID = Request.QueryString["RMID"].ToString();
                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, RMID, 1);

                        Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        MeterLOCNo1Str += "<select id=\"MeterLOCNo1\" class=\"input-text required size-MINI\" style=\"width:90px\">";
                        MeterLOCNo1Str += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        {
                            MeterLOCNo1Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        }
                        MeterLOCNo1Str += "</select>";

                    }
                }
                else
                    GotoErrorPage();
            }
            catch
            {
                GotoErrorPage();
            }
        }

        protected string list = "";
        protected string RMID = "";
        protected string MeterLOCNo1Str = "";
        private string createList(string MeterLOCNo1, string MeterLOCNo2, string MeterLOCNo3, string MeterLOCNo4,string RMID, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            try
            {
                sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                sb.Append("<thead>");
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<th width='19%'>表记编号</th>");
                sb.Append("<th width='22%'>表记名称</th>");
                sb.Append("<th width='32%'>园区/建设期/楼栋/楼层</th>");
                sb.Append("<th width='27%'>房间编号</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                string meterType = string.Empty;
                if (Request.QueryString["MeterType"] != null)
                    meterType = Request.QueryString["MeterType"].ToString();

                sb.Append("<tbody>");
                Business.Base.BusinessMeter pt = new project.Business.Base.BusinessMeter();
                foreach (Entity.Base.EntityMeter it in pt.GetListQuery(MeterLOCNo1, MeterLOCNo2, MeterLOCNo3, MeterLOCNo4, string.Empty, meterType, string.Empty, RMID, "", string.Empty, "open", page, 15))
                {
                    sb.Append("<tr class=\"text-c\" id='" + it.MeterNo + "' onclick='submit(\"" + it.MeterNo + "\")'>");
                    sb.Append("<td style='white-space: nowrap;'>" + it.MeterNo + "<input type='hidden' id='it" + it.MeterNo + "' value='" + it.MeterName + "' /></td>");
                    sb.Append("<td style='white-space: nowrap;'>" + it.MeterName + "</td>");
                    sb.Append("<td>" + it.MeterLOCNo1Name + "/" + it.MeterLOCNo2Name + "/" + it.MeterLOCNo3Name + "/" + it.MeterLOCNo4Name + "</td>");
                    sb.Append("<td>" + it.MeterRMID + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append(Paginat(pt.GetListCount(MeterLOCNo1, MeterLOCNo2, MeterLOCNo3, MeterLOCNo4, string.Empty, meterType, string.Empty, RMID, "", string.Empty, "open"), 15, page, 5));
            }
            catch { }
            return sb.ToString();
        }
        /// <summary>
        /// 服务器端ajax调用响应请求方法
        /// </summary>
        /// <param name="eventArgument">客户端回调参数</param>

        void System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            this._clientArgument = eventArgument;
        }
        private string _clientArgument = "";

        string System.Web.UI.ICallbackEventHandler.GetCallbackResult()
        {
            string result = "";
            JsonArrayParse jp = new JsonArrayParse(this._clientArgument);
            if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "getvalue")
                result = getvalueaction(jp);
            return result;
        }

        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string isok = "1";
            try
            {
                collection.Add(new JsonStringValue("type", "select"));
                collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MeterLOCNo1"), jp.getValue("MeterLOCNo2"), jp.getValue("MeterLOCNo3"), jp.getValue("MeterLOCNo4"), jp.getValue("RMID"), int.Parse(jp.getValue("page")))));
            }
            catch
            { isok = "0"; }
            collection.Add(new JsonStringValue("flag", isok));

            return collection.ToString();
        }

        private string getvalueaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string subtype = "";
            try
            {
                int row = 0;
                Business.Base.BusinessLocation bt = new Business.Base.BusinessLocation();
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, jp.getValue("parent")))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }

                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));
                collection.Add(new JsonStringValue("child", jp.getValue("child")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getvalue"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
    }
}