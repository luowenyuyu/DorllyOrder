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

namespace project.Presentation.Base
{
    public partial class Service : AbstractPmPage, System.Web.UI.ICallbackEventHandler
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
                    CheckRight(user.Entity, "pm/Base/Service.aspx");

                    if (!Page.IsCallback)
                    {
                        if (user.Entity.UserType.ToUpper() != "ADMIN")
                        {
                            string sqlstr = "select a.RightCode from Sys_UserRight a left join sys_menu b on a.MenuId=b.MenuID " +
                                "where a.UserType='" + user.Entity.UserType + "' and menupath='pm/Base/Service.aspx'";
                            DataTable dt = obj.PopulateDataSet(sqlstr).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                string rightCode = dt.Rows[0]["RightCode"].ToString();
                                if (rightCode.IndexOf("insert") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("update") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("delete") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                                if (rightCode.IndexOf("vilad") >= 0)
                                    Buttons += "<a href=\"javascript:;\" onclick=\"valid()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 启用/停用</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            Buttons += "<a href=\"javascript:;\" onclick=\"insert()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe600;</i> 添加</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"update()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe60c;</i> 修改</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"del()\" class=\"btn btn-danger radius\"><i class=\"Hui-iconfont\">&#xe6e2;</i> 删除</a>&nbsp;&nbsp;";
                            Buttons += "<a href=\"javascript:;\" onclick=\"valid()\" class=\"btn btn-primary radius\"><i class=\"Hui-iconfont\">&#xe615;</i> 启用/停用</a>&nbsp;&nbsp;";
                        }

                        list = createList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1);

                        SRVTypeNo1Str = "<select class=\"input-text required\" id=\"SRVTypeNo1\" style=\"width:240px;\" data-valid=\"isNonEmpty\" data-error=\"请选择所属服务大类\">";
                        SRVTypeNo1Str += "<option value=\"\" selected></option>";

                        SRVTypeNo1StrS = "<select class=\"input-text size-MINI\" id=\"SRVTypeNo1S\" style=\"width:120px;\" >";
                        SRVTypeNo1StrS += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessServiceType bc1 = new project.Business.Base.BusinessServiceType();
                        foreach (Entity.Base.EntityServiceType it in bc1.GetListQuery(string.Empty, string.Empty,"null",string.Empty,true))
                        {
                            SRVTypeNo1Str += "<option value='" + it.SRVTypeNo + "'>" + it.SRVTypeName + "</option>";
                            SRVTypeNo1StrS += "<option value='" + it.SRVTypeNo + "'>" + it.SRVTypeName + "</option>";
                        }
                        SRVTypeNo1Str += "</select>";
                        SRVTypeNo1StrS += "</select>";

                        SRVTypeNo2Str = "<select class=\"input-text required\" id=\"SRVTypeNo2\" style=\"width:240px;\" data-valid=\"isNonEmpty\" data-error=\"请选择所属服务小类\">";
                        SRVTypeNo2Str += "<option value=\"\" selected></option>";

                        SRVTypeNo2StrS = "<select class=\"input-text size-MINI\" id=\"SRVTypeNo2S\" style=\"width:120px;\">";
                        SRVTypeNo2StrS += "<option value=\"\" selected>全部</option>";
                        //Business.Base.BusinessServiceType bc2 = new project.Business.Base.BusinessServiceType();
                        //foreach (Entity.Base.EntityServiceType it in bc1.GetListQuery(string.Empty, string.Empty,"empty",string.Empty,true))
                        //{
                        //    SRVTypeNo2Str += "<option value='" + it.SRVTypeNo + "'>" + it.SRVTypeName + "</option>";
                        //    SRVTypeNo2StrS += "<option value='" + it.SRVTypeNo + "'>" + it.SRVTypeName + "</option>";
                        //}
                        SRVTypeNo2Str += "</select>";
                        SRVTypeNo2StrS += "</select>";

                        CANoStr = "<select class=\"input-text required\" id=\"CANo\" style=\"width:240px;\" data-valid=\"isNonEmpty\" data-error=\"财务收费科目不能为空\">";
                        CANoStr += "<option value=\"\" selected></option>";

                        CANoStrS = "<select class=\"input-text size-MINI\" id=\"CANoS\" style=\"width:120px;\" >";
                        CANoStrS += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessChargeAccount bc2 = new project.Business.Base.BusinessChargeAccount();
                        foreach (Entity.Base.EntityChargeAccount it in bc2.GetListQuery(string.Empty, string.Empty, string.Empty))
                        {
                            //CANoStr += "<option value='" + it.CANo + "'>" + it.CAName + " - " + it.CASPName + "</option>";
                            CANoStrS += "<option value='" + it.CANo + "'>" + it.CAName + " - " + it.CASPName + "</option>";
                        }
                        CANoStr += "</select>";
                        CANoStrS += "</select>";

                        SRVSPNoStr = "<select class=\"input-text required\" id=\"SRVSPNo\" style=\"width:240px;\" data-valid=\"isNonEmpty\" data-error=\"请选择服务商\">";
                        SRVSPNoStr += "<option value=\"\"></option>";

                        SRVSPNoStrS = "<select class=\"input-text size-MINI\" id=\"SRVSPNoS\" style=\"width:120px;\" >";
                        SRVSPNoStrS += "<option value=\"\" selected>全部</option>";
                        Business.Base.BusinessServiceProvider tp = new project.Business.Base.BusinessServiceProvider();
                        foreach (Entity.Base.EntityServiceProvider it in tp.GetListQuery(string.Empty, string.Empty, true))
                        {
                            SRVSPNoStr += "<option value='" + it.SPNo + "'>" + it.SPName + "</option>";
                            SRVSPNoStrS += "<option value='" + it.SPNo + "'>" + it.SPName + "</option>";
                        }
                        SRVSPNoStr += "</select>";
                        SRVSPNoStrS += "</select>";
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
        protected string CAStr = "";
        protected string SRVTypeNo1Str = "";
        protected string SRVTypeNo1StrS = "";
        protected string SRVTypeNo2Str = "";
        protected string SRVTypeNo2StrS = "";
        protected string CANoStr = "";
        protected string CANoStrS = "";
        protected string SRVSPNoStr = "";
        protected string SRVSPNoStrS = "";
        private string createList(string SRVNo, string SRVName, string SRVTypeNo1, string SRVTypeNo2, string SRVSPNo, string CANo, string SRVCalType, int page)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.Append("<table class=\"table table-border table-bordered table-hover table-bg table-sort\" id=\"tablelist\">");
            sb.Append("<thead>");
            sb.Append("<tr class=\"text-c\">");
            sb.Append("<th width=\"5%\">序号</th>");
            sb.Append("<th width='8%'>费用项目编号</th>");
            sb.Append("<th width='10%'>费用项目名称</th>");
            sb.Append("<th width='8%'>所属服务大类</th>");
            sb.Append("<th width='8%'>所属服务子类</th>");
            sb.Append("<th width='8%'>所属服务商</th>");
            sb.Append("<th width='8%'>取整方式</th>");
            sb.Append("<th width='5%'>小数位</th>");
            sb.Append("<th width='10%'>财务收费科目编号</th>");
            sb.Append("<th width='10%'>财务收费科目名称</th>");
            sb.Append("<th width='15%'>备注</th>");
            sb.Append("<th width='5%'>状态</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

            int r = 1;
            sb.Append("<tbody>");
            Business.Base.BusinessService bc = new project.Business.Base.BusinessService();
            foreach (Entity.Base.EntityService it in bc.GetListQuery(SRVNo, SRVName, SRVTypeNo1, SRVTypeNo2, SRVSPNo, CANo, SRVCalType, page, pageSize))
            {
                sb.Append("<tr class=\"text-c\" id=\"" + it.SRVNo + "\">");
                sb.Append("<td align='center'>" + r.ToString() + "</td>");
                sb.Append("<td>" + it.SRVNo + "</td>");
                sb.Append("<td>" + it.SRVName + "</td>");
                sb.Append("<td>" + it.SRVTypeNo1Name + "</td>");
                sb.Append("<td>" + it.SRVTypeNo2Name + "</td>");
                sb.Append("<td>" + it.SRVSPName + "</td>");
                sb.Append("<td>" + it.SRVRoundTypeName + "</td>");
                sb.Append("<td>" + it.SRVDecimalPoint.ToString() + "</td>");
                sb.Append("<td>" + it.CANo + "</td>");
                sb.Append("<td>" + it.CAName + "</td>");
                sb.Append("<td>" + it.SRVRemark + "</td>");
                sb.Append("<td class=\"td-status\"><span class=\"label " + (it.SRVStatus ? "label-success" : "") + " radius\">" + (it.SRVStatus ? "有效" : "已失效") + "</span></td>");
                sb.Append("</tr>");
                r++;
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append(Paginat(bc.GetListCount(SRVNo, SRVName, SRVTypeNo1, SRVTypeNo2, SRVSPNo, CANo, SRVCalType), pageSize, page, 7));

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
            if (jp.getValue("Type") == "delete")
                result = deleteaction(jp);
            else if (jp.getValue("Type") == "update")
                result = updateaction(jp);
            else if (jp.getValue("Type") == "submit")
                result = submitaction(jp);
            else if (jp.getValue("Type") == "select")
                result = selectaction(jp);
            else if (jp.getValue("Type") == "jump")
                result = jumpaction(jp);
            else if (jp.getValue("Type") == "valid")
                result = validaction(jp);
            else if (jp.getValue("Type") == "getsubtype")
                result = getsubtypeaction(jp);
            else if (jp.getValue("Type") == "getsubtypes")
                result = getsubtypesaction(jp);
            else if (jp.getValue("Type") == "getcano")
                result = getcanoaction(jp);
            return result;
        }

        private string updateaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessService bc = new project.Business.Base.BusinessService();
                bc.load(jp.getValue("id"));

                collection.Add(new JsonStringValue("SRVNo", bc.Entity.SRVNo));
                collection.Add(new JsonStringValue("SRVName", bc.Entity.SRVName));
                collection.Add(new JsonStringValue("SRVTypeNo1", bc.Entity.SRVTypeNo1));
                collection.Add(new JsonStringValue("SRVTypeNo1Name", bc.Entity.SRVTypeNo1Name));
                collection.Add(new JsonStringValue("SRVTypeNo2", bc.Entity.SRVTypeNo2));
                collection.Add(new JsonStringValue("SRVTypeNo2Name", bc.Entity.SRVTypeNo2Name));
                collection.Add(new JsonStringValue("SRVSPNo", bc.Entity.SRVSPNo));
                collection.Add(new JsonStringValue("SRVSPName", bc.Entity.SRVSPName));
                collection.Add(new JsonStringValue("CANo", bc.Entity.CANo));
                collection.Add(new JsonStringValue("CAName", bc.Entity.CAName));
                collection.Add(new JsonStringValue("SRVCalType", bc.Entity.SRVCalType));
                collection.Add(new JsonStringValue("SRVRoundType", bc.Entity.SRVRoundType));
                collection.Add(new JsonStringValue("SRVDecimalPoint", bc.Entity.SRVDecimalPoint.ToString()));
                collection.Add(new JsonStringValue("SRVRate", bc.Entity.SRVRate.ToString("0.####")));
                collection.Add(new JsonStringValue("SRVTaxRate", bc.Entity.SRVTaxRate.ToString("0.####")));
                collection.Add(new JsonStringValue("SRVRemark", bc.Entity.SRVRemark));
                
                string subtype = "";
                int row = 0;
                Business.Base.BusinessServiceType bs = new Business.Base.BusinessServiceType();
                foreach (Entity.Base.EntityServiceType it in bs.GetListQuery(string.Empty, string.Empty, bc.Entity.SRVTypeNo1,string.Empty,true))
                {
                    subtype += it.SRVTypeNo + ":" + it.SRVTypeName + ";";
                    row++;
                }
                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));

                string subtype1 = "";
                int row1 = 0;
                Business.Base.BusinessChargeAccount ba = new Business.Base.BusinessChargeAccount();
                foreach (Entity.Base.EntityChargeAccount it in ba.GetListQuery(string.Empty, string.Empty, bc.Entity.SRVSPNo))
                {
                    subtype1 += it.CANo + ":" + it.CAName + " - " + it.CASPName + ";";
                    row1++;
                }
                collection.Add(new JsonNumericValue("row1", row1));
                collection.Add(new JsonStringValue("subtype1", subtype1));
            }
            catch
            { flag = "2"; }

            collection.Add(new JsonStringValue("type", "update"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string deleteaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessService bc = new project.Business.Base.BusinessService();
                bc.load(jp.getValue("id"));

                int r = bc.delete();
                if (r <= 0)
                    flag = "2";
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "delete"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SRVNoS"), jp.getValue("SRVNameS"), jp.getValue("SRVTypeNo1S"), jp.getValue("SRVTypeNo2S"),
                jp.getValue("SRVSPNoS"), jp.getValue("CANoS"), jp.getValue("SRVCalTypeS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string submitaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessService bc = new project.Business.Base.BusinessService();
                if (jp.getValue("tp") == "update")
                {
                    bc.load(jp.getValue("id"));
                    bc.Entity.SRVName = jp.getValue("SRVName");
                    bc.Entity.SRVTypeNo1 = jp.getValue("SRVTypeNo1");
                    bc.Entity.SRVTypeNo2 = jp.getValue("SRVTypeNo2");
                    bc.Entity.SRVSPNo = jp.getValue("SRVSPNo");
                    bc.Entity.CANo = jp.getValue("CANo");
                    bc.Entity.SRVCalType = jp.getValue("SRVCalType");
                    bc.Entity.SRVRoundType = jp.getValue("SRVRoundType");
                    bc.Entity.SRVDecimalPoint = ParseIntForString(jp.getValue("SRVDecimalPoint"));
                    bc.Entity.SRVRate = ParseDecimalForString(jp.getValue("SRVRate"));
                    bc.Entity.SRVTaxRate = ParseDecimalForString(jp.getValue("SRVTaxRate"));
                    bc.Entity.SRVRemark = jp.getValue("SRVRemark");
                    int r = bc.Save("update");
                    
                    if (r <= 0)
                        flag = "2";
                }
                else
                {
                    Data obj = new Data();
                    DataTable dt = obj.PopulateDataSet("select cnt=COUNT(*) from Mstr_Service where SRVNo='" + jp.getValue("SRVNo") + "'").Tables[0];
                    if (int.Parse(dt.Rows[0]["cnt"].ToString()) > 0)
                        flag = "3";
                    else
                    {
                        bc.Entity.SRVNo = jp.getValue("SRVNo");
                        bc.Entity.SRVName = jp.getValue("SRVName");
                        bc.Entity.SRVTypeNo1 = jp.getValue("SRVTypeNo1");;
                        bc.Entity.SRVTypeNo2 = jp.getValue("SRVTypeNo2");
                        bc.Entity.SRVSPNo = jp.getValue("SRVSPNo");
                        bc.Entity.CANo = jp.getValue("CANo");
                        bc.Entity.SRVCalType = jp.getValue("SRVCalType");
                        bc.Entity.SRVRoundType = jp.getValue("SRVRoundType");
                        bc.Entity.SRVDecimalPoint = ParseIntForString(jp.getValue("SRVDecimalPoint"));
                        bc.Entity.SRVRate = ParseDecimalForString(jp.getValue("SRVRate"));
                        bc.Entity.SRVTaxRate = ParseDecimalForString(jp.getValue("SRVTaxRate"));
                        bc.Entity.SRVRemark = jp.getValue("SRVRemark");

                        int r = bc.Save("insert");
                        if (r <= 0)
                            flag = "2";
                    }
                }
            }
            catch { flag = "2"; }


            collection.Add(new JsonStringValue("type", "submit"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SRVNoS"), jp.getValue("SRVNameS"), jp.getValue("SRVTypeNo1S"), jp.getValue("SRVTypeNo2S"),
                jp.getValue("SRVSPNoS"), jp.getValue("CANoS"), jp.getValue("SRVCalTypeS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string selectaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "select"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SRVNoS"), jp.getValue("SRVNameS"), jp.getValue("SRVTypeNo1S"), jp.getValue("SRVTypeNo2S"),
                jp.getValue("SRVSPNoS"), jp.getValue("CANoS"), jp.getValue("SRVCalTypeS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string jumpaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";

            collection.Add(new JsonStringValue("type", "jump"));
            collection.Add(new JsonStringValue("flag", flag));
            collection.Add(new JsonStringValue("liststr", createList(jp.getValue("SRVNoS"), jp.getValue("SRVNameS"), jp.getValue("SRVTypeNo1S"), jp.getValue("SRVTypeNo2S"),
                jp.getValue("SRVSPNoS"), jp.getValue("CANoS"), jp.getValue("SRVCalTypeS"), ParseIntForString(jp.getValue("page")))));

            return collection.ToString();
        }
        private string validaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            try
            {
                Business.Base.BusinessService bc = new project.Business.Base.BusinessService();
                bc.load(jp.getValue("id"));
                bc.Entity.SRVStatus = !bc.Entity.SRVStatus;

                int r = bc.valid();
                if (r <= 0) flag = "2";
                if (bc.Entity.SRVStatus)
                    collection.Add(new JsonStringValue("stat", "<span class=\"label label-success radius\">有效</span>"));
                else
                    collection.Add(new JsonStringValue("stat", "<span class=\"label radius\">已失效</span>"));
                collection.Add(new JsonStringValue("id", jp.getValue("id")));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "valid"));
            collection.Add(new JsonStringValue("flag", flag));
            return collection.ToString();
        }

        private string getsubtypeaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string subtype = "";
            string type = "empty";
            if (jp.getValue("SRVTypeNo1") != "") type = jp.getValue("SRVTypeNo1");
            try
            {
                int row = 0;
                Business.Base.BusinessServiceType bt = new Business.Base.BusinessServiceType();
                foreach (Entity.Base.EntityServiceType it in bt.GetListQuery(string.Empty, string.Empty, type, string.Empty, true))
                {
                    subtype += it.SRVTypeNo + ":" + it.SRVTypeName + ";";
                    row++;
                }

                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getsubtype"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string getsubtypesaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string subtype = "";
            string type = "empty";
            if (jp.getValue("SRVTypeNo1") != "") type = jp.getValue("SRVTypeNo1");
            try
            {
                int row = 0;
                Business.Base.BusinessServiceType bt = new Business.Base.BusinessServiceType();
                foreach (Entity.Base.EntityServiceType it in bt.GetListQuery(string.Empty, string.Empty, type, string.Empty, true))
                {
                    subtype += it.SRVTypeNo + ":" + it.SRVTypeName + ";";
                    row++;
                }

                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getsubtypes"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
        private string getcanoaction(JsonArrayParse jp)
        {
            JsonObjectCollection collection = new JsonObjectCollection();
            string flag = "1";
            string subtype = "";
            string type = "empty";
            if (jp.getValue("SRVSPNo") != "") type = jp.getValue("SRVSPNo");
            try
            {
                int row = 0;
                Business.Base.BusinessChargeAccount bt = new Business.Base.BusinessChargeAccount();
                foreach (Entity.Base.EntityChargeAccount it in bt.GetListQuery(string.Empty, string.Empty, type))
                {
                    subtype += it.CANo + ":" + it.CAName + " - " + it.CASPName + ";";
                    row++;
                }

                collection.Add(new JsonNumericValue("row", row));
                collection.Add(new JsonStringValue("subtype", subtype));
            }
            catch { flag = "2"; }

            collection.Add(new JsonStringValue("type", "getcano"));
            collection.Add(new JsonStringValue("flag", flag));

            return collection.ToString();
        }
    }
}