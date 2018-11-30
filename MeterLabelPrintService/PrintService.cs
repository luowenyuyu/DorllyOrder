using log4net;
using MSXML2;
using Newtonsoft.Json;
using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace MeterLabelPrintService
{
    public partial class PrintService : ServiceBase
    {
        private string _serviceUrl = string.Empty;
        private int _interval = 1000;
        private string _machineName = "ZDesigner GK888t";
        private static readonly ILog logger = LogManager.GetLogger(typeof(PrintService));
        public PrintService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //初始化参数
            logger.Info("服务初始化中！");
            try
            {
                int.TryParse(ConfigurationManager.AppSettings["Time"].ToString(), out _interval);
                _machineName = ConfigurationManager.AppSettings["MachineName"].ToString();
                _serviceUrl = ConfigurationManager.AppSettings["WebServiceUrl"].ToString();
                if (string.IsNullOrEmpty(_serviceUrl)) throw new Exception("接口服务地址获取失败！");
                logger.Info("检索时间间隔：" + _interval);
                logger.Info("服务接口：" + _serviceUrl);
                //设置定时任务
                Timer printTimer = new Timer();
                printTimer.Interval = _interval;
                printTimer.Enabled = true;
                printTimer.Elapsed += new ElapsedEventHandler(LabelPrint);//注册事件
                logger.Info("成功注册定时任务!");
                TestService();
                logger.Info("服务启动...");
            }
            catch (Exception ex)
            {
                logger.Info("初始化失败！");
                logger.Info(ex.ToString());
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            logger.Info("服务停止...");
        }
        /// <summary>
        /// 定时执行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelPrint(object sender, ElapsedEventArgs e)
        {
            DorllyOrderService.Service service = new DorllyOrderService.Service();
            service.Url = _serviceUrl;
            try
            {
                logger.Info("开始检索打印数据...");
                DataTable dt = service.PopulateDataSet("Select * From OP_MeterPrint Where Status='0'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    service.ExecuteNonQuery("Update OP_MeterPrint Set Status='1' Where Status='0'");
                    List<Op_MeterPrint> printList = JsonConvert.DeserializeObject<List<Op_MeterPrint>>(JsonConvert.SerializeObject(dt));
                    try
                    {
                        logger.Info(string.Format("数据获取成功，数量为{0}", printList.Count));
                        logger.Info("");
                        logger.Info("--------------------- 打印开始 ---------------------");
                        logger.Info("");
                        foreach (var item in printList)
                        {
                            Engine printEngine = new Engine();
                            printEngine.Start();
                            LabelFormatDocument labelFormat = printEngine.Documents.Open(AppDomain.CurrentDomain.BaseDirectory + item.PrintFormat);
                            labelFormat.PrintSetup.PrinterName = _machineName;
                            labelFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
                            //编号
                            labelFormat.SubStrings["meterNo"].Value = item.MeterNo;
                            //名称
                            labelFormat.SubStrings["meterName"].Value = item.MeterName;
                            //费率
                            labelFormat.SubStrings["meterRate"].Value = item.MeterRate.ToString("0.####");
                            //位数
                            labelFormat.SubStrings["meterDigit"].Value = item.MeterDigit.ToString();
                            //备注
                            labelFormat.SubStrings["remark"].Value = item.Remark;
                            //二维码
                            labelFormat.SubStrings["meterCode"].Value = item.MeterNo;
                            labelFormat.Print();
                            labelFormat.Close(SaveOptions.DoNotSaveChanges);//不保存对打开模板的修改
                            printEngine.Stop();
                            logger.Info("完成编号为 "+item.MeterNo+" 的打印！");
                        }
                        logger.Info("--------------------- 打印结束 ---------------------");
                        logger.Info("");
                    }
                    catch (Exception ex)
                    {
                        logger.Error("打印发生错误！详情见错误信息！");
                        logger.Error(ex.ToString());
                    }
                }//end count if
                else logger.Info("暂无打印数据...");
            }
            catch (Exception ex)
            {
                logger.Error("数据获取错误！详情见错误信息！");
                logger.Error(ex.ToString());
            }

        }

        private void TestService()
        {
            bool success = false;
            XMLHTTP http = new XMLHTTP();
            try
            {
                logger.Info("测试接口是否联通？");
                http.open("GET", _serviceUrl, false, null, null);
                http.send(_serviceUrl);
                if (http.status == 200)
                {
                    logger.Info("接口正常！");
                    success = true;
                }
                else logger.Error("数据接口不可用！");
            }
            catch
            {
                logger.Error("数据接口不可用！");
            }
            finally
            {
                if (!success) this.Stop();
            }

        }
    }
}
