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
    /// <summary>
    /// 未收款报表类
    /// </summary>
    public partial class GetUnPaidRT : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/GetUnPaidRT.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/GetUnPaidRT.aspx'";
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

                        list = createList(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"), DateTime.Now.ToString("yyyy-MM"), "", "");
                        bindFeeTypeSelect();
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
        protected string FeeTypeSelectStr = string.Empty;
        protected decimal TotalEarnPaid = 0;//应交总额
        protected decimal TotalPaid = 0;        //已交总额
        protected decimal TotalUnpaid = 0;//未交总额
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

        private string createList(string BeginMonth, string EndMonth, string CustName,string FeeTypeNo)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"2%\">序号</th>");
            sb.Append("<th width=\"5%\">订单编号</th>");
            sb.Append("<th width='6%'>订单类别</th>");
            sb.Append("<th width='5%'>订单状态</th>");
            sb.Append("<th width='20%'>客户名称</th>");
            sb.Append("<th width='6%'>所属年月</th>");
            sb.Append("<th width='7%'>合同编号</th>");
            sb.Append("<th width='13%'>资源编号</th>");
            sb.Append("<th width='5%'>费用项目</th>");
            sb.Append("<th width='6%'>应缴金额</th>");
            sb.Append("<th width='6%'>已缴金额</th>");
            sb.Append("<th width='6%'>未缴金额</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            int order = 1;
            
            sb.Append("<tbody>");
            foreach (DataRow it in GetUnPaidListProc(BeginMonth,EndMonth,CustName,FeeTypeNo).Rows)
            {
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td>" + order++ + "</td>");
                sb.Append("<td>" + it["OrderNo"].ToString() + "</td>");
                sb.Append("<td>" + it["OrderTypeName"].ToString() + "</td>");
                sb.Append("<td>" + it["OrderStatus"].ToString()+ "</td>");
                sb.Append("<td>" + it["CustName"].ToString() + "</td>");
                sb.Append("<td>" + it["OrderTime"].ToString() + "</td>");
                sb.Append("<td>" + it["ODContractNo"].ToString() + "</td>");
                sb.Append("<td>" + it["ResourceNo"].ToString() + "</td>");
                sb.Append("<td>" + it["SRVName"].ToString() + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ODARAmount"].ToString()).ToString("0.00") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["PaidAmount"].ToString()).ToString("0.00") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["UnpaidAmount"].ToString()).ToString("0.00") + "</td>");
                sb.Append("</tr>");
               TotalEarnPaid += Convert.ToDecimal(it["ODARAmount"].ToString());
                TotalPaid += Convert.ToDecimal(it["PaidAmount"].ToString());
                TotalUnpaid += Convert.ToDecimal(it["UnpaidAmount"].ToString());
            }
            sb.Append("<tr class=\"text-c\" style=\"font-weight:bold;\"><td colspan=\"9\">合计</td>");
            sb.Append("<td>" + TotalEarnPaid.ToString("0.00") + "</td>");
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
        private string _clientArgument = "";

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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("BeginMonth"), jp.getValue("EndMonth"), jp.getValue("CustName"), jp.getValue("FeeTypeNo"))));
            return collection.ToString();
        }
        private string excelaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            try
            {
                pathName = string.Format("未缴明细报表[查询日期-{0}][客户名称-{1}][费用类型-{2}].xls", jp.getValue("Month"), jp.getValue("CustName") == "" ? "全部" : jp.getValue("CustName"), jp.getValue("FeeTypeName"));
                //pathName = "未缴明细报表[客户" + GetDate().ToString("yyyy-MM") + ".xls";

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("未缴明细表" + GetDate().ToString("yyyy-MM"));
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("序号");
                headerRow.CreateCell(1).SetCellValue("订单编号");
                headerRow.CreateCell(2).SetCellValue("订单类别");
                headerRow.CreateCell(3).SetCellValue("订单状态");
                headerRow.CreateCell(4).SetCellValue("客户名称");
                headerRow.CreateCell(5).SetCellValue("所属年月");
                headerRow.CreateCell(6).SetCellValue("合同编号");
                headerRow.CreateCell(7).SetCellValue("资源编号");
                headerRow.CreateCell(8).SetCellValue("费用项目");
                headerRow.CreateCell(9).SetCellValue("应缴金额");
                headerRow.CreateCell(10).SetCellValue("已缴金额");
                headerRow.CreateCell(11).SetCellValue("未缴金额");


                int rowIndex = 1;
                foreach (DataRow it in GetUnPaidListProc(jp.getValue("BeginMonth"), jp.getValue("EndMonth"), jp.getValue("CustName"),jp.getValue("FeeTypeNo")).Rows)
                {
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(rowIndex.ToString());
                    dataRow.CreateCell(1).SetCellValue(it["OrderNo"].ToString());
                    dataRow.CreateCell(2).SetCellValue(it["OrderTypeName"].ToString());
                    dataRow.CreateCell(3).SetCellValue(it["OrderStatus"].ToString());
                    dataRow.CreateCell(4).SetCellValue(it["CustName"].ToString());
                    dataRow.CreateCell(5).SetCellValue(it["OrderTime"].ToString());
                    dataRow.CreateCell(6).SetCellValue(it["ODContractNo"].ToString());
                    dataRow.CreateCell(7).SetCellValue(it["ResourceNo"].ToString());
                    dataRow.CreateCell(8).SetCellValue(it["SRVName"].ToString());
                    dataRow.CreateCell(9).SetCellValue(Convert.ToDouble(it["ODARAmount"].ToString()));// ParseDecimalForString(it["ODARAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(10).SetCellValue(Convert.ToDouble(it["PaidAmount"].ToString())); //ParseDecimalForString(it["PaidAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(11).SetCellValue(Convert.ToDouble(it["UnpaidAmount"].ToString())); //ParseDecimalForString(it["UnpaidAmount"].ToString()).ToString("0.##"));
                    TotalEarnPaid += Convert.ToDecimal(it["ODARAmount"].ToString());
                    TotalPaid += Convert.ToDecimal(it["PaidAmount"].ToString());
                    TotalUnpaid += Convert.ToDecimal(it["UnpaidAmount"].ToString());
                    dataRow = null;
                    rowIndex++;
                }

                sheet.AddMergedRegion(new NPOI.SS.Util.Region(rowIndex,0,rowIndex,8));
                HSSFRow row = (HSSFRow)sheet.CreateRow(rowIndex);
                HSSFCell cell =(HSSFCell) row.CreateCell(0);
                cell.SetCellValue("合计");
                row.CreateCell(9).SetCellValue(Convert.ToDouble(TotalEarnPaid));
                row.CreateCell(10).SetCellValue(Convert.ToDouble(TotalPaid)); 
                row.CreateCell(11).SetCellValue(Convert.ToDouble(TotalUnpaid));


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
        /// 未缴费明细报表数据库查询
        /// </summary>
        /// <param name="Month">月份</param>
        /// <param name="CustName">客户名称，支持模糊查询</param>
        /// <param name="FeeTypeNo">费用编号</param>
        /// <returns>未缴费明细表</returns>
        public DataTable GetUnPaidListProc(string BeginMonth,string EndMonth,string CustName,string FeeTypeNo)
            {
            SqlConnection con = null;
            SqlCommand command = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GetUnPaidRT", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@BeginMonth", SqlDbType.NVarChar, 7).Value = BeginMonth;
                command.Parameters.Add("@EndMonth", SqlDbType.NVarChar, 7).Value = EndMonth;
                command.Parameters.Add("@CustName", SqlDbType.NVarChar, 50).Value = CustName;
                command.Parameters.Add("@SRVNo", SqlDbType.NVarChar, 30).Value = FeeTypeNo;
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