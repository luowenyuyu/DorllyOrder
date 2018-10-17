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
    public partial class CRDisposit : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Op/CRDisposit.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Op/CRDisposit.aspx'";
                            DataTable dt = obj.PopulateDataSet(sqlstr).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                string rightCode = dt.Rows[0]["RightCode"].ToString();
                                if (rightCode.IndexOf("refund") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"refund()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe66b;</i> 退押金</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"refund()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe66b;</i> 退押金</a>&nbsp;&nbsp;";
                        }

                        list = createList("","", "0", 1);
                        date = GetDate().ToString("yyyy-MM-dd");
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
        private string createList(string MinDate, string MaxDate, string PayState, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='10%'>支付单号</th>");
            sb.Append("<th width='12%'>会议室名称</th>");
            sb.Append("<th width='13%'>申请客户</th>");
            sb.Append("<th width='10%'>申请人</th>");
            sb.Append("<th width='10%'>租赁时间</th>");
            sb.Append("<th width='10%'>支付金额</th>");
            sb.Append("<th width='10%'>租金</th>");
            sb.Append("<th width='10%'>押金</th>");
            sb.Append("<th width='10%'>状态</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessSetting setting=new Business.Base.BusinessSetting();
            setting.load("CurrParkNo");
            
            ButlerSrv.AppService service=new ButlerSrv.AppService();
            string str1 = service.GetCRDispositList(setting.Entity.StringValue, MinDate, MaxDate, PayState, page, 15, "5218E3ED752A49D4");

            PaymentInfo ot = new PaymentInfo();
            object oj = JsonToObject(str1, ot);

            foreach (Entity.Op.EntityPayment it in ((PaymentInfo)oj).list)
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.RowPointer + "\">");
                sb.Append("<td style=\"text-align:center;\">" + r.ToString() + "</td>");
                sb.Append("<td>" + it.PayNo + "</td>");
                sb.Append("<td>" + it.CRName + "</td>");
                sb.Append("<td>" + it.ApplyCustName + "</td>");
                sb.Append("<td>" + it.ApplyUserName + "</td>");
                sb.Append("<td>" + it.ReservedTime + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it.Amount).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it.RentAmount).ToString("0.##") + "</td>");
                sb.Append("<td>" + ParseDecimalForString(it.Disposit).ToString("0.##") + "</td>");
                sb.Append("<td><span class=\"label " + (it.ARState == "1" ? "label-success" : "") + " radius\">" + it.ARStateName + "</span></td>");

                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append(Paginat(service.GetCRDispositCount(setting.Entity.StringValue, MinDate, MaxDate, PayState, "5218E3ED752A49D4"), pageSize, page, 7));
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
            if (jp.getValue("Type") == "refund")
                result = refundaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            return result;
        }
        private string refundaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessSetting setting=new Business.Base.BusinessSetting();
                setting.load("CurrParkNo");

                ButlerSrv.AppService service=new ButlerSrv.AppService();
                string str1 = service.CRDispositLoad(jp.getValue("id"), "5218E3ED752A49D4");

                Entity.Op.EntityPayment ot = new project.Entity.Op.EntityPayment();
                object oj = JsonToObject(str1, ot);
                Entity.Op.EntityPayment bc = (Entity.Op.EntityPayment)oj;

                if (bc.ARState != "0")
                {
                    flag = "3";
                    collection.Add(new JsonStringValue("info", "客服尚未申请退押金，当前状态不允许退押金！"));
                }
                else
                {
                    RefundReturn rt = Refund(bc.TransactionId, bc.PayNo,
                        (ParseDecimalForString(bc.Amount) * 100).ToString("0"),
                        (ParseDecimalForString(bc.RefundAmount) * 100).ToString("0"));
                    if (rt.ResultCode == "SUCCESS")
                    {
                        string InfoMsg = service.Refund_Disposrit(jp.getValue("id"), "5218E3ED752A49D4");
                        if (InfoMsg != "")
                        {
                            flag = "3";
                            collection.Add(new JsonStringValue("info", "退押金成功，服务申请状态修改异常！"));
                        }
                        else
                        {
                            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MinDate"), jp.getValue("MaxDate"),
                                jp.getValue("PayState"), ParseIntForString(jp.getValue("page")))));
                        }
                    }
                    else
                    {
                        flag = "3";
                        collection.Add(new JsonStringValue("info", rt.ErrCodeDes));
                    }
                }
            }
            catch(Exception ex)
            {
                flag = "3";
                collection.Add(new JsonStringValue("info", ex.ToString()));
            }

            collection.Add(new JsonStringValue("type", "refund"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MinDate"), jp.getValue("MaxDate"),
                jp.getValue("PayState"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("MinDate"), jp.getValue("MaxDate"),
                jp.getValue("PayState"), ParseIntForString(jp.getValue("page")))));
            return collection.ToString();
        }

    }

    public class PaymentInfo
    {
        Entity.Op.EntityPayment[] _list;
        public PaymentInfo() { }

        public Entity.Op.EntityPayment[] list
        {
            get { return _list; }
            set { _list = value; }
        }
    }
}

