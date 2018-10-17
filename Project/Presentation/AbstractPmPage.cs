using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Json;
using System.Text;
using System.Web;
using System.Web.Security;
using WxPayAPI;

namespace project.Presentation
{
    /// <summary>
    /// 所有页面的基类，派生于BasePage
    /// </summary>
    /// <author>tz</author>
    /// <date>2011-07-28</date>
    public class AbstractPmPage : System.Web.UI.Page
    {
        //根目录
        public static readonly string mpath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
        //错误跳转页面
        public string errorpage = "<script type='text/javascript'>window.parent.window.location.href='../../login.aspx';</script>";
        public string norightpage = "<script type='text/javascript'>location.href='../../errorpage.htm';</script>";
        public string localpath = HttpRuntime.AppDomainAppPath+"downfile\\";
        //public string localpath = "E:\\Project\\DorllyOrder\\DOWeb\\downfile\\";
        public string wopath = "http://47.89.28.145:8082/api/WebService.asmx";
                

        public int pageSize = 15;
        Data obj = new Data();
        protected virtual void Page_Load(object sender, EventArgs e)
        {
        }

        public void GotoErrorPage()
        {
            Response.Write(errorpage);
            return;
        }

        public void GotoNoRightsPage()
        {
            Response.Write(norightpage);
            return;
        }
        public static string WebRootPath
        {
            get
            {
                string result = System.Web.HttpContext.Current.Request.ApplicationPath + "/";
                return (result == "//" ? "/" : result);
            }
        }
        protected DateTime GetDate()
        {
            return DateTime.Parse(obj.PopulateDataSet("select DT=getdate()").Tables[0].Rows[0]["DT"].ToString());
        }

        protected System.Web.HttpCookie getCookie(string lg)
        {
            if (lg == "1")
                return Request.Cookies["__dorllyorder__sys__guid__"];
            else
                return null;
        }
        protected System.DateTime ParseDateForString(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return DateTime.MinValue.AddYears(1900);
            }

            return DateTime.Parse(val);
        }
        protected System.DateTime ParseSearchDateForString(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return default(DateTime);
            }

            return DateTime.Parse(val);
        }
        protected string ParseStringForDate(System.DateTime? date)
        {
            if (null == date)
                return "";
            if (DateTime.MinValue.AddYears(1900).Equals(date))
                return "";

            return ((System.DateTime)date).ToString("yyyy-MM-dd", null);
        }
        protected string ParseStringForDateTime(System.DateTime? date)
        {
            if (null == date)
                return "";
            if (DateTime.MinValue.AddYears(1900).Equals(date))
                return "";


            return ((System.DateTime)date).ToString("yyyy-MM-dd HH:mm", null);
        }
        protected decimal ParseDecimalForString(string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0;

            return decimal.Parse(val);
        }
        protected int ParseIntForString(string val)
        {
            if (string.IsNullOrEmpty(val))
                return 0;

            return Int32.Parse(val);
        }
        protected string creatFileName(string expandedName)
        {
            Random rand = new Random();
            char[] code = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789".ToCharArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int j = 0; j < 10; j++)
            {
                sb.Append(code[rand.Next(code.Length)]);
            }
            string fileName = sb.ToString() + "." + expandedName;
            return fileName;
        }
        protected string getRandom()
        {
            Random rand = new Random();
            char[] code = "1234567890".ToCharArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int j = 0; j < 6; j++)
            {
                sb.Append(code[rand.Next(code.Length)]);
            }
            return sb.ToString();
        }
        protected string getRandom(int len)
        {
            Random rand = new Random();
            char[] code = "1234567890".ToCharArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int j = 0; j < len; j++)
            {
                sb.Append(code[rand.Next(code.Length)]);
            }
            return sb.ToString();
        }

        public static bool isnull(string str)
        {
            bool flag = false;
            if ((str == null) || (str == ""))
            {
                flag = true;
            }
            return flag;
        }
        
        /// <summary>
        /// 分页区
        /// </summary>
        /// <param name="rows">总行数</param>
        /// <param name="pagerow">每页行数</param>
        /// <param name="page">第几页</param>
        /// <param name="onepagecnt">显示几个图标</param>
        /// <returns></returns>
        public string Paginat(int rows, int pagerow, int page, int onepagecnt)
        {
            StringBuilder sb = new StringBuilder("");
            int pages = int.Parse(System.Math.Ceiling(decimal.Parse(rows.ToString()) / decimal.Parse(pagerow.ToString())).ToString()); //计算页数
            int num = int.Parse(System.Math.Floor(decimal.Parse(onepagecnt.ToString()) / 2).ToString());   //计算第几个起
            int firstpage = 1;
            int endpage = 1;

            //默认第一页；非第一页，计算第几页开始
            if (page > num)
                firstpage = page - num;

            //如果超出，截止最大页数为止，不然计算截止页
            if (page + num > pages)
                endpage = pages;
            else if (pages > onepagecnt)
                endpage = firstpage + onepagecnt - 1;
            else
                endpage = pages;

            if (endpage == pages && pages > num)
                firstpage = endpage - onepagecnt + 1;
            if (firstpage <= 0)
                firstpage = 1;

            sb.Append("<div style='clear:both;'></div>");
            sb.Append("<div class='paginat'>");
            sb.Append("<ul>");
            sb.Append("<li><i>共 " + rows.ToString() + " 条数据 共 " + pages.ToString() + " 页</i></li>");
            sb.Append("<li class='nextpage'><a onclick='jump(1)'>首页</a></li>");
            if (page != 1 && page != 0)
                sb.Append("<li class='nextpage'><a onclick='jump(" + (page - 1) + ")'>上一页</a></li>");
            else if (page != 0)
                sb.Append("<li class='currentpage'>上一页</li>");
            for (int i = firstpage; i <= endpage; i++)
            {
                if (i == page)
                    sb.Append("<li class='currentpage'>" + i.ToString() + "</li>");
                else
                    sb.Append("<li><a onclick='jump(" + i.ToString() + ")'>" + i.ToString() + "</a></li>");
            }

            if (page != pages && page != 0)
                sb.Append("<li class='nextpage'><a onclick='jump(" + (page + 1) + ")'>下一页</a></li>");
            else if (page != 0)
                sb.Append("<li class='currentpage'>下一页</li>");
            sb.Append("<li class='nextpage'><a onclick='jump(" + pages + ")'>尾页</a></li>");
            sb.Append("<li><input id=\"paget\" type=\"text\" onblur=\"validInt(this.id)\" class=\"input-text size-MINI\" style=\"width:42px; height:25px;\" /><input type=\"button\" class=\"btn btn-primary jumpbtn\" onclick=\"jump($('#paget').val())\" value=\"跳转\" /></li>");
            sb.Append("</ul>");
            sb.Append("</div>");
            return sb.ToString();
        }

        public string check(string tp, string val, JsonObjectCollection collection)
        {
            Data obj = new Data();
            string flag = "1";
            try
            {
                if (tp == "user")
                {
                    DataTable dt = obj.PopulateDataSet("select top 1 * from Sys_UserInfo where UserNo='" + val + "'" + " or UserName='" + val + "' order by UserNo").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        collection.Add(new JsonStringValue("Code", dt.Rows[0]["UserNo"].ToString()));
                        collection.Add(new JsonStringValue("Name", dt.Rows[0]["UserName"].ToString()));
                    }
                    else
                    {
                        DataTable dt1 = obj.PopulateDataSet("select top 1 * from Sys_UserInfo where UserNo like '%" + val + "%'" + " or UserName like '%" + val + "%' order by UserNo").Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            collection.Add(new JsonStringValue("Code", dt1.Rows[0]["UserNo"].ToString()));
                            collection.Add(new JsonStringValue("Name", dt1.Rows[0]["UserName"].ToString()));
                        }
                        else
                        {
                            flag = "3";
                        }
                    }
                }
                else if (tp == "SRVType")
                {
                    DataTable dt = obj.PopulateDataSet("select top 1 * from Mstr_ServiceType where SRVTypeNo='" + val + "'" + " or SRVTypeName='" + val + "' order by SRVTypeNo").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        collection.Add(new JsonStringValue("Code", dt.Rows[0]["SRVTypeNo"].ToString()));
                        collection.Add(new JsonStringValue("Name", dt.Rows[0]["SRVTypeName"].ToString()));
                    }
                    else
                    {
                        DataTable dt1 = obj.PopulateDataSet("select top 1 * from Mstr_ServiceType where SRVTypeNo like '%" + val + "%'" + " or SRVTypeName like '%" + val + "%' order by SRVTypeNo").Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            collection.Add(new JsonStringValue("Code", dt1.Rows[0]["SRVTypeNo"].ToString()));
                            collection.Add(new JsonStringValue("Name", dt1.Rows[0]["SRVTypeName"].ToString()));
                        }
                        else
                        {
                            flag = "3";
                        }
                    }
                }
                else if (tp == "LOC")
                {
                    DataTable dt = obj.PopulateDataSet("select top 1 * from Mstr_Location where LOCNo='" + val + "'" + " or LOCName='" + val + "' order by LOCNo").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        collection.Add(new JsonStringValue("Code", dt.Rows[0]["LOCNo"].ToString()));
                        collection.Add(new JsonStringValue("Name", dt.Rows[0]["LOCName"].ToString()));
                    }
                    else
                    {
                        DataTable dt1 = obj.PopulateDataSet("select top 1 * from Mstr_Location where LOCNo like '%" + val + "%'" + " or LOCName like '%" + val + "%' order by LOCNo").Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            collection.Add(new JsonStringValue("Code", dt1.Rows[0]["LOCNo"].ToString()));
                            collection.Add(new JsonStringValue("Name", dt1.Rows[0]["LOCName"].ToString()));
                        }
                        else
                        {
                            flag = "3";
                        }
                    }
                }
                else if (tp == "Cust")
                {
                    DataTable dt = obj.PopulateDataSet("select top 1 * from Mstr_Customer where CustNo='" + val + "'" + " or CustName='" + val + "' order by CustNo").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        collection.Add(new JsonStringValue("Code", dt.Rows[0]["CustNo"].ToString()));
                        collection.Add(new JsonStringValue("Name", dt.Rows[0]["CustName"].ToString()));
                    }
                    else
                    {
                        DataTable dt1 = obj.PopulateDataSet("select top 1 * from Mstr_Customer where CustNo like '%" + val + "%'" + " or CustName like '%" + val + "%' order by CustNo").Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            collection.Add(new JsonStringValue("Code", dt1.Rows[0]["CustNo"].ToString()));
                            collection.Add(new JsonStringValue("Name", dt1.Rows[0]["CustName"].ToString()));
                        }
                        else
                        {
                            flag = "3";
                        }
                    }
                }
                else if (tp == "RM")
                {
                    DataTable dt = obj.PopulateDataSet("select top 1 * from Mstr_Room where RMID='" + val + "' order by RMID").Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        collection.Add(new JsonStringValue("Code", dt.Rows[0]["RMID"].ToString()));
                        collection.Add(new JsonStringValue("Name", dt.Rows[0]["RMID"].ToString()));
                    }
                    else
                    {
                        DataTable dt1 = obj.PopulateDataSet("select top 1 * from Mstr_Room where RMID like '%" + val + "%' order by RMID").Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            collection.Add(new JsonStringValue("Code", dt1.Rows[0]["RMID"].ToString()));
                            collection.Add(new JsonStringValue("Name", dt1.Rows[0]["RMID"].ToString()));
                        }
                        else
                        {
                            flag = "3";
                        }
                    }
                }
            }
            catch { flag = "2"; }

            return flag;
        }

        public void CheckRight(Entity.Sys.EntityUserInfo user, string PathName)
        {
            if (user.UserType.ToUpper() != "ADMIN")
            {
                Data obj = new Data();
                try
                {
                    string sqlstr = "select a.MenuId from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID where a.UserType='" + user.UserType +
                        "' and menupath='" + PathName + "'";
                    DataTable dt = obj.PopulateDataSet(sqlstr).Tables[0];

                    if (dt.Rows.Count == 0)
                        GotoNoRightsPage();
                }
                catch { }
            }
        }


        public RefundReturn Refund(string transaction_id, string out_trade_no, string total_fee, string refund_fee)
        {
            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", out_trade_no);
            }

            data.SetValue("total_fee", int.Parse(total_fee));//订单总金额
            data.SetValue("refund_fee", int.Parse(refund_fee));//退款金额
            data.SetValue("out_refund_no", WxPayApi.GenerateOutTradeNo());//随机生成商户退款单号
            data.SetValue("op_user_id", WxPayConfig.MCHID);//操作员，默认为商户号
            WxPayData result = WxPayApi.Refund(data);//提交退款申请给API，接收返回数据

            RefundReturn refreturn = new RefundReturn();
            string result_code = result.GetValue("result_code").ToString();
            if (result_code == "SUCCESS")
            {
                refreturn.ResultCode = result_code;
                refreturn.ErrCode = "";
                refreturn.ErrCodeDes = "";
            }
            else
            {
                refreturn.ResultCode = result_code;
                refreturn.ErrCode = result.GetValue("err_code").ToString();
                refreturn.ErrCodeDes = result.GetValue("err_code_des").ToString();
            }

            return refreturn;
        }


        public string Post(string Url, string jsonParas)
        {
            string strURL = Url;

            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //设置参数，并进行URL编码
            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);    

            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流  
            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流

            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            Stream s = response.GetResponseStream();

            Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();

            return postContent;//返回Json数据
        }

        public static object JsonToObject(string jsonString, object obj)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
            System.IO.MemoryStream mStream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }
    }

    /// <summary>
    /// 用以切割以：；形式存在的字符串
    /// </summary>
    public class JsonArrayParse
    {
        private string _strId;
        private char _ch1=':';
        private char _ch2=';';
        public JsonArrayParse(string strId)
        {
            this._strId = strId;
        }
        public JsonArrayParse(string strId,char ch1,char ch2)
        {
            this._strId = strId;
            this._ch1 = ch1;
            this._ch2 = ch2;
        }

        public string getValue(string id)
        {
            foreach (string it in _strId.Split(_ch2))
            {
                string[] s = it.Split(_ch1);
                if (s[0].ToString() == id)
                    return s[1].ToString().Replace("[~&*!^%]", ":").Replace("[^%$#*]", ";");
            }
            return "";
        }
    }

    public class U8Return
    {
        public U8Return() { }

        public U8Msg[] rtn { get; set; }
    }
    public class U8Msg
    {
        public U8Msg() { }

        public string ydate{get; set;}
        public string flag { get; set; }
        public string sbillno { get; set; }
        public string msg { get; set; }
    }


    public class RefundReturn
    {
        public RefundReturn() { }

        public string ResultCode { get; set; }
        public string ErrCode { get; set; }
        public string ErrCodeDes { get; set; }
    }

    
    public class SycnResourceStatus
    {
        public SycnResourceStatus() { }

        public Nullable<int> SysID { get; set; }
        public string ResourceID { get; set; }
        public decimal RentArea { get; set; }
        public string BusinessID { get; set; }
        public Nullable<int> BusinessType { get; set; }
        public Nullable<System.DateTime> RentBeginTime { get; set; }
        public Nullable<System.DateTime> RentEndTime { get; set; }
        public string CustLongName { get; set; }
        public string CustShortName { get; set; }
        public string CustTel { get; set; }
        public string ReserveDept { get; set; }
        public string ReserveName { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> Enable { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }

}
