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
using Newtonsoft.Json;

namespace project.Presentation.Base
{
    public partial class Billboard : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/Billboard.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/Billboard.aspx'";
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
                        
                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1);
                        
                        Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        BBLOCNoStr += "<select id=\"BBLOCNo\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"园区不能为空\">";
                        BBLOCNoStrS += "<select id=\"BBLOCNoS\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        BBLOCNoStr += "<option value=''>请选择园区</option>";
                        BBLOCNoStrS += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        {
                            BBLOCNoStr += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                            BBLOCNoStrS += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        }
                        BBLOCNoStr += "</select>";
                        BBLOCNoStrS += "</select>";

                        Business.Base.BusinessBillboardType bt = new Business.Base.BusinessBillboardType();
                        BBTypeStr += "<select id=\"BBType\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"广告位类型不能为空\">";
                        BBTypeStrS += "<select id=\"BBTypeS\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        BBTypeStr += "<option value=''>请选择广告位类型</option>";
                        BBTypeStrS += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityBillboardType it in bt.GetListQuery(string.Empty, string.Empty))
                        {
                            BBTypeStr += "<option value='" + it.BBTypeNo + "'>" + it.BBTypeName + "</option>";
                            BBTypeStrS += "<option value='" + it.BBTypeNo + "'>" + it.BBTypeName + "</option>";
                        }
                        BBTypeStr += "</select>";
                        BBTypeStrS += "</select>";

                        BBSPNoStr = "<select class=\"input-text required\" id=\"BBSPNo\" data-valid=\"isNonEmpty\" data-error=\"请选择服务商\">";
                        BBSPNoStrS += "<select id=\"BBSPNoS\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        BBSPNoStr += "<option value=''>请选择服务商</option>";
                        BBSPNoStrS += "<option value=''>全部</option>";
                        Business.Base.BusinessServiceProvider tp = new project.Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider it in tp.GetListQuery(string.Empty, string.Empty, true))
                        {
                            BBSPNoStr += "<option value='" + it.SPNo + "'>" + it.SPShortName + "</option>";
                            BBSPNoStrS += "<option value='" + it.SPNo + "'>" + it.SPShortName + "</option>";
                        }
                        BBSPNoStr += "</select>";
                        BBSPNoStrS += "</select>";
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
        protected string BBLOCNoStr = "";
        protected string BBLOCNoStrS = "";
        protected string BBTypeStr = "";
        protected string BBTypeStrS = "";
        protected string BBSPNoStr = "";
        protected string BBSPNoStrS = "";

        private string createList(string BBNo, string BBName, string BBAddr, string BBStatus, string BBType, string BBSPNo, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='10%'>广告位编号</th>");
            sb.Append("<th width='14%'>广告位名称</th>");
            sb.Append("<th width='10%'>所在园区</th>");
            sb.Append("<th width='20%'>所在位置</th>");
            sb.Append("<th width='9%'>广告位类别</th>");
            sb.Append("<th width='10%'>服务商</th>");
            sb.Append("<th width='9%'>规格</th>");
            sb.Append("<th width='7%'>状态</th>");
            sb.Append("<th width='7%'>是否禁用</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            
            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessBillboard bc = new project.Business.Base.BusinessBillboard();
            foreach (Entity.Base.EntityBillboard it in bc.GetListQuery(BBNo, BBName, BBAddr, BBStatus, BBType, BBSPNo, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.BBNo + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.BBNo + "</td>");
                sb.Append("<td>" + it.BBName + "</td>");
                sb.Append("<td>" + it.BBSPName + "</td>");
                sb.Append("<td>" + it.BBAddr + "</td>");
                sb.Append("<td>" + it.BBTypeName + "</td>");
                sb.Append("<td>" + it.BBSPName + "</td>");
                sb.Append("<td>" + it.BBSize + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.BBStatus == "use" ? "label-success" : "") + " radius\">" + it.BBStatusName + "</span></td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.BBISEnable == false ? "label-success" : "") + " radius\">" + (it.BBISEnable == false ? "正常" : "已禁用") + "</span></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(BBNo, BBName, BBAddr, BBStatus, BBType, BBSPNo), pageSize, page, 7));

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
                Business.Base.BusinessBillboard bc = new project.Business.Base.BusinessBillboard();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("BBNo", bc.Entity.BBNo));
                collection.Add(new JsonStringValue("BBName", bc.Entity.BBName));
                collection.Add(new JsonStringValue("BBSPNo", bc.Entity.BBSPNo));
                collection.Add(new JsonStringValue("BBLOCNo", bc.Entity.BBLOCNo));
                collection.Add(new JsonStringValue("BBAddr", bc.Entity.BBAddr));
                collection.Add(new JsonStringValue("BBSize", bc.Entity.BBSize));
                collection.Add(new JsonStringValue("BBType", bc.Entity.BBType));
                collection.Add(new JsonStringValue("BBINPriceDay", bc.Entity.BBINPriceDay.ToString("0.####")));
                collection.Add(new JsonStringValue("BBOUTPriceDay", bc.Entity.BBOUTPriceDay.ToString("0.####")));
                collection.Add(new JsonStringValue("BBINPriceMonth", bc.Entity.BBINPriceMonth.ToString("0.####")));
                collection.Add(new JsonStringValue("BBOUTPriceMonth", bc.Entity.BBOUTPriceMonth.ToString("0.####")));
                collection.Add(new JsonStringValue("BBINPriceQuarter", bc.Entity.BBINPriceQuarter.ToString("0.####")));
                collection.Add(new JsonStringValue("BBOUTPriceQuarter", bc.Entity.BBOUTPriceQuarter.ToString("0.####")));
                collection.Add(new JsonStringValue("BBINPriceYear", bc.Entity.BBINPriceYear.ToString("0.####")));
                collection.Add(new JsonStringValue("BBOUTPriceYear", bc.Entity.BBOUTPriceYear.ToString("0.####")));
                collection.Add(new JsonStringValue("BBDeposit", bc.Entity.BBDeposit.ToString("0.####")));
                collection.Add(new JsonStringValue("BBImage", bc.Entity.BBImage));
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
                Business.Base.BusinessBillboard bc = new project.Business.Base.BusinessBillboard();
                bc.load(jp.getValue("id"));

                if (obj.PopulateDataSet("select 1 from Op_ContractBBRentalDetail where BBNo='" + bc.Entity.BBNo + "'").Tables[0].Rows.Count > 0)
                {
                    flag = "3";
                }
                else
                {
                    if (bc.Entity.BBStatus == "use")
                    {
                        flag = "4";
                    }
                    else
                    {
                        int r = bc.delete();
                        if (r <= 0)
                            flag = "2";
                        else
                        {
                            ResourceService.ResourceService srv = new ResourceService.ResourceService();
                            srv.DeleteResource(bc.Entity.BBNo);
                        }
                    }
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("BBNoS"), jp.getValue("BBNameS"), jp.getValue("BBAddrS"), jp.getValue("BBStatusS"),
                 jp.getValue("BBTypeS"), jp.getValue("BBSPNoS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessBillboard bc = new project.Business.Base.BusinessBillboard();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.BBName = jp.getValue("BBName");
                    bc.Entity.BBSPNo = jp.getValue("BBSPNo");
                    bc.Entity.BBLOCNo = jp.getValue("BBLOCNo");
                    bc.Entity.BBAddr = jp.getValue("BBAddr");
                    bc.Entity.BBSize = jp.getValue("BBSize");
                    bc.Entity.BBType = jp.getValue("BBType");
                    bc.Entity.BBINPriceDay = ParseDecimalForString(jp.getValue("BBINPriceDay"));
                    bc.Entity.BBOUTPriceDay = ParseDecimalForString(jp.getValue("BBOUTPriceDay"));
                    bc.Entity.BBINPriceMonth = ParseDecimalForString(jp.getValue("BBINPriceMonth"));
                    bc.Entity.BBOUTPriceMonth = ParseDecimalForString(jp.getValue("BBOUTPriceMonth"));
                    bc.Entity.BBINPriceQuarter = ParseDecimalForString(jp.getValue("BBINPriceQuarter"));
                    bc.Entity.BBOUTPriceQuarter = ParseDecimalForString(jp.getValue("BBOUTPriceQuarter"));
                    bc.Entity.BBINPriceYear = ParseDecimalForString(jp.getValue("BBINPriceYear"));
                    bc.Entity.BBOUTPriceYear = ParseDecimalForString(jp.getValue("BBOUTPriceYear"));
                    bc.Entity.BBDeposit = ParseDecimalForString(jp.getValue("BBDeposit"));
                    bc.Entity.BBImage = jp.getValue("BBImage");
                                        
                    int r = bc.Save("update");

                    if (r <= 0)
                        flag = "2";
                    else
                    {
                        ResourceService.ResourceService srv = new ResourceService.ResourceService();
                        string aa = srv.AddOrUpdateBillboard(JsonConvert.SerializeObject(bc.Entity));
                        string ss = aa;
                    }
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_Billboard where BBNo='" + jp.getValue("BBNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.BBNo = jp.getValue("BBNo");
                        bc.Entity.BBName = jp.getValue("BBName");
                        bc.Entity.BBSPNo = jp.getValue("BBSPNo");
                        bc.Entity.BBLOCNo = jp.getValue("BBLOCNo");
                        bc.Entity.BBAddr = jp.getValue("BBAddr");
                        bc.Entity.BBSize = jp.getValue("BBSize");
                        bc.Entity.BBType = jp.getValue("BBType");
                        bc.Entity.BBINPriceDay = ParseDecimalForString(jp.getValue("BBINPriceDay"));
                        bc.Entity.BBOUTPriceDay = ParseDecimalForString(jp.getValue("BBOUTPriceDay"));
                        bc.Entity.BBINPriceMonth = ParseDecimalForString(jp.getValue("BBINPriceMonth"));
                        bc.Entity.BBOUTPriceMonth = ParseDecimalForString(jp.getValue("BBOUTPriceMonth"));
                        bc.Entity.BBINPriceQuarter = ParseDecimalForString(jp.getValue("BBINPriceQuarter"));
                        bc.Entity.BBOUTPriceQuarter = ParseDecimalForString(jp.getValue("BBOUTPriceQuarter"));
                        bc.Entity.BBINPriceYear = ParseDecimalForString(jp.getValue("BBINPriceYear"));
                        bc.Entity.BBOUTPriceYear = ParseDecimalForString(jp.getValue("BBOUTPriceYear"));
                        bc.Entity.BBDeposit = ParseDecimalForString(jp.getValue("BBDeposit"));
                        bc.Entity.BBImage = jp.getValue("BBImage");

                        bc.Entity.BBCreator = user.Entity.UserName;
                        bc.Entity.BBCreateDate = GetDate();
                        
                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                        else
                        {
                            ResourceService.ResourceService srv = new ResourceService.ResourceService();
                            srv.AddOrUpdateBillboard(JsonConvert.SerializeObject(bc.Entity));
                        }
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));

            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("BBNoS"), jp.getValue("BBNameS"), jp.getValue("BBAddrS"), jp.getValue("BBStatusS"),
                 jp.getValue("BBTypeS"), jp.getValue("BBSPNoS"), ParseIntForString(jp.getValue("page"))))); return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("BBNoS"), jp.getValue("BBNameS"), jp.getValue("BBAddrS"), jp.getValue("BBStatusS"),
                 jp.getValue("BBTypeS"), jp.getValue("BBSPNoS"), ParseIntForString(jp.getValue("page"))))); 
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("BBNoS"), jp.getValue("BBNameS"), jp.getValue("BBAddrS"), jp.getValue("BBStatusS"),
                 jp.getValue("BBTypeS"), jp.getValue("BBSPNoS"), ParseIntForString(jp.getValue("page"))))); 
            return collection.ToString();
        }
        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessBillboard bc = new project.Business.Base.BusinessBillboard();
                bc.load(jp.getValue("id"));
                bc.Entity.BBISEnable = !bc.Entity.BBISEnable;

                int r = bc.valid();
                if (r <= 0) flag = "2";
                if (bc.Entity.BBISEnable)
                    collection.Add(new JsonStringValue("stat", "<span class=\"label radius\">禁用</span>"));
                else
                    collection.Add(new JsonStringValue("stat", "<span class=\"label label-success radius\">正常</span>"));

                collection.Add(new JsonStringValue("id", jp.getValue("id")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "valid"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("BBNoS"), jp.getValue("BBNameS"), jp.getValue("BBAddrS"), jp.getValue("BBStatusS"),
                 jp.getValue("BBTypeS"), jp.getValue("BBSPNoS"), ParseIntForString(jp.getValue("page"))))); return collection.ToString();
        }
    }
}