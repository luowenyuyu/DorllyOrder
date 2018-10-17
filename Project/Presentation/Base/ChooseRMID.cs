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
    public partial class ChooseRMID : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                        string LOCNo = string.Empty;
                        if (Request.QueryString["LOCNo"] != null)
                            LOCNo = Request.QueryString["LOCNo"].ToString();

                        list = createList(string.Empty, string.Empty, string.Empty, LOCNo, string.Empty,  string.Empty, 1);

                        Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        RMLOCNo1Str += "<select id=\"RMLOCNo1\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        RMLOCNo1Str += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        {
                            RMLOCNo1Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        }
                        RMLOCNo1Str += "</select>";
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
        protected string RMLOCNo1Str = "";
        private string createList(string RMLOCNo1, string RMLOCNo2, string RMLOCNo3, string RMLOCNo4, string RMID, string CustName, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            try
            {
                sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                sb.Append("<thead>");
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<th width='30%'>房间编号</th>");
                sb.Append("<th width='70%'>当前客户</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                sb.Append("<tbody>");
                Business.Base.BusinessRoom pt = new project.Business.Base.BusinessRoom();
                foreach (Entity.Base.EntityRoom it in pt.GetListQuery(RMID, RMLOCNo1, RMLOCNo2, RMLOCNo3, RMLOCNo4, string.Empty, CustName, string.Empty, false, page, 15))
                {
                    string guid = Guid.NewGuid().ToString().Substring(0,6);
                    sb.Append("<tr class=\"text-c\" id='" + guid + "' onclick='submit(\"" + it.RMID + "\",\"" + guid + "\")'>");
                    sb.Append("<td style='white-space: nowrap;'>" + it.RMID + "<input type='hidden' id='it" + guid + "' value='" + it.RMID + "' /></td>");
                    sb.Append("<td style='white-space: nowrap;'>" + it.RMCurrentCustName + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append(Paginat(pt.GetListCount(RMID, RMLOCNo1, RMLOCNo2, RMLOCNo3, RMLOCNo4, string.Empty, CustName, string.Empty, false), 15, page, 5));
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
                collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMLOCNo1"), jp.getValue("RMLOCNo2"),
                    jp.getValue("RMLOCNo3"), jp.getValue("RMLOCNo4"), jp.getValue("RMID"), jp.getValue("CustName"), int.Parse(jp.getValue("page")))));
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