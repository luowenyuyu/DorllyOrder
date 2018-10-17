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
    public partial class Customer : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/Customer.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/Customer.aspx'";
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
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty,1);
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
        protected string Buttons = "";
        private string createList(string CustNo, string CustName, string CustType, string CustStatus, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='8%'>客户编号</th>");
            sb.Append("<th width='19%'>客户名称</th>");
            sb.Append("<th width='19%'>客户简称</th>");
            sb.Append("<th width='6%'>客户类别</th>");
            sb.Append("<th width='6%'>单位法人</th>");
            sb.Append("<th width='6%'>客户联系人</th>");
            sb.Append("<th width='8%'>固定电话</th>");
            sb.Append("<th width='9%'>联系人手机号码</th>");
            sb.Append("<th width='9%'>电子邮箱</th>");
            sb.Append("<th width='6%'>状态</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessCustomer bc = new project.Business.Base.BusinessCustomer();
            foreach (Entity.Base.EntityCustomer it in bc.GetListQuery(CustNo, CustName, CustType, CustStatus,page,pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.CustNo + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.CustNo + "</td>");
                sb.Append("<td>" + it.CustName + "</td>");
                sb.Append("<td>" + it.CustShortName + "</td>");
                sb.Append("<td>" + it.CustType + "</td>");
                sb.Append("<td>" + it.Representative + "</td>");
                sb.Append("<td>" + it.CustContact + "</td>");
                sb.Append("<td>" + it.CustTel + "</td>");
                sb.Append("<td>" + it.CustContactMobile + "</td>");
                sb.Append("<td>" + it.CustEmail + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.CustStatus=="1" ? "label-success" : "") + " radius\">" + it.CustStatusName + "</span></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(CustNo, CustName, CustType, CustStatus), pageSize, page, 7));

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
            if (jp.getValue("Type") == "insert")
                result = insertaction(jp);
            else if (jp.getValue("Type") == "delete")
                result = deleteaction(jp);
            else if (jp.getValue("Type") == "update")
                result = updateaction(jp);
            else if (jp.getValue("Type") == "submit")
                result = submitaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            return result;
        }
        private string insertaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessCustomer bc = new project.Business.Base.BusinessCustomer();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("CustNo", bc.Entity.CustNo));
                collection.Add(new JsonStringValue("CustName", bc.Entity.CustName));
                collection.Add(new JsonStringValue("CustShortName", bc.Entity.CustShortName));
                collection.Add(new JsonStringValue("CustType", bc.Entity.CustType));
                collection.Add(new JsonStringValue("Representative", bc.Entity.Representative));
                collection.Add(new JsonStringValue("BusinessScope", bc.Entity.BusinessScope));
                collection.Add(new JsonStringValue("CustLicenseNo", bc.Entity.CustLicenseNo));
                collection.Add(new JsonStringValue("RepIDCard", bc.Entity.RepIDCard));
                collection.Add(new JsonStringValue("CustContact", bc.Entity.CustContact));
                collection.Add(new JsonStringValue("CustTel", bc.Entity.CustTel));
                collection.Add(new JsonStringValue("CustContactMobile", bc.Entity.CustContactMobile));
                collection.Add(new JsonStringValue("CustEmail", bc.Entity.CustEmail));
                collection.Add(new JsonStringValue("CustBankTitle", bc.Entity.CustBankTitle));
                collection.Add(new JsonStringValue("CustBankAccount", bc.Entity.CustBankAccount));
                collection.Add(new JsonStringValue("CustBank", bc.Entity.CustBank));
                collection.Add(new JsonStringValue("IsExternal", (bc.Entity.IsExternal ? "1" : "0")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "insert"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessCustomer bc = new project.Business.Base.BusinessCustomer();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("CustNo", bc.Entity.CustNo));
                collection.Add(new JsonStringValue("CustName", bc.Entity.CustName));
                collection.Add(new JsonStringValue("CustShortName", bc.Entity.CustShortName));
                collection.Add(new JsonStringValue("CustType", bc.Entity.CustType));
                collection.Add(new JsonStringValue("Representative", bc.Entity.Representative));
                collection.Add(new JsonStringValue("BusinessScope", bc.Entity.BusinessScope));
                collection.Add(new JsonStringValue("CustLicenseNo", bc.Entity.CustLicenseNo));
                collection.Add(new JsonStringValue("RepIDCard", bc.Entity.RepIDCard));
                collection.Add(new JsonStringValue("CustContact", bc.Entity.CustContact));
                collection.Add(new JsonStringValue("CustTel", bc.Entity.CustTel));
                collection.Add(new JsonStringValue("CustContactMobile", bc.Entity.CustContactMobile));
                collection.Add(new JsonStringValue("CustEmail", bc.Entity.CustEmail));
                collection.Add(new JsonStringValue("CustBankTitle", bc.Entity.CustBankTitle));
                collection.Add(new JsonStringValue("CustBankAccount", bc.Entity.CustBankAccount));
                collection.Add(new JsonStringValue("CustBank", bc.Entity.CustBank));
                collection.Add(new JsonStringValue("IsExternal", (bc.Entity.IsExternal ? "1" : "0")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "update"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string deleteaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessCustomer bc = new project.Business.Base.BusinessCustomer();
                bc.load(jp.getValue("id"));

                //if (obj.PopulateDataSet("select 1 from Mstr_Customer where CustNo='" + bc.Entity.CustNo + "'").Tables[0].Rows.Count > 0)
                //{
                //    flag = "3";
                //}
                //else
                //{
                    int r = bc.delete();
                    if (r <= 0)
                        flag = "2";
                //}
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CustNoS"), jp.getValue("CustNameS"), jp.getValue("CustTypeS"), jp.getValue("CustStatusS"),ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessCustomer bc = new project.Business.Base.BusinessCustomer();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.CustName = jp.getValue("CustName");
                    bc.Entity.CustShortName = jp.getValue("CustShortName");
                    bc.Entity.CustType = jp.getValue("CustType");
                    bc.Entity.Representative = jp.getValue("Representative");
                    bc.Entity.BusinessScope = jp.getValue("BusinessScope");
                    bc.Entity.CustLicenseNo = jp.getValue("CustLicenseNo");
                    bc.Entity.RepIDCard = jp.getValue("RepIDCard");
                    bc.Entity.CustContact = jp.getValue("CustContact");
                    bc.Entity.CustTel = jp.getValue("CustTel");
                    bc.Entity.CustContactMobile = jp.getValue("CustContactMobile");
                    bc.Entity.CustEmail = jp.getValue("CustEmail");
                    bc.Entity.CustBankTitle = jp.getValue("CustBankTitle");
                    bc.Entity.CustBankAccount = jp.getValue("CustBankAccount");
                    bc.Entity.CustBank = jp.getValue("CustBank");
                    bc.Entity.IsExternal = (jp.getValue("IsExternal")=="1");
                   
                    int r = bc.Save("update");

                    if (r <= 0)
                        flag = "2";
                    else
                    {
                        try
                        {
                            //同步艾众管家
                            ButlerSrv.AppService service = new ButlerSrv.AppService();
                            service.SetCustomer(bc.Entity.CustNo, bc.Entity.CustName, bc.Entity.CustShortName, bc.Entity.CustType, bc.Entity.Representative,
                                bc.Entity.BusinessScope, bc.Entity.CustLicenseNo, bc.Entity.RepIDCard, bc.Entity.CustContact, bc.Entity.CustTel,
                                bc.Entity.CustContactMobile, bc.Entity.CustEmail, bc.Entity.CustBankTitle, bc.Entity.CustBankAccount, bc.Entity.CustBank, bc.Entity.IsExternal,
                                user.Entity.UserName, "update", "5218E3ED752A49D4");

                            WOService.WebService service1 = new WOService.WebService();
                            service1.Url = wopath;
                            service1.SetCustomer(bc.Entity.CustNo, bc.Entity.CustName, bc.Entity.CustShortName, bc.Entity.CustType, bc.Entity.Representative,
                                bc.Entity.BusinessScope, bc.Entity.CustLicenseNo, bc.Entity.RepIDCard, bc.Entity.CustContact, bc.Entity.CustTel,
                                bc.Entity.CustContactMobile, bc.Entity.CustEmail, bc.Entity.CustBankTitle, bc.Entity.CustBankAccount, bc.Entity.CustBank, bc.Entity.IsExternal,
                                user.Entity.UserName, "update", "5218E3ED752A49D4");
                        }
                        catch { }
                    }
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_Customer where CustNo='" + jp.getValue("CustNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.CustNo = jp.getValue("CustNo");
                        bc.Entity.CustName = jp.getValue("CustName");
                        bc.Entity.CustShortName = jp.getValue("CustShortName");
                        bc.Entity.CustType = jp.getValue("CustType");
                        bc.Entity.Representative = jp.getValue("Representative");
                        bc.Entity.BusinessScope = jp.getValue("BusinessScope");
                        bc.Entity.CustLicenseNo = jp.getValue("CustLicenseNo");
                        bc.Entity.RepIDCard = jp.getValue("RepIDCard");
                        bc.Entity.CustContact = jp.getValue("CustContact");
                        bc.Entity.CustTel = jp.getValue("CustTel");
                        bc.Entity.CustContactMobile = jp.getValue("CustContactMobile");
                        bc.Entity.CustEmail = jp.getValue("CustEmail");
                        bc.Entity.CustBankTitle = jp.getValue("CustBankTitle");
                        bc.Entity.CustBankAccount = jp.getValue("CustBankAccount");
                        bc.Entity.CustBank = jp.getValue("CustBank");
                        bc.Entity.IsExternal = (jp.getValue("IsExternal") == "1");

                        bc.Entity.CustStatus = "3";
                        bc.Entity.CustCreator = user.Entity.UserName;
                        bc.Entity.CustCreateDate = GetDate();

                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                        else 
                        {
                            try
                            {
                                //同步艾众管家
                                ButlerSrv.AppService service = new ButlerSrv.AppService();
                                service.SetCustomer(bc.Entity.CustNo, bc.Entity.CustName, bc.Entity.CustShortName, bc.Entity.CustType, bc.Entity.Representative,
                                    bc.Entity.BusinessScope, bc.Entity.CustLicenseNo, bc.Entity.RepIDCard, bc.Entity.CustContact, bc.Entity.CustTel,
                                    bc.Entity.CustContactMobile, bc.Entity.CustEmail, bc.Entity.CustBankTitle, bc.Entity.CustBankAccount, bc.Entity.CustBank, bc.Entity.IsExternal,
                                    user.Entity.UserName, "insert", "5218E3ED752A49D4");

                                WOService.WebService service1 = new WOService.WebService();
                                service1.Url = wopath;
                                service1.SetCustomer(bc.Entity.CustNo, bc.Entity.CustName, bc.Entity.CustShortName, bc.Entity.CustType, bc.Entity.Representative,
                                    bc.Entity.BusinessScope, bc.Entity.CustLicenseNo, bc.Entity.RepIDCard, bc.Entity.CustContact, bc.Entity.CustTel,
                                    bc.Entity.CustContactMobile, bc.Entity.CustEmail, bc.Entity.CustBankTitle, bc.Entity.CustBankAccount, bc.Entity.CustBank, bc.Entity.IsExternal,
                                    user.Entity.UserName, "insert", "5218E3ED752A49D4");
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CustNoS"), jp.getValue("CustNameS"), jp.getValue("CustTypeS"), jp.getValue("CustStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CustNoS"), jp.getValue("CustNameS"), jp.getValue("CustTypeS"), jp.getValue("CustStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CustNoS"), jp.getValue("CustNameS"), jp.getValue("CustTypeS"), jp.getValue("CustStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }

    }
}