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
    public partial class WorkPlace : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/WorkPlace.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/WorkPlace.aspx'";
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

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1);

                        Business.Base.BusinessWorkPlaceType bw = new Business.Base.BusinessWorkPlaceType();
                        WPTypeStr += "<select id=\"WPType\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"工位类型不能为空\">";
                        WPTypeStrS += "<select id=\"WPTypeS\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        WPTypeStr += "<option value=''>请选择工位类型</option>";
                        WPTypeStrS += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityWorkPlaceType it in bw.GetListQuery(string.Empty, string.Empty))
                        {
                            WPTypeStr += "<option value='" + it.WPTypeNo + "'>" + it.WPTypeName + "</option>";
                            WPTypeStrS += "<option value='" + it.WPTypeNo + "'>" + it.WPTypeName + "</option>";
                        }
                        WPTypeStr += "</select>";
                        WPTypeStrS += "</select>";

                        Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        WPLOCNo1Str += "<select id=\"WPLOCNo1\" class=\"input-text required\" data-valid=\"isNonEmpty\" data-error=\"园区不能为空\">";
                        WPLOCNo1StrS += "<select id=\"WPLOCNo1S\" class=\"input-text required size-MINI\" style=\"width:120px\">";
                        WPLOCNo1Str += "<option value=''>请选择园区</option>";
                        WPLOCNo1StrS += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        {
                            WPLOCNo1Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                            WPLOCNo1StrS += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        }
                        WPLOCNo1Str += "</select>";
                        WPLOCNo1StrS += "</select>";
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
        protected string WPTypeStr = "";
        protected string WPTypeStrS = "";
        protected string WPLOCNo1Str = "";
        protected string WPLOCNo1StrS = "";

        private string createList(string WPNo, string WPType, string WPLOCNo1, string WPLOCNo2, string WPLOCNo3, string WPLOCNo4, string WPRMID, string WPStatus, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='15%'>工位编号</th>");
            sb.Append("<th width='12%'>房间编号</th>");
            sb.Append("<th width='15%'>园区/建设期/楼栋/楼层</th>");
            sb.Append("<th width='11%'>所属项目</th>");
            sb.Append("<th width='5%'>人数</th>");
            sb.Append("<th width='7%'>每工位单价</th>");
            sb.Append("<th width='6%'>状态</th>");
            sb.Append("<th width='6%'>是否禁用</th>");
            sb.Append("<th width='21%'>地址</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessWorkPlace bc = new project.Business.Base.BusinessWorkPlace();
            foreach (Entity.Base.EntityWorkPlace it in bc.GetListQuery(WPNo, WPType, WPLOCNo1, WPLOCNo2, WPLOCNo3, WPLOCNo4, WPRMID, WPStatus, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.WPNo + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.WPNo + "</td>");
                sb.Append("<td>" + it.WPRMID + "</td>");
                sb.Append("<td>" + it.WPLOCNo1Name + "/" + it.WPLOCNo2Name + "/" + it.WPLOCNo3Name + "/" + it.WPLOCNo4Name + "</td>");
                sb.Append("<td>" + it.WPProject + "</td>");
                sb.Append("<td>" + it.WPSeat + "</td>");
                sb.Append("<td>" + it.WPSeatPrice.ToString("0.####") + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.WPStatus == "use" ? "label-success" : "") + " radius\">" + it.WPStatusName + "</span></td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.WPISEnable == false ? "label-success" : "") + " radius\">" + (it.WPISEnable == false ? "正常" : "已禁用") + "</span></td>");
                sb.Append("<td>" + it.WPAddr + "</td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(WPNo, WPType, WPLOCNo1, WPLOCNo2, WPLOCNo3, WPLOCNo4, WPRMID, WPStatus), pageSize, page, 7));

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
                Business.Base.BusinessWorkPlace bc = new project.Business.Base.BusinessWorkPlace();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("WPNo", bc.Entity.WPNo));
                collection.Add(new JsonStringValue("WPType", bc.Entity.WPType));
                collection.Add(new JsonStringValue("WPSeat", bc.Entity.WPSeat.ToString()));
                collection.Add(new JsonStringValue("WPSeatPrice", bc.Entity.WPSeatPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("WPLOCNo1", bc.Entity.WPLOCNo1));
                collection.Add(new JsonStringValue("WPLOCNo2", bc.Entity.WPLOCNo2));
                collection.Add(new JsonStringValue("WPLOCNo3", bc.Entity.WPLOCNo3));
                collection.Add(new JsonStringValue("WPLOCNo4", bc.Entity.WPLOCNo4));
                collection.Add(new JsonStringValue("WPRMID", bc.Entity.WPRMID));
                collection.Add(new JsonStringValue("WPProject", bc.Entity.WPProject));
                collection.Add(new JsonStringValue("WPAddr", bc.Entity.WPAddr));
                collection.Add(new JsonStringValue("IsStatistics", (bc.Entity.IsStatistics ? "true" : "false")));

                string subtype = "";
                int row = 0;
                Business.Base.BusinessLocation bt = new Business.Base.BusinessLocation();
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.WPLOCNo1))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));

                row = 0;
                subtype = "";
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.WPLOCNo2))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row1", row));
                collection.Add(new JsonStringValue("subtype1", subtype));

                row = 0;
                subtype = "";
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, bc.Entity.WPLOCNo3))
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
                Business.Base.BusinessWorkPlace bc = new project.Business.Base.BusinessWorkPlace();
                bc.load(jp.getValue("id"));

                //if (obj.PopulateDataSet("select 1 from Mstr_WorkPlace where WPNo='" + bc.Entity.WPNo + "'").Tables[0].Rows.Count > 0)
                //{
                //    flag = "3";
                //}
                //else
                //{
                if (bc.Entity.WPStatus == "use")
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
                            syncResult = srv.DeleteResource(bc.Entity.WPNo);
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("WPNoS"), jp.getValue("WPTypeS"), jp.getValue("WPLOCNo1S"), jp.getValue("WPLOCNo2S"),
                jp.getValue("WPLOCNo3S"), jp.getValue("WPLOCNo4S"), jp.getValue("WPRMIDS"), jp.getValue("WPStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessWorkPlace bc = new project.Business.Base.BusinessWorkPlace();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.WPType = jp.getValue("WPType");
                    bc.Entity.WPSeat = ParseIntForString(jp.getValue("WPSeat"));
                    bc.Entity.WPSeatPrice = ParseDecimalForString(jp.getValue("WPSeatPrice"));
                    bc.Entity.WPLOCNo1 = jp.getValue("WPLOCNo1");
                    bc.Entity.WPLOCNo2 = jp.getValue("WPLOCNo2");
                    bc.Entity.WPLOCNo3 = jp.getValue("WPLOCNo3");
                    bc.Entity.WPLOCNo4 = jp.getValue("WPLOCNo4");
                    bc.Entity.WPRMID = jp.getValue("WPRMID");
                    bc.Entity.WPProject = jp.getValue("WPProject");
                    bc.Entity.WPAddr = jp.getValue("WPAddr");
                    bc.Entity.IsStatistics = bool.Parse(jp.getValue("IsStatistics"));
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
                            syncResult = srv.AddOrUpdateWorkPlace(JsonConvert.SerializeObject(bc.Entity));
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
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_WorkPlace where WPNo='" + jp.getValue("WPNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.WPNo = jp.getValue("WPNo");
                        bc.Entity.WPType = jp.getValue("WPType");
                        bc.Entity.WPSeat = ParseIntForString(jp.getValue("WPSeat"));
                        bc.Entity.WPSeatPrice = ParseDecimalForString(jp.getValue("WPSeatPrice"));
                        bc.Entity.WPLOCNo1 = jp.getValue("WPLOCNo1");
                        bc.Entity.WPLOCNo2 = jp.getValue("WPLOCNo2");
                        bc.Entity.WPLOCNo3 = jp.getValue("WPLOCNo3");
                        bc.Entity.WPLOCNo4 = jp.getValue("WPLOCNo4");
                        bc.Entity.WPRMID = jp.getValue("WPRMID");
                        bc.Entity.WPProject = jp.getValue("WPProject");
                        bc.Entity.WPAddr = jp.getValue("WPAddr");
                        bc.Entity.IsStatistics = bool.Parse(jp.getValue("IsStatistics"));
                        bc.Entity.WPCreator = user.Entity.UserName;
                        bc.Entity.WPCreateDate = GetDate();

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
                                syncResult = srv.AddOrUpdateWorkPlace(JsonConvert.SerializeObject(bc.Entity));
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("WPNoS"), jp.getValue("WPTypeS"), jp.getValue("WPLOCNo1S"), jp.getValue("WPLOCNo2S"),
                jp.getValue("WPLOCNo3S"), jp.getValue("WPLOCNo4S"), jp.getValue("WPRMIDS"), jp.getValue("WPStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("WPNoS"), jp.getValue("WPTypeS"), jp.getValue("WPLOCNo1S"), jp.getValue("WPLOCNo2S"),
                jp.getValue("WPLOCNo3S"), jp.getValue("WPLOCNo4S"), jp.getValue("WPRMIDS"), jp.getValue("WPStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("WPNoS"), jp.getValue("WPTypeS"), jp.getValue("WPLOCNo1S"), jp.getValue("WPLOCNo2S"),
                jp.getValue("WPLOCNo3S"), jp.getValue("WPLOCNo4S"), jp.getValue("WPRMIDS"), jp.getValue("WPStatusS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessWorkPlace bc = new project.Business.Base.BusinessWorkPlace();
                bc.load(jp.getValue("id"));
                bc.Entity.WPISEnable = !bc.Entity.WPISEnable;

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
                        syncResult = srv.AddOrUpdateWorkPlace(JsonConvert.SerializeObject(bc.Entity));
                    }
                    catch (Exception ex)
                    {
                        syncResult = ex.ToString();
                    }
                    collection.Add(new JsonStringValue("sync", syncResult));

                    #endregion
                }
                if (bc.Entity.WPISEnable)
                    collection.Add(new JsonStringValue("stat", "<span class=\"label radius\">禁用</span>"));
                else
                    collection.Add(new JsonStringValue("stat", "<span class=\"label label-success radius\">正常</span>"));

                collection.Add(new JsonStringValue("id", jp.getValue("id")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "valid"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("WPNoS"), jp.getValue("WPTypeS"), jp.getValue("WPLOCNo1S"), jp.getValue("WPLOCNo2S"),
                jp.getValue("WPLOCNo3S"), jp.getValue("WPLOCNo4S"), jp.getValue("WPRMIDS"), jp.getValue("WPStatusS"), ParseIntForString(jp.getValue("page")))));
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