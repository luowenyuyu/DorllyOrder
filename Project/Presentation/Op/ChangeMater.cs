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

namespace project.Presentation.Op
{
    public partial class ChangeMeter : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/ChangeMeter.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/ChangeMeter.aspx'";
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
                                if (rightCode.IndexOf("approve") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"audit()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 审核</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"audit()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 审核</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, 1);
                        date = GetDate().ToString("yyyy-MM-dd");

                        CMOperatorStr = "<select class=\"input-text required\" id=\"CMOperator\" data-valid=\"isNonEmpty\" data-error=\"抄表人不能为空\">";
                        CMOperatorStr += "<option value=\"\" selected></option>";
                        Business.Base.BusinessMeterReader bc1 = new project.Business.Base.BusinessMeterReader();
                        foreach (Entity.Base.EntityMeterReader it in bc1.GetListQuery(string.Empty, string.Empty, "open"))
                        {
                            CMOperatorStr += "<option value='" + it.ReaderNo + "'>" + it.ReaderName + "</option>";
                        }
                        CMOperatorStr += "</select>";
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
        protected string date = "";
        protected string CMOperatorStr = "";
        private string createList(string RMID, string OldMeterNo, string NewMeterNo, string OldMeterType, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='12%'>房间编号</th>");
            sb.Append("<th width='6%'>表计类别</th>");
            sb.Append("<th width='13%'>原表计编号</th>");
            sb.Append("<th width='13%'>新表计编号</th>");
            sb.Append("<th width='8%'>上期读数</th>");
            sb.Append("<th width='8%'>换表前止数</th>");
            sb.Append("<th width='8%'>换表前行度</th>");
            sb.Append("<th width='8%'>换表后起度</th>");
            sb.Append("<th width='8%'>换表日期</th>");
            sb.Append("<th width='6%'>操作员</th>");
            sb.Append("<th width='6%'>状态</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Op.BusinessChangeMeter bc = new project.Business.Op.BusinessChangeMeter();
            foreach (Entity.Op.EntityChangeMeter it in bc.GetListQuery(RMID, OldMeterNo, NewMeterNo, OldMeterType, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.RMID + "</td>");
                sb.Append("<td>" + it.OldMeterTypeName + "</td>");
                sb.Append("<td>" + it.OldMeterNo + "</td>");
                sb.Append("<td>" + it.NewMeterNo + "</td>");
                sb.Append("<td>" + it.OldMeterLastReadout.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.OldMeterReadout.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.OldMeterReadings.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.NewMeterReadout.ToString("0.####") + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.CMDate) + "</td>");
                sb.Append("<td>" + it.CMOperator + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.AuditStatus=="1" ? "label-success" : "") + " radius\">" + it.AuditStatusName + "</span></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(RMID, OldMeterNo, NewMeterNo, OldMeterType), pageSize, page, 7));

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
            else if (jp.getValue("Type") == "audit")
                result = auditaction(jp);
            else if (jp.getValue("Type") == "getMeterInfo")
                result = getMeterInfoaction(jp);
            return result;
        }
        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessChangeMeter bc = new project.Business.Op.BusinessChangeMeter();
                bc.load(jp.getValue("id"));
                if (bc.Entity.AuditStatus != "0")
                {
                    flag = "3";
                }
                else
                {
                    collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                    collection.Add(new JsonStringValue("OldMeterNo", bc.Entity.OldMeterNo));
                    collection.Add(new JsonStringValue("OldMeterType", bc.Entity.OldMeterType));
                    collection.Add(new JsonStringValue("OldMeterLastReadout", bc.Entity.OldMeterLastReadout.ToString("0.####")));
                    collection.Add(new JsonStringValue("OldMeterReadout", bc.Entity.OldMeterReadout.ToString("0.####")));
                    collection.Add(new JsonStringValue("OldMeterReadings", bc.Entity.OldMeterReadings.ToString("0.####")));
                    collection.Add(new JsonStringValue("NewMeterNo", bc.Entity.NewMeterNo));
                    collection.Add(new JsonStringValue("NewMeterName", bc.Entity.NewMeterName));
                    collection.Add(new JsonStringValue("NewMeterSize", bc.Entity.NewMeterSize));
                    collection.Add(new JsonStringValue("NewMeterReadout", bc.Entity.NewMeterReadout.ToString("0.####")));
                    collection.Add(new JsonStringValue("NewMeterRate", bc.Entity.NewMeterRate.ToString("0.####")));
                    collection.Add(new JsonStringValue("NewMeterDigit", bc.Entity.NewMeterDigit.ToString("0.####")));
                    collection.Add(new JsonStringValue("CMOperator", bc.Entity.CMOperator));
                    collection.Add(new JsonStringValue("CMDate", ParseStringForDate(bc.Entity.CMDate)));
                }
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
                Business.Op.BusinessChangeMeter bc = new project.Business.Op.BusinessChangeMeter();
                bc.load(jp.getValue("id"));

                if (bc.Entity.AuditStatus != "0")
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("OldMeterNoS"), jp.getValue("NewMeterNoS"), jp.getValue("OldMeterTypeS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessChangeMeter bc = new project.Business.Op.BusinessChangeMeter();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    //bc.Entity.RMID = jp.getValue("RMID");
                    bc.Entity.OldMeterNo = jp.getValue("OldMeterNo");
                    
                    string isOk = "1";
                    try
                    {
                        Business.Base.BusinessMeter bm = new Business.Base.BusinessMeter();
                        bm.load(jp.getValue("OldMeterNo"));
                        bc.Entity.RMID = bm.Entity.MeterRMID;
                    }
                    catch
                    {
                        isOk = "0";
                    }

                    if (isOk == "1")
                    {
                        DataTable dt = obj.PopulateDataSet("select cnt=COUNT(1) from Mstr_Meter where MeterNo='" + jp.getValue("NewMeterNo") + "'").Tables[0];
                        if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                            flag = "3"; //新表记编号已存在
                        else
                        {
                            bc.Entity.OldMeterLastReadout = ParseDecimalForString(jp.getValue("OldMeterLastReadout"));
                            bc.Entity.OldMeterReadout = ParseDecimalForString(jp.getValue("OldMeterReadout"));
                            bc.Entity.OldMeterReadings = ParseDecimalForString(jp.getValue("OldMeterReadings"));
                            bc.Entity.NewMeterNo = jp.getValue("NewMeterNo");
                            bc.Entity.NewMeterName = jp.getValue("NewMeterName");
                            bc.Entity.NewMeterSize = jp.getValue("NewMeterSize");
                            bc.Entity.NewMeterReadout = ParseDecimalForString(jp.getValue("NewMeterReadout"));
                            bc.Entity.NewMeterRate = ParseDecimalForString(jp.getValue("NewMeterRate"));
                            bc.Entity.NewMeterDigit = ParseIntForString(jp.getValue("NewMeterDigit"));
                            bc.Entity.CMOperator = jp.getValue("CMOperator");
                            bc.Entity.CMDate = ParseDateForString(jp.getValue("CMDate"));
                            int r = bc.Save();

                            if (r <= 0)
                                flag = "2";
                        }
                    }
                    else
                    {
                        flag = "4";
                    }
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(1) from Mstr_Meter where MeterNo='" + jp.getValue("NewMeterNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3"; //新表记编号已存在
                    else
                    {
                        DataTable dt1 = obj.PopulateDataSet("select MeterType from Mstr_Meter where MeterNo='" + jp.getValue("OldMeterNo") + "'").Tables[0];
                        if (dt1.Rows.Count == 0)
                            flag = "4"; //旧表记编号不存在
                        else
                        {
                            //bc.Entity.RMID = jp.getValue("RMID");
                            bc.Entity.OldMeterNo = jp.getValue("OldMeterNo");

                            Business.Base.BusinessMeter bm = new Business.Base.BusinessMeter();
                            bm.load(jp.getValue("OldMeterNo"));
                            bc.Entity.RMID = bm.Entity.MeterRMID;

                            bc.Entity.OldMeterType = dt1.Rows[0]["MeterType"].ToString();
                            bc.Entity.OldMeterLastReadout = ParseDecimalForString(jp.getValue("OldMeterLastReadout"));
                            bc.Entity.OldMeterReadout = ParseDecimalForString(jp.getValue("OldMeterReadout"));
                            bc.Entity.OldMeterReadings = ParseDecimalForString(jp.getValue("OldMeterReadings"));
                            bc.Entity.NewMeterNo = jp.getValue("NewMeterNo");
                            bc.Entity.NewMeterName = jp.getValue("NewMeterName");
                            bc.Entity.NewMeterSize = jp.getValue("NewMeterSize");
                            bc.Entity.NewMeterReadout = ParseDecimalForString(jp.getValue("NewMeterReadout"));
                            bc.Entity.NewMeterRate = ParseDecimalForString(jp.getValue("NewMeterRate"));
                            bc.Entity.NewMeterDigit = ParseIntForString(jp.getValue("NewMeterDigit"));
                            bc.Entity.CMOperator = jp.getValue("CMOperator");
                            bc.Entity.CMDate = ParseDateForString(jp.getValue("CMDate"));

                            bc.Entity.CMCreateDate = GetDate();
                            bc.Entity.CMCreator = user.Entity.UserName;
                            int r = bc.Save();
                            if (r <= 0)
                                flag = "2";
                        }
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("OldMeterNoS"), jp.getValue("NewMeterNoS"), jp.getValue("OldMeterTypeS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string auditaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessChangeMeter bc = new project.Business.Op.BusinessChangeMeter();
                bc.load(jp.getValue("id"));
                string InfoMsg = bc.audit(user.Entity.UserName);
                if (InfoMsg != "")
                {
                    flag = "3";
                    collection.Add(new JsonStringValue("InfoMsg", InfoMsg));
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "audit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("OldMeterNoS"), jp.getValue("NewMeterNoS"), jp.getValue("OldMeterTypeS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("OldMeterNoS"), jp.getValue("NewMeterNoS"), jp.getValue("OldMeterTypeS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("OldMeterNoS"), jp.getValue("NewMeterNoS"), jp.getValue("OldMeterTypeS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }

        private string getMeterInfoaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            decimal readout = 0;
            try
            {
                DataTable dt = obj.PopulateDataSet("select Top 1 * from Op_Readout where MeterNo='" + jp.getValue("OldMeterNo") + "' and AuditStatus='1' order by ROCreateDate desc").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    readout = ParseDecimalForString(dt.Rows[0]["Readout"].ToString());
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("OldMeterNo", jp.getValue("OldMeterNo")));
            collection.Add(new JsonStringValue("readout", readout.ToString("0.####")));
            collection.Add(new JsonStringValue("type", "getMeterInfo"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
    }
}