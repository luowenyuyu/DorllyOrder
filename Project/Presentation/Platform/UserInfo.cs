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
    public partial class UserInfo : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Platform/UserInfo.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Platform/UserType.aspx'";
                            DataTable dt = obj.PopulateDataSet(sqlstr).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                string rightCode = dt.Rows[0]["RightCode"].ToString();
                                if (rightCode.IndexOf("insert") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("update") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("delete") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("newpassword") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"newpassword()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 生成新密码</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("valid") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"valid()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 启用/停用</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"newpassword()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 生成新密码</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"valid()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 启用/停用</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty);

                        userType = "<select class=\"input-text required\" id=\"UserType\" data-valid=\"isNonEmpty\" data-error=\"请选择用户角色\">";
                        userType += "<option value=\"\" selected>请选择用户角色</option>";

                        Business.Sys.BusinessUserType tp = new project.Business.Sys.BusinessUserType();
                        foreach (Entity.Sys.EntityUserType it in tp.GetUserTypeListQuery(string.Empty, string.Empty))
                        {
                            userType += "<option value='" + it.UserTypeNo + "'>" + it.UserTypeName + "</option>";
                        }
                        userType += "</select>";
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

        Data obj = new Data();
        protected string list = "";
        protected string Buttons = "";
        protected string userType = "";
        private string createList(string DeptNo, string UserName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='15%'>用户ID</th>");
            sb.Append("<th width='20%'>用户姓名</th>");
            sb.Append("<th width='15%'>用户角色</th>");
            sb.Append("<th width='15%'>电话</th>");
            sb.Append("<th width='15%'>Email</th>");
            sb.Append("<th width='10%'>创建日期</th>");
            sb.Append("<th width='5%'>状态</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Sys.BusinessUserInfo bc = new project.Business.Sys.BusinessUserInfo();
            foreach (Entity.Sys.EntityUserInfo it in bc.GetUserInfoListQuery(string.Empty, UserName))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.InnerEntityOID + "\">");
                sb.Append("<td align='center'>" + r.ToString() + "</td>");
                sb.Append("<td>" + it.UserNo + "</td>");
                sb.Append("<td>" + it.UserName + "</td>");
                sb.Append("<td>" + it.UserTypeName + "</td>");
                sb.Append("<td>" + it.Tel + "</td>");
                sb.Append("<td>" + it.Email + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.RegDate) + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.Valid ? "label-success" : "") + " radius\">" + (it.Valid ? "有效" : "已失效") + "</span></td>");
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
            if (jp.getValue("Type") == "delete")
                result = deleteaction(jp);
            else if (jp.getValue("Type") == "update")
                result = updateaction(jp);
            else if (jp.getValue("Type") == "submit")
                result = submitaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "newpassword")
                result = newpasswordaction(jp);
            else if (jp.getValue("Type") == "valid")
                result = validaction(jp);
            return result;
        }

        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Sys.BusinessUserInfo bc = new project.Business.Sys.BusinessUserInfo();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("UserNo", bc.Entity.UserNo));
                collection.Add(new JsonStringValue("UserName", bc.Entity.UserName));
                collection.Add(new JsonStringValue("UserType", bc.Entity.UserType));
                collection.Add(new JsonStringValue("Tel", bc.Entity.Tel));
                collection.Add(new JsonStringValue("Email", bc.Entity.Email));
                collection.Add(new JsonStringValue("Addr", bc.Entity.Addr));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "update"));
            collection.Add(new JsonStringValue("flag", flag));

            result = collection.ToString();

            return result;
        }
        private string deleteaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Sys.BusinessUserInfo bc = new project.Business.Sys.BusinessUserInfo();
                bc.load(jp.getValue("id"));
                if (bc.Entity.UserNo.ToUpper() == "ADMIN")
                    flag = "3";
                else
                {
                    int r = bc.delete();
                    if (r <= 0)
                        flag = "2";
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(string.Empty, jp.getValue("UserNameS"))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Sys.BusinessUserInfo bc = new project.Business.Sys.BusinessUserInfo();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.UserName = jp.getValue("UserName");
                    bc.Entity.UserType = jp.getValue("UserType");
                    bc.Entity.Tel = jp.getValue("Tel");
                    bc.Entity.Addr = jp.getValue("Addr");
                    bc.Entity.Email = jp.getValue("Email");
                    int r = bc.Save();
                    if (r <= 0)
                        flag = "2";
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Sys_UserInfo where UserNo='" + jp.getValue("UserNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.UserNo = jp.getValue("UserNo");
                        bc.Entity.UserName = jp.getValue("UserName");
                        bc.Entity.UserType = jp.getValue("UserType");
                        bc.Entity.Tel = jp.getValue("Tel");
                        bc.Entity.Addr = jp.getValue("Addr");
                        bc.Entity.Email = jp.getValue("Email");
                        bc.Entity.Valid = true;
                        bc.Entity.RegDate=GetDate();
                        bc.Entity.Password = Encrypt.EncryptDES("123456", "1");
                        int r = bc.Save();
                        if (r <= 0)
                            flag = "2";
                    }
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(string.Empty, jp.getValue("UserNameS"))));

            return collection.ToString();
        }

        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(string.Empty, jp.getValue("UserNameS"))));

            return collection.ToString();
        }

        private string newpasswordaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Sys.BusinessUserInfo bc = new project.Business.Sys.BusinessUserInfo();
                bc.load(jp.getValue("id"));
                string newpwd = getRandom();

                bc.Entity.Password = Encrypt.EncryptDES(newpwd, "1");
                int r = bc.changepwd();
                if (r <= 0)
                    flag = "2";

                collection.Add(new JsonStringValue("type", "newpassword"));
                collection.Add(new JsonStringValue("flag", flag));
                collection.Add(new JsonStringValue("newpassword", newpwd));
            }
            catch
            { flag = "2"; }
            return collection.ToString();
        }

        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Sys.BusinessUserInfo bc = new project.Business.Sys.BusinessUserInfo();
                bc.load(jp.getValue("id"));
                bc.Entity.Valid = !bc.Entity.Valid;

                int r = bc.valid();
                if (r <= 0) flag = "2";
                if (bc.Entity.Valid)
                    collection.Add(new JsonStringValue("stat", "<span class=\"label label-success radius\">有效</span>"));
                else
                    collection.Add(new JsonStringValue("stat", "<span class=\"label radius\">已失效</span>"));
                collection.Add(new JsonStringValue("id", jp.getValue("id")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("type", "valid"));
            return collection.ToString();
        }
        
    }
}