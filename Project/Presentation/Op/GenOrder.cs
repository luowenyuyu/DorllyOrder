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
    public partial class GenOrder : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/GenOrder.aspx");

                    if (!Page.IsCallback)
                    {
                        list = createList("", "", "", "", "", "",GetDate().AddMonths(-1).ToString("yyyy-MM"),GetDate().ToString("yyyy-MM"),"");

                        Business.Base.BusinessLocation loc = new Business.Base.BusinessLocation();
                        RMLOCNo1Str += "<select id=\"RMLOCNo1\" class=\"input-text required\" style=\"width:120px;\">";
                        RMLOCNo1Str += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityLocation it in loc.GetListQuery(string.Empty, string.Empty, "null"))
                        {
                            RMLOCNo1Str += "<option value='" + it.LOCNo + "'>" + it.LOCName + "</option>";
                        }
                        RMLOCNo1Str += "</select>";

                        Business.Base.BusinessContractType bc = new Business.Base.BusinessContractType();
                        ContractType += "<select id=\"ContractType\" class=\"input-text required\" style=\"width:120px;\">";
                        ContractType += "<option value=''>全部</option>";
                        foreach (Entity.Base.EntityContractType it in bc.GetListQuery(string.Empty, string.Empty))
                        {
                            ContractType += "<option value='" + it.ContractTypeNo + "'>" + it.ContractTypeName + "</option>";
                        }
                        ContractType += "</select>";

                        SPNoStr = "<select class=\"input-text size-MINI\" id=\"SPNo\" style=\"width:120px;\" >";
                        SPNoStr += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessServiceProvider sp = new Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider item in sp.GetListQuery("", "", true))
                        {
                            SPNoStr += "<option value='" + item.SPNo + "'>" + item.SPShortName + "</option>";
                        }
                        SPNoStr += "</select>";
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
        protected string RMLOCNo1Str = "";
        protected string ContractType = "";
        protected string SPNoStr = "";

        private string createList(string LOCNo1, string LOCNo2, string LOCNo3, string LOCNo4, string CustName, string ContractType, string MinMonth, string MaxMonth, string SPNo)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            //sb.Append("<th>勾选</th>");
            sb.Append("<td width='4%'><input type=\"checkbox\" class=\"input-text size-MINI\" id=\"checkall\" /></td>");
            sb.Append("<th width='4%'>序号</th>");
            sb.Append("<th width='10%'>客户名称</th>");
            sb.Append("<th width='8%'>合同编号</th>");
            sb.Append("<th width='7%'>合同类型</th>");
            sb.Append("<th width='6%'>月份</th>");
            sb.Append("<th width='11%'>房间号/广告位</th>");
            sb.Append("<th width='12%'>工位号</th>");
            sb.Append("<th width='7%'>租金</th>");
            sb.Append("<th width='7%'>管理/空调/服务</th>");
            sb.Append("<th width='7%'>水量（吨）</th>");
            sb.Append("<th width='7%'>电量（度）</th>");
            sb.Append("<th width='7%'>超额（度）</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            
            if (LOCNo1 == "") LOCNo1 = "%";
            if (LOCNo2 == "") LOCNo2 = "%";
            if (LOCNo3 == "") LOCNo3 = "%";
            if (LOCNo4 == "") LOCNo4 = "%";
            if (CustName == "") CustName = "%";
            if (ContractType == "") ContractType = "%";
            if (SPNo == "") SPNo = "%";

            if (MinMonth == "") MinMonth = GetDate().AddYears(-10).ToString("yyyy-MM");
            if (MaxMonth == "") MaxMonth = GetDate().AddYears(10).ToString("yyyy-MM");

            int r = 1;
            sb.Append("<tbody id=\"tbody\">");
            DataTable dt = GetUnGenContract(LOCNo1, LOCNo2, LOCNo3, LOCNo4, CustName, ContractType,MinMonth,MaxMonth, SPNo);
            foreach (DataRow dr in dt.Rows)
            {
                string id = Guid.NewGuid().ToString().Substring(0,6);

                sb.Append("<tr class=\"text-c\">");
                sb.Append("<td><input type=\"checkbox\" class=\"input-text size-MINI\" value=\"" + id + "\" name=\"chk\" />" +
                    "<input id=\"ID" + id + "\" value=\"" + dr["ContractID"].ToString() + "\" type=\"hidden\" />" +
                    "<input id=\"Month" + id + "\" value=\"" + dr["FeeMonth"].ToString() + "\" type=\"hidden\" />" +
                    "</td>");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + dr["CustName"].ToString() + "</td>");
                sb.Append("<td>" + dr["ContractNo"].ToString() + "</td>");
                sb.Append("<td>" + dr["ContractTypeName"].ToString() + "</td>");
                sb.Append("<td>" + dr["FeeMonth"].ToString() + "</td>");
                sb.Append("<td>" + dr["RMID"].ToString().Replace(";","<br />") + "</td>");
                sb.Append("<td>" + dr["WPNo"].ToString().Replace(";", "<br />") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(dr["FeeAmount"].ToString()).ToString("0.####") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(dr["FeeAmount_PA"].ToString()).ToString("0.####") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(dr["FeeAmount_WM"].ToString()).ToString("0.####") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(dr["FeeAmount_AM"].ToString()).ToString("0.####") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(dr["FeeAmount_OAM"].ToString()).ToString("0.####") + "</td>");
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
            else if (jp.getValue("Type") == "getvalue")
                result = getvalueaction(jp);
            return result;
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMLOCNo1"), jp.getValue("RMLOCNo2"), jp.getValue("RMLOCNo3"), jp.getValue("RMLOCNo4"),
                jp.getValue("CustName"), jp.getValue("ContractType"), jp.getValue("MinMonth"), jp.getValue("MaxMonth"), jp.getValue("SPNo"))));
            return collection.ToString();
        }
        private string genorderaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            string TabName = "Tmp" + getRandom();
            obj.ExecuteNonQuery("CREATE TABLE " + TabName + "(ContractID NVARCHAR(36),FeeMonth NVARCHAR(7))");
            foreach (string str in jp.getValue("ContractID").Split(';'))
            {
                if (str == "") continue;
                obj.ExecuteNonQuery("INSERT INTO " + TabName + " VALUES('" + str.Split(':')[0] + "','" + str.Split(':')[1] + "')");
            }

            string InfoBar = GenOrderFromContract(TabName, user.Entity.UserName);
            if (InfoBar != "")
            {
                flag = "2";
                collection.Add(new JsonStringValue("InfoBar", InfoBar));
            }

            collection.Add(new JsonStringValue("type", "genorder"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("RMLOCNo1"), jp.getValue("RMLOCNo2"), jp.getValue("RMLOCNo3"), jp.getValue("RMLOCNo4"),
                jp.getValue("CustName"), jp.getValue("ContractType"), jp.getValue("MinMonth"), jp.getValue("MaxMonth"), jp.getValue("SPNo"))));
            return collection.ToString();
        }
        private string getvalueaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string subtype = "";
            try
            {
                int row = 0;
                Business.Base.BusinessLocation bt = new Business.Base.BusinessLocation();
                foreach (Entity.Base.EntityLocation it in bt.GetListQuery(string.Empty, string.Empty, jp.getValue("parent")))
                {
                    subtype += it.LOCNo + ":" + it.LOCName + ";";
                    row++;
                }

                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));
                collection.Add(new JsonStringValue("child", jp.getValue("child")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getvalue"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        


        /// </summary>
        /// 获取在租费用信息 
        /// </summary>
        public DataTable GetUnGenContract(string LOCNo1, string LOCNo2, string LOCNo3, string LOCNo4, string CustName, string ContractType, string MinMonth, string MaxMonth, string SPNo)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            DataSet ds = new DataSet();
            try
            {
                con = Data.Conn();
                cmd = new SqlCommand("GetUnGenContract", con);
                cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] parameter = new SqlParameter[] { 
                    new SqlParameter("@LOCNo1",SqlDbType.NVarChar,30),
                    new SqlParameter("@LOCNo2",SqlDbType.NVarChar,30),
                    new SqlParameter("@LOCNo3",SqlDbType.NVarChar,30),
                    new SqlParameter("@LOCNo4",SqlDbType.NVarChar,30),
                    new SqlParameter("@CustName",SqlDbType.NVarChar,50),
                    new SqlParameter("@ContractType",SqlDbType.NVarChar,30),
                    new SqlParameter("@MinMonth",SqlDbType.NVarChar,7),
                    new SqlParameter("@MaxMonth",SqlDbType.NVarChar,7),
                    new SqlParameter("@SPNo",SqlDbType.NVarChar,30)
                };
                parameter[0].Value = LOCNo1;
                parameter[1].Value = LOCNo2;
                parameter[2].Value = LOCNo3;
                parameter[3].Value = LOCNo4;
                parameter[4].Value = CustName;
                parameter[5].Value = ContractType;
                parameter[6].Value = MinMonth;
                parameter[7].Value = MaxMonth;
                parameter[8].Value = SPNo;
                cmd.Parameters.AddRange(parameter);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
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
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Dispose();
            }
        }

        
        /// </summary>
        /// 生成订单
        /// </summary>
        public string GenOrderFromContract(string TableName, string UserName)
        {

            string InfoMsg = "";
            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = Data.Conn();
                command = new SqlCommand("GenOrderFromContract", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@TableName", SqlDbType.NVarChar, 36).Value = TableName;
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