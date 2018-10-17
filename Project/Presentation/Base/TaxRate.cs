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
    public partial class TaxRate : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/TaxRate.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/TaxRate.aspx'";
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

                        list = createList(string.Empty, string.Empty, 1);

                        Business.Base.BusinessServiceProvider bw = new Business.Base.BusinessServiceProvider();
                        SPNo += "<select id=\"SPNo\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"请选择服务商\">";
                        SPNo += "<option value=''></option>";
                        SPNoS += "<select id=\"SPNoS\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        SPNoS += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityServiceProvider it in bw.GetListQuery(string.Empty, string.Empty, true))
                        {
                            SPNo += "<option value='" + it.SPNo + "'>" + it.SPName + "</option>";
                            SPNoS += "<option value='" + it.SPNo + "'>" + it.SPName + "</option>";
                        }
                        SPNo += "</select>";
                        SPNoS += "</select>";

                        Business.Base.BusinessService loc = new Business.Base.BusinessService();
                        SRVNo += "<select id=\"SRVNo\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"请选择费用项目\">";
                        SRVNo += "<option value=''></option>";
                        SRVNoS += "<select id=\"SRVNoS\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        SRVNoS += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityService it in loc.GetListQuery("","","","","","",""))
                        {
                            SRVNo += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                            SRVNoS += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                        }
                        SRVNo += "</select>";
                        SRVNoS += "</select>";
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
        protected string SPNo = "";
        protected string SPNoS = "";
        protected string SRVNo = "";
        protected string SRVNoS = "";

        private string createList(string SPNo, string SRVNo, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='35%'>费用项目</th>");
            sb.Append("<th width='35%'>服务商</th>");
            sb.Append("<th width='25%'>税率</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            
            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessTaxRate bc = new project.Business.Base.BusinessTaxRate();
            foreach (Entity.Base.EntityTaxRate it in bc.GetListQuery(SPNo, SRVNo, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RP + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.SRVName + "</td>");
                sb.Append("<td>" + it.SPName + "</td>");
                sb.Append("<td>" + it.Rate.ToString("0.####") + "</td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(SPNo, SRVNo), pageSize, page, 7));

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
            return result;
        }

        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessTaxRate bc = new project.Business.Base.BusinessTaxRate();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("SPNo", bc.Entity.SPNo));
                collection.Add(new JsonStringValue("SRVNo", bc.Entity.SRVNo));
                collection.Add(new JsonStringValue("Rate", bc.Entity.Rate.ToString("0.####")));
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
                Business.Base.BusinessTaxRate bc = new project.Business.Base.BusinessTaxRate();
                bc.load(jp.getValue("id"));

                int r = bc.delete();
                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SPNoS"), jp.getValue("SRVNoS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessTaxRate bc = new project.Business.Base.BusinessTaxRate();
                if (jp.getValue("tp") == "update")
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_TaxRate "+
                        "where SPNo='" + jp.getValue("SPNo") + "' and SRVNo='" + jp.getValue("SRVNo") + "' and RP<>'" + jp.getValue("id") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.load(jp.getValue("id"));
                        bc.Entity.SPNo = jp.getValue("SPNo");
                        bc.Entity.SRVNo = jp.getValue("SRVNo");
                        bc.Entity.Rate = ParseDecimalForString(jp.getValue("Rate"));
                        bc.Entity.UpdateUser = user.Entity.UserName;
                        bc.Entity.UpdateDate = GetDate();
                        int r = bc.Save();

                        if (r <= 0)
                            flag = "2";
                    }
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_TaxRate where SPNo='" + jp.getValue("SPNo") + "' and SRVNo='" + jp.getValue("SRVNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.SPNo = jp.getValue("SPNo");
                        bc.Entity.SRVNo = jp.getValue("SRVNo");
                        bc.Entity.Rate = ParseDecimalForString(jp.getValue("Rate"));
                        bc.Entity.CreateUser = user.Entity.UserName;
                        bc.Entity.CreateDate = GetDate();
                        bc.Entity.UpdateUser = user.Entity.UserName;
                        bc.Entity.UpdateDate = GetDate();
                        int r = bc.Save();
                        if (r <= 0)
                            flag = "2";
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SPNoS"), jp.getValue("SRVNoS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SPNoS"), jp.getValue("SRVNoS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SPNoS"), jp.getValue("SRVNoS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
    }
}