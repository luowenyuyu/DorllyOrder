using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using System.Threading;
using System.Data.Entity.Migrations;

namespace ParkingOrder
{
    partial class ParkingService : ServiceBase
    {
        #region 全局参数

        /// <summary>
        /// 轮询间隔
        /// </summary>
        private int _intervalSeconds = 180000;
        /// <summary>
        /// 过车记录http接口
        /// </summary>
        private string _passCarHttp = string.Empty;
        /// <summary>
        /// 月租车充值记录http接口
        /// </summary>
        private string _monthCarHttp = string.Empty;
        /// <summary>
        /// 订单上客户编码
        /// </summary>
        private string _customer = string.Empty;
        /// <summary>
        /// 记录本次数据的用户名称
        /// </summary>
        private string _userName = string.Empty;
        /// <summary>
        /// 上次轮询的时间
        /// </summary>
        private DateTime _lastCheckTime;
        /// <summary>
        /// 订单类型
        /// </summary>
        private string _orderType = string.Empty;
        /// <summary>
        /// 日志记录对象
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(ParkingService));
        /// <summary>
        /// 接口签名(MD5加密)
        /// </summary>
        private string _signature = string.Empty;
        /// <summary>
        /// 调用接口获取数据数量
        /// </summary>
        private int _limit = 1000;
        /// <summary>
        /// 停车场编码
        /// </summary>
        private string _parkingCode = string.Empty;
        /// <summary>
        /// 加密密钥
        /// </summary>
        private string _md5Key = string.Empty;
        /// <summary>
        /// 费用编码
        /// </summary>
        private string _feeID = string.Empty;
        /// <summary>
        /// 每次获取数据时间间隔
        /// </summary>
        private int _passSeconds = 1800;
        /// <summary>
        /// 程序根目录
        /// </summary>
        private string _rootPath = Environment.CurrentDirectory;

        private List<Op_PassCar> passCarList = null;
        private List<Op_MonthCar> monthCarList = null;
        private bool isRun = false;

        #endregion

        public ParkingService()
        {
            logger.Info("ParkingService InitializeComponent");
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            try
            {
                logger.Info("########   初始化中... ########");
                int.TryParse(ConfigurationManager.AppSettings["IntervalSeconds"].ToString(), out _intervalSeconds);
                logger.Info("轮询间隔时间(秒,默认：30):" + _intervalSeconds);
                _lastCheckTime = DateTime.Parse(DateTime.Parse(ConfigurationManager.AppSettings["LastCheckTime"].ToString()).ToShortDateString());
                logger.Info("上次轮询日期:" + _lastCheckTime);
                _customer = ConfigurationManager.AppSettings["Customer"].ToString();
                logger.Info("客户名称:" + _customer);
                _orderType = ConfigurationManager.AppSettings["OrderType"].ToString();
                logger.Info("订单类型:" + _orderType);
                _feeID = ConfigurationManager.AppSettings["FeeID"].ToString();
                logger.Info("费用编码:" + _feeID);
                _userName = ConfigurationManager.AppSettings["UserName"].ToString();
                logger.Info("用户名称:" + _userName);
                _passCarHttp = ConfigurationManager.AppSettings["PassCarHttp"].ToString();
                logger.Info("过车记录接口:" + _passCarHttp);
                _monthCarHttp = ConfigurationManager.AppSettings["MonthCarHttp"].ToString();
                logger.Info("月租车接口:" + _monthCarHttp);
                int.TryParse(ConfigurationManager.AppSettings["Limit"].ToString(), out _limit);
                logger.Info("一次获取数据量(默认：1000):" + _limit);
                int.TryParse(ConfigurationManager.AppSettings["PassSeconds"].ToString(), out _passSeconds);
                logger.Info("每次获取时间间隔(默认：1800):" + _passSeconds);
                _parkingCode = ConfigurationManager.AppSettings["ParkingCode"].ToString();
                logger.Info("停车场编码(接口方提供):" + _parkingCode);
                _md5Key = ConfigurationManager.AppSettings["MD5Key"].ToString();
                logger.Info("密钥(接口方提供):" + _md5Key);
                if (string.IsNullOrEmpty(_customer) ||
                    string.IsNullOrEmpty(_orderType) ||
                     string.IsNullOrEmpty(_feeID) ||
                    string.IsNullOrEmpty(_userName) ||
                    string.IsNullOrEmpty(_passCarHttp) ||
                    string.IsNullOrEmpty(_monthCarHttp) ||
                    string.IsNullOrEmpty(_parkingCode) ||
                    string.IsNullOrEmpty(_md5Key))
                    throw new Exception("初始化参数有误！");
                logger.Info("######## 初始化完成 ########");
                this.ParkingTimer.Interval = _intervalSeconds * 1000;
                this.ParkingTimer.Enabled = true;
                this.ParkingTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.RollingRepeatModel_Elapse);
            }
            catch (Exception ex)
            {
                logger.Debug("初始化失败！");
                logger.Debug(ex.ToString());
                OnStop();
            }
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            logger.Info("######## 服务停止... ########");
        }


        #region 不可重复获取数据模式
        /// <summary>
        /// 定时触发事件
        /// 在时间段内返回的数据不重复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollingNoRepeatModel_Elapse(object sender, ElapsedEventArgs e)
        {
            try
            {
                DateTime now = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

                if (_lastCheckTime < now.AddDays(-1))
                {
                    passCarList.Clear();
                    monthCarList.Clear();

                    //请求参数处理
                    RequestParam param = new RequestParam();
                    param.parkingCode = _parkingCode;
                    param.retLimit = _limit.ToString();
                    param.startTime = _lastCheckTime.ToString("yyyy-MM-dd") + " 00:00:00";
                    param.endTime = _lastCheckTime.ToString("yyyy-MM-dd") + " 23:59:59";
                    param.requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    param.signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Format("endTime={0}&" +
                        "parkingCode={1}&" +
                        "requestTime={2}&" +
                        "retLimit={3}&" +
                        "startTime={4}&{5}",
                        param.endTime,
                        param.parkingCode,
                        param.requestTime,
                        param.retLimit,
                        param.startTime,
                        _md5Key), "MD5");
                    //处理过车
                    DeserializablePassCar();
                    PassCarNoRepeatPost(param);
                    //处理月租车
                    DeserializableMonthCar();
                    MonthCarNoRepeatPost(param);
                    //生成订单
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
            }
            finally
            {
                if (passCarList.Count > 0) SerializablePassCar();
                if (monthCarList.Count > 0) SerializableMonthCar();
            }


        }

        #region 过车记录处理
        private void PassCarNoRepeatPost(RequestParam param)
        {
            var strParam = JsonConvert.SerializeObject(param);
            while (true)
            {
                string result = HttpPost(_passCarHttp, strParam);
                if (string.IsNullOrEmpty(result))
                {
                    logger.Info("result is null.");
                }
                else
                {
                    var resultList = JsonConvert.DeserializeObject<ResultList>(result);
                    if (resultList.Code != "0")
                    {
                        logger.Info("result is wrong!");
                        logger.Info(result);
                        break;
                    }
                    if (resultList.PassCarResult != null) passCarList.AddRange(resultList.PassCarResult);
                    if (resultList.PassCarResult == null || resultList.PassCarResult.Count < _limit) break;
                    Thread.Sleep(500);
                }

            }
        }
        /// <summary>
        /// 序列化过车记录
        /// </summary>
        private void SerializablePassCar()
        {
            if (passCarList.Count > 0)
            {
                var fileName = _lastCheckTime.ToString("yyyy-MM-dd") + "_passcar.dat";
                SerializeHelper.BinarySerializeToFile(passCarList, _rootPath, fileName);
                passCarList.Clear();
            }
        }
        /// <summary>
        /// 反序列化过车记录
        /// </summary>
        private void DeserializablePassCar()
        {
            var fileName = _lastCheckTime.ToString("yyyy-MM-dd") + "_passcar.dat";
            var path = string.Format(@"{0}\{1}", _rootPath, fileName);
            if (File.Exists(path)) passCarList.AddRange(SerializeHelper.BinaryDeserializeFromFile<List<Op_PassCar>>(path));
        }
        #endregion

        #region 月租车处理       
        private void MonthCarNoRepeatPost(RequestParam param)
        {
            var strParam = JsonConvert.SerializeObject(param);
            while (true)
            {
                string result = HttpPost(_passCarHttp, strParam);
                if (string.IsNullOrEmpty(result))
                {
                    logger.Info("result is null,break while.");
                }
                else
                {
                    var resultList = JsonConvert.DeserializeObject<ResultList>(result);
                    if (resultList.Code != "0")
                    {
                        logger.Info("result is wrong!");
                        logger.Info(result);
                        break;
                    }
                    if (resultList.MonthCarResult != null) monthCarList.AddRange(resultList.MonthCarResult);
                    if (resultList.MonthCarResult == null || resultList.MonthCarResult.Count < _limit) break;
                    Thread.Sleep(500);
                }
            }
        }
        private void SerializableMonthCar()
        {
            if (monthCarList.Count > 0)
            {
                var fileName = _lastCheckTime.ToString("yyyy-MM-dd") + "_monthcar.dat";
                SerializeHelper.BinarySerializeToFile(monthCarList, _rootPath, fileName);
                monthCarList.Clear();
            }
        }
        private void DeserializableMonthCar()
        {
            var fileName = _lastCheckTime.ToString("yyyy-MM-dd") + "_monthcar.dat";
            var path = string.Format(@"{0}\{1}", _rootPath, fileName);
            if (File.Exists(path)) monthCarList.AddRange(SerializeHelper.BinaryDeserializeFromFile<List<Op_MonthCar>>(path));
        }
        #endregion

        #endregion

        #region 可重复获取数据模式
        /// <summary>
        /// 定时触发事件
        /// 在时间段内返回的数据重复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollingRepeatModel_Elapse(object sender, ElapsedEventArgs e)
        {
            try
            {

                if (_lastCheckTime < DateTime.Now.AddDays(-2) && isRun == false)
                {
                    isRun = true;
                    passCarList = new List<Op_PassCar>();
                    monthCarList = new List<Op_MonthCar>();
                    DateTime startTime = _lastCheckTime;
                    DateTime endTime = _lastCheckTime.AddDays(1).AddSeconds(-1);
                    int count = 0;
                    logger.Info(string.Format("本次同步开始【开始时间:{0};结束时间:{1}】...", startTime, endTime));
                    //数据同步
                    while (startTime < endTime)
                    {

                        //请求参数处理
                        DateTime tempTime = startTime.AddSeconds(_passSeconds);
                        if (tempTime >= endTime) tempTime = endTime;
                        logger.Info(string.Format("开始第{0}次数据请求,", ++count));
                        RequestParam param = new RequestParam();
                        param.parkingCode = _parkingCode;
                        param.retLimit = _limit.ToString();
                        param.startTime = startTime.ToString("yyyy-MM-dd HH:mm:ss");
                        param.endTime = tempTime.ToString("yyyy-MM-dd HH:mm:ss");
                        param.requestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        param.signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Format("endTime={0}&" +
                            "parkingCode={1}&" +
                            "requestTime={2}&" +
                            "retLimit={3}&" +
                            "startTime={4}&{5}",
                            param.endTime,
                            param.parkingCode,
                            param.requestTime,
                            param.retLimit,
                            param.startTime,
                            _md5Key), "MD5").ToLower();
                        var strParam = JsonConvert.SerializeObject(param);
                        logger.Info(string.Format("请求过车数据,开始时间:{0}-结束时间:{1}", startTime, tempTime));
                        PassCarRepeatPost(strParam);
                        logger.Info(string.Format("请求月租数据,开始时间:{0}-结束时间:{1}", startTime, tempTime));
                        MonthCarRepeatPost(strParam);
                        startTime = tempTime.AddMilliseconds(1);
                        Thread.Sleep(500);
                    }
                    logger.Info("同步完成，处理数据...");
                    //同步完成
                    DorllyOrderModel model = new DorllyOrderModel();
                    logger.Info(string.Format("原始数据处理...【过车数据{0}条,月租充值{1}条】", passCarList.Count(), monthCarList.Count));

                    //保存原始数据
                    passCarList = passCarList.GroupBy(a => a.UniqueID).Select(a=>a.First()).ToList();
                    foreach (var item in passCarList)
                    {
                        model.Op_PassCar.AddOrUpdate(item);
                    }
                    monthCarList = monthCarList.GroupBy(a => a.CarNo).Select(a => a.First()).ToList();
                    foreach (var item in monthCarList)
                    {
                        model.Op_MonthCar.AddOrUpdate(item);
                    }
                    logger.Info("订单数据处理...");
                    string orderID = Guid.NewGuid().ToString();
                    decimal rateAmount = AddOrderDetail(orderID, passCarList, monthCarList, model);//订单明细填充                   
                    AddOrderHeader(orderID, rateAmount, passCarList, monthCarList, model); //订单主体填充
                    logger.Info("数据提交...");
                    model.SaveChanges();
                    logger.Info("配置处理...");
                    _lastCheckTime = endTime.AddSeconds(1);
                    //ConfigurationManager.AppSettings.Set("LastCheckTime", _lastCheckTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    //ConfigurationManager.RefreshSection("appSettings");
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["LastCheckTime"].Value = _lastCheckTime.ToString("yyyy-MM-dd HH:mm:ss");
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    passCarList.Clear();
                    monthCarList.Clear();
                    logger.Info("处理结束，将进行下一次同步！");
                    //this.Stop();
                    isRun = false;
                }

            }
            catch (Exception ex)
            {
                logger.Debug("本次同步异常，详情见下面异常描述，等待下次同步！");
                logger.Debug(ex.ToString());
                logger.Debug(ex.InnerException.Message);
                isRun = false;
                //this.Stop();
            }

        }
        /*
            1001	未找到该车牌对应的车辆信息
            1002	订单已存在
            1003	请求超时,requestTime超时
            1004	签名错误
            1005	参数错误
            1006	系统内部错误
         */

        private void PassCarRepeatPost(string param)
        {
            string result = string.Empty;
            try
            {
                result = HttpPost(_passCarHttp, param);
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Replace("result", "PassCarResult");
                    var resultList = JsonConvert.DeserializeObject<ResultList>(result);
                    if (resultList.Code == "0")
                    {
                        if (resultList.PassCarResult != null)
                        {
                            passCarList.AddRange(resultList.PassCarResult);
                        }
                        else logger.Info(string.Format("返回成功，但是没有数据，返回值：{0}", result));
                    }
                    else if (resultList.Code == "1001")
                    {
                        logger.Info(string.Format("没有该时间段数据，返回值：{0}", result));
                    }
                    else
                    {
                        throw new Exception("返回错误，终止本次同步！");
                    }
                }
                else
                {
                    throw new Exception("返回空值，终止本次同步！");
                }

            }
            catch (Exception ex)
            {
                logger.Info(string.Format("发送参数：{0}", param));
                logger.Info(string.Format("返回结果：{0}", result));
                throw ex;
            }

        }

        private void MonthCarRepeatPost(string param)
        {
            string result = string.Empty;
            try
            {
                result = HttpPost(_monthCarHttp, param);
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Replace("result", "MonthCarResult");
                    var resultList = JsonConvert.DeserializeObject<ResultList>(result);
                    if (resultList.Code == "0")
                    {
                        if (resultList.MonthCarResult != null)
                        {
                            monthCarList.AddRange(resultList.MonthCarResult);
                        }
                        else logger.Info(string.Format("返回成功，但是没有数据，返回值：{0}", result));
                    }
                    else if (resultList.Code == "1001")
                    {
                        logger.Info(string.Format("没有该时间段数据，返回值：{0}", result));
                    }
                    else
                    {
                        throw new Exception("返回错误，终止本次同步！");
                    }
                }
                else
                {
                    throw new Exception("返回空值，终止本次同步！");
                }

            }
            catch (Exception ex)
            {
                logger.Info(string.Format("发送参数：{0}", param));
                logger.Info(string.Format("返回结果：{0}", result));
                throw ex;
            }
        }
        #endregion




        private void AddOrderHeader(string orderID, decimal rateAmount, List<Op_PassCar> passCarList, List<Op_MonthCar> monthCarList, DorllyOrderModel model)
        {
            decimal monthAmount = monthCarList.Sum(a => a.Amount) / 100;
            decimal cashAmount = passCarList.Sum(a => a.ActualPay) / 100;
            decimal prepayAmount = passCarList.Sum(a => a.Prepay) / 100;
            decimal discountAmount = passCarList.Sum(a => a.DiscountVal) / 100;
            Op_OrderHeader oh = new Op_OrderHeader();
            oh.RowPointer = orderID;
            //订单编号
            var time = DateTime.Now.ToString("yyyyMMdd");
            var orderNo = model.Op_OrderHeader.Where(a => a.OrderNo.Contains(time)).Max(a => a.OrderNo);
            if (string.IsNullOrEmpty(orderNo))
                oh.OrderNo = DateTime.Now.ToString("yyyyMMdd") + "00001";
            else
                oh.OrderNo = (long.Parse(orderNo) + 1).ToString();
            oh.OrderType = _orderType;
            oh.CustNo = _customer;
            oh.OrderTime = _lastCheckTime;
            oh.ARDate = _lastCheckTime;
            oh.DaysofMonth = 1;
            oh.OrderStatus = "0";
            oh.ARAmount = Math.Round(monthAmount + cashAmount + prepayAmount + discountAmount, 2);
            oh.ReduceAmount = 0;
            oh.PaidinAmount = 0;
            oh.ODTaxAmount = rateAmount;
            oh.Remark = "";
            oh.OrderCreator = _userName;
            oh.OrderCreateDate = DateTime.Now;
            oh.OrderLastReviser = _userName;
            oh.OrderLastRevisedDate = DateTime.Now;
            model.Op_OrderHeader.Add(oh);
        }
        private decimal AddOrderDetail(string orderID, List<Op_PassCar> passCarList, List<Op_MonthCar> monthCarList, DorllyOrderModel model)
        {
            decimal rateAmount = 0;
            var feeObject = model.Mstr_Service.Where(a => a.SRVNo == _feeID).FirstOrDefault();
            decimal rate = model.Mstr_TaxRate.Where(a => a.SRVNo == _feeID).FirstOrDefault().Rate ?? 0;
            Op_OrderDetail orderDetail = new Op_OrderDetail();
            //不变
            orderDetail.RefRP = orderID;
            orderDetail.ODSRVTypeNo1 = feeObject.SRVTypeNo1;
            orderDetail.ODSRVTypeNo2 = feeObject.SRVTypeNo2;
            orderDetail.ODContractSPNo = feeObject.SRVSPNo;
            orderDetail.ODSRVNo = feeObject.SRVNo;
            //orderDetail.ResourceNo = _parkingCode;
            orderDetail.ODFeeStartDate = _lastCheckTime;
            orderDetail.ODFeeEndDate = DateTime.Parse(_lastCheckTime.ToShortDateString()).AddDays(1).AddSeconds(-1);
            orderDetail.BillingDays = 1;
            orderDetail.ODQTY = 1;
            orderDetail.ODUnit = "天";
            orderDetail.ODTaxRate = rate;
            orderDetail.ODCANo = feeObject.CANo;
            orderDetail.ReduceAmount = 0;
            orderDetail.ODPaidAmount = 0;
            orderDetail.IsLateFee = false;
            orderDetail.ODCreateDate = DateTime.Now;
            orderDetail.ODCreator = _userName;
            orderDetail.ODLastRevisedDate = DateTime.Now;
            orderDetail.ODLastReviser = _userName;
            //月租车收入
            decimal monthAmount = Math.Round(monthCarList.Sum(a => a.Amount) / 100, 2);
            if (monthAmount > 0)
            {
                var detail = JsonConvert.DeserializeObject<Op_OrderDetail>(JsonConvert.SerializeObject(orderDetail));
                detail.RowPointer = Guid.NewGuid().ToString();
                detail.ResourceNo = _parkingCode+"_Card";
                detail.ResourceName = "月租车充值_" + _lastCheckTime.ToString("yyyy-MM-dd");
                detail.ODUnitPrice = monthAmount;
                detail.ODARAmount = monthAmount;
                detail.ODTaxAmount = monthAmount - Math.Round(monthAmount / (1 + rate), 2);
                rateAmount += monthAmount - Math.Round(monthAmount / (1 + rate), 2);
                model.Op_OrderDetail.Add(detail);
            }
            //现金支付
            decimal cashAmount = Math.Round(passCarList.Sum(a => a.ActualPay) / 100, 2);
            if (cashAmount > 0)
            {
                var detail = JsonConvert.DeserializeObject<Op_OrderDetail>(JsonConvert.SerializeObject(orderDetail));
                detail.RowPointer = Guid.NewGuid().ToString();
                detail.ResourceNo = _parkingCode + "_Cash";
                detail.ResourceName = "现金支付_" + _lastCheckTime.ToString("yyyy-MM-dd");
                detail.ODUnitPrice = cashAmount;
                detail.ODARAmount = cashAmount;
                detail.ODTaxAmount = cashAmount - Math.Round(cashAmount / (1 + rate), 2);
                rateAmount += cashAmount - Math.Round(cashAmount / (1 + rate), 2);
                model.Op_OrderDetail.Add(detail);
            }
            //电子支付
            decimal prepayAmount = Math.Round(passCarList.Sum(a => a.Prepay) / 100, 2);
            if (prepayAmount > 0)
            {
                var detail = JsonConvert.DeserializeObject<Op_OrderDetail>(JsonConvert.SerializeObject(orderDetail));
                detail.RowPointer = Guid.NewGuid().ToString();
                detail.ResourceNo = _parkingCode + "_Elec";
                detail.ResourceName = "电子支付_" + _lastCheckTime.ToString("yyyy-MM-dd");
                detail.ODUnitPrice = prepayAmount;
                detail.ODARAmount = prepayAmount;
                detail.ODTaxAmount = prepayAmount - Math.Round(prepayAmount / (1 + rate), 2);
                rateAmount += prepayAmount - Math.Round(prepayAmount / (1 + rate), 2);
                model.Op_OrderDetail.Add(detail);
            }
            //优惠券
            decimal discountAmount = Math.Round(passCarList.Sum(a => a.DiscountVal) / 100, 2);
            if (discountAmount > 0)
            {
                var detail = JsonConvert.DeserializeObject<Op_OrderDetail>(JsonConvert.SerializeObject(orderDetail));
                detail.RowPointer = Guid.NewGuid().ToString();
                detail.ResourceNo = _parkingCode + "_Apex";
                detail.ResourceName = "优惠券_" + _lastCheckTime.ToString("yyyy-MM-dd");
                detail.ODUnitPrice = discountAmount;
                detail.ODARAmount = discountAmount;
                detail.ODTaxAmount = discountAmount - Math.Round(discountAmount / (1 + rate), 2);
                rateAmount += discountAmount - Math.Round(discountAmount / (1 + rate), 2);
                model.Op_OrderDetail.Add(detail);
            }
            return rateAmount;
        }

        /// <summary>
        /// 公共方法：http请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonParams"></param>
        /// <returns></returns>
        private string HttpPost(string url, string jsonParams)
        {
            string result = string.Empty;
            Stream requestWriter = null;
            StreamReader responseReader = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                /*
                    application/xhtml+xml：XHTML格式
                    application/xml：XML数据格式
                    application/atom+xml：AtomXML聚合格式
                    application/json：JSON数据格式
                    application/pdf：pdf格式
                    application/msword：Word文档格式
                    application/octet-stream：二进制流数据（如常见的文件下载）
                    application/x-www-form-urlencoded：<formencType=””>
                    中默认的encType，form表单数据被编码为key/value格式发送到服务器（表单默认的提交数据的格式）
                */
                request.ContentType = "application/json";
                byte[] paramsByte = Encoding.UTF8.GetBytes(jsonParams);
                request.ContentLength = paramsByte.Length;
                //发送请求
                requestWriter = request.GetRequestStream();
                requestWriter.Write(paramsByte, 0, paramsByte.Length);
                requestWriter.Close();
                //接收数据
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseReader = new StreamReader(response.GetResponseStream());
                result = responseReader.ReadToEnd();
                responseReader.Close();
            }
            catch (Exception ex)
            {
                logger.Debug("http请求异常！");
                throw ex;
            }
            finally
            {
                if (requestWriter != null) requestWriter.Close();
                if (responseReader != null) responseReader.Close();
            }
            return result;
        }


    }
}
