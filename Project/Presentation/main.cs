using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Text;
using System.Net.Json;

namespace project.Presentation
{
    public partial class main : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
        protected Business.Sys.BusinessUserInfo user = new project.Business.Sys.BusinessUserInfo();
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie hc = getCookie("1");
                if (hc != null)
                {
                    string str = hc.Value.Replace("%3D", "=");
                    string userid = Encrypt.DecryptDES(str, "1");
                    user.load(userid);
                    if (!IsCallback)
                    {
                        int r = 0;
                        string locno2 = "";
                        //园区
                        Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        RMLOCNo1Str += "<select id=\"RMLOCNo1\" class=\"input-text size-MINI\" style=\"width:120px;vertical-align:middle\">";
                        foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        {
                            if (r == 0)
                            {
                                RMLOCNo1Str += "<option value='" + it.LOCNo + "' selected='selected'>" + it.LOCName + "</option>";
                                locno = it.LOCNo;
                            }
                            else
                                RMLOCNo1Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                            r++;
                        }
                        RMLOCNo1Str += "</select>";

                        //建设期
                        r = 0;
                        Business.Base.BusinessLocation loc1 = new Business.Base.BusinessLocation();
                        RMLOCNo2Str += "<select id=\"RMLOCNo2\" class=\"input-text size-MINI\" style=\"width:120px;vertical-align:middle\">";
                        foreach (Entity.Base.EntityLocation it in loc1.GetListQuery(string.Empty, string.Empty, locno))
                        {
                            if (r == 0)
                            {
                                RMLOCNo2Str += "<option value='" + it.LOCNo + "' selected='selected'>" + it.LOCName + "</option>";
                                locno1 = it.LOCNo;
                            }
                            else
                                RMLOCNo2Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                            r++;
                        }
                        RMLOCNo2Str += "</select>";

                        //楼栋
                        r = 0;
                        Business.Base.BusinessLocation loc2 = new Business.Base.BusinessLocation();
                        foreach (Entity.Base.EntityLocation it in loc2.GetListQuery(string.Empty, string.Empty, locno1))
                        {
                            if (r == 0)
                            {
                                RMLOCNo3Str += "<span class=\"btn btn-primary radius\" id=\"" + it.LOCNo + "\">" + it.LOCName + "</span>";
                                locno2 = it.LOCNo;
                            }
                            else
                                RMLOCNo3Str += "<span class=\"btn btn-default radius\" id=\"" + it.LOCNo + "\" onclick=\"getRM('" + it.LOCNo + "')\">" + it.LOCName + "</span>";
                            r++;
                        }

                        rmlist = getRMList(locno, locno1, locno2, "", "", "");

                        DataSet ds = GetParkProfile();

                        #region 园区概况 Temp

                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            packprofile += "<dl>";
                            packprofile += "<dt>楼栋</dt>";
                            packprofile += "<dd class=\" text-r\">" + dt.Rows[0]["BUILDING"].ToString() + "</dd>";
                            packprofile += "<dt>房间</dt>";
                            packprofile += "<dd class=\" text-r\">" + dt.Rows[0]["ROOM"].ToString() + "</dd>";
                            packprofile += "<dt>空置</dt>";
                            packprofile += "<dd class=\" text-r\">" + dt.Rows[0]["VACANCY"].ToString() + "</dd>";
                            packprofile += "<dt>空置率</dt>";
                            packprofile += "<dd class=\" text-r\">" + (ParseDecimalForString(dt.Rows[0]["VACANCYRATE"].ToString()) * 100).ToString("0.####") + "%</dd>";
                            packprofile += "<dt>日/月新租</dt>";
                            packprofile += "<dd class=\" text-r\">" + dt.Rows[0]["DAYRENTAL"].ToString() + "/" + dt.Rows[0]["MONTHRENTAL"].ToString() + "</dd>";
                            packprofile += "<dt>日/月退租申请</dt>";
                            packprofile += "<dd class=\" text-r\">" + dt.Rows[0]["DAYLEASE"].ToString() + "/" + dt.Rows[0]["MONTHLEASE"].ToString() + "</dd>";
                            packprofile += "</dl>";
                        } 

                        #endregion

                        #region 事务提醒 Tmp3

                        trans += "<dl>";
                        DataTable dt2 = ds.Tables[2];
                        for (int i = 0; i < 6; i++)
                        {
                            if (i < dt2.Rows.Count)
                            {
                                trans += "<dt>" + dt2.Rows[i]["TranName"].ToString() + "</dd>";
                                trans += "<dd class=\"text-r\">" + dt2.Rows[i]["Cnt"].ToString() + "</dt>";
                            }
                            else
                            {
                                trans += "<dt></dd>";
                                trans += "<dd class=\"text-r\"></dt>";
                            }

                        }
                        trans += "</dl>";
                        #endregion

                        #region 合同到期提醒 Temp2
                        DataTable dt1 = ds.Tables[1];
                        expireRemind += "<table class=\"table\"><tbody><tr><th>客户名称</th><th>合同类别</th><th>退租状态</th><th>合同到期日期</th></tr>";
                        if (dt1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                expireRemind += "<tr><td>" + dt1.Rows[i]["CustName"].ToString() + "</td>";
                                expireRemind += "<td>" + dt1.Rows[i]["CntType"].ToString() + "</td>";
                                expireRemind += "<td>" + dt1.Rows[i]["LeaveStatus"].ToString() + "</td>";
                                expireRemind += "<td>" + ParseStringForDate(ParseDateForString(dt1.Rows[i]["EndDate"].ToString())) + "</td></tr>";
                            }
                        }
                        else
                        {
                            expireRemind += "<tr ><td  colspan=\"4\" style=\"text-align:center;\">无数据</td></tr>";
                        }
                        expireRemind += "<tr></tr></tbody></table>";
                        //expire += "<dl>";
                        //expire += "<dt class=\"titles\">客户</dd>";
                        //expire += "<dd class=\"titles text-r\" style=\"color:orange \">到期日期</dt>";
                        //foreach (DataRow dr in dt1.Rows)
                        //{

                        //    expire += "<dd>" + dr["CustName"].ToString() + "</dd>";
                        //    expire += "<dt class=\"text-r\"><a href=\"#\">" + ParseStringForDate(ParseDateForString(dr["EndDate"].ToString())) + "</a></dt>";
                        //}
                        //expire += "</dl>";

                        #endregion

                        #region 新租提醒 Temp4
                        DataTable dt4 = ds.Tables[3];
                        newRentRemind += "<table class=\"table\"><tbody><tr><th>客户名称</th><th>合同类别</th><th>合同状态</th><th>合同签订日期</th></tr>";
                        if (dt4.Rows.Count > 0) { 
                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            newRentRemind += "<tr><td>" + dt4.Rows[i]["CustName"].ToString() + "</td>";
                            newRentRemind += "<td>" + dt4.Rows[i]["CntType"].ToString() + "</td>";
                            newRentRemind += "<td>" + dt4.Rows[i]["CntStatus"].ToString() + "</td>";
                            newRentRemind += "<td>" + ParseStringForDate(ParseDateForString(dt4.Rows[i]["SingnDate"].ToString())) + "</td></tr>";
                        }
                        }
                        else
                        {
                            newRentRemind += "<tr ><td  colspan=\"4\" style=\"text-align:center;\">无数据</td></tr>";
                        }
                        newRentRemind += "<tr></tr></tbody></table>";
                        #endregion

                        #region 预约退租提醒 Temp5
                        DataTable dt5 = ds.Tables[4];
                        reservationRemind += "<table class=\"table\"><tbody><tr><th>客户名称</th><th>合同类别</th><th>退租状态</th><th>预计退租日期</th></tr>";
                        if (dt5.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt5.Rows.Count; i++)
                            {
                                reservationRemind += "<tr><td>" + dt5.Rows[i]["CustName"].ToString() + "</td>";
                                reservationRemind += "<td>" + dt5.Rows[i]["CntType"].ToString() + "</td>";
                                reservationRemind += "<td>" + dt5.Rows[i]["LeaveStatus"].ToString() + "</td>";
                                reservationRemind += "<td>" + ParseStringForDate(ParseDateForString(dt5.Rows[i]["LeaveDate"].ToString())) + "</td></tr>";
                            }
                        }
                        else
                        {
                            reservationRemind += "<tr ><td  colspan=\"4\" style=\"text-align:center;\">无数据</td></tr>";
                        }
                        reservationRemind += "<tr></tr></tbody></table>";
                        #endregion

                        #region 退租提醒 Temp6
                        DataTable dt6 = ds.Tables[5];
                        endRentRemind += "<table class=\"table\"><tbody><tr><th>客户名称</th><th>合同类别</th><th>退租状态</th><th>实际退租日期</th></tr>";
                        if (dt6.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt6.Rows.Count; i++)
                            {
                                endRentRemind += "<tr><td>" + dt6.Rows[i]["CustName"].ToString() + "</td>";
                                endRentRemind += "<td>" + dt6.Rows[i]["CntType"].ToString() + "</td>";
                                endRentRemind += "<td>" + dt6.Rows[i]["LeaveStatus"].ToString() + "</td>";
                                endRentRemind += "<td>" + ParseStringForDate(ParseDateForString(dt6.Rows[i]["LeaveDate"].ToString())) + "</td></tr>";
                            }
                        }
                        else
                        {
                            endRentRemind += "<tr ><td  colspan=\"4\" style=\"text-align:center;\">无数据</td></tr>";
                        }
                        endRentRemind += "<tr></tr></tbody></table>";
                        #endregion

                    }
                }
                else
                {
                    Response.Write("<script type='text/javascript'>window.parent.window.location.href='login.aspx';</script>");
                    return;
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script type='text/javascript'>window.parent.window.location.href='login.aspx';</script>");
                return;
            }
        }
        protected string RMLOCNo1Str = "";
        protected string RMLOCNo2Str = "";
        protected string RMLOCNo3Str = "";
        protected string locno = "";
        protected string locno1 = "";
        protected string rmlist = "";
        protected string packprofile = "";
        /// <summary>
        /// 合同到期提醒
        /// </summary>
        protected string expireRemind = "";
        /// <summary>
        /// 新租提醒
        /// </summary>
        protected string newRentRemind = "";
        /// <summary>
        /// 预约退租提醒
        /// </summary>
        protected string reservationRemind = "";
        /// <summary>
        /// 退租提醒
        /// </summary>
        protected string endRentRemind = "";
        protected string trans = "";

        private string getRMList(string RMLOCNo1, string RMLOCNo2, string RMLOCNo3, string RMID, string CustName, string RMStatus)
        {
            StringBuilder sb = new StringBuilder();

            Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
            foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, RMLOCNo3))
            {
                sb.Append("<div class=\"mb-10\" style=\"clear:both;\">");
                sb.Append("<h3>" + it.LOCName + "</h3>");

                Business.Base.BusinessRoom br = new Business.Base.BusinessRoom();
                foreach (Entity.Base.EntityRoom it1 in br.GetListQuery(RMID, RMLOCNo1, RMLOCNo2, RMLOCNo3, it.LOCNo, string.Empty, CustName, RMStatus, false))
                {
                    sb.Append("<div class=\"div-room pd-10 radius cl mr-10 mb-10\">");

                    if (it1.RMStatus == "free")
                        sb.Append("<div class=\"room-status c-red \"><label class=\"c-green\">空闲</label></div>");
                    else if (it1.RMStatus == "use")
                        sb.Append("<div class=\"room-status\"><label class=\"c-red\">已租</label></div>");
                    else
                        sb.Append("<div class=\"room-status c-red\"> <label class=\"c-orange\">预留</label></div>");

                    sb.Append("<div class=\"room-text\">" + it1.RMNo + "</div>");
                    sb.Append("<div class=\"text-c  div-name va-m\"><strong>" + it1.RMCurrentCustName + "</strong></div>");
                    sb.Append("<span class=\"span-w text-l dil-b f-l va-m\">" + it1.RMRentSize.ToString("0.####") + "平米</span>");
                    //<span class=\"span-w text-r dil-b f-l va-m\"></span>
                    sb.Append("</div>");
                }
                sb.Append("</div>");
            }

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
            if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "getRM")
                result = getRMaction(jp);
            else if (jp.getValue("Type") == "getvalue")
                result = getvalueaction(jp);
            return result;
        }

        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            string building = "";
            int r = 0;
            string locno = "";
            Business.Base.BusinessLocation loc2 = new Business.Base.BusinessLocation();
            foreach (Entity.Base.EntityLocation it in loc2.GetListQuery(string.Empty, string.Empty, jp.getValue("RMLOCNo2")))
            {
                if (r == 0)
                {
                    building += "<span class=\"btn btn-primary radius\" id=\"" + it.LOCNo + "\">" + it.LOCName + "</span>";
                    locno = it.LOCNo;
                }
                else
                    building += "<span class=\"btn btn-default radius\" id=\"" + it.LOCNo + "\" onclick=\"getRM('" + it.LOCNo + "')\">" + it.LOCName + "</span>";
                r++;
            }

            collection.Add(new JsonStringValue("building", building));
            collection.Add(new JsonStringValue("rmlist", getRMList(jp.getValue("RMLOCNo1"), jp.getValue("RMLOCNo2"), locno, jp.getValue("RMID"), jp.getValue("CustName"), jp.getValue("RMStatus"))));

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }

        private string getRMaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            Business.Base.BusinessLocation bc = new Business.Base.BusinessLocation();
            bc.load(jp.getValue("RMLOCNo3"));

            string building = "";
            Business.Base.BusinessLocation loc2 = new Business.Base.BusinessLocation();
            foreach (Entity.Base.EntityLocation it in loc2.GetListQuery(string.Empty, string.Empty, bc.Entity.ParentLOCNo))
            {
                if (it.LOCNo == jp.getValue("RMLOCNo3"))
                    building += "<span class=\"btn btn-primary radius\" id=\"" + it.LOCNo + "\">" + it.LOCName + "</span>";
                else
                    building += "<span class=\"btn btn-default radius\" id=\"" + it.LOCNo + "\" onclick=\"getRM('" + it.LOCNo + "')\">" + it.LOCName + "</span>";
            }

            collection.Add(new JsonStringValue("building", building));
            collection.Add(new JsonStringValue("rmlist", getRMList(jp.getValue("RMLOCNo1"), jp.getValue("RMLOCNo2"), jp.getValue("RMLOCNo3"), jp.getValue("RMID"), jp.getValue("CustName"), jp.getValue("RMStatus"))));

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
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

        /// </summary>
        /// 获取主页信息 
        /// </summary>
        public DataSet GetParkProfile()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                cmd = new SqlCommand("GetParkProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameter = new SqlParameter[] {};
                cmd.Parameters.AddRange(parameter);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Dispose();
            }
        }
    }
}
