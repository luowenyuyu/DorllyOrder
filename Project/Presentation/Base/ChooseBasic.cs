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
    public partial class ChooseBasic : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
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
                    userid = Encrypt.DecryptDES(str,"1");
                    user.load(userid);
                    id = Request.QueryString["id"].ToString();

                    if (!Page.IsCallback)
                        list = createList(null, 1);
                }
                else
                    GotoErrorPage();
            }
            catch
            {
                GotoErrorPage();
            }
        }

        Business.Sys.BusinessUserInfo user = new project.Business.Sys.BusinessUserInfo();
        Data obj = new Data();
        protected string list = "";
        private string createList(string Name, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            try
            {
                if (Request.QueryString["type"] == "user")
                {
                    sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                    sb.Append("<thead>");
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<th width='35%'>用户编号</th>");
                    sb.Append("<th width='65%'>用户名称</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    sb.Append("<tbody>");
                    Business.Sys.BusinessUserInfo pt = new project.Business.Sys.BusinessUserInfo();
                    foreach (Entity.Sys.EntityUserInfo it in pt.GetUserInfoListQuery(string.Empty, Name, page, 15))
                    {
                        sb.Append("<tr class=\"text-c\" id='" + it.UserNo + "' onclick='submit(\"" + it.UserNo + "\")'>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.UserNo + "<input type='hidden' id='it" + it.UserNo + "' value='" + it.UserName + "' /></td>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.UserName + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append(Paginat(pt.GetUserInfoListCount(string.Empty, Name), 15, page, 5));
                }
                else if (Request.QueryString["type"] == "SRVType")
                {
                    sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                    sb.Append("<thead>");
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<th width='35%'>服务类型编号</th>");
                    sb.Append("<th width='65%'>服务类型名称</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    sb.Append("<tbody>");
                    Business.Base.BusinessServiceType pt = new project.Business.Base.BusinessServiceType();
                    foreach (Entity.Base.EntityServiceType it in pt.GetListQuery(string.Empty, Name,  "null", string.Empty, true, page, 15))
                    {
                        sb.Append("<tr class=\"text-c\" id='" + it.SRVTypeNo + "' onclick='submit(\"" + it.SRVTypeNo + "\")'>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.SRVTypeNo + "<input type='hidden' id='it" + it.SRVTypeNo + "' value='" + it.SRVTypeName + "' /></td>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.SRVTypeName + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append(Paginat(pt.GetListCount(string.Empty, Name, "null", string.Empty, true), 15, page, 5));
                }
                else if (Request.QueryString["type"] == "LOC")
                {
                    sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                    sb.Append("<thead>");
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<th width='35%'>编号</th>");
                    sb.Append("<th width='65%'>名称</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    sb.Append("<tbody>");
                    Business.Base.BusinessLocation pt = new project.Business.Base.BusinessLocation();
                    foreach (Entity.Base.EntityLocation it in pt.GetListQuery(string.Empty, Name, string.Empty, page, 15))
                    {
                        string spacestr = "";
                        int row = 1;
                        while (row <= it.LOCLevel)
                        {
                            spacestr += "&nbsp;&nbsp;&nbsp;&nbsp;";
                            row++;
                        }

                        sb.Append("<tr class=\"text-c\" id='" + it.LOCNo + "' onclick='submit(\"" + it.LOCNo + "\")'>");
                        sb.Append("<td style=\"white-space: nowrap;text-align:left;\">" + it.LOCNo + "<input type='hidden' id='it" + it.LOCNo + "' value='" + it.LOCName + "' /></td>");
                        sb.Append("<td style=\"white-space: nowrap;text-align:left;\">" + spacestr + it.LOCName + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append(Paginat(pt.GetListCount(string.Empty, Name, string.Empty), 15, page, 5));
                }
                else if (Request.QueryString["type"] == "Cust")
                {
                    sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                    sb.Append("<thead>");
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<th width='35%'>客户编号</th>");
                    sb.Append("<th width='65%'>客户名称</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    sb.Append("<tbody>");
                    Business.Base.BusinessCustomer pt = new project.Business.Base.BusinessCustomer();
                    foreach (Entity.Base.EntityCustomer it in pt.GetListQuery(string.Empty, Name, string.Empty, string.Empty, page, 15))
                    {
                        sb.Append("<tr class=\"text-c\" id='" + it.CustNo + "' onclick='submit(\"" + it.CustNo + "\")'>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.CustNo + "<input type='hidden' id='it" + it.CustNo + "' value='" + it.CustName + "' /></td>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.CustName + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append(Paginat(pt.GetListCount(string.Empty, Name, string.Empty, string.Empty), 15, page, 5));
                }
                else if (Request.QueryString["type"] == "Billboard")
                {
                    sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
                    sb.Append("<thead>");
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<th width='20%'>广告位编号</th>");
                    sb.Append("<th width='25%'>广告位名称</th>");
                    sb.Append("<th width='40%'>位置</th>");
                    sb.Append("<th width='15%'>广告位类型</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    sb.Append("<tbody>");
                    Business.Base.BusinessBillboard pt = new project.Business.Base.BusinessBillboard();
                    foreach (Entity.Base.EntityBillboard it in pt.GetListQuery(string.Empty, Name, string.Empty, string.Empty,string.Empty ,string.Empty, page, 15))
                    {
                        sb.Append("<tr class=\"text-c\" id='" + it.BBNo + "' onclick='submit(\"" + it.BBNo + "\")'>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.BBNo + "<input type='hidden' id='it" + it.BBNo + "' value='" + it.BBName + "' /></td>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.BBName + "</td>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.BBAddr + "</td>");
                        sb.Append("<td style='white-space: nowrap;'>" + it.BBTypeName + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    sb.Append(Paginat(pt.GetListCount(string.Empty, Name, string.Empty, string.Empty, string.Empty, string.Empty), 15, page, 5));
                }
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
                collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Name"), int.Parse(jp.getValue("page")))));
            }
            catch
            { isok = "0"; }
            collection.Add(new JsonStringValue("flag", isok));

            return collection.ToString();
        }

    }
}