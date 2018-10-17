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
    public partial class Setting : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
        protected string userid = "";
        Business.Sys.BusinessUserInfo user = new project.Business.Sys.BusinessUserInfo();
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
                    CheckRight(user.Entity, "pm/Base/Setting.aspx");

                    if (!Page.IsCallback)
                    {
                        list = createList();
                    }
                }
                else
                {
                    Response.Write(errorpage);
                    return;
                }
            }
            catch
            {
                Response.Write(errorpage);
                return;
            }
        }

        Data obj = new Data();
        protected string list = "";
        private string createList()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='25%'>项目</th>");
            sb.Append("<th width='30%'>内容</th>");
            sb.Append("<th width='20%'>服务项目</th>");
            sb.Append("<th width='20%'>操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            
            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessSetting bc = new project.Business.Base.BusinessSetting();
            foreach (Entity.Base.EntitySetting it in bc.GetListQuery())
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.SettingCode + "\">");
                sb.Append("<td>" + r.ToString() + "</td>");
                sb.Append("<td>" + it.SettingName + "</td>");
                if (it.SettingType == "String")
                {
                    if (it.SettingCode == "CurrParkNo")
                        sb.Append("<td><input class=\"input-text size-S\" type=\"text\" id=\"Val" + it.SettingCode + "\" value=\"" + it.StringValue + "\" /></td>");
                    else
                    {
                        sb.Append("<td><select class=\"input-text size-S\" id=\"Val" + it.SettingCode + "\" />");
                        Business.Base.BusinessServiceProvider bs = new Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider it1 in bs.GetListQuery(string.Empty, string.Empty, true))
                        {
                            if (it.StringValue == it1.SPNo)
                                sb.Append("<option value=\"" + it1.SPNo + "\" selected=\"selected\">" + it1.SPName + "</option>");
                            else
                                sb.Append("<option value=\"" + it1.SPNo + "\">" + it1.SPName + "</option>");
                        }
                        sb.Append("</select></td>");
                    }
                }
                else if (it.SettingType == "Int")
                    sb.Append("<td><input class=\"input-text size-S\" type=\"text\" id=\"Val" + it.SettingCode + "\" value=\"" + it.IntValue.ToString() + "\" onblur=\"validInt(this.id)\" /></td>");
                else if (it.SettingType == "Decimal")
                    sb.Append("<td><input class=\"input-text size-S\" type=\"text\" id=\"Val" + it.SettingCode + "\" value=\"" + it.DecimalValue.ToString("0.####") + "\" onblur=\"validDecimal(this.id)\" /></td>");
                else
                    sb.Append("<td><input class=\"input-text size-S\" type=\"text\" id=\"Val" + it.SettingCode + "\" disabled=\"disabled\" /></td>");

                if (it.SettingType == "String")
                    sb.Append("<td></td>");
                else
                {
                    sb.Append("<td><select class=\"input-text size-S\" id=\"SRV" + it.SettingCode + "\" />");
                    Business.Base.BusinessService bs = new Business.Base.BusinessService();
                    foreach (Entity.Base.EntityService it1 in bs.GetListQuery(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty))
                    {
                        if (it.SRVNo == it1.SRVNo)
                            sb.Append("<option value=\"" + it1.SRVNo + "\" selected=\"selected\">" + it1.SRVName + "</option>");
                        else
                            sb.Append("<option value=\"" + it1.SRVNo + "\">" + it1.SRVName + "</option>");
                    }
                    sb.Append("</select></td>");
                }

                sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"save('" + it.SettingCode + "')\" value=\"&nbsp;保&nbsp;&nbsp;存&nbsp;\" /></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

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
            if (jp.getValue("Type") == "submit")
                result = submitaction(jp);
            return result;
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessSetting bc = new project.Business.Base.BusinessSetting();

                bc.load(jp.getValue("id"));
                if (bc.Entity.SettingType == "String")
                    bc.Entity.StringValue = jp.getValue("val");
                else if (bc.Entity.SettingType == "Int")
                    bc.Entity.IntValue = ParseIntForString(jp.getValue("val"));
                else if (bc.Entity.SettingType == "Decimal")
                    bc.Entity.DecimalValue = ParseDecimalForString(jp.getValue("val"));

                bc.Entity.SRVNo = jp.getValue("SRVval").Replace("undefined","");
                int r = bc.Save("update");

                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
    }
}