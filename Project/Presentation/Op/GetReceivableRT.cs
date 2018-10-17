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
    public partial class GetReceivableRT : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
        protected string userid = "";
        private string _clientArgument = "";
        protected string FeeTypeSelectStr = string.Empty;
        protected string OrderTypeSelectStr = string.Empty;
        protected string OrderStatusSelectStr = string.Empty;
        protected string SPSelectStr = string.Empty;
        Data obj = new Data();
        protected string list = "";
        protected string Buttons = "";
        protected decimal TotalEarnPaid = 0;//原应交总额
        protected decimal TotalReduceAmt = 0;  //减免总额
        protected decimal TotalARAmt = 0;  //应交金额
        protected decimal TotalPaid = 0;  //已交总额
        protected decimal TotalUnpaid = 0;//未交总额
        protected decimal TotalTax = 0;//税额总额
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
                    CheckRight(user.Entity, "pm/Op/GetReceivableRT.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/GetReceivableRT.aspx'";
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

                        list = createList(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"), DateTime.Now.ToString("yyyy-MM"), "", "","","","");
                        bindFeeTypeSelect();
                        bindOrderTypeSelect();
                        bindOrderStatusSelect();
                        bindSPSelect();
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

        /// <summary>
        /// 绑定“订单状态”下拉选择项数据
        /// </summary>
        private void bindOrderStatusSelect()
        {
            StringBuilder sb = new StringBuilder("<select class=\"input-text size-MINI\" style=\"width:110px\" id=\"OrderStatus\">");
            sb.AppendLine("<option value=\"\" select>全部</option>");
            sb.AppendLine("<option value=\"0\" select>等待审核</option>");
            sb.AppendLine("<option value=\"1\" select>审核通过</option>");
            sb.AppendLine("<option value=\"2\" select>部分收款</option>");
            sb.AppendLine("<option value=\"3\" select>完成收款</option>");
            sb.AppendLine("</select>");
            OrderStatusSelectStr = sb.ToString();
        }
        /// <summary>
        /// 绑定“订单主体”下拉选择项数据
        /// </summary>
        private void bindSPSelect()
        {
            StringBuilder sb = new StringBuilder("<select class=\"input-text size-MINI\" style=\"width:110px\" id=\"SP\">");
            sb.AppendLine("<option value=\"\" select>全部</option>");
            string sql = "select SPNo,SPShortName from Mstr_ServiceProvider where SPStatus='1'";
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
            SPSelectStr = sb.ToString();
        }
        /// <summary>
        /// 绑定“订单类别”下拉选择项数据
        /// </summary>
        private void bindOrderTypeSelect()
        {
            StringBuilder sb = new StringBuilder("<select class=\"input-text size-MINI\" style=\"width:110px\" id=\"OrderType\">");
            sb.AppendLine("<option value=\"\" select>全部</option>");
            string sql = "select * from Mstr_OrderType";
            Data data = new Data();
            try
            {
                DataTable dt = data.PopulateDataSet(sql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<option value=\"");
                    sb.Append(dt.Rows[i]["OrderTypeNo"].ToString() + "\">");
                    sb.Append(dt.Rows[i]["OrderTypeName"].ToString());
                    sb.AppendLine("</option>");

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            sb.AppendLine("</select>");
            OrderTypeSelectStr = sb.ToString();
        }
        /// <summary>
        /// 绑定“费用项目”下拉选择项数据
        /// </summary>
        private void bindFeeTypeSelect()
        {
            StringBuilder sb = new StringBuilder("<select class=\"input-text size-MINI\" style=\"width:110px\" id=\"FeeType\">");
            sb.AppendLine("<option value=\"\" select>全部</option>");
            string sql = "SELECT SRVNO,SRVNAME FROM Mstr_Service where SRVStatus=1";
            Data data = new Data();
            try
            {
                DataTable dt = data.PopulateDataSet(sql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<option value=\"");
                    sb.Append(dt.Rows[i]["SRVNO"].ToString() + "\">");
                    sb.Append(dt.Rows[i]["SRVNAME"].ToString());
                    sb.AppendLine("</option>");

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            #region 数据库直接操作
            // SqlConnection con = null;
            //SqlCommand command = null;
            //DataSet ds = null;
            //try
            //{
            //    con = Data.Conn();
            //    command = con.CreateCommand();
            //    command.CommandText = sql;
            //    con.Open();

            //    SqlDataAdapter da = new SqlDataAdapter(command);
            //    ds = new DataSet();
            //    da.Fill(ds);
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    if (command != null)
            //        command.Dispose();
            //    if (con != null)
            //        con.Dispose();
            //}
            //if (ds != null)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        sb.Append("<option value=");
            //        sb.Append(ds.Tables[0].Rows[i]["SRVNO"].ToString()+"\">");
            //        sb.Append(ds.Tables[0].Rows[i]["SRVNAME"].ToString());
            //        sb.AppendLine("</option>");
            //    }
            //} 
            #endregion

            sb.AppendLine("</select>");
            FeeTypeSelectStr = sb.ToString();
        }

        private string createList(string BeginMonth, string EndMonth, string CustName, string FeeTypeNo, string OrderType, string OrderStatus, string SP)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\" style=\"width:1800px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"2%\">序号</th>");
            sb.Append("<th width='5%'>合同编号</th>");
            sb.Append("<th width='5%'>订单编号</th>");
            sb.Append("<th width='3%'>主体</th>");
            sb.Append("<th width='5%'>订单类别</th>");
            sb.Append("<th width='4%'>订单状态</th>");
            sb.Append("<th width='12%'>客户名称</th>");
            sb.Append("<th width='4%'>所属年月</th>");
            sb.Append("<th width='4%'>费用项目</th>");
            sb.Append("<th width='10%'>资源编号</th>");
            sb.Append("<th width='3%'>税率</th>");
            sb.Append("<th width='3%'>税额</th>");
            sb.Append("<th width='4%'>原应缴金额</th>");
            sb.Append("<th width='4%'>减免金额</th>");
            sb.Append("<th width='4%'>应缴金额</th>");
            sb.Append("<th width='4%'>已缴金额</th>");
            sb.Append("<th width='4%'>未缴金额</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int r = 1;
            sb.Append("<tbody>");
            foreach (DataRow it in GetReceivableListProc(BeginMonth, EndMonth, CustName, FeeTypeNo,OrderType,OrderStatus,SP).Rows)
            {
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it["ODContractNo"].ToString() + "</td>");
                sb.Append("<td>" + it["OrderNo"].ToString() + "</td>");
                sb.Append("<td>" + it["SPShortName"].ToString() + "</td>");
                sb.Append("<td>" + it["OrderTypeName"].ToString() + "</td>");//SPShortName
                sb.Append("<td>" + it["OrderStatus"].ToString() + "</td>");
                sb.Append("<td>" + it["CustName"].ToString() + "</td>");
                sb.Append("<td>" + it["ReceTime"].ToString() + "</td>");
                sb.Append("<td>" + it["SRVName"].ToString() + "</td>");
                sb.Append("<td>" + it["ResourceNo"].ToString() + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ODTaxRate"].ToString()).ToString("0.00") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ODTaxAmount"].ToString()).ToString("0.00") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["Amount"].ToString()).ToString("0.00") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ReduceAmount"].ToString()).ToString("0.00") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ARAmount"].ToString()).ToString("0.00") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["PaidAmount"].ToString()).ToString("0.00") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["UnPaidAmount"].ToString()).ToString("0.00") + "</td>"); ;
                sb.Append("</tr>");
                
                TotalTax += Convert.ToDecimal(ParseDecimalForString(it["ODTaxAmount"].ToString()));
                TotalEarnPaid += Convert.ToDecimal(ParseDecimalForString(it["Amount"].ToString()));
                TotalReduceAmt += Convert.ToDecimal(ParseDecimalForString(it["ReduceAmount"].ToString()));
                TotalARAmt += Convert.ToDecimal(ParseDecimalForString(it["ARAmount"].ToString()));
                TotalPaid += Convert.ToDecimal(ParseDecimalForString(it["PaidAmount"].ToString()));
                TotalUnpaid += Convert.ToDecimal(ParseDecimalForString(it["UnPaidAmount"].ToString()));

                r++;
            }
            sb.Append("<tr class=\"text-c\" style=\"font-weight:bold;\"><td colspan=\"11\">合计</td>");
            sb.Append("<td>" + TotalTax.ToString("0.00") + "</td>");
            sb.Append("<td>" + TotalEarnPaid.ToString("0.00") + "</td>");
            sb.Append("<td>" + TotalReduceAmt.ToString("0.00") + "</td>");
            sb.Append("<td>" + TotalARAmt.ToString("0.00") + "</td>");
            sb.Append("<td>" + TotalPaid.ToString("0.00") + "</td>");
            sb.Append("<td>" + TotalUnpaid.ToString("0.00") + "</td>");
            sb.Append("</tr>");
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


        string System.Web.UI.ICallbackEventHandler.GetCallbackResult()
        {
            string result = "";
            JsonArrayParse jp = new JsonArrayParse(this._clientArgument);

            if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "excel")
                result = excelaction(jp);
            return result;
        }

        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("BeginMonth"),
                jp.getValue("EndMonth"), 
                jp.getValue("CustName"), 
                jp.getValue("FeeTypeNo"), 
                jp.getValue("OrderType"), 
                jp.getValue("OrderStatus"), 
                jp.getValue("SP"))));
            return collection.ToString();
        }
        private string excelaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            try
            {
                pathName = string.Format("应收明细报表[查询日期-{0}][客户名称-{1}][费用类型-{2}].xls", jp.getValue("Month"), jp.getValue("CustName") == "" ? "全部" : jp.getValue("CustName"), jp.getValue("FeeTypeName"));

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("应收明细报表" + GetDate().ToString("yyyy-MM"));
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("合同编号");
                headerRow.CreateCell(1).SetCellValue("订单编号");
                headerRow.CreateCell(2).SetCellValue("订单类别");
                headerRow.CreateCell(3).SetCellValue("订单状态");
                headerRow.CreateCell(4).SetCellValue("客户名称");
                headerRow.CreateCell(5).SetCellValue("所属年月");
                headerRow.CreateCell(6).SetCellValue("费用项目");
                headerRow.CreateCell(7).SetCellValue("资源编号");
                headerRow.CreateCell(8).SetCellValue("税率");
                headerRow.CreateCell(9).SetCellValue("税额");
                headerRow.CreateCell(10).SetCellValue("原应缴金额");
                headerRow.CreateCell(11).SetCellValue("减免金额");
                headerRow.CreateCell(12).SetCellValue("应缴金额");
                headerRow.CreateCell(13).SetCellValue("已缴金额");
                headerRow.CreateCell(14).SetCellValue("未缴金额");

                int rowIndex = 1;
                foreach (DataRow it in GetReceivableListProc(jp.getValue("BeginMonth"), jp.getValue("EndMonth"), jp.getValue("CustName"), jp.getValue("FeeTypeNo"),
                jp.getValue("OrderType"),
                jp.getValue("OrderStatus"),
                jp.getValue("SP")).Rows)
                {
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(it["ODContractNo"].ToString());
                    dataRow.CreateCell(1).SetCellValue(it["OrderNo"].ToString());
                    dataRow.CreateCell(2).SetCellValue(it["OrderTypeName"].ToString());
                    dataRow.CreateCell(3).SetCellValue(it["OrderStatus"].ToString());
                    dataRow.CreateCell(4).SetCellValue(it["CustName"].ToString());
                    dataRow.CreateCell(5).SetCellValue(it["ReceTime"].ToString());
                    dataRow.CreateCell(6).SetCellValue(it["SRVName"].ToString());
                    dataRow.CreateCell(7).SetCellValue(it["ResourceNo"].ToString());
                    dataRow.CreateCell(8).SetCellValue(it["ODTaxRate"].ToString());
                    dataRow.CreateCell(9).SetCellValue(ParseDecimalForString(it["ODTaxAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(10).SetCellValue(ParseDecimalForString(it["Amount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(11).SetCellValue(ParseDecimalForString(it["ReduceAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(12).SetCellValue(ParseDecimalForString(it["ARAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(13).SetCellValue(ParseDecimalForString(it["PaidAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(14).SetCellValue(ParseDecimalForString(it["UnPaidAmount"].ToString()).ToString("0.##"));

                    TotalTax += ParseDecimalForString(it["ODTaxAmount"].ToString());
                    TotalEarnPaid += ParseDecimalForString(it["Amount"].ToString());
                    TotalReduceAmt += ParseDecimalForString(it["ReduceAmount"].ToString());
                    TotalARAmt += ParseDecimalForString(it["ARAmount"].ToString());
                    TotalReduceAmt += ParseDecimalForString(it["ReduceAmount"].ToString());
                    TotalARAmt += ParseDecimalForString(it["ARAmount"].ToString());
                    TotalPaid += ParseDecimalForString(it["PaidAmount"].ToString());
                    TotalUnpaid += ParseDecimalForString(it["UnPaidAmount"].ToString());
                    dataRow = null;
                    rowIndex++;
                }
                sheet.AddMergedRegion(new NPOI.SS.Util.Region(rowIndex, 0, rowIndex, 8));
                HSSFRow row = (HSSFRow)sheet.CreateRow(rowIndex);
                HSSFCell cell = (HSSFCell)row.CreateCell(0);
                cell.SetCellValue("合计");
                row.CreateCell(9).SetCellValue(Convert.ToDouble(TotalTax));
                row.CreateCell(10).SetCellValue(Convert.ToDouble(TotalEarnPaid));
                row.CreateCell(11).SetCellValue(Convert.ToDouble(TotalReduceAmt));
                row.CreateCell(12).SetCellValue(Convert.ToDouble(TotalARAmt));
                row.CreateCell(13).SetCellValue(Convert.ToDouble(TotalPaid));
                row.CreateCell(14).SetCellValue(Convert.ToDouble(TotalUnpaid));


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
        /// 应收款明细表
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public DataTable GetReceivableListProc(string BeginMonth, string EndMonth, string CustName, string FeeTypeNo,string OrderType,string OrderStatus,string SP)
        {
            SqlConnection con = null;
            SqlCommand command = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GetReceivableRT", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@BeginMonth", SqlDbType.NVarChar, 7).Value = BeginMonth;
                command.Parameters.Add("@EndMonth", SqlDbType.NVarChar, 7).Value = EndMonth;
                command.Parameters.Add("@CustName", SqlDbType.NVarChar, 50).Value = CustName;
                command.Parameters.Add("@SRVNo", SqlDbType.NVarChar, 30).Value = FeeTypeNo;
                command.Parameters.Add("@OrderType", SqlDbType.NVarChar, 30).Value = OrderType;
                command.Parameters.Add("@OrderStatus", SqlDbType.NVarChar, 30).Value = OrderStatus;
                command.Parameters.Add("@SProvider", SqlDbType.NVarChar, 30).Value = SP;
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