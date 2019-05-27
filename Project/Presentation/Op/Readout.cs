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
using NPOI.HSSF.UserModel;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace project.Presentation.Op
{
    public partial class Readout : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
        protected string userid = "";
        Data sqlExecute = new Data();
        Business.Sys.BusinessUserInfo user = new project.Business.Sys.BusinessUserInfo();
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //
                HttpCookie hc = getCookie("1");
                if (hc != null)
                {
                    string str = hc.Value.Replace("%3D", "=");
                    userid = Encrypt.DecryptDES(str, "1");
                    user.load(userid);
                    CheckRight(user.Entity, "pm/Op/Readout.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/Readout.aspx'";
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
                                if (rightCode.IndexOf("unapprove") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"unaudit()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe6dd;</i> 取消审核</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("excel") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"excel()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 导出Excel</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"audit()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 审核</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"unaudit()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe6dd;</i> 取消审核</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"excel()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 导出Excel</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "", "", 1);
                        date = GetDate().ToString("yyyy-MM-dd");
                        //抄表人数据组装
                        ROOperatorStr = "<select class=\"input-text required\" id=\"ROOperator\" data-valid=\"isNonEmpty\" data-error=\"抄表人不能为空\">";
                        ROOperatorStr += "<option value=\"\" selected></option>";
                        Business.Base.BusinessMeterReader bc1 = new project.Business.Base.BusinessMeterReader();
                        foreach (Entity.Base.EntityMeterReader it in bc1.GetListQuery(string.Empty, string.Empty, "open"))
                        {
                            ROOperatorStr += "<option value='" + it.ReaderNo + "'>" + it.ReaderName + "</option>";
                        }
                        ROOperatorStr += "</select>";
                        //楼栋数据组装
                        //loc3List = "<option value=''>全部</option>";
                        Data objdata = new Data();
                        DataTable loc3Dt = objdata.PopulateDataSet("select * from Mstr_Location where LOCLevel= 3").Tables[0];
                        if (loc3Dt != null)
                        {
                            Business.Base.BusinessLocation location = new Business.Base.BusinessLocation();
                            foreach (Entity.Base.EntityLocation item in location.Query(loc3Dt))
                            {
                                loc3List += string.Format("<option value='{0}'>{1}</option>", item.LOCNo, item.LOCName);
                            }
                        }

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
        protected string ROOperatorStr = "";
        protected string treelist = "";
        protected string loc3List = string.Empty;
        private string createList(string Loc3, string Loc4, string MeterNo, string RMID, string ReadoutType, string AuditStatus, string MeterType, string MinRODate, string MaxRODate, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"3%\">序号</th>");
            sb.Append("<th width='9%'>房间号</th>");
            sb.Append("<th width='9%'>表计编号</th>");
            sb.Append("<th width='6%'>表记类型</th>");
            sb.Append("<th width='7%'>抄表类别</th>");
            sb.Append("<th width='7%'>上期读数</th>");
            sb.Append("<th width='7%'>本期读数</th>");
            sb.Append("<th width='8%'>关联表记行度</th>");
            sb.Append("<th width='7%'>行度</th>");
            sb.Append("<th width='4%'>换表</th>");
            sb.Append("<th width='7%'>原表记行度</th>");
            sb.Append("<th width='8%'>抄表图片</th>");
            //sb.Append("<th width='8%'>不通过原因</th>");
            sb.Append("<th width='8%'>抄表日期</th>");
            sb.Append("<th width='6%'>状态</th>");
            sb.Append("<th width='6%'>订单引用</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            DateTime MinRODateS = default(DateTime);
            DateTime MaxRODateS = default(DateTime);
            if (MinRODate != "") MinRODateS = ParseDateForString(MinRODate);
            if (MaxRODate != "") MaxRODateS = ParseDateForString(MaxRODate);

            int r = 1;
            sb.Append("<tbody>");
            Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
            foreach (Entity.Op.EntityReadout it in bc.GetListQuery(Loc3, Loc4, MeterNo, RMID, ReadoutType, AuditStatus, MeterType, MinRODateS, MaxRODateS, page, pageSize))
            {
                string id = Guid.NewGuid().ToString();
                sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.RMNo + "</td>");
                sb.Append("<td class='td-meterno'>" + it.MeterNo + "</td>");
                sb.Append("<td>" + it.MeterTypeName + "</td>");
                sb.Append("<td>" + it.ReadoutTypeName + "</td>");
                sb.Append("<td>" + it.LastReadout.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.Readout.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.JoinReadings.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.Readings.ToString("0.####") + "</td>");
                sb.Append("<td>" + (it.IsChange ? "是" : "否") + "</td>");
                sb.Append("<td>" + it.OldMeterReadings.ToString("0.####") + "</td>");
                //sb.Append("<td>" + it.AuditReason + "</td>");
                if (!string.IsNullOrEmpty(it.Img))
                {
                    sb.Append("<td><div id='" + id + "' class='readoutImg'><img src='../../upload/meter/" + it.Img + "'/></div></td>");
                }
                else sb.Append("<td></td>");

                sb.Append("<td>" + ParseStringForDate(it.RODate) + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.AuditStatus == "1" ? "label-success" : "") + " radius\">" + it.AuditStatusName + "</span></td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.IsOrder ? "label-success" : "") + " radius\">" + (it.IsOrder ? "已引用" : "未引用") + "</span></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(MeterNo, RMID, ReadoutType, AuditStatus, MeterType, MinRODateS, MaxRODateS), pageSize, page, 7));

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
                result = insertOrUpdateAction(jp);
            else if (jp.getValue("Type") == "submit1")
                result = insertOrUpdateAction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            else if (jp.getValue("Type") == "excel")
                result = excelaction(jp);
            else if (jp.getValue("Type") == "audit")
                result = auditaction(jp);
            else if (jp.getValue("Type") == "unaudit")
                result = unauditaction(jp);
            else if (jp.getValue("Type") == "gettreelist")
                result = gettreeaction(jp);
            else if (jp.getValue("Type") == "getMeterInfo")
                result = getMeterInfoaction(jp);
            else if (jp.getValue("Type") == "getLoc4")
                result = getLoc4action(jp);
            return result;
        }
        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
                bc.load(jp.getValue("id"));
                if (bc.Entity.AuditStatus != "0")
                {
                    flag = "3";
                }
                else
                {
                    collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                    collection.Add(new JsonStringValue("MeterNo", bc.Entity.MeterNo));
                    collection.Add(new JsonStringValue("ReadoutType", bc.Entity.ReadoutType));
                    collection.Add(new JsonStringValue("MeterType", bc.Entity.MeterType));
                    collection.Add(new JsonStringValue("LastReadout", bc.Entity.LastReadout.ToString("0.####")));
                    collection.Add(new JsonStringValue("Readout", bc.Entity.Readout.ToString("0.####")));
                    collection.Add(new JsonStringValue("JoinReadings", bc.Entity.JoinReadings.ToString("0.####")));
                    collection.Add(new JsonStringValue("Readings", bc.Entity.Readings.ToString("0.####")));
                    collection.Add(new JsonStringValue("MeteRate", bc.Entity.MeteRate.ToString("0.####")));
                    //collection.Add(new JsonStringValue("OldMeterReadings", bc.Entity.OldMeterReadings.ToString("0.####")));
                    collection.Add(new JsonStringValue("ROOperator", bc.Entity.ROOperator));
                    collection.Add(new JsonStringValue("RODate", ParseStringForDate(bc.Entity.RODate)));
                    collection.Add(new JsonStringValue("Img", bc.Entity.Img));

                    Business.Base.BusinessMeter met = new Business.Base.BusinessMeter();
                    met.load(bc.Entity.MeterNo);
                    collection.Add(new JsonStringValue("MeterDigit", met.Entity.MeterDigit.ToString()));
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
                Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
                bc.load(jp.getValue("id"));
                string imgPath = Server.MapPath("~/upload/meter/") + bc.Entity.Img;
                if (bc.Entity.AuditStatus != "0")
                {
                    flag = "3";
                }
                else
                {
                    int r = bc.delete();
                    if (r <= 0)
                        flag = "2";
                    else
                    {
                        if (File.Exists(imgPath)) File.Delete(imgPath);
                        obj.ExecuteNonQuery("delete from Op_Readout_ChangeList where RefRP='" + bc.Entity.RowPointer + "'");

                    }

                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNoS"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    //bc.Entity.RMID = jp.getValue("RMID");
                    bc.Entity.MeterNo = jp.getValue("MeterNo");

                    string isOk = "1";
                    try
                    {
                        Business.Base.BusinessMeter bm = new Business.Base.BusinessMeter();
                        bm.load(jp.getValue("MeterNo"));
                        bc.Entity.RMID = bm.Entity.MeterRMID;
                    }
                    catch
                    {
                        isOk = "0";
                    }

                    if (isOk == "1")
                    {
                        bc.Entity.ReadoutType = jp.getValue("ReadoutType");
                        bc.Entity.LastReadout = ParseDecimalForString(jp.getValue("LastReadout"));
                        bc.Entity.Readout = ParseDecimalForString(jp.getValue("Readout"));
                        bc.Entity.JoinReadings = ParseDecimalForString(jp.getValue("JoinReadings"));
                        bc.Entity.Readings = ParseDecimalForString(jp.getValue("Readings"));
                        //bc.Entity.MeteRate = ParseDecimalForString(jp.getValue("MeteRate"));
                        bc.Entity.ROOperator = jp.getValue("ROOperator");
                        bc.Entity.RODate = ParseDateForString(jp.getValue("RODate"));
                        bc.Entity.Img = jp.getValue("Img");
                        //bc.Entity.OldMeterReadings = ParseDecimalForString(jp.getValue("OldMeterReadings"));

                        int r = bc.Save("update");
                        if (r <= 0)
                            flag = "2";
                    }
                    else
                    {
                        flag = "3";
                    }
                    collection.Add(new JsonStringValue("MeterNo", ""));
                    collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNoS"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                        jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
                }
                else
                {
                    string id = Guid.NewGuid().ToString();
                    bc.Entity.RowPointer = id;
                    //bc.Entity.RMID = jp.getValue("RMID");
                    bc.Entity.MeterNo = jp.getValue("MeterNo");

                    string isOk = "1";
                    try
                    {
                        Business.Base.BusinessMeter bm = new Business.Base.BusinessMeter();
                        bm.load(jp.getValue("MeterNo"));
                        bc.Entity.RMID = bm.Entity.MeterRMID;
                        bc.Entity.MeterType = bm.Entity.MeterType;
                        bc.Entity.MeteRate = bm.Entity.MeterRate;
                    }
                    catch
                    {
                        isOk = "0";
                    }

                    if (isOk == "1")
                    {
                        bc.Entity.ReadoutType = jp.getValue("ReadoutType");
                        bc.Entity.LastReadout = ParseDecimalForString(jp.getValue("LastReadout"));
                        bc.Entity.Readout = ParseDecimalForString(jp.getValue("Readout"));
                        bc.Entity.JoinReadings = ParseDecimalForString(jp.getValue("JoinReadings"));
                        bc.Entity.Readings = ParseDecimalForString(jp.getValue("Readings"));
                        bc.Entity.ROOperator = jp.getValue("ROOperator");
                        bc.Entity.RODate = ParseDateForString(jp.getValue("RODate"));
                        bc.Entity.Img = jp.getValue("Img");
                        bc.Entity.ROCreateDate = GetDate();
                        bc.Entity.ROCreator = user.Entity.UserName;

                        //获取换表记录 [换表记录已审核，并没有被抄表引用]
                        DataTable dt = obj.PopulateDataSet("select RowPointer,OldMeterReadings from Op_ChangeMeter " +
                            "where AuditStatus='1' and NewMeterNo='" + bc.Entity.MeterNo + "' " +
                            "and RowPointer not in (select ChangeID from Op_Readout_ChangeList)").Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            bc.Entity.IsChange = true;
                            decimal OldMeterReadings = 0;

                            //考虑多次换表的情况
                            foreach (DataRow dr in dt.Rows)
                            {
                                OldMeterReadings += ParseDecimalForString(dr["OldMeterReadings"].ToString());
                            }
                            bc.Entity.OldMeterReadings = OldMeterReadings;
                        }
                        else
                        {
                            bc.Entity.IsChange = false;
                            bc.Entity.OldMeterReadings = 0;
                        }

                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                        else
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                obj.ExecuteNonQuery("insert into Op_Readout_ChangeList(RowPointer,RefRP,ChangeID) values(newid(),'" + id + "','" + dr["RowPointer"].ToString() + "')");
                            }
                        }
                    }
                    else
                    {
                        flag = "3";
                    }
                    collection.Add(new JsonStringValue("MeterNo", jp.getValue("MeterNo")));
                    collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNo"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                        jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string submit1action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    //bc.Entity.RMID = jp.getValue("RMID");
                    bc.Entity.MeterNo = jp.getValue("MeterNo");

                    string isOk = "1";
                    try
                    {
                        Business.Base.BusinessMeter bm = new Business.Base.BusinessMeter();
                        bm.load(jp.getValue("MeterNo"));
                        bc.Entity.RMID = bm.Entity.MeterRMID;
                    }
                    catch
                    {
                        isOk = "0";
                    }

                    if (isOk == "1")
                    {
                        bc.Entity.ReadoutType = jp.getValue("ReadoutType");
                        bc.Entity.LastReadout = ParseDecimalForString(jp.getValue("LastReadout"));
                        bc.Entity.Readout = ParseDecimalForString(jp.getValue("Readout"));
                        bc.Entity.JoinReadings = ParseDecimalForString(jp.getValue("JoinReadings"));
                        bc.Entity.Readings = ParseDecimalForString(jp.getValue("Readings"));
                        //bc.Entity.MeteRate = ParseDecimalForString(jp.getValue("MeteRate"));
                        bc.Entity.ROOperator = jp.getValue("ROOperator");
                        bc.Entity.RODate = ParseDateForString(jp.getValue("RODate"));
                        bc.Entity.Img = jp.getValue("Img");
                        //bc.Entity.OldMeterReadings = ParseDecimalForString(jp.getValue("OldMeterReadings"));

                        int r = bc.Save("update");
                        if (r <= 0)
                            flag = "2";
                    }
                    else
                    {
                        flag = "3";
                    }
                    collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNoS"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                        jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
                }
                else
                {
                    string id = Guid.NewGuid().ToString();
                    bc.Entity.RowPointer = id;
                    //bc.Entity.RMID = jp.getValue("RMID");
                    bc.Entity.MeterNo = jp.getValue("MeterNo");

                    string isOk = "1";
                    try
                    {
                        Business.Base.BusinessMeter bm = new Business.Base.BusinessMeter();
                        bm.load(jp.getValue("MeterNo"));
                        bc.Entity.RMID = bm.Entity.MeterRMID;
                        bc.Entity.MeterType = bm.Entity.MeterType;
                        bc.Entity.MeteRate = bm.Entity.MeterRate;
                    }
                    catch
                    {
                        isOk = "0";
                    }

                    if (isOk == "1")
                    {
                        bc.Entity.ReadoutType = jp.getValue("ReadoutType");
                        bc.Entity.LastReadout = ParseDecimalForString(jp.getValue("LastReadout"));
                        bc.Entity.Readout = ParseDecimalForString(jp.getValue("Readout"));
                        bc.Entity.JoinReadings = ParseDecimalForString(jp.getValue("JoinReadings"));
                        bc.Entity.Readings = ParseDecimalForString(jp.getValue("Readings"));
                        bc.Entity.ROOperator = jp.getValue("ROOperator");
                        bc.Entity.RODate = ParseDateForString(jp.getValue("RODate"));
                        bc.Entity.Img = jp.getValue("Img");

                        bc.Entity.ROCreateDate = GetDate();
                        bc.Entity.ROCreator = user.Entity.UserName;

                        //获取换表记录 [换表记录已审核，并没有被抄表引用]
                        DataTable dt = obj.PopulateDataSet("select RowPointer,OldMeterReadings from Op_ChangeMeter " +
                            "where AuditStatus='1' and NewMeterNo='" + bc.Entity.MeterNo + "' " +
                            "and RowPointer not in (select ChangeID from Op_Readout_ChangeList)").Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            bc.Entity.IsChange = true;
                            decimal OldMeterReadings = 0;

                            //考虑多次换表的情况
                            foreach (DataRow dr in dt.Rows)
                            {
                                OldMeterReadings += ParseDecimalForString(dr["OldMeterReadings"].ToString());
                            }
                            bc.Entity.OldMeterReadings = OldMeterReadings;
                        }
                        else
                        {
                            bc.Entity.IsChange = false;
                            bc.Entity.OldMeterReadings = 0;
                        }

                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                        else
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                obj.ExecuteNonQuery("insert into Op_Readout_ChangeList(RowPointer,RefRP,ChangeID) values(newid(),'" + id + "','" + dr["RowPointer"].ToString() + "')");
                            }
                        }
                    }
                    else
                    {
                        flag = "3";
                    }
                    collection.Add(new JsonStringValue("MeterNo", jp.getValue("MeterNo")));
                    collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNo"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                        jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "submit1"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string insertOrUpdateAction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            int flag = 0;//0表示失败
            string msg = "数据保存成功！";
            string oldImgName = string.Empty;
            string newImgName = jp.getValue("Img");
            string imgRootPath = HttpContext.Current.Request.MapPath("~/upload/meter/");
            try
            {
                string type = string.IsNullOrEmpty(jp.getValue("id")) ? "insert" : "update";
                Business.Op.BusinessReadout bc = new Business.Op.BusinessReadout();
                //主键
                if (type.Equals("insert")) bc.Entity.RowPointer = Guid.NewGuid().ToString();
                else bc.load(jp.getValue("id"));
                //表记数据
                try
                {
                    Business.Base.BusinessMeter bm = new Business.Base.BusinessMeter();
                    bm.load(jp.getValue("MeterNo"));
                    bc.Entity.MeterNo = bm.Entity.MeterNo;
                    bc.Entity.RMID = bm.Entity.MeterRMID;
                    bc.Entity.MeterType = bm.Entity.MeterType;
                    bc.Entity.MeteRate = bm.Entity.MeterRate;
                }
                catch
                {
                    msg = "保存失败,无当前表记资料！";
                    throw;
                }
                //图片单独处理
                oldImgName = bc.Entity.Img;
                //表单数据
                bc.Entity.ReadoutType = jp.getValue("ReadoutType");
                bc.Entity.LastReadout = ParseDecimalForString(jp.getValue("LastReadout"));
                bc.Entity.Readout = ParseDecimalForString(jp.getValue("Readout"));
                bc.Entity.JoinReadings = ParseDecimalForString(jp.getValue("JoinReadings"));
                bc.Entity.Readings = ParseDecimalForString(jp.getValue("Readings"));
                bc.Entity.ROOperator = jp.getValue("ROOperator");
                bc.Entity.RODate = ParseDateForString(jp.getValue("RODate"));
                bc.Entity.Img = jp.getValue("Img");
                //自动补充数据
                bc.Entity.ROCreateDate = GetDate();
                bc.Entity.ROCreator = user.Entity.UserName;
                /*
                 * 保存数据操作
                 */
                if (type.Equals("insert"))
                {
                    //换表导致的旧表读数（新记录执行此步骤）
                    //备注：获取换表记录 [换表记录已审核，并没有被抄表引用]
                    DataTable dt = obj.PopulateDataSet("select RowPointer,OldMeterReadings from Op_ChangeMeter " +
                        "where AuditStatus='1' and NewMeterNo='" + bc.Entity.MeterNo + "' " +
                        "and RowPointer not in (select ChangeID from Op_Readout_ChangeList)").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        bc.Entity.IsChange = true;
                        decimal OldMeterReadings = 0;

                        //考虑多次换表的情况
                        foreach (DataRow dr in dt.Rows)
                        {
                            OldMeterReadings += ParseDecimalForString(dr["OldMeterReadings"].ToString());
                        }
                        bc.Entity.OldMeterReadings = OldMeterReadings;
                    }
                    else
                    {
                        bc.Entity.IsChange = false;
                        bc.Entity.OldMeterReadings = 0;
                    }
                    //保存
                    if (bc.Save("insert") > 0)
                    {
                        flag = 1;
                        foreach (DataRow dr in dt.Rows)
                        {
                            obj.ExecuteNonQuery("insert into Op_Readout_ChangeList(RowPointer,RefRP,ChangeID) values(newid(),'" + bc.Entity.RowPointer + "','" + dr["RowPointer"].ToString() + "')");
                        }
                    }
                    else msg = "保存失败";
                }
                else
                {
                    //更新
                    if (bc.Save("update") > 0) flag = 1;
                    else msg = "更新失败！";
                }
                /*
                 * 图片操作
                 */
                if (flag == 0 && !string.IsNullOrEmpty(newImgName))
                {
                    //失败的情况下，有新图片，删除新图片
                    if (File.Exists(imgRootPath + newImgName)) File.Delete(imgRootPath + newImgName);
                }
                else if ((flag == 1 && !string.IsNullOrEmpty(newImgName) && !string.IsNullOrEmpty(oldImgName) && !oldImgName.Equals(newImgName)) ||
                    (flag == 1 && !string.IsNullOrEmpty(oldImgName) && string.IsNullOrEmpty(newImgName)))
                {
                    //成功的情况下，有新图片，有旧图片，并且两个图片不一样，删除旧图片
                    //成功的情况下，没有新图片，有旧图片，删除旧图片
                    if (File.Exists(imgRootPath + oldImgName)) File.Delete(imgRootPath + oldImgName);
                }
                //查询返回数据
                collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNo"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                        jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
            }
            catch (Exception ex)
            {
                //异常的情况下，有新图片，删除新图片
                if (File.Exists(imgRootPath + newImgName)) File.Delete(imgRootPath + newImgName);
                //异常提示
                if (!msg.Contains("成功")) msg = "保存失败,数据异常!";
                collection.Add(new JsonStringValue("ex", ex.Message));
            }
            collection.Add(new JsonStringValue("type", jp.getValue("Type")));
            collection.Add(new JsonStringValue("flag", flag.ToString()));
            collection.Add(new JsonStringValue("msg", msg));
            return collection.ToString();

        }
        private string auditaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNoS"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string unauditaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
                bc.load(jp.getValue("id"));

                string InfoMsg = bc.unaudit(user.Entity.UserName);
                if (InfoMsg != "")
                {
                    flag = "3";
                    collection.Add(new JsonStringValue("InfoMsg", InfoMsg));
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "unaudit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNoS"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNoS"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNoS"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), jp.getValue("MinRODate"), jp.getValue("MaxRODate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string excelaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            try
            {
                pathName = "抄表记录" + GetDate().ToString("yyMMddHHmmss") + getRandom(4) + ".xls";

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("抄表记录");
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("序号");
                headerRow.CreateCell(1).SetCellValue("房间号");
                headerRow.CreateCell(2).SetCellValue("表计编号");
                headerRow.CreateCell(3).SetCellValue("表记类型");
                headerRow.CreateCell(4).SetCellValue("倍率");
                headerRow.CreateCell(5).SetCellValue("位数");
                headerRow.CreateCell(6).SetCellValue("抄表类别");
                headerRow.CreateCell(7).SetCellValue("上期读数");
                headerRow.CreateCell(8).SetCellValue("本期读数");
                headerRow.CreateCell(9).SetCellValue("关联表记行度");
                headerRow.CreateCell(10).SetCellValue("行度");
                headerRow.CreateCell(11).SetCellValue("换表");
                headerRow.CreateCell(12).SetCellValue("原表记行度");
                headerRow.CreateCell(13).SetCellValue("不通过原因");
                headerRow.CreateCell(14).SetCellValue("抄表日期");
                headerRow.CreateCell(15).SetCellValue("状态");
                headerRow.CreateCell(16).SetCellValue("订单引用");

                DateTime MinRODateS = default(DateTime);
                DateTime MaxRODateS = default(DateTime);
                if (jp.getValue("MinRODate") != "") MinRODateS = ParseDateForString(jp.getValue("MinRODate"));
                if (jp.getValue("MaxRODate") != "") MaxRODateS = ParseDateForString(jp.getValue("MaxRODate"));

                int rowIndex = 1;
                Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
                foreach (Entity.Op.EntityReadout it in bc.GetListQuery(jp.getValue("Loc3"), jp.getValue("Loc4"), jp.getValue("MeterNoS"), jp.getValue("RMIDS"), jp.getValue("ReadoutTypeS"),
                        jp.getValue("AuditStatusS"), jp.getValue("MeterTypeS"), MinRODateS, MaxRODateS))
                {
                    Business.Base.BusinessMeter bm = new Business.Base.BusinessMeter();
                    bm.load(it.MeterNo);

                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(rowIndex.ToString());
                    dataRow.CreateCell(1).SetCellValue(it.RMNo);
                    dataRow.CreateCell(2).SetCellValue(it.MeterNo);
                    dataRow.CreateCell(3).SetCellValue(it.MeterTypeName);
                    dataRow.CreateCell(4).SetCellValue(it.MeteRate.ToString("0"));
                    dataRow.CreateCell(5).SetCellValue(bm.Entity.MeterDigit);
                    dataRow.CreateCell(6).SetCellValue(it.ReadoutTypeName);
                    dataRow.CreateCell(7).SetCellValue(it.LastReadout.ToString("0.####"));
                    dataRow.CreateCell(8).SetCellValue(it.Readout.ToString("0.####"));
                    dataRow.CreateCell(9).SetCellValue(it.JoinReadings.ToString("0.####"));
                    dataRow.CreateCell(10).SetCellValue(it.Readings.ToString("0.####"));
                    dataRow.CreateCell(11).SetCellValue((it.IsChange ? "是" : "否"));
                    dataRow.CreateCell(12).SetCellValue(it.OldMeterReadings.ToString("0.####"));
                    dataRow.CreateCell(13).SetCellValue(it.AuditReason);
                    dataRow.CreateCell(14).SetCellValue(ParseStringForDate(it.RODate));
                    dataRow.CreateCell(15).SetCellValue(it.AuditStatusName);
                    dataRow.CreateCell(16).SetCellValue((it.IsOrder ? "已引用" : "未引用"));

                    dataRow = null;
                    rowIndex++;
                }

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                headerRow = null;
                sheet = null;
                workbook = null;
                FileStream fs = new FileStream(localpath + pathName, FileMode.OpenOrCreate);
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(ms.ToArray());
                fs.Close();
                ms.Close();
                ms.Dispose();
            }
            catch (Exception ex)
            {
                flag = "2";
                collection.Add(new JsonStringValue("ex", ex.ToString()));
            }

            collection.Add(new JsonStringValue("type", "excel"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("path", pathName));
            return collection.ToString();
        }
        private string gettreelistaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            int row = 1;

            JsonObjectCollection collection2 = new JsonObjectCollection();
            Business.Base.BusinessRoom room = new Business.Base.BusinessRoom();
            foreach (Entity.Base.EntityRoom it in room.GetListQuery("", "", "", "", "", "", "", "", false))
            {
                Business.Base.BusinessMeter meter = new Business.Base.BusinessMeter();
                if (meter.GetListCount("", "", "", "", "", "", "", "", it.RMID, "", "open") > 0)
                {
                    JsonObjectCollection collection1 = new JsonObjectCollection();
                    JsonObjectCollection collection4 = new JsonObjectCollection();
                    int row1 = 1;
                    JsonObjectCollection collection3 = new JsonObjectCollection();
                    foreach (Entity.Base.EntityMeter it1 in meter.GetListQuery("", "", "", "", "", "", "", "", it.RMID, "", "open"))
                    {
                        collection3.Add(new JsonStringValue("MeterNo", it1.MeterNo));
                        collection4.Add(new JsonStringValue(row1.ToString(), collection3.ToString()));
                        row1++;
                    }
                    collection1.Add(new JsonStringValue("RMID", it.RMNo));
                    collection1.Add(new JsonStringValue("Loc3", it.RMLOCNo3Name));
                    collection1.Add(new JsonStringValue("Loc4", it.RMLOCNo4Name));
                    collection1.Add(new JsonStringValue("mlist", collection4.ToString()));
                    collection1.Add(new JsonNumericValue("mcount", row1 - 1));

                    collection2.Add(new JsonStringValue(row.ToString(), collection1.ToString()));
                    row++;
                }
            }
            collection.Add(new JsonStringValue("list", collection2.ToString()));
            collection.Add(new JsonNumericValue("count", row - 1));


            collection.Add(new JsonStringValue("type", "gettreelist"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string gettreeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            int flag = 0;//0代表成功
            string msg = string.Empty;//返回的消息
            try
            {
                string sql = @"select
                a.MeterNo as ID,
                a.MeterRMID as RID,
                b.RMNo as RName,
                a.MeterLOCNo4 as L4ID,
                c.LOCName as L4Name,
                a.MeterLOCNo3 as L3ID,
                d.LOCName as L3Name
                from Mstr_Meter a
                left
                join Mstr_Room b on a.MeterRMID = b.RMID--房间
                left join Mstr_Location c on a.MeterLOCNo4 = c.LOCNo--楼层
                left join Mstr_Location d on a.MeterLOCNo3 = d.LOCNo--楼栋
                where 1 = 1 ";
                if (!string.IsNullOrEmpty(jp.getValue("Loc3"))) sql += string.Format("and a.MeterLOCNo3='{0}'", jp.getValue("Loc3"));
                if (!string.IsNullOrEmpty(jp.getValue("Loc4"))) sql += string.Format("and a.MeterLOCNo4='{0}'", jp.getValue("Loc4"));
                if (!string.IsNullOrEmpty(jp.getValue("RMIDS"))) sql += string.Format("and a.MeterRMID like '%{0}%'", jp.getValue("RMIDS"));
                if (!string.IsNullOrEmpty(jp.getValue("MeterNoS"))) sql += string.Format("and a.MeterNo like'%{0}%'", jp.getValue("MeterNoS"));
                if (!string.IsNullOrEmpty(jp.getValue("ReadoutTypeS"))) sql += string.Format("and a.ReadoutType='{0}'", jp.getValue("ReadoutTypeS"));
                if (!string.IsNullOrEmpty(jp.getValue("MeterTypeS"))) sql += string.Format("and a.MeterType='{0}'", jp.getValue("MeterTypeS"));
                if (!string.IsNullOrEmpty(jp.getValue("AuditStatusS"))) sql += string.Format("and a.AuditStatus='{0}'", jp.getValue("AuditStatusS"));
                if (!string.IsNullOrEmpty(jp.getValue("MinRODate"))) sql += string.Format("and convert(nvarchar(10),a.RODate,121) >= '{0}'", jp.getValue("MinRODate").Trim());
                if (!string.IsNullOrEmpty(jp.getValue("MaxRODate"))) sql += string.Format("and convert(nvarchar(10),a.RODate,121) <='{0}'", jp.getValue("MaxRODate").Trim());
                DataTable treeTable = sqlExecute.PopulateDataSet(sql).Tables[0];
                List<MeterTree> treeList = JsonConvert.DeserializeObject<List<MeterTree>>(JsonConvert.SerializeObject(treeTable));
                List<MeterTreeObj> treeObjList = treeList.GroupBy(a => new { a.L3ID, a.L3Name }).OrderBy(a => a.Key.L3ID).Select(a => new MeterTreeObj
                {
                    //楼栋
                    id = a.Key.L3ID,
                    name = a.Key.L3Name,
                    type = "building",
                    children = treeList.Where(b => b.L3ID == a.Key.L3ID).GroupBy(b => new { b.L4ID, b.L4Name }).OrderBy(b => b.Key.L4ID).Select(b => new MeterTreeObj
                    {
                        //楼层
                        id = b.Key.L4ID,
                        name = b.Key.L4Name,
                        type = "floor"
                    }).ToList()
                }).ToList();
                foreach (var building in treeObjList)
                {
                    foreach (var floor in building.children)
                    {
                        floor.children = new List<MeterTreeObj>();
                        //有房间编码的表记，挂载到房间
                        floor.children.AddRange(treeList.Where(a => a.L4ID == floor.id && !string.IsNullOrEmpty(a.RID)).GroupBy(a => new { a.RID, a.RName }).OrderBy(a => a.Key.RID).Select(a => new MeterTreeObj
                        {
                            id = a.Key.RID,
                            name = a.Key.RName,
                            type = "room",
                            children = treeList.Where(b => b.RID == a.Key.RID).OrderBy(b => b.ID).Select(b => new MeterTreeObj
                            {
                                id = b.ID,
                                name = b.ID,
                                type = "meter"
                            }).ToList()
                        }).ToList());
                        //没有房间编码的表记，直接挂载到楼层
                        floor.children.AddRange(treeList.Where(a => a.L4ID == floor.id && string.IsNullOrEmpty(a.RID)).OrderBy(a => a.ID).Select(a => new MeterTreeObj
                        {
                            id = a.ID,
                            name = a.ID,
                            type = "meter"
                        }).ToList());
                    }
                }
                collection.Add(new JsonStringValue("data", JsonConvert.SerializeObject(treeObjList)));
            }
            catch (Exception ex)
            {
                flag = 99;
                msg = ex.Message;
            }
            collection.Add(new JsonStringValue("type", "gettree"));
            collection.Add(new JsonNumericValue("flag", flag));
            collection.Add(new JsonStringValue("msg", msg));
            return collection.ToString();
        }
        private string getMeterInfoaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string rmid = "";
            decimal readout = 0;
            decimal rate = 0;
            decimal digit = 0;

            DataTable dt = obj.PopulateDataSet("select MeterReadout,MeterRate,MeterDigit,MeterRMID from Mstr_Meter where MeterNo='" + jp.getValue("MeterNo") + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                rmid = dt.Rows[0]["MeterRMID"].ToString();
                readout = ParseDecimalForString(dt.Rows[0]["MeterReadout"].ToString());
                rate = ParseDecimalForString(dt.Rows[0]["MeterRate"].ToString());
                digit = ParseDecimalForString(dt.Rows[0]["MeterDigit"].ToString());
            }

            collection.Add(new JsonStringValue("MeterRMID", rmid));
            collection.Add(new JsonStringValue("MeterNo", jp.getValue("MeterNo")));
            collection.Add(new JsonStringValue("readout", readout.ToString("0.####")));
            collection.Add(new JsonStringValue("rate", rate.ToString("0.####")));
            collection.Add(new JsonStringValue("digit", digit.ToString("0.####")));
            collection.Add(new JsonStringValue("type", "getMeterInfo"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }

        private string getLoc4action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string loc4List = "<option value='' selected>全部</option>";
            string msg = string.Empty;
            try
            {
                DataTable loc4Dt = sqlExecute.PopulateDataSet(string.Format("select * from Mstr_Location where ParentLOCNo='{0}'", jp.getValue("Loc3No"))).Tables[0];
                if (loc4Dt != null)
                {
                    Business.Base.BusinessLocation location = new Business.Base.BusinessLocation();
                    foreach (Entity.Base.EntityLocation item in location.Query(loc4Dt))
                    {
                        loc4List += string.Format("<option value='{0}'>{1}</option>", item.LOCNo, item.LOCName);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            collection.Add(new JsonStringValue("type", "getLoc4"));
            collection.Add(new JsonStringValue("data", loc4List));
            collection.Add(new JsonStringValue("msg", msg));
            return collection.ToString();
        }
    }
    public class MeterTree
    {
        public string ID { get; set; }
        public string RID { get; set; }
        public string RName { get; set; }
        public string L3ID { get; set; }
        public string L3Name { get; set; }
        public string L4ID { get; set; }
        public string L4Name { get; set; }
    }
    public class MeterTreeObj
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<MeterTreeObj> children { get; set; }
    }


}