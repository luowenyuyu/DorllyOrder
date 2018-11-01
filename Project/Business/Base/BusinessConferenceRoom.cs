using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 会议室资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessConferenceRoom : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityConferenceRoom _entity = new project.Entity.Base.EntityConferenceRoom();
        public string OrderField = "CRNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessConferenceRoom() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessConferenceRoom(project.Entity.Base.EntityConferenceRoom entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityConferenceRoom)关联
        /// </summary>
        public project.Entity.Base.EntityConferenceRoom Entity
        {
            get { return _entity as project.Entity.Base.EntityConferenceRoom; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.CustName as CRCurrentCustName " +
                "from Mstr_ConferenceRoom a left join Mstr_Customer b on a.CRCurrentCustNo=b.CustNo "+
                "where a.CRNo='" + id + "' "
                ).Tables[0].Rows[0];
            _entity.CRNo = dr["CRNo"].ToString();
            _entity.CRName = dr["CRName"].ToString();
            _entity.CRCapacity = dr["CRCapacity"].ToString();
            _entity.CRINPriceHour = ParseDecimalForString(dr["CRINPriceHour"].ToString());
            _entity.CRINPriceHalfDay = ParseDecimalForString(dr["CRINPriceHalfDay"].ToString());
            _entity.CRINPriceDay = ParseDecimalForString(dr["CRINPriceDay"].ToString());
            _entity.CROUTPriceHour = ParseDecimalForString(dr["CROUTPriceHour"].ToString());
            _entity.CROUTPriceHalfDay = ParseDecimalForString(dr["CROUTPriceHalfDay"].ToString());
            _entity.CROUTPriceDay = ParseDecimalForString(dr["CROUTPriceDay"].ToString());
            _entity.CRDeposit = ParseDecimalForString(dr["CRDeposit"].ToString());
            _entity.CRAddr = dr["CRAddr"].ToString();
            _entity.CRISEnable = bool.Parse(dr["CRISEnable"].ToString());
            _entity.CRReservedDate = ParseDateTimeForString(dr["CRReservedDate"].ToString());
            _entity.CRBegReservedDate = ParseDateTimeForString(dr["CRBegReservedDate"].ToString());
            _entity.CREndReservedDate = ParseDateTimeForString(dr["CREndReservedDate"].ToString());
            _entity.CRCurrentCustNo = dr["CRCurrentCustNo"].ToString();
            _entity.CRCurrentCustName = dr["CRCurrentCustName"].ToString();
            _entity.CRStatus = dr["CRStatus"].ToString();
            _entity.CRCreateDate = ParseDateTimeForString(dr["CRCreateDate"].ToString());
            _entity.CRCreator = dr["CRCreator"].ToString();
            _entity.IsStatistics = bool.Parse(string.IsNullOrEmpty(dr["IsStatistics"].ToString()) ? "0" : dr["IsStatistics"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_ConferenceRoom(CRNo,CRName,CRCapacity,CRINPriceHour,CRINPriceHalfDay,CRINPriceDay,CROUTPriceHour,CROUTPriceHalfDay,CROUTPriceDay," +
                        "CRDeposit,CRAddr,CRISEnable,CRStatus,CRReservedDate,CRBegReservedDate,CREndReservedDate,CRCurrentCustNo,CRCreateDate,CRCreator)" +
                    "values('" + Entity.CRNo + "'" + "," + "'" + Entity.CRName + "'" + "," + "'" + Entity.CRCapacity + "'" + "," + Entity.CRINPriceHour + "," +
                    Entity.CRINPriceHalfDay + "," + Entity.CRINPriceDay + "," + Entity.CROUTPriceHour + "," + Entity.CROUTPriceHalfDay + "," + Entity.CROUTPriceDay + "," +
                    Entity.CRDeposit + "," + "'" + Entity.CRAddr + "'" + "," + "0,'free',null,null,null,''," +
                    "'" + Entity.CRCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + Entity.CRCreator + "')";
            else
                sqlstr = "update Mstr_ConferenceRoom" +
                    " set CRName=" + "'" + Entity.CRName + "'" + "," +
                    "CRCapacity=" + "'" + Entity.CRCapacity + "'" + "," + "CRINPriceHour=" + Entity.CRINPriceHour + "," +
                    "CRINPriceHalfDay=" + Entity.CRINPriceHalfDay + "," + "CRINPriceDay=" + Entity.CRINPriceDay + "," +
                    "CROUTPriceHour=" + Entity.CROUTPriceHour + "," + "CROUTPriceHalfDay=" + Entity.CROUTPriceHalfDay + "," +
                    "CROUTPriceDay=" + Entity.CROUTPriceDay + "," + "CRDeposit=" + Entity.CRDeposit + "," + "CRAddr=" + "'" + Entity.CRAddr + "'" +
                    " where CRNo='" + Entity.CRNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_ConferenceRoom where CRNo='" + Entity.CRNo + "'");
        }

        /// </summary>
        /// 禁用/启用 
        /// </summary>
        /// <returns></returns>
        public int valid()
        {
            return objdata.ExecuteNonQuery("update Mstr_ConferenceRoom set CRISEnable=" + (Entity.CRISEnable == true ? "1" : "0") + " where CRNo='" + Entity.CRNo + "'");
        }

        /// </summary>
        /// 预定 
        /// </summary>
        /// <returns></returns>
        public int reserve()
        {
            return objdata.ExecuteNonQuery("update Mstr_ConferenceRoom "+
                "set CRReservedDate = '" + Entity.CRReservedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "CRBegReservedDate = '" + Entity.CRBegReservedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "CREndReservedDate = '" + Entity.CREndReservedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "CRCurrentCustNo='" + Entity.CRCurrentCustNo + "',"+
                "CRStatus='reserve' " +
                "where CRNo='" + Entity.CRNo + "'");
        }
        
        /// <summary>
        /// 取消预定
        /// </summary>
        /// <returns></returns>
        public int unreserve()
        {
            return objdata.ExecuteNonQuery("update Mstr_ConferenceRoom " +
                "set CRReservedDate = null," +
                "CRBegReservedDate = null," +
                "CREndReservedDate = null," +
                "CRCurrentCustNo=''," +
                "CRStatus='free' " +
                "where CRNo='" + Entity.CRNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="CRNo">会议室编号</param>
        /// <param name="CRName">会议室名称</param>
        /// <param name="CRAddr">园区</param>
        /// <param name="CRStatus">状态</param>
        /// <param name="startRow">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string CRNo, string CRName, string CRAddr, string CRStatus, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(CRNo, CRName, CRAddr, CRStatus, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="CRNo">会议室编号</param>
        /// <param name="CRName">会议室名称</param>
        /// <param name="CRAddr">园区</param>
        /// <param name="CRStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string CRNo, string CRName, string CRAddr, string CRStatus)
        {
            return GetListHelper(CRNo, CRName, CRAddr, CRStatus, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="CRNo">会议室编号</param>
        /// <param name="CRName">会议室名称</param>
        /// <param name="CRAddr">园区</param>
        /// <param name="CRStatus">状态</param>
        /// <returns></returns>
        public int GetListCount(string CRNo, string CRName, string CRAddr, string CRStatus)
        {
            string wherestr = "";
            if (CRNo != string.Empty)
            {
                wherestr = wherestr + " and CRNo like '%" + CRNo + "%'";
            }
            if (CRName != string.Empty)
            {
                wherestr = wherestr + " and CRName like '%" + CRName + "%'";
            }
            if (CRAddr != string.Empty)
            {
                wherestr = wherestr + " and CRAddr like '%" + CRAddr + "%'";
            }
            if (CRStatus != string.Empty)
            {
                wherestr = wherestr + " and CRStatus = '" + CRStatus + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_ConferenceRoom where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="CRNo">会议室编号</param>
        /// <param name="CRName">会议室名称</param>
        /// <param name="CRAddr">园区</param>
        /// <param name="CRStatus">状态</param>
        /// <param name="startRow">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string CRNo, string CRName, string CRAddr, string CRStatus, int startRow, int pageSize)
        {
            string wherestr = "";
            if (CRNo != string.Empty)
            {
                wherestr = wherestr + " and CRNo like '%" + CRNo + "%'";
            }
            if (CRName != string.Empty)
            {
                wherestr = wherestr + " and CRName like '%" + CRName + "%'";
            }
            if (CRAddr != string.Empty)
            {
                wherestr = wherestr + " and CRAddr like '%" + CRAddr + "%'";
            }
            if (CRStatus != string.Empty)
            {
                wherestr = wherestr + " and CRStatus = '" + CRStatus + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_ConferenceRoom a left join Mstr_Customer b on a.CRCurrentCustNo=b.CustNo",
                    "a.*,b.CustName as CRCurrentCustName ", 
                    wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_ConferenceRoom a left join Mstr_Customer b on a.CRCurrentCustNo=b.CustNo ",
                    "a.*,b.CustName as CRCurrentCustName ", 
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
                project.Entity.Base.EntityConferenceRoom entity = new project.Entity.Base.EntityConferenceRoom();
                entity.CRNo = dr["CRNo"].ToString();
                entity.CRName = dr["CRName"].ToString();
                entity.CRCapacity = dr["CRCapacity"].ToString();
                entity.CRINPriceHour = ParseDecimalForString(dr["CRINPriceHour"].ToString());
                entity.CRINPriceHalfDay = ParseDecimalForString(dr["CRINPriceHalfDay"].ToString());
                entity.CRINPriceDay = ParseDecimalForString(dr["CRINPriceDay"].ToString());
                entity.CROUTPriceHour = ParseDecimalForString(dr["CROUTPriceHour"].ToString());
                entity.CROUTPriceHalfDay = ParseDecimalForString(dr["CROUTPriceHalfDay"].ToString());
                entity.CROUTPriceDay = ParseDecimalForString(dr["CROUTPriceDay"].ToString());
                entity.CRDeposit = ParseDecimalForString(dr["CRDeposit"].ToString());
                entity.CRAddr = dr["CRAddr"].ToString();
                entity.CRISEnable = bool.Parse(dr["CRISEnable"].ToString());
                entity.CRReservedDate = ParseDateTimeForString(dr["CRReservedDate"].ToString());
                entity.CREndReservedDate = ParseDateTimeForString(dr["CREndReservedDate"].ToString());
                entity.CRCurrentCustNo = dr["CRCurrentCustNo"].ToString();
                entity.CRCurrentCustName = dr["CRCurrentCustName"].ToString();
                entity.CRStatus = dr["CRStatus"].ToString();
                entity.CRCreateDate = ParseDateTimeForString(dr["CRCreateDate"].ToString());
                entity.CRCreator = dr["CRCreator"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
