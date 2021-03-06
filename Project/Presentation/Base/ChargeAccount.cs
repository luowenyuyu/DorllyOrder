﻿using System;
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
    public partial class ChargeAccount : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/ChargeAccount.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/ChargeAccount.aspx'";
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

                        list = createList(string.Empty,string.Empty,string.Empty,1);

                        CASPNoStr = "<select class=\"input-text required\" id=\"CASPNo\" style=\"width:360px;\" data-valid=\"isNonEmpty\" data-error=\"请选择服务商\">";
                        CASPNoStr += "<option value=\"\"></option>";

                        CASPNoStrS = "<select class=\"input-text size-MINI\" id=\"CASPNoS\" style=\"width:120px;\" >";
                        CASPNoStrS += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessServiceProvider tp = new project.Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider it in tp.GetListQuery(string.Empty, string.Empty, true))
                        {
                            CASPNoStr += "<option value='" + it.SPNo + "'>" + it.SPName + "</option>";
                            CASPNoStrS += "<option value='" + it.SPNo + "'>" + it.SPName + "</option>";
                        }
                        CASPNoStr += "</select>";
                        CASPNoStrS += "</select>";
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
        protected string CASPNoStr = "";
        protected string CASPNoStrS = "";
        private string createList(string CANo, string CAName, string CASPNo, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='20%'>费用科目编号</th>");
            sb.Append("<th width='25%'>费用科目名称</th>");
            sb.Append("<th width='25%'>服务商</th>");
            sb.Append("<th width='25%'>应收账款科目编码</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessChargeAccount bc = new project.Business.Base.BusinessChargeAccount();
            foreach (Entity.Base.EntityChargeAccount it in bc.GetListQuery(CANo, CAName, CASPNo, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.CANo + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td style=\"text-align:left;\">" + it.CANo + "</td>");
                sb.Append("<td style=\"text-align:left;\">" + it.CAName + "</td>");
                sb.Append("<td style=\"text-align:left;\">" + it.CASPName + "</td>");
                sb.Append("<td style=\"text-align:left;\">" + it.APNo + "</td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(CANo, CAName, CASPNo), pageSize, page, 7));           
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
                Business.Base.BusinessChargeAccount bc = new project.Business.Base.BusinessChargeAccount();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("CANo", bc.Entity.CANo));
                collection.Add(new JsonStringValue("CAName", bc.Entity.CAName));
                collection.Add(new JsonStringValue("CASPNo", bc.Entity.CASPNo));
                collection.Add(new JsonStringValue("APNo", bc.Entity.APNo));
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
                Business.Base.BusinessChargeAccount bc = new project.Business.Base.BusinessChargeAccount();
                bc.load(jp.getValue("id"));

                if (obj.PopulateDataSet("select 1 from Mstr_Service where CANo='" + bc.Entity.CANo + "' and SRVSPNo='" + bc.Entity.CASPNo + "'").Tables[0].Rows.Count > 0)
                {
                    flag = "3";
                }
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CANoS"),jp.getValue("CANameS"),jp.getValue("CASPNoS"),int.Parse(jp.getValue("page")))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessChargeAccount bc = new project.Business.Base.BusinessChargeAccount();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.CAName = jp.getValue("CAName");
                    bc.Entity.APNo = jp.getValue("APNo");
                    int r = bc.Save("update");
                    
                    if (r <= 0)
                        flag = "2";
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_ChargeAccount where CANo='" + jp.getValue("CANo") + "' and CASPNo='" + jp.getValue("CASPNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.CANo = jp.getValue("CANo");
                        bc.Entity.CAName = jp.getValue("CAName");
                        bc.Entity.CASPNo = jp.getValue("CASPNo");
                        bc.Entity.APNo = jp.getValue("APNo");

                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CANoS"), jp.getValue("CANameS"), jp.getValue("CASPNoS"), int.Parse(jp.getValue("page")))));

            return collection.ToString();
        }

        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CANoS"), jp.getValue("CANameS"), jp.getValue("CASPNoS"), int.Parse(jp.getValue("page")))));

            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CANoS"), jp.getValue("CANameS"), jp.getValue("CASPNoS"), int.Parse(jp.getValue("page")))));

            return collection.ToString();
        }
        
    }
}