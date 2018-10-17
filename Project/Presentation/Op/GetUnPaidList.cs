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

namespace project.Presentation.Op
{
    public partial class GetUnPaidList : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/GetUnPaidList.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/GetUnPaidList.aspx'";
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

                        list = createList(GetDate().ToString("yyyy-MM"));
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

        private string createList(string Month)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"3%\">序号</th>");
            sb.Append("<th width='12%'>客户</th>");
            sb.Append("<th width='12%'>房间代码</th>");
            sb.Append("<th width='7%'>租金</th>");
            sb.Append("<th width='7%'>押金</th>");
            sb.Append("<th width='7%'>管理费</th>");
            sb.Append("<th width='7%'>空调费</th>");
            sb.Append("<th width='7%'>水费</th>");
            sb.Append("<th width='7%'>电费</th>");
            sb.Append("<th width='7%'>其他费</th>");
            sb.Append("<th width='8%'>滞纳金_违约金</th>");
            sb.Append("<th width='8%'>往月费用</th>");
            sb.Append("<th width='8%'>合计</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            
            decimal RentAmount = 0;
            decimal WPRentalDeposit = 0;
            decimal Property = 0;
            decimal AirConditiion = 0;
            decimal WaterFee = 0;
            decimal ElectricFee = 0;
            decimal ElseFee = 0;
            decimal PenaltyFee = 0;
            decimal BeforeFee = 0;
            decimal TotFee = 0;
            int r = 1;
            sb.Append("<tbody>");
            foreach (DataRow it in GetUnPaidListProc(Month).Rows)
            {
                RentAmount += ParseDecimalForString(it["RentAmount"].ToString());
                WPRentalDeposit += ParseDecimalForString(it["WPRentalDeposit"].ToString());
                Property += ParseDecimalForString(it["Property"].ToString());
                AirConditiion += ParseDecimalForString(it["AirConditiion"].ToString());
                WaterFee += ParseDecimalForString(it["WaterFee"].ToString());
                ElectricFee += ParseDecimalForString(it["ElectricFee"].ToString());
                ElseFee += ParseDecimalForString(it["ElseFee"].ToString());
                PenaltyFee += ParseDecimalForString(it["PenaltyFee"].ToString());
                BeforeFee += ParseDecimalForString(it["BeforeFee"].ToString());
                TotFee += ParseDecimalForString(it["TotFee"].ToString());

                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it["CustName"].ToString() + "</td>");
                sb.Append("<td>" + it["RMID"].ToString() + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["RentAmount"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["WPRentalDeposit"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["Property"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["AirConditiion"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["WaterFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ElectricFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ElseFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["PenaltyFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforeFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["TotFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("</tr>");
                r++;
            }

            sb.Append("<tr class=\"text-c\">");
            sb.Append("<td style=\"text-align:center;\">合计</td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td>" + RentAmount.ToString("0.##") + "</td>");
            sb.Append("<td>" + WPRentalDeposit.ToString("0.##") + "</td>");
            sb.Append("<td>" + Property.ToString("0.##") + "</td>");
            sb.Append("<td>" + AirConditiion.ToString("0.##") + "</td>");
            sb.Append("<td>" + WaterFee.ToString("0.##") + "</td>");
            sb.Append("<td>" + ElectricFee.ToString("0.##") + "</td>");
            sb.Append("<td>" + ElseFee.ToString("0.##") + "</td>");
            sb.Append("<td>" + PenaltyFee.ToString("0.##") + "</td>");
            sb.Append("<td>" + BeforeFee.ToString("0.##") + "</td>");
            sb.Append("<td>" + TotFee.ToString("0.##") + "</td>");
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Month"))));
            return collection.ToString();
        }
        private string excelaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            try
            {
                pathName = "未缴明细表" + GetDate().ToString("yyyy-MM") + ".xls";

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("未缴明细表" + GetDate().ToString("yyyy-MM"));
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("客户名称");
                headerRow.CreateCell(1).SetCellValue("房间代码");
                headerRow.CreateCell(2).SetCellValue("租金");
                headerRow.CreateCell(3).SetCellValue("押金");
                headerRow.CreateCell(4).SetCellValue("管理费");
                headerRow.CreateCell(5).SetCellValue("空调费");
                headerRow.CreateCell(6).SetCellValue("水费");
                headerRow.CreateCell(7).SetCellValue("电费");
                headerRow.CreateCell(8).SetCellValue("其他费");
                headerRow.CreateCell(9).SetCellValue("滞纳金_违约金");
                headerRow.CreateCell(10).SetCellValue("往月费用");
                headerRow.CreateCell(11).SetCellValue("合计");

                
                int rowIndex = 1;
                decimal RentAmount = 0;
                decimal WPRentalDeposit = 0;
                decimal Property = 0;
                decimal AirConditiion = 0;
                decimal WaterFee = 0;
                decimal ElectricFee = 0;
                decimal ElseFee = 0;
                decimal PenaltyFee = 0;
                decimal BeforeFee = 0;
                decimal TotFee = 0;
                foreach (DataRow it in GetUnPaidListProc(jp.getValue("Month")).Rows)
                {
                    RentAmount += ParseDecimalForString(it["RentAmount"].ToString());
                    WPRentalDeposit += ParseDecimalForString(it["WPRentalDeposit"].ToString());
                    Property += ParseDecimalForString(it["Property"].ToString());
                    AirConditiion += ParseDecimalForString(it["AirConditiion"].ToString());
                    WaterFee += ParseDecimalForString(it["WaterFee"].ToString());
                    ElectricFee += ParseDecimalForString(it["ElectricFee"].ToString());
                    ElseFee += ParseDecimalForString(it["ElseFee"].ToString());
                    PenaltyFee += ParseDecimalForString(it["PenaltyFee"].ToString());
                    BeforeFee += ParseDecimalForString(it["BeforeFee"].ToString());
                    TotFee += ParseDecimalForString(it["TotFee"].ToString());

                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(it["CustName"].ToString());
                    dataRow.CreateCell(1).SetCellValue(it["RMID"].ToString());
                    dataRow.CreateCell(2).SetCellValue(ParseDecimalForString(it["RentAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(3).SetCellValue(ParseDecimalForString(it["WPRentalDeposit"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(4).SetCellValue(ParseDecimalForString(it["Property"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(5).SetCellValue(ParseDecimalForString(it["AirConditiion"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(6).SetCellValue(ParseDecimalForString(it["WaterFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(7).SetCellValue(ParseDecimalForString(it["ElectricFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(8).SetCellValue(ParseDecimalForString(it["ElseFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(9).SetCellValue(ParseDecimalForString(it["PenaltyFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(10).SetCellValue(ParseDecimalForString(it["BeforeFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(11).SetCellValue(ParseDecimalForString(it["TotFee"].ToString()).ToString("0.##"));
                    dataRow = null;
                    rowIndex++;
                }

                HSSFRow dataRow1 = (HSSFRow)sheet.CreateRow(rowIndex);
                dataRow1.CreateCell(0).SetCellValue("合计");
                dataRow1.CreateCell(1).SetCellValue("");
                dataRow1.CreateCell(2).SetCellValue(RentAmount.ToString("0.##"));
                dataRow1.CreateCell(3).SetCellValue(WPRentalDeposit.ToString("0.##"));
                dataRow1.CreateCell(4).SetCellValue(Property.ToString("0.##"));
                dataRow1.CreateCell(5).SetCellValue(AirConditiion.ToString("0.##"));
                dataRow1.CreateCell(6).SetCellValue(WaterFee.ToString("0.##"));
                dataRow1.CreateCell(7).SetCellValue(ElectricFee.ToString("0.##"));
                dataRow1.CreateCell(8).SetCellValue(ElseFee.ToString("0.##"));
                dataRow1.CreateCell(9).SetCellValue(PenaltyFee.ToString("0.##"));
                dataRow1.CreateCell(10).SetCellValue(BeforeFee.ToString("0.##"));
                dataRow1.CreateCell(11).SetCellValue(TotFee.ToString("0.##"));

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

            collection.Add(new JsonStringValue("type", "excel"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("path", pathName));
            return collection.ToString();
        }


        /// <summary>
        /// 未缴费明细
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public DataTable GetUnPaidListProc(string Month)
        {
            SqlConnection con = null;
            SqlCommand command = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GetUnPaidList", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Month", SqlDbType.NVarChar, 7).Value = Month;

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