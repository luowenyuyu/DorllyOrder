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
    public partial class ChooseWPNo : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                        list = createList(1);
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
        private string createList(int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            try
            {
                sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                sb.Append("<thead>");
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<th width='30%'>工位编号</th>");
                sb.Append("<th width='70%'>工位人数</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                sb.Append("<tbody>");
                Business.Base.BusinessWorkPlace pt = new project.Business.Base.BusinessWorkPlace();
                foreach (Entity.Base.EntityWorkPlace it in pt.GetListQuery(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
                    Request.QueryString["RMID"].ToString(), string.Empty, page, 15))
                {
                    sb.Append("<tr class=\"text-c\" id='" + it.WPNo + "' onclick='submit(\"" + it.WPNo + "\")'>");
                    sb.Append("<td style='white-space: nowrap;'>" + it.WPNo + "<input type='hidden' id='it" + it.WPNo + "' value='" + it.WPNo+ "' /></td>");
                    sb.Append("<td style='white-space: nowrap;'>" + it.WPSeat.ToString() + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table>");
                sb.Append(Paginat(pt.GetListCount(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                    Request.QueryString["RMID"].ToString(), string.Empty), 15, page, 5));
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
            return result;
        }

        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string isok = "1";
            try
            {
                collection.Add(new JsonStringValue("type", "select"));
                collection.Add(new JsonStringValue("liststr", createList(int.Parse(jp.getValue("page")))));
            }
            catch
            { isok = "0"; }
            collection.Add(new JsonStringValue("flag", isok));

            return collection.ToString();
        }
    }
}