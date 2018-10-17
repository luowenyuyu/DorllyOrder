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
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text;

namespace project.Presentation.Op
{
    public partial class GetTenementRT : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/GetTenementRT.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/GetTenementRT.aspx'";
                            DataTable dt = obj.PopulateDataSet(sqlstr).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                string rightCode = dt.Rows[0]["RightCode"].ToString();
                                if (rightCode.IndexOf("excel") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"excel()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 导出</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"excel()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 导出</a>&nbsp;&nbsp;";
                        }
                        bindCSSelect();
                        bindCTSelect();
                        bindOSSelect();
                        bindSPSelect();
                        list = createList(_signStartDate, _signEndDate, "", "", "", "", "", "", "", "", "");
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
        protected string SPSelect = string.Empty;//服务商
        protected string CSSelect = string.Empty;//合同状态
        protected string CTSelect = string.Empty;//合同类型
        protected string OSSelect = string.Empty;//退租状态
        private string _signStartDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
        private string _signEndDate = DateTime.Now.ToString("yyyy-MM-dd");

        #region 异步刷新代码
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
            else if (jp.getValue("Type") == "excel")
                result = excelaction(jp);
            else if (jp.getValue("Type") == "getdate")
                result = pageGetDate(jp);
            return result;
        }

        #endregion

        private string pageGetDate(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            collection.Add(new JsonStringValue("type", "getdate"));
            collection.Add(new JsonStringValue("flag", "1"));
            collection.Add(new JsonStringValue("start", _signStartDate));
            collection.Add(new JsonStringValue("end", _signEndDate));
            return collection.ToString();
        }


        #region 下拉选择框数据绑定


        /// <summary>
        /// 绑定“服务商”数据
        /// </summary>
        private void bindSPSelect()
        {
            StringBuilder sb = new StringBuilder("<select class=\"input-text size-MINI\" style=\"width:110px\" id=\"SPNo\">");
            sb.AppendLine("<option value=\"\" select>全部</option>");
            string sql = "select SPNo,SPShortName from Mstr_ServiceProvider";
            Data data = new Data();
            try
            {
                DataTable dt = data.PopulateDataSet(sql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<option value=\"");
                    sb.Append(dt.Rows[i]["SPNo"].ToString() + "\">");
                    sb.Append(dt.Rows[i]["SPShortName"].ToString());
                    sb.AppendLine("</option>");

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            sb.AppendLine("</select>");
            SPSelect = sb.ToString();
        }


        /// <summary>
        /// 绑定“合同类型”数据
        /// </summary>
        private void bindCTSelect()
        {
            StringBuilder sb = new StringBuilder("<select class=\"input-text size-MINI\" style=\"width:110px\" id=\"ContractTypeNo\">");
            sb.AppendLine("<option value=\"\" select>全部</option>");
            string sql = "select ContractTypeNo,ContractTypeName from Mstr_ContractType";
            Data data = new Data();
            try
            {
                DataTable dt = data.PopulateDataSet(sql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<option value=\"");
                    sb.Append(dt.Rows[i]["ContractTypeNo"].ToString() + "\">");
                    sb.Append(dt.Rows[i]["ContractTypeName"].ToString());
                    sb.AppendLine("</option>");

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            sb.AppendLine("</select>");
            CTSelect = sb.ToString();
        }


        /// <summary>
        /// 绑定“合同状态”数据
        /// </summary>
        private void bindCSSelect()
        {
            StringBuilder sb = new StringBuilder("<select class=\"input-text size-MINI\" style=\"width:110px\" id=\"ContractStatus\">");
            sb.AppendLine("<option value=\"\" select>全部</option>");
            sb.AppendLine("<option value=\"1\" select>制单</option>");
            sb.AppendLine("<option value=\"2\" select>已审核</option>");
            sb.AppendLine("<option value=\"3\" select>已退租</option>");
            sb.AppendLine("<option value=\"4\" select>已作废</option>");
            sb.AppendLine("</select>");
            CSSelect = sb.ToString();
        }



        /// <summary>
        /// 绑定“退租状态”数据
        /// </summary>
        private void bindOSSelect()
        {
            StringBuilder sb = new StringBuilder("<select class=\"input-text size-MINI\" style=\"width:110px\" id=\"OffLeaseStatus\">");
            sb.AppendLine("<option value=\"\" select>全部</option>");
            sb.AppendLine("<option value=\"1\" select>未退租</option>");
            sb.AppendLine("<option value=\"2\" select>已申请</option>");
            sb.AppendLine("<option value=\"3\" select>已办理</option>");
            sb.AppendLine("<option value=\"4\" select>已结算</option>");
            sb.AppendLine("<option value=\"-1\" select>审核不通过</option>");
            sb.AppendLine("</select>");
            OSSelect = sb.ToString();
        }

        #endregion

        //<!--SignStartDate SignEndDate ExpireStartDate ExpireEndDate RealStartDate  RealEndData  ServiceProvider ContractTypeNo ContractStatus OffLeaseStatus -->

        private string createList(string SignStartDate, string SignEndDate, string ExpireStartDate, string ExpireEndDate, string RealStartDate, string RealEndDate,
            string SPNo, string CustName, string ContractTypeNo, string ContractStatus, string OffLeaseStatus)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\" style =\"width:3800px;\" >");//style =\"width:2200px;\"
            sb.Append("<thead>");
            #region 百分比
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"1%\">序号</th>");
            sb.Append("<th width='2%'>合同编号</th>");
            sb.Append("<th width='4%'>资源编号</th>");
            sb.Append("<th width='2%'>面积(\\㎡)</th>");
            sb.Append("<th width='2%'>服务商</th>");
            sb.Append("<th width='5%'>客户名称</th>");
            sb.Append("<th width='2%'>合同类型</th>");
            sb.Append("<th width='2%'>费用类型</th>");
            sb.Append("<th width='2%'>合同状态</th>");
            sb.Append("<th width='2%'>退租状态</th>");
            sb.Append("<th width='2%'>合同签订日期</th>");
            sb.Append("<th width='2%'>合同生效日期</th>");
            sb.Append("<th width='2%'>合同到期日期</th>");
            sb.Append("<th width='2%'>租金起收日期</th>");
            sb.Append("<th width='2%'>实际退租日期</th>");
            sb.Append("<th width='2%'>合同审核日期</th>");
            sb.Append("<th width='2%'>审核人员</th>");
            sb.Append("<th width='2%'>减免开始日期1</th>");
            sb.Append("<th width='2%'>减免结束日期1</th>");
            sb.Append("<th width='2%'>减免开始日期2</th>");
            sb.Append("<th width='2%'>减免结束日期2</th>");
            sb.Append("<th width='2%'>减免开始日期3</th>");
            sb.Append("<th width='2%'>减免结束日期3</th>");
            sb.Append("<th width='2%'>减免开始日期4</th>");
            sb.Append("<th width='2%'>减免结束日期4</th>");
            sb.Append("<th width='2%'>是否固定金额</th>");
            sb.Append("<th width='2%'>递增方式</th>");
            sb.Append("<th width='2%'>固定金额</th>");
            sb.Append("<th width='1%'>递增值1</th>");
            sb.Append("<th width='2%'>递增开始时间1</th>");
            sb.Append("<th width='1%'>递增值2</th>");
            sb.Append("<th width='2%'>递增开始时间2</th>");
            sb.Append("<th width='1%'>递增值3</th>");
            sb.Append("<th width='2%'>递增开始时间3</th>");
            sb.Append("<th width='1%'>递增值4</th>");
            sb.Append("<th width='2%'>递增开始时间4</th>");
            #endregion
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            DataTable dt = GetTenementProc(SignStartDate, SignEndDate, ExpireStartDate, ExpireEndDate, RealStartDate, RealEndDate, SPNo, CustName, ContractTypeNo, ContractStatus, OffLeaseStatus);
            sb.Append("<tbody>");
            foreach (DataRow it in dt.Rows)
            {

                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it["ContractNo"].ToString() + "</td>");
                sb.Append("<td>" + it["RMID"].ToString() + "</td>");
                sb.Append("<td>" + (it["RMArea"] == DBNull.Value ? "" : ParseDecimalForString(it["RMArea"].ToString()).ToString("0.##")) + "</td>");
                sb.Append("<td>" + it["SPShortName"].ToString() + "</td>");
                sb.Append("<td>" + it["CustName"].ToString() + "</td>");

                sb.Append("<td>" + it["ContractTypeName"].ToString() + "</td>");
                sb.Append("<td>" + it["SRVName"].ToString() + "</td>");


                sb.Append("<td>" + it["ContractStatus"].ToString() + "</td>");
                sb.Append("<td>" + it["OffLeaseStatus"].ToString() + "</td>");

                sb.Append("<td>" + (it["ContractSignedDate"] == DBNull.Value ? "" : ParseDateForString(it["ContractSignedDate"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["ContractStartDate"] == DBNull.Value ? "" : ParseDateForString(it["ContractStartDate"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["contractenddate"] == DBNull.Value ? "" : ParseDateForString(it["contractenddate"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["feestartdate"] == DBNull.Value ? "" : ParseDateForString(it["feestartdate"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["OffLeaseActulDate"] == DBNull.Value ? "" : ParseDateForString(it["OffLeaseActulDate"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["contractauditdate"] == DBNull.Value ? "" : ParseDateForString(it["contractauditdate"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + it["contractauditor"].ToString() + "</td>");

                sb.Append("<td>" + (it["ReduceStartDate1"] == DBNull.Value ? "" : ParseDateForString(it["ReduceStartDate1"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["ReduceEndDate1"] == DBNull.Value ? "" : ParseDateForString(it["ReduceEndDate1"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["ReduceStartDate2"] == DBNull.Value ? "" : ParseDateForString(it["ReduceStartDate2"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["ReduceEndDate2"] == DBNull.Value ? "" : ParseDateForString(it["ReduceEndDate2"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["ReduceStartDate3"] == DBNull.Value ? "" : ParseDateForString(it["ReduceStartDate3"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["ReduceEndDate3"] == DBNull.Value ? "" : ParseDateForString(it["ReduceEndDate3"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["ReduceStartDate4"] == DBNull.Value ? "" : ParseDateForString(it["ReduceStartDate4"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["ReduceEndDate4"] == DBNull.Value ? "" : ParseDateForString(it["ReduceEndDate4"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["IsFixedAmt"] == DBNull.Value ? "" : it["IsFixedAmt"].ToString()) + "</td>");
                sb.Append("<td>" + (it["IncreaseType"] == DBNull.Value ? "" : it["IncreaseType"].ToString()) + "</td>");
                sb.Append("<td>" + (it["Amount"] == DBNull.Value ? "" : ParseDecimalForString(it["Amount"].ToString()).ToString("0.00")) + "</td>");

                sb.Append("<td>" + (it["IncreaseRate1"] == DBNull.Value ? "" : ParseDecimalForString(it["IncreaseRate1"].ToString()).ToString("0.00")) + "</td>");
                sb.Append("<td>" + (it["IncreaseStartDate1"] == DBNull.Value ? "" : ParseDateForString(it["IncreaseStartDate1"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["IncreaseRate2"] == DBNull.Value ? "" : ParseDecimalForString(it["IncreaseRate2"].ToString()).ToString("0.00")) + "</td>");
                sb.Append("<td>" + (it["IncreaseStartDate2"] == DBNull.Value ? "" : ParseDateForString(it["IncreaseStartDate2"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["IncreaseRate3"] == DBNull.Value ? "" : ParseDecimalForString(it["IncreaseRate3"].ToString()).ToString("0.00")) + "</td>");
                sb.Append("<td>" + (it["IncreaseStartDate3"] == DBNull.Value ? "" : ParseDateForString(it["IncreaseStartDate3"].ToString()).ToString("yyyy-MM-dd")) + "</td>");
                sb.Append("<td>" + (it["IncreaseRate4"] == DBNull.Value ? "" : ParseDecimalForString(it["IncreaseRate4"].ToString()).ToString("0.00")) + "</td>");
                sb.Append("<td>" + (it["IncreaseStartDate4"] == DBNull.Value ? "" : ParseDateForString(it["IncreaseStartDate4"].ToString()).ToString("yyyy-MM-dd")) + "</td>");


                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }



        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SignStartDate"), jp.getValue("SignEndDate"), jp.getValue("ExpireStartDate"),
                jp.getValue("ExpireEndDate"), jp.getValue("RealStartDate"), jp.getValue("RealEndDate"), jp.getValue("SPNo"), jp.getValue("CustName"),
                jp.getValue("ContractTypeNo"), jp.getValue("ContractStatus"), jp.getValue("OffLeaseStatus"))));
            return collection.ToString();
        }
        private string excelaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            try
            {
                pathName = "出租情况统计表" + GetDate().ToString("yyyy-MM-dd HH.mm.ss") + ".xls";

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("出租情况统计表");
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);

                headerRow.CreateCell(0).SetCellValue("序号");
                headerRow.CreateCell(1).SetCellValue("合同编号");
                headerRow.CreateCell(2).SetCellValue("资源编号");
                headerRow.CreateCell(3).SetCellValue("面积(\\㎡)");
                headerRow.CreateCell(4).SetCellValue("服务商");
                headerRow.CreateCell(5).SetCellValue("客户名称");
                headerRow.CreateCell(6).SetCellValue("合同类型");
                headerRow.CreateCell(7).SetCellValue("费用类型");
                headerRow.CreateCell(8).SetCellValue("合同状态");
                headerRow.CreateCell(9).SetCellValue("退租状态");
                headerRow.CreateCell(10).SetCellValue("合同签订日期");
                headerRow.CreateCell(11).SetCellValue("合同生效日期");
                headerRow.CreateCell(12).SetCellValue("合同到期日期");
                headerRow.CreateCell(13).SetCellValue("租金起收日期");
                headerRow.CreateCell(14).SetCellValue("实际退租日期");
                headerRow.CreateCell(15).SetCellValue("合同审核日期");
                headerRow.CreateCell(16).SetCellValue("合同审核人员");
                headerRow.CreateCell(17).SetCellValue("减免开始日期1");
                headerRow.CreateCell(18).SetCellValue("减免结束日期1");
                headerRow.CreateCell(19).SetCellValue("减免开始日期2");
                headerRow.CreateCell(20).SetCellValue("减免结束日期2");
                headerRow.CreateCell(21).SetCellValue("减免开始日期3");
                headerRow.CreateCell(22).SetCellValue("减免结束日期3");
                headerRow.CreateCell(23).SetCellValue("减免开始日期4");
                headerRow.CreateCell(24).SetCellValue("减免结束日期4");

                headerRow.CreateCell(25).SetCellValue("是否按固定金额");
                
                headerRow.CreateCell(26).SetCellValue("递增方式");
                headerRow.CreateCell(27).SetCellValue("固定金额");
                headerRow.CreateCell(28).SetCellValue("递增值1");
                headerRow.CreateCell(29).SetCellValue("递增开始时间1");
                headerRow.CreateCell(30).SetCellValue("递增值2");
                headerRow.CreateCell(31).SetCellValue("递增开始时间2");
                headerRow.CreateCell(32).SetCellValue("递增值3");
                headerRow.CreateCell(33).SetCellValue("递增开始时间3");
                headerRow.CreateCell(34).SetCellValue("递增值4");
                headerRow.CreateCell(35).SetCellValue("递增开始时间4");

                int rowIndex = 1;

                DataTable dt = GetTenementProc(jp.getValue("SignStartDate"), jp.getValue("SignEndDate"), jp.getValue("ExpireStartDate"),
                                                jp.getValue("ExpireEndDate"), jp.getValue("RealStartDate"), jp.getValue("RealEndDate"),
                                                jp.getValue("SPNo"), jp.getValue("CustName"), jp.getValue("ContractTypeNo"),
                                                jp.getValue("ContractStatus"), jp.getValue("OffLeaseStatus"));
                foreach (DataRow it in dt.Rows)
                {


                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(rowIndex);
                    dataRow.CreateCell(1).SetCellValue(it["ContractNo"].ToString());
                    dataRow.CreateCell(2).SetCellValue(it["RMID"].ToString());
                    dataRow.CreateCell(3).SetCellValue(ParseDecimalForString(it["RMArea"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(4).SetCellValue(it["SPShortName"].ToString());
                    dataRow.CreateCell(5).SetCellValue(it["CustName"].ToString());
                    dataRow.CreateCell(6).SetCellValue(it["ContractTypeName"].ToString());
                    dataRow.CreateCell(7).SetCellValue(it["SRVName"].ToString());


                    dataRow.CreateCell(8).SetCellValue(it["ContractStatus"].ToString());
                    dataRow.CreateCell(9).SetCellValue(it["OffLeaseStatus"].ToString());
                    dataRow.CreateCell(10).SetCellValue((it["ContractSignedDate"] == DBNull.Value ? "" : ParseDateForString(it["ContractSignedDate"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(11).SetCellValue((it["ContractStartDate"] == DBNull.Value ? "" : ParseDateForString(it["ContractStartDate"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(12).SetCellValue((it["contractenddate"] == DBNull.Value ? "" : ParseDateForString(it["contractenddate"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(13).SetCellValue((it["feestartdate"] == DBNull.Value ? "" : ParseDateForString(it["feestartdate"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(14).SetCellValue((it["OffLeaseActulDate"] == DBNull.Value ? "" : ParseDateForString(it["OffLeaseActulDate"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(15).SetCellValue(it["contractauditdate"].ToString());
                    dataRow.CreateCell(16).SetCellValue(it["contractauditor"].ToString());

                    dataRow.CreateCell(17).SetCellValue((it["ReduceStartDate1"] == DBNull.Value ? "" : ParseDateForString(it["ReduceStartDate1"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(18).SetCellValue((it["ReduceEndDate1"] == DBNull.Value ? "" : ParseDateForString(it["ReduceEndDate1"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(19).SetCellValue((it["ReduceStartDate2"] == DBNull.Value ? "" : ParseDateForString(it["ReduceStartDate2"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(20).SetCellValue((it["ReduceEndDate2"] == DBNull.Value ? "" : ParseDateForString(it["ReduceEndDate2"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(21).SetCellValue((it["ReduceStartDate3"] == DBNull.Value ? "" : ParseDateForString(it["ReduceStartDate3"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(22).SetCellValue((it["ReduceEndDate3"] == DBNull.Value ? "" : ParseDateForString(it["ReduceEndDate3"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(23).SetCellValue((it["ReduceStartDate4"] == DBNull.Value ? "" : ParseDateForString(it["ReduceStartDate4"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(24).SetCellValue((it["ReduceEndDate4"] == DBNull.Value ? "" : ParseDateForString(it["ReduceEndDate4"].ToString()).ToString("yyyy-MM-dd")));

                    dataRow.CreateCell(25).SetCellValue(it["IsFixedAmt"].ToString());
                    dataRow.CreateCell(26).SetCellValue(it["IncreaseType"].ToString());
                    dataRow.CreateCell(27).SetCellValue(ParseDecimalForString(it["Amount"].ToString()).ToString("0.##"));

                    dataRow.CreateCell(28).SetCellValue(ParseDecimalForString(it["IncreaseRate1"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(29).SetCellValue((it["IncreaseStartDate1"] == DBNull.Value ? "" : ParseDateForString(it["IncreaseStartDate1"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(30).SetCellValue(ParseDecimalForString(it["IncreaseRate2"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(31).SetCellValue((it["IncreaseStartDate2"] == DBNull.Value ? "" : ParseDateForString(it["IncreaseStartDate2"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(32).SetCellValue(ParseDecimalForString(it["IncreaseRate3"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(33).SetCellValue((it["IncreaseStartDate3"] == DBNull.Value ? "" : ParseDateForString(it["IncreaseStartDate3"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow.CreateCell(34).SetCellValue(ParseDecimalForString(it["IncreaseRate4"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(35).SetCellValue((it["IncreaseStartDate4"] == DBNull.Value ? "" : ParseDateForString(it["IncreaseStartDate4"].ToString()).ToString("yyyy-MM-dd")));
                    dataRow = null;
                    rowIndex++;
                }
                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                headerRow = null;
                sheet = null;
                workbook = null;
                if (!Directory.Exists(localpath))
                    Directory.CreateDirectory(localpath);
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


        /// <summary>
        /// 未缴费明细
        /// </summary>
        /// <param name="CustName"></param>
        /// <param name="MinARDate"></param>
        /// <param name="MaxARDate"></param>
        /// <returns></returns>
        public DataTable GetTenementProc(string SignStartDate, string SignEndDate, string ExpireStartDate, string ExpireEndDate, string RealStartDate, string RealEndDate,
            string SPNo, string CustName, string ContractTypeNo, string ContractStatus, string OffLeaseStatus)
        {
            SqlConnection con = null;
            SqlCommand command = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GetTenementRT", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@SignStartDate", SqlDbType.NVarChar, 100).Value = SignStartDate;
                command.Parameters.Add("@SignEndDate", SqlDbType.NVarChar, 100).Value = SignEndDate;
                command.Parameters.Add("@ExpireStartDate", SqlDbType.NVarChar, 100).Value = ExpireStartDate;
                command.Parameters.Add("@ExpireEndDate", SqlDbType.NVarChar, 100).Value = ExpireEndDate;
                command.Parameters.Add("@RealStartDate", SqlDbType.NVarChar, 100).Value = RealStartDate;
                command.Parameters.Add("@RealEndData", SqlDbType.NVarChar, 100).Value = RealEndDate;
                command.Parameters.Add("@CustName", SqlDbType.NVarChar, 50).Value = CustName;
                command.Parameters.Add("@SPNo", SqlDbType.NVarChar, 30).Value = SPNo;
                command.Parameters.Add("@ContractStatus", SqlDbType.NVarChar, 2).Value = ContractStatus;
                command.Parameters.Add("@OffLeaseStatus", SqlDbType.NVarChar, 2).Value = OffLeaseStatus;
                command.Parameters.Add("@ContractTypeNo", SqlDbType.NVarChar, 30).Value = ContractTypeNo;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (command != null)
                    command.Dispose();
                if (con != null)
                    con.Dispose();
            }
        }
    }
}