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
    public partial class Room : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/Room.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/Room.aspx'";
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

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1);

                        Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        RMLOCNo1Str += "<select id=\"RMLOCNo1\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"园区不能为空\">";
                        RMLOCNo1StrS += "<select id=\"RMLOCNo1S\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        RMLOCNo1Str += "<option value=''>请选择园区</option>";
                        RMLOCNo1StrS += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        {
                            RMLOCNo1Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                            RMLOCNo1StrS += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        }
                        RMLOCNo1Str += "</select>";
                        RMLOCNo1StrS += "</select>";
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
        protected string RMLOCNo1Str = "";
        protected string RMLOCNo1StrS = "";

        private string createList(string RMID, string RMLOCNo1, string RMLOCNo2, string RMLOCNo3, string RMLOCNo4, string RMCurrentRMNo, string RMStatus, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='14%'>房间编号</th>");
            sb.Append("<th width='8%'>房间号</th>");
            sb.Append("<th width='14%'>园区/建设期/楼栋/楼层</th>");
            sb.Append("<th width='8%'>出租类型</th>");
            sb.Append("<th width='8%'>出租面积</th>");
            sb.Append("<th width='6%'>状态</th>");
            sb.Append("<th width='6%'>是否禁用</th>");
            sb.Append("<th width='15%'>租客名称</th>");
            sb.Append("<th width='8%'>预留日期</th>");
            sb.Append("<th width='8%'>操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessRoom bc = new project.Business.Base.BusinessRoom();
            foreach (Entity.Base.EntityRoom it in bc.GetListQuery(RMID, RMLOCNo1, RMLOCNo2, RMLOCNo3, RMLOCNo4, RMCurrentRMNo, string.Empty, RMStatus, null, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RMID + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.RMID + "</td>");
                sb.Append("<td>" + it.RMNo + "</td>");
                sb.Append("<td>" + it.RMLOCNo1Name + "/" + it.RMLOCNo2Name + "/" + it.RMLOCNo3Name + "/" + it.RMLOCNo4Name + "</td>");
                sb.Append("<td>" + it.RMRentTypeName + "</td>");
                sb.Append("<td>" + it.RMRentSize.ToString("0.####") + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.RMStatus == "use" ? "label-success" : "") + " radius\">" + it.RMStatusName + "</span></td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.RMISEnable == false ? "label-success" : "") + " radius\">" + (it.RMISEnable == false ? "正常" : "已禁用") + "</span></td>");
                sb.Append("<td>" + it.RMCurrentCustName + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.RMEndReservedDate) + "</td>");
                if (it.RMStatus == "use")
                    sb.Append("<td></td>");
                else if (it.RMStatus == "free")
                    sb.Append("<td><input class=\"btn btn-primary radius size-MINI\" style=\"padding:0px 10px; margin:0px;\" type=\"button\" value=\"预留\" onclick=\"reserve('" + it.RMID + "')\" /></td>");
                else
                    sb.Append("<td><input class=\"btn btn-primary radius size-MINI\" style=\"padding:0px 10px; margin:0px;\" type=\"button\" value=\"取消预留\" onclick=\"unreserve('" + it.RMID + "')\" /></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(RMID, RMLOCNo1, RMLOCNo2, RMLOCNo3, RMLOCNo4, RMCurrentRMNo, string.Empty, RMStatus, null), pageSize, page, 7));

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
                Business.Base.BusinessRoom bc = new project.Business.Base.BusinessRoom();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("RMNo", bc.Entity.RMNo));
                collection.Add(new JsonStringValue("RMLOCNo1", bc.Entity.RMLOCNo1));
                collection.Add(new JsonStringValue("RMLOCNo2", bc.Entity.RMLOCNo2));
                collection.Add(new JsonStringValue("RMLOCNo3", bc.Entity.RMLOCNo3));
                collection.Add(new JsonStringValue("RMLOCNo4", bc.Entity.RMLOCNo4));
                collection.Add(new JsonStringValue("RMRentType", bc.Entity.RMRentType));
                collection.Add(new JsonStringValue("RMBuildSize", bc.Entity.RMBuildSize.ToString("0.####")));
                collection.Add(new JsonStringValue("RMRentSize", bc.Entity.RMRentSize.ToString("0.####")));
                collection.Add(new JsonStringValue("RMAddr", bc.Entity.RMAddr));
                collection.Add(new JsonStringValue("RMRemark", bc.Entity.RMRemark));
                collection.Add(new JsonStringValue("HaveAirCondition", (bc.Entity.HaveAirCondition ? "true" : "false")));
                collection.Add(new JsonStringValue("IsStatistics", (bc.Entity.IsStatistics ? "true" : "false")));

                string subtype = "";
                int row = 0;
                Business.Base.BusinessLocation bt = new Business.Base.BusinessLocation();
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.RMLOCNo1))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));

                row = 0;
                subtype = "";
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.RMLOCNo2))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row1", row));
                collection.Add(new JsonStringValue("subtype1", subtype));

                row = 0;
                subtype = "";
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.RMLOCNo3))
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
                Business.Base.BusinessRoom bc = new project.Business.Base.BusinessRoom();
                bc.load(jp.getValue("id"));

                if (bc.Entity.RMStatus == "use")
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
                        #region 同步到资源系统

                        string syncResult = string.Empty;
                        try
                        {
                            ResourceService.ResourceService srv = new ResourceService.ResourceService();
                            srv.Url = ConfigurationManager.AppSettings["ResourceServiceUrl"].ToString();
                            syncResult = srv.DeleteResource(bc.Entity.RMID);
                        }
                        catch (Exception ex)
                        {
                            syncResult = ex.ToString();
                        }
                        collection.Add(new JsonStringValue("sync", syncResult));

                        #endregion
                    }
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("RMLOCNo1S"), jp.getValue("RMLOCNo2S"), jp.getValue("RMLOCNo3S"),
                jp.getValue("RMLOCNo4S"), jp.getValue("CustNoS"), jp.getValue("RMStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessRoom bc = new project.Business.Base.BusinessRoom();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.RMLOCNo1 = jp.getValue("RMLOCNo1");
                    bc.Entity.RMLOCNo2 = jp.getValue("RMLOCNo2");
                    bc.Entity.RMLOCNo3 = jp.getValue("RMLOCNo3");
                    bc.Entity.RMLOCNo4 = jp.getValue("RMLOCNo4");
                    bc.Entity.RMRentType = jp.getValue("RMRentType");
                    bc.Entity.IsStatistics = bool.Parse(jp.getValue("IsStatistics"));
                    bc.Entity.RMBuildSize = ParseDecimalForString(jp.getValue("RMBuildSize"));
                    bc.Entity.RMRentSize = ParseDecimalForString(jp.getValue("RMRentSize"));
                    bc.Entity.RMAddr = jp.getValue("RMAddr");
                    bc.Entity.RMRemark = jp.getValue("RMRemark");
                    bc.Entity.HaveAirCondition = bool.Parse(jp.getValue("HaveAirCondition"));

                    int r = bc.Save("update");
                    if (r <= 0)
                        flag = "2";
                    else
                    {
                        #region 同步到资源系统

                        string syncResult = string.Empty;
                        try
                        {
                            ResourceService.ResourceService srv = new ResourceService.ResourceService();
                            srv.Url = ConfigurationManager.AppSettings["ResourceServiceUrl"].ToString();
                            syncResult = srv.AddOrUpdateRoom(JsonConvert.SerializeObject(bc.Entity));
                        }
                        catch (Exception ex)
                        {
                            syncResult = ex.ToString();
                        }
                        collection.Add(new JsonStringValue("sync", syncResult));

                        #endregion
                    }
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_Room where RMID='" + jp.getValue("RMID") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.RMID = jp.getValue("RMID");
                        bc.Entity.RMNo = jp.getValue("RMNo");
                        bc.Entity.RMLOCNo1 = jp.getValue("RMLOCNo1");
                        bc.Entity.RMLOCNo2 = jp.getValue("RMLOCNo2");
                        bc.Entity.RMLOCNo3 = jp.getValue("RMLOCNo3");
                        bc.Entity.RMLOCNo4 = jp.getValue("RMLOCNo4");
                        bc.Entity.RMRentType = jp.getValue("RMRentType");
                        bc.Entity.IsStatistics = bool.Parse(jp.getValue("IsStatistics"));
                        bc.Entity.RMBuildSize = ParseDecimalForString(jp.getValue("RMBuildSize"));
                        bc.Entity.RMRentSize = ParseDecimalForString(jp.getValue("RMRentSize"));
                        bc.Entity.RMAddr = jp.getValue("RMAddr");
                        bc.Entity.RMRemark = jp.getValue("RMRemark");
                        bc.Entity.HaveAirCondition = bool.Parse(jp.getValue("HaveAirCondition"));

                        bc.Entity.RMCreator = user.Entity.UserName;
                        bc.Entity.RMCreateDate = GetDate();

                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                        else
                        {
                            #region 同步到资源系统

                            string syncResult = string.Empty;
                            try
                            {
                                ResourceService.ResourceService srv = new ResourceService.ResourceService();
                                srv.Url = ConfigurationManager.AppSettings["ResourceServiceUrl"].ToString();
                                syncResult = srv.AddOrUpdateRoom(JsonConvert.SerializeObject(bc.Entity));
                            }
                            catch (Exception ex)
                            {
                                syncResult = ex.ToString();
                            }
                            collection.Add(new JsonStringValue("sync", syncResult));

                            #endregion
                        }
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("RMLOCNo1S"), jp.getValue("RMLOCNo2S"), jp.getValue("RMLOCNo3S"),
                jp.getValue("RMLOCNo4S"), jp.getValue("CustNoS"), jp.getValue("RMStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("RMLOCNo1S"), jp.getValue("RMLOCNo2S"), jp.getValue("RMLOCNo3S"),
                jp.getValue("RMLOCNo4S"), jp.getValue("CustNoS"), jp.getValue("RMStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("RMLOCNo1S"), jp.getValue("RMLOCNo2S"), jp.getValue("RMLOCNo3S"),
                jp.getValue("RMLOCNo4S"), jp.getValue("CustNoS"), jp.getValue("RMStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessRoom bc = new project.Business.Base.BusinessRoom();
                bc.load(jp.getValue("id"));
                bc.Entity.RMISEnable = !bc.Entity.RMISEnable;

                int r = bc.valid();
                if (r <= 0) flag = "2";
                else
                {
                    #region 同步到资源系统

                    string syncResult = string.Empty;
                    try
                    {
                        ResourceService.ResourceService srv = new ResourceService.ResourceService();
                        srv.Url = ConfigurationManager.AppSettings["ResourceServiceUrl"].ToString();
                        syncResult = srv.AddOrUpdateRoom(JsonConvert.SerializeObject(bc.Entity));
                    }
                    catch (Exception ex)
                    {
                        syncResult = ex.ToString();
                    }
                    collection.Add(new JsonStringValue("sync", syncResult));

                    #endregion
                }
                if (bc.Entity.RMISEnable)
                    collection.Add(new JsonStringValue("stat", "<span class=\"label radius\">禁用</span>"));
                else
                    collection.Add(new JsonStringValue("stat", "<span class=\"label label-success radius\">正常</span>"));

                collection.Add(new JsonStringValue("id", jp.getValue("id")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "valid"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("RMLOCNo1S"), jp.getValue("RMLOCNo2S"), jp.getValue("RMLOCNo3S"),
                jp.getValue("RMLOCNo4S"), jp.getValue("CustNoS"), jp.getValue("RMStatusS"), ParseIntForString(jp.getValue("page")))));
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
                Business.Base.BusinessRoom bc = new project.Business.Base.BusinessRoom();
                bc.load(jp.getValue("id"));
                if (bc.Entity.RMStatus == "free")
                {
                    bc.Entity.RMCurrentCustNo = jp.getValue("ReserveCust");
                    bc.Entity.RMReservedDate = GetDate();
                    bc.Entity.RMEndReservedDate = ParseDateForString(jp.getValue("ReserveDate"));
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("RMLOCNo1S"), jp.getValue("RMLOCNo2S"), jp.getValue("RMLOCNo3S"),
                jp.getValue("RMLOCNo4S"), jp.getValue("CustNoS"), jp.getValue("RMStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string unreserveaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessRoom bc = new project.Business.Base.BusinessRoom();
                bc.load(jp.getValue("id"));
                if (bc.Entity.RMStatus == "reserve")
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMIDS"), jp.getValue("RMLOCNo1S"), jp.getValue("RMLOCNo2S"), jp.getValue("RMLOCNo3S"),
                jp.getValue("RMLOCNo4S"), jp.getValue("CustNoS"), jp.getValue("RMStatusS"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
    }
}