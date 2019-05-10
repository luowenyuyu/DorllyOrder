using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Json;
using System.Text;
using System.Web;
using System.IO;

namespace project.Presentation.Op
{
    public class ReadoutImg : IHttpHandler
    {
        /// <summary>
        /// 保存图片根目录
        /// </summary>
        private string _rootPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/upload/meter/");
            }
        }
        /// <summary>
        /// 表记编号
        /// </summary>
        private string _meterNo
        {
            get
            {
                return HttpContext.Current.Request.Form["meterNo"].ToString();
            }
        }
        /// <summary>
        /// 抄表记录主键
        /// </summary>
        private string _readoutID
        {
            get
            {
                return HttpContext.Current.Request.Form["id"].ToString();
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            int flag = 0;
            string info = string.Empty;
            context.Response.ContentType = "text/plain";
            JsonObjectCollection collection = new JsonObjectCollection();
            try
            {
                if (!string.IsNullOrEmpty(_meterNo))
                {
                    if (!string.IsNullOrEmpty(_readoutID))
                    {
                        /*
                         * 新增图片
                         */
                        //保存图片
                        info = SaveReadoutImg(context);
                        if (info.Contains(_meterNo)) flag = 1;
                    }
                    else
                    {
                        /*
                         * 变更图片
                         */
                        //保存图片
                        info = SaveReadoutImg(context);
                        if (info.Contains(_meterNo)) flag = 1;
                        //{
                        //    flag = 1;
                        //    //删除图片
                        //    bc = new project.Business.Op.BusinessReadout();
                        //    bc.load(_readoutID);
                        //    if (!string.IsNullOrEmpty(bc.Entity.Img))
                        //    {
                        //        string imgPath = _rootPath + bc.Entity.Img;
                        //        if (File.Exists(imgPath)) File.Delete(imgPath);
                        //    }
                        //}
                    }
                }
                else info = "表记编号为空！";
            }
            catch (Exception ex)
            {
                info = ex.Message;
            }
            collection.Add(new JsonNumericValue("flag", flag));
            collection.Add(new JsonStringValue("info", info));
            context.Response.Write(collection.ToString());
        }


        public string SaveReadoutImg(HttpContext context)
        {
            string result = string.Empty;
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpPostedFile postFile = context.Request.Files[0];
                    if (postFile.ContentType.ToLower().Contains("image"))
                    {
                        if (!Directory.Exists(_rootPath)) Directory.CreateDirectory(_rootPath);
                        result = _meterNo + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + postFile.FileName.Substring(postFile.FileName.LastIndexOf("."));
                        postFile.SaveAs(_rootPath + result);
                    }
                    else result = "文件不是图片类型！";
                }
                else result = "图片文件为零！";
            }
            catch (Exception ex)
            {
                result = "保存文件异常！" + ex.Message;
            }
            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}
