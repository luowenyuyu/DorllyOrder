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
    public partial class Meter : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/Meter.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/Meter.aspx'";
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

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1);
                        
                        Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        MeterLOCNo1Str += "<select id=\"MeterLOCNo1\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"园区不能为空\">";
                        MeterLOCNo1StrS += "<select id=\"MeterLOCNo1S\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        MeterLOCNo1Str += "<option value=''>请选择园区</option>";
                        MeterLOCNo1StrS += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        {
                            MeterLOCNo1Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                            MeterLOCNo1StrS += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        }
                        MeterLOCNo1Str += "</select>";
                        MeterLOCNo1StrS += "</select>";
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
        protected string MeterLOCNo1Str = "";
        protected string MeterLOCNo1StrS = "";
        private string createList(string MeterLOCNo1, string MeterLOCNo2, string MeterLOCNo3, string MeterLOCNo4, string MeterNo, string MeterType, string MeterUsageType, string MeterRMID, string MeterSize, string MeterStatus, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='13%'>房间编号</th>");
            sb.Append("<th width='10%'>表记编号</th>");
            sb.Append("<th width='10%'>表记名称</th>");
            sb.Append("<th width='16%'>园区/建设期/楼栋/楼层</th>");
            sb.Append("<th width='6%'>倍数</th>");
            sb.Append("<th width='6%'>位数</th>");
            sb.Append("<th width='8%'>使用类别</th>");
            sb.Append("<th width='9%'>当月读数</th>");
            sb.Append("<th width='10%'>读数日期</th>");
            sb.Append("<th width='7%'>状态</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessMeter bc = new project.Business.Base.BusinessMeter();
            foreach (Entity.Base.EntityMeter it in bc.GetListQuery(MeterLOCNo1, MeterLOCNo2, MeterLOCNo3, MeterLOCNo4, MeterNo, MeterType, MeterUsageType, MeterRMID,"", MeterSize, MeterStatus, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.MeterNo + "\">");
                sb.Append("<td>" + r.ToString() + "</td>");
                sb.Append("<td>" + it.MeterRMID + "</td>");
                sb.Append("<td>" + it.MeterNo + "</td>");
                sb.Append("<td>" + it.MeterName + "</td>");
                sb.Append("<td>" + it.MeterLOCNo1Name + "/" + it.MeterLOCNo2Name + "/" + it.MeterLOCNo3Name + "/" + it.MeterLOCNo4Name + "</td>");
                sb.Append("<td>" + it.MeterRate.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.MeterDigit + "</td>");
                sb.Append("<td>" + it.MeterUsageTypeName + "</td>");
                sb.Append("<td>" + it.MeterReadout.ToString("0.####") + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.MeterReadoutDate) + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.MeterStatus == "open" ? "label-success" : "") + " radius\">" + it.MeterStatusName + "</span></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(MeterLOCNo1, MeterLOCNo2, MeterLOCNo3, MeterLOCNo4, MeterNo, MeterType, MeterUsageType, MeterRMID, "", MeterSize, MeterStatus), pageSize, page, 7));

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
            else if (jp.getValue("Type") == "getvalue")
                result = getvalueaction(jp);
            return result;
        }
        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessMeter bc = new project.Business.Base.BusinessMeter();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("MeterNo", bc.Entity.MeterNo));
                collection.Add(new JsonStringValue("MeterName", bc.Entity.MeterName));
                collection.Add(new JsonStringValue("MeterType", bc.Entity.MeterType));
                collection.Add(new JsonStringValue("MeterLOCNo1", bc.Entity.MeterLOCNo1));
                collection.Add(new JsonStringValue("MeterLOCNo2", bc.Entity.MeterLOCNo2));
                collection.Add(new JsonStringValue("MeterLOCNo3", bc.Entity.MeterLOCNo3));
                collection.Add(new JsonStringValue("MeterLOCNo4", bc.Entity.MeterLOCNo4));
                collection.Add(new JsonStringValue("MeterRate", bc.Entity.MeterRate.ToString("0.####")));
                collection.Add(new JsonStringValue("MeterDigit", bc.Entity.MeterDigit.ToString()));
                collection.Add(new JsonStringValue("MeterUsageType", bc.Entity.MeterUsageType));
                collection.Add(new JsonStringValue("MeterNatureType", bc.Entity.MeterNatureType));
                collection.Add(new JsonStringValue("MeterReadout", bc.Entity.MeterReadout.ToString("0.####")));
                collection.Add(new JsonStringValue("MeterReadoutDate", ParseStringForDate(bc.Entity.MeterReadoutDate)));
                collection.Add(new JsonStringValue("MeterRMID", bc.Entity.MeterRMID));
                collection.Add(new JsonStringValue("MeterSize", bc.Entity.MeterSize));
                collection.Add(new JsonStringValue("MeterRelatedMeterNo", bc.Entity.MeterRelatedMeterNo));
                collection.Add(new JsonStringValue("Addr", bc.Entity.Addr));

                string subtype = "";
                int row = 0;
                Business.Base.BusinessLocation bt = new Business.Base.BusinessLocation();
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.MeterLOCNo1))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));

                row = 0;
                subtype = "";
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.MeterLOCNo2))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row1", row));
                collection.Add(new JsonStringValue("subtype1", subtype));

                row = 0;
                subtype = "";
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.MeterLOCNo3))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row2", row));
                collection.Add(new JsonStringValue("subtype2", subtype));
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
                Business.Base.BusinessMeter bc = new project.Business.Base.BusinessMeter();
                bc.load(jp.getValue("id"));

                if (obj.PopulateDataSet("select 1 from Op_ChangeMeter where OldMeterNo='" + bc.Entity.MeterNo + "' or NewMeterNo='" + bc.Entity.MeterNo + "'").Tables[0].Rows.Count > 0)
                {
                    flag = "3";
                }
                else
                {
                    if (obj.PopulateDataSet("select 1 from Op_Readout where MeterNo='" + bc.Entity.MeterNo + "'").Tables[0].Rows.Count > 0)
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
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MeterLOCNo1S"), jp.getValue("MeterLOCNo2S"), jp.getValue("MeterLOCNo3S"),
                jp.getValue("MeterLOCNo4S"), jp.getValue("MeterNoS"), jp.getValue("MeterTypeS"), jp.getValue("MeterUsageTypeS"),
                jp.getValue("MeterRMIDS"), jp.getValue("MeterSizeS"), jp.getValue("MeterStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessMeter bc = new project.Business.Base.BusinessMeter();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.MeterName = jp.getValue("MeterName");
                    bc.Entity.MeterType = jp.getValue("MeterType");
                    bc.Entity.MeterLOCNo1 = jp.getValue("MeterLOCNo1");
                    bc.Entity.MeterLOCNo2 = jp.getValue("MeterLOCNo2");
                    bc.Entity.MeterLOCNo3 = jp.getValue("MeterLOCNo3");
                    bc.Entity.MeterLOCNo4 = jp.getValue("MeterLOCNo4");
                    bc.Entity.MeterRate = ParseDecimalForString(jp.getValue("MeterRate"));
                    bc.Entity.MeterDigit =ParseIntForString(jp.getValue("MeterDigit"));
                    bc.Entity.MeterUsageType = jp.getValue("MeterUsageType");
                    bc.Entity.MeterNatureType = jp.getValue("MeterNatureType");
                    //bc.Entity.MeterReadout = ParseDecimalForString(jp.getValue("MeterReadout"));
                    //bc.Entity.MeterReadoutDate = ParseDateForString(jp.getValue("MeterReadoutDate"));
                    bc.Entity.MeterRMID = jp.getValue("MeterRMID");
                    bc.Entity.MeterSize = jp.getValue("MeterSize");
                    bc.Entity.MeterRelatedMeterNo = jp.getValue("MeterRelatedMeterNo");
                    bc.Entity.Addr = jp.getValue("Addr");
                    
                    int r = bc.Save("update");

                    if (r <= 0)
                        flag = "2";
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_Meter where MeterNo='" + jp.getValue("MeterNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.MeterNo = jp.getValue("MeterNo");
                        bc.Entity.MeterName = jp.getValue("MeterName");
                        bc.Entity.MeterType = jp.getValue("MeterType");
                        bc.Entity.MeterLOCNo1 = jp.getValue("MeterLOCNo1");
                        bc.Entity.MeterLOCNo2 = jp.getValue("MeterLOCNo2");
                        bc.Entity.MeterLOCNo3 = jp.getValue("MeterLOCNo3");
                        bc.Entity.MeterLOCNo4 = jp.getValue("MeterLOCNo4");
                        bc.Entity.MeterRate = ParseDecimalForString(jp.getValue("MeterRate"));
                        bc.Entity.MeterDigit = ParseIntForString(jp.getValue("MeterDigit"));
                        bc.Entity.MeterUsageType = jp.getValue("MeterUsageType");
                        bc.Entity.MeterNatureType = jp.getValue("MeterNatureType");
                        //bc.Entity.MeterReadout = ParseDecimalForString(jp.getValue("MeterReadout"));
                        //bc.Entity.MeterReadoutDate = ParseDateForString(jp.getValue("MeterReadoutDate"));
                        bc.Entity.MeterRMID = jp.getValue("MeterRMID");
                        bc.Entity.MeterSize = jp.getValue("MeterSize");
                        bc.Entity.MeterRelatedMeterNo = jp.getValue("MeterRelatedMeterNo");
                        bc.Entity.Addr = jp.getValue("Addr");

                        bc.Entity.MeterCreator = user.Entity.UserName;
                        bc.Entity.MeterCreateDate = GetDate();

                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MeterLOCNo1S"), jp.getValue("MeterLOCNo2S"), jp.getValue("MeterLOCNo3S"),
                jp.getValue("MeterLOCNo4S"), jp.getValue("MeterNoS"), jp.getValue("MeterTypeS"), jp.getValue("MeterUsageTypeS"),
                jp.getValue("MeterRMIDS"), jp.getValue("MeterSizeS"), jp.getValue("MeterStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MeterLOCNo1S"), jp.getValue("MeterLOCNo2S"), jp.getValue("MeterLOCNo3S"),
                jp.getValue("MeterLOCNo4S"), jp.getValue("MeterNoS"), jp.getValue("MeterTypeS"), jp.getValue("MeterUsageTypeS"),
                jp.getValue("MeterRMIDS"), jp.getValue("MeterSizeS"), jp.getValue("MeterStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MeterLOCNo1S"), jp.getValue("MeterLOCNo2S"), jp.getValue("MeterLOCNo3S"),
                jp.getValue("MeterLOCNo4S"), jp.getValue("MeterNoS"), jp.getValue("MeterTypeS"), jp.getValue("MeterUsageTypeS"),
                jp.getValue("MeterRMIDS"), jp.getValue("MeterSizeS"), jp.getValue("MeterStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessMeter bc = new project.Business.Base.BusinessMeter();
                bc.load(jp.getValue("id"));
                if (bc.Entity.MeterStatus == "open")
                    bc.Entity.MeterStatus = "close";
                else
                    bc.Entity.MeterStatus = "open";

                int r = bc.valid();
                if (r <= 0) flag = "2";

                if (bc.Entity.MeterStatus == "close")
                    collection.Add(new JsonStringValue("stat", "<span class=\"label radius\">禁用</span>"));
                else
                    collection.Add(new JsonStringValue("stat", "<span class=\"label label-success radius\">正常</span>"));

                collection.Add(new JsonStringValue("id", jp.getValue("id")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "valid"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MeterLOCNo1S"), jp.getValue("MeterLOCNo2S"), jp.getValue("MeterLOCNo3S"),
                jp.getValue("MeterLOCNo4S"), jp.getValue("MeterNoS"), jp.getValue("MeterTypeS"), jp.getValue("MeterUsageTypeS"),
                jp.getValue("MeterRMIDS"), jp.getValue("MeterSizeS"), jp.getValue("MeterStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string getvalueaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string subtype = "";
            try
            {
                int row = 0;
                Business.Base.BusinessLocation bt = new Business.Base.BusinessLocation();
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, jp.getValue("parent")))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }

                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));
                collection.Add(new JsonStringValue("child", jp.getValue("child")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getvalue"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
    }
}