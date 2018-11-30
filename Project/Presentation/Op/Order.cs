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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Linq;
namespace project.Presentation.Op
{
    public partial class Order : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    userName = user.Entity.UserName;
                    CheckRight(user.Entity, "pm/Op/Order.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/Order.aspx'";
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
                                if (rightCode.IndexOf("view") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"view()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                if (rightCode.IndexOf("approve") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"auditpass()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 审核通过</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("unapprove") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"raudit()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 反审核</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("print") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"print()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 打印缴费通知单</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("unpayprint") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"unpayprint()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 打未印缴费通知单</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("excelrentorder") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"excelrentorder()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 导出租赁订单</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("excelproporder") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"excelproporder()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 导出物业订单</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("reduce") >= 0)
                                    Buttons += "&nbsp;&nbsp;<a href=\"javascript:;\" onclick=\"getfee()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe6ba;</i> 减免</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"view()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"auditpass()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 审核通过</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"raudit()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 反审核</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"print()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 打印缴费通知单</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"unpayprint()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 打印未缴费通知单</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"excelrentorder()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 导出租赁订单</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"excelproporder()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 导出物业订单</a>&nbsp;&nbsp;";
                            Buttons += "&nbsp;&nbsp;<a href=\"javascript:;\" onclick=\"getfee()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe6ba;</i> 减免</a>&nbsp;&nbsp;";
                        }
                        DateTime now = GetDate();
                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, now.AddDays(-now.Day + 1).ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), string.Empty, "OrderNo", 1, "15", "");

                        date = GetDate().ToString("yyyy-MM-dd");

                        OrderTypeStr = "<select class=\"input-text required\" id=\"OrderType\">";
                        OrderTypeStr += "<option value=\"\"></option>";
                        OrderTypeStrS = "<select class=\"input-text size-MINI\" id=\"OrderTypeS\" style=\"width:120px;\" >";
                        OrderTypeStrS += "<option value=\"\" selected>全部</option>";

                        if (user.Entity.UserType.ToUpper() == "ADMIN")
                        {
                            Business.Base.BusinessOrderType bc = new project.Business.Base.BusinessOrderType();
                            foreach (Entity.Base.EntityOrderType it in bc.GetListQuery(string.Empty, string.Empty))
                            {
                                OrderTypeStr += "<option value='" + it.OrderTypeNo + "'>" + it.OrderTypeName + "</option>";
                                OrderTypeStrS += "<option value='" + it.OrderTypeNo + "'>" + it.OrderTypeName + "</option>";
                            }
                        }
                        else
                        {
                            string sqlstr1 = "select a.OrderType,b.OrderTypeName from Sys_UserOrderTypeRight a left join Mstr_OrderType b on a.OrderType=b.OrderTypeNo " +
                                    "where a.UserType='" + user.Entity.UserType + "'";
                            DataTable dt1 = obj.PopulateDataSet(sqlstr1).Tables[0];
                            foreach (DataRow dr in dt1.Rows)
                            {
                                OrderTypeStr += "<option value='" + dr["OrderType"].ToString() + "'>" + dr["OrderTypeName"].ToString() + "</option>";
                                OrderTypeStrS += "<option value='" + dr["OrderType"].ToString() + "'>" + dr["OrderTypeName"].ToString() + "</option>";
                            }
                        }
                        OrderTypeStr += "</select>";
                        OrderTypeStrS += "</select>";

                        //ODSRVNoStr = "<select class=\"input-text size-MINI\" id=\"ODSRVNo\">";
                        //ODSRVNoStr += "<option value=\"\"></option>";
                        //Business.Base.BusinessService bc2 = new project.Business.Base.BusinessService();
                        //foreach (Entity.Base.EntityService it in bc2.GetListQuery(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty))
                        //{
                        //    ODSRVNoStr += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                        //}
                        //ODSRVNoStr += "</select>";

                        ODContractSPNoStr = "<select class=\"input-text size-MINI\" id=\"ODContractSPNo\">";
                        ODContractSPNoStr += "<option value=\"\"></option>";
                        Business.Base.BusinessServiceProvider bc3 = new project.Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider it in bc3.GetListQuery(string.Empty, string.Empty, true))
                        {
                            ODContractSPNoStr += "<option value='" + it.SPNo + "'>" + it.SPShortName + "</option>";
                        }
                        ODContractSPNoStr += "</select>";


                        SRVTypeNo1Str = "<select class=\"input-text required\" id=\"ODSRVTypeNo1\" data-valid=\"isNonEmpty\" data-error=\"请选择所属服务大类\">";
                        SRVTypeNo1Str += "<option value=\"\" selected></option>";

                        Business.Base.BusinessServiceType bc1 = new project.Business.Base.BusinessServiceType();
                        foreach (Entity.Base.EntityServiceType it in bc1.GetListQuery(string.Empty, string.Empty, "null", string.Empty, true))
                        {
                            SRVTypeNo1Str += "<option value='" + it.SRVTypeNo + "'>" + it.SRVTypeName + "</option>";
                        }
                        SRVTypeNo1Str += "</select>";

                        SRVTypeNo2Str = "<select class=\"input-text required\" id=\"ODSRVTypeNo2\" data-valid=\"isNonEmpty\" data-error=\"请选择所属服务小类\">";
                        SRVTypeNo2Str += "<option value=\"\" selected></option>";
                        SRVTypeNo2Str += "</select>";

                        /*
                        **  主体数据绑定
                        */
                        serviceProvider = "<select class=\"input-text size-MINI\" id=\"serviceProvider\" style=\"width:120px;\" >";
                        serviceProvider += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessServiceProvider sp = new Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider item in sp.GetListQuery("", "", true))
                        {
                            serviceProvider += "<option value='" + item.SPNo + "'>" + item.SPShortName + "</option>";
                        }
                        serviceProvider += "</select>";
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
        protected string userName = "";
        protected string OrderTypeStr = "";
        protected string OrderTypeStrS = "";
        //protected string ODSRVNoStr = "";
        protected string SRVTypeNo1Str = "";
        protected string SRVTypeNo2Str = "";
        protected string ODContractSPNoStr = "";
        protected string serviceProvider = "";

        private string createList(string OrderNo, string OrderType, string CustNo, string OrderTime, string MinOrderCreateDate, string MaxOrderCreateDate,
            string OrderStatus, string OrderField, int page, string PageCount, string serviceProvider)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<td width=\"3%\"><input type=\"checkbox\" class=\"input-text size-MINI\" id=\"checkall\" /></td>");
            sb.Append("<th width=\"3%\">序号</th>");
            sb.Append("<th width=\"8%\">订单编号</th>");
            sb.Append("<th width=\"8%\">订单类别</th>");
            sb.Append("<th width=\"5%\">订单状态</th>");
            sb.Append("<th width=\"5%\">客户编号</th>");
            sb.Append("<th width=\"15%\">客户名称</th>");
            sb.Append("<th width=\"5%\">所属年月</th>");
            sb.Append("<th width=\"7%\">应收日期</th>");
            sb.Append("<th width=\"7%\">应收总金额</th>");
            sb.Append("<th width=\"6%\">减免金额</th>");
            sb.Append("<th width=\"7%\">实收总金额</th>");
            sb.Append("<th width=\"7%\">制单日期</th>");
            sb.Append("<th width=\"6%\">制单人</th>");
            sb.Append("<th width=\"9%\">备注</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            string OrderTimeS = string.Empty;
            if (OrderTime != "") OrderTimeS = OrderTime + "-01";
            if (OrderField == "") OrderField = "OrderNo DESC";

            int pagecnt = 15;
            if (PageCount != "ALL")
            {
                try
                {
                    pagecnt = ParseIntForString(PageCount);
                }
                catch { }
            }
            else
                pagecnt = 1000000;

            string OrderTypes = "";
            if (user.Entity.UserType.ToUpper() == "ADMIN")
            {
                Business.Base.BusinessOrderType bc1 = new project.Business.Base.BusinessOrderType();
                foreach (Entity.Base.EntityOrderType it in bc1.GetListQuery(string.Empty, string.Empty))
                {
                    OrderTypes += (OrderTypes == "" ? "" : ",") + "'" + it.OrderTypeNo + "'";
                }
            }
            else
            {
                string sqlstr1 = "select a.OrderType,b.OrderTypeName from Sys_UserOrderTypeRight a left join Mstr_OrderType b on a.OrderType=b.OrderTypeNo " +
                        "where a.UserType='" + user.Entity.UserType + "'";
                DataTable dt1 = obj.PopulateDataSet(sqlstr1).Tables[0];
                foreach (DataRow dr in dt1.Rows)
                {
                    OrderTypes += (OrderTypes == "" ? "" : ",") + "'" + dr["OrderType"].ToString() + "'";
                }
            }
            if (OrderTypes == "") OrderTypes = "''";

            int r = 1;
            sb.Append("<tbody id=\"tbody\">");
            Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
            bc.OrderField = OrderField;
            foreach (Entity.Op.EntityOrderHeader it in bc.GetListQuery(OrderNo, OrderType, OrderTypes, CustNo, ParseSearchDateForString(OrderTimeS), ParseSearchDateForString(MinOrderCreateDate), ParseSearchDateForString(MaxOrderCreateDate), OrderStatus, page, pagecnt, serviceProvider))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                sb.Append("<td><input type=\"checkbox\" class=\"input-text size-MINI\" value=\"" + it.RowPointer + "\" name=\"chk\" /></td>");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.OrderNo + "</td>");
                sb.Append("<td>" + it.OrderTypeName + "</td>");
                string color = "black";
                if (it.OrderStatus == "1")
                    color = "blue";
                else if (it.OrderStatus == "-1")
                    color = "red";
                else if (it.OrderStatus == "0")
                    color = "black";
                else
                    color = "green";
                sb.Append("<td><font style=\"color:" + color + ";\">" + it.OrderStatusName + "</font></td>");
                sb.Append("<td>" + it.CustNo + "</td>");
                sb.Append("<td>" + it.CustName + "</td>");
                sb.Append("<td>" + it.OrderTime.ToString("yyyy-MM") + "</td>");
                sb.Append("<td>" + it.ARDate.ToString("yyyy-MM-dd") + "</td>");
                sb.Append("<td>" + it.ARAmount.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.ReduceAmount.ToString("0.####") + "</td>");
                sb.Append("<td>" + it.PaidinAmount.ToString("0.####") + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.OrderCreateDate) + "</td>");
                sb.Append("<td>" + it.OrderCreator + "</td>");
                sb.Append("<td>" + it.Remark + "</td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(OrderNo, OrderType, OrderTypes, CustNo, ParseSearchDateForString(OrderTimeS), ParseSearchDateForString(MinOrderCreateDate), ParseSearchDateForString(MaxOrderCreateDate), OrderStatus, serviceProvider), pagecnt, page, 7));
            return sb.ToString();
        }

        //房屋信息
        private string createItemList1(string RefRP, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\" style=\"width:1040px; table-layout:fixed; margin:0px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th style=\"width:5%\">序号</th>");
            sb.Append("<th style=\"width:12%\">订单服务项</th>");
            sb.Append("<th style=\"width:12%\">合同主体</th>");
            sb.Append("<th style=\"width:16%\">资源编号</th>");
            sb.Append("<th style=\"width:8%\">数量</th>");
            sb.Append("<th style=\"width:5%\">单位</th>");
            sb.Append("<th style=\"width:9%\">单价</th>");
            sb.Append("<th style=\"width:10%\">应收金额</th>");
            sb.Append("<th style=\"width:10%\">税额</th>");
            sb.Append("<th style=\"width:12%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;

            sb.Append("<tbody id=\"ItemBody1\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessOrderDetail bc = new project.Business.Op.BusinessOrderDetail();
                foreach (Entity.Op.EntityOrderDetail it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.ODSRVName + "</td>");
                    sb.Append("<td>" + it.ODContractSPName + "</td>");
                    sb.Append("<td>" + it.ResourceNo + "</td>");
                    sb.Append("<td>" + it.ODQTY.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.ODUnit + "</td>");
                    sb.Append("<td>" + it.ODUnitPrice.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.ODARAmount.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.ODTaxAmount.ToString("0.####") + "</td>");
                    if (type != "view")
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate1('" + it.RowPointer + "')\" value=\"修改\" />&nbsp;<input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"itemdel1('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                    else
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate1('" + it.RowPointer + "')\" value=\"查看\" /></td>");
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
            if (jp.getValue("Type") == "insert")
                result = insertaction(jp);
            else if (jp.getValue("Type") == "delete")
                result = deleteaction(jp);
            else if (jp.getValue("Type") == "update")
                result = updateaction(jp);
            else if (jp.getValue("Type") == "submit")
                result = submitaction(jp);
            else if (jp.getValue("Type") == "auditpass")
                result = auditpassaction(jp);
            else if (jp.getValue("Type") == "raudit")
                result = rauditaction(jp);
            else if (jp.getValue("Type") == "auditfailed")
                result = auditfailedaction(jp);
            else if (jp.getValue("Type") == "view")
                result = viewaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            else if (jp.getValue("Type") == "print")
                result = printaction(jp);
            else if (jp.getValue("Type") == "unpayprint")
                result = unpayprintaction(jp);
            else if (jp.getValue("Type") == "excelrentorder")
                result = excelrentorderaction(jp);
            else if (jp.getValue("Type") == "excelproporder")
                result = excelproporderaction(jp);
            else if (jp.getValue("Type") == "checkcust")
                result = checkcustaction(jp);
            else if (jp.getValue("Type") == "check")
                result = checkaction(jp);

            else if (jp.getValue("Type") == "itemsave1")
                result = itemsave1action(jp);
            else if (jp.getValue("Type") == "itemupdate1")
                result = itemupdate1action(jp);
            else if (jp.getValue("Type") == "itemdel1")
                result = itemdel1action(jp);
            else if (jp.getValue("Type") == "ODSRVNoChange")
                result = ODSRVNoChangeaction(jp);
            else if (jp.getValue("Type") == "gettype")
                result = gettypeaction(jp);
            else if (jp.getValue("Type") == "getsubtype")
                result = getsubtypeaction(jp);
            else if (jp.getValue("Type") == "CalcAmount")
                result = CalcAmountaction(jp);
            else if (jp.getValue("Type") == "CalcTax")
                result = CalcTaxaction(jp);

            else if (jp.getValue("Type") == "getfee")
                result = getfeeaction(jp);
            else if (jp.getValue("Type") == "reduce")
                result = reduceaction(jp);
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
        private string checkcustaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                flag = check(jp.getValue("tp"), jp.getValue("val"), collection);
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "checkcust"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string insertaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                string itemlist1 = createItemList1(Guid.Empty.ToString(), "update");
                collection.Add(new JsonStringValue("itemlist1", itemlist1));
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
                Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                bc.load(jp.getValue("id"));
                if (bc.Entity.OrderStatus != "0")
                {
                    flag = "3";
                }
                else
                {
                    collection.Add(new JsonStringValue("OrderNo", bc.Entity.OrderNo));
                    collection.Add(new JsonStringValue("OrderType", bc.Entity.OrderType));
                    collection.Add(new JsonStringValue("CustNo", bc.Entity.CustNo));
                    collection.Add(new JsonStringValue("CustName", bc.Entity.CustName));
                    collection.Add(new JsonStringValue("OrderTime", bc.Entity.OrderTime.ToString("yyyy-MM")));
                    collection.Add(new JsonStringValue("DaysofMonth", bc.Entity.DaysofMonth.ToString("")));
                    collection.Add(new JsonStringValue("ARDate", ParseStringForDate(bc.Entity.ARDate)));
                    collection.Add(new JsonStringValue("ARAmount", bc.Entity.ARAmount.ToString("0.####")));
                    collection.Add(new JsonStringValue("ReduceAmount", bc.Entity.ReduceAmount.ToString("0.####")));
                    collection.Add(new JsonStringValue("PaidinAmount", bc.Entity.PaidinAmount.ToString("0.####")));
                    collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));
                    collection.Add(new JsonStringValue("OrderCreator", bc.Entity.OrderCreator));
                    collection.Add(new JsonStringValue("OrderCreateDate", ParseStringForDate(bc.Entity.OrderCreateDate)));
                    collection.Add(new JsonStringValue("OrderLastReviser", bc.Entity.OrderLastReviser));
                    collection.Add(new JsonStringValue("OrderLastRevisedDate", ParseStringForDate(bc.Entity.OrderLastRevisedDate)));
                    collection.Add(new JsonStringValue("OrderAuditor", bc.Entity.OrderAuditor));
                    collection.Add(new JsonStringValue("OrderAuditDate", ParseStringForDate(bc.Entity.OrderAuditDate)));
                    collection.Add(new JsonStringValue("OrderAuditReason", bc.Entity.OrderAuditReason));
                    collection.Add(new JsonStringValue("OrderRAuditor", bc.Entity.OrderRAuditor));
                    collection.Add(new JsonStringValue("OrderRAuditDate", ParseStringForDate(bc.Entity.OrderRAuditDate)));
                    collection.Add(new JsonStringValue("OrderRAuditReason", bc.Entity.OrderRAuditReason));
                    collection.Add(new JsonStringValue("OrderStatus", bc.Entity.OrderStatusName));

                    collection.Add(new JsonStringValue("itemlist1", createItemList1(bc.Entity.RowPointer, "update")));
                }
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "update"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string viewaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                bc.load(jp.getValue("id"));
                collection.Add(new JsonStringValue("OrderNo", bc.Entity.OrderNo));
                collection.Add(new JsonStringValue("OrderType", bc.Entity.OrderType));
                collection.Add(new JsonStringValue("CustNo", bc.Entity.CustNo));
                collection.Add(new JsonStringValue("CustName", bc.Entity.CustName));
                collection.Add(new JsonStringValue("OrderTime", bc.Entity.OrderTime.ToString("yyyy-MM")));
                collection.Add(new JsonStringValue("DaysofMonth", bc.Entity.DaysofMonth.ToString("")));
                collection.Add(new JsonStringValue("ARDate", ParseStringForDate(bc.Entity.ARDate)));
                collection.Add(new JsonStringValue("ARAmount", bc.Entity.ARAmount.ToString("0.####")));
                collection.Add(new JsonStringValue("ReduceAmount", bc.Entity.ReduceAmount.ToString("0.####")));
                collection.Add(new JsonStringValue("PaidinAmount", bc.Entity.PaidinAmount.ToString("0.####")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));
                collection.Add(new JsonStringValue("OrderCreator", bc.Entity.OrderCreator));
                collection.Add(new JsonStringValue("OrderCreateDate", ParseStringForDate(bc.Entity.OrderCreateDate)));
                collection.Add(new JsonStringValue("OrderLastReviser", bc.Entity.OrderLastReviser));
                collection.Add(new JsonStringValue("OrderLastRevisedDate", ParseStringForDate(bc.Entity.OrderLastRevisedDate)));
                collection.Add(new JsonStringValue("OrderAuditor", bc.Entity.OrderAuditor));
                collection.Add(new JsonStringValue("OrderAuditDate", ParseStringForDate(bc.Entity.OrderAuditDate)));
                collection.Add(new JsonStringValue("OrderAuditReason", bc.Entity.OrderAuditReason));
                collection.Add(new JsonStringValue("OrderRAuditor", bc.Entity.OrderRAuditor));
                collection.Add(new JsonStringValue("OrderRAuditDate", ParseStringForDate(bc.Entity.OrderRAuditDate)));
                collection.Add(new JsonStringValue("OrderRAuditReason", bc.Entity.OrderRAuditReason));
                collection.Add(new JsonStringValue("OrderStatus", bc.Entity.OrderStatusName));

                collection.Add(new JsonStringValue("itemlist1", createItemList1(bc.Entity.RowPointer, "view")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "view"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string deleteaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                bc.load(jp.getValue("id"));
                if (bc.Entity.OrderStatus != "0")
                {
                    flag = "3";
                }
                else
                {
                    string InfoBar = bc.delete();
                    if (InfoBar != "")
                    {
                        flag = "4";
                        collection.Add(new JsonStringValue("InfoBar", InfoBar));
                    }
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"),
                jp.getValue("OrderField"), ParseIntForString(jp.getValue("page")), jp.getValue("PageCount"), jp.getValue("serviceProvider"))));
            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.OrderNo = jp.getValue("OrderNo");
                    bc.Entity.OrderType = jp.getValue("OrderType");
                    bc.Entity.CustNo = jp.getValue("CustNo");
                    bc.Entity.OrderTime = ParseDateForString(jp.getValue("OrderTime"));
                    bc.Entity.DaysofMonth = ParseIntForString(jp.getValue("DaysofMonth"));
                    bc.Entity.ARDate = ParseDateForString(jp.getValue("ARDate"));
                    //bc.Entity.ARAmount = ParseDecimalForString(jp.getValue("ARAmount"));
                    //bc.Entity.ReduceAmount = ParseDecimalForString(jp.getValue("ReduceAmount"));
                    //bc.Entity.PaidinAmount = ParseDecimalForString(jp.getValue("PaidinAmount"));
                    bc.Entity.Remark = jp.getValue("Remark");

                    bc.Entity.OrderLastReviser = user.Entity.UserName;
                    bc.Entity.OrderLastRevisedDate = GetDate();
                    int r = bc.Save("update");
                    if (r <= 0)
                        flag = "2";
                    collection.Add(new JsonStringValue("RowPointer", jp.getValue("id")));
                }
                else
                {
                    string prefix = GetDate().ToString("yyyyMM");
                    string OrderNo = "";
                    DataTable dt = obj.PopulateDataSet("select top 1 OrderNo from Op_OrderHeader where OrderNo like '" + prefix + "%' order by OrderNo desc").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            long num = long.Parse(dt.Rows[0]["OrderNo"].ToString());
                            OrderNo = (num + 1).ToString();
                        }
                        catch
                        {
                            OrderNo = prefix + "00001";
                        }
                    }
                    else
                    {
                        OrderNo = prefix + "00001";
                    }

                    string id = Guid.NewGuid().ToString();
                    bc.Entity.RowPointer = id;
                    bc.Entity.OrderNo = OrderNo;
                    bc.Entity.OrderType = jp.getValue("OrderType");
                    bc.Entity.CustNo = jp.getValue("CustNo");
                    bc.Entity.OrderTime = ParseDateForString(jp.getValue("OrderTime"));
                    bc.Entity.DaysofMonth = ParseIntForString(jp.getValue("DaysofMonth"));
                    bc.Entity.ARDate = ParseDateForString(jp.getValue("ARDate"));
                    //bc.Entity.ARAmount = ParseDecimalForString(jp.getValue("ARAmount"));
                    //bc.Entity.ReduceAmount = ParseDecimalForString(jp.getValue("ReduceAmount"));
                    //bc.Entity.PaidinAmount = ParseDecimalForString(jp.getValue("PaidinAmount"));
                    bc.Entity.Remark = jp.getValue("Remark");

                    bc.Entity.OrderCreator = user.Entity.UserName;
                    bc.Entity.OrderCreateDate = GetDate();
                    bc.Entity.OrderLastReviser = user.Entity.UserName;
                    bc.Entity.OrderLastRevisedDate = GetDate();
                    bc.Entity.OrderStatus = "0";
                    int r = bc.Save("insert");
                    if (r <= 0)
                        flag = "2";

                    collection.Add(new JsonStringValue("RowPointer", id));
                    collection.Add(new JsonStringValue("OrderNo", OrderNo));
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("submittp", jp.getValue("submittp")));
            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"),
                jp.getValue("OrderField"), ParseIntForString(jp.getValue("page")), jp.getValue("PageCount"), jp.getValue("serviceProvider"))));
            return collection.ToString();
        }
        private string auditpassaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                bc.load(jp.getValue("id"));
                if (bc.Entity.OrderStatus != "0")
                {
                    flag = "3";
                }
                else
                {
                    if (obj.PopulateDataSet("select 1 from Op_OrderDetail where RefRP='" + bc.Entity.RowPointer + "'").Tables[0].Rows.Count == 0)
                    {
                        flag = "6";
                    }
                    else
                    {
                        bc.Entity.OrderAuditor = user.Entity.UserName;
                        bc.Entity.OrderAuditDate = GetDate();
                        bc.Entity.OrderAuditReason = "";
                        bc.Entity.OrderStatus = "1";
                        int row = bc.approve();
                        if (row <= 0)
                        {
                            flag = "5";
                        }
                        else
                        {
                            string company = "";
                            //-------------------------同步U8-------------------------
                            //------------抬头------------
                            JsonObjectCollection importInfo = new JsonObjectCollection();
                            importInfo.Add(new JsonStringValue("user", "Admin"));
                            importInfo.Add(new JsonStringValue("password", ""));
                            //------------抬头------------

                            //------------表头------------
                            Business.Op.BusinessOrderDetail detail = new Business.Op.BusinessOrderDetail();
                            System.Collections.ICollection list = detail.GetListQuery(bc.Entity.RowPointer);

                            JsonObjectCollection importDsJson = new JsonObjectCollection();
                            JsonObjectCollection mtb = new JsonObjectCollection();
                            mtb.Add(new JsonStringValue("ytype", "应收凭证"));
                            mtb.Add(new JsonStringValue("FDate", bc.Entity.ARDate.ToString("yyyy/M/d")));

                            int cnt = 0;
                            string SPNo = "";
                            string JFSubject = "";
                            string TaxSubject = "";
                            string SRVNo = "";
                            string SRVName = "";
                            string FID = ""; //房间号+客户+月份+费用种类 (K/1101-有芯电子-2017年11月-（空调费、管理费、电费、水费）)
                            string ResourceNo = "";
                            foreach (Entity.Op.EntityOrderDetail it in list)
                            {
                                DataTable dt = obj.PopulateDataSet("select * from dbo.Mstr_Room where RMID='" + it.ResourceNo + "'").Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    if (ResourceNo.IndexOf(dt.Rows[0]["RMNo"].ToString()) < 0)
                                        ResourceNo += dt.Rows[0]["RMNo"].ToString() + ";";
                                }
                                else
                                {
                                    if (ResourceNo.IndexOf(it.ResourceNo) < 0)
                                        ResourceNo += it.ResourceNo + ";";
                                }

                                if (SRVNo.IndexOf(it.ODSRVNo) < 0)
                                {
                                    SRVNo += it.ODSRVNo;
                                    SRVName += it.ODSRVName.Replace("公摊", "") + "、";
                                }
                                if (cnt == 0 && it.ODARAmount > 0)
                                {
                                    DataTable dt1 = obj.PopulateDataSet("select APNo from Mstr_ChargeAccount where CANo='" + it.ODCANo + "'").Tables[0];
                                    if (dt1.Rows.Count > 0)
                                        JFSubject = dt1.Rows[0]["APNo"].ToString();

                                    SPNo = it.ODContractSPNo;

                                    DataTable dt2 = obj.PopulateDataSet("select U8Account,TaxAccount from Mstr_ServiceProvider where SPNo='" + SPNo + "'").Tables[0];
                                    if (dt2.Rows.Count > 0)
                                    {
                                        company = dt2.Rows[0]["U8Account"].ToString();
                                        TaxSubject = dt2.Rows[0]["TaxAccount"].ToString();
                                    }
                                    cnt++;
                                }

                            }
                            //if (FID.Length > 1) FID = FID.Substring(0, FID.Length - 1);
                            //if (SRVName.Length > 1) SRVName = SRVName.Substring(0, SRVName.Length - 1);
                            //FID += "-" + bc.Entity.CustShortName;
                            //FID += "-" + bc.Entity.OrderTime.ToString("yyyy/MM");
                            //FID += "-(" + SRVName + ")";

                            ///////////////////////////////////////////////
                            if (ResourceNo.Length > 1) FID = ResourceNo.Substring(0, ResourceNo.Length - 1);
                            if (SRVName.Length > 1) SRVName = SRVName.Substring(0, SRVName.Length - 1);
                            FID += "-" + bc.Entity.CustShortName;
                            FID += "-" + bc.Entity.OrderTime.ToString("yyyy/MM");
                            FID += "-(" + SRVName + ")";

                            //如果长度 > 60 ,则自动缩减房间号长度
                            if (FID.Length > 60)
                            {
                                int len = 60 - (FID.Length - (ResourceNo.Length - 1));
                                if (len < 0) len = 0;

                                if (ResourceNo.Length > 1) FID = ResourceNo.Substring(0, len);

                                FID += "-" + bc.Entity.CustShortName;
                                FID += "-" + bc.Entity.OrderTime.ToString("yyyy/MM");
                                FID += "-(" + SRVName + ")";
                            }
                            ///////////////////////////////////////////////

                            mtb.Add(new JsonStringValue("FID", FID));
                            mtb.Add(new JsonStringValue("JFSubject", JFSubject));//借方科目
                            mtb.Add(new JsonStringValue("TaxSubject", TaxSubject));
                            importDsJson.Add(new JsonObjectCollection("mtb", mtb));
                            //------------表头------------
                            if (company == "001")
                                importInfo.Add(new JsonStringValue("type", "YSPZ"));
                            else
                                importInfo.Add(new JsonStringValue("type", "Line_YSPZ"));
                            importInfo.Add(new JsonStringValue("company", company));

                            //------------表体------------
                            JsonObjectCollection dtb = new JsonObjectCollection();
                            DataTable dt3 = obj.PopulateDataSet("select ODCANo,SUM(ODARAmount-ISNULL(ReduceAmount,0)) as ODARAmount,SUM(ODTaxAmount) as ODTaxAmount " +
                                "from Op_OrderDetail where RefRP='" + bc.Entity.RowPointer + "' Group By ODCANo").Tables[0];
                            foreach (DataRow it in dt3.Rows)
                            {
                                if (ParseDecimalForString(it["ODARAmount"].ToString()) > 0)
                                {
                                    JsonObjectCollection collection1 = new JsonObjectCollection();
                                    collection1.Add(new JsonStringValue("ytype", "应收凭证"));
                                    collection1.Add(new JsonStringValue("FID", FID));
                                    collection1.Add(new JsonStringValue("FNote", FID));
                                    collection1.Add(new JsonStringValue("Trading", ""));
                                    collection1.Add(new JsonStringValue("DFSubject", it["ODCANo"].ToString()));//贷方科目
                                    collection1.Add(new JsonStringValue("MTID", "RMB"));
                                    collection1.Add(new JsonStringValue("FCurrency", "人民币"));
                                    collection1.Add(new JsonStringValue("Amount", (ParseDecimalForString(it["ODARAmount"].ToString()) - ParseDecimalForString(it["ODTaxAmount"].ToString())).ToString("0.##")));
                                    collection1.Add(new JsonStringValue("cus", bc.Entity.CustNo));
                                    collection1.Add(new JsonStringValue("taxamount", ParseDecimalForString(it["ODTaxAmount"].ToString()).ToString("0.##")));

                                    dtb.Add(new JsonObjectCollection(collection1));
                                }
                            }
                            importDsJson.Add(new JsonObjectCollection("dtb", dtb));
                            //------------表体------------                     

                            try
                            {
                                string result = "";

                                U8EAISrv.EAISrv eai = new U8EAISrv.EAISrv();
                                result = eai.ImportFromJson(importInfo.ToString().Replace("\r", "").Replace("\n", "").Replace("\t", ""), importDsJson.ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("{{", "[{").Replace("}}", "}]"));

                                U8Return ot = new U8Return();
                                object oj = JsonToObject(result, ot);
                                U8Msg msg = ((U8Return)oj).rtn[0];
                                if (msg.flag == "True")
                                {
                                    //collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                                    //    jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"),
                                    //    jp.getValue("OrderField"), ParseIntForString(jp.getValue("page")))));
                                }
                                else
                                {
                                    obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='0' where RowPointer='" + bc.Entity.RowPointer + "'");
                                    flag = "4";
                                    collection.Add(new JsonStringValue("info", "审核不成功！" + msg.msg));
                                }
                            }
                            catch (Exception ex)
                            {
                                obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='0' where RowPointer='" + bc.Entity.RowPointer + "'");
                                flag = "4";
                                collection.Add(new JsonStringValue("info", ex.Message));
                            }
                            // -------------------------同步U8------------------------ -
                        }
                    }
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("id", jp.getValue("id")));
            collection.Add(new JsonStringValue("type", "auditpass"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string rauditaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                bc.load(jp.getValue("id"));
                if (bc.Entity.OrderStatus != "1")
                {
                    flag = "3";
                }
                else
                {
                    bc.Entity.OrderStatus = "0";
                    bc.Entity.OrderRAuditor = user.Entity.UserName;
                    bc.Entity.OrderRAuditReason = jp.getValue("OrderRAuditReason");
                    bc.Entity.OrderRAuditDate = GetDate();
                    int row = bc.approve();
                    if (row <= 0)
                    {
                        flag = "5";
                    }
                    collection.Add(new JsonStringValue("status", bc.Entity.OrderStatusName));
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("id", jp.getValue("id")));
            collection.Add(new JsonStringValue("type", "raudit"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string auditfailedaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                bc.load(jp.getValue("id"));
                if (bc.Entity.OrderStatus != "0")
                {
                    flag = "3";
                }
                else
                {
                    bc.Entity.OrderStatus = "-1";
                    bc.Entity.OrderAuditor = user.Entity.UserName;
                    bc.Entity.OrderAuditReason = jp.getValue("OrderAuditReason");
                    bc.Entity.OrderAuditDate = GetDate();
                    int row = bc.approve();
                    if (row <= 0)
                    {
                        flag = "5";
                    }
                    collection.Add(new JsonStringValue("status", bc.Entity.OrderStatusName));
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("id", jp.getValue("id")));
            collection.Add(new JsonStringValue("type", "auditfailed"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"),
                jp.getValue("OrderField"), ParseIntForString(jp.getValue("page")), jp.getValue("PageCount"), jp.getValue("serviceProvider"))));
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"),
                jp.getValue("OrderField"), ParseIntForString(jp.getValue("page")), jp.getValue("PageCount"), jp.getValue("serviceProvider"))));
            return collection.ToString();
        }
        private string printaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            string newName = "";
            try
            {
                foreach (string id in jp.getValue("ids").Split(';'))
                {
                    if (id == "") continue;
                    DataTable dt = obj.PopulateDataSet("select count(1) as Cnt from Op_OrderDetail where RefRP='" + id + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["Cnt"].ToString()) == 0)
                    {
                        flag = "4";
                        break;
                    }
                }
                if (flag != "4")
                {
                    Business.Op.BusinessOrderHeader bc = new Business.Op.BusinessOrderHeader();
                    bc.load(jp.getValue("ids").Split(';')[0]);
                    pathName = bc.Entity.OrderNo + ".pdf";

                    //A4纸张竖向
                    Document doc = new Document(PageSize.A5.Rotate(), 20, 20, 20, 10);
                    newName = creatFileName("pdf");
                    PdfWriter.GetInstance(doc, new FileStream(OrderPrint.Path + newName, FileMode.Create));
                    doc.Open();

                    int row = 0;
                    foreach (string id in jp.getValue("ids").Split(';'))
                    {
                        if (id == "") continue;
                        if (row > 0) doc.NewPage();

                        OrderPrint.Print(doc, id);

                        row++;
                    }

                    try { doc.Close(); }
                    catch { }


                    //加水印
                    string picName = "a.png";
                    DataTable dt = obj.PopulateDataSet("select * from Op_OrderDetail where RefRP='" + bc.Entity.RowPointer + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //FWC-001
                        if (dt.Rows[0]["ODContractSPNo"].ToString() == "FWC-001")
                            picName = "dl.png";
                        else if (dt.Rows[0]["ODContractSPNo"].ToString() == "FWC-002")
                            picName = "mh.png";
                        else if (dt.Rows[0]["ODContractSPNo"].ToString() == "FWC-003")
                            picName = "fzd.png";
                    }
                    Console.WriteLine(OrderPrint.Path);
                    Console.WriteLine(OrderPrint.Path + pathName);
                    Console.WriteLine(OrderPrint.Path.Replace("缴费通知单", "") + picName);
                    OrderPrint.PDFWatermark(OrderPrint.Path + newName, OrderPrint.Path + pathName, OrderPrint.Path.Replace("缴费通知单", "") + picName, 369, 111);
                }
            }
            catch (Exception ex)
            {
                flag = "2";
                collection.Add(new JsonStringValue("ex", ex.ToString()));
            }

            collection.Add(new JsonStringValue("type", "print"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("path", pathName));
            return collection.ToString();
        }
        private string unpayprintaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            string newName = "";
            try
            {
                string contractList = string.Join("','", jp.getValue("ids").Split(';'));
                contractList = "'" + contractList.Substring(0, contractList.Length - 2);
                DataTable dt = obj.PopulateDataSet("select count(1) as Cnt from Op_OrderDetail where RefRP in(" + contractList
                    + ") and ISNULL(ODARAmount,0)!=ISNULL(ODPaidAmount,0)").Tables[0];
                if (int.Parse(dt.Rows[0]["Cnt"].ToString()) == 0)
                {
                    flag = "4";
                }
                //foreach (string id in jp.getValue("ids").Split(';'))
                //{
                //    if (id == "") continue;
                //    DataTable dt = obj.PopulateDataSet("select count(1) as Cnt from Op_OrderDetail where RefRP='" + id + "'").Tables[0];
                //    if (int.Parse(dt.Rows[0]["Cnt"].ToString()) == 0)
                //    {
                //        flag = "4";
                //        break;
                //    }
                //}
                if (flag != "4")
                {
                    Business.Op.BusinessOrderHeader bc = new Business.Op.BusinessOrderHeader();
                    bc.load(jp.getValue("ids").Split(';')[0]);
                    pathName = bc.Entity.OrderNo + ".pdf";

                    //A4纸张竖向
                    Document doc = new Document(PageSize.A5.Rotate(), 20, 20, 20, 10);
                    newName = creatFileName("pdf");
                    PdfWriter.GetInstance(doc, new FileStream(OrderPrint.UnpayPath + newName, FileMode.Create));
                    doc.Open();

                    int row = 0;
                    foreach (string id in jp.getValue("ids").Split(';'))
                    {
                        if (id == "") continue;
                        dt = obj.PopulateDataSet("select count(1) as Cnt from Op_OrderDetail where RefRP='" + id
                            + "' and ISNULL(ODARAmount,0)!=ISNULL(ODPaidAmount,0)").Tables[0];
                        if (int.Parse(dt.Rows[0]["Cnt"].ToString()) > 0)
                        {
                            if (row > 0) doc.NewPage();
                            OrderPrint.UnpayPrint(doc, id);
                            row++;
                        }
                    }

                    try
                    {
                        doc.Close();
                    }
                    catch { }

                    //加水印
                    string picName = "a.png";
                    dt = obj.PopulateDataSet("select * from Op_OrderDetail where RefRP='" + bc.Entity.RowPointer + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //FWC-001
                        if (dt.Rows[0]["ODContractSPNo"].ToString() == "FWC-001")
                            picName = "dl.png";
                        else if (dt.Rows[0]["ODContractSPNo"].ToString() == "FWC-002")
                            picName = "mh.png";
                        else if (dt.Rows[0]["ODContractSPNo"].ToString() == "FWC-003")
                            picName = "fzd.png";
                    }
                    OrderPrint.PDFWatermark(OrderPrint.UnpayPath + newName,
                        OrderPrint.UnpayPath + pathName,
                        OrderPrint.UnpayPath.Replace("未缴费通知单", "") + picName, 369, 111);
                }
            }
            catch (Exception ex)
            {
                flag = "2";
                collection.Add(new JsonStringValue("ex", ex.ToString()));
            }

            collection.Add(new JsonStringValue("type", "unpayprint"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("path", pathName));
            return collection.ToString();
        }
        private string excelrentorderaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            try
            {
                pathName = "租赁订单" + GetDate().ToString("yyMMddHHmmss") + getRandom(4) + ".xls";

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("租赁订单");
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("客户");
                headerRow.CreateCell(1).SetCellValue("房间号");
                headerRow.CreateCell(2).SetCellValue("租金");
                headerRow.CreateCell(3).SetCellValue("税额");

                string OrderTimeS = string.Empty;
                if (jp.getValue("OrderTimeS") != "") OrderTimeS = jp.getValue("OrderTimeS") + "-01";

                string OrderField = jp.getValue("OrderField");
                if (OrderField == "") OrderField = "OrderNo DESC";

                string OrderTypes = "";
                if (user.Entity.UserType.ToUpper() == "ADMIN")
                {
                    Business.Base.BusinessOrderType bc1 = new project.Business.Base.BusinessOrderType();
                    foreach (Entity.Base.EntityOrderType it in bc1.GetListQuery(string.Empty, string.Empty))
                    {
                        OrderTypes += (OrderTypes == "" ? "" : ",") + "'" + it.OrderTypeNo + "'";
                    }
                }
                else
                {
                    string sqlstr1 = "select a.OrderType,b.OrderTypeName from Sys_UserOrderTypeRight a left join Mstr_OrderType b on a.OrderType=b.OrderTypeNo " +
                            "where a.UserType='" + user.Entity.UserType + "'";
                    DataTable dt1 = obj.PopulateDataSet(sqlstr1).Tables[0];
                    foreach (DataRow dr in dt1.Rows)
                    {
                        OrderTypes += (OrderTypes == "" ? "" : ",") + "'" + dr["OrderType"].ToString() + "'";
                    }
                }
                if (OrderTypes == "") OrderTypes = "''";

                int rowIndex = 1;
                project.Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                bc.OrderField = OrderField;
                foreach (project.Entity.Op.EntityOrderHeader it in bc.GetListQuery(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), OrderTypes, jp.getValue("CustNoS"),
                    ParseSearchDateForString(OrderTimeS), ParseSearchDateForString(jp.getValue("MinOrderCreateDate")),
                    ParseSearchDateForString(jp.getValue("MaxOrderCreateDate")), "", jp.getValue("serviceProvider")))
                {
                    string rimd = "";
                    decimal amount = 0;
                    decimal taxAmount = 0;
                    project.Business.Op.BusinessOrderDetail bc1 = new project.Business.Op.BusinessOrderDetail();
                    foreach (project.Entity.Op.EntityOrderDetail it1 in bc1.GetListQuery(it.RowPointer))
                    {
                        rimd += it1.ResourceNo + " ; ";
                        amount += it1.ODARAmount;
                        taxAmount += it1.ODTaxAmount;
                    }
                    if (rimd.Length > 0) rimd = rimd.Substring(0, rimd.Length - 3);

                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(it.CustName);
                    dataRow.CreateCell(1).SetCellValue(rimd);
                    dataRow.CreateCell(2).SetCellValue(amount.ToString("0.####"));
                    dataRow.CreateCell(3).SetCellValue(taxAmount.ToString("0.####"));
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

            collection.Add(new JsonStringValue("type", "excelrentorder"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("path", pathName));
            return collection.ToString();
        }
        private string excelproporderaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            try
            {
                pathName = "物业订单" + GetDate().ToString("yyMMddHHmmss") + getRandom(4) + ".xls";


                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("物业订单");
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("客户");
                headerRow.CreateCell(1).SetCellValue("房间号");
                headerRow.CreateCell(2).SetCellValue("金额");
                headerRow.CreateCell(3).SetCellValue("管理费");
                headerRow.CreateCell(4).SetCellValue("空调费");
                headerRow.CreateCell(5).SetCellValue("水费");
                headerRow.CreateCell(6).SetCellValue("电费");

                string OrderTimeS = string.Empty;
                if (jp.getValue("OrderTimeS") != "") OrderTimeS = jp.getValue("OrderTimeS") + "-01";

                string OrderField = jp.getValue("OrderField");
                if (OrderField == "") OrderField = "OrderNo DESC";

                string OrderTypes = "";
                if (user.Entity.UserType.ToUpper() == "ADMIN")
                {
                    Business.Base.BusinessOrderType bc1 = new project.Business.Base.BusinessOrderType();
                    foreach (Entity.Base.EntityOrderType it in bc1.GetListQuery(string.Empty, string.Empty))
                    {
                        OrderTypes += (OrderTypes == "" ? "" : ",") + "'" + it.OrderTypeNo + "'";
                    }
                }
                else
                {
                    string sqlstr1 = "select a.OrderType,b.OrderTypeName from Sys_UserOrderTypeRight a left join Mstr_OrderType b on a.OrderType=b.OrderTypeNo " +
                            "where a.UserType='" + user.Entity.UserType + "'";
                    DataTable dt1 = obj.PopulateDataSet(sqlstr1).Tables[0];
                    foreach (DataRow dr in dt1.Rows)
                    {
                        OrderTypes += (OrderTypes == "" ? "" : ",") + "'" + dr["OrderType"].ToString() + "'";
                    }
                }
                if (OrderTypes == "") OrderTypes = "''";

                int rowIndex = 1;
                project.Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
                bc.OrderField = OrderField;
                foreach (project.Entity.Op.EntityOrderHeader it in bc.GetListQuery(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), OrderTypes, jp.getValue("CustNoS"),
                    ParseSearchDateForString(OrderTimeS), ParseSearchDateForString(jp.getValue("MinOrderCreateDate")),
                    ParseSearchDateForString(jp.getValue("MaxOrderCreateDate")), "", jp.getValue("serviceProvider")))
                {
                    string rimd = "";
                    decimal amount = 0;
                    decimal glf = 0;
                    decimal ktf = 0;
                    decimal sf = 0;
                    decimal df = 0;
                    project.Business.Op.BusinessOrderDetail bc1 = new project.Business.Op.BusinessOrderDetail();
                    foreach (project.Entity.Op.EntityOrderDetail it1 in bc1.GetListQuery(it.RowPointer))
                    {
                        if (rimd.IndexOf(it1.ResourceNo) < 0)
                            rimd += it1.ResourceNo + " ; ";
                        amount += it1.ODARAmount;


                        if (it1.ODSRVName == "管理费") glf += it1.ODARAmount;
                        if (it1.ODSRVName == "空调费") ktf += it1.ODARAmount;
                        if (it1.ODSRVName == "公摊电费") df += it1.ODARAmount;
                        if (it1.ODSRVName == "电费") df += it1.ODARAmount;
                        if (it1.ODSRVName == "公摊水费") sf += it1.ODARAmount;
                        if (it1.ODSRVName == "水费") sf += it1.ODARAmount;
                    }
                    if (rimd.Length > 0) rimd = rimd.Substring(0, rimd.Length - 3);

                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(it.CustName);
                    dataRow.CreateCell(1).SetCellValue(rimd);
                    dataRow.CreateCell(2).SetCellValue(amount.ToString("0.####"));
                    dataRow.CreateCell(3).SetCellValue(glf.ToString("0.####"));
                    dataRow.CreateCell(4).SetCellValue(ktf.ToString("0.####"));
                    dataRow.CreateCell(5).SetCellValue(sf.ToString("0.####"));
                    dataRow.CreateCell(6).SetCellValue(df.ToString("0.####"));
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

            collection.Add(new JsonStringValue("type", "excelproporder"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("path", pathName));
            return collection.ToString();
        }

        private string getfeeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bc = new Business.Op.BusinessOrderHeader();
                bc.load(jp.getValue("id"));

                if (bc.Entity.OrderStatus != "0")
                {
                    flag = "5";
                }
                else
                {
                    if (obj.PopulateDataSet("select 1 from Op_OrderDetail where RefRP='" + bc.Entity.RowPointer + "'").Tables[0].Rows.Count == 0)
                    {
                        flag = "6";
                    }
                    else
                    {
                        string SPName = "";
                        DataTable dt = obj.PopulateDataSet("select top 1 b.SPName from Op_OrderDetail a left join Mstr_ServiceProvider b on a.ODContractSPNo=b.SPNo " +
                            "where RefRP='" + bc.Entity.RowPointer + "'").Tables[0];
                        if (dt.Rows.Count > 0) SPName = dt.Rows[0]["SPName"].ToString();

                        collection.Add(new JsonStringValue("ODCustName", bc.Entity.CustName));
                        collection.Add(new JsonStringValue("ODSPName", SPName));


                        System.Text.StringBuilder sb = new System.Text.StringBuilder("");
                        sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\">");
                        sb.Append("<thead>");
                        sb.Append("<tr class=\"text-c\">");
                        sb.Append("<th style=\"width:31%\">费用项</th>");
                        sb.Append("<th style=\"width:23%\">原应收金额</th>");
                        sb.Append("<th style=\"width:23%\">减免金额</th>");
                        sb.Append("<th style=\"width:23%\">应收金额</th>");
                        sb.Append("</tr>");
                        sb.Append("</thead>");

                        int r = 1;
                        decimal ODARAmount = 0;
                        decimal ReduceAmount = 0;
                        decimal UnReduceAmount = 0;
                        sb.Append("<tbody id=\"reducetbody\">");
                        Business.Op.BusinessOrderDetail bc1 = new project.Business.Op.BusinessOrderDetail();
                        foreach (Entity.Op.EntityOrderDetail it in bc1.GetListQuery(jp.getValue("id")))
                        {
                            decimal PayAmt = it.ODARAmount - it.ReduceAmount;
                            if (PayAmt < 0) PayAmt = 0;

                            sb.Append("<tr class=\"text-c\">");
                            sb.Append("<td>" + it.ODSRVName + "<input type=\"checkbox\" class=\"input-text size-MINI\" value=\"" + it.RowPointer + "\" name=\"chk\" style=\"display:none;\" /></td>");
                            sb.Append("<td><label id=\"aramt" + it.RowPointer + "\">" + it.ODARAmount.ToString("0.##") + "</label></td>");
                            sb.Append("<td><input class=\"input-text size-MINI\" id=\"amt" + it.RowPointer + "\" value=\"" + it.ReduceAmount.ToString("0.##") + "\" onblur=\"validDecimal2(this.id);\" onchange=\"CaluReduceAmt('" + it.RowPointer + "');\" style=\"text-align:center;\" /></td>");
                            sb.Append("<td><label id=\"unamt" + it.RowPointer + "\">" + PayAmt.ToString("0.##") + "</label></td>");
                            sb.Append("</tr>");

                            ODARAmount = ODARAmount + it.ODARAmount;
                            ReduceAmount = ReduceAmount + it.ReduceAmount;
                            UnReduceAmount = UnReduceAmount + PayAmt;
                            r++;
                        }
                        sb.Append("<tr class=\"text-c\">");
                        sb.Append("<td>合计：</td>");
                        sb.Append("<td><label id=\"TotODARAmount\">" + ODARAmount.ToString("0.##") + "</label></td>");
                        sb.Append("<td><label id=\"TotReduceAmt\">" + ReduceAmount.ToString("0.##") + "</label></td>");
                        sb.Append("<td><label id=\"TotUnReduceAmount\">" + UnReduceAmount.ToString("0.##") + "</label></td>");
                        sb.Append("</tr>");
                        sb.Append("</tbody>");
                        sb.Append("</table>");

                        collection.Add(new JsonStringValue("paylist", sb.ToString()));

                        collection.Add(new JsonStringValue("ODARAmount", ODARAmount.ToString("0.##")));
                        collection.Add(new JsonStringValue("ReduceAmount", ReduceAmount.ToString("0.##")));
                        collection.Add(new JsonStringValue("UnReduceAmount", UnReduceAmount.ToString("0.##")));
                    }
                }
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getfee"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string reduceaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bo = new Business.Op.BusinessOrderHeader();
                bo.load(jp.getValue("id"));

                foreach (string it in jp.getValue("ids").Split(';'))
                {
                    if (it == "") continue;
                    obj.ExecuteNonQuery("UPDATE Op_OrderDetail SET ReduceAmount = " + it.Split(':')[1] + "," +
                        "ODTaxAmount=(ODARAmount-" + it.Split(':')[1] + ") - ROUND(((ODARAmount-" + it.Split(':')[1] + ") / (1+ODTaxRate)),2)" +
                        "WHERE RowPointer = '" + it.Split(':')[0] + "'");
                }
                obj.ExecuteNonQuery("UPDATE Op_OrderHeader SET ReduceDate='" + jp.getValue("ReduceDate") + "',ReduceAmount = ISNULL((SELECT SUM(ISNULL(ReduceAmount,0)) FROM Op_OrderDetail WHERE RefRP=Op_OrderHeader.RowPointer),0) WHERE RowPointer = " + "'" + jp.getValue("id") + "'");
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "reduce"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"),
                jp.getValue("OrderField"), ParseIntForString(jp.getValue("page")), jp.getValue("PageCount"), jp.getValue("serviceProvider"))));
            return collection.ToString();
        }

        #region Item
        private string itemsave1action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderDetail bc = new project.Business.Op.BusinessOrderDetail();
                if (jp.getValue("itemtp") == "update")
                {
                    bc.load(jp.getValue("itemid"));
                    bc.Entity.ODLastRevisedDate = GetDate();
                    bc.Entity.ODLastReviser = user.Entity.UserName;
                }
                else
                {
                    bc.Entity.RefRP = jp.getValue("id");
                    bc.Entity.ODLastRevisedDate = GetDate();
                    bc.Entity.ODLastReviser = user.Entity.UserName;
                    bc.Entity.ODCreateDate = DateTime.Now;
                    bc.Entity.ODCreator = user.Entity.UserName;
                }

                bc.Entity.ODSRVTypeNo1 = jp.getValue("ODSRVTypeNo1");
                bc.Entity.ODSRVTypeNo2 = jp.getValue("ODSRVTypeNo2");
                bc.Entity.ODSRVNo = jp.getValue("ODSRVNo");
                bc.Entity.ODSRVRemark = jp.getValue("ODSRVRemark");
                bc.Entity.ODSRVCalType = "";
                bc.Entity.ODContractSPNo = jp.getValue("ODContractSPNo");
                bc.Entity.ODContractNo = jp.getValue("ODContractNo");
                bc.Entity.ODContractNoManual = jp.getValue("ODContractNoManual");
                bc.Entity.ResourceNo = jp.getValue("ResourceNo");
                bc.Entity.ResourceName = jp.getValue("ResourceName");
                bc.Entity.ODFeeStartDate = ParseDateForString(jp.getValue("ODFeeStartDate"));
                bc.Entity.ODFeeEndDate = ParseDateForString(jp.getValue("ODFeeEndDate"));
                bc.Entity.BillingDays = ParseIntForString(jp.getValue("BillingDays"));
                bc.Entity.ODQTY = ParseDecimalForString(jp.getValue("ODQTY"));
                bc.Entity.ODUnit = jp.getValue("ODUnit");
                bc.Entity.ODUnitPrice = ParseDecimalForString(jp.getValue("ODUnitPrice"));
                bc.Entity.ODARAmount = ParseDecimalForString(jp.getValue("ODARAmount"));
                bc.Entity.ODTaxRate = ParseDecimalForString(jp.getValue("ODTaxRate"));
                bc.Entity.ODTaxAmount = ParseDecimalForString(jp.getValue("ODTaxAmount"));

                Business.Base.BusinessService bs = new Business.Base.BusinessService();
                bs.load(jp.getValue("ODSRVNo"));
                bc.Entity.ODCANo = bs.Entity.CANo;
                bc.Entity.ODSRVTypeNo1 = bs.Entity.SRVTypeNo1;
                bc.Entity.ODSRVTypeNo2 = bs.Entity.SRVTypeNo2;
                //bc.Entity.ODPaidinAmount = ParseDecimalForString(jp.getValue("ODPaidinAmount"));
                //bc.Entity.ODReduceAmount = ParseDecimalForString(jp.getValue("ODReduceAmount"));
                //bc.Entity.ODPaidDate = ParseDateForString(jp.getValue("ODPaidDate"));
                //bc.Entity.ODFeeReceiver = jp.getValue("ODFeeReceiver");
                //bc.Entity.ODFeeReceiveRemark = jp.getValue("ODFeeReceiveRemark");
                //bc.Entity.ODReduceAmount = ParseDecimalForString(jp.getValue("ODReduceAmount"));
                //bc.Entity.ODReduceStartDate = ParseDateForString(jp.getValue("ODReduceStartDate"));
                //bc.Entity.ODReduceEndDate = ParseDateForString(jp.getValue("ODReduceEndDate"));
                //bc.Entity.ODReducedDate = ParseDateForString(jp.getValue("ODReducedDate"));
                //bc.Entity.ODReducePerson = jp.getValue("ODReducePerson");
                //bc.Entity.ODReduceReason = jp.getValue("ODReduceReason");
                //bc.Entity.ODCANo = jp.getValue("ODCANo");
                int r = bc.Save();
                if (r <= 0)
                    flag = "2";
                else
                {
                    obj.ExecuteNonQuery("update Op_OrderHeader set ARAmount=(select SUM(ISNULL(ODARAmount,0)) from Op_OrderDetail where RefRP=Op_OrderHeader.RowPointer)" +
                        " where RowPointer='" + bc.Entity.RefRP + "'");

                    DataTable dt = obj.PopulateDataSet("select ARAmount from Op_OrderHeader where RowPointer='" + bc.Entity.RefRP + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                        collection.Add(new JsonStringValue("ARAmount", ParseDecimalForString(dt.Rows[0]["ARAmount"].ToString()).ToString("0.####")));
                    else
                        collection.Add(new JsonStringValue("ARAmount", "0"));
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemsave1"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList1(jp.getValue("id"), "update")));
            return collection.ToString();
        }
        private string itemupdate1action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessOrderDetail bc = new Business.Op.BusinessOrderDetail();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("ODSRVTypeNo1", bc.Entity.ODSRVTypeNo1));
                collection.Add(new JsonStringValue("ODSRVTypeNo2", bc.Entity.ODSRVTypeNo2));
                collection.Add(new JsonStringValue("ODSRVNo", bc.Entity.ODSRVNo));
                collection.Add(new JsonStringValue("ODSRVRemark", bc.Entity.ODSRVRemark));
                collection.Add(new JsonStringValue("ODSRVCalType", bc.Entity.ODSRVCalType));
                collection.Add(new JsonStringValue("ODContractSPNo", bc.Entity.ODContractSPNo));
                collection.Add(new JsonStringValue("ODContractNo", bc.Entity.ODContractNo));
                collection.Add(new JsonStringValue("ODContractNoManual", bc.Entity.ODContractNoManual));
                collection.Add(new JsonStringValue("ResourceNo", bc.Entity.ResourceNo));
                collection.Add(new JsonStringValue("ResourceName", bc.Entity.ResourceName));
                collection.Add(new JsonStringValue("ODFeeStartDate", bc.Entity.ODFeeStartDate.ToString("yyyy-MM-dd")));
                collection.Add(new JsonStringValue("ODFeeEndDate", bc.Entity.ODFeeEndDate.ToString("yyyy-MM-dd")));
                collection.Add(new JsonStringValue("BillingDays", bc.Entity.BillingDays.ToString()));
                collection.Add(new JsonStringValue("ODQTY", bc.Entity.ODQTY.ToString("0.####")));
                collection.Add(new JsonStringValue("ODUnit", bc.Entity.ODUnit));
                collection.Add(new JsonStringValue("ODUnitPrice", bc.Entity.ODUnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("ODARAmount", bc.Entity.ODARAmount.ToString("0.####")));
                collection.Add(new JsonStringValue("ODTaxRate", bc.Entity.ODTaxRate.ToString("0.####")));
                collection.Add(new JsonStringValue("ODTaxAmount", bc.Entity.ODTaxAmount.ToString("0.####")));
                //collection.Add(new JsonStringValue("ODPaidinAmount", bc.Entity.ODPaidinAmount.ToString("0.####")));
                //collection.Add(new JsonStringValue("ODReduceAmount", bc.Entity.ODReduceAmount.ToString("0.####")));
                //collection.Add(new JsonStringValue("ODPaidDate", ParseStringForDate(bc.Entity.ODPaidDate)));
                //collection.Add(new JsonStringValue("ODFeeReceiver", bc.Entity.ODFeeReceiver));
                //collection.Add(new JsonStringValue("ODFeeReceiveRemark", bc.Entity.ODFeeReceiveRemark));
                //collection.Add(new JsonStringValue("ODReduceAmount", bc.Entity.ODReduceAmount.ToString("0.####")));
                //collection.Add(new JsonStringValue("ODReduceStartDate", ParseStringForDate(bc.Entity.ODReduceStartDate)));
                //collection.Add(new JsonStringValue("ODReduceEndDate", ParseStringForDate(bc.Entity.ODReduceEndDate)));
                //collection.Add(new JsonStringValue("ODReducedDate", ParseStringForDate(bc.Entity.ODReducedDate)));
                //collection.Add(new JsonStringValue("ODReducePerson", bc.Entity.ODReducePerson));
                //collection.Add(new JsonStringValue("ODReduceReason", bc.Entity.ODReduceReason));
                collection.Add(new JsonStringValue("ODCANo", bc.Entity.ODCANo));

                string subtype = "";
                int row = 0;
                Business.Base.BusinessServiceType bs = new Business.Base.BusinessServiceType();
                foreach (Entity.Base.EntityServiceType it in bs.GetListQuery(string.Empty, string.Empty, bc.Entity.ODSRVTypeNo1, string.Empty, true))
                {
                    subtype += it.SRVTypeNo + ":" + it.SRVTypeName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));

                string subtype1 = "";
                int row1 = 0;
                Business.Base.BusinessService bt1 = new Business.Base.BusinessService();
                foreach (Entity.Base.EntityService it in bt1.GetListQuery(string.Empty, string.Empty, bc.Entity.ODSRVTypeNo1, bc.Entity.ODSRVTypeNo2, string.Empty, string.Empty, string.Empty))
                {
                    subtype1 += it.SRVNo + ":" + it.SRVName + ";";
                    row1++;
                }
                collection.Add(new JsonNumericValue("row1", row1));
                collection.Add(new JsonStringValue("subtype1", subtype1));

                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate1"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string itemdel1action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessOrderDetail bc = new project.Business.Op.BusinessOrderDetail();
                bc.load(jp.getValue("itemid"));
                string InfoBar = bc.delete();
                if (InfoBar != "")
                {
                    flag = "4";
                    collection.Add(new JsonStringValue("InfoBar", InfoBar));
                }
                else
                {
                    obj.ExecuteNonQuery("update Op_OrderHeader set ARAmount=(select SUM(ISNULL(ODARAmount,0)) from Op_OrderDetail where RefRP=Op_OrderHeader.RowPointer)" +
                        " where RowPointer='" + bc.Entity.RefRP + "'");

                    DataTable dt = obj.PopulateDataSet("select ARAmount from Op_OrderHeader where RowPointer='" + bc.Entity.RefRP + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                        collection.Add(new JsonStringValue("ARAmount", ParseDecimalForString(dt.Rows[0]["ARAmount"].ToString()).ToString("0.####")));
                    else
                        collection.Add(new JsonStringValue("ARAmount", "0"));
                }
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemdel1"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList1(jp.getValue("id"), "update")));
            result = collection.ToString();
            return result;
        }
        private string ODSRVNoChangeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessService bc = new Business.Base.BusinessService();
                bc.load(jp.getValue("ODSRVNo"));
                collection.Add(new JsonStringValue("SRVTaxRate", bc.Entity.SRVTaxRate.ToString()));
                /*
                collection.Add(new JsonStringValue("SRVSPNo", bc.Entity.SRVSPNo));
                collection.Add(new JsonStringValue("SRVCalType", bc.Entity.SRVCalType));                
                collection.Add(new JsonStringValue("SRVRoundType", bc.Entity.SRVRoundType.ToString()));
                decimal amount = 0;
                decimal taxAmount = 0;
                if (ParseDecimalForString(jp.getValue("ODQTY")) > 0 && ParseDecimalForString(jp.getValue("ODUnitPrice")) > 0)
                {
                    //Business.Base.BusinessService bc1 = new Business.Base.BusinessService();
                    //bc1.load(jp.getValue("ODSRVNo"));

                    if (bc.Entity.SRVRoundType.ToUpper() == "FLOOR")
                    {
                        amount = Math.Floor(ParseDecimalForString(jp.getValue("ODQTY"))
                            * ParseDecimalForString(jp.getValue("ODUnitPrice")));
                        //taxAmount = Math.Floor(ParseDecimalForString(jp.getValue("ODQTY"))
                        //    * ParseDecimalForString(jp.getValue("ODUnitPrice"))
                        //    * ParseDecimalForString(jp.getValue("ODTaxRate")));
                        taxAmount = amount - (Math.Floor(amount / (1 + bc.Entity.SRVTaxRate)));
                    }
                    else if (bc.Entity.SRVRoundType.ToUpper() == "CEILING")
                    {
                        amount = Math.Ceiling(ParseDecimalForString(jp.getValue("ODQTY")) * ParseDecimalForString(jp.getValue("ODUnitPrice")));
                        taxAmount = amount - (Math.Ceiling(amount / (1 + bc.Entity.SRVTaxRate)));
                        //taxAmount = Math.Ceiling(ParseDecimalForString(jp.getValue("ODQTY")) 
                        //    * ParseDecimalForString(jp.getValue("ODUnitPrice")) 
                        //    * ParseDecimalForString(jp.getValue("ODTaxRate")));
                    }
                    else
                    {
                        amount = Math.Round(ParseDecimalForString(jp.getValue("ODQTY"))
                            * ParseDecimalForString(jp.getValue("ODUnitPrice")), bc.Entity.SRVDecimalPoint);
                        taxAmount = amount - (Math.Round(amount / (1 + bc.Entity.SRVTaxRate)));
                        //taxAmount = Math.Round(ParseDecimalForString(jp.getValue("ODQTY")) 
                        //    * ParseDecimalForString(jp.getValue("ODUnitPrice")) 
                        //    * ParseDecimalForString(jp.getValue("ODTaxRate")), bc1.Entity.SRVDecimalPoint);
                    }
                }
                collection.Add(new JsonStringValue("amount", amount.ToString()));
                collection.Add(new JsonStringValue("taxAmount", taxAmount.ToString()));
                */
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "ODSRVNoChange"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string gettypeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string subtype = "";
            string type = "empty";
            string firsubtype = "";
            string subtype1 = "";
            if (jp.getValue("SRVTypeNo1") != "") type = jp.getValue("SRVTypeNo1");
            try
            {
                int row = 0;
                Business.Base.BusinessServiceType bt = new Business.Base.BusinessServiceType();
                foreach (Entity.Base.EntityServiceType it in bt.GetListQuery(string.Empty, string.Empty, type, string.Empty, true))
                {
                    subtype += it.SRVTypeNo + ":" + it.SRVTypeName + ";";

                    if (row == 0)
                        firsubtype = it.SRVTypeNo;

                    row++;
                }

                int row1 = 0;
                Business.Base.BusinessService bt1 = new Business.Base.BusinessService();
                foreach (Entity.Base.EntityService it in bt1.GetListQuery(string.Empty, string.Empty, type, firsubtype, string.Empty, string.Empty, string.Empty))
                {
                    subtype1 += it.SRVNo + ":" + it.SRVName + ";";
                    row1++;
                }

                collection.Add(new JsonNumericValue("row1", row1));
                collection.Add(new JsonStringValue("subtype1", subtype1));

                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "gettype"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string getsubtypeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string subtype = "";
            string type = "empty";
            if (jp.getValue("SRVTypeNo2") != "") type = jp.getValue("SRVTypeNo2");
            try
            {
                int row = 0;
                Business.Base.BusinessService bt = new Business.Base.BusinessService();
                foreach (Entity.Base.EntityService it in bt.GetListQuery(string.Empty, string.Empty, string.Empty, type, string.Empty, string.Empty, string.Empty))
                {
                    subtype += it.SRVNo + ":" + it.SRVName + ";";
                    row++;
                }

                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getsubtype"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string CalcAmountaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                string ODSRVNo = jp.getValue("ODSRVNo");
                decimal ODQTY = ParseDecimalForString(jp.getValue("ODQTY"));
                decimal ODUnitPrice = ParseDecimalForString(jp.getValue("ODUnitPrice"));
                decimal amount = 0;
                Business.Base.BusinessService bc = new Business.Base.BusinessService();
                bc.load(ODSRVNo);
                if (bc.Entity.SRVRoundType.ToUpper() == "FLOOR") amount = Math.Floor(ODQTY * ODUnitPrice);
                else if (bc.Entity.SRVRoundType.ToUpper() == "CEILING") amount = Math.Ceiling(ODQTY * ODUnitPrice);
                else amount = Math.Round(ODQTY * ODUnitPrice, bc.Entity.SRVDecimalPoint);
                collection.Add(new JsonStringValue("amount", amount.ToString()));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "CalcAmount"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string CalcTaxaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {

                string ODSRVNo = jp.getValue("ODSRVNo");
                decimal amount = ParseDecimalForString(jp.getValue("ODARAmount"));
                decimal rate = ParseDecimalForString(jp.getValue("ODTaxRate"));
                decimal tax = 0;
                Business.Base.BusinessService bc = new Business.Base.BusinessService();
                bc.load(ODSRVNo);
                if (bc.Entity.SRVRoundType.ToUpper() == "FLOOR")
                    tax = Math.Floor(amount - Math.Round(amount / (1 + rate), 2));
                else if (bc.Entity.SRVRoundType.ToUpper() == "CEILING")
                    tax = Math.Ceiling(amount - Math.Round(amount / (1 + rate), 2));
                else
                    tax = Math.Round(amount - Math.Round(amount / (1 + rate), 2), bc.Entity.SRVDecimalPoint);
                collection.Add(new JsonStringValue("tax", tax.ToString()));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "CalcTax"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }

        private string CalcAmountAction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                string ODSRVNo = jp.getValue("ODSRVNo");
                decimal ODQTY = ParseDecimalForString(jp.getValue("ODQTY"));
                decimal ODUnitPrice = ParseDecimalForString(jp.getValue("ODUnitPrice"));
                decimal ODTaxRate = ParseDecimalForString(jp.getValue("ODTaxRate"));
                decimal total = 0;
                decimal tax = 0;
                Business.Base.BusinessService bc = new Business.Base.BusinessService();
                bc.load(ODSRVNo);
                decimal SRVRate = bc.Entity.SRVRate;
                if (SRVRate <= 0) SRVRate = 0;

                if (bc.Entity.SRVRoundType.ToUpper() == "FLOOR")
                {
                    total = Math.Floor(ODQTY * ODUnitPrice);
                    tax = Math.Floor(total - Math.Round(total / (1 + ODTaxRate), 2));
                }
                else if (bc.Entity.SRVRoundType.ToUpper() == "CEILING")
                {
                    total = Math.Ceiling(ODQTY * ODUnitPrice);
                    tax = Math.Ceiling(total - Math.Round(total / (1 + ODTaxRate), 2));
                }
                else
                {
                    total = Math.Round(ODQTY * ODUnitPrice, bc.Entity.SRVDecimalPoint);
                    tax = Math.Round(total - Math.Round(total / (1 + ODTaxRate), 2), bc.Entity.SRVDecimalPoint);
                }

                collection.Add(new JsonStringValue("total", total.ToString()));
                collection.Add(new JsonStringValue("tax", tax.ToString()));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "CalcAmount"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        #endregion

    }
}