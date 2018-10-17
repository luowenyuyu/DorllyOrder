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
    public partial class ServiceProvider : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/ServiceProvider.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/ServiceProvider.aspx'";
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
                                if (rightCode.IndexOf("vilad") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"valid()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 启用/停用</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"valid()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 启用/停用</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, 1);

                        ServiceTypeStr = "<select class=\"input-text required\" id=\"MService\" data-valid=\"between:0-30\" data-error=\"\">";
                        ServiceTypeStr += "<option value=\"\" selected>请选择主营业务</option>";

                        Business.Base.BusinessServiceType tp = new project.Business.Base.BusinessServiceType();
                        foreach (Entity.Base.EntityServiceType it in tp.GetListQuery(string.Empty, string.Empty, "null", string.Empty, true))
                        {
                            ServiceTypeStr += "<option value='" + it.SRVTypeNo + "'>" + it.SRVTypeName + "</option>";
                        }
                        ServiceTypeStr += "</select>";
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
        protected string ServiceTypeStr = "";
        private string createList(string SPName, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='8%'>服务商编号</th>");
            sb.Append("<th width='19%'>服务商名称</th>");
            sb.Append("<th width='9%'>服务商简称</th>");
            sb.Append("<th width='9%'>主营业务</th>");
            sb.Append("<th width='9%'>联系人</th>");
            sb.Append("<th width='9%'>电话</th>");
            sb.Append("<th width='9%'>U8账套</th>");
            //sb.Append("<th width='8%'>U8银行科目</th>");
            sb.Append("<th width='9%'>U8现金科目</th>");
            sb.Append("<th width='9%'>U8税额科目</th>");
            sb.Append("<th width='5%'>状态</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessServiceProvider bc = new project.Business.Base.BusinessServiceProvider();
            foreach (Entity.Base.EntityServiceProvider it in bc.GetListQuery(string.Empty, SPName, null, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.SPNo + "\">");
                sb.Append("<td align='center'>" + r.ToString() + "</td>");
                sb.Append("<td>" + it.SPNo + "</td>");
                sb.Append("<td>" + it.SPName + "</td>");
                sb.Append("<td>" + it.SPShortName + "</td>");
                sb.Append("<td>" + it.MServiceName + "</td>");
                sb.Append("<td>" + it.SPContact + "</td>");
                sb.Append("<td>" + it.SPTel + "</td>");
                sb.Append("<td>" + it.U8Account + "</td>");
                //sb.Append("<td>" + it.BankAccount + "</td>");
                sb.Append("<td>" + it.CashAccount + "</td>");
                sb.Append("<td>" + it.TaxAccount + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.SPStatus ? "label-success" : "") + " radius\">" + (it.SPStatus ? "有效" : "已失效") + "</span></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append(Paginat(bc.GetListCount(string.Empty, SPName, null), pageSize, page, 7));
            
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
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            else if (jp.getValue("Type") == "valid")
                result = validaction(jp);
            return result;
        }

        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessServiceProvider bc = new project.Business.Base.BusinessServiceProvider();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("SPNo", bc.Entity.SPNo));
                collection.Add(new JsonStringValue("SPName", bc.Entity.SPName));
                collection.Add(new JsonStringValue("SPShortName", bc.Entity.SPShortName));
                collection.Add(new JsonStringValue("MService", bc.Entity.MService));
                collection.Add(new JsonStringValue("SPLicenseNo", bc.Entity.SPLicenseNo));
                collection.Add(new JsonStringValue("SPContact", bc.Entity.SPContact));
                collection.Add(new JsonStringValue("SPContactMobile", bc.Entity.SPContactMobile));
                collection.Add(new JsonStringValue("SPTel", bc.Entity.SPTel));
                collection.Add(new JsonStringValue("SPEMail", bc.Entity.SPEMail));
                collection.Add(new JsonStringValue("SPAddr", bc.Entity.SPAddr));
                collection.Add(new JsonStringValue("SPBank", bc.Entity.SPBank));
                collection.Add(new JsonStringValue("SPBankAccount", bc.Entity.SPBankAccount));
                collection.Add(new JsonStringValue("SPBankTitle", bc.Entity.SPBankTitle));
                collection.Add(new JsonStringValue("U8Account", bc.Entity.U8Account));
                //collection.Add(new JsonStringValue("BankAccount", bc.Entity.BankAccount));
                collection.Add(new JsonStringValue("CashAccount", bc.Entity.CashAccount));
                collection.Add(new JsonStringValue("TaxAccount", bc.Entity.TaxAccount));
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
                Business.Base.BusinessServiceProvider bc = new project.Business.Base.BusinessServiceProvider();
                bc.load(jp.getValue("id"));

                int r = bc.delete();
                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SPNameS"),ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessServiceProvider bc = new project.Business.Base.BusinessServiceProvider();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.SPName = jp.getValue("SPName");
                    bc.Entity.SPShortName = jp.getValue("SPShortName");
                    bc.Entity.MService = jp.getValue("MService");
                    bc.Entity.SPLicenseNo = jp.getValue("SPLicenseNo");
                    bc.Entity.SPContact = jp.getValue("SPContact");
                    bc.Entity.SPContactMobile = jp.getValue("SPContactMobile");
                    bc.Entity.SPTel = jp.getValue("SPTel");
                    bc.Entity.SPEMail = jp.getValue("SPEMail");
                    bc.Entity.SPAddr = jp.getValue("SPAddr");
                    bc.Entity.SPBank = jp.getValue("SPBank");
                    bc.Entity.SPBankAccount = jp.getValue("SPBankAccount");
                    bc.Entity.SPBankTitle = jp.getValue("SPBankTitle");
                    bc.Entity.U8Account = jp.getValue("U8Account");
                    //bc.Entity.BankAccount = jp.getValue("BankAccount");
                    bc.Entity.CashAccount = jp.getValue("CashAccount");
                    bc.Entity.TaxAccount = jp.getValue("TaxAccount");
                    int r = bc.Save("update");
                    
                    if (r <= 0)
                        flag = "2";
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_ServiceProvider where SPNo='" + jp.getValue("SPNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.SPNo = jp.getValue("SPNo");
                        bc.Entity.SPName = jp.getValue("SPName");
                        bc.Entity.SPShortName = jp.getValue("SPShortName");
                        bc.Entity.MService = jp.getValue("MService");
                        bc.Entity.SPLicenseNo = jp.getValue("SPLicenseNo");
                        bc.Entity.SPContact = jp.getValue("SPContact");
                        bc.Entity.SPContactMobile = jp.getValue("SPContactMobile");
                        bc.Entity.SPTel = jp.getValue("SPTel");
                        bc.Entity.SPEMail = jp.getValue("SPEMail");
                        bc.Entity.SPAddr = jp.getValue("SPAddr");
                        bc.Entity.SPBank = jp.getValue("SPBank");
                        bc.Entity.SPBankAccount = jp.getValue("SPBankAccount");
                        bc.Entity.SPBankTitle = jp.getValue("SPBankTitle");
                        bc.Entity.U8Account = jp.getValue("U8Account");
                        //bc.Entity.BankAccount = jp.getValue("BankAccount");
                        bc.Entity.CashAccount = jp.getValue("CashAccount");
                        bc.Entity.TaxAccount = jp.getValue("TaxAccount");
                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SPNameS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SPNameS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SPNameS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }        
        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessServiceProvider bc = new project.Business.Base.BusinessServiceProvider();
                bc.load(jp.getValue("id"));
                bc.Entity.SPStatus = !bc.Entity.SPStatus;

                int r = bc.valid();
                if (r <= 0) flag = "2";
                if (bc.Entity.SPStatus)
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