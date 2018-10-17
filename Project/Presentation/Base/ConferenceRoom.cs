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
    public partial class ConferenceRoom : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/ConferenceRoom.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/ConferenceRoom.aspx'";
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
                                if (rightCode.IndexOf("valid") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"valid()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 启用/停用</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"valid()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 启用/停用</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, 1);

                        //Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        //CRLOCNo1Str += "<select id=\"CRLOCNo1\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"园区不能为空\">";
                        //CRLOCNo1StrS += "<select id=\"CRLOCNo1S\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        //CRLOCNo1Str += "<option value=''>请选择园区</option>";
                        //CRLOCNo1StrS += "<option value=''>全部</option>";
                        //foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        //{
                        //    CRLOCNo1Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        //    CRLOCNo1StrS += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        //}
                        //CRLOCNo1Str += "</select>";
                        //CRLOCNo1StrS += "</select>";
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
        //protected string CRLOCNo1Str = "";
        //protected string CRLOCNo1StrS = "";

        private string createList(string CRNo, string CRName, string CRAddr, string CRStatus, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='8%'>会议室编号</th>");
            sb.Append("<th width='12%'>会议室名称</th>");
            sb.Append("<th width='7%'>容纳人数</th>");
            sb.Append("<th width='7%'>对内价格/小时</th>");
            sb.Append("<th width='7%'>对内价格/半天</th>");
            sb.Append("<th width='7%'>对内价格/全天</th>");
            sb.Append("<th width='7%'>对外价格/小时</th>");
            sb.Append("<th width='7%'>对外价格/半天</th>");
            sb.Append("<th width='7%'>对外价格/全天</th>");
            sb.Append("<th width='7%'>押金</th>");
            sb.Append("<th width='6%'>状态</th>");
            sb.Append("<th width='6%'>是否禁用</th>");
            sb.Append("<th width='8%'>操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessConferenceRoom bc = new project.Business.Base.BusinessConferenceRoom();
            foreach (Entity.Base.EntityConferenceRoom it in bc.GetListQuery(CRNo, CRName, CRAddr, CRStatus, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.CRNo + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.CRNo + "</td>");
                sb.Append("<td>" + it.CRName + "</td>");
                sb.Append("<td>" + it.CRCapacity + "</td>");
                sb.Append("<td>" + it.CRINPriceHour.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.CRINPriceHalfDay.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.CRINPriceDay.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.CROUTPriceHour.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.CROUTPriceHalfDay.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.CROUTPriceDay.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.CRDeposit.ToString("0.####") + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.CRStatus == "use" ? "label-success" : "") + " radius\">" + it.CRStatusName + "</span></td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.CRISEnable == false ? "label-success" : "") + " radius\">" + (it.CRISEnable == false ? "正常" : "已禁用") + "</span></td>");
                if (it.CRStatus == "use")
                    sb.Append("<td></td>");
                else if (it.CRStatus == "free")
                    sb.Append("<td><input class=\"btn btn-primary radius size-MINI\" style=\"padding:0px 10px; margin:0px;\" type=\"button\" value=\"预定\" onclick=\"reserve('"+it.CRNo+"')\" /></td>");
                else
                    sb.Append("<td><input class=\"btn btn-primary radius size-MINI\" style=\"padding:0px 10px; margin:0px;\" type=\"button\" value=\"取消预定\" onclick=\"unreserve('" + it.CRNo + "')\" /></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(CRNo, CRName, CRAddr, CRStatus), pageSize, page, 7));

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
            //else if (jp.getValue("Type") == "getvalue")
            //    result = getvalueaction(jp);
            else if (jp.getValue("Type") == "check")
                result = checkaction(jp);
            else if (jp.getValue("Type") == "reserve")
                result = reserveaction(jp);
            else if (jp.getValue("Type") == "unreserve")
                result = unreserveaction(jp);
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
        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessConferenceRoom bc = new project.Business.Base.BusinessConferenceRoom();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("CRNo", bc.Entity.CRNo));
                collection.Add(new JsonStringValue("CRName", bc.Entity.CRName));
                collection.Add(new JsonStringValue("CRCapacity", bc.Entity.CRCapacity));
                collection.Add(new JsonStringValue("CRINPriceHour", bc.Entity.CRINPriceHour.ToString("0.####")));
                collection.Add(new JsonStringValue("CRINPriceHalfDay", bc.Entity.CRINPriceHalfDay.ToString("0.####")));
                collection.Add(new JsonStringValue("CRINPriceDay", bc.Entity.CRINPriceDay.ToString("0.####")));
                collection.Add(new JsonStringValue("CROUTPriceHour", bc.Entity.CROUTPriceHour.ToString("0.####")));
                collection.Add(new JsonStringValue("CROUTPriceHalfDay", bc.Entity.CROUTPriceHalfDay.ToString("0.####")));
                collection.Add(new JsonStringValue("CROUTPriceDay", bc.Entity.CROUTPriceDay.ToString("0.####")));
                collection.Add(new JsonStringValue("CRDeposit", bc.Entity.CRDeposit.ToString("0.####")));
                collection.Add(new JsonStringValue("CRAddr", bc.Entity.CRAddr));

                //string subtype = "";
                //int row = 0;
                //Business.Base.BusinessLocation bt = new Business.Base.BusinessLocation();
                //foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.CRLOCNo1))
                //{
                //    subtype += it.LOCNo + ":" + it.LOCName + ";";
                //    row++;
                //}
                //collection.Add(new JsonNumericValue("row", row));
                //collection.Add(new JsonStringValue("subtype", subtype));

                //row = 0;
                //subtype = "";
                //foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.CRLOCNo2))
                //{
                //    subtype += it.LOCNo + ":" + it.LOCName + ";";
                //    row++;
                //}
                //collection.Add(new JsonNumericValue("row1", row));
                //collection.Add(new JsonStringValue("subtype1", subtype));

                //row = 0;
                //subtype = "";
                //foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.CRLOCNo3))
                //{
                //    subtype += it.LOCNo + ":" + it.LOCName + ";";
                //    row++;
                //}
                //collection.Add(new JsonNumericValue("row2", row));
                //collection.Add(new JsonStringValue("subtype2", subtype));
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
                Business.Base.BusinessConferenceRoom bc = new project.Business.Base.BusinessConferenceRoom();
                bc.load(jp.getValue("id"));

                //if (obj.PopulateDataSet("select 1 from Mstr_ConferenceRoom where CRNo='" + bc.Entity.CRNo + "'").Tables[0].Rows.Count > 0)
                //{
                //    flag = "3";
                //}
                //else
                //{
                if (bc.Entity.CRStatus == "use") 
                {
                    flag = "4";
                }
                else
                {
                    int r = bc.delete();
                    if (r <= 0)
                        flag = "2";
                }
                //}
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag)); 
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CRNoS"), jp.getValue("CRNameS"), jp.getValue("CRAddrS"), jp.getValue("CRStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessConferenceRoom bc = new project.Business.Base.BusinessConferenceRoom();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.CRName = jp.getValue("CRName");
                    bc.Entity.CRCapacity = jp.getValue("CRCapacity");
                    bc.Entity.CRINPriceHour = ParseDecimalForString(jp.getValue("CRINPriceHour"));
                    bc.Entity.CRINPriceHalfDay = ParseDecimalForString(jp.getValue("CRINPriceHalfDay"));
                    bc.Entity.CRINPriceDay = ParseDecimalForString(jp.getValue("CRINPriceDay"));
                    bc.Entity.CROUTPriceHour = ParseDecimalForString(jp.getValue("CROUTPriceHour"));
                    bc.Entity.CROUTPriceHalfDay = ParseDecimalForString(jp.getValue("CROUTPriceHalfDay"));
                    bc.Entity.CROUTPriceDay = ParseDecimalForString(jp.getValue("CROUTPriceDay"));
                    bc.Entity.CRDeposit = ParseDecimalForString(jp.getValue("CRDeposit"));
                    bc.Entity.CRAddr = jp.getValue("CRAddr");
                    
                    int r = bc.Save("update");

                    if (r <= 0)
                        flag = "2";
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_ConferenceRoom where CRNo='" + jp.getValue("CRNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.CRNo = jp.getValue("CRNo");
                        bc.Entity.CRName = jp.getValue("CRName");
                        bc.Entity.CRCapacity = jp.getValue("CRCapacity");
                        bc.Entity.CRINPriceHour = ParseDecimalForString(jp.getValue("CRINPriceHour"));
                        bc.Entity.CRINPriceHalfDay = ParseDecimalForString(jp.getValue("CRINPriceHalfDay"));
                        bc.Entity.CRINPriceDay = ParseDecimalForString(jp.getValue("CRINPriceDay"));
                        bc.Entity.CROUTPriceHour = ParseDecimalForString(jp.getValue("CROUTPriceHour"));
                        bc.Entity.CROUTPriceHalfDay = ParseDecimalForString(jp.getValue("CROUTPriceHalfDay"));
                        bc.Entity.CROUTPriceDay = ParseDecimalForString(jp.getValue("CROUTPriceDay"));
                        bc.Entity.CRDeposit = ParseDecimalForString(jp.getValue("CRDeposit"));
                        bc.Entity.CRAddr = jp.getValue("CRAddr");

                        bc.Entity.CRCreator = user.Entity.UserName;
                        bc.Entity.CRCreateDate = GetDate();
                        
                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CRNoS"), jp.getValue("CRNameS"), jp.getValue("CRAddrS"), jp.getValue("CRStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag)); 
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CRNoS"), jp.getValue("CRNameS"), jp.getValue("CRAddrS"), jp.getValue("CRStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag)); 
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CRNoS"), jp.getValue("CRNameS"), jp.getValue("CRAddrS"), jp.getValue("CRStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessConferenceRoom bc = new project.Business.Base.BusinessConferenceRoom();
                bc.load(jp.getValue("id"));
                bc.Entity.CRISEnable = !bc.Entity.CRISEnable;

                int r = bc.valid();
                if (r <= 0) flag = "2";
                if (bc.Entity.CRISEnable)
                    collection.Add(new JsonStringValue("stat", "<span class=\"label radius\">禁用</span>"));
                else
                    collection.Add(new JsonStringValue("stat", "<span class=\"label label-success radius\">正常</span>"));

                collection.Add(new JsonStringValue("id", jp.getValue("id")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "valid"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CRNoS"), jp.getValue("CRNameS"), jp.getValue("CRAddrS"), jp.getValue("CRStatusS"), ParseIntForString(jp.getValue("page")))));
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
        private string reserveaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessConferenceRoom bc = new project.Business.Base.BusinessConferenceRoom();
                bc.load(jp.getValue("id"));
                if (bc.Entity.CRStatus == "free")
                {
                    bc.Entity.CRCurrentCustNo = jp.getValue("ReserveCust");
                    bc.Entity.CRReservedDate = GetDate();
                    bc.Entity.CRBegReservedDate = ParseDateForString(jp.getValue("BegReserveDate"));
                    bc.Entity.CREndReservedDate = ParseDateForString(jp.getValue("EndReserveDate"));
                    int r = bc.reserve();
                    if (r <= 0)
                        flag = "4";
                }
                else
                    flag = "3";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "reserve"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CRNoS"), jp.getValue("CRNameS"), jp.getValue("CRAddrS"), jp.getValue("CRStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string unreserveaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessConferenceRoom bc = new project.Business.Base.BusinessConferenceRoom();
                bc.load(jp.getValue("id"));
                if (bc.Entity.CRStatus == "reserve")
                {
                    int r = bc.unreserve();
                    if (r <= 0)
                        flag = "4";
                }
                else
                    flag = "3";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "unreserve"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CRNoS"), jp.getValue("CRNameS"), jp.getValue("CRAddrS"), jp.getValue("CRStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }        
    }
}