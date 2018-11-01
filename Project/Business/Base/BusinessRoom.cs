using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 客户资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessRoom : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityRoom _entity = new project.Entity.Base.EntityRoom();
        public string OrderField = "a.RMID";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessRoom() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessRoom(project.Entity.Base.EntityRoom entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityRoom)关联
        /// </summary>
        public project.Entity.Base.EntityRoom Entity
        {
            get { return _entity as project.Entity.Base.EntityRoom; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.CustShortName as RMCurrentCustName,c.LOCName as RMLOCNo1Name," +
                "d.LOCName as RMLOCNo2Name,e.LOCName as RMLOCNo3Name,f.LOCName as RMLOCNo4Name " +
                "from Mstr_Room a left join Mstr_Customer b on a.RMCurrentCustNo=b.CustNo "+
                "left join Mstr_Location c on c.LOCNo=a.RMLOCNo1 " +
                "left join Mstr_Location d on d.LOCNo=a.RMLOCNo2 " +
                "left join Mstr_Location e on e.LOCNo=a.RMLOCNo3 " +
                "left join Mstr_Location f on f.LOCNo=a.RMLOCNo4 " +
                "where a.RMID='" + id + "' "
                ).Tables[0].Rows[0];
            _entity.RMID = dr["RMID"].ToString();
            _entity.RMNo = dr["RMNo"].ToString();
            _entity.RMLOCNo1 = dr["RMLOCNo1"].ToString();
            _entity.RMLOCNo1Name = dr["RMLOCNo1Name"].ToString();
            _entity.RMLOCNo2 = dr["RMLOCNo2"].ToString();
            _entity.RMLOCNo2Name = dr["RMLOCNo2Name"].ToString();
            _entity.RMLOCNo3 = dr["RMLOCNo3"].ToString();
            _entity.RMLOCNo3Name = dr["RMLOCNo3Name"].ToString();
            _entity.RMLOCNo4 = dr["RMLOCNo4"].ToString();
            _entity.RMLOCNo4Name = dr["RMLOCNo4Name"].ToString();
            _entity.RMRentType = dr["RMRentType"].ToString();
            _entity.RMBuildSize = ParseDecimalForString(dr["RMBuildSize"].ToString());
            _entity.RMRentSize = ParseDecimalForString(dr["RMRentSize"].ToString());
            _entity.RMAddr = dr["RMAddr"].ToString();
            _entity.RMRemark = dr["RMRemark"].ToString();
            _entity.RMISEnable = bool.Parse(dr["RMISEnable"].ToString());
            _entity.RMStatus = dr["RMStatus"].ToString();
            _entity.RMReservedDate = ParseDateTimeForString(dr["RMReservedDate"].ToString());
            _entity.RMEndReservedDate = ParseDateTimeForString(dr["RMEndReservedDate"].ToString());
            _entity.RMCurrentCustNo = dr["RMCurrentCustNo"].ToString();
            _entity.RMCurrentCustName = dr["RMCurrentCustName"].ToString();
            _entity.RMCreateDate = ParseDateTimeForString(dr["RMCreateDate"].ToString());
            _entity.RMCreator = dr["RMCreator"].ToString();
            _entity.HaveAirCondition = bool.Parse(dr["HaveAirCondition"].ToString());
            _entity.IsStatistics = bool.Parse(string.IsNullOrEmpty(dr["IsStatistics"].ToString()) ? "false" : dr["IsStatistics"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_Room(RMID,RMNo,RMLOCNo1,RMLOCNo2,RMLOCNo3,RMLOCNo4,RMRentType,RMBuildSize,RMRentSize,RMAddr,RMRemark,RMISEnable," +
                        "RMStatus,RMReservedDate,RMEndReservedDate,RMCurrentCustNo,RMCreateDate,RMCreator,HaveAirCondition,IsStatistics)" +
                    "values('" + Entity.RMID + "'" + "," + "'" + Entity.RMNo + "'" + "," + "'" + Entity.RMLOCNo1 + "'" + "," + "'" + Entity.RMLOCNo2 + "'" + "," +
                    "'" + Entity.RMLOCNo3 + "'" + "," + "'" + Entity.RMLOCNo4 + "'" + "," + "'" + Entity.RMRentType + "'" + "," + Entity.RMBuildSize + "," +
                    Entity.RMRentSize + "," + "'" + Entity.RMAddr + "'" + "," + "'" + Entity.RMRemark + "'" + "," + "0,'free',null,null,''," +
                    "'" + Entity.RMCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + Entity.RMCreator + "'" + "," +
                    (Entity.HaveAirCondition == true ? "1" : "0") + "," + (Entity.IsStatistics == true ? "1" : "0") + ")";
            else
                sqlstr = "update Mstr_Room" +
                    " set RMLOCNo1=" + "'" + Entity.RMLOCNo1 + "'" + "," +
                    "RMLOCNo2=" + "'" + Entity.RMLOCNo2 + "'" + "," + "RMLOCNo3=" + "'" + Entity.RMLOCNo3 + "'" + "," +
                    "RMLOCNo4=" + "'" + Entity.RMLOCNo4 + "'" + "," + "RMRentType=" + "'" + Entity.RMRentType + "'" + "," +
                    "RMBuildSize=" + Entity.RMBuildSize + "," + "RMRentSize=" + Entity.RMRentSize + "," +
                    "RMAddr=" + "'" + Entity.RMAddr + "'" + "," + "RMRemark=" + "'" + Entity.RMRemark + "'" + "," +
                    "HaveAirCondition=" + (Entity.HaveAirCondition == true ? "1" : "0") + "," +
                    "IsStatistics=" + (Entity.IsStatistics == true ? "1" : "0")+
                    " where RMID='" + Entity.RMID + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_Room where RMID='" + Entity.RMID + "'");
        }

        /// </summary>
        /// 禁用/启用 
        /// </summary>
        /// <returns></returns>
        public int valid()
        {
            return objdata.ExecuteNonQuery("update Mstr_Room set RMISEnable=" + (Entity.RMISEnable == true ? "1" : "0") + " where RMID='" + Entity.RMID + "'");
        }

        /// </summary>
        /// 预留 
        /// </summary>
        /// <returns></returns>
        public int reserve()
        {
            return objdata.ExecuteNonQuery("update Mstr_Room "+
                "set RMReservedDate = '" + Entity.RMReservedDate.ToString("yyyy-MM-dd HH:mm:ss") + "',"+
                "RMEndReservedDate = '" + Entity.RMEndReservedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "RMCurrentCustNo='" + Entity.RMCurrentCustNo + "',"+
                "RMStatus='reserve' " +
                "where RMID='" + Entity.RMID + "'");
        }
        
        /// <summary>
        /// 取消预留 
        /// </summary>
        /// <returns></returns>
        public int unreserve()
        {
            return objdata.ExecuteNonQuery("update Mstr_Room " +
                "set RMReservedDate = null," +
                "RMEndReservedDate = null," +
                "RMCurrentCustNo=''," +
                "RMStatus='free' " +
                "where RMID='" + Entity.RMID + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="RMID">房间编号</param>
        /// <param name="RMLOCNo1">园区</param>
        /// <param name="RMLOCNo2">建设期</param>
        /// <param name="RMLOCNo3">楼栋</param>
        /// <param name="RMLOCNo4">楼层</param>
        /// <param name="RMCurrentCustNo">客户编号</param>
        /// <param name="RMCurrentCustName">客户名称</param>
        /// <param name="RMStatus">状态</param>
        /// <param name="startRow">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string RMID, string RMLOCNo1, string RMLOCNo2, string RMLOCNo3, string RMLOCNo4, string RMCurrentCustNo, string RMCurrentCustName, string RMStatus, bool? RMISEnable, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(RMID, RMLOCNo1, RMLOCNo2, RMLOCNo3, RMLOCNo4, RMCurrentCustNo, RMCurrentCustName, RMStatus, RMISEnable, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="RMID">房间编号</param>
        /// <param name="RMLOCNo1">园区</param>
        /// <param name="RMLOCNo2">建设期</param>
        /// <param name="RMLOCNo3">楼栋</param>
        /// <param name="RMLOCNo4">楼层</param>
        /// <param name="RMCurrentCustNo">客户编号</param>
        /// <param name="RMCurrentCustName">客户名称</param>
        /// <param name="RMStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string RMID, string RMLOCNo1, string RMLOCNo2, string RMLOCNo3, string RMLOCNo4, string RMCurrentCustNo, string RMCurrentCustName, string RMStatus, bool? RMISEnable)
        {
            return GetListHelper(RMID, RMLOCNo1, RMLOCNo2, RMLOCNo3, RMLOCNo4, RMCurrentCustNo, RMCurrentCustName, RMStatus, RMISEnable, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="RMID">房间编号</param>
        /// <param name="RMLOCNo1">园区</param>
        /// <param name="RMLOCNo2">建设期</param>
        /// <param name="RMLOCNo3">楼栋</param>
        /// <param name="RMLOCNo4">楼层</param>
        /// <param name="RMCurrentCustNo">客户编号</param>
        /// <param name="RMCurrentCustName">客户名称</param>
        /// <param name="RMStatus">状态</param>
        /// <returns></returns>
        public int GetListCount(string RMID, string RMLOCNo1, string RMLOCNo2, string RMLOCNo3, string RMLOCNo4, string RMCurrentCustNo, string RMCurrentCustName, string RMStatus, bool? RMISEnable)
        {
            string wherestr = "";
            if (RMID != string.Empty)
            {
                wherestr = wherestr + " and a.RMID like '%" + RMID + "%'";
            }
            if (RMLOCNo1 != string.Empty)
            {
                wherestr = wherestr + " and a.RMLOCNo1 = '" + RMLOCNo1 + "'";
            }
            if (RMLOCNo2 != string.Empty)
            {
                wherestr = wherestr + " and a.RMLOCNo2 = '" + RMLOCNo2 + "'";
            }
            if (RMLOCNo3 != string.Empty)
            {
                wherestr = wherestr + " and a.RMLOCNo3 = '" + RMLOCNo3 + "'";
            }
            if (RMLOCNo4 != string.Empty)
            {
                wherestr = wherestr + " and a.RMLOCNo4 = '" + RMLOCNo4 + "'";
            }
            if (RMCurrentCustNo != string.Empty)
            {
                wherestr = wherestr + " and a.RMCurrentCustNo = '" + RMCurrentCustNo + "'";
            }
            if (RMCurrentCustName != string.Empty)
            {
                wherestr = wherestr + " and b.CustName like '%" + RMCurrentCustName + "%'";
            }
            if (RMStatus != string.Empty)
            {
                wherestr = wherestr + " and a.RMStatus = '" + RMStatus + "'";
            }
            if (RMISEnable != null)
            {
                wherestr = wherestr + " and a.RMISEnable = " + (RMISEnable == true ? "1" : "0");
            }

            string count = objdata.PopulateDataSet("select count(1) as cnt from Mstr_Room a left join Mstr_Customer b on a.RMCurrentCustNo=b.CustNo where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="RMID">房间编号</param>
        /// <param name="RMLOCNo1">园区</param>
        /// <param name="RMLOCNo2">建设期</param>
        /// <param name="RMLOCNo3">楼栋</param>
        /// <param name="RMLOCNo4">楼层</param>
        /// <param name="RMCurrentCustNo">客户编号</param>
        /// <param name="RMCurrentCustName">客户名称</param>
        /// <param name="RMStatus">状态</param>
        /// <param name="startRow">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string RMID, string RMLOCNo1, string RMLOCNo2, string RMLOCNo3, string RMLOCNo4, string RMCurrentCustNo, string RMCurrentCustName, string RMStatus, bool? RMISEnable, int startRow, int pageSize)
        {
            string wherestr = "";
            if (RMID != string.Empty)
            {
                wherestr = wherestr + " and a.RMID like '%" + RMID + "%'";
            }
            if (RMLOCNo1 != string.Empty)
            {
                wherestr = wherestr + " and a.RMLOCNo1 = '" + RMLOCNo1 + "'";
            }
            if (RMLOCNo2 != string.Empty)
            {
                wherestr = wherestr + " and a.RMLOCNo2 = '" + RMLOCNo2 + "'";
            }
            if (RMLOCNo3 != string.Empty)
            {
                wherestr = wherestr + " and a.RMLOCNo3 = '" + RMLOCNo3 + "'";
            }
            if (RMLOCNo4 != string.Empty)
            {
                wherestr = wherestr + " and a.RMLOCNo4 = '" + RMLOCNo4 + "'";
            }
            if (RMCurrentCustNo != string.Empty)
            {
                wherestr = wherestr + " and a.RMCurrentCustNo = '" + RMCurrentCustNo + "'";
            }
            if (RMCurrentCustName != string.Empty)
            {
                wherestr = wherestr + " and b.CustName like '%" + RMCurrentCustName + "%'";
            }
            if (RMStatus != string.Empty)
            {
                wherestr = wherestr + " and a.RMStatus = '" + RMStatus + "'";
            }
            if (RMISEnable != null)
            {
                wherestr = wherestr + " and a.RMISEnable = " + (RMISEnable == true ? "1" : "0");
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_Room a left join Mstr_Customer b on a.RMCurrentCustNo=b.CustNo " +
                    "left join Mstr_Location c on c.LOCNo=a.RMLOCNo1 " +
                    "left join Mstr_Location d on d.LOCNo=a.RMLOCNo2 " +
                    "left join Mstr_Location e on e.LOCNo=a.RMLOCNo3 " +
                    "left join Mstr_Location f on f.LOCNo=a.RMLOCNo4 ",
                    "a.*,b.CustShortName as RMCurrentCustName,c.LOCName as RMLOCNo1Name," +
                    "d.LOCName as RMLOCNo2Name,e.LOCName as RMLOCNo3Name,f.LOCName as RMLOCNo4Name ", 
                    wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_Room a left join Mstr_Customer b on a.RMCurrentCustNo=b.CustNo " +
                    "left join Mstr_Location c on c.LOCNo=a.RMLOCNo1 " +
                    "left join Mstr_Location d on d.LOCNo=a.RMLOCNo2 " +
                    "left join Mstr_Location e on e.LOCNo=a.RMLOCNo3 " +
                    "left join Mstr_Location f on f.LOCNo=a.RMLOCNo4 ",
                    "a.*,b.CustShortName as RMCurrentCustName,c.LOCName as RMLOCNo1Name," +
                    "d.LOCName as RMLOCNo2Name,e.LOCName as RMLOCNo3Name,f.LOCName as RMLOCNo4Name ", 
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
                project.Entity.Base.EntityRoom entity = new project.Entity.Base.EntityRoom();
                entity.RMID = dr["RMID"].ToString();
                entity.RMNo = dr["RMNo"].ToString();
                entity.RMLOCNo1 = dr["RMLOCNo1"].ToString();
                entity.RMLOCNo1Name = dr["RMLOCNo1Name"].ToString();
                entity.RMLOCNo2 = dr["RMLOCNo2"].ToString();
                entity.RMLOCNo2Name = dr["RMLOCNo2Name"].ToString();
                entity.RMLOCNo3 = dr["RMLOCNo3"].ToString();
                entity.RMLOCNo3Name = dr["RMLOCNo3Name"].ToString();
                entity.RMLOCNo4 = dr["RMLOCNo4"].ToString();
                entity.RMLOCNo4Name = dr["RMLOCNo4Name"].ToString();
                entity.RMRentType = dr["RMRentType"].ToString();
                entity.RMBuildSize = ParseDecimalForString(dr["RMBuildSize"].ToString());
                entity.RMRentSize = ParseDecimalForString(dr["RMRentSize"].ToString());
                entity.RMAddr = dr["RMAddr"].ToString();
                entity.RMRemark = dr["RMRemark"].ToString();
                entity.RMISEnable = bool.Parse(dr["RMISEnable"].ToString());
                entity.RMStatus = dr["RMStatus"].ToString();
                entity.RMReservedDate = ParseDateTimeForString(dr["RMReservedDate"].ToString());
                entity.RMEndReservedDate = ParseDateTimeForString(dr["RMEndReservedDate"].ToString());
                entity.RMCurrentCustNo = dr["RMCurrentCustNo"].ToString();
                entity.RMCurrentCustName = dr["RMCurrentCustName"].ToString();
                entity.RMCreateDate = ParseDateTimeForString(dr["RMCreateDate"].ToString());
                entity.RMCreator = dr["RMCreator"].ToString();
                entity.HaveAirCondition = bool.Parse(dr["HaveAirCondition"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
