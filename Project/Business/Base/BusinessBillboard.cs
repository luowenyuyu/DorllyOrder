using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 广告位资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessBillboard : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityBillboard _entity = new project.Entity.Base.EntityBillboard();
        public string OrderField = "BBNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessBillboard() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessBillboard(project.Entity.Base.EntityBillboard entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityBillboard)关联
        /// </summary>
        public project.Entity.Base.EntityBillboard Entity
        {
            get { return _entity as project.Entity.Base.EntityBillboard; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.CustName as BBCurrentCustName,c.SPShortName as SPName,d.LOCName as BBLOCName,e.BBTypeName as BBTypeName " +
                "from Mstr_Billboard a left join Mstr_Customer b on a.BBCurrentCustNo=b.CustNo "+
                "left join Mstr_ServiceProvider c on c.SPNo=a.BBSPNo " +
                "left join Mstr_Location d on d.LOCNo=a.BBLOCNo " +
                "left join Mstr_BillboardType e on e.BBTypeNo=a.BBType " +
                "where a.BBNo='" + id + "' "
                ).Tables[0].Rows[0];
            _entity.BBNo = dr["BBNo"].ToString();
            _entity.BBName = dr["BBName"].ToString();
            _entity.BBSPNo = dr["BBSPNo"].ToString();
            _entity.BBSPName = dr["SPName"].ToString();
            _entity.BBLOCNo = dr["BBLOCNo"].ToString();
            _entity.BBLOCName = dr["BBLOCName"].ToString();
            _entity.BBAddr = dr["BBAddr"].ToString();
            _entity.BBSize = dr["BBSize"].ToString();
            _entity.BBType = dr["BBType"].ToString();
            _entity.BBTypeName = dr["BBTypeName"].ToString();
            _entity.BBINPriceDay = ParseDecimalForString(dr["BBINPriceDay"].ToString());
            _entity.BBOUTPriceDay = ParseDecimalForString(dr["BBOUTPriceDay"].ToString());
            _entity.BBINPriceMonth = ParseDecimalForString(dr["BBINPriceMonth"].ToString());
            _entity.BBOUTPriceMonth = ParseDecimalForString(dr["BBOUTPriceMonth"].ToString());
            _entity.BBINPriceQuarter = ParseDecimalForString(dr["BBINPriceQuarter"].ToString());
            _entity.BBOUTPriceQuarter = ParseDecimalForString(dr["BBOUTPriceQuarter"].ToString());
            _entity.BBINPriceYear = ParseDecimalForString(dr["BBINPriceYear"].ToString());
            _entity.BBOUTPriceYear = ParseDecimalForString(dr["BBOUTPriceYear"].ToString());
            _entity.BBDeposit = ParseDecimalForString(dr["BBDeposit"].ToString());
            _entity.BBImage = dr["BBImage"].ToString();
            _entity.BBISEnable = bool.Parse(dr["BBISEnable"].ToString());
            _entity.BBCurrentCustNo = dr["BBCurrentCustNo"].ToString();
            _entity.BBCurrentCustName = dr["BBCurrentCustName"].ToString();
            _entity.BBStatus = dr["BBStatus"].ToString();
            _entity.BBCreateDate = ParseDateTimeForString(dr["BBCreateDate"].ToString());
            _entity.BBCreator = dr["BBCreator"].ToString();
            _entity.IsStatistics = bool.Parse(string.IsNullOrEmpty(dr["IsStatistics"].ToString()) ? "false" : dr["IsStatistics"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_Billboard(BBNo,BBName,BBSPNo,BBLOCNo,BBAddr,BBSize,BBType,"+
                        "BBINPriceDay,BBOUTPriceDay,BBINPriceMonth,BBOUTPriceMonth,BBINPriceQuarter,BBOUTPriceQuarter,BBINPriceYear,BBOUTPriceYear," +
                        "BBDeposit,BBImage,BBISEnable,BBStatus,BBCurrentCustNo,BBCreateDate,BBCreator,IsStatistics)" +
                    "values('" + Entity.BBNo + "'" + "," + "'" + Entity.BBName + "'" + "," + "'" + Entity.BBSPNo + "'" + "," + "'" + Entity.BBLOCNo + "'" + "," +
                    "'" + Entity.BBAddr + "'" + "," + "'" + Entity.BBSize + "'" + "," + "'" + Entity.BBType + "'" + "," +
                    Entity.BBINPriceDay + "," + Entity.BBOUTPriceDay + "," + Entity.BBINPriceMonth + "," + Entity.BBOUTPriceMonth + "," +
                    Entity.BBINPriceQuarter + "," + Entity.BBOUTPriceQuarter + "," + Entity.BBINPriceYear + "," + Entity.BBOUTPriceYear + "," +
                    Entity.BBDeposit + "," + "'" + Entity.BBImage + "'" + "," + "0,'free',''," +
                    "'" + Entity.BBCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + Entity.BBCreator + "'," + (Entity.IsStatistics == true ? "1" : "0") + ")";
            else
                sqlstr = "update Mstr_Billboard" +
                    " set BBName=" + "'" + Entity.BBName + "'" + "," + "BBSPNo=" + "'" + Entity.BBSPNo + "'" + "," + "BBLOCNo=" + "'" + Entity.BBLOCNo + "'" + "," +
                    "BBAddr=" + "'" + Entity.BBAddr + "'" + "," + "BBSize=" + "'" + Entity.BBSize + "'" + "," + "BBType=" + "'" + Entity.BBType + "'" + "," +
                    "BBINPriceDay=" + Entity.BBINPriceDay + "," + "BBOUTPriceDay=" + Entity.BBOUTPriceDay + "," +
                    "BBINPriceMonth=" + Entity.BBINPriceMonth + "," + "BBOUTPriceMonth=" + Entity.BBOUTPriceMonth + "," +
                    "BBINPriceQuarter=" + Entity.BBINPriceQuarter + "," + "BBOUTPriceYear=" + Entity.BBOUTPriceYear + "," +
                    "BBDeposit=" + Entity.BBDeposit + "," + "BBImage=" + "'" + Entity.BBImage + "'" + "," +
                    "IsStatistics=" + (Entity.IsStatistics == true ? "1" : "0") +
                    " where BBNo='" + Entity.BBNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_Billboard where BBNo='" + Entity.BBNo + "'");
        }

        /// </summary>
        /// 禁用/启用 
        /// </summary>
        /// <returns></returns>
        public int valid()
        {
            return objdata.ExecuteNonQuery("update Mstr_Billboard set BBISEnable=" + (Entity.BBISEnable == true ? "1" : "0") + " where BBNo='" + Entity.BBNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="BBNo">广告位编号</param>
        /// <param name="BBName">广告位名称</param>
        /// <param name="BBAddr">位置</param>
        /// <param name="BBStatus">状态</param>
        /// <param name="BBType">广告位类型</param>
        /// <param name="BBSPNo">服务商</param>
        /// <param name="startRow">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string BBNo, string BBName, string BBAddr, string BBStatus, string BBType, string BBSPNo, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(BBNo, BBName, BBAddr, BBStatus, BBType, BBSPNo, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="BBNo">广告位编号</param>
        /// <param name="BBName">广告位名称</param>
        /// <param name="BBAddr">位置</param>
        /// <param name="BBStatus">状态</param>
        /// <param name="BBType">广告位类型</param>
        /// <param name="BBSPNo">服务商</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string BBNo, string BBName, string BBAddr, string BBStatus, string BBType, string BBSPNo)
        {
            return GetListHelper(BBNo, BBName, BBAddr, BBStatus, BBType, BBSPNo, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="BBNo">广告位编号</param>
        /// <param name="BBName">广告位名称</param>
        /// <param name="BBAddr">位置</param>
        /// <param name="BBStatus">状态</param>
        /// <param name="BBType">广告位类型</param>
        /// <param name="BBSPNo">服务商</param>
        /// <returns></returns>
        public int GetListCount(string BBNo, string BBName, string BBAddr, string BBStatus, string BBType, string BBSPNo)
        {
            string wherestr = "";
            if (BBNo != string.Empty)
            {
                wherestr = wherestr + " and BBNo like '%" + BBNo + "%'";
            }
            if (BBName != string.Empty)
            {
                wherestr = wherestr + " and BBName like '%" + BBName + "%'";
            }
            if (BBAddr != string.Empty)
            {
                wherestr = wherestr + " and BBAddr like '%" + BBAddr + "%'";
            }
            if (BBStatus != string.Empty)
            {
                wherestr = wherestr + " and BBStatus = '" + BBStatus + "'";
            }
            if (BBType != string.Empty)
            {
                wherestr = wherestr + " and BBType = '" + BBType + "'";
            }
            if (BBSPNo != string.Empty)
            {
                wherestr = wherestr + " and BBSPNo = '" + BBSPNo + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_Billboard where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="BBNo">广告位编号</param>
        /// <param name="BBName">广告位名称</param>
        /// <param name="BBAddr">位置</param>
        /// <param name="BBStatus">状态</param>
        /// <param name="BBType">广告位类型</param>
        /// <param name="BBSPNo">服务商</param>
        /// <param name="startRow">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string BBNo, string BBName, string BBAddr, string BBStatus, string BBType, string BBSPNo, int startRow, int pageSize)
        {
            string wherestr = "";
            if (BBNo != string.Empty)
            {
                wherestr = wherestr + " and BBNo like '%" + BBNo + "%'";
            }
            if (BBName != string.Empty)
            {
                wherestr = wherestr + " and BBName like '%" + BBName + "%'";
            }
            if (BBAddr != string.Empty)
            {
                wherestr = wherestr + " and BBAddr like '%" + BBAddr + "%'";
            }
            if (BBStatus != string.Empty)
            {
                wherestr = wherestr + " and BBStatus = '" + BBStatus + "'";
            }
            if (BBType != string.Empty)
            {
                wherestr = wherestr + " and BBType = '" + BBType + "'";
            }
            if (BBSPNo != string.Empty)
            {
                wherestr = wherestr + " and BBSPNo = '" + BBSPNo + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_Billboard a left join Mstr_Customer b on a.BBCurrentCustNo=b.CustNo "+
                    "left join Mstr_ServiceProvider c on c.SPNo=a.BBSPNo " +
                    "left join Mstr_Location d on d.LOCNo=a.BBLOCNo " +
                    "left join Mstr_BillboardType e on e.BBTypeNo=a.BBType",
                    "a.*,b.CustName as BBCurrentCustName,c.SPShortName as SPName,d.LOCName as BBLOCName,e.BBTypeName as BBTypeName ", 
                    wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_Billboard a left join Mstr_Customer b on a.BBCurrentCustNo=b.CustNo " +
                    "left join Mstr_ServiceProvider c on c.SPNo=a.BBSPNo " +
                    "left join Mstr_Location d on d.LOCNo=a.BBLOCNo " +
                    "left join Mstr_BillboardType e on e.BBTypeNo=a.BBType",
                    "a.*,b.CustName as BBCurrentCustName,c.SPShortName as SPName,d.LOCName as BBLOCName,e.BBTypeName as BBTypeName ", 
                    wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
            }
            return entitys;
        }
        
        /// </summary>
        ///Query 方法 dt查询结果
        /// </summary>
        public System.Collections.IList Query(System.Data.DataTable dt)
        {
            System.Collections.IList result = new System.Collections.ArrayList();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                project.Entity.Base.EntityBillboard entity = new project.Entity.Base.EntityBillboard();
                entity.BBNo = dr["BBNo"].ToString();
                entity.BBName = dr["BBName"].ToString();
                entity.BBSPNo = dr["BBSPNo"].ToString();
                entity.BBSPName = dr["SPName"].ToString();
                entity.BBLOCNo = dr["BBLOCNo"].ToString();
                entity.BBLOCName = dr["BBLOCName"].ToString();
                entity.BBAddr = dr["BBAddr"].ToString();
                entity.BBSize = dr["BBSize"].ToString();
                entity.BBType = dr["BBType"].ToString();
                entity.BBTypeName = dr["BBTypeName"].ToString();
                entity.BBINPriceDay = ParseDecimalForString(dr["BBINPriceDay"].ToString());
                entity.BBOUTPriceDay = ParseDecimalForString(dr["BBOUTPriceDay"].ToString());
                entity.BBINPriceMonth = ParseDecimalForString(dr["BBINPriceMonth"].ToString());
                entity.BBOUTPriceMonth = ParseDecimalForString(dr["BBOUTPriceMonth"].ToString());
                entity.BBINPriceQuarter = ParseDecimalForString(dr["BBINPriceQuarter"].ToString());
                entity.BBOUTPriceQuarter = ParseDecimalForString(dr["BBOUTPriceQuarter"].ToString());
                entity.BBINPriceYear = ParseDecimalForString(dr["BBINPriceYear"].ToString());
                entity.BBOUTPriceYear = ParseDecimalForString(dr["BBOUTPriceYear"].ToString());
                entity.BBDeposit = ParseDecimalForString(dr["BBDeposit"].ToString());
                entity.BBImage = dr["BBImage"].ToString();
                entity.BBISEnable = bool.Parse(dr["BBISEnable"].ToString());
                entity.BBCurrentCustNo = dr["BBCurrentCustNo"].ToString();
                entity.BBCurrentCustName = dr["BBCurrentCustName"].ToString();
                entity.BBStatus = dr["BBStatus"].ToString();
                entity.BBCreateDate = ParseDateTimeForString(dr["BBCreateDate"].ToString());
                entity.BBCreator = dr["BBCreator"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
