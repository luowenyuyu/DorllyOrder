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
    public partial class GetUnPaymentList : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/GetUnPaymentList.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/GetUnPaymentList.aspx'";
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

                        list = createList("", GetDate().AddDays(-GetDate().Day + 1).ToString("yyyy-MM-dd"), GetDate().ToString("yyyy-MM-dd"));
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

        private string createList(string CustName, string MinARDate, string MaxARDate)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\" style =\"width:1600px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"3%\">序号</th>");
            sb.Append("<th width='6%'>楼栋/楼层</th>");
            sb.Append("<th width='6%'>房间号</th>");
            sb.Append("<th width='5%'>出租面积</th>");
            sb.Append("<th width='9%'>客户名称</th>");
            sb.Append("<th width='11%'>租赁期限</th>");
            sb.Append("<th width='9%'>备注</th>");
            sb.Append("<th width='5%'>单价</th>");
            sb.Append("<th width='5%'>租金</th>");
            sb.Append("<th width='5%'>押金</th>");
            sb.Append("<th width='5%'>管理费</th>");
            sb.Append("<th width='5%'>空调费</th>");
            sb.Append("<th width='5%'>水费</th>");
            sb.Append("<th width='5%'>电费</th>");
            sb.Append("<th width='5%'>其他费</th>");
            sb.Append("<th width='6%'>滞纳金_违约金</th>");
            sb.Append("<th width='5%'>铭华合计</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            DateTime MinARDateS = GetDate().AddYears(-20);
            DateTime MaxARDateS = GetDate().AddYears(20);
            if (MinARDate != "") MinARDateS = ParseDateForString(MinARDate);
            if (MaxARDate != "") MaxARDateS = ParseDateForString(MaxARDate);

            string loc = "";
            string loc1 = "";
            decimal RentAmount = 0;
            decimal WPRentalDeposit = 0;
            decimal Property = 0;
            decimal AirConditiion = 0;
            decimal WaterFee = 0;
            decimal ElectricFee = 0;
            decimal ElseFee = 0;
            decimal PenaltyFee = 0;
            decimal PTTotFee = 0;
            int r = 1;
            sb.Append("<tbody>");
            foreach (DataRow it in GetUnPaymentListProc(CustName, MinARDateS, MaxARDateS).Rows)
            {
                if (loc == it["BuildingLoc"].ToString() + it["FloorLoc"].ToString())
                    loc1 = "";
                else
                {
                    loc = it["BuildingLoc"].ToString() + it["FloorLoc"].ToString();
                    loc1 = loc;
                }
                RentAmount += ParseDecimalForString(it["RentAmount"].ToString());
                WPRentalDeposit += ParseDecimalForString(it["WPRentalDeposit"].ToString());
                Property += ParseDecimalForString(it["Property"].ToString());
                AirConditiion += ParseDecimalForString(it["AirConditiion"].ToString());
                WaterFee += ParseDecimalForString(it["WaterFee"].ToString());
                ElectricFee += ParseDecimalForString(it["ElectricFee"].ToString());
                ElseFee += ParseDecimalForString(it["ElseFee"].ToString());
                PenaltyFee += ParseDecimalForString(it["PenaltyFee"].ToString());
                PTTotFee += ParseDecimalForString(it["PTTotFee"].ToString());

                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + loc1 + "</td>");
                sb.Append("<td>" + it["RMNo"].ToString() + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["RMRentSize"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + it["CustName"].ToString() + "</td>");
                sb.Append("<td>" + it["RentDate"].ToString() + "</td>");
                sb.Append("<td>" + it["Remark"] + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ODUnitPrice"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["RentAmount"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["WPRentalDeposit"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["Property"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["AirConditiion"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["WaterFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ElectricFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ElseFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["PenaltyFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["PTTotFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("</tr>");
                r++;
            }

            sb.Append("<tr class=\"text-c\">");
            sb.Append("<td style=\"text-align:center;\">合计</td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
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
            sb.Append("<td>" + PTTotFee.ToString("0.##") + "</td>");
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
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("CustName"), jp.getValue("MinARDate"), jp.getValue("MaxARDate"))));
            return collection.ToString();
        }
        private string excelaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string pathName = "";
            try
            {
                pathName = "出租情况统计表" + GetDate().ToString("yyMMddHHmmss") + getRandom(4) + ".xls";

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("出租情况统计表");
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("楼栋/楼层");
                headerRow.CreateCell(1).SetCellValue("房间号");
                headerRow.CreateCell(2).SetCellValue("出租面积");
                headerRow.CreateCell(3).SetCellValue("客户名称");
                headerRow.CreateCell(4).SetCellValue("租赁期限");
                headerRow.CreateCell(5).SetCellValue("备注");
                headerRow.CreateCell(6).SetCellValue("单价");
                headerRow.CreateCell(7).SetCellValue("租金");
                headerRow.CreateCell(8).SetCellValue("押金");
                headerRow.CreateCell(9).SetCellValue("管理费");
                headerRow.CreateCell(10).SetCellValue("空调费");
                headerRow.CreateCell(11).SetCellValue("水费");
                headerRow.CreateCell(12).SetCellValue("电费");
                headerRow.CreateCell(13).SetCellValue("其他费");
                headerRow.CreateCell(14).SetCellValue("滞纳金_违约金");
                headerRow.CreateCell(15).SetCellValue("铭华合计");


                DateTime MinARDateS = GetDate().AddYears(-20);
                DateTime MaxARDateS = GetDate().AddYears(20);
                if (jp.getValue("MinARDate") != "") MinARDateS = ParseDateForString(jp.getValue("MinARDate"));
                if (jp.getValue("MaxARDate") != "") MaxARDateS = ParseDateForString(jp.getValue("MaxARDate"));

                int rowIndex = 1;
                string loc = "";
                string loc1 = "";
                decimal RentAmount = 0;
                decimal WPRentalDeposit = 0;
                decimal Property = 0;
                decimal AirConditiion = 0;
                decimal WaterFee = 0;
                decimal ElectricFee = 0;
                decimal ElseFee = 0;
                decimal PenaltyFee = 0;
                decimal PTTotFee = 0;
                foreach (DataRow it in GetUnPaymentListProc(jp.getValue("CustName"), MinARDateS, MaxARDateS).Rows)
                {
                    if (loc == it["BuildingLoc"].ToString() + it["FloorLoc"].ToString())
                        loc1 = "";
                    else
                    {
                        loc = it["BuildingLoc"].ToString() + it["FloorLoc"].ToString();
                        loc1 = loc;
                    }
                    RentAmount += ParseDecimalForString(it["RentAmount"].ToString());
                    WPRentalDeposit += ParseDecimalForString(it["WPRentalDeposit"].ToString());
                    Property += ParseDecimalForString(it["Property"].ToString());
                    AirConditiion += ParseDecimalForString(it["AirConditiion"].ToString());
                    WaterFee += ParseDecimalForString(it["WaterFee"].ToString());
                    ElectricFee += ParseDecimalForString(it["ElectricFee"].ToString());
                    ElseFee += ParseDecimalForString(it["ElseFee"].ToString());
                    PenaltyFee += ParseDecimalForString(it["PenaltyFee"].ToString());
                    PTTotFee += ParseDecimalForString(it["PTTotFee"].ToString());
                    
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(loc1);
                    dataRow.CreateCell(1).SetCellValue(it["RMNo"].ToString());
                    dataRow.CreateCell(2).SetCellValue(ParseDecimalForString(it["RMRentSize"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(3).SetCellValue(it["CustName"].ToString());
                    dataRow.CreateCell(4).SetCellValue(it["RentDate"].ToString());
                    dataRow.CreateCell(5).SetCellValue(it["Remark"].ToString());
                    dataRow.CreateCell(6).SetCellValue(ParseDecimalForString(it["ODUnitPrice"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(7).SetCellValue(ParseDecimalForString(it["RentAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(8).SetCellValue(ParseDecimalForString(it["WPRentalDeposit"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(9).SetCellValue(ParseDecimalForString(it["Property"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(10).SetCellValue(ParseDecimalForString(it["AirConditiion"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(11).SetCellValue(ParseDecimalForString(it["WaterFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(12).SetCellValue(ParseDecimalForString(it["ElectricFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(13).SetCellValue(ParseDecimalForString(it["ElseFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(14).SetCellValue(ParseDecimalForString(it["PenaltyFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(15).SetCellValue(ParseDecimalForString(it["PTTotFee"].ToString()).ToString("0.##"));
                    dataRow = null;
                    rowIndex++;
                }

                HSSFRow dataRow1 = (HSSFRow)sheet.CreateRow(rowIndex);
                dataRow1.CreateCell(0).SetCellValue("合计");
                dataRow1.CreateCell(1).SetCellValue("");
                dataRow1.CreateCell(2).SetCellValue(RentAmount.ToString("0.##"));
                dataRow1.CreateCell(3).SetCellValue("");
                dataRow1.CreateCell(4).SetCellValue("");
                dataRow1.CreateCell(5).SetCellValue("");
                dataRow1.CreateCell(6).SetCellValue("");
                dataRow1.CreateCell(7).SetCellValue(RentAmount.ToString("0.##"));
                dataRow1.CreateCell(8).SetCellValue(WPRentalDeposit.ToString("0.##"));
                dataRow1.CreateCell(9).SetCellValue(Property.ToString("0.##"));
                dataRow1.CreateCell(10).SetCellValue(AirConditiion.ToString("0.##"));
                dataRow1.CreateCell(11).SetCellValue(WaterFee.ToString("0.##"));
                dataRow1.CreateCell(12).SetCellValue(ElectricFee.ToString("0.##"));
                dataRow1.CreateCell(13).SetCellValue(ElseFee.ToString("0.##"));
                dataRow1.CreateCell(14).SetCellValue(PenaltyFee.ToString("0.##"));
                dataRow1.CreateCell(15).SetCellValue(PTTotFee.ToString("0.##"));

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
        /// <param name="CustName"></param>
        /// <param name="MinARDate"></param>
        /// <param name="MaxARDate"></param>
        /// <returns></returns>
        public DataTable GetUnPaymentListProc(string CustName, DateTime MinARDate, DateTime MaxARDate)
        {
            SqlConnection con = null;
            SqlCommand command = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GetUnPaymentList", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@CustName", SqlDbType.NVarChar, 36).Value = CustName;
                command.Parameters.Add("@MinARDate", SqlDbType.NVarChar, 10).Value = MinARDate.ToString("yyyy-MM-dd");
                command.Parameters.Add("@MaxARDate", SqlDbType.NVarChar, 10).Value = MaxARDate.ToString("yyyy-MM-dd");

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