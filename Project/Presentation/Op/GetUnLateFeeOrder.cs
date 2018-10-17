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
    public partial class GetUnLateFeeOrder : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/GetUnLateFeeOrder.aspx");

                    if (!Page.IsCallback)
                    {
                        DateTime now = GetDate().AddMonths(-1);
                        Month = now.ToString("yyyy-MM");
                        ARDate = now.ToString("yyyy-MM") + "-05";
                        EndDate = ParseDateForString(now.ToString("yyyy-MM") + "-01").AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                        list = createList(Month, ARDate, EndDate, "%");
                        
                        Business.Base.BusinessContractType bc = new Business.Base.BusinessContractType();
                        ContractType += "<select id=\"ContractType\" class=\"input-text required\" style=\"width:120px;\">";
                        ContractType += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityContractType it in bc.GetListQuery(string.Empty, string.Empty))
                        {
                            ContractType += "<option value='" + it.ContractTypeNo + "'>" + it.ContractTypeName + "</option>";
                        }
                        ContractType += "</select>";
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
        protected string ContractType = "";
        protected string Month = "";
        protected string ARDate = "";
        protected string EndDate = "";
        private string createList(string Month, string ARDate, string EndDate, string OrderType)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<td width='5%'><input type=\"checkbox\" class=\"input-text size-MINI\" id=\"checkall\" /></td>");
            sb.Append("<th width='5%'>序号</th>");
            sb.Append("<th width='11%'>订单编号</th>");
            sb.Append("<th width='11%'>合同编号</th>");
            sb.Append("<th width='17%'>费用项目</th>");
            sb.Append("<th width='17%'>资源编号</th>");
            sb.Append("<th width='12%'>未收款金额</th>");
            sb.Append("<th width='10%'>天数</th>");
            sb.Append("<th width='12%'>金额</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");


            string OrderTypes = "";
            if (user.Entity.UserType.ToUpper() == "ADMIN")
            {
                Business.Base.BusinessOrderType bc1 = new project.Business.Base.BusinessOrderType();
                foreach (Entity.Base.EntityOrderType it in bc1.GetListQuery(string.Empty, string.Empty))
                {
                    OrderTypes += it.OrderTypeNo + ",";
                }
            }
            else
            {
                string sqlstr1 = "select a.OrderType,b.OrderTypeName from Sys_UserOrderTypeRight a left join Mstr_OrderType b on a.OrderType=b.OrderTypeNo " +
                        "where a.UserType='" + user.Entity.UserType + "'";
                DataTable dt1 = obj.PopulateDataSet(sqlstr1).Tables[0];
                foreach (DataRow dr in dt1.Rows)
                {
                    OrderTypes += dr["OrderType"].ToString() + ",";
                }
            }
            if (OrderTypes == "") OrderTypes = "''";
            if (OrderType == "") OrderType = "%";

            int r = 1;
            sb.Append("<tbody id=\"tbody\">");
            DataTable dt = GetUnLateFeeOrder_Proc(Month, ARDate, EndDate, OrderType, OrderTypes);
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td><input type=\"checkbox\" class=\"input-text size-MINI\" value=\"" + dr["RowPointer"].ToString() + "\" name=\"chk\" /></td>");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + dr["OrderNo"].ToString() + "</td>");
                sb.Append("<td>" + dr["ODContractNo"].ToString() + "</td>");
                sb.Append("<td>" + dr["SRVName"].ToString() + "</td>");
                sb.Append("<td>" + dr["ResourceNo"].ToString() + "</td>");
                sb.Append("<td>" +  ParseDecimalForString(dr["UnPaidAmount"].ToString()).ToString("0.##") + "</td>");
                sb.Append("<td>" + dr["LateDays"].ToString() + "</td>");
                sb.Append("<td>" + ParseDecimalForString(dr["LateAmount"].ToString()).ToString("0.##") + "</td>");
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
            else if (jp.getValue("Type") == "genorder")
                result = genorderaction(jp);
            return result;
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Month"), jp.getValue("ARDate"), jp.getValue("EndDate"), jp.getValue("OrderType"))));
            return collection.ToString();
        }
        private string genorderaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            string TabName = "Tmp" + getRandom();
            obj.ExecuteNonQuery("CREATE TABLE " + TabName + "(OrderID NVARCHAR(36))");
            foreach (string str in jp.getValue("OrderID").Split(';'))
            {
                if (str == "") continue;
                obj.ExecuteNonQuery("INSERT INTO " + TabName + " VALUES('" + str + "')");
            }

            string InfoBar = GenOrderFromLateFeeOrder(TabName, jp.getValue("ARDate"), jp.getValue("EndDate"), user.Entity.UserName);
            if (InfoBar != "")
            {
                flag = "2";
                collection.Add(new JsonStringValue("InfoBar", InfoBar));
            }

            collection.Add(new JsonStringValue("type", "genorder"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("Month"), jp.getValue("ARDate"), jp.getValue("EndDate"), jp.getValue("OrderType"))));
            return collection.ToString();
        }



        /// </summary>
        /// 获取待交滞纳金
        /// </summary>
        public DataTable GetUnLateFeeOrder_Proc(string Month, string ARDate, string EndDate, string OrderType,string UserOrderType)
        {
            SqlConnection con = null;
            SqlCommand command = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GetUnLateFeeOrder", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Month", SqlDbType.NVarChar, 7).Value = Month;
                command.Parameters.Add("@ARDate", SqlDbType.NVarChar, 10).Value = ARDate;
                command.Parameters.Add("@EndDate", SqlDbType.NVarChar, 10).Value = EndDate;
                command.Parameters.Add("@OrderType", SqlDbType.NVarChar, 30).Value = OrderType;
                command.Parameters.Add("@UserOrderType", SqlDbType.NVarChar, 500).Value = UserOrderType;

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

        
        /// </summary>
        /// 生成订单
        /// </summary>
        public string GenOrderFromLateFeeOrder(string TableName, string ARDate, string EndDate, string UserName)
        {

            string InfoMsg = "";
            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GenOrderFromLateFeeOrder", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@TableName", SqlDbType.NVarChar, 30).Value = TableName;
                command.Parameters.Add("@ARDate", SqlDbType.NVarChar, 10).Value = ARDate;
                command.Parameters.Add("@EndDate", SqlDbType.NVarChar, 10).Value = EndDate;
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