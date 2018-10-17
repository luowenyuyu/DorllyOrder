using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 工位类型资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessWorkPlace : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityWorkPlace _entity = new project.Entity.Base.EntityWorkPlace();
        public string OrderField = "WPNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessWorkPlace() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessWorkPlace(project.Entity.Base.EntityWorkPlace entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityWorkPlace)关联
        /// </summary>
        public project.Entity.Base.EntityWorkPlace Entity
        {
            get { return _entity as project.Entity.Base.EntityWorkPlace; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.WPTypeName,c.LOCName as WPLOCNo1Name," +
                "d.LOCName as WPLOCNo2Name,e.LOCName as WPLOCNo3Name,f.LOCName as WPLOCNo4Name " +
                "from Mstr_WorkPlace a " +
                "left join Mstr_WorkPlaceType b on b.WPTypeNo=a.WPType " +
                "left join Mstr_Location c on c.LOCNo=a.WPLOCNo1 " +
                "left join Mstr_Location d on d.LOCNo=a.WPLOCNo2 " +
                "left join Mstr_Location e on e.LOCNo=a.WPLOCNo3 " +
                "left join Mstr_Location f on f.LOCNo=a.WPLOCNo4 " +
                "where WPNo='" + id + "'").Tables[0].Rows[0];
            _entity.WPNo = dr["WPNo"].ToString();
            _entity.WPType = dr["WPType"].ToString();
            _entity.WPTypeName = dr["WPTypeName"].ToString();
            _entity.WPSeat = ParseIntForString(dr["WPSeat"].ToString());
            _entity.WPSeatPrice = ParseDecimalForString(dr["WPSeatPrice"].ToString());
            _entity.WPLOCNo1 = dr["WPLOCNo1"].ToString();
            _entity.WPLOCNo1Name = dr["WPLOCNo1Name"].ToString();
            _entity.WPLOCNo2 = dr["WPLOCNo2"].ToString();
            _entity.WPLOCNo2Name = dr["WPLOCNo2Name"].ToString();
            _entity.WPLOCNo3 = dr["WPLOCNo3"].ToString();
            _entity.WPLOCNo3Name = dr["WPLOCNo3Name"].ToString();
            _entity.WPLOCNo4 = dr["WPLOCNo4"].ToString();
            _entity.WPLOCNo4Name = dr["WPLOCNo4Name"].ToString();
            _entity.WPRMID = dr["WPRMID"].ToString();
            _entity.WPProject = dr["WPProject"].ToString();
            _entity.WPAddr = dr["WPAddr"].ToString();
            _entity.WPStatus = dr["WPStatus"].ToString();
            _entity.WPISEnable = bool.Parse(dr["WPISEnable"].ToString());
            _entity.WPCreateDate = ParseDateTimeForString(dr["WPCreateDate"].ToString());
            _entity.WPCreator = dr["WPCreator"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_WorkPlace(WPNo,WPType,WPSeat,WPSeatPrice,WPLOCNo1,WPLOCNo2,WPLOCNo3,WPLOCNo4,WPRMID,WPProject,WPAddr,"+
                    "WPStatus,WPISEnable,WPCreateDate,WPCreator)" +
                    "values('" + Entity.WPNo + "'" + "," + "'" + Entity.WPType + "'" + "," + Entity.WPSeat + "," + Entity.WPSeatPrice + "," +
                    "'" + Entity.WPLOCNo1 + "'" + "," + "'" + Entity.WPLOCNo2 + "'" + "," + "'" + Entity.WPLOCNo3 + "'" + "," + "'" + Entity.WPLOCNo4 + "'" + "," +
                    "'" + Entity.WPRMID + "'" + "," + "'" + Entity.WPProject + "'" + "," + "'" + Entity.WPAddr + "'" + ",'free',0,"+
                    "'" + Entity.WPCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + "'" + Entity.WPCreator + "'" + ")";
            else
                sqlstr = "update Mstr_WorkPlace" +
                    " set WPType=" + "'" + Entity.WPType + "'" + "," + "WPSeat=" + Entity.WPSeat + "," + "WPSeatPrice=" + Entity.WPSeatPrice + "," +
                    "WPLOCNo1=" + "'" + Entity.WPLOCNo1 + "'" + "," + "WPLOCNo2=" + "'" + Entity.WPLOCNo2 + "'" + "," + "WPLOCNo3=" + "'" + Entity.WPLOCNo3 + "'" + "," +
                    "WPLOCNo4=" + "'" + Entity.WPLOCNo4 + "'" + "," + "WPRMID=" + "'" + Entity.WPRMID + "'" + "," + "WPProject=" + "'" + Entity.WPProject + "'" + "," +
                    "WPAddr=" + "'" + Entity.WPAddr + "'" +
                    " where WPNo='" + Entity.WPNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_WorkPlace where WPNo='" + Entity.WPNo + "'");
        }

        /// </summary>
        /// 禁用/启用 
        /// </summary>
        /// <returns></returns>
        public int valid()
        {
            return objdata.ExecuteNonQuery("update Mstr_WorkPlace set WPISEnable=" + (Entity.WPISEnable == true ? "1" : "0") + " where WPNo='" + Entity.WPNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="WPNo">工位编号</param>
        /// <param name="WPType">工位类型</param>
        /// <param name="WPLOCNo1">园区</param>
        /// <param name="WPLOCNo2">建设期</param>
        /// <param name="WPLOCNo3">楼栋</param>
        /// <param name="WPLOCNo4">楼层</param>
        /// <param name="WPRMID">房间号</param>
        /// <param name="WPStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string WPNo, string WPType, string WPLOCNo1, string WPLOCNo2, string WPLOCNo3, string WPLOCNo4, string WPRMID, string WPStatus, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(WPNo, WPType, WPLOCNo1, WPLOCNo2, WPLOCNo3, WPLOCNo4, WPRMID, WPStatus, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="WPNo">工位编号</param>
        /// <param name="WPType">工位类型</param>
        /// <param name="WPLOCNo1">园区</param>
        /// <param name="WPLOCNo2">建设期</param>
        /// <param name="WPLOCNo3">楼栋</param>
        /// <param name="WPLOCNo4">楼层</param>
        /// <param name="WPRMID">房间号</param>
        /// <param name="WPStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string WPNo, string WPType, string WPLOCNo1, string WPLOCNo2, string WPLOCNo3, string WPLOCNo4, string WPRMID, string WPStatus)
        {
            return GetListHelper(WPNo, WPType, WPLOCNo1, WPLOCNo2, WPLOCNo3, WPLOCNo4, WPRMID, WPStatus, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="WPNo">工位编号</param>
        /// <param name="WPType">工位类型</param>
        /// <param name="WPLOCNo1">园区</param>
        /// <param name="WPLOCNo2">建设期</param>
        /// <param name="WPLOCNo3">楼栋</param>
        /// <param name="WPLOCNo4">楼层</param>
        /// <param name="WPRMID">房间号</param>
        /// <param name="WPStatus">状态</param>
        /// <returns></returns>
        public int GetListCount(string WPNo, string WPType, string WPLOCNo1, string WPLOCNo2, string WPLOCNo3, string WPLOCNo4, string WPRMID, string WPStatus)
        {
            string wherestr = "";
            if (WPNo != string.Empty)
            {
                wherestr = wherestr + " and WPNo like '%" + WPNo + "%'";
            }
            if (WPType != string.Empty)
            {
                wherestr = wherestr + " and WPType = '" + WPType + "'";
            }
            if (WPLOCNo1 != string.Empty)
            {
                wherestr = wherestr + " and WPLOCNo1 = '" + WPLOCNo1 + "'";
            }
            if (WPLOCNo2 != string.Empty)
            {
                wherestr = wherestr + " and WPLOCNo2 = '" + WPLOCNo2 + "'";
            }
            if (WPLOCNo3 != string.Empty)
            {
                wherestr = wherestr + " and WPLOCNo3 = '" + WPLOCNo3 + "'";
            }
            if (WPLOCNo4 != string.Empty)
            {
                wherestr = wherestr + " and WPLOCNo4 = '" + WPLOCNo4 + "'";
            }
            if (WPRMID != string.Empty)
            {
                wherestr = wherestr + " and WPRMID = '" + WPRMID + "'";
            }
            if (WPStatus != string.Empty)
            {
                wherestr = wherestr + " and WPStatus = '" + WPStatus + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_WorkPlace where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="WPNo">工位编号</param>
        /// <param name="WPType">工位类型</param>
        /// <param name="WPLOCNo1">园区</param>
        /// <param name="WPLOCNo2">建设期</param>
        /// <param name="WPLOCNo3">楼栋</param>
        /// <param name="WPLOCNo4">楼层</param>
        /// <param name="WPRMID">房间号</param>
        /// <param name="WPStatus">状态</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string WPNo, string WPType, string WPLOCNo1, string WPLOCNo2, string WPLOCNo3, string WPLOCNo4, string WPRMID, string WPStatus, int startRow, int pageSize)
        {
            string wherestr = "";
            if (WPNo != string.Empty)
            {
                wherestr = wherestr + " and WPNo like '%" + WPNo + "%'";
            }
            if (WPType != string.Empty)
            {
                wherestr = wherestr + " and WPType = '" + WPType + "'";
            }
            if (WPLOCNo1 != string.Empty)
            {
                wherestr = wherestr + " and WPLOCNo1 = '" + WPLOCNo1 + "'";
            }
            if (WPLOCNo2 != string.Empty)
            {
                wherestr = wherestr + " and WPLOCNo2 = '" + WPLOCNo2 + "'";
            }
            if (WPLOCNo3 != string.Empty)
            {
                wherestr = wherestr + " and WPLOCNo3 = '" + WPLOCNo3 + "'";
            }
            if (WPLOCNo4 != string.Empty)
            {
                wherestr = wherestr + " and WPLOCNo4 = '" + WPLOCNo4 + "'";
            }
            if (WPRMID != string.Empty)
            {
                wherestr = wherestr + " and WPRMID = '" + WPRMID + "'";
            }
            if (WPStatus != string.Empty)
            {
                wherestr = wherestr + " and WPStatus = '" + WPStatus + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_WorkPlace a " +
                    "left join Mstr_WorkPlaceType b on b.WPTypeNo=a.WPType " +
                    "left join Mstr_Location c on c.LOCNo=a.WPLOCNo1 " +
                    "left join Mstr_Location d on d.LOCNo=a.WPLOCNo2 " +
                    "left join Mstr_Location e on e.LOCNo=a.WPLOCNo3 " +
                    "left join Mstr_Location f on f.LOCNo=a.WPLOCNo4 ", 
                    "a.*,b.WPTypeName,c.LOCName as WPLOCNo1Name,d.LOCName as WPLOCNo2Name,e.LOCName as WPLOCNo3Name,f.LOCName as WPLOCNo4Name", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_WorkPlace a " +
                    "left join Mstr_WorkPlaceType b on b.WPTypeNo=a.WPType " +
                    "left join Mstr_Location c on c.LOCNo=a.WPLOCNo1 " +
                    "left join Mstr_Location d on d.LOCNo=a.WPLOCNo2 " +
                    "left join Mstr_Location e on e.LOCNo=a.WPLOCNo3 " +
                    "left join Mstr_Location f on f.LOCNo=a.WPLOCNo4 ",
                    "a.*,b.WPTypeName,c.LOCName as WPLOCNo1Name,d.LOCName as WPLOCNo2Name,e.LOCName as WPLOCNo3Name,f.LOCName as WPLOCNo4Name", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityWorkPlace entity = new project.Entity.Base.EntityWorkPlace();
                entity.WPNo = dr["WPNo"].ToString();
                entity.WPType = dr["WPType"].ToString();
                entity.WPSeat = ParseIntForString(dr["WPSeat"].ToString());
                entity.WPSeatPrice = ParseDecimalForString(dr["WPSeatPrice"].ToString());
                entity.WPLOCNo1 = dr["WPLOCNo1"].ToString();
                entity.WPLOCNo1Name = dr["WPLOCNo1Name"].ToString();
                entity.WPLOCNo2 = dr["WPLOCNo2"].ToString();
                entity.WPLOCNo2Name = dr["WPLOCNo2Name"].ToString();
                entity.WPLOCNo3 = dr["WPLOCNo3"].ToString();
                entity.WPLOCNo3Name = dr["WPLOCNo3Name"].ToString();
                entity.WPLOCNo4 = dr["WPLOCNo4"].ToString();
                entity.WPLOCNo4Name = dr["WPLOCNo4Name"].ToString();
                entity.WPRMID = dr["WPRMID"].ToString();
                entity.WPProject = dr["WPProject"].ToString();
                entity.WPAddr = dr["WPAddr"].ToString();
                entity.WPStatus = dr["WPStatus"].ToString();
                entity.WPISEnable = bool.Parse(dr["WPISEnable"].ToString());
                entity.WPCreateDate = ParseDateTimeForString(dr["WPCreateDate"].ToString());
                entity.WPCreator = dr["WPCreator"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
