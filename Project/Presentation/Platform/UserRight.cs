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

namespace project.Presentation.Platform
{
    public partial class UserRight : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie hc = getCookie("1");
                if (hc != null)
                {
                    string str = hc.Value.Replace("%3D", "=");
                    string userid = Encrypt.DecryptDES(str, "1");
                    user = new project.Business.Sys.BusinessUserInfo();
                    user.load(userid);
                    if (user.Entity.UserType.ToUpper() != "ADMIN") {
                        Response.Write(errorpage);
                        return;
                    }

                    if (!Page.IsCallback)
                    {
                        string firsttype = "";
                        TypeStr = "<select id='UserType' class='input-text' style='width:120px;'>";

                        Business.Sys.BusinessUserType bu = new project.Business.Sys.BusinessUserType();
                        foreach (Entity.Sys.EntityUserType it in bu.GetUserTypeListQuery(string.Empty, string.Empty))
                        {
                            if (it.UserTypeNo.ToUpper() == "ADMIN") continue;

                            if (firsttype == "")
                            {
                                firsttype = it.UserTypeNo;
                                TypeStr += "<option value='" + it.UserTypeNo + "' selected='selected'>" + it.UserTypeName + "</option>";
                            }
                            else
                                TypeStr += "<option value='" + it.UserTypeNo + "'>" + it.UserTypeName + "</option>";
                        }
                        TypeStr += "</select>";

                        list = createList(firsttype);
                    }
                }
                else
                {
                    Response.Write("<script type='text/javascript'>window.parent.window.location.href='../login.aspx';</script>");
                    return;
                }
            }
            catch
            {
                Response.Write("<script type='text/javascript'>window.parent.window.location.href='../login.aspx';</script>");
                return;
            }
        }

        int row = 0;
        Data obj = new Data();
        Business.Sys.BusinessUserInfo user = null;
        protected string list = "";
        protected string TypeStr = "";
        private string createList(string UserType)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width='5%'>序号</th>");
            sb.Append("<th width='10%'>是否权限</th>");
            sb.Append("<th width='25%'>菜单名称</th>");
            sb.Append("<th width='60%'>权限</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            sb.Append("<tbody id='ItemBody'>");
            Business.Sys.BusinessUserRight bc = new project.Business.Sys.BusinessUserRight();
            foreach (Entity.Sys.EntityUserInfoRights it in bc.GetUserRightInfo(UserType, "Sys"))
            {
                row++;
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td align='center'>" + row.ToString() + "</td>");
                sb.Append("<td align='center'><input type='checkbox' name='chkmenu' id='" + it.InnerEntityOID + "'" + (it.Right ? "checked='checked'" : "") + " /></td>");
                sb.Append("<td style='text-align:left'>" + (it.Flag == 1 ? "" : "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + it.MenuName + "</td>");

                sb.Append("<td style='text-align:left'>");
                int col = -1;
                foreach (string right in it.RightCode.Split(';'))
                {
                    col++;
                    if (right == "") continue;

                    if (it.HaveRightCode.IndexOf(right) >= 0)
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' checked='checked' id='" + it.InnerEntityOID + right + "' name='" + it.InnerEntityOID + "' value='" + right + "' />&nbsp;<label for='" + it.InnerEntityOID + right + "'>" + it.RightName.Split(';')[col].ToString() + "</label>");
                    else
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='" + it.InnerEntityOID + right + "' name='" + it.InnerEntityOID + "' value='" + right + "' />&nbsp;<label for='" + it.InnerEntityOID + right + "'>" + it.RightName.Split(';')[col].ToString() + "</label>");
                }
                sb.Append("<input type='hidden' id='parent" + it.InnerEntityOID + "' value='" + it.Parent + "'>");
                sb.Append("</td>");
                sb.Append("</tr>");
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
            if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "submit")
                result = submitaction(jp);
            return result;
        }

        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", "1"));
            collection.Add(new JsonStringValue("UserType", jp.getValue("UserType")));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("UserType"))));

            return collection.ToString();
        }

        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string isok = "1";
            string errrow = "";
            string errinfo = "";
            try
            {
                errrow = "保存出现错误！";
                obj.ExecuteNonQuery("delete from Sys_UserRight where UserType='" + jp.getValue("UserType") + "'");

                int col = -1;
                string jsonText = jp.getValue("ID");
                string items = jp.getValue("items");
                foreach (string str in jsonText.Split('@'))
                {
                    col++;
                    if (str == "") continue;
                    
                    Business.Sys.BusinessUserRight bc = new project.Business.Sys.BusinessUserRight();
                    bc.Entity.MenuID = str;
                    bc.Entity.UserType = jp.getValue("UserType");
                    bc.Entity.RightCode = items.Split('@')[col].ToString();
                    int row = bc.Save();
                    if (row < 1)
                    {
                        isok = "2";
                        errinfo += errrow + ";";
                    }
                    else
                        errrow = "";
                }

            }
            catch { isok = "2"; errinfo = errrow; }

            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", isok));
            collection.Add(new JsonStringValue("errinfo", errinfo));

            return collection.ToString();
        }


    }
}