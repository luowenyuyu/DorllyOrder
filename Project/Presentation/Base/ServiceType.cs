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
    public partial class ServiceType : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/ServiceType.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/ServiceType.aspx'";
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

                        list = createList(string.Empty, string.Empty);

                        SRVSPNoStr = "<select class=\"input-text required\" id=\"SRVSPNo\" data-valid=\"between:0-30\" data-error=\"\">";
                        SRVSPNoStr += "<option value=\"\" selected>请选择服务商</option>";

                        Business.Base.BusinessServiceProvider tp = new project.Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider it in tp.GetListQuery(string.Empty, string.Empty, true))
                        {
                            SRVSPNoStr += "<option value='" + it.SPNo + "'>" + it.SPName + "</option>";
                        }
                        SRVSPNoStr += "</select>";
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
        protected string SRVSPNoStr = "";
        private string createList(string SRVTypeName, string SRVSPNo)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='15%'>服务类型编号</th>");
            sb.Append("<th width='20%'>服务类型名称</th>");
            sb.Append("<th width='20%'>父项类型</th>");
            //sb.Append("<th width='15%'>服务商</th>");
            sb.Append("<th width='30%'>描述</th>");
            sb.Append("<th width='10%'>状态</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessServiceType bc = new project.Business.Base.BusinessServiceType();
            foreach (Entity.Base.EntityServiceType it in bc.GetListQuery(string.Empty, SRVTypeName, "null", SRVSPNo, null))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.SRVTypeNo + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td style=\"text-align:left;\">" + it.SRVTypeNo + "</td>");
                sb.Append("<td style=\"text-align:left;\">" + it.SRVTypeName + "</td>");
                sb.Append("<td style=\"text-align:left;\">" + it.ParentTypeName + "</td>");
                //sb.Append("<td style=\"text-align:left;\">" + it.SRVSPName + "</td>");
                sb.Append("<td style=\"text-align:left;\">" + it.Remark + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.SRVStatus ? "label-success" : "") + " radius\">" + (it.SRVStatus ? "有效" : "已失效") + "</span></td>");
                sb.Append("</tr>");
                r++;

                Business.Base.BusinessServiceType bc1 = new project.Business.Base.BusinessServiceType();
                foreach (Entity.Base.EntityServiceType it1 in bc1.GetListQuery(string.Empty, SRVTypeName, it.SRVTypeNo, SRVSPNo, null))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it1.SRVTypeNo + "\">");
                    sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                    sb.Append("<td style=\"text-align:left;\">" + it1.SRVTypeNo + "</td>");
                    sb.Append("<td style=\"text-align:left;\">&nbsp;&nbsp;&nbsp;&nbsp;" + it1.SRVTypeName + "</td>");
                    sb.Append("<td style=\"text-align:left;\">" + it1.ParentTypeName + "</td>");
                    //sb.Append("<td style=\"text-align:left;\">" + it1.SRVSPName + "</td>");
                    sb.Append("<td style=\"text-align:left;\">" + it1.Remark + "</td>");
                    sb.Append("<td class=\"td-status\"><span class=\"label " + (it1.SRVStatus ? "label-success" : "") + " radius\">" + (it1.SRVStatus ? "有效" : "已失效") + "</span></td>");
                    sb.Append("</tr>");
                    r++;
                }

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
            if (jp.getValue("Type") == "check")
                result = checkaction(jp);
            else if (jp.getValue("Type") == "insert")
                result = insertaction(jp);
            else if (jp.getValue("Type") == "delete")
                result = deleteaction(jp);
            else if (jp.getValue("Type") == "update")
                result = updateaction(jp);
            else if (jp.getValue("Type") == "submit")
                result = submitaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "valid")
                result = validaction(jp);
            return result;
        }

        private string checkaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                flag = check(jp.getValue("tp"), jp.getValue("val"), collection);
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("id", jp.getValue("id")));
            collection.Add(new JsonStringValue("type", "check"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string insertaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessServiceType bc = new project.Business.Base.BusinessServiceType();
                bc.load(jp.getValue("id"));
                if (bc.Entity.ParentTypeNo == "")
                {
                    collection.Add(new JsonStringValue("SRVTypeNo", bc.Entity.SRVTypeNo));
                    collection.Add(new JsonStringValue("SRVTypeName", bc.Entity.SRVTypeName));
                }
                else
                {
                    collection.Add(new JsonStringValue("SRVTypeNo", bc.Entity.ParentTypeNo));
                    collection.Add(new JsonStringValue("SRVTypeName", bc.Entity.ParentTypeName));
                }
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
                Business.Base.BusinessServiceType bc = new project.Business.Base.BusinessServiceType();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("SRVTypeNo", bc.Entity.SRVTypeNo));
                collection.Add(new JsonStringValue("SRVTypeName", bc.Entity.SRVTypeName));
                collection.Add(new JsonStringValue("ParentTypeNo", bc.Entity.ParentTypeNo));
                collection.Add(new JsonStringValue("ParentTypeNoName", bc.Entity.ParentTypeName));
                collection.Add(new JsonStringValue("SRVSPNo", bc.Entity.SRVSPNo));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));
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
                Business.Base.BusinessServiceType bc = new project.Business.Base.BusinessServiceType();
                bc.load(jp.getValue("id"));

                if (obj.PopulateDataSet("select 1 from Mstr_ServiceType where ParentTypeNo='" + bc.Entity.SRVTypeNo + "'").Tables[0].Rows.Count > 0)
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SRVTypeNameS"), jp.getValue("SRVSPNoS"))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessServiceType bc = new project.Business.Base.BusinessServiceType();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.SRVTypeName = jp.getValue("SRVTypeName");
                    bc.Entity.ParentTypeNo = jp.getValue("ParentTypeNo");
                    bc.Entity.SRVSPNo = jp.getValue("SRVSPNo");
                    bc.Entity.Remark = jp.getValue("Remark");
                    int r = bc.Save("update");
                    
                    if (r <= 0)
                        flag = "2";
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_ServiceType where SRVTypeNo='" + jp.getValue("SRVTypeNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.SRVTypeNo = jp.getValue("SRVTypeNo");
                        bc.Entity.SRVTypeName = jp.getValue("SRVTypeName");
                        bc.Entity.ParentTypeNo = jp.getValue("ParentTypeNo");
                        bc.Entity.SRVSPNo = jp.getValue("SRVSPNo");
                        bc.Entity.Remark = jp.getValue("Remark");

                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SRVTypeNameS"), jp.getValue("SRVSPNoS"))));

            return collection.ToString();
        }

        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SRVTypeNameS"), jp.getValue("SRVSPNoS"))));

            return collection.ToString();
        }

        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessServiceType bc = new project.Business.Base.BusinessServiceType();
                bc.load(jp.getValue("id"));
                bc.Entity.SRVStatus = !bc.Entity.SRVStatus;

                int r = bc.valid();
                if (r <= 0) flag = "2";
                if (bc.Entity.SRVStatus)
                    collection.Add(new JsonStringValue("stat", "<span class=\"label label-success radius\">有效</span>"));
                else
                    collection.Add(new JsonStringValue("stat", "<span class=\"label radius\">已失效</span>"));
                //collection.Add(new JsonStringValue("id", jp.getValue("id")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "valid"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SRVTypeNameS"), jp.getValue("SRVSPNoS"))));
            return collection.ToString();
        }
        
    }
}