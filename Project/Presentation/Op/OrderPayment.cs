using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace project.Presentation.Op
{
    public partial class OrderPayment : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/OrderPayment.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/OrderPayment.aspx'";
                            DataTable dt = obj.PopulateDataSet(sqlstr).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                string rightCode = dt.Rows[0]["RightCode"].ToString();
                                if (rightCode.IndexOf("view") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"view()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看订单详情</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                if (rightCode.IndexOf("view") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"viewfee()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看收款记录</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                if (rightCode.IndexOf("pay") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"fee()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 收款</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"view()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看订单详情</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"viewfee()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看收款记录</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"fee()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 收款</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ALL", "", 1);

                        date = GetDate().ToString("yyyy-MM-dd");

                        OrderTypeStr = "<select class=\"input-text required\" id=\"OrderType\">";
                        OrderTypeStr += "<option value=\"\"></option>";
                        OrderTypeStrS = "<select class=\"input-text size-MINI\" id=\"OrderTypeS\" style=\"width:120px;\" >";
                        OrderTypeStrS += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessOrderType bc = new project.Business.Base.BusinessOrderType();
                        foreach (Entity.Base.EntityOrderType it in bc.GetListQuery(string.Empty, string.Empty))
                        {
                            OrderTypeStr += "<option value='" + it.OrderTypeNo + "'>" + it.OrderTypeName + "</option>";
                            OrderTypeStrS += "<option value='" + it.OrderTypeNo + "'>" + it.OrderTypeName + "</option>";
                        }
                        OrderTypeStr += "</select>";
                        OrderTypeStrS += "</select>";

                        ODSRVNoStr = "<select class=\"input-text size-MINI\" id=\"ODSRVNo\">";
                        ODSRVNoStr += "<option value=\"\"></option>";
                        Business.Base.BusinessService bc2 = new project.Business.Base.BusinessService();
                        foreach (Entity.Base.EntityService it in bc2.GetListQuery(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty))
                        {
                            ODSRVNoStr += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                        }
                        ODSRVNoStr += "</select>";

                        ODContractSPNoStr = "<select class=\"input-text size-MINI\" id=\"ODContractSPNo\">";
                        ODContractSPNoStr += "<option value=\"\"></option>";
                        Business.Base.BusinessServiceProvider bc3 = new project.Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider it in bc3.GetListQuery(string.Empty, string.Empty, true))
                        {
                            ODContractSPNoStr += "<option value='" + it.SPNo + "'>" + it.SPShortName + "</option>";
                        }
                        ODContractSPNoStr += "</select>";

                        BankStr = "<select class=\"input-text size-MINI\" id=\"ODPaidBank\" style=\"width:200px;\">";
                        BankStr += "<option value=\"\"></option>";
                        Business.Base.BusinessBank bc4 = new project.Business.Base.BusinessBank();
                        foreach (Entity.Base.EntityBank it in bc4.GetListQuery(string.Empty, string.Empty, true))
                        {
                            BankStr += "<option value='" + it.BankNo + "'>" + it.BankName + "</option>";
                        }
                        BankStr += "</select>";

                        serviceProvider = "<select class=\"input-text size-MINI\" id=\"SPNo\" style=\"width:120px;\" >";
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
        protected string ODSRVNoStr = "";
        protected string ODContractSPNoStr = "";
        protected string BankStr = "";
        protected string serviceProvider = "";

        private string createList(string OrderNo, string OrderType, string CustNo, string OrderTime, string MinOrderCreateDate, string MaxOrderCreateDate, string OrderStatus, string SPNo, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='8%'>订单编号</th>");
            sb.Append("<th width='8%'>订单类别</th>");
            sb.Append("<th width='7%'>订单状态</th>");
            sb.Append("<th width='6%'>客户编号</th>");
            sb.Append("<th width='18%'>客户名称</th>");
            sb.Append("<th width='6%'>所属年月</th>");
            sb.Append("<th width='8%'>应收日期</th>");
            sb.Append("<th width='7%'>应收总金额</th>");
            sb.Append("<th width='7%'>实收总金额</th>");
            sb.Append("<th width='8%'>制单日期</th>");
            sb.Append("<th width='7%'>制单人</th>");
            //sb.Append("<th width='10%'>订单备注</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            string OrderTimeS = string.Empty;
            if (OrderTime != "") OrderTimeS = OrderTime + "-01";
            int r = 1;
            sb.Append("<tbody>");
            Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
            foreach (Entity.Op.EntityOrderHeader it in bc.GetListQuery(OrderNo, OrderType, "", CustNo, ParseSearchDateForString(OrderTimeS), ParseSearchDateForString(MinOrderCreateDate), ParseSearchDateForString(MaxOrderCreateDate), OrderStatus, page, pageSize, SPNo))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
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
                sb.Append("<td>" + (it.ARAmount - it.ReduceAmount).ToString("0.####") + "</td>");
                sb.Append("<td>" + it.PaidinAmount.ToString("0.####") + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.OrderCreateDate) + "</td>");
                sb.Append("<td>" + it.OrderCreator + "</td>");
                //sb.Append("<td>" + it.Remark + "</td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetListCount(OrderNo, OrderType, "", CustNo, ParseSearchDateForString(OrderTimeS), ParseSearchDateForString(MinOrderCreateDate), ParseSearchDateForString(MaxOrderCreateDate), OrderStatus, SPNo), pageSize, page, 7));
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
            sb.Append("<th style=\"width:10%\">合同主体</th>");
            sb.Append("<th style=\"width:16%\">资源编号</th>");
            sb.Append("<th style=\"width:8%\">数量</th>");
            sb.Append("<th style=\"width:5%\">单位</th>");
            sb.Append("<th style=\"width:9%\">单价</th>");
            sb.Append("<th style=\"width:9%\">原应收金额</th>");
            sb.Append("<th style=\"width:9%\">减免金额</th>");
            sb.Append("<th style=\"width:9%\">应收金额</th>");
            sb.Append("<th style=\"width:8%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;

            sb.Append("<tbody id=\"ItemBody1\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessOrderDetail bc = new project.Business.Op.BusinessOrderDetail();
                foreach (Entity.Op.EntityOrderDetail it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"Detail" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.ODSRVName + "</td>");
                    sb.Append("<td>" + it.ODContractSPName + "</td>");
                    sb.Append("<td>" + it.ResourceNo + "</td>");
                    sb.Append("<td>" + it.ODQTY.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.ODUnit + "</td>");
                    sb.Append("<td>" + it.ODUnitPrice.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.ODARAmount.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.ReduceAmount.ToString("0.####") + "</td>");
                    sb.Append("<td>" + (it.ODARAmount - it.ReduceAmount).ToString("0.####") + "</td>");
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
            if (jp.getValue("Type") == "view")
                result = viewaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            else if (jp.getValue("Type") == "viewfee")
                result = viewfeeaction(jp);
            else if (jp.getValue("Type") == "getfee")
                result = getfeeaction(jp);
            else if (jp.getValue("Type") == "feesubmit")
                result = feesubmitaction(jp);

            else if (jp.getValue("Type") == "itemupdate1")
                result = itemupdate1action(jp);
            else if (jp.getValue("Type") == "feedelete")
                result = feedeleteaction(jp);
            return result;
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

                collection.Add(new JsonStringValue("itemlist1", createItemList1(bc.Entity.RowPointer, "view")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "view"));
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
                jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"), jp.getValue("SPNo"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"), jp.getValue("SPNo"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string viewfeeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("");
                sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\">");
                sb.Append("<thead>");
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<th style=\"width:5%\">序号</th>");
                sb.Append("<th style=\"width:20%\">收款日期</th>");
                sb.Append("<th style=\"width:15%\">收款金额</th>");
                sb.Append("<th style=\"width:15%\">收款人</th>");
                sb.Append("<th style=\"width:30%\">备注</th>");
                sb.Append("<th style=\"width:15%\">操作</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                int r = 1;
                sb.Append("<tbody id=\"ItemBody\">");
                Business.Op.BusinessOrderFeeReceiver bc = new project.Business.Op.BusinessOrderFeeReceiver();
                foreach (Entity.Op.EntityOrderFeeReceiver it in bc.GetListQuery(jp.getValue("id")))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.ODPaidDate.ToString("yyyy-MM-dd") + "</td>");
                    sb.Append("<td>" + it.ODPaidAmount.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.ODFeeReceiver + "</td>");
                    sb.Append("<td>" + it.ODFeeReceiveRemark + "</td>");
                    sb.Append("<td><input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"feedelete('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                    sb.Append("</tr>");
                    r++;
                }
                sb.Append("</tbody>");
                sb.Append("</table>");

                collection.Add(new JsonStringValue("feelist", sb.ToString()));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "viewfee"));
            collection.Add(new JsonStringValue("flag", flag));

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
                    collection.Add(new JsonStringValue("ODUnAmount", (bc.Entity.ARAmount - bc.Entity.PaidinAmount).ToString("0.##")));


                    System.Text.StringBuilder sb = new System.Text.StringBuilder("");
                    sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\">");
                    sb.Append("<thead>");
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<th style=\"width:25%\">费用项</th>");
                    sb.Append("<th style=\"width:15%\">原应收金额</th>");
                    sb.Append("<th style=\"width:15%\">减免金额</th>");
                    sb.Append("<th style=\"width:15%\">应收金额</th>");
                    sb.Append("<th style=\"width:15%\">实收金额</th>");
                    sb.Append("<th style=\"width:15%\">待收金额</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    int r = 1;
                    decimal ODARAmount = 0;
                    decimal ReduceAmount = 0;
                    decimal UnReduceAmount = 0;
                    decimal UnPayAmount = 0;
                    sb.Append("<tbody id=\"tbody\">");
                    Business.Op.BusinessOrderDetail bc1 = new project.Business.Op.BusinessOrderDetail();
                    foreach (Entity.Op.EntityOrderDetail it in bc1.GetListQuery(jp.getValue("id")))
                    {
                        decimal PayAmt = it.ODARAmount - it.ReduceAmount - it.ODPaidAmount;
                        if (PayAmt < 0) PayAmt = 0;

                        sb.Append("<tr class=\"text-c\">");
                        sb.Append("<td>" + it.ODSRVName + "<input type=\"checkbox\" class=\"input-text size-MINI\" value=\"" + it.RowPointer + "\" name=\"chk\" style=\"display:none;\" /></td>");
                        sb.Append("<td>" + it.ODARAmount.ToString("0.##") + "</td>");
                        sb.Append("<td>" + it.ReduceAmount.ToString("0.##") + "</td>");
                        sb.Append("<td>" + (it.ODARAmount - it.ReduceAmount).ToString("0.##") + "</td>");
                        sb.Append("<td><input class=\"input-text size-MINI\" id=\"amt" + it.RowPointer + "\" value=\"" + PayAmt.ToString("0.##") + "\" onblur=\"validDecimal2(this.id);\" onchange=\"caluamount('" + it.RowPointer + "');\" style=\"text-align:center;\" /></td>");
                        sb.Append("<td>" + PayAmt.ToString("0.##") + "</td>");
                        sb.Append("</tr>");

                        ODARAmount = ODARAmount + it.ODARAmount;
                        ReduceAmount = ReduceAmount + it.ReduceAmount;
                        UnReduceAmount = UnReduceAmount + (it.ODARAmount - it.ReduceAmount);
                        UnPayAmount = UnPayAmount + PayAmt;
                        r++;
                    }
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<td>合计：</td>");
                    sb.Append("<td>" + ODARAmount.ToString("0.##") + "</td>");
                    sb.Append("<td>" + ReduceAmount.ToString("0.##") + "</td>");
                    sb.Append("<td>" + UnReduceAmount.ToString("0.##") + "</td>");
                    sb.Append("<td><label id=\"PayAmt\">" + UnPayAmount.ToString("0.##") + "</td>");
                    sb.Append("<td>" + UnPayAmount.ToString("0.##") + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");

                    collection.Add(new JsonStringValue("paylist", sb.ToString()));
                }
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getfee"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string feesubmitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessOrderHeader bo = new Business.Op.BusinessOrderHeader();
                bo.load(jp.getValue("id"));

                string BankNo = jp.getValue("ODPaidBank");
                string BankName = "";
                string BankAccount = "";
                if (BankNo != "")
                {
                    Business.Base.BusinessBank bb = new Business.Base.BusinessBank();
                    bb.load(BankNo);
                    BankName = bb.Entity.BankName;
                    BankAccount = bb.Entity.BankAccount;
                }

                string TmpName = "Tmp" + getRandom();
                obj.ExecuteNonQuery("Create table " + TmpName + "(DetailRP NVARCHAR(36),ODPaidAmount DECIMAL(15,4))");

                decimal ODPaidAmount = 0;
                foreach (string it in jp.getValue("ids").Split(';'))
                {
                    if (it == "") continue;
                    obj.ExecuteNonQuery("insert into " + TmpName + " values('" + it.Split(':')[0] + "','" + it.Split(':')[1] + "')");

                    ODPaidAmount += ParseDecimalForString(it.Split(':')[1]);
                }

                string frid = Guid.NewGuid().ToString();
                Business.Op.BusinessOrderFeeReceiver bc = new Business.Op.BusinessOrderFeeReceiver();
                bc.Entity.RefRP = jp.getValue("id");
                bc.Entity.ODPaidAmount = ODPaidAmount;
                bc.Entity.ODPaidDate = ParseDateForString(jp.getValue("ODPaidDate"));
                bc.Entity.ODFeeReceiver = user.Entity.UserName;
                bc.Entity.ODFeeReceiveRemark = jp.getValue("ODFeeReceiveRemark");
                bc.Entity.ODPaidType = jp.getValue("ODPaidType");
                bc.Entity.ODPaidBank = BankName;
                bc.Entity.ODCreator = user.Entity.UserName;
                bc.Entity.ODCreateDate = GetDate();
                bc.Entity.ODLastReviser = user.Entity.UserName;
                bc.Entity.ODLastRevisedDate = GetDate();
                //int r = bc.Save(frid, "insert");
                //if (r <= 0)
                //    flag = "2";

                string InfoMsg = GenOrderPayment(jp.getValue("id"), frid, TmpName, jp.getValue("ODPaidDate"), jp.getValue("ODPaidType"), BankName, jp.getValue("ODFeeReceiveRemark"), user.Entity.UserName);
                if (InfoMsg != "")
                {
                    flag = "4";
                    collection.Add(new JsonStringValue("info", InfoMsg));
                }
                else
                {
                    //obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='2',PaidinAmount=ISNULL((SELECT SUM(ISNULL(ODPaidAmount,0)) FROM Op_OrderFeeReceiver WHERE RefRP=Op_OrderHeader.RowPointer),0) where RowPointer='" + jp.getValue("id") + "'");
                    //obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='3' where PaidinAmount>=ARAmount and RowPointer='" + jp.getValue("id") + "'");

                    string company = "";
                    //-------------------------同步U8-------------------------
                    //------------抬头------------
                    JsonObjectCollection importInfo = new JsonObjectCollection();
                    importInfo.Add(new JsonStringValue("user", "Admin"));
                    importInfo.Add(new JsonStringValue("password", ""));
                    //------------抬头------------

                    //------------表头------------
                    Business.Op.BusinessOrderDetail detail = new Business.Op.BusinessOrderDetail();
                    System.Collections.ICollection list = detail.GetListQuery(jp.getValue("id"));

                    JsonObjectCollection importDsJson = new JsonObjectCollection();
                    JsonObjectCollection mtb = new JsonObjectCollection();
                    mtb.Add(new JsonStringValue("ytype", "应收凭证"));
                    mtb.Add(new JsonStringValue("FDate", bc.Entity.ODPaidDate.ToString("yyyy/M/d")));

                    int cnt = 0;
                    string SPNo = "";
                    string JFSubject = "";
                    //string BankAccount = "";
                    string CashAccount = "";
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
                            // SRVName += it.ODSRVName.Replace("公摊", "") + "、";
                            SRVName += Regex.Replace(Regex.Replace(it.ODSRVName.Replace("公摊", ""), @"\(.*\)", ""), @"\（.*\）","") + "、";
                        }
                        if (cnt == 0 && it.ODARAmount > 0)
                        {
                            DataTable dt1 = obj.PopulateDataSet("select APNo from Mstr_ChargeAccount where CANo='" + it.ODCANo + "'").Tables[0];
                            if (dt1.Rows.Count > 0)
                                JFSubject = dt1.Rows[0]["APNo"].ToString();

                            SPNo = it.ODContractSPNo;

                            DataTable dt2 = obj.PopulateDataSet("select U8Account,BankAccount,CashAccount from Mstr_ServiceProvider where SPNo='" + SPNo + "'").Tables[0];
                            if (dt2.Rows.Count > 0)
                            {
                                company = dt2.Rows[0]["U8Account"].ToString();
                                //BankAccount = dt2.Rows[0]["BankAccount"].ToString();
                                CashAccount = dt2.Rows[0]["CashAccount"].ToString();
                            }
                            cnt++;
                        }


                    }
                    if (ResourceNo.Length > 1)
                    {
                        if (bc.Entity.ODPaidType == "银行")
                            FID = bc.Entity.ODPaidBank + "-" + ResourceNo.Substring(0, ResourceNo.Length - 1);
                        else
                            FID = bc.Entity.ODPaidType + "-" + ResourceNo.Substring(0, ResourceNo.Length - 1);
                    }
                    else
                    {
                        if (bc.Entity.ODPaidType == "银行")
                            FID = bc.Entity.ODPaidBank;
                        else
                            FID = bc.Entity.ODPaidType;
                    }
                    if (SRVName.Length > 1) SRVName = SRVName.Substring(0, SRVName.Length - 1);

                    FID += "-" + bo.Entity.CustShortName;
                    FID += "-" + bo.Entity.OrderTime.ToString("yyyy/MM");
                    FID += "-(" + SRVName + ")";


                    //如果长度 > 60 ,则自动缩减房间号长度
                    if (FID.Length > 60)
                    {
                        int len = 60 - (FID.Length - (ResourceNo.Length - 1));
                        if (len < 0) len = 0;

                        if (ResourceNo.Length > 1)
                        {
                            if (bc.Entity.ODPaidType == "银行")
                                FID = bc.Entity.ODPaidBank + "-" + ResourceNo.Substring(0, len);
                            else
                                FID = bc.Entity.ODPaidType + "-" + ResourceNo.Substring(0, len);
                        }
                        else
                        {
                            if (bc.Entity.ODPaidType == "银行")
                                FID = bc.Entity.ODPaidBank;
                            else
                                FID = bc.Entity.ODPaidType;
                        }

                        FID += "-" + bo.Entity.CustShortName;
                        FID += "-" + bo.Entity.OrderTime.ToString("yyyy/MM");
                        FID += "-(" + SRVName + ")";
                    }


                    mtb.Add(new JsonStringValue("FID", FID));
                    if (bc.Entity.ODPaidType == "银行")
                        mtb.Add(new JsonStringValue("JFSubject", BankAccount));
                    else
                        mtb.Add(new JsonStringValue("JFSubject", CashAccount));
                    importDsJson.Add(new JsonObjectCollection("mtb", mtb));
                    //------------表头------------
                    if (company == "001")
                        importInfo.Add(new JsonStringValue("type", "SKPZ"));
                    else
                        importInfo.Add(new JsonStringValue("type", "Line_SKPZ"));
                    importInfo.Add(new JsonStringValue("company", company));

                    //------------表体------------
                    JsonObjectCollection dtb = new JsonObjectCollection();
                    JsonObjectCollection collection1 = new JsonObjectCollection();
                    collection1.Add(new JsonStringValue("ytype", "应收凭证"));
                    collection1.Add(new JsonStringValue("FID", FID));
                    collection1.Add(new JsonStringValue("FNote", FID));
                    collection1.Add(new JsonStringValue("Trading", ""));
                    collection1.Add(new JsonStringValue("DFSubject", JFSubject));
                    collection1.Add(new JsonStringValue("MTID", "RMB"));
                    collection1.Add(new JsonStringValue("FCurrency", "人民币"));
                    collection1.Add(new JsonStringValue("Amount", bc.Entity.ODPaidAmount.ToString("0.##")));
                    collection1.Add(new JsonStringValue("cus", bo.Entity.CustNo));
                    collection1.Add(new JsonStringValue("taxamount", "0"));
                    dtb.Add(new JsonObjectCollection(collection1));

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
                            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                                jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"), jp.getValue("SPNo"), ParseIntForString(jp.getValue("page")))));
                        }
                        else
                        {
                            obj.ExecuteNonQuery("delete from Op_OrderFeeReceiver_OrderDetail where RefRP='" + frid + "'");
                            obj.ExecuteNonQuery("delete from Op_OrderFeeReceiver where RowPointer='" + frid + "'");
                            obj.ExecuteNonQuery("update Op_OrderDetail set ODPaidAmount = ISNULL((select sum(ODPaidAmount) from Op_OrderFeeReceiver_OrderDetail where DetailRP=Op_OrderDetail.RowPointer),0) where RefRP = '" + jp.getValue("id") + "'");
                            obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='2',PaidinAmount=ISNULL((SELECT SUM(ISNULL(ODPaidAmount,0)) FROM Op_OrderFeeReceiver WHERE RefRP=Op_OrderHeader.RowPointer),0) where RowPointer='" + jp.getValue("id") + "'");
                            obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='3' where PaidinAmount>=ARAmount-ISNULL(ReduceAmount,0) and RowPointer='" + jp.getValue("id") + "'");
                            obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='1' where RowPointer='" + jp.getValue("id") + "' and PaidinAmount=0");
                            flag = "4";
                            collection.Add(new JsonStringValue("info", "审核不成功！" + msg.msg));
                        }

                        //collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                        //    jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"), jp.getValue("SPNo"), ParseIntForString(jp.getValue("page")))));
                    }
                    catch (Exception ex)
                    {
                        obj.ExecuteNonQuery("delete from Op_OrderFeeReceiver_OrderDetail where RefRP='" + frid + "'");
                        obj.ExecuteNonQuery("delete from Op_OrderFeeReceiver where RowPointer='" + frid + "'");
                        obj.ExecuteNonQuery("update Op_OrderDetail set ODPaidAmount = ISNULL((select sum(ODPaidAmount) from Op_OrderFeeReceiver_OrderDetail where DetailRP=Op_OrderDetail.RowPointer),0) where RefRP = '" + jp.getValue("id") + "'");
                        obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='2',PaidinAmount=ISNULL((SELECT SUM(ISNULL(ODPaidAmount,0)) FROM Op_OrderFeeReceiver WHERE RefRP=Op_OrderHeader.RowPointer),0) where RowPointer='" + jp.getValue("id") + "'");
                        obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='3' where PaidinAmount>=ARAmount-ISNULL(ReduceAmount,0) and RowPointer='" + jp.getValue("id") + "'");
                        obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='1' where RowPointer='" + jp.getValue("id") + "' and PaidinAmount=0");
                        flag = "4";
                        collection.Add(new JsonStringValue("info", ex.Message));
                    }
                    //-------------------------同步U8-------------------------
                }
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "feesubmit"));
            collection.Add(new JsonStringValue("flag", flag));

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
                collection.Add(new JsonStringValue("ODCANo", bc.Entity.ODCANo));

                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate1"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string feedeleteaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessOrderFeeReceiver bc = new Business.Op.BusinessOrderFeeReceiver();
                bc.load(jp.getValue("itemid"));
                int row = bc.delete();
                if (row <= 0)
                {
                    flag = "2";
                }
                else
                {
                    obj.ExecuteNonQuery("delete from Op_OrderFeeReceiver_OrderDetail where RefRP='" + jp.getValue("itemid") + "'");
                    obj.ExecuteNonQuery("update Op_OrderDetail set ODPaidAmount = ISNULL((select sum(ODPaidAmount) from Op_OrderFeeReceiver_OrderDetail where DetailRP=Op_OrderDetail.RowPointer),0) where RefRP = '" + jp.getValue("id") + "'");
                    obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='2',PaidinAmount=ISNULL((SELECT SUM(ISNULL(ODPaidAmount,0)) FROM Op_OrderFeeReceiver WHERE RefRP=Op_OrderHeader.RowPointer),0) where RowPointer='" + jp.getValue("id") + "'");
                    obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='3' where PaidinAmount>=ARAmount-ISNULL(ReduceAmount,0) and RowPointer='" + jp.getValue("id") + "'");
                    obj.ExecuteNonQuery("update Op_OrderHeader set OrderStatus='1' where RowPointer='" + jp.getValue("id") + "' and PaidinAmount=0");


                    System.Text.StringBuilder sb = new System.Text.StringBuilder("");
                    sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\">");
                    sb.Append("<thead>");
                    sb.Append("<tr class=\"text-c\">");
                    sb.Append("<th style=\"width:5%\">序号</th>");
                    sb.Append("<th style=\"width:20%\">收款日期</th>");
                    sb.Append("<th style=\"width:15%\">收款金额</th>");
                    sb.Append("<th style=\"width:15%\">收款人</th>");
                    sb.Append("<th style=\"width:30%\">备注</th>");
                    sb.Append("<th style=\"width:15%\">操作</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    int r = 1;
                    sb.Append("<tbody id=\"ItemBody\">");
                    Business.Op.BusinessOrderFeeReceiver bc1 = new project.Business.Op.BusinessOrderFeeReceiver();
                    foreach (Entity.Op.EntityOrderFeeReceiver it in bc1.GetListQuery(jp.getValue("id")))
                    {
                        sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                        sb.Append("<td align='center'>" + r.ToString() + "</td>");
                        sb.Append("<td>" + it.ODPaidDate.ToString("yyyy-MM-dd") + "</td>");
                        sb.Append("<td>" + it.ODPaidAmount.ToString("0.####") + "</td>");
                        sb.Append("<td>" + it.ODFeeReceiver + "</td>");
                        sb.Append("<td>" + it.ODFeeReceiveRemark + "</td>");
                        sb.Append("<td><input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"feedelete('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                        sb.Append("</tr>");
                        r++;
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table>");

                    collection.Add(new JsonStringValue("feelist", sb.ToString()));

                    collection.Add(new JsonStringValue("liststr", createList(jp.getValue("OrderNoS"), jp.getValue("OrderTypeS"), jp.getValue("CustNoS"),
                        jp.getValue("OrderTimeS"), jp.getValue("MinOrderCreateDate"), jp.getValue("MaxOrderCreateDate"), jp.getValue("OrderStatusS"), jp.getValue("SPNo"), ParseIntForString(jp.getValue("page")))));
                }
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "feedelete"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }



        /// <summary>
        /// 生成收款金额，分配到明细
        /// </summary>
        /// <param name="OrderID">订单主键</param>
        /// <param name="TmpName">收款明细主键</param>
        /// <param name="TmpName">临时表名称</param>
        /// <param name="ODPaidDate">收款日期</param>
        /// <param name="ODPaidType">收款类型</param>
        /// <param name="BankName">银行名称</param>
        /// <param name="ODFeeReceiveRemark">备注</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public string GenOrderPayment(string OrderID, string RefRP, string TmpName, string ODPaidDate, string ODPaidType, string BankName, string ODFeeReceiveRemark, string UserName)
        {
            string InfoMsg = "";
            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GenOrderPayment", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@OrderID", SqlDbType.NVarChar, 36).Value = OrderID;
                command.Parameters.Add("@RefRP", SqlDbType.NVarChar, 36).Value = RefRP;
                command.Parameters.Add("@TmpName", SqlDbType.NVarChar, 30).Value = TmpName;
                command.Parameters.Add("@ODPaidDate", SqlDbType.NVarChar, 10).Value = ODPaidDate;
                command.Parameters.Add("@ODPaidType", SqlDbType.NVarChar, 10).Value = ODPaidType;
                command.Parameters.Add("@BankName", SqlDbType.NVarChar, 50).Value = BankName;
                command.Parameters.Add("@ODFeeReceiveRemark", SqlDbType.NVarChar, 300).Value = ODFeeReceiveRemark;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar, 30).Value = UserName;
                command.Parameters.Add("@InfoMsg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                con.Open();
                command.ExecuteNonQuery();
                InfoMsg = command.Parameters["@InfoMsg"].Value.ToString();
            }
            catch (Exception ex)
            {
                InfoMsg = ex.ToString();
            }
            finally
            {
                if (command != null)
                    command.Dispose();
                if (con != null)
                    con.Dispose();
            }
            return InfoMsg;
        }
    }
}