using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// ImportService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{

    Data obj = new Data();
    public string ReadoutImgPath { get { return Server.MapPath("~/upload/meter/"); } }
    public Service()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int ExecuteNonQuery(string cmdText)
    {
        return obj.ExecuteNonQuery(cmdText);
    }

    [WebMethod]
    public DataSet PopulateDataSet(string cmdText)
    {
        return obj.PopulateDataSet(cmdText);
    }

    [WebMethod]
    public DateTime GetServerDateTime()
    {
        return GetDate();
    }


    //艾众管家
    [WebMethod]
    public string GenOrderFromParkinLot(string PRTableName, string MCTableName, string Guid, string Date)
    {
        string InfoMsg = "";
        SqlConnection con = null;
        SqlCommand command = null;
        try
        {
            con = Data.Conn();
            command = new SqlCommand("GenOrderFromParkinLot", con);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@PRTableName", SqlDbType.NVarChar, 36).Value = PRTableName;
            command.Parameters.Add("@MCTableName", SqlDbType.NVarChar, 30).Value = MCTableName;
            command.Parameters.Add("@Guid", SqlDbType.NVarChar, 50).Value = Guid;
            command.Parameters.Add("@Date", SqlDbType.NVarChar, 30).Value = Date;
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

    [WebMethod]
    public string GenOrderFromConferenceRepair(string ResourceNo, string ResourceName, decimal amount, string Remark, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return "";

        string InfoMsg = "";
        try
        {
            string prefix = GetDate().ToString("yyyyMM");
            string OrderNo = "";
            DataTable dt = obj.PopulateDataSet("select top 1 OrderNo from Op_OrderHeader where OrderNo like '" + prefix + "%' order by OrderNo desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                try
                {
                    long num = long.Parse(dt.Rows[0]["OrderNo"].ToString());
                    OrderNo = (num + 1).ToString();
                }
                catch
                {
                    OrderNo = prefix + "00001";
                }
            }
            else
            {
                OrderNo = prefix + "00001";
            }
            DateTime now = GetDate();
            string id = System.Guid.NewGuid().ToString();
            project.Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
            bc.Entity.RowPointer = id;
            bc.Entity.OrderNo = OrderNo;
            bc.Entity.OrderType = "10";
            bc.Entity.CustNo = "22222";
            bc.Entity.OrderTime = now.AddDays(-now.Day + 1);
            bc.Entity.DaysofMonth = ParseDateForString(now.AddMonths(1).ToString("yyyy-MM") + "-01").AddDays(-1).Day;
            bc.Entity.ARDate = now;
            bc.Entity.ARAmount = amount;
            bc.Entity.ReduceAmount = 0;
            bc.Entity.PaidinAmount = 0;
            bc.Entity.Remark = Remark;

            bc.Entity.OrderCreator = "艾众管家";
            bc.Entity.OrderCreateDate = now;
            bc.Entity.OrderLastReviser = "艾众管家";
            bc.Entity.OrderLastRevisedDate = now;
            bc.Entity.OrderStatus = "0";
            bc.Save("insert");

            project.Business.Base.BusinessService bs = new project.Business.Base.BusinessService();
            bs.load("HYSZJ-59");

            project.Business.Op.BusinessOrderDetail detail = new project.Business.Op.BusinessOrderDetail();
            detail.Entity.RefRP = id;
            detail.Entity.ODSRVTypeNo1 = bs.Entity.SRVTypeNo1;
            detail.Entity.ODSRVTypeNo2 = bs.Entity.SRVTypeNo2;
            detail.Entity.ODSRVNo = bs.Entity.SRVNo;
            detail.Entity.ODContractSPNo = bs.Entity.SRVSPNo;
            detail.Entity.ResourceNo = ResourceNo;
            detail.Entity.ResourceName = ResourceName;
            detail.Entity.ODFeeStartDate = now;
            detail.Entity.ODFeeEndDate = now;
            detail.Entity.BillingDays = 1;
            detail.Entity.ODQTY = 1;
            detail.Entity.ODUnit = "";
            detail.Entity.ODUnitPrice = amount;
            detail.Entity.ODARAmount = amount;
            detail.Entity.ODCANo = bs.Entity.CANo;
            detail.Entity.ODCreator = "艾众管家";
            detail.Entity.ODCreateDate = now;
            detail.Entity.ODLastReviser = "艾众管家";
            detail.Entity.ODLastRevisedDate = now;
            detail.Entity.ODTaxRate = bs.Entity.SRVTaxRate;
            detail.Entity.ODTaxAmount = amount - Math.Round(amount / (1 + bs.Entity.SRVTaxRate), 2);
            detail.Save();

        }
        catch (Exception ex)
        {
            InfoMsg = ex.Message;
        }

        return InfoMsg;
    }

    [WebMethod]
    public string GenOrderFromCompRepair(string CustNo, decimal amount, string Remark, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return "";

        string InfoMsg = "";
        try
        {
            string prefix = GetDate().ToString("yyyyMM");
            string OrderNo = "";
            DataTable dt = obj.PopulateDataSet("select top 1 OrderNo from Op_OrderHeader where OrderNo like '" + prefix + "%' order by OrderNo desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                try
                {
                    long num = long.Parse(dt.Rows[0]["OrderNo"].ToString());
                    OrderNo = (num + 1).ToString();
                }
                catch
                {
                    OrderNo = prefix + "00001";
                }
            }
            else
            {
                OrderNo = prefix + "00001";
            }
            DateTime now = GetDate();
            string id = System.Guid.NewGuid().ToString();
            project.Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
            bc.Entity.RowPointer = id;
            bc.Entity.OrderNo = OrderNo;
            bc.Entity.OrderType = "11";
            bc.Entity.CustNo = CustNo;
            bc.Entity.OrderTime = now.AddDays(-now.Day + 1);
            bc.Entity.DaysofMonth = ParseDateForString(now.AddMonths(1).ToString("yyyy-MM") + "-01").AddDays(-1).Day;
            bc.Entity.ARDate = now;
            bc.Entity.ARAmount = amount;
            bc.Entity.ReduceAmount = 0;
            bc.Entity.PaidinAmount = 0;
            bc.Entity.Remark = Remark;

            bc.Entity.OrderCreator = "艾众管家";
            bc.Entity.OrderCreateDate = now;
            bc.Entity.OrderLastReviser = "艾众管家";
            bc.Entity.OrderLastRevisedDate = now;
            bc.Entity.OrderStatus = "0";
            bc.Save("insert");

            project.Business.Base.BusinessService bs = new project.Business.Base.BusinessService();
            bs.load("WXF-60");

            project.Business.Op.BusinessOrderDetail detail = new project.Business.Op.BusinessOrderDetail();
            detail.Entity.RefRP = id;
            detail.Entity.ODSRVTypeNo1 = bs.Entity.SRVTypeNo1;
            detail.Entity.ODSRVTypeNo2 = bs.Entity.SRVTypeNo2;
            detail.Entity.ODSRVNo = bs.Entity.SRVNo;
            detail.Entity.ODContractSPNo = bs.Entity.SRVSPNo;
            detail.Entity.ResourceNo = "";
            detail.Entity.ResourceName = "";
            detail.Entity.ODFeeStartDate = now;
            detail.Entity.ODFeeEndDate = now;
            detail.Entity.BillingDays = 1;
            detail.Entity.ODQTY = 1;
            detail.Entity.ODUnit = "";
            detail.Entity.ODUnitPrice = amount;
            detail.Entity.ODARAmount = amount;
            detail.Entity.ODCANo = bs.Entity.CANo;
            detail.Entity.ODCreator = "艾众管家";
            detail.Entity.ODCreateDate = now;
            detail.Entity.ODLastReviser = "艾众管家";
            detail.Entity.ODLastRevisedDate = now;
            detail.Entity.ODTaxRate = bs.Entity.SRVTaxRate;
            detail.Entity.ODTaxAmount = amount - Math.Round(amount / (1 + bs.Entity.SRVTaxRate), 2);
            detail.Save();

        }
        catch (Exception ex)
        {
            InfoMsg = ex.Message;
        }

        return InfoMsg;
    }

    [WebMethod]
    public string GenOrderFromCompRepairForWO(string CustNo, decimal amount, string Remark, string KEY, out string OrderNo)
    {
        OrderNo = "";
        if (KEY != "6E5F0C851FD4") return "";

        string InfoMsg = "";
        try
        {
            string prefix = GetDate().ToString("yyyyMM");
            DataTable dt = obj.PopulateDataSet("select top 1 OrderNo from Op_OrderHeader where OrderNo like '" + prefix + "%' order by OrderNo desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                try
                {
                    long num = long.Parse(dt.Rows[0]["OrderNo"].ToString());
                    OrderNo = (num + 1).ToString();
                }
                catch
                {
                    OrderNo = prefix + "00001";
                }
            }
            else
            {
                OrderNo = prefix + "00001";
            }
            DateTime now = GetDate();
            string id = System.Guid.NewGuid().ToString();
            project.Business.Op.BusinessOrderHeader bc = new project.Business.Op.BusinessOrderHeader();
            bc.Entity.RowPointer = id;
            bc.Entity.OrderNo = OrderNo;
            bc.Entity.OrderType = "11";
            bc.Entity.CustNo = CustNo;
            bc.Entity.OrderTime = now.AddDays(-now.Day + 1);
            bc.Entity.DaysofMonth = ParseDateForString(now.AddMonths(1).ToString("yyyy-MM") + "-01").AddDays(-1).Day;
            bc.Entity.ARDate = now;
            bc.Entity.ARAmount = amount;
            bc.Entity.ReduceAmount = 0;
            bc.Entity.PaidinAmount = 0;
            bc.Entity.Remark = Remark;

            bc.Entity.OrderCreator = "工单系统";
            bc.Entity.OrderCreateDate = now;
            bc.Entity.OrderLastReviser = "工单系统";
            bc.Entity.OrderLastRevisedDate = now;
            bc.Entity.OrderStatus = "0";
            bc.Save("insert");

            project.Business.Base.BusinessService bs = new project.Business.Base.BusinessService();
            bs.load("WXF-60");

            project.Business.Op.BusinessOrderDetail detail = new project.Business.Op.BusinessOrderDetail();
            detail.Entity.RefRP = id;
            detail.Entity.ODSRVTypeNo1 = bs.Entity.SRVTypeNo1;
            detail.Entity.ODSRVTypeNo2 = bs.Entity.SRVTypeNo2;
            detail.Entity.ODSRVNo = bs.Entity.SRVNo;
            detail.Entity.ODContractSPNo = bs.Entity.SRVSPNo;
            detail.Entity.ResourceNo = "";
            detail.Entity.ResourceName = "";
            detail.Entity.ODFeeStartDate = now;
            detail.Entity.ODFeeEndDate = now;
            detail.Entity.BillingDays = 1;
            detail.Entity.ODQTY = 1;
            detail.Entity.ODUnit = "";
            detail.Entity.ODUnitPrice = amount;
            detail.Entity.ODARAmount = amount;
            detail.Entity.ODCANo = bs.Entity.CANo;
            detail.Entity.ODCreator = "工单系统";
            detail.Entity.ODCreateDate = now;
            detail.Entity.ODLastReviser = "工单系统";
            detail.Entity.ODLastRevisedDate = now;
            detail.Entity.ODTaxRate = bs.Entity.SRVTaxRate;
            detail.Entity.ODTaxAmount = amount - Math.Round(amount / (1 + bs.Entity.SRVTaxRate), 2);
            detail.Save();
        }
        catch (Exception ex)
        {
            InfoMsg = ex.Message;
        }

        return InfoMsg;
    }
    /// <summary>
    /// 账单详情 
    /// </summary>
    /// <param name="custno"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    [WebMethod]
    public string GetRentListInfo(string custno, string month)
    {
        #region 查询sql语句

        /*
            select
            b.RowPointer as OrderID,		--订单主键
            b.OrderNo,						--订单编号
            b.OrderTime,					--收租月份
            b.OrderType,					--订单类型编号
            c.OrderTypeName,				--订单类型名称
            b.ARAmount as TotalAmount,		--订单总额
            a.ODSRVNo as FeeNo,				--费用编号
            d.SRVName as FeeName,			--费用名称
            a.ODARAmount as FeeAmount,		--费用总额
            a.ODQTY as Quantity,			--费用数量
            a.ODUnitPrice as UnitPrice,		--费用单价
            a.ODContractSPNo as ProviderNo,	--服务商编号
            e.SPName as ProviderName,		--服务商名称
            e.SPBank as Bank,				--服务商银行卡所属银行名称
            e.SPBankAccount as Account,		--服务商银行卡账号
            e.SPTel as Tel					--服务商联系电话
            from Op_OrderDetail a 
            left join Op_OrderHeader b on a.RefRP=b.RowPointer
            left join Mstr_OrderType c on b.OrderType=c.OrderTypeNo
            left join Mstr_Service d on a.ODSRVNo=d.SRVNo
            left join Mstr_ServiceProvider e on a.ODContractSPNo=e.SPNo
            where b.OrderStatus='1'
            and b.CustNo='00058'
            and CONVERT(char(7),b.OrderTime,121)='2018-11'  
            
            /////////string sql = string.Format(@"
                            select
                            b.RowPointer as OrderID,		--订单主键
                            b.OrderNo,						--订单编号
                            b.OrderTime,					--收租月份
                            b.OrderType,					--订单类型编号
                            c.OrderTypeName,				--订单类型名称
                            b.ARAmount as TotalAmount,		--订单总额
                            a.ResourceNo,                   --资源编号
                            a.ODSRVNo as FeeNo,				--费用编号
                            d.SRVName as FeeName,			--费用名称
                            a.ODARAmount as FeeAmount,		--费用总额
                            a.ODQTY as Quantity,			--费用数量
                            a.ODUnitPrice as UnitPrice,		--费用单价
                            a.ODContractSPNo as ProviderNo,	--服务商编号
                            e.SPName as ProviderName,		--服务商名称
                            e.SPBank as Bank,				--服务商银行卡所属银行名称
                            e.SPBankAccount as Account,		--服务商银行卡账号
                            e.SPTel as Tel					--服务商联系电话
                            from Op_OrderDetail a 
                            left join Op_OrderHeader b on a.RefRP=b.RowPointer
                            left join Mstr_OrderType c on b.OrderType=c.OrderTypeNo
                            left join Mstr_Service d on a.ODSRVNo=d.SRVNo
                            left join Mstr_ServiceProvider e on a.ODContractSPNo=e.SPNo
                            where b.OrderStatus>='1'
                            and e.SPName not like '%福中达%'
                            and b.CustNo='{0}'
                            and CONVERT(char(7),b.OrderTime,121)='{1}'", custno, month);

        /////////
        /// string sql = string.Format(@"
                            select
                            a.RowPointer as OrderID,		--订单主键
                            a.OrderNo,			            --订单编号
                            a.OrderTime,			        --收租月份
                            a.OrderStatus AS Status,		--订单状态
                            a.OrderType,			        --订单类型编号
                            c.OrderTypeName,			    --订单类型名称
                            a.ARAmount as TotalAmount,		--订单总额
                            b.ResourceNo, 			        --资源编号
                            b.ODSRVNo as FeeNo,		        --费用编号
                            d.SRVName as FeeName,		    --费用名称
                            b.ODARAmount as FeeAmount,	    --费用总额
                            b.ODQTY as Quantity,		    --费用数量
                            b.ODUnitPrice as UnitPrice,		--费用单价
                            b.ODContractSPNo as ProviderNo,	--服务商编号
                            e.SPName as ProviderName,		--服务商名称
                            e.SPBank as Bank,			    --服务商银行卡所属银行名称
                            e.SPBankAccount as Account,		--服务商银行卡账号
                            e.SPTel as Tel			        --服务商联系电话
                            from Op_OrderHeader a 
                            left join Op_OrderDetail b on a.RowPointer=b.RefRP
                            left join Mstr_OrderType c on a.OrderType=c.OrderTypeNo
                            left join Mstr_Service d on b.ODSRVNo=d.SRVNo
                            left join Mstr_ServiceProvider e on b.ODContractSPNo=e.SPNo
                            where a.OrderStatus>='1'
                            and e.SPName not like '%福中达%'
                            and a.CustNo='{0}'
                            and CONVERT(char(7),a.OrderTime,121)='{1}'", custno, month);
         */

        #endregion
        string result = string.Empty;
        try
        {
            string sql = string.Format(@"
                            select
                            b.RowPointer as OrderID,		--订单主键
                            b.OrderNo,						--订单编号
                            b.OrderTime,					--收租月份
                            b.OrderType,					--订单类型编号
                            c.OrderTypeName,				--订单类型名称
                            b.ARAmount as TotalAmount,		--订单总额
                            b.OrderStatus AS Status,		--订单状态
                            a.ResourceNo,                   --资源编号
                            a.ODSRVNo as FeeNo,				--费用编号
                            d.SRVName as FeeName,			--费用名称
                            a.ODARAmount as FeeAmount,		--费用总额
                            a.ODQTY as Quantity,			--费用数量
                            a.ODUnitPrice as UnitPrice,		--费用单价
                            a.ODContractSPNo as ProviderNo,	--服务商编号
                            e.SPName as ProviderName,		--服务商名称
                            e.SPBank as Bank,				--服务商银行卡所属银行名称
                            e.SPBankAccount as Account,		--服务商银行卡账号
                            e.SPTel as Tel					--服务商联系电话
                            from Op_OrderDetail a 
                            left join Op_OrderHeader b on a.RefRP=b.RowPointer
                            left join Mstr_OrderType c on b.OrderType=c.OrderTypeNo
                            left join Mstr_Service d on a.ODSRVNo=d.SRVNo
                            left join Mstr_ServiceProvider e on a.ODContractSPNo=e.SPNo
                            where b.OrderStatus>='1'
                            --and e.SPName not like '%福中达%'
                            and e.SPNo='FWC-001'
                            and b.CustNo='{0}'
                            and CONVERT(char(7),b.OrderTime,121)='{1}'", custno, month);
            var dt = obj.PopulateDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                var order = dt.AsEnumerable().GroupBy(a => new
                {
                    OrderID = a.Field<string>("OrderID"),
                    OrderNo = a.Field<string>("OrderNo"),
                    OrderType = a.Field<string>("OrderType"),
                    TotalAmount = a.Field<decimal>("TotalAmount"),
                    ProviderName = a.Field<string>("ProviderName"),
                    Bank = a.Field<string>("Bank"),
                    Status = a.Field<string>("Status"),
                    Account = a.Field<string>("Account"),
                    Tel = a.Field<string>("Tel")
                }).Select(a => new
                {
                    a.Key.OrderID,
                    a.Key.OrderNo,
                    a.Key.OrderType,
                    a.Key.TotalAmount,
                    a.Key.ProviderName,
                    a.Key.Bank,
                    a.Key.Account,
                    a.Key.Tel,
                    Detail = dt.AsEnumerable().Where(b => b.Field<string>("OrderID") == a.Key.OrderID).Select(b => new
                    {
                        ResourceNo = b.Field<string>("ResourceNo"),
                        FeeNo = b.Field<string>("FeeNo"),
                        FeeName = b.Field<string>("FeeName"),
                        Quantity = b.Field<decimal>("Quantity"),
                        UnitPrice = b.Field<decimal>("UnitPrice"),
                        FeeAmount = b.Field<decimal>("FeeAmount")
                    })
                });
                result = JsonConvert.SerializeObject(new { flag = 1, data = order });
            }
            else
            {
                result = JsonConvert.SerializeObject(new { flag = 2, data = new List<object>(), info = "无相关数据！" });
            }
        }
        catch (Exception ex)
        {
            result = JsonConvert.SerializeObject(new { flag = 3, data = new List<object>(), info = ex.ToString() });
        }
        return result;
    }
    /// <summary>
    /// 账单文件
    /// </summary>
    /// <param name="orderid"></param>
    /// <returns></returns>
    [WebMethod]
    public string GetRentListFile(string orderid)
    {
        string result = string.Empty;
        try
        {
            string sql = string.Format("select a.ODContractSPNo as Provider,b.OrderNo from Op_OrderDetail a left join Op_OrderHeader b on a.RefRP=b.RowPointer where a.RefRP='{0}'", orderid);
            var dt = obj.PopulateDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string rootPath = HttpRuntime.AppDomainAppPath;
                string rootUrl = "http://" + HttpContext.Current.Request.Url.Authority;
                string pic = !string.IsNullOrEmpty(dt.Rows[0]["Provider"].ToString()) ?
                    rootPath + "pdf\\" + dt.Rows[0]["Provider"].ToString() + ".png"
                    : rootPath + "pdf\\a.png";
                string file = dt.Rows[0]["OrderNo"].ToString() + ".pdf";
                string input = rootPath + "pdf\\" + Guid.NewGuid().ToString() + ".pdf";
                string output = rootPath + "pdf\\" + file;
                Document doc = new Document(PageSize.A5.Rotate(), 20, 20, 20, 10);
                PdfWriter.GetInstance(doc, new FileStream(input, FileMode.Create));
                doc.Open();
                doc.NewPage();
                project.Presentation.Op.OrderPrint.Print(doc, orderid);
                try { doc.Close(); } catch { }
                project.Presentation.Op.OrderPrint.PDFWatermark(input, output, pic, 369, 111);
                if (File.Exists(input)) File.Delete(input);
                result = JsonConvert.SerializeObject(new { flag = 1, url = rootUrl + "/pdf/" + file, info = "文件生成成功！" });

            }
            else
            {
                result = JsonConvert.SerializeObject(new { flag = 2, url = "", info = "无相关数据！" });
            }

        }
        catch (Exception ex)
        {
            result = JsonConvert.SerializeObject(new { flag = 3, url = "", info = ex.ToString() });
        }
        return result;
    }


    //工单
    [WebMethod]
    public string GetMeterInfo(string MeterNo, string KEY, out string meterRMID, out decimal readout, out int digit)
    {
        meterRMID = "";
        readout = 0;
        digit = 0;
        string InfoMsg = "";
        if (KEY != "6E5F0C851FD4")
        {
            InfoMsg = "参数有误！";
            return "";
        }
        try
        {
            DataTable dt = obj.PopulateDataSet("select MeterReadout,MeterRate,MeterDigit,MeterRMID from Mstr_Meter where MeterNo='" + MeterNo + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                meterRMID = dt.Rows[0]["MeterRMID"].ToString();
                readout = ParseDecimalForString(dt.Rows[0]["MeterReadout"].ToString());
                digit = ParseIntForString(dt.Rows[0]["MeterDigit"].ToString());
            }
            else
            {
                InfoMsg = "当前表记编号不存在！";
            }
        }
        catch { InfoMsg = "获取表记信息异常"; }
        return InfoMsg;
    }

    [WebMethod]
    public string MeterReadout(string MeterNo, string ReadoutType, decimal LastReadout, decimal Readout,
        decimal JoinReadings, decimal Readings, string UserName, string KEY, byte[] ImgByte)
    {
        if (KEY != "6E5F0C851FD4") return "";

        string InfoMsg = "";
        string imgName = MeterNo + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
        string imgPath = ReadoutImgPath + imgName;
        try
        {
            if (ImgByte != null) SaveReadoutImg(MeterNo, imgName, ImgByte);//保存图片
            else imgName = string.Empty;

            project.Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
            string id = Guid.NewGuid().ToString();
            bc.Entity.RowPointer = id;
            bc.Entity.MeterNo = MeterNo;

            string isOk = "1";
            try
            {
                project.Business.Base.BusinessMeter bm = new project.Business.Base.BusinessMeter();
                bm.load(MeterNo);
                bc.Entity.RMID = bm.Entity.MeterRMID;
                bc.Entity.MeterType = bm.Entity.MeterType;
                bc.Entity.MeteRate = bm.Entity.MeterRate;
            }
            catch
            {
                isOk = "0";
                InfoMsg = "当前表记未找到！";
                DelReadoutImg(imgPath);
            }

            if (isOk == "1")
            {
                bc.Entity.ReadoutType = ReadoutType;
                bc.Entity.LastReadout = LastReadout;
                bc.Entity.Readout = Readout;
                bc.Entity.JoinReadings = JoinReadings;
                bc.Entity.Readings = Readings;
                DataTable tempDt = obj.PopulateDataSet(string.Format("SELECT READERNO FROM Mstr_MeterReader WHERE READERNAME='{0}'", UserName)).Tables[0];
                bc.Entity.ROOperator = tempDt.Rows.Count > 0 ? tempDt.Rows[0]["ReaderNo"].ToString() : "";
                bc.Entity.RODate = GetDate();
                bc.Entity.Img = imgName;
                bc.Entity.ROCreateDate = GetDate();
                bc.Entity.ROCreator = UserName;

                //获取换表记录 [换表记录已审核，并没有被抄表引用]
                DataTable dt = obj.PopulateDataSet("select RowPointer,OldMeterReadings from Op_ChangeMeter " +
                    "where AuditStatus='1' and NewMeterNo='" + bc.Entity.MeterNo + "' " +
                    "and RowPointer not in (select ChangeID from Op_Readout_ChangeList)").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    bc.Entity.IsChange = true;
                    decimal OldMeterReadings = 0;

                    //考虑多次换表的情况
                    foreach (DataRow dr in dt.Rows)
                    {
                        OldMeterReadings += ParseDecimalForString(dr["OldMeterReadings"].ToString());
                    }
                    bc.Entity.OldMeterReadings = OldMeterReadings;
                }
                else
                {
                    bc.Entity.IsChange = false;
                    bc.Entity.OldMeterReadings = 0;
                }

                int r = bc.Save("insert");
                if (r <= 0)
                {
                    DelReadoutImg(imgPath);
                    InfoMsg = "抄表写入失败！";

                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        obj.ExecuteNonQuery("insert into Op_Readout_ChangeList(RowPointer,RefRP,ChangeID) values(newid(),'" + id + "','" + dr["RowPointer"].ToString() + "')");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            DelReadoutImg(imgPath);
            InfoMsg = ex.Message;
        }

        return InfoMsg;
    }

    //资源
    [WebMethod]
    public DataTable GetStatisticsList_Resourse(string ResType, string StartDate, string EndDate, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return null;

        SqlConnection con = null;
        SqlCommand command = null;
        DataSet ds = new DataSet();
        try
        {
            con = Data.Conn();
            command = new SqlCommand("GetStatisticsList_Resourse", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ResType", SqlDbType.NVarChar, 10).Value = ResType;
            command.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10).Value = StartDate;
            command.Parameters.Add("@EndDate", SqlDbType.NVarChar, 10).Value = EndDate;
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

    [WebMethod]
    public DataSet GetStatisticsList_Room(string StartDate, string EndDate, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return null;

        SqlConnection con = null;
        SqlCommand command = null;
        DataSet ds = new DataSet();
        try
        {
            con = Data.Conn();
            command = new SqlCommand("GetStatisticsList_Room", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10).Value = StartDate;
            command.Parameters.Add("@EndDate", SqlDbType.NVarChar, 10).Value = EndDate;
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(ds);
            return ds;
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
    [WebMethod]
    public DataTable GetStatisticsList_Room_Charts(string StartDate, string EndDate, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return null;

        SqlConnection con = null;
        SqlCommand command = null;
        DataSet ds = new DataSet();
        try
        {
            con = Data.Conn();
            command = new SqlCommand("GetStatisticsList_Room_Charts", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10).Value = StartDate;
            command.Parameters.Add("@EndDate", SqlDbType.NVarChar, 10).Value = EndDate;
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
    [WebMethod]
    public DataTable GetOccupancyRate(string StartDate, string EndDate, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return null;

        SqlConnection con = null;
        SqlCommand command = null;
        DataSet ds = new DataSet();
        try
        {
            con = Data.Conn();
            command = new SqlCommand("GetOccupancyRate", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MinMonth", SqlDbType.NVarChar, 7).Value = StartDate;
            command.Parameters.Add("@MaxMonth", SqlDbType.NVarChar, 7).Value = EndDate;
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
    [WebMethod]
    public DataTable GetStatisticsList_WP_Charts(string StartDate, string EndDate, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return null;

        SqlConnection con = null;
        SqlCommand command = null;
        DataSet ds = new DataSet();
        try
        {
            con = Data.Conn();
            command = new SqlCommand("GetStatisticsList_WP_Charts", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10).Value = StartDate;
            command.Parameters.Add("@EndDate", SqlDbType.NVarChar, 10).Value = EndDate;
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
    [WebMethod]
    public DataTable GetStatisticsList_WP_Charts1(string MinMonth, string MaxMonth, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return null;

        SqlConnection con = null;
        SqlCommand command = null;
        DataSet ds = new DataSet();
        try
        {
            con = Data.Conn();
            command = new SqlCommand("GetStatisticsList_WP_Charts1", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@MinMonth", SqlDbType.NVarChar, 7).Value = MinMonth;
            command.Parameters.Add("@MaxMonth", SqlDbType.NVarChar, 7).Value = MaxMonth;
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
    [WebMethod]
    public DataTable GetStatisticsList_BB(string StartDate, string EndDate, string KEY)
    {
        if (KEY != "6E5F0C851FD4") return null;

        SqlConnection con = null;
        SqlCommand command = null;
        DataSet ds = new DataSet();
        try
        {
            con = Data.Conn();
            command = new SqlCommand("GetStatisticsList_BB", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10).Value = StartDate;
            command.Parameters.Add("@EndDate", SqlDbType.NVarChar, 10).Value = EndDate;
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

    private DateTime GetDate()
    {
        Data obj = new Data();
        return DateTime.Parse(obj.PopulateDataSet("select DT=getdate()").Tables[0].Rows[0]["DT"].ToString());
    }
    private System.DateTime ParseDateForString(string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            return DateTime.MinValue.AddYears(1900);
        }

        return DateTime.Parse(val);
    }
    private decimal ParseDecimalForString(string val)
    {
        if (string.IsNullOrEmpty(val))
            return 0;

        return decimal.Parse(val);
    }
    private int ParseIntForString(string val)
    {
        if (string.IsNullOrEmpty(val))
            return 0;

        return Int32.Parse(val);
    }
    public bool SaveReadoutImg(string meterNo, string imgName, byte[] imgByte)
    {
        FileStream fs = null;
        bool success = false;
        string imgPath = ReadoutImgPath + imgName;
        try
        {
            //保存图片
            if (!Directory.Exists(ReadoutImgPath)) Directory.CreateDirectory(ReadoutImgPath);
            fs = new FileStream(imgPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fs.Write(imgByte, 0, imgByte.Length);

            //数据做保留三张图片
            DataTable dt = obj.PopulateDataSet(string.Format("SELECT * FROM Op_Readout WHERE MeterNo='{0}' AND Img <>'' ORDER BY RODate DESC", meterNo)).Tables[0];
            for (int i = 2; i < dt.Rows.Count; i++)
            {
                string deleteFilePath = string.Format("{0}{1}", ReadoutImgPath, dt.Rows[i]["ImgUrl"].ToString());
                if (File.Exists(deleteFilePath)) File.Delete(deleteFilePath);
                obj.ExecuteNonQuery(string.Format("UPDATE Op_Readout SET Img='' WHERE RowPointer='{0}'", dt.Rows[i]["RowPointer"].ToString()));
            }
            success = true;
        }
        catch (Exception)
        {
            DelReadoutImg(imgPath);
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
            }
        }
        return success;
    }
    public void DelReadoutImg(string imgPath)
    {
        try { if (File.Exists(imgPath)) File.Delete(imgPath); }
        catch { }
    }
}
