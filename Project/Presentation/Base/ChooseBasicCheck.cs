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
    public partial class ChooseBasicCheck : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
        Business.Sys.BusinessUserInfo user = new project.Business.Sys.BusinessUserInfo();
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
                        list = createList(string.Empty);
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
        private string createList(string Name)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            if (Request.QueryString["type"] == "user")
            {
                sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                sb.Append("<thead>");
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<th width='10%'>勾选</th>");
                sb.Append("<th width='30%'>用户编号</th>");
                sb.Append("<th width='60%'>用户名称</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");

                sb.Append("<tbody>");
                Business.Sys.BusinessUserInfo pt = new project.Business.Sys.BusinessUserInfo();
                foreach (Entity.Sys.EntityUserInfo it in pt.GetUserInfoListQuery(string.Empty, Name))
                {
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<td align='center'><input type='checkbox' name='chk' id='" + it.UserNo + "' value='" + it.UserName + "' /></td>");
                    sb.Append("<td style='white-space: nowrap;'>" + it.UserNo + "</td>");
                    sb.Append("<td style='white-space: nowrap;'>" + it.UserName + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table>");
            }
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
                collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Name"))));
            }
            catch
            { isok = "0"; }
            collection.Add(new JsonStringValue("flag", isok));

            return collection.ToString();
        }

    }
}