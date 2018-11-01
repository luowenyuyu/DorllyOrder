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
    public partial class Contract_PT : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/Contract_PT.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/Contract_PT.aspx'";
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
                                if (rightCode.IndexOf("insert") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"clone()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe604;</i> 复制</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("view") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"view()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("view") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"viewfee()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看费用明细</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                                if (rightCode.IndexOf("approve") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"approve()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 审核</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("void") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"invalid()\" class=\"btn btn-warning radius\"><i class=\"Hui-iconfont\">&#xe60b;</i> 作废</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"clone()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe604;</i> 复制</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"view()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"viewfee()\" class=\"btn btn-secondary radius\"><i class=\"Hui-iconfont\">&#xe627;</i> 查看费用明细</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"approve()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 审核</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"invalid()\" class=\"btn btn-warning radius\"><i class=\"Hui-iconfont\">&#xe60b;</i> 作废</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty, "04", string.Empty, string.Empty, string.Empty,
                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1);

                        date = GetDate().ToString("yyyy-MM-dd");

                        ContractTypeStr = "<select class=\"input-text required\" id=\"ContractType\" disabled=\"disabled\">";
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

                        //管理费 空调费 水费 电费 超额电费 固定服务费
                        Business.Base.BusinessSetting setting = new Business.Base.BusinessSetting();
                        setting.load("PropertyFee");
                        PTRentSRVNo = setting.Entity.SRVNo;
                        PTRentFee = setting.Entity.DecimalValue.ToString("0.####");

                        Business.Base.BusinessSetting setting1 = new Business.Base.BusinessSetting();
                        setting1.load("AirConditionFee");

                        Business.Base.BusinessSetting setting2 = new Business.Base.BusinessSetting();
                        setting2.load("WaterFee");

                        Business.Base.BusinessSetting setting3 = new Business.Base.BusinessSetting();
                        setting3.load("ElectricFee");

                        Business.Base.BusinessSetting setting4 = new Business.Base.BusinessSetting();
                        setting4.load("SharedWaterFee");

                        Business.Base.BusinessSetting setting5 = new Business.Base.BusinessSetting();
                        setting5.load("SharedElectricFee");

                        Business.Base.BusinessSetting setting6 = new Business.Base.BusinessSetting();
                        setting6.load("ServiceCharge");

                        SRVNo4Str = "<select class=\"input-text size-MINI\" id=\"SRVNo4\">";
                        SRVNo4Str += "<option value='" + setting.Entity.SRVNo + "'>" + setting.Entity.SRVName + "</option>";
                        SRVNo4Str += "<option value='" + setting1.Entity.SRVNo + "'>" + setting1.Entity.SRVName + "</option>";
                        SRVNo4Str += "<option value='" + setting2.Entity.SRVNo + "'>" + setting2.Entity.SRVName + "</option>";
                        SRVNo4Str += "<option value='" + setting4.Entity.SRVNo + "'>" + setting4.Entity.SRVName + "</option>";
                        SRVNo4Str += "<option value='" + setting3.Entity.SRVNo + "'>" + setting3.Entity.SRVName + "</option>";
                        SRVNo4Str += "<option value='" + setting5.Entity.SRVNo + "'>" + setting5.Entity.SRVName + "</option>";
                        SRVNo4Str += "<option value='" + setting6.Entity.SRVNo + "'>" + setting6.Entity.SRVName + "</option>";
                        SRVNo4Str += "</select>";

                        setting.load("PTSPNo");
                        PTSPNo = setting.Entity.StringValue;
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
        protected string SRVNo4Str = "";
        protected string PTRentSRVNo = "";
        protected string PTRentFee = "";
        protected string PTSPNo = "";

        private string createList(string ContractNo, string ContractNoManual, string ContractType, string ContractSPNo, string ContractCustNo,
            string MinContractSignedDate, string MaxContractSignedDate, string MinContractEndDate, string MaxContractEndDate, string ContractStatusS,
            string OffLeaseStatus, string MinOffLeaseActulDate, string MaxOffLeaseActulDate, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='8%'>合同编号</th>");
            sb.Append("<th width='10%'>手工合同编号</th>");
            sb.Append("<th width='16%'>客户名称</th>");
            sb.Append("<th width='8%'>合同签订日期</th>");
            sb.Append("<th width='8%'>租金起收日期</th>");
            sb.Append("<th width='8%'>合同到期日期</th>");
            sb.Append("<th width='6%'>合同状态</th>");
            sb.Append("<th width='6%'>退租处理状态</th>");
            sb.Append("<th width='8%'>预约退租日期</th>");
            sb.Append("<th width='8%'>实际退租日期</th>");
            sb.Append("<th width='10%'>退租原因</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
            foreach (Entity.Op.EntityContract it in bc.GetListQuery(ContractNo, ContractNoManual, ContractType, ContractSPNo, ContractCustNo,
                            ParseSearchDateForString(MinContractSignedDate), ParseSearchDateForString(MaxContractSignedDate),
                            ParseSearchDateForString(MinContractEndDate), ParseSearchDateForString(MaxContractEndDate), ContractStatusS, OffLeaseStatus,
                            ParseSearchDateForString(MinOffLeaseActulDate), ParseSearchDateForString(MaxOffLeaseActulDate), page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.ContractNo + "</td>");
                sb.Append("<td>" + it.ContractNoManual + "</td>");
                sb.Append("<td>" + it.ContractCustName + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.ContractSignedDate) + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.FeeStartDate) + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.ContractEndDate) + "</td>");
                if (it.ContractStatus == "1")
                    sb.Append("<td><label style=\"color:red;\">" + it.ContractStatusName + "</span></td>");
                else if (it.ContractStatus == "2")
                    sb.Append("<td><label style=\"color:blue;\">" + it.ContractStatusName + "</span></td>");
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

            sb.Append(Paginat(bc.GetListCount(ContractNo, ContractNoManual, ContractType, ContractSPNo, ContractCustNo,
                            ParseSearchDateForString(MinContractSignedDate), ParseSearchDateForString(MaxContractSignedDate),
                            ParseSearchDateForString(MinContractEndDate), ParseSearchDateForString(MaxContractEndDate), ContractStatusS, OffLeaseStatus,
                            ParseSearchDateForString(MinOffLeaseActulDate), ParseSearchDateForString(MaxOffLeaseActulDate)), pageSize, page, 7));

            return sb.ToString();
        }

        //管理费、空调费
        private string createItemList4(string RefRP, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\" style=\"width:1040px; table-layout:fixed; margin:0px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th style=\"width:5%\">序号</th>");
            sb.Append("<th style=\"width:13%\">费用项目</th>");
            sb.Append("<th style=\"width:28%\">房屋编号</th>");
            sb.Append("<th style=\"width:12%\">出租面积</th>");
            sb.Append("<th style=\"width:12%\">单价(元/㎡/月)</th>");
            sb.Append("<th style=\"width:20%\">备注</th>");
            sb.Append("<th style=\"width:10%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            sb.Append("<tbody id=\"ItemBody4\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessContractPropertyFee bc = new project.Business.Op.BusinessContractPropertyFee();
                foreach (Entity.Op.EntityContractPropertyFee it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.SRVName + "</td>");
                    sb.Append("<td>" + it.RMID + "</td>");
                    sb.Append("<td>" + it.RMArea.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.UnitPrice.ToString("0.######") + "</td>");
                    sb.Append("<td>" + it.Remark + "</td>");
                    if (type != "view")
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate4('" + it.RowPointer + "')\" value=\"修改\" />&nbsp;<input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"itemdel4('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                    else
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate4('" + it.RowPointer + "')\" value=\"查看\" /></td>");
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
            else if (jp.getValue("Type") == "clone")
                result = cloneaction(jp);
            else if (jp.getValue("Type") == "submit")
                result = submitaction(jp);
            else if (jp.getValue("Type") == "approve")
                result = approveaction(jp);
            else if (jp.getValue("Type") == "invalid")
                result = invalidaction(jp);
            else if (jp.getValue("Type") == "view")
                result = viewaction(jp);
            else if (jp.getValue("Type") == "viewfee")
                result = viewfeeaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            else if (jp.getValue("Type") == "checkcust")
                result = checkcustaction(jp);
            else if (jp.getValue("Type") == "check")
                result = checkaction(jp);

            else if (jp.getValue("Type") == "itemsave4")
                result = itemsave4action(jp);
            else if (jp.getValue("Type") == "itemupdate4")
                result = itemupdate4action(jp);
            else if (jp.getValue("Type") == "itemdel4")
                result = itemdel4action(jp);
            else if (jp.getValue("Type") == "getrmid4")
                result = getrmid4action(jp);
            else if (jp.getValue("Type") == "getprice4")
                result = getprice4action(jp);
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
                //AirConditionFee 空调费, ElectricFee 电费, PropertyFee	管理费, WaterFee 水费
                Business.Base.BusinessSetting bc = new Business.Base.BusinessSetting();
                bc.load("AirConditionFee");
                collection.Add(new JsonStringValue("AirConditionFee", bc.Entity.DecimalValue.ToString("0.####")));
                bc.load("ElectricFee");
                collection.Add(new JsonStringValue("ElectricFee", bc.Entity.DecimalValue.ToString("0.####")));
                bc.load("PropertyFee");
                collection.Add(new JsonStringValue("PropertyFee", bc.Entity.DecimalValue.ToString("0.####")));
                bc.load("WaterFee");
                collection.Add(new JsonStringValue("WaterFee", bc.Entity.DecimalValue.ToString("0.####")));

                collection.Add(new JsonStringValue("itemlist4", createItemList4(Guid.Empty.ToString(), "update")));
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
                Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                if (bc.Entity.ContractStatus == "2")
                {
                    flag = "3";
                }
                else
                {
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
                    collection.Add(new JsonStringValue("ReduceEndDate4", ParseStringForDate(bc.Entity.ReduceEndDate4)));
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

                    collection.Add(new JsonStringValue("itemlist4", createItemList4(bc.Entity.RowPointer, "update")));
                }
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "update"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string cloneaction(JsonArrayParse jp)
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
                collection.Add(new JsonStringValue("ReduceEndDate4", ParseStringForDate(bc.Entity.ReduceEndDate4)));
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

                collection.Add(new JsonStringValue("ContractAttachment", ""));
                collection.Add(new JsonStringValue("files", ""));

                collection.Add(new JsonStringValue("cloneid", jp.getValue("id")));

                collection.Add(new JsonStringValue("itemlist4", createItemList4(Guid.Empty.ToString(), "update")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "clone"));
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
                collection.Add(new JsonStringValue("ReduceEndDate4", ParseStringForDate(bc.Entity.ReduceEndDate4)));
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

                collection.Add(new JsonStringValue("itemlist4", createItemList4(bc.Entity.RowPointer, "view")));
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
                sb.Append("<th style=\"width:32%\">房屋编号</th>");
                sb.Append("<th style=\"width:12%\">起始日期</th>");
                sb.Append("<th style=\"width:12%\">截止日期</th>");
                sb.Append("<th style=\"width:12%\">金额</th>");
                sb.Append("<th style=\"width:12%\">是否生成订单</th>");
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
        private string deleteaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                if (bc.Entity.ContractStatus != "1")
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("ContractNoS"), jp.getValue("ContractNoManualS"), jp.getValue("ContractTypeS"),
                jp.getValue("ContractSPNoS"), jp.getValue("ContractCustNoS"), jp.getValue("MinContractSignedDate"), jp.getValue("MaxContractSignedDate"),
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("ContractStatusS"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.ContractType = jp.getValue("ContractType");
                    bc.Entity.ContractSPNo = jp.getValue("ContractSPNo");
                    bc.Entity.ContractCustNo = jp.getValue("ContractCustNo");
                    bc.Entity.ContractNoManual = jp.getValue("ContractNoManual");
                    bc.Entity.ContractHandler = jp.getValue("ContractHandler");
                    bc.Entity.ContractCustNo = jp.getValue("ContractCustNo");
                    bc.Entity.ContractSignedDate = ParseDateForString(jp.getValue("ContractSignedDate"));
                    bc.Entity.ContractStartDate = ParseDateForString(jp.getValue("ContractStartDate"));
                    bc.Entity.ContractEndDate = ParseDateForString(jp.getValue("ContractEndDate"));
                    bc.Entity.EntryDate = ParseDateForString(jp.getValue("EntryDate"));
                    bc.Entity.FeeStartDate = ParseDateForString(jp.getValue("FeeStartDate"));
                    bc.Entity.ReduceStartDate1 = ParseDateForString(jp.getValue("ReduceStartDate1"));
                    bc.Entity.ReduceEndDate1 = ParseDateForString(jp.getValue("ReduceEndDate1"));
                    bc.Entity.ReduceStartDate2 = ParseDateForString(jp.getValue("ReduceStartDate2"));
                    bc.Entity.ReduceEndDate2 = ParseDateForString(jp.getValue("ReduceEndDate2"));
                    bc.Entity.ReduceStartDate3 = ParseDateForString(jp.getValue("ReduceStartDate3"));
                    bc.Entity.ReduceEndDate3 = ParseDateForString(jp.getValue("ReduceEndDate3"));
                    bc.Entity.ReduceStartDate4 = ParseDateForString(jp.getValue("ReduceStartDate4"));
                    bc.Entity.ReduceEndDate4 = ParseDateForString(jp.getValue("ReduceEndDate4"));
                    bc.Entity.ContractLatefeeRate = ParseDecimalForString(jp.getValue("ContractLatefeeRate"));
                    bc.Entity.RMRentalDeposit = ParseDecimalForString(jp.getValue("RMRentalDeposit"));
                    bc.Entity.RMUtilityDeposit = ParseDecimalForString(jp.getValue("RMUtilityDeposit"));
                    bc.Entity.PropertyFeeStartDate = ParseDateForString(jp.getValue("PropertyFeeStartDate"));
                    bc.Entity.PropertyFeeReduceStartDate = ParseDateForString(jp.getValue("PropertyFeeReduceStartDate"));
                    bc.Entity.PropertyFeeReduceEndDate = ParseDateForString(jp.getValue("PropertyFeeReduceEndDate"));
                    bc.Entity.WaterUnitPrice = ParseDecimalForString(jp.getValue("WaterUnitPrice"));
                    bc.Entity.ElecticityUintPrice = ParseDecimalForString(jp.getValue("ElecticityUintPrice"));
                    bc.Entity.AirconUnitPrice = ParseDecimalForString(jp.getValue("AirconUnitPrice"));
                    bc.Entity.PropertyUnitPrice = ParseDecimalForString(jp.getValue("PropertyUnitPrice"));
                    bc.Entity.SharedWaterFee = ParseDecimalForString(jp.getValue("SharedWaterFee"));
                    bc.Entity.SharedElectricyFee = ParseDecimalForString(jp.getValue("SharedElectricyFee"));
                    bc.Entity.WPRentalDeposit = ParseDecimalForString(jp.getValue("WPRentalDeposit"));
                    bc.Entity.WPUtilityDeposit = ParseDecimalForString(jp.getValue("WPUtilityDeposit"));
                    bc.Entity.WPQTY = ParseIntForString(jp.getValue("WPQTY"));
                    bc.Entity.WPElectricyLimit = ParseDecimalForString(jp.getValue("WPElectricyLimit"));
                    bc.Entity.WPOverElectricyPrice = ParseDecimalForString(jp.getValue("WPOverElectricyPrice"));
                    bc.Entity.BBQTY = ParseIntForString(jp.getValue("BBQTY"));
                    bc.Entity.BBAmount = ParseDecimalForString(jp.getValue("BBAmount"));
                    bc.Entity.IncreaseStartDate1 = ParseDateForString(jp.getValue("IncreaseStartDate1"));
                    bc.Entity.IncreaseRate1 = ParseDecimalForString(jp.getValue("IncreaseRate1"));
                    bc.Entity.IncreaseStartDate2 = ParseDateForString(jp.getValue("IncreaseStartDate2"));
                    bc.Entity.IncreaseRate2 = ParseDecimalForString(jp.getValue("IncreaseRate2"));
                    bc.Entity.IncreaseStartDate3 = ParseDateForString(jp.getValue("IncreaseStartDate3"));
                    bc.Entity.IncreaseRate3 = ParseDecimalForString(jp.getValue("IncreaseRate3"));
                    bc.Entity.IncreaseStartDate4 = ParseDateForString(jp.getValue("IncreaseStartDate4"));
                    bc.Entity.IncreaseRate4 = ParseDecimalForString(jp.getValue("IncreaseRate4"));
                    bc.Entity.ContractAttachment = jp.getValue("ContractAttachment");
                    bc.Entity.Remark = jp.getValue("Remark");

                    bc.Entity.ContractLastReviser = user.Entity.UserName;
                    bc.Entity.ContractLastReviseDate = GetDate();

                    int r = bc.Save("update");

                    if (r <= 0)
                        flag = "2";
                    collection.Add(new JsonStringValue("RowPointer", jp.getValue("id")));
                }
                else
                {
                    string prefix = GetDate().ToString("yyyyMM");
                    string ContractNo = "";
                    DataTable dt = obj.PopulateDataSet("select top 1 ContractNo from Op_Contract where ContractNo like '" + prefix + "%' order by ContractNo desc").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            long num = long.Parse(dt.Rows[0]["ContractNo"].ToString());
                            ContractNo = (num + 1).ToString();
                        }
                        catch
                        {
                            ContractNo = prefix + "00001";
                        }
                    }
                    else
                    {
                        ContractNo = prefix + "00001";
                    }
                    string id = Guid.NewGuid().ToString();
                    bc.Entity.RowPointer = id;
                    bc.Entity.ContractNo = ContractNo;
                    bc.Entity.ContractType = jp.getValue("ContractType");
                    bc.Entity.ContractSPNo = jp.getValue("ContractSPNo");
                    bc.Entity.ContractCustNo = jp.getValue("ContractCustNo");
                    bc.Entity.ContractNoManual = jp.getValue("ContractNoManual");
                    bc.Entity.ContractHandler = jp.getValue("ContractHandler");
                    bc.Entity.ContractCustNo = jp.getValue("ContractCustNo");
                    bc.Entity.ContractSignedDate = ParseDateForString(jp.getValue("ContractSignedDate"));
                    bc.Entity.ContractStartDate = ParseDateForString(jp.getValue("ContractStartDate"));
                    bc.Entity.ContractEndDate = ParseDateForString(jp.getValue("ContractEndDate"));
                    bc.Entity.EntryDate = ParseDateForString(jp.getValue("EntryDate"));
                    bc.Entity.FeeStartDate = ParseDateForString(jp.getValue("FeeStartDate"));
                    bc.Entity.ReduceStartDate1 = ParseDateForString(jp.getValue("ReduceStartDate1"));
                    bc.Entity.ReduceEndDate1 = ParseDateForString(jp.getValue("ReduceEndDate1"));
                    bc.Entity.ReduceStartDate2 = ParseDateForString(jp.getValue("ReduceStartDate2"));
                    bc.Entity.ReduceEndDate2 = ParseDateForString(jp.getValue("ReduceEndDate2"));
                    bc.Entity.ReduceStartDate3 = ParseDateForString(jp.getValue("ReduceStartDate3"));
                    bc.Entity.ReduceEndDate3 = ParseDateForString(jp.getValue("ReduceEndDate3"));
                    bc.Entity.ReduceStartDate4 = ParseDateForString(jp.getValue("ReduceStartDate4"));
                    bc.Entity.ReduceEndDate4 = ParseDateForString(jp.getValue("ReduceEndDate4"));
                    bc.Entity.ContractLatefeeRate = ParseDecimalForString(jp.getValue("ContractLatefeeRate"));
                    bc.Entity.RMRentalDeposit = ParseDecimalForString(jp.getValue("RMRentalDeposit"));
                    bc.Entity.RMUtilityDeposit = ParseDecimalForString(jp.getValue("RMUtilityDeposit"));
                    bc.Entity.PropertyFeeStartDate = ParseDateForString(jp.getValue("PropertyFeeStartDate"));
                    bc.Entity.PropertyFeeReduceStartDate = ParseDateForString(jp.getValue("PropertyFeeReduceStartDate"));
                    bc.Entity.PropertyFeeReduceEndDate = ParseDateForString(jp.getValue("PropertyFeeReduceEndDate"));
                    bc.Entity.WaterUnitPrice = ParseDecimalForString(jp.getValue("WaterUnitPrice"));
                    bc.Entity.ElecticityUintPrice = ParseDecimalForString(jp.getValue("ElecticityUintPrice"));
                    bc.Entity.AirconUnitPrice = ParseDecimalForString(jp.getValue("AirconUnitPrice"));
                    bc.Entity.PropertyUnitPrice = ParseDecimalForString(jp.getValue("PropertyUnitPrice"));
                    bc.Entity.SharedWaterFee = ParseDecimalForString(jp.getValue("SharedWaterFee"));
                    bc.Entity.SharedElectricyFee = ParseDecimalForString(jp.getValue("SharedElectricyFee"));
                    bc.Entity.WPRentalDeposit = ParseDecimalForString(jp.getValue("WPRentalDeposit"));
                    bc.Entity.WPUtilityDeposit = ParseDecimalForString(jp.getValue("WPUtilityDeposit"));
                    bc.Entity.WPQTY = ParseIntForString(jp.getValue("WPQTY"));
                    bc.Entity.WPElectricyLimit = ParseDecimalForString(jp.getValue("WPElectricyLimit"));
                    bc.Entity.WPOverElectricyPrice = ParseDecimalForString(jp.getValue("WPOverElectricyPrice"));
                    bc.Entity.BBQTY = ParseIntForString(jp.getValue("BBQTY"));
                    bc.Entity.BBAmount = ParseDecimalForString(jp.getValue("BBAmount"));
                    bc.Entity.IncreaseType = "1";
                    bc.Entity.IncreaseStartDate1 = ParseDateForString(jp.getValue("IncreaseStartDate1"));
                    bc.Entity.IncreaseRate1 = ParseDecimalForString(jp.getValue("IncreaseRate1"));
                    bc.Entity.IncreaseStartDate2 = ParseDateForString(jp.getValue("IncreaseStartDate2"));
                    bc.Entity.IncreaseRate2 = ParseDecimalForString(jp.getValue("IncreaseRate2"));
                    bc.Entity.IncreaseStartDate3 = ParseDateForString(jp.getValue("IncreaseStartDate3"));
                    bc.Entity.IncreaseRate3 = ParseDecimalForString(jp.getValue("IncreaseRate3"));
                    bc.Entity.IncreaseStartDate4 = ParseDateForString(jp.getValue("IncreaseStartDate4"));
                    bc.Entity.IncreaseRate4 = ParseDecimalForString(jp.getValue("IncreaseRate4"));
                    bc.Entity.ContractAttachment = jp.getValue("ContractAttachment");
                    bc.Entity.Remark = jp.getValue("Remark");

                    bc.Entity.ContractCreator = user.Entity.UserName;
                    bc.Entity.ContractCreateDate = GetDate();
                    bc.Entity.ContractLastReviser = user.Entity.UserName;
                    bc.Entity.ContractLastReviseDate = GetDate();
                    bc.Entity.ContractStatus = "1";
                    int r = bc.Save("insert");
                    if (r <= 0)
                        flag = "2";
                    else
                    {
                        if (jp.getValue("copyid") != "") //复制
                        {
                            string KSQL = "insert into Op_ContractPropertyFee(RowPointer,RefRP,RMID,RMArea,UnitPrice,Remark,SRVNo,Creator,CreateDate,LastReviser,LastReviseDate)" +
                                "select NEWID(),'" + id + "',RMID,RMArea,UnitPrice,Remark,SRVNo," +
                                "'" + user.Entity.UserName + "',GetDate(),'" + user.Entity.UserName + "',GetDate() " +
                                "from Op_ContractPropertyFee where RefRP='" + jp.getValue("copyid") + "' order by CreateDate";
                            obj.ExecuteNonQuery(KSQL);

                            collection.Add(new JsonStringValue("itemlist4", createItemList4(id, "insert")));
                        }
                    }

                    collection.Add(new JsonStringValue("RowPointer", id));
                    collection.Add(new JsonStringValue("ContractNo", ContractNo));
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("submittp", jp.getValue("submittp")));
            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));

            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("ContractNoS"), jp.getValue("ContractNoManualS"), jp.getValue("ContractTypeS"),
                jp.getValue("ContractSPNoS"), jp.getValue("ContractCustNoS"), jp.getValue("MinContractSignedDate"), jp.getValue("MaxContractSignedDate"),
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("ContractStatusS"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string approveaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                if (bc.Entity.ContractStatus != "1" && bc.Entity.ContractStatus != "2")
                {
                    flag = "3";
                }
                else
                {
                    if (bc.Entity.ContractStatus == "1")
                    {
                        bc.Entity.ContractStatus = "2";
                        string InfoBar = bc.approve_PT(user.Entity.UserName);
                        if (InfoBar != "")
                        {
                            flag = "5";
                            collection.Add(new JsonStringValue("InfoBar", InfoBar));
                        }
                        else
                        {
                            #region 同步到资源系统
                            /*
                            ResourceService.ResourceService srv = new ResourceService.ResourceService();
                            string Items = "";

                            Business.Base.BusinessCustomer cust = new Business.Base.BusinessCustomer();
                            cust.load(bc.Entity.ContractCustNo);
                            DataTable dt1 = obj.PopulateDataSet("SELECT RMID FROM Op_ContractPropertyFee WHERE RefRP='" + bc.Entity.RowPointer + "' GROUP BY RMID").Tables[0];
                            foreach (DataRow dr in dt1.Rows)
                            {
                                SycnResourceStatus rs = new SycnResourceStatus();
                                rs.SysID = 1; //1.订单
                                rs.ResourceID = dr["RMID"].ToString();
                                rs.BusinessID = bc.Entity.RowPointer;
                                rs.BusinessNo = bc.Entity.ContractNo;
                                rs.BusinessType = 2;//1租赁，2物业
                                rs.RentBeginTime = bc.Entity.FeeStartDate;
                                rs.RentEndTime = bc.Entity.ContractEndDate;
                                rs.CustLongName = bc.Entity.ContractCustName;
                                rs.CustShortName = cust.Entity.CustShortName;
                                rs.CustTel = cust.Entity.CustTel;
                                rs.Status = 1;
                                rs.RentType = 1;
                                rs.UpdateTime = GetDate();
                                rs.UpdateUser = user.Entity.UserName;

                                Items += (Items == "" ? "" : ",") + JsonConvert.SerializeObject(rs);
                            }
                            string syncResult = srv.LeaseIn("[" + Items + "]");
                            collection.Add(new JsonStringValue("sync", syncResult)); 
                            */
                            #endregion
                        }
                        collection.Add(new JsonStringValue("status", bc.Entity.ContractStatus));
                    }
                    else
                    {
                        if (int.Parse(obj.PopulateDataSet("select Count(1) as Cnt from Op_ContractRMRentList where RefRP='" + bc.Entity.RowPointer + "' and FeeStatus='1'").Tables[0].Rows[0]["Cnt"].ToString()) > 0)
                        {
                            flag = "4";
                        }
                        else
                        {
                            bc.Entity.ContractStatus = "1";
                            bc.Entity.ContractAuditor = user.Entity.UserName;
                            bc.Entity.ContractAuditDate = GetDate();
                            bc.invalid();
                            collection.Add(new JsonStringValue("status", bc.Entity.ContractStatus));

                            #region 同步到资源系统
                            /*
                            ResourceService.ResourceService srv = new ResourceService.ResourceService();
                            string Items = "";

                            Business.Base.BusinessCustomer cust = new Business.Base.BusinessCustomer();
                            cust.load(bc.Entity.ContractCustNo);
                            DataTable dt1 = obj.PopulateDataSet("SELECT RMID FROM Op_ContractPropertyFee WHERE RefRP='" + bc.Entity.RowPointer + "' GROUP BY RMID").Tables[0];
                            foreach (DataRow dr in dt1.Rows)
                            {
                                SycnResourceStatus rs = new SycnResourceStatus();
                                rs.SysID = 1; //1.订单
                                rs.ResourceID = dr["RMID"].ToString();
                                rs.BusinessID = bc.Entity.RowPointer;
                                rs.BusinessNo = bc.Entity.ContractNo;
                                rs.BusinessType = 2;//1租赁，2物业
                                rs.RentBeginTime = bc.Entity.FeeStartDate;
                                rs.RentEndTime = bc.Entity.ContractEndDate;
                                rs.CustLongName = bc.Entity.ContractCustName;
                                rs.CustShortName = cust.Entity.CustShortName;
                                rs.CustTel = cust.Entity.CustTel;
                                rs.Status = 2;
                                rs.RentType = 1;
                                rs.UpdateTime = GetDate();
                                rs.UpdateUser = user.Entity.UserName;
                                Items += (Items == "" ? "" : ",") + JsonConvert.SerializeObject(rs);
                            }
                            string syncResult = srv.LeaseIn("[" + Items + "]");
                            collection.Add(new JsonStringValue("sync", syncResult));
                            */
                            #endregion
                        }
                    }
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("id", jp.getValue("id")));
            collection.Add(new JsonStringValue("type", "approve"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }
        private string invalidaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                if (bc.Entity.ContractStatus != "1" && bc.Entity.ContractStatus != "3")
                {
                    flag = "3";
                }
                else
                {
                    if (bc.Entity.ContractStatus == "1")
                    {
                        bc.Entity.ContractStatus = "3";
                        bc.Entity.ContractAuditor = user.Entity.UserName;
                        bc.Entity.ContractAuditDate = GetDate();
                    }
                    else
                    {
                        bc.Entity.ContractStatus = "1";
                        bc.Entity.ContractAuditor = user.Entity.UserName;
                        bc.Entity.ContractAuditDate = GetDate();
                    }
                    bc.invalid();
                    collection.Add(new JsonStringValue("status", bc.Entity.ContractStatus));
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("id", jp.getValue("id")));
            collection.Add(new JsonStringValue("type", "invalid"));
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
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("ContractStatusS"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
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
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("ContractStatusS"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }


        #region Item4 管理费、空调费
        private string itemsave4action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContractPropertyFee bc = new project.Business.Op.BusinessContractPropertyFee();
                if (jp.getValue("itemtp") == "update")
                {
                    bc.load(jp.getValue("itemid"));
                    bc.Entity.LastReviseDate = GetDate();
                    bc.Entity.LastReviser = user.Entity.UserName;
                }
                else
                {
                    bc.Entity.RefRP = jp.getValue("id");
                    bc.Entity.LastReviseDate = GetDate();
                    bc.Entity.LastReviser = user.Entity.UserName;
                    bc.Entity.CreateDate = DateTime.Now;
                    bc.Entity.Creator = user.Entity.UserName;
                }

                bc.Entity.SRVNo = jp.getValue("SRVNo");
                bc.Entity.RMID = jp.getValue("RMID");
                bc.Entity.RMArea = ParseDecimalForString(jp.getValue("RMArea"));
                bc.Entity.UnitPrice = ParseDecimalForString(jp.getValue("UnitPrice"));
                bc.Entity.Remark = jp.getValue("Remark"); ;

                int r = bc.Save();
                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemsave4"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList4(jp.getValue("id"), "update")));

            return collection.ToString();
        }
        private string itemupdate4action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractPropertyFee bc = new Business.Op.BusinessContractPropertyFee();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("SRVNo", bc.Entity.SRVNo));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("RMArea", bc.Entity.RMArea.ToString("0.####")));
                collection.Add(new JsonStringValue("UnitPrice", bc.Entity.UnitPrice.ToString("0.######")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));

                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate4"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string itemdel4action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractPropertyFee bc = new project.Business.Op.BusinessContractPropertyFee();
                bc.load(jp.getValue("itemid"));
                int r = bc.delete();

                if (r <= 0)
                    flag = "2";
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemdel4"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList4(jp.getValue("id"), "update")));
            result = collection.ToString();
            return result;
        }
        private string getrmid4action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessRoom bc = new Business.Base.BusinessRoom();
                bc.load(jp.getValue("rmid"));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("RMBuildSize", bc.Entity.RMRentSize.ToString("0.####")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getrmid4"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string getprice4action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                decimal UnitPrice = 0;
                DataTable dt = obj.PopulateDataSet("select top 1 DecimalValue from Sys_Setting where SRVNo='" + jp.getValue("SRVNo") + "'").Tables[0];
                if (dt.Rows.Count > 0)
                    UnitPrice = ParseDecimalForString(dt.Rows[0]["DecimalValue"].ToString());

                collection.Add(new JsonStringValue("UnitPrice", UnitPrice.ToString("0.####")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getprice4"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        #endregion
    }
}