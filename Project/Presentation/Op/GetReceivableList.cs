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
    public partial class GetReceivableList : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\" style=\"width:1600px;\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"3%\">序号</th>");
            sb.Append("<th width='5%'>客户编号</th>");
            sb.Append("<th width='9%'>客户名称</th>");
            sb.Append("<th width='6%'>楼栋楼层</th>");
            sb.Append("<th width='9%'>资源编号</th>");
            sb.Append("<th width='4%'>租金</th>");
            sb.Append("<th width='4%'>往月租金</th>");
            sb.Append("<th width='4%'>管理费</th>");
            sb.Append("<th width='4%'>往月管理费</th>");
            sb.Append("<th width='4%'>空调费</th>");
            sb.Append("<th width='4%'>往月空调费</th>");
            sb.Append("<th width='4%'>水费</th>");
            sb.Append("<th width='4%'>往月水费</th>");
            sb.Append("<th width='4%'>电费</th>");
            sb.Append("<th width='4%'>往月电费</th>");
            sb.Append("<th width='4%'>其他费</th>");
            sb.Append("<th width='4%'>往月其他费</th>");
            sb.Append("<th width='4%'>滞纳金_违约金</th>");
            sb.Append("<th width='4%'>往月滞纳金_违约金</th>");
            sb.Append("<th width='4%'>当月合计费用</th>");
            sb.Append("<th width='4%'>往月合计费用</th>");
            sb.Append("<th width='4%'>合计费用</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            foreach (DataRow it in GetReceivableListProc(Month).Rows)
            {
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it["CustNo"].ToString() + "</td>");
                sb.Append("<td>" + it["CustName"].ToString() + "</td>");
                sb.Append("<td>" + it["BuildingLoc"].ToString() + it["FloorLoc"].ToString() + "</td>");
                sb.Append("<td>" + it["RMID"].ToString() + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["RentAmount"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforeRentAmount"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["Property"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforeProperty"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["AirConditiion"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforeAirConditiion"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["WaterFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforeWaterFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ElectricFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforeElectricFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["ElseFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforeElseFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["PenaltyFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforePenaltyFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["TheTotFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["BeforeTotFee"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it["TotFee"].ToString()).ToString("0.##") + "</td>");

                sb.Append("</tr>");
                r++;
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
                pathName = "应收明细报表" + GetDate().ToString("yyyy-MM") + ".xls";

                HSSFWorkbook workbook = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("应收明细报表" + GetDate().ToString("yyyy-MM"));
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("客户编号");
                headerRow.CreateCell(1).SetCellValue("客户名称");
                headerRow.CreateCell(2).SetCellValue("楼栋楼层");
                headerRow.CreateCell(3).SetCellValue("资源编号");
                headerRow.CreateCell(4).SetCellValue("租金");
                headerRow.CreateCell(5).SetCellValue("往月租金");
                headerRow.CreateCell(6).SetCellValue("管理费");
                headerRow.CreateCell(7).SetCellValue("往月管理费");
                headerRow.CreateCell(8).SetCellValue("空调费");
                headerRow.CreateCell(9).SetCellValue("往月空调费");
                headerRow.CreateCell(10).SetCellValue("水费");
                headerRow.CreateCell(11).SetCellValue("往月水费");
                headerRow.CreateCell(12).SetCellValue("电费");
                headerRow.CreateCell(13).SetCellValue("往月电费");
                headerRow.CreateCell(14).SetCellValue("其他费");
                headerRow.CreateCell(15).SetCellValue("往月其他费");
                headerRow.CreateCell(16).SetCellValue("滞纳金_违约金");
                headerRow.CreateCell(17).SetCellValue("往月滞纳金_违约金");
                headerRow.CreateCell(18).SetCellValue("当月合计费用");
                headerRow.CreateCell(19).SetCellValue("往月合计费用");
                headerRow.CreateCell(20).SetCellValue("合计费用");

                int rowIndex = 1;
                foreach (DataRow it in GetReceivableListProc(jp.getValue("Month")).Rows)
                {
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(it["CustNo"].ToString());
                    dataRow.CreateCell(1).SetCellValue(it["CustName"].ToString());
                    dataRow.CreateCell(2).SetCellValue(it["BuildingLoc"].ToString() + it["FloorLoc"].ToString());
                    dataRow.CreateCell(3).SetCellValue(it["RMID"].ToString());
                    dataRow.CreateCell(4).SetCellValue(ParseDecimalForString(it["RentAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(5).SetCellValue(ParseDecimalForString(it["BeforeRentAmount"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(6).SetCellValue(ParseDecimalForString(it["Property"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(7).SetCellValue(ParseDecimalForString(it["BeforeProperty"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(8).SetCellValue(ParseDecimalForString(it["AirConditiion"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(9).SetCellValue(ParseDecimalForString(it["BeforeAirConditiion"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(10).SetCellValue(ParseDecimalForString(it["WaterFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(11).SetCellValue(ParseDecimalForString(it["BeforeWaterFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(12).SetCellValue(ParseDecimalForString(it["ElectricFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(13).SetCellValue(ParseDecimalForString(it["BeforeElectricFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(14).SetCellValue(ParseDecimalForString(it["ElseFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(15).SetCellValue(ParseDecimalForString(it["BeforeElseFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(16).SetCellValue(ParseDecimalForString(it["PenaltyFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(17).SetCellValue(ParseDecimalForString(it["BeforePenaltyFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(18).SetCellValue(ParseDecimalForString(it["TheTotFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(19).SetCellValue(ParseDecimalForString(it["BeforeTotFee"].ToString()).ToString("0.##"));
                    dataRow.CreateCell(20).SetCellValue(ParseDecimalForString(it["TotFee"].ToString()).ToString("0.##"));

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
        public DataTable GetReceivableListProc(string Month)
        {
            SqlConnection con = null;
            SqlCommand command = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GetReceivableList", con);
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