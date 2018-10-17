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
    public partial class Contract_Bak : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/Contract.aspx");

                    if (!Page.IsCallback)
                    {
                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1);

                        itemlist1 = createItemList1(Guid.Empty.ToString(), "update");
                        itemlist2 = createItemList2(Guid.Empty.ToString(), "update");
                        itemlist3 = createItemList3(Guid.Empty.ToString(), "update");
                        itemlist4 = createItemList4(Guid.Empty.ToString(), "update");
                        itemlist5 = createItemList5(Guid.Empty.ToString(), "update");
                        itemlist6 = createItemList6(Guid.Empty.ToString(), "update");
                        itemlist7 = createItemList7(Guid.Empty.ToString(), "update");

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

                        SRVNo1Str = "<select class=\"input-text size-MINI\" id=\"SRVNo1\">";
                        SRVNo1Str += "<option value=\"\"></option>";
                        SRVNo2Str = "<select class=\"input-text size-MINI\" id=\"SRVNo2\">";
                        SRVNo2Str += "<option value=\"\"></option>";
                        SRVNo3Str = "<select class=\"input-text size-MINI\" id=\"SRVNo3\">";
                        SRVNo3Str += "<option value=\"\"></option>";
                        SRVNo4Str = "<select class=\"input-text size-MINI\" id=\"SRVNo4\">";
                        SRVNo4Str += "<option value=\"\"></option>";
                        SRVNo5Str = "<select class=\"input-text size-MINI\" id=\"SRVNo5\">";
                        SRVNo5Str += "<option value=\"\"></option>";
                        Business.Base.BusinessService bc2 = new project.Business.Base.BusinessService();
                        foreach (Entity.Base.EntityService it in bc2.GetListQuery(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty))
                        {
                            SRVNo1Str += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                            SRVNo2Str += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                            SRVNo3Str += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                            SRVNo4Str += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                            SRVNo5Str += "<option value='" + it.SRVNo + "'>" + it.SRVName + "</option>";
                        }
                        SRVNo1Str += "</select>";
                        SRVNo2Str += "</select>";
                        SRVNo3Str += "</select>";
                        SRVNo4Str += "</select>";
                        SRVNo5Str += "</select>";
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
        protected string itemlist1 = "";
        protected string itemlist2 = "";
        protected string itemlist3 = "";
        protected string itemlist4 = "";
        protected string itemlist5 = "";
        protected string itemlist6 = "";
        protected string itemlist7 = "";
        protected string date = "";
        protected string userName = "";
        protected string ContractTypeStr = "";
        protected string ContractTypeStrS = "";
        protected string ContractSPNoStr = "";
        protected string ContractSPNoStrS = "";
        protected string SRVNo1Str = "";
        protected string SRVNo2Str = "";
        protected string SRVNo3Str = "";
        protected string SRVNo4Str = "";
        protected string SRVNo5Str = "";
        
        private string createList(string ContractNo, string ContractNoManual, string ContractType, string ContractSPNo, string ContractCustNo,
            string MinContractSignedDate, string MaxContractSignedDate, string MinContractEndDate, string MaxContractEndDate, string OffLeaseStatus,
            string MinOffLeaseActulDate, string MaxOffLeaseActulDate, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"4%\">序号</th>");
            sb.Append("<th width='8%'>合同编号</th>");
            sb.Append("<th width='6%'>合同类别</th>");
            sb.Append("<th width='10%'>手工合同编号</th>");
            sb.Append("<th width='10%'>客户名称</th>");
            sb.Append("<th width='8%'>合同签订日期</th>");
            sb.Append("<th width='8%'>合同生效日期</th>");
            sb.Append("<th width='8%'>合同到期日期</th>");
            sb.Append("<th width='6%'>合同状态</th>");
            sb.Append("<th width='6%'>退租处理状态</th>");
            sb.Append("<th width='8%'>预约退租日期</th>");
            sb.Append("<th width='8%'>实际退租日期</th>");
            sb.Append("<th width='10%'>退租原因</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");								

            DateTime MinContractSignedDateS=default(DateTime);
            DateTime MaxContractSignedDateS=default(DateTime);
            if (MinContractSignedDate != "") MinContractSignedDateS = ParseDateForString(MinContractSignedDate);
            if (MaxContractSignedDate != "") MaxContractSignedDateS = ParseDateForString(MaxContractSignedDate);
            
            DateTime MinContractEndDateS = default(DateTime);
            DateTime MaxContractEndDateS = default(DateTime);
            if (MinContractEndDate != "") MinContractEndDateS = ParseDateForString(MinContractEndDate);
            if (MaxContractEndDate != "") MaxContractEndDateS = ParseDateForString(MaxContractEndDate);
            
            DateTime MinOffLeaseActulDateS = default(DateTime);
            DateTime MaxOffLeaseActulDateS = default(DateTime);
            if (MinOffLeaseActulDate != "") MinOffLeaseActulDateS = ParseDateForString(MinOffLeaseActulDate);
            if (MaxOffLeaseActulDate != "") MaxOffLeaseActulDateS = ParseDateForString(MaxOffLeaseActulDate);

            int r = 1;
            sb.Append("<tbody>");
            Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
            foreach (Entity.Op.EntityContract it in bc.GetListQuery(ContractNo, ContractNoManual, ContractType, ContractSPNo, ContractCustNo,
                            MinContractSignedDateS, MaxContractSignedDateS, MinContractEndDateS, MaxContractEndDateS, string.Empty, OffLeaseStatus,
                            MinOffLeaseActulDateS, MaxOffLeaseActulDateS, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.ContractNo + "</td>");
                sb.Append("<td>" + it.ContractTypeName + "</td>");
                sb.Append("<td>" + it.ContractNoManual + "</td>");
                sb.Append("<td>" + it.ContractCustName + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.ContractSignedDate) + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.ContractStartDate) + "</td>");
                sb.Append("<td>" + ParseStringForDate(it.ContractEndDate) + "</td>");
                sb.Append("<td><label style=\"color:" + (it.ContractStatus == "1" ? "red" : "blue") + ";\">" + it.ContractStatusName + "</span></td>");
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
                            MinContractSignedDateS, MaxContractSignedDateS, MinContractEndDateS, MaxContractEndDateS, string.Empty, OffLeaseStatus,
                            MinOffLeaseActulDateS, MaxOffLeaseActulDateS), pageSize, page, 7));

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

        //工位信息
        private string createItemList2(string RefRP, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\" style=\"width:1040px; table-layout:fixed; margin:0px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th style=\"width:4%\">序号</th>");
            sb.Append("<th style=\"width:15%\">房屋编号</th>");
            sb.Append("<th style=\"width:17%\">工位编号</th>");
            sb.Append("<th style=\"width:9%\">工位数(个)</th>");
            sb.Append("<th style=\"width:11%\">单价(元/工位/月)</th>");
            sb.Append("<th style=\"width:14%\">所属费用项目</th>");
            sb.Append("<th style=\"width:20%\">位置</th>");
            sb.Append("<th style=\"width:10%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            sb.Append("<tbody id=\"ItemBody2\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessContractWPRentalDetail bc = new project.Business.Op.BusinessContractWPRentalDetail();
                foreach (Entity.Op.EntityContractWPRentalDetail it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.RMID + "</td>");
                    sb.Append("<td>" + it.WPNo + "</td>");
                    sb.Append("<td>" + it.WPQTY.ToString() + "</td>");
                    sb.Append("<td>" + it.WPRentalUnitPrice.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.SRVName + "</td>");
                    sb.Append("<td>" + it.RMLoc + "</td>");
                    if (type != "view")
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate2('" + it.RowPointer + "')\" value=\"修改\" />&nbsp;<input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"itemdel2('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                    else
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate2('" + it.RowPointer + "')\" value=\"查看\" /></td>");
                    sb.Append("</tr>");
                    r++;
                }
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        //广告位信息
        private string createItemList3(string RefRP, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\" style=\"width:1040px; table-layout:fixed; margin:0px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th style=\"width:4%\">序号</th>");
            sb.Append("<th style=\"width:12%\">广告位编号</th>");
            sb.Append("<th style=\"width:16%\">广告位位置</th>");
            sb.Append("<th style=\"width:9%\">规格</th>");
            sb.Append("<th style=\"width:10%\">开始投放日期</th>");
            sb.Append("<th style=\"width:10%\">结束投放日期</th>");
            sb.Append("<th style=\"width:9%\">租赁时间(月)</th>");
            sb.Append("<th style=\"width:10%\">单价(元/月)</th>");
            sb.Append("<th style=\"width:9%\">金额(元)</th>");
            sb.Append("<th style=\"width:10%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;

            sb.Append("<tbody id=\"ItemBody3\">");

            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessContractBBRentalDetail bc = new project.Business.Op.BusinessContractBBRentalDetail();
                foreach (Entity.Op.EntityContractBBRentalDetail it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.BBNo + "</td>");
                    sb.Append("<td>" + it.BBAddr + "</td>");
                    sb.Append("<td>" + it.BBSize + "</td>");
                    sb.Append("<td>" + ParseStringForDate(it.BBStartDate) + "</td>");
                    sb.Append("<td>" + ParseStringForDate(it.BBEndDate) + "</td>");
                    sb.Append("<td>" + it.BBRentalMonths.ToString() + "</td>");
                    sb.Append("<td>" + it.RentalUnitPrice.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.RentalAmount.ToString("0.####") + "</td>");
                    if (type != "view")
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate3('" + it.RowPointer + "')\" value=\"修改\" />&nbsp;<input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"itemdel3('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                    else
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate3('" + it.RowPointer + "')\" value=\"查看\" /></td>");
                    sb.Append("</tr>");
                    r++;
                }
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        //水表信息
        private string createItemList4(string RefRP, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\" style=\"width:1040px; table-layout:fixed; margin:0px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th style=\"width:5%\">序号</th>");
            sb.Append("<th style=\"width:20%\">房屋编号</th>");
            sb.Append("<th style=\"width:15%\">水表编号</th>");
            sb.Append("<th style=\"width:15%\">水表起始数</th>");
            sb.Append("<th style=\"width:35%\">备注</th>");
            sb.Append("<th style=\"width:10%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            sb.Append("<tbody id=\"ItemBody4\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessContractWMDetail bc = new project.Business.Op.BusinessContractWMDetail();
                foreach (Entity.Op.EntityContractWMDetail it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.RMID + "</td>");
                    sb.Append("<td>" + it.WMMeterNo + "</td>");
                    sb.Append("<td>" + it.WMStartReadout.ToString("0.####") + "</td>");
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

        //电表信息
        private string createItemList5(string RefRP, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\" style=\"width:1040px; table-layout:fixed; margin:0px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th style=\"width:5%\">序号</th>");
            sb.Append("<th style=\"width:20%\">房屋编号</th>");
            sb.Append("<th style=\"width:15%\">电表骗号</th>");
            sb.Append("<th style=\"width:15%\">电表起始数</th>");
            sb.Append("<th style=\"width:35%\">备注</th>");
            sb.Append("<th style=\"width:10%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            sb.Append("<tbody id=\"ItemBody5\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessContractAMDetail bc = new project.Business.Op.BusinessContractAMDetail();
                foreach (Entity.Op.EntityContractAMDetail it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.RMID + "</td>");
                    sb.Append("<td>" + it.AMMeterNo + "</td>");
                    sb.Append("<td>" + it.AMStartReadout.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.Remark + "</td>");
                    if (type != "view")
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate5('" + it.RowPointer + "')\" value=\"修改\" />&nbsp;<input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"itemdel5('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                    else
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate5('" + it.RowPointer + "')\" value=\"查看\" /></td>");
                    sb.Append("</tr>");
                    r++;
                }
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        //管理费信息
        private string createItemList6(string RefRP, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\" style=\"width:1040px; table-layout:fixed; margin:0px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th style=\"width:5%\">序号</th>");
            sb.Append("<th style=\"width:20%\">房屋编号</th>");
            sb.Append("<th style=\"width:15%\">出租面积</th>");
            sb.Append("<th style=\"width:15%\">单价(元/㎡/月)</th>");
            sb.Append("<th style=\"width:35%\">备注</th>");
            sb.Append("<th style=\"width:10%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            sb.Append("<tbody id=\"ItemBody6\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessContractPropertyFee bc = new project.Business.Op.BusinessContractPropertyFee();
                foreach (Entity.Op.EntityContractPropertyFee it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.RMID + "</td>");
                    sb.Append("<td>" + it.RMArea.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.UnitPrice.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.Remark + "</td>");
                    if (type != "view")
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate6('" + it.RowPointer + "')\" value=\"修改\" />&nbsp;<input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"itemdel6('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                    else
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate6('" + it.RowPointer + "')\" value=\"查看\" /></td>");
                    sb.Append("</tr>");
                    r++;
                }
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        //空调费信息
        private string createItemList7(string RefRP, string type)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort mt-5\" style=\"width:1040px; table-layout:fixed; margin:0px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th style=\"width:5%\">序号</th>");
            sb.Append("<th style=\"width:20%\">房屋编号</th>");
            sb.Append("<th style=\"width:15%\">出租面积</th>");
            sb.Append("<th style=\"width:15%\">单价(元/㎡/月)</th>");
            sb.Append("<th style=\"width:35%\">备注</th>");
            sb.Append("<th style=\"width:10%\">操作</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            sb.Append("<tbody id=\"ItemBody7\">");
            if (RefRP != Guid.Empty.ToString())
            {
                Business.Op.BusinessContractAirConditionFee bc = new project.Business.Op.BusinessContractAirConditionFee();
                foreach (Entity.Op.EntityContractAirConditionFee it in bc.GetListQuery(RefRP))
                {
                    sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                    sb.Append("<td align='center'>" + r.ToString() + "</td>");
                    sb.Append("<td>" + it.RMID + "</td>");
                    sb.Append("<td>" + it.RMArea.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.UnitPrice.ToString("0.####") + "</td>");
                    sb.Append("<td>" + it.Remark + "</td>");
                    if (type != "view")
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate7('" + it.RowPointer + "')\" value=\"修改\" />&nbsp;<input class=\"btn btn-danger radius size-S\" type=\"button\" onclick=\"itemdel7('" + it.RowPointer + "')\" value=\"删除\" /></td>");
                    else
                        sb.Append("<td><input class=\"btn btn-primary radius size-S\" type=\"button\" onclick=\"itemupdate7('" + it.RowPointer + "')\" value=\"查看\" /></td>");
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
            else if (jp.getValue("Type") == "approve")
                result = approveaction(jp);
            else if (jp.getValue("Type") == "invalid")
                result = invalidaction(jp);
            else if (jp.getValue("Type") == "view")
                result = viewaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
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
            else if (jp.getValue("Type") == "getrmid")
                result = getrmidaction(jp);

            else if (jp.getValue("Type") == "itemsave2")
                result = itemsave2action(jp);
            else if (jp.getValue("Type") == "itemupdate2")
                result = itemupdate2action(jp);
            else if (jp.getValue("Type") == "itemdel2")
                result = itemdel2action(jp);
            else if (jp.getValue("Type") == "getwpno")
                result = getwpnoaction(jp);

            else if (jp.getValue("Type") == "itemsave3")
                result = itemsave3action(jp);
            else if (jp.getValue("Type") == "itemupdate3")
                result = itemupdate3action(jp);
            else if (jp.getValue("Type") == "itemdel3")
                result = itemdel3action(jp);
            else if (jp.getValue("Type") == "getbbno")
                result = getbbnoaction(jp);

            else if (jp.getValue("Type") == "itemsave4")
                result = itemsave4action(jp);
            else if (jp.getValue("Type") == "itemupdate4")
                result = itemupdate4action(jp);
            else if (jp.getValue("Type") == "itemdel4")
                result = itemdel4action(jp);
            else if (jp.getValue("Type") == "getwmmeterno")
                result = getwmmeternoaction(jp);

            else if (jp.getValue("Type") == "itemsave5")
                result = itemsave5action(jp);
            else if (jp.getValue("Type") == "itemupdate5")
                result = itemupdate5action(jp);
            else if (jp.getValue("Type") == "itemdel5")
                result = itemdel5action(jp);
            else if (jp.getValue("Type") == "getammeterno")
                result = getammeternoaction(jp);

            else if (jp.getValue("Type") == "itemsave6")
                result = itemsave6action(jp);
            else if (jp.getValue("Type") == "itemupdate6")
                result = itemupdate6action(jp);
            else if (jp.getValue("Type") == "itemdel6")
                result = itemdel6action(jp);
            else if (jp.getValue("Type") == "getpropertyfee")
                result = getpropertyfeeaction(jp);

            else if (jp.getValue("Type") == "itemsave7")
                result = itemsave7action(jp);
            else if (jp.getValue("Type") == "itemupdate7")
                result = itemupdate7action(jp);
            else if (jp.getValue("Type") == "itemdel7")
                result = itemdel7action(jp);
            else if (jp.getValue("Type") == "getairconditionfee")
                result = getairconditionfeeaction(jp);
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
                collection.Add(new JsonStringValue("itemlist1", itemlist1));
                collection.Add(new JsonStringValue("itemlist2", itemlist2));
                collection.Add(new JsonStringValue("itemlist3", itemlist3));
                collection.Add(new JsonStringValue("itemlist4", itemlist4));
                collection.Add(new JsonStringValue("itemlist5", itemlist5));
                collection.Add(new JsonStringValue("itemlist6", itemlist6));
                collection.Add(new JsonStringValue("itemlist7", itemlist7));
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

                    collection.Add(new JsonStringValue("itemlist1", createItemList1(bc.Entity.RowPointer, "update")));
                    collection.Add(new JsonStringValue("itemlist2", createItemList2(bc.Entity.RowPointer, "update")));
                    collection.Add(new JsonStringValue("itemlist3", createItemList3(bc.Entity.RowPointer, "update")));
                    collection.Add(new JsonStringValue("itemlist4", createItemList4(bc.Entity.RowPointer, "update")));
                    collection.Add(new JsonStringValue("itemlist5", createItemList5(bc.Entity.RowPointer, "update")));
                    collection.Add(new JsonStringValue("itemlist6", createItemList6(bc.Entity.RowPointer, "update")));
                    collection.Add(new JsonStringValue("itemlist7", createItemList7(bc.Entity.RowPointer, "update")));
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
                collection.Add(new JsonStringValue("itemlist2", createItemList2(bc.Entity.RowPointer, "view")));
                collection.Add(new JsonStringValue("itemlist3", createItemList3(bc.Entity.RowPointer, "view")));
                collection.Add(new JsonStringValue("itemlist4", createItemList4(bc.Entity.RowPointer, "view")));
                collection.Add(new JsonStringValue("itemlist5", createItemList5(bc.Entity.RowPointer, "view")));
                collection.Add(new JsonStringValue("itemlist6", createItemList6(bc.Entity.RowPointer, "view")));
                collection.Add(new JsonStringValue("itemlist7", createItemList7(bc.Entity.RowPointer, "view")));
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
                Business.Op.BusinessContract bc = new project.Business.Op.BusinessContract();
                bc.load(jp.getValue("id"));
                if (bc.Entity.ContractStatus == "2")
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
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
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

                    bc.Entity.ContractCreator = user.Entity.UserName;
                    bc.Entity.ContractCreateDate = GetDate();
                    bc.Entity.ContractLastReviser = user.Entity.UserName;
                    bc.Entity.ContractLastReviseDate = GetDate();
                    bc.Entity.ContractStatus = "1";
                    int r = bc.Save("insert");
                    if (r <= 0)
                        flag = "2";

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
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
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
                        bc.Entity.ContractAuditor = user.Entity.UserName;
                        bc.Entity.ContractAuditDate = GetDate();
                        bc.invalid();
                        collection.Add(new JsonStringValue("status", bc.Entity.ContractStatus));
                        try 
                        {
                            obj.ExecuteNonQuery("update Mstr_Room set RMCurrentCustNo='" + bc.Entity.ContractCustNo + "' where RMID in (select RMID from Op_ContractRMRentalDetail where RefRP='"+bc.Entity.RowPointer+"')");
                        }
                        catch { }
                    }
                    else
                    {
                        if (int.Parse(obj.PopulateDataSet("select Count(1) as Cnt from Op_OrderDetail where ContractNo='" + bc.Entity.ContractNo + "'").Tables[0].Rows[0]["Cnt"].ToString()) > 0)
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
                            try
                            {
                                obj.ExecuteNonQuery("update Mstr_Room set RMCurrentCustNo='' where RMID in (select RMID from Op_ContractRMRentalDetail where RefRP='" + bc.Entity.RowPointer + "')");
                            }
                            catch { }
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
                jp.getValue("MinContractEndDate"), jp.getValue("MaxContractEndDate"), jp.getValue("OffLeaseStatusS"), jp.getValue("MinOffLeaseActulDate"),
                jp.getValue("MaxOffLeaseActulDate"), ParseIntForString(jp.getValue("page"))))); 
            return collection.ToString();
        }


        #region Item1 房屋信息
        private string itemsave1action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContractRMRentalDetail bc = new project.Business.Op.BusinessContractRMRentalDetail();
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

                bc.Entity.RMID = jp.getValue("RMID");
                bc.Entity.SRVNo = jp.getValue("SRVNo");
                bc.Entity.RMLoc = jp.getValue("RMLoc");
                bc.Entity.RMArea = ParseDecimalForString(jp.getValue("RMArea"));
                bc.Entity.RentalUnitPrice = ParseDecimalForString(jp.getValue("RentalUnitPrice"));
                bc.Entity.Remark = jp.getValue("Remark");
                int r = bc.Save();
                if (r <= 0)
                    flag = "2";
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
        private string itemdel1action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractRMRentalDetail bc = new project.Business.Op.BusinessContractRMRentalDetail();
                bc.load(jp.getValue("itemid"));
                int r = bc.delete();

                if (r <= 0)
                    flag = "2";
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemdel1"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList1(jp.getValue("id"), "update")));
            result = collection.ToString();
            return result;
        }
        private string getrmidaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessRoom bc = new Business.Base.BusinessRoom();
                bc.load(jp.getValue("rmid"));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("RMBuildSize", bc.Entity.RMBuildSize.ToString("0.####")));
                collection.Add(new JsonStringValue("RMAddr", bc.Entity.RMAddr));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getrmid"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        #endregion


        #region Item2 工位信息
        private string itemsave2action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContractWPRentalDetail bc = new project.Business.Op.BusinessContractWPRentalDetail();
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

                bc.Entity.RMID = jp.getValue("RMID");
                bc.Entity.WPNo = jp.getValue("WPNo");
                bc.Entity.SRVNo = jp.getValue("SRVNo");
                bc.Entity.RMLoc = jp.getValue("RMLoc");
                bc.Entity.WPQTY = ParseIntForString(jp.getValue("WPQTY"));
                bc.Entity.WPRentalUnitPrice = ParseDecimalForString(jp.getValue("WPRentalUnitPrice"));
                bc.Entity.Remark = jp.getValue("Remark");
                int r = bc.Save();
                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemsave2"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList2(jp.getValue("id"), "update")));
            return collection.ToString();
        }
        private string itemupdate2action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractWPRentalDetail bc = new Business.Op.BusinessContractWPRentalDetail();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("WPNo", bc.Entity.WPNo));
                collection.Add(new JsonStringValue("SRVNo", bc.Entity.SRVNo));
                collection.Add(new JsonStringValue("RMLoc", bc.Entity.RMLoc));
                collection.Add(new JsonStringValue("WPQTY", bc.Entity.WPQTY.ToString("")));
                collection.Add(new JsonStringValue("WPRentalUnitPrice", bc.Entity.WPRentalUnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));

                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate2"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string itemdel2action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractWPRentalDetail bc = new project.Business.Op.BusinessContractWPRentalDetail();
                bc.load(jp.getValue("itemid"));
                int r = bc.delete();
                if (r <= 0)
                    flag = "2";
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemdel2"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList2(jp.getValue("id"), "update")));
            result = collection.ToString();
            return result;
        }
        private string getwpnoaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessWorkPlace bc = new project.Business.Base.BusinessWorkPlace();
                bc.load(jp.getValue("wpid"));
                collection.Add(new JsonStringValue("WPNo", bc.Entity.WPNo));
                collection.Add(new JsonStringValue("WPSeat", bc.Entity.WPSeat.ToString()));
                collection.Add(new JsonStringValue("WPSeatPrice", bc.Entity.WPSeatPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("WPAddr", bc.Entity.WPAddr));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getwpno"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        #endregion


        #region Item3 广告位信息
        private string itemsave3action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContractBBRentalDetail bc = new project.Business.Op.BusinessContractBBRentalDetail();
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
                bc.Entity.BBNo = jp.getValue("BBNo");
                bc.Entity.BBName = jp.getValue("BBName");
                bc.Entity.BBSize = jp.getValue("BBSize");
                bc.Entity.BBAddr = jp.getValue("BBAddr");
                bc.Entity.BBStartDate = ParseDateForString(jp.getValue("BBStartDate"));
                bc.Entity.BBEndDate = ParseDateForString(jp.getValue("BBEndDate"));
                bc.Entity.BBRentalMonths = ParseIntForString(jp.getValue("BBRentalMonths"));
                bc.Entity.RentalUnitPrice = ParseDecimalForString(jp.getValue("RentalUnitPrice"));
                bc.Entity.RentalAmount = ParseDecimalForString(jp.getValue("RentalUnitPrice")) * ParseIntForString(jp.getValue("BBRentalMonths"));
                bc.Entity.Remark = jp.getValue("Remark");
                
                int r = bc.Save();
                if (r <= 0)
                    flag = "2";
                else
                {
                    obj.ExecuteNonQuery("update Op_Contract set BBQTY=(select Count(1) from Op_ContractBBRentalDetail where RefRP=Op_Contract.RowPointer)," +
                        "BBAmount=(select SUM(ISNULL(RentalAmount,0)) from Op_ContractBBRentalDetail where RefRP=Op_Contract.RowPointer)" +
                        " where RowPointer='" + bc.Entity.RefRP + "'");

                    DataTable dt = obj.PopulateDataSet("select BBQTY,BBAmount from Op_Contract where RowPointer='" + bc.Entity.RefRP + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        collection.Add(new JsonStringValue("BBQTY", ParseIntForString(dt.Rows[0]["BBQTY"].ToString()).ToString()));
                        collection.Add(new JsonStringValue("BBAmount", ParseDecimalForString(dt.Rows[0]["BBAmount"].ToString()).ToString("0.####")));
                    }
                    else
                    {
                        collection.Add(new JsonStringValue("BBQTY", "0"));
                        collection.Add(new JsonStringValue("BBAmount", "0"));
                    }
                }
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemsave3"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList3(jp.getValue("id"), "update")));

            return collection.ToString();
        }
        private string itemupdate3action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractBBRentalDetail bc = new Business.Op.BusinessContractBBRentalDetail();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("SRVNo", bc.Entity.SRVNo));
                collection.Add(new JsonStringValue("BBNo", bc.Entity.BBNo));
                collection.Add(new JsonStringValue("BBName", bc.Entity.BBName));
                collection.Add(new JsonStringValue("BBSize", bc.Entity.BBSize));
                collection.Add(new JsonStringValue("BBAddr", bc.Entity.BBAddr));
                collection.Add(new JsonStringValue("BBStartDate", ParseStringForDate(bc.Entity.BBStartDate)));
                collection.Add(new JsonStringValue("BBEndDate", ParseStringForDate(bc.Entity.BBEndDate)));
                collection.Add(new JsonStringValue("BBRentalMonths", bc.Entity.BBRentalMonths.ToString("")));
                collection.Add(new JsonStringValue("RentalUnitPrice", bc.Entity.RentalUnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("RentalAmount", bc.Entity.RentalAmount.ToString("0.####")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));
                
                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate3"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string itemdel3action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractBBRentalDetail bc = new project.Business.Op.BusinessContractBBRentalDetail();
                bc.load(jp.getValue("itemid"));
                int r = bc.delete();

                if (r <= 0)
                    flag = "2";
                else
                {
                    obj.ExecuteNonQuery("update Op_Contract set BBQTY=(select Count(1) from Op_ContractBBRentalDetail where RefRP=Op_Contract.RowPointer)," +
                        "BBAmount=(select SUM(ISNULL(RentalAmount,0)) from Op_ContractBBRentalDetail where RefRP=Op_Contract.RowPointer)" +
                        " where RowPointer='" + bc.Entity.RefRP + "'");

                    DataTable dt = obj.PopulateDataSet("select BBQTY,BBAmount from Op_Contract where RowPointer='" + bc.Entity.RefRP + "'").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        collection.Add(new JsonStringValue("BBQTY", ParseIntForString(dt.Rows[0]["BBQTY"].ToString()).ToString()));
                        collection.Add(new JsonStringValue("BBAmount", ParseDecimalForString(dt.Rows[0]["BBAmount"].ToString()).ToString("0.####")));
                    }
                    else
                    {
                        collection.Add(new JsonStringValue("BBQTY", "0"));
                        collection.Add(new JsonStringValue("BBAmount", "0"));
                    }
                }
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemdel3"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList3(jp.getValue("id"), "update")));
            result = collection.ToString();
            return result;
        }
        private string getbbnoaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessBillboard bc = new Business.Base.BusinessBillboard();
                bc.load(jp.getValue("bbid"));
                collection.Add(new JsonStringValue("BBNo", bc.Entity.BBNo));
                collection.Add(new JsonStringValue("BBName", bc.Entity.BBName));
                collection.Add(new JsonStringValue("BBSize", bc.Entity.BBSize));
                collection.Add(new JsonStringValue("BBAddr", bc.Entity.BBAddr));
                collection.Add(new JsonStringValue("BBINPrice", bc.Entity.BBINPriceMonth.ToString("0.####")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getbbno"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        #endregion

        
        #region Item4 水费信息
        private string itemsave4action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContractWMDetail bc = new project.Business.Op.BusinessContractWMDetail();
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

                bc.Entity.RMID = jp.getValue("RMID");
                bc.Entity.SRVNo = jp.getValue("SRVNo");
                bc.Entity.WMMeterNo = jp.getValue("WMMeterNo");
                bc.Entity.WMStartReadout = ParseDecimalForString(jp.getValue("WMStartReadout"));
                bc.Entity.WMMeterRate = ParseDecimalForString(jp.getValue("WMMeterRate"));
                bc.Entity.Remark = jp.getValue("Remark");
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
                Business.Op.BusinessContractWMDetail bc = new Business.Op.BusinessContractWMDetail();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("SRVNo", bc.Entity.SRVNo));
                collection.Add(new JsonStringValue("WMMeterNo", bc.Entity.WMMeterNo));
                collection.Add(new JsonStringValue("WMStartReadout", bc.Entity.WMStartReadout.ToString("0.####")));
                collection.Add(new JsonStringValue("WMMeterRate", bc.Entity.WMMeterRate.ToString("0.####")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));

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
                Business.Op.BusinessContractWMDetail bc = new project.Business.Op.BusinessContractWMDetail();
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
        private string getwmmeternoaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessMeter bc = new Business.Base.BusinessMeter();
                bc.load(jp.getValue("meterid"));
                collection.Add(new JsonStringValue("MeterReadout", bc.Entity.MeterReadout.ToString("0.####")));
                collection.Add(new JsonStringValue("MeterRate", bc.Entity.MeterRate.ToString("0.####")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getwmmeterno"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        #endregion


        #region Item5 电费信息
        private string itemsave5action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContractAMDetail bc = new project.Business.Op.BusinessContractAMDetail();
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

                bc.Entity.RMID = jp.getValue("RMID");
                bc.Entity.SRVNo = jp.getValue("SRVNo");
                bc.Entity.AMMeterNo = jp.getValue("AMMeterNo");
                bc.Entity.AMStartReadout = ParseDecimalForString(jp.getValue("AMStartReadout"));
                bc.Entity.AMMeterRate = ParseDecimalForString(jp.getValue("AMMeterRate"));
                bc.Entity.Remark = jp.getValue("Remark");
                int r = bc.Save();
                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemsave5"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList5(jp.getValue("id"), "update")));
            return collection.ToString();
        }
        private string itemupdate5action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractAMDetail bc = new Business.Op.BusinessContractAMDetail();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("SRVNo", bc.Entity.SRVNo));
                collection.Add(new JsonStringValue("AMMeterNo", bc.Entity.AMMeterNo));
                collection.Add(new JsonStringValue("AMStartReadout", bc.Entity.AMStartReadout.ToString("0.####")));
                collection.Add(new JsonStringValue("AMMeterRate", bc.Entity.AMMeterRate.ToString("0.####")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));

                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate5"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string itemdel5action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractAMDetail bc = new project.Business.Op.BusinessContractAMDetail();
                bc.load(jp.getValue("itemid"));
                int r = bc.delete();
                if (r <= 0)
                    flag = "2";
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemdel5"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList5(jp.getValue("id"), "update")));
            result = collection.ToString();
            return result;
        }
        private string getammeternoaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessMeter bc = new Business.Base.BusinessMeter();
                bc.load(jp.getValue("meterid"));
                collection.Add(new JsonStringValue("MeterReadout", bc.Entity.MeterReadout.ToString("0.####")));
                collection.Add(new JsonStringValue("MeterRate", bc.Entity.MeterRate.ToString("0.####")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getammeterno"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        #endregion


        #region Item6 管理费信息
        private string itemsave6action(JsonArrayParse jp)
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

                bc.Entity.RMID = jp.getValue("RMID");
                bc.Entity.RMArea = ParseDecimalForString(jp.getValue("RMArea"));
                bc.Entity.UnitPrice = ParseDecimalForString(jp.getValue("UnitPrice"));
                bc.Entity.Remark = jp.getValue("Remark");
                int r = bc.Save();
                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemsave6"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList6(jp.getValue("id"), "update")));
            return collection.ToString();
        }
        private string itemupdate6action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractPropertyFee bc = new Business.Op.BusinessContractPropertyFee();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("RMArea", bc.Entity.RMArea.ToString("0.####")));
                collection.Add(new JsonStringValue("UnitPrice", bc.Entity.UnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));

                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate6"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string itemdel6action(JsonArrayParse jp)
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

            collection.Add(new JsonStringValue("type", "itemdel6"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList6(jp.getValue("id"), "update")));
            result = collection.ToString();
            return result;
        }
        private string getpropertyfeeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessRoom bc = new Business.Base.BusinessRoom();
                bc.load(jp.getValue("rmid"));
                collection.Add(new JsonStringValue("RMBuildSize", bc.Entity.RMBuildSize.ToString("0.####")));

                Business.Base.BusinessSetting setting = new Business.Base.BusinessSetting();
                setting.load("PropertyFee");
                collection.Add(new JsonStringValue("PropertyFee", setting.Entity.DecimalValue.ToString("0.####")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getpropertyfee"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        #endregion


        #region Item7 空调费信息
        private string itemsave7action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Op.BusinessContractAirConditionFee bc = new project.Business.Op.BusinessContractAirConditionFee();
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

                bc.Entity.RMID = jp.getValue("RMID");
                bc.Entity.RMArea = ParseDecimalForString(jp.getValue("RMArea"));
                bc.Entity.UnitPrice = ParseDecimalForString(jp.getValue("UnitPrice"));
                bc.Entity.Remark = jp.getValue("Remark");
                int r = bc.Save();
                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemsave7"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList7(jp.getValue("id"), "update")));
            return collection.ToString();
        }
        private string itemupdate7action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractAirConditionFee bc = new Business.Op.BusinessContractAirConditionFee();
                bc.load(jp.getValue("itemid"));
                collection.Add(new JsonStringValue("RMID", bc.Entity.RMID));
                collection.Add(new JsonStringValue("RMArea", bc.Entity.RMArea.ToString("0.####")));
                collection.Add(new JsonStringValue("UnitPrice", bc.Entity.UnitPrice.ToString("0.####")));
                collection.Add(new JsonStringValue("Remark", bc.Entity.Remark));

                collection.Add(new JsonStringValue("ItemId", jp.getValue("itemid")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemupdate7"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        private string itemdel7action(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Op.BusinessContractAirConditionFee bc = new project.Business.Op.BusinessContractAirConditionFee();
                bc.load(jp.getValue("itemid"));
                int r = bc.delete();
                if (r <= 0)
                    flag = "2";
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "itemdel7"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createItemList7(jp.getValue("id"), "update")));
            result = collection.ToString();
            return result;
        }
        private string getairconditionfeeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string result = ""; ;
            try
            {
                Business.Base.BusinessRoom bc = new Business.Base.BusinessRoom();
                bc.load(jp.getValue("rmid"));
                collection.Add(new JsonStringValue("RMBuildSize", bc.Entity.RMBuildSize.ToString("0.####")));

                Business.Base.BusinessSetting setting = new Business.Base.BusinessSetting();
                setting.load("AirConditionFee");
                collection.Add(new JsonStringValue("AirConditionFee", setting.Entity.DecimalValue.ToString("0.####")));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getairconditionfee"));
            collection.Add(new JsonStringValue("flag", flag));
            result = collection.ToString();
            return result;
        }
        #endregion
    }
}