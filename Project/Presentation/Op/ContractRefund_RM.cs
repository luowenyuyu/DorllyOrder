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

namespace project.Presentation.Op
{
    public partial class ContractRefund_RM : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/ContractRefund_RM.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/ContractRefund_RM.aspx'";
                            DataTable dt = obj.PopulateDataSet(sqlstr).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                string rightCode = dt.Rows[0]["RightCode"].ToString();
                                if (rightCode.IndexOf("view") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"view()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("view") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"viewfee()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看费用明细</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                if (rightCode.IndexOf("applyrefund") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"applyrefund()\" class=\"btn btn-warning radius\"><i class=\"Hui-iconfont\">&#xe6e0;</i> 预约退租</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("cancelrefund") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"cancelrefund()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe66b;</i> 取消预约退租</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("refund") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"refund()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe631;</i> 确认退租</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"view()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"viewfee()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看费用明细</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"applyrefund()\" class=\"btn btn-warning radius\"><i class=\"Hui-iconfont\">&#xe6e0;</i> 预约退租</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"cancelrefund()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe66b;</i> 取消预约退租</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"refund()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe631;</i> 确认退租</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty, "01", string.Empty, string.Empty, string.Empty,
                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1);

                        date = GetDate().ToString("yyyy-MM-dd");

                        ContractTypeStr = "<select class=\"input-text required\" id=\"ContractType\">";
                        ContractTypeStr += "<option value=\"\"></option>";
                        ContractTypeStrS = "<select class=\"input-text size-MINI\" id=\"ContractTypeS\" style=\"width:120px;\" >";
                        ContractTypeStrS += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessContractType bc = new project.Business.Base.BusinessContractType();
                        foreach (Entity.Base.EntityContractType it in bc.GetListQuery(string.Empty, string.Empty))
                        {
                            ContractTypeStr += "<option value='" + it.ContractTypeNo + "'>" + it.ContractTypeName + "</option>";
                            ContractTypeStrS += "<option value='" + it.ContractTypeNo + "'>" + it.ContractTypeName + "</option>";
                        }
                        ContractTypeStr += "</select>";
                        ContractTypeStrS += "</select>";

                        ContractSPNoStr = "<select class=\"input-text required\" id=\"ContractSPNo\">";
                        ContractSPNoStr += "<option value=\"\"></option>";
                        ContractSPNoStrS = "<select class=\"input-text size-MINI\" id=\"ContractSPNoS\" style=\"width:120px;\" >";
                        ContractSPNoStrS += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessServiceProvider bc1 = new project.Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider it in bc1.GetListQuery(string.Empty, string.Empty, true))
                        {
                            ContractSPNoStr += "<option value='" + it.SPNo + "'>" + it.SPShortName + "</option>";
                            ContractSPNoStrS += "<option value='" + it.SPNo + "'>" + it.SPShortName + "</option>";
                        }
                        ContractSPNoStr += "</select>";
                        ContractSPNoStrS += "</select>";


                        Business.Base.BusinessSetting setting = new Business.Base.BusinessSetting();
                        setting.load("RMRentFee");

                        SRVNo1Str = "<select class=\"input-text size-MINI\" id=\"SRVNo1\">";
                        SRVNo1Str += "<option value='" + setting.Entity.SRVNo + "'>" + setting.Entity.SRVName + "</option>";
                        SRVNo1Str += "</select>";
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
        protected string ContractTypeStr = "";
        protected string ContractTypeStrS = "";
        protected string ContractSPNoStr = "";
        protected string ContractSPNoStrS = "";
        protected string SRVNo1Str = "";

        private string createList(string ContractNo, string ContractNoManual, string ContractType, string ContractSPNo, string ContractCustNo,
            string MinContractSignedDate, string MaxContractSignedDate, string MinContractEndDate, string MaxContractEndDate, string OffLeaseStatus,
            string MinOffLeaseActulDate, string MaxOffLeaseActulDate, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='6%'>合同类型</th>");
            sb.Append("<th width='7%'>合同编号</th>");
            sb.Append("<th width='9%'>手工合同编号</th>");
            sb.Append("<th width='13%'>客户名称</th>");
            sb.Append("<th width='8%'>合同签订日期</th>");
            sb.Append("<th width='8%'>合同生效日期</th>");
            sb.Append("<th width='8%'>合同到期日期</th>");
            sb.Append("<th width='5%'>合同状态</th>");
            sb.Append("<th width='6%'>退租处理状态</th>");
            sb.Append("<th width='8%'>预约退租日期</th>");
            sb.Append("<th width='8%'>实际退租日期</th>");
            sb.Append("<th width='10%'>退租原因</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
            foreach (Entity.Op.EntityContract it in bc.GetRefundListQuery(ContractNo, ContractNoManual, ContractType, ContractSPNo, ContractCustNo,
                            ParseSearchDateForString(MinContractSignedDate), ParseSearchDateForString(MaxContractSignedDate),
                            ParseSearchDateForString(MinContractEndDate), ParseSearchDateForString(MaxContractEndDate), OffLeaseStatus,
                            ParseSearchDateForString(MinOffLeaseActulDate), ParseSearchDateForString(MaxOffLeaseActulDate), page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.ContractTypeName + "</td>");
                sb.Append("<td>" + it.ContractNo + "</td>");
                sb.Append("<td>" + it.ContractNoManual + "</td>");
                sb.Append("<td>" + it.ContractCustName + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.ContractSignedDate) + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.ContractStartDate) + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.ContractEndDate) + "</td>");
                if (it.ContractStatus == "1")
                    sb.Append("<td><label style=\"color:red;\">" + it.ContractStatusName + "</span></td>");
                else if (it.ContractStatus == "2")
                    sb.Append("<td><label style=\"color:blue;\">" + it.ContractStatusName + "</span></td>");
                else if (it.ContractStatus == "3")
                    sb.Append("<td><label style=\"color:green;\">" + it.ContractStatusName + "</span></td>");
                else
                    sb.Append("<td><label style=\"color:gray;\">" + it.ContractStatusName + "</span></td>");
                sb.Append("<td><span class=\"label " + (it.OffLeaseStatus == "1" ? "label-success" : "") + " radius\">" + it.OffLeaseStatusName + "</span></td>");
                sb.Append("<td>" + ParseStringForDate(it.OffLeaseScheduleDate) + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.OffLeaseActulDate) + "</td>");
                sb.Append("<td>" + it.OffLeaseReason + "</td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(bc.GetRefundListCount(ContractNo, ContractNoManual, ContractType, ContractSPNo, ContractCustNo,
                            ParseSearchDateForString(MinContractSignedDate), ParseSearchDateForString(MaxContractSignedDate),
                            ParseSearchDateForString(MinContractEndDate), ParseSearchDateForString(MaxContractEndDate), OffLeaseStatus,
                            ParseSearchDateForString(MinOffLeaseActulDate), ParseSearchDateForString(MaxOffLeaseActulDate)), pageSize, page, 7));

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
            sb.Append("<th style=\"width:15%\">房屋编号</th>");
            sb.Append("<th style=\"width:13%\">面积(㎡)</th>");
            sb.Append("<th style=\"width:12%\">单价(元/㎡/月)</th>");
            sb.Append("<th style=\"width:15%\">所属费用项目</th>");
            sb.Append("<th style=\"width:25%\">位置</th>");
            sb.Append("<th style=\"width:15%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            sb.Append("<tbody id=\"ItemBody1\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessContractRMRentalDetail bc = new project.Business.Op.BusinessContractRMRentalDetail();
                foreach (Entity.Op.EntityContractRMRentalDetail it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.RMID + "</td>");
                    sb.Append("<td>" + it.RMArea.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.RentalUnitPrice.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.SRVName + "</td>");
                    sb.Append("<td>" + it.RMLoc + "</td>");
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
            if (jp.getValue("Type") == "cancelrefund") //确认退租
                result = cancelrefundaction(jp);
            else if (jp.getValue("Type") == "applyrefundsubmit")
                result = applyrefundsubmitaction(jp);
            else if (jp.getValue("Type") == "refundsubmit") //确认退租
                result = refundsubmitaction(jp);
            else if (jp.getValue("Type") == "view")
                result = viewaction(jp);
            else if (jp.getValue("Type") == "viewfee")
                result = viewfeeaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            else if (jp.getValue("Type") == "check")
                result = checkaction(jp);

            else if (jp.getValue("Type") == "itemupdate1")
                result = itemupdate1action(jp);
            return result;
        }

        private string cancelrefundaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            try
            {
                Business.Op.BusinessContract bc = new Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                if (bc.Entity.OffLeaseStatus != "2")
                {
                    flag = "3";
                }
                else
                {
                    bc.Entity.OffLeaseStatus = "1";
                    bc.Entity.OffLeaseApplyDate = DateTime.MinValue.AddYears(1900);
                    bc.Entity.OffLeaseScheduleDate = DateTime.MinValue.AddYears(1900);
                    bc.Entity.OffLeaseReason = "";
                    bc.refund();
                }
            }
            catch { }

            collection.Add(new JsonStringValue("type", "cancelrefund"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("ContractNoS"), jp.getValue("ContractNoManualS"), jp.getValue("ContractTypeS"),
                jp.getValue("ContractSPNoS"), jp.getValue("ContractCustNoS"), jp.getValue("MinContractSignedDate"), jp.getValue("MaxContractSignedDate"),
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string applyrefundsubmitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            try
            {
                Business.Op.BusinessContract bc = new Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                if (bc.Entity.OffLeaseStatus != "1")
                {
                    flag = "3";
                }
                else
                {
                    bc.Entity.OffLeaseStatus = "2";
                    bc.Entity.OffLeaseApplyDate = GetDate();
                    bc.Entity.OffLeaseScheduleDate = ParseDateForString(jp.getValue("OffLeaseScheduleDate"));
                    bc.Entity.OffLeaseReason = jp.getValue("OffLeaseReason");
                    bc.refund();
                }
            }
            catch { }

            collection.Add(new JsonStringValue("type", "applyrefundsubmit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("ContractNoS"), jp.getValue("ContractNoManualS"), jp.getValue("ContractTypeS"),
                jp.getValue("ContractSPNoS"), jp.getValue("ContractCustNoS"), jp.getValue("MinContractSignedDate"), jp.getValue("MaxContractSignedDate"),
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string refundsubmitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            try
            {
                Business.Op.BusinessContract bc = new Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                if (bc.Entity.ContractStatus != "2")
                {
                    flag = "3";
                }
                else
                {
                    //string sqlcmd = "UPDATE Op_Contract SET ContractStatus='3',OffLeaseStatus='3',OffLeaseActulDate=GETDATE() "+
                    //            "WHERE RowPointer='"+jp.getValue("id")+"'";
                    //int r = obj.ExecuteNonQuery(sqlcmd);
                    //if (r <= 0)
                    //{
                    //    flag = "2";
                    //}
                    //else
                    //{
                    //    obj.ExecuteNonQuery("UPDATE Mstr_Room SET RMCurrentCustNo='',RMStatus='free' WHERE RMID IN(SELECT RMID FROM Op_ContractRMRentalDetail WHERE RefRP='" + jp.getValue("id") + "') ");
                    //    obj.ExecuteNonQuery("UPDATE Mstr_Customer SET CustStatus = '2' "+
                    //        "WHERE CustNo='" + bc.Entity.ContractCustNo + "' AND (SELECT COUNT(1) FROM Op_Contract WHERE ContractCustNo='" + bc.Entity.ContractCustNo + "' AND ContractStatus='2' AND ContractType='01')=0 ");


                    //    collection.Add(new JsonStringValue("liststr", createList(jp.getValue("ContractNoS"), jp.getValue("ContractNoManualS"), jp.getValue("ContractTypeS"),
                    //            jp.getValue("ContractSPNoS"), jp.getValue("ContractCustNoS"), jp.getValue("MinContractSignedDate"), jp.getValue("MaxContractSignedDate"),
                    //            jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                    //            jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));
                    //}

                    string InfoBar = refund(jp.getValue("id"), jp.getValue("RefundDate"), user.Entity.UserName);
                    if (InfoBar != "")
                    {
                        collection.Add(new JsonStringValue("InfoBar", InfoBar));
                        flag = "4";
                    }
                    else
                    {
                        collection.Add(new JsonStringValue("liststr", createList(jp.getValue("ContractNoS"), jp.getValue("ContractNoManualS"), jp.getValue("ContractTypeS"),
                                jp.getValue("ContractSPNoS"), jp.getValue("ContractCustNoS"), jp.getValue("MinContractSignedDate"), jp.getValue("MaxContractSignedDate"),
                                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));


                        #region 同步到资源系统

                        ResourceService.ResourceService srv = new ResourceService.ResourceService();
                        string Items = "";

                        Business.Base.BusinessCustomer cust = new Business.Base.BusinessCustomer();
                        cust.load(bc.Entity.ContractCustNo);
                        DataTable dt = obj.PopulateDataSet("SELECT RMID FROM Op_ContractRMRentalDetail WHERE RefRP='" + bc.Entity.RowPointer + "' GROUP BY RMID").Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            SycnResourceStatus rs = new SycnResourceStatus();
                            rs.SysID = 1;
                            rs.ResourceID = dr["RMID"].ToString();
                            rs.BusinessID = bc.Entity.RowPointer;
                            rs.BusinessNo = bc.Entity.ContractNo;
                            rs.BusinessType = 1;
                            rs.RentBeginTime = bc.Entity.FeeStartDate;
                            rs.RentEndTime = ParseDateForString(jp.getValue("RefundDate"));
                            rs.Status = 2;
                            rs.RentType = 1;
                            rs.UpdateTime = GetDate();
                            rs.UpdateUser = user.Entity.UserName;

                            Items += (Items == "" ? "" : ",") + JsonConvert.SerializeObject(rs);
                        }

                        string syncResult = srv.LeaseOut("[" + Items + "]");
                        collection.Add(new JsonStringValue("sync", syncResult));

                        #endregion
                    }
                }
            }
            catch { }

            collection.Add(new JsonStringValue("type", "refundsubmit"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
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
        private string viewaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                collection.Add(new JsonStringValue("ContractNo", bc.Entity.ContractNo));
                collection.Add(new JsonStringValue("ContractType", bc.Entity.ContractType));
                collection.Add(new JsonStringValue("ContractSPNo", bc.Entity.ContractSPNo));
                collection.Add(new JsonStringValue("ContractCustNo", bc.Entity.ContractCustNo));
                collection.Add(new JsonStringValue("ContractCustName", bc.Entity.ContractCustName));
                collection.Add(new JsonStringValue("ContractNoManual", bc.Entity.ContractNoManual));
                collection.Add(new JsonStringValue("ContractHandler", bc.Entity.ContractHandler));
                collection.Add(new JsonStringValue("ContractSignedDate", ParseStringForDate(bc.Entity.ContractSignedDate)));
                collection.Add(new JsonStringValue("ContractStartDate", ParseStringForDate(bc.Entity.ContractStartDate)));
                collection.Add(new JsonStringValue("ContractEndDate", ParseStringForDate(bc.Entity.ContractEndDate)));
                collection.Add(new JsonStringValue("EntryDate", ParseStringForDate(bc.Entity.EntryDate)));
                collection.Add(new JsonStringValue("FeeStartDate", ParseStringForDate(bc.Entity.FeeStartDate)));
                collection.Add(new JsonStringValue("ReduceStartDate1", ParseStringForDate(bc.Entity.ReduceStartDate1)));
                collection.Add(new JsonStringValue("ReduceEndDate1", ParseStringForDate(bc.Entity.ReduceEndDate1)));
                collection.Add(new JsonStringValue("ReduceStartDate2", ParseStringForDate(bc.Entity.ReduceStartDate2)));
                collection.Add(new JsonStringValue("ReduceEndDate2", ParseStringForDate(bc.Entity.ReduceEndDate2)));
                collection.Add(new JsonStringValue("ReduceStartDate3", ParseStringForDate(bc.Entity.ReduceStartDate3)));
                collection.Add(new JsonStringValue("ReduceEndDate3", ParseStringForDate(bc.Entity.ReduceEndDate3)));
                collection.Add(new JsonStringValue("ReduceStartDate4", ParseStringForDate(bc.Entity.ReduceStartDate4)));
                collection.Add(new JsonStringValue("ContractLatefeeRate", bc.Entity.ContractLatefeeRate.ToString("0.####")));
                collection.Add(new JsonStringValue("RMRentalDeposit", bc.Entity.RMRentalDeposit.ToString("0.####")));
                collection.Add(new JsonStringValue("RMUtilityDeposit", bc.Entity.RMUtilityDeposit.ToString("0.####")));
                collection.Add(new JsonStringValue("PropertyFeeStartDate", ParseStringForDate(bc.Entity.PropertyFeeStartDate)));
                collection.Add(new JsonStringValue("PropertyFeeReduceStartDate", ParseStringForDate(bc.Entity.PropertyFeeReduceStartDate)));
                collection.Add(new JsonStringValue("PropertyFeeReduceEndDate", ParseStringForDate(bc.Entity.PropertyFeeReduceEndDate)));
                collection.Add(new JsonStringValue("WaterUnitPrice", bc.Entity.WaterUnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("ElecticityUintPrice", bc.Entity.ElecticityUintPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("AirconUnitPrice", bc.Entity.AirconUnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("PropertyUnitPrice", bc.Entity.PropertyUnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("SharedWaterFee", bc.Entity.SharedWaterFee.ToString("0.####")));
                collection.Add(new JsonStringValue("SharedElectricyFee", bc.Entity.SharedElectricyFee.ToString("0.####")));
                collection.Add(new JsonStringValue("WPRentalDeposit", bc.Entity.WPRentalDeposit.ToString("0.####")));
                collection.Add(new JsonStringValue("WPUtilityDeposit", bc.Entity.WPUtilityDeposit.ToString("0.####")));
                collection.Add(new JsonStringValue("WPQTY", bc.Entity.WPQTY.ToString()));
                collection.Add(new JsonStringValue("WPElectricyLimit", bc.Entity.WPElectricyLimit.ToString("0.####")));
                collection.Add(new JsonStringValue("WPOverElectricyPrice", bc.Entity.WPOverElectricyPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("BBQTY", bc.Entity.BBQTY.ToString()));
                collection.Add(new JsonStringValue("BBAmount", bc.Entity.BBAmount.ToString("0.####")));
                collection.Add(new JsonStringValue("IncreaseStartDate1", ParseStringForDate(bc.Entity.IncreaseStartDate1)));
                collection.Add(new JsonStringValue("IncreaseRate1", bc.Entity.IncreaseRate1.ToString("0.####")));
                collection.Add(new JsonStringValue("IncreaseStartDate2", ParseStringForDate(bc.Entity.IncreaseStartDate2)));
                collection.Add(new JsonStringValue("IncreaseRate2", bc.Entity.IncreaseRate2.ToString("0.####")));
                collection.Add(new JsonStringValue("IncreaseStartDate3", ParseStringForDate(bc.Entity.IncreaseStartDate3)));
                collection.Add(new JsonStringValue("IncreaseRate3", bc.Entity.IncreaseRate3.ToString("0.####")));
                collection.Add(new JsonStringValue("IncreaseStartDate4", ParseStringForDate(bc.Entity.IncreaseStartDate4)));
                collection.Add(new JsonStringValue("IncreaseRate4", bc.Entity.IncreaseRate4.ToString("0.####")));
                collection.Add(new JsonStringValue("OffLeaseStatus", bc.Entity.OffLeaseStatus));
                collection.Add(new JsonStringValue("OffLeaseApplyDate", ParseStringForDate(bc.Entity.OffLeaseApplyDate)));
                collection.Add(new JsonStringValue("OffLeaseScheduleDate", ParseStringForDate(bc.Entity.OffLeaseScheduleDate)));
                collection.Add(new JsonStringValue("OffLeaseActulDate", ParseStringForDate(bc.Entity.OffLeaseActulDate)));
                collection.Add(new JsonStringValue("OffLeaseReason", bc.Entity.OffLeaseReason));
                collection.Add(new JsonStringValue("ContractCreator", bc.Entity.ContractCreator));
                collection.Add(new JsonStringValue("ContractCreateDate", ParseStringForDate(bc.Entity.ContractCreateDate)));
                collection.Add(new JsonStringValue("ContractLastReviser", bc.Entity.ContractLastReviser));
                collection.Add(new JsonStringValue("ContractLastReviseDate", ParseStringForDate(bc.Entity.ContractLastReviseDate)));
                collection.Add(new JsonStringValue("ContractStatus", bc.Entity.ContractStatusName));
                collection.Add(new JsonStringValue("ContractAuditor", bc.Entity.ContractAuditor));
                collection.Add(new JsonStringValue("ContractAuditDate", ParseStringForDate(bc.Entity.ContractAuditDate)));
                collection.Add(new JsonStringValue("ContractFinanceAuditor", bc.Entity.ContractFinanceAuditor));
                collection.Add(new JsonStringValue("ContractFinanceAuditDate", ParseStringForDate(bc.Entity.ContractFinanceAuditDate)));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));

                collection.Add(new JsonStringValue("ContractAttachment", bc.Entity.ContractAttachment));
                string files = "";
                foreach (string str in bc.Entity.ContractAttachment.Replace("<file>", "").Replace("</file>", "^").Split('^'))
                {
                    if (str == "") continue;
                    files += "<span style=\"margin-left:10px;\"><a href=\"..\\..\\upload\\" + str + "\">" + str + "</a>&nbsp;" +
                        "<button id=\"" + getRandom() + "\" onclick=\"deletefile(this.id,'" + str + "')\">删除</button></span>";
                }
                collection.Add(new JsonStringValue("files", files));

                collection.Add(new JsonStringValue("itemlist1", createItemList1(bc.Entity.RowPointer, "view")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "view"));
            collection.Add(new JsonStringValue("flag", flag));

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
                sb.Append("<th style=\"width:15%\">费用项目</th>");
                sb.Append("<th style=\"width:20%\">房屋编号</th>");
                sb.Append("<th style=\"width:15%\">起始日期</th>");
                sb.Append("<th style=\"width:15%\">截止日期</th>");
                sb.Append("<th style=\"width:15%\">金额</th>");
                sb.Append("<th style=\"width:15%\">是否生成订单</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                int r = 1;
                sb.Append("<tbody id=\"ItemBody1\">");
                Business.Op.BusinessContractRMRentList bc = new project.Business.Op.BusinessContractRMRentList();
                foreach (Entity.Op.EntityContractRMRentList it in bc.GetListQuery(jp.getValue("id")))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.SRVName + "</td>");
                    sb.Append("<td>" + it.RMID + "</td>");
                    sb.Append("<td>" + ParseStringForDate(it.FeeStartDate) + "</td>");
                    sb.Append("<td>" + ParseStringForDate(it.FeeEndDate) + "</td>");
                    sb.Append("<td>" + it.FeeAmount.ToString("0.####") + "</td>");
                    sb.Append("<td><span style=\"color:" + (it.FeeStatus == "0" ? "red" : "blue") + ";\">" + (it.FeeStatus == "0" ? "未生成" : "已生成") + "</span></td>");
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
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("ContractNoS"), jp.getValue("ContractNoManualS"), jp.getValue("ContractTypeS"),
                jp.getValue("ContractSPNoS"), jp.getValue("ContractCustNoS"), jp.getValue("MinContractSignedDate"), jp.getValue("MaxContractSignedDate"),
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("ContractNoS"), jp.getValue("ContractNoManualS"), jp.getValue("ContractTypeS"),
                jp.getValue("ContractSPNoS"), jp.getValue("ContractCustNoS"), jp.getValue("MinContractSignedDate"), jp.getValue("MaxContractSignedDate"),
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }


        //房屋信息
        private string itemupdate1action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractRMRentalDetail bc = new Business.Op.BusinessContractRMRentalDetail();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("SRVNo", bc.Entity.SRVNo));
                collection.Add(new JsonStringValue("RMLoc", bc.Entity.RMLoc));
                collection.Add(new JsonStringValue("RMArea", bc.Entity.RMArea.ToString("0.####")));
                collection.Add(new JsonStringValue("RentalUnitPrice", bc.Entity.RentalUnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));
                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate1"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }




        /// </summary>
        /// 确认 
        /// </summary>
        public string refund(string ContractID, string RefundDate, string UserName)
        {
            string InfoMsg = "";
            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GenOrderFromRefund_RM", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ContractID", SqlDbType.NVarChar, 36).Value = ContractID;
                command.Parameters.Add("@RefundDate", SqlDbType.NVarChar, 10).Value = RefundDate;
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