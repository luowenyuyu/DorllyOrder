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

namespace project.Presentation
{
    public partial class index : AbstractPmPage, System.Web.UI.ICallbackEventHandler
    {
        protected Business.Sys.BusinessUserInfo user = new project.Business.Sys.BusinessUserInfo();
        protected string menulist="";
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie hc = getCookie("1");
                if (hc != null)
                {
                    string str = hc.Value.Replace("%3D", "=");
                    string userid = Encrypt.DecryptDES(str, "1");
                    user.load(userid);
                    if (!IsCallback)
                    {
                        createMenu(user.Entity.UserType);

                        //DateTime now = DateTime.Now.AddDays(-1);
                        //getParkRecord(now);
                        //getMonthCard(now);
                    }
                }
                else
                {
                    Response.Write("<script type='text/javascript'>window.parent.window.location.href='login.aspx';</script>");
                    return;
                }
            }
            catch
            {
                Response.Write("<script type='text/javascript'>window.parent.window.location.href='login.aspx';</script>");
                return;
            }
        }

        protected int msgnum = 0;
        protected System.Text.StringBuilder menustr = new System.Text.StringBuilder("");
        private void createMenu(string usertype)
        {
            int row = 1;
            Business.Sys.BusinessMenu menu = new Business.Sys.BusinessMenu();
            foreach (Entity.Sys.EntityMenu it in menu.GetRightMenuList("null", "sys", user.Entity.UserType))
            {
                string tit = "&#xe616;";
                if (it.MenuName == "系统管理") tit = "&#xe62e";
                else if (it.MenuName == "基础资料") tit = "&#xe63c";
                else if (it.MenuName == "表记管理") tit = "&#xe634";
                else if (it.MenuName == "退租管理") tit = "&#xe6e1";
                else if (it.MenuName == "合同管理") tit = "&#xe61a";

                menustr.Append("<dl id=\""+it.InnerEntityOID+"\">\n");
                menustr.Append("<dt><i class=\"Hui-iconfont\">" + tit + "</i> " + it.MenuName + "<b class=\"Hui-iconfont menu_dropdown-arrow\">&#xe6d6;</b></dt>\n");
                menustr.Append("<dd>\n");
                menustr.Append("<ul>\n");

                Business.Sys.BusinessMenu item = new Business.Sys.BusinessMenu();
                foreach (Entity.Sys.EntityMenu sub in item.GetRightMenuList(it.InnerEntityOID, "sys", user.Entity.UserType))
                {
                    menustr.Append("<li><a _href=\"" + sub.MenuPath + "\" href=\"javascript:void(0)\">" + sub.MenuName + "</a></li>\n");
                }

                menustr.Append("</ul>\n");
                menustr.Append("</dd>\n");
                menustr.Append("</dl>\n");

                row++;
            }
            menulist = menustr.ToString();
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
            return result;
        }


        private string getParkRecord(DateTime now)
        {
            DateTime requestTime = GetDate();

            string str = "";
            str += "effTime=" + now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00&";
            str += "expTime=" + now.ToString("yyyy-MM-dd") + " 00:00:00&";
            str += "parkingCode=" + "ydt_2402&";
            str += "requestTime=" + requestTime.ToString("yyyy-MM-dd HH:mm:ss") + "&";
            str += "retLimit=" + "10000&";
            str += "6Z24Qi90C7EPz7G5";
            string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();

            JsonObjectCollection collection = new JsonObjectCollection();
            collection.Add(new JsonStringValue("parkingCode", "ydt_2402"));
            collection.Add(new JsonStringValue("requestTime", requestTime.ToString("yyyy-MM-dd HH:mm:ss")));
            collection.Add(new JsonStringValue("signature", signature));
            collection.Add(new JsonStringValue("effTime", now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00"));
            collection.Add(new JsonStringValue("expTime", now.ToString("yyyy-MM-dd") + " 00:00:00"));
            collection.Add(new JsonStringValue("retLimit", "10000"));
            string str1 = Post("http://www.dmzparking.com/ydtu/external/getOutInfo.htm", collection.ToString());

            decimal amt = 0;
            int cnt = 0;
            try
            {
                Data obj = new Data();
                ParkRecordInfo ot = new ParkRecordInfo();
                object oj = JsonToObject(str1, ot);
                foreach (ParkRecord it in ((ParkRecordInfo)oj).result)
                {
                    cnt++;
                    //obj.ExecuteNonQuery("insert into Mstr_ParkRecord "+
                    //    "values(newid(),getdate(),'" + it.plateNo + "','" + it.outTime + "'," + it.amount + ",'" + it.payType + "','"+it.chargePaidNo+"'," + it.duration + ",'',0)");

                    //if (it.amount != null)
                    //{
                        if (decimal.Parse(it.actualPay) >0)
                            amt += decimal.Parse(it.actualPay);
                    //}
                }
                decimal d = amt;
            }
            catch (Exception ex) { str1 = ex.ToString(); }
            return str1;
        }

        private string getMonthCard(DateTime now)
        {
            DateTime requestTime = GetDate();

            string str = "";
            //str += "plateNo=" + "&";
            str += "endTime=" + now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00&";
            str += "parkingCode=" + "ydt_2402&";
            str += "requestTime=" + requestTime.ToString("yyyy-MM-dd HH:mm:ss") + "&";
            str += "startTime=" + now.AddDays(-2).ToString("yyyy-MM-dd") + " 00:00:00&";
            str += "6Z24Qi90C7EPz7G5";
            string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();

            JsonObjectCollection collection = new JsonObjectCollection();
            collection.Add(new JsonStringValue("plateNo", ""));
            collection.Add(new JsonStringValue("parkingCode", "ydt_2402"));
            collection.Add(new JsonStringValue("requestTime", requestTime.ToString("yyyy-MM-dd HH:mm:ss")));
            collection.Add(new JsonStringValue("startTime", now.AddDays(-2).ToString("yyyy-MM-dd") + " 00:00:00"));
            collection.Add(new JsonStringValue("endTime", now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00"));
            collection.Add(new JsonStringValue("signature", signature));
            string str1 = Post("http://www.dmzparking.com/ydtu/external/getMonthCarRecord.htm", collection.ToString());

            try
            {
                decimal amt = 0;
                Data obj = new Data();
                MonthCardInfo ot = new MonthCardInfo();
                object oj = JsonToObject(str1, ot);
                foreach (MonthCard it in ((MonthCardInfo)oj).result)
                {
                    //obj.ExecuteNonQuery("insert into Mstr_MonthCard(RowPointer,CreateDate,MCPlateNo,MCAmount,MCCreateTime,MCEndTime,MCChargeType,RefRP,MCStatus) " +
                    //    "values(newid(),getdate(),'" + it.carPlate + "'," + it.amount + ",'" + it.createTime + "','" + it.endDate + "'," + it.chargeType + ",'',0)");

                    //if (decimal.Parse(it.amount) > 0)
                        amt += decimal.Parse(it.amount);
                }
                decimal d = amt;
            }
            catch (Exception ex) { str1 = ex.ToString(); }
            return str1;
        }

    }

    public class ParkRecordInfo
    {
        string _errorCode;
        string _errorMsg;
        ParkRecord[] _result;
        public ParkRecordInfo() { }

        public string errorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
        public string errorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value; }
        }
        public ParkRecord[] result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
    public class ParkRecord
    {
        string _plateNo;
        string _outTime;
        string _needPay;
        string _actualPay;
        string _beforePay;
        string _payType;
        string _carNature;
        string _duration;
        public ParkRecord() { }

        public string plateNo
        {
            get { return _plateNo; }
            set { _plateNo = value; }
        }
        public string outTime
        {
            get { return _outTime; }
            set { _outTime = value; }
        }
        public string needPay
        {
            get { return _needPay; }
            set { _needPay = value; }
        }
        public string actualPay
        {
            get { return _actualPay; }
            set { _actualPay = value; }
        }
        public string beforePay
        {
            get { return _beforePay; }
            set { _beforePay = value; }
        }
        public string payType
        {
            get { return _payType; }
            set { _payType = value; }
        }
        public string carNature
        {
            get { return _carNature; }
            set { _carNature = value; }
        }
        public string duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
    }
    public class MonthCardInfo
    {
        string _errorCode;
        string _errorMsg;
        MonthCard[] _result;
        public MonthCardInfo() { }

        public string errorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
        public string errorMsg
        {
            get { return _errorMsg; }
            set { _errorMsg = value; }
        }
        public MonthCard[] result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
    public class MonthCard
    {
        string _carPlate;
        string _amount;
        string _createTime;
        string _endDate;
        string _chargeType;
        public MonthCard() { }

        public string carPlate
        {
            get { return _carPlate; }
            set { _carPlate = value; }
        }
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string createTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public string endDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        public string chargeType
        {
            get { return _chargeType; }
            set { _chargeType = value; }
        }
    }
}