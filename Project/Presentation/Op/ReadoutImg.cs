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

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string path = context.Server.MapPath("~/upload/meter/");
            string imgPath = string.Empty;
            string newImgName = string.Empty;
            int flag = 0;
            string info = string.Empty;
            string exinfo = string.Empty;
            JsonObjectCollection collection = new JsonObjectCollection();
            try
            {
                string imgName = context.Request.Form["Img"];
                string meterNo = context.Request["meterNo"];
                string id = context.Request["id"];
                string opration = context.Request["flag"];

                if (opration == "1")
                {
                    if (context.Request.Files.Count > 0)
                    {
                        HttpPostedFile postFile = context.Request.Files[0];
                        string mime = postFile.ContentType.ToLower();
                        if (mime.Contains("image"))
                        {
                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                            if (!string.IsNullOrEmpty(meterNo))
                            {
                                //保存图片
                                newImgName = meterNo + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                                postFile.SaveAs(path + newImgName);

                                //更改记录
                                if (!string.IsNullOrEmpty(id))
                                {
                                    Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
                                    bc.load(id);
                                    bc.Entity.Img = newImgName;
                                    bc.Save("update");
                                }

                                //删除原始图片
                                if (!string.IsNullOrEmpty(imgName))
                                {
                                    imgPath = path + imgName;
                                    if (File.Exists(imgPath)) File.Delete(imgPath);
                                }
                                flag = 1;
                                info = "图片保存成功！";
                            }
                            else
                            {
                                flag = 2;
                                info = "表计编号为空！";
                            }
                        }
                        else
                        {
                            flag = 2;
                            info = "图片格式错误！";
                        }
                    }
                    else
                    {
                        flag = 2;
                        info = "未检测到图片文件！";
                    }
                }
                else
                {
                    //更改记录
                    if (!string.IsNullOrEmpty(id))
                    {
                        Business.Op.BusinessReadout bc = new project.Business.Op.BusinessReadout();
                        bc.load(id);
                        bc.Entity.Img = "";
                        bc.Save("update");
                    }

                    //删除原始图片
                    if (!string.IsNullOrEmpty(imgName))
                    {
                        imgPath = path + imgName;
                        if (File.Exists(imgPath)) File.Delete(imgPath);
                    }
                    flag = 1;
                    info = "删除图片成功！";
                }
            }
            catch (Exception ex)
            {
                flag = 3;
                info = "操作异常！";
                exinfo = ex.ToString();
            }
            collection.Add(new JsonNumericValue("flag", flag));
            collection.Add(new JsonStringValue("info", info));
            collection.Add(new JsonStringValue("exinfo", exinfo));
            collection.Add(new JsonStringValue("img", newImgName));
            context.Response.Write(collection.ToString());
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
