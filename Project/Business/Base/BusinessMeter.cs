using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 表记资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-07-12</date>
    public sealed class BusinessMeter : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityMeter _entity = new project.Entity.Base.EntityMeter();
        public string OrderField = "a.MeterNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessMeter() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessMeter(project.Entity.Base.EntityMeter entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityMeter)关联
        /// </summary>
        public project.Entity.Base.EntityMeter Entity
        {
            get { return _entity as project.Entity.Base.EntityMeter; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,c.LOCName as MeterLOCNo1Name,d.LOCName as MeterLOCNo2Name,e.LOCName as MeterLOCNo3Name,f.LOCName as MeterLOCNo4Name " +
                "from Mstr_Meter a " +
                "left join Mstr_Location c on c.LOCNo=a.MeterLOCNo1 " +
                "left join Mstr_Location d on d.LOCNo=a.MeterLOCNo2 " +
                "left join Mstr_Location e on e.LOCNo=a.MeterLOCNo3 " +
                "left join Mstr_Location f on f.LOCNo=a.MeterLOCNo4 " +
                " where a.MeterNo='" + id + "' ").Tables[0].Rows[0];
            _entity.MeterNo = dr["MeterNo"].ToString();
            _entity.MeterName = dr["MeterName"].ToString();
            _entity.MeterType = dr["MeterType"].ToString();
            _entity.MeterLOCNo1 = dr["MeterLOCNo1"].ToString();
            _entity.MeterLOCNo1Name = dr["MeterLOCNo1Name"].ToString();
            _entity.MeterLOCNo2 = dr["MeterLOCNo2"].ToString();
            _entity.MeterLOCNo2Name = dr["MeterLOCNo2Name"].ToString();
            _entity.MeterLOCNo3 = dr["MeterLOCNo3"].ToString();
            _entity.MeterLOCNo3Name = dr["MeterLOCNo3Name"].ToString();
            _entity.MeterLOCNo4 = dr["MeterLOCNo4"].ToString();
            _entity.MeterLOCNo4Name = dr["MeterLOCNo4Name"].ToString();
            _entity.MeterRate = ParseDecimalForString(dr["MeterRate"].ToString());
            _entity.MeterDigit = ParseIntForString(dr["MeterDigit"].ToString());
            _entity.MeterUsageType = dr["MeterUsageType"].ToString();
            _entity.MeterNatureType = dr["MeterNatureType"].ToString();
            _entity.MeterReadout = ParseDecimalForString(dr["MeterReadout"].ToString());
            _entity.MeterReadoutDate = ParseDateTimeForString(dr["MeterReadoutDate"].ToString());
            _entity.MeterRMID = dr["MeterRMID"].ToString();
            _entity.MeterSize = dr["MeterSize"].ToString();
            _entity.MeterRelatedMeterNo = dr["MeterRelatedMeterNo"].ToString();
            _entity.Addr = dr["Addr"].ToString();
            _entity.MeterStatus = dr["MeterStatus"].ToString();
            _entity.MeterCreateDate = ParseDateTimeForString(dr["MeterCreateDate"].ToString());
            _entity.MeterCreator = dr["MeterCreator"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";

            string readout = "null";
            if (Entity.MeterReadoutDate.Year > 1900)
                readout = "'" + Entity.MeterReadoutDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            if (type == "insert")
                sqlstr = "insert into Mstr_Meter(MeterNo,MeterName,MeterType,MeterLOCNo1,MeterLOCNo2,MeterLOCNo3,MeterLOCNo4," +
                        "MeterRate,MeterDigit,MeterUsageType,MeterNatureType,MeterReadout,MeterReadoutDate,MeterRMID,MeterSize," +
                        "MeterRelatedMeterNo,Addr,MeterStatus,MeterCreateDate,MeterCreator)" +
                    "values('" + Entity.MeterNo + "'" + "," + "'" + Entity.MeterName + "'" + "," + "'" + Entity.MeterType + "'" + "," +
                    "'" + Entity.MeterLOCNo1 + "'" + "," + "'" + Entity.MeterLOCNo2 + "'" + "," +
                    "'" + Entity.MeterLOCNo3 + "'" + "," + "'" + Entity.MeterLOCNo4 + "'" + "," + Entity.MeterRate + "," +
                    Entity.MeterDigit + "," + "'" + Entity.MeterUsageType + "'" + "," + "'" + Entity.MeterNatureType + "'" + "," +
                    Entity.MeterReadout + "," + readout + "," + "'" + Entity.MeterRMID + "'" + "," +
                    "'" + Entity.MeterSize + "'" + "," + "'" + Entity.MeterRelatedMeterNo + "'" + "," + "'" + Entity.Addr + "'" + "," + "'open'," +
                    "'" + Entity.MeterCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + Entity.MeterCreator + "')";
            else
                sqlstr = "update Mstr_Meter" +
                    " set MeterName=" + "'" + Entity.MeterName + "'" + "," + "MeterType=" + "'" + Entity.MeterType + "'" + "," +
                    "MeterLOCNo1=" + "'" + Entity.MeterLOCNo1 + "'" + "," + "MeterLOCNo2=" + "'" + Entity.MeterLOCNo2 + "'" + "," +
                    "MeterLOCNo3=" + "'" + Entity.MeterLOCNo3 + "'" + "," + "MeterLOCNo4=" + "'" + Entity.MeterLOCNo4 + "'" + "," +
                    "MeterRate=" + Entity.MeterRate + "," + "MeterDigit=" + Entity.MeterDigit + "," +
                    "MeterUsageType=" + "'" + Entity.MeterUsageType + "'" + "," + "MeterNatureType=" + "'" + Entity.MeterNatureType + "'" + "," +
                    "MeterReadout=" + Entity.MeterReadout + "," +
                    "MeterRMID=" + "'" + Entity.MeterRMID + "'" + "," + "MeterSize=" + Entity.MeterSize + "," +
                    "MeterRelatedMeterNo=" + "'" + Entity.MeterRelatedMeterNo + "'" + "," + "Addr=" + "'" + Entity.Addr + "'" +
                    " where MeterNo='" + Entity.MeterNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_Meter where MeterNo='" + Entity.MeterNo + "'");
        }

        /// </summary>
        /// 启用/停用
        /// </summary>
        /// <returns></returns>
        public int valid()
        {
            return objdata.ExecuteNonQuery("update Mstr_Meter set MeterStatus='" + Entity.MeterStatus + "' where MeterNo='" + Entity.MeterNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="MeterLOCNo1">园区</param>
        /// <param name="MeterLOCNo2">建设期</param>
        /// <param name="MeterLOCNo3">楼栋</param>
        /// <param name="MeterLOCNo4">楼层</param>
        /// <param name="MeterNo">表记编号</param>
        /// <param name="MeterType">表记类型</param>
        /// <param name="MeterUsageType">使用类型</param>
        /// <param name="MeterRMID">房间号</param>
        /// <param name="MeterSize">大小类型</param>
        /// <param name="MeterStatus">状态</param>
        /// <param name="startRow">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string MeterLOCNo1, string MeterLOCNo2, string MeterLOCNo3, string MeterLOCNo4, string MeterNo,
            string MeterType, string MeterUsageType, string MeterRMID, string AccurateMeterRMID, string MeterSize, string MeterStatus, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(MeterLOCNo1, MeterLOCNo2, MeterLOCNo3, MeterLOCNo4, MeterNo, MeterType, MeterUsageType, MeterRMID, AccurateMeterRMID, MeterSize, MeterStatus, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="MeterLOCNo1">园区</param>
        /// <param name="MeterLOCNo2">建设期</param>
        /// <param name="MeterLOCNo3">楼栋</param>
        /// <param name="MeterLOCNo4">楼层</param>
        /// <param name="MeterNo">表记编号</param>
        /// <param name="MeterType">表记类型</param>
        /// <param name="MeterUsageType">使用类型</param>
        /// <param name="MeterRMID">房间号</param>
        /// <param name="MeterSize">大小类型</param>
        /// <param name="MeterStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string MeterLOCNo1, string MeterLOCNo2, string MeterLOCNo3, string MeterLOCNo4, string MeterNo,
            string MeterType, string MeterUsageType, string MeterRMID, string AccurateMeterRMID, string MeterSize, string MeterStatus)
        {
            return GetListHelper(MeterLOCNo1, MeterLOCNo2, MeterLOCNo3, MeterLOCNo4, MeterNo, MeterType, MeterUsageType, MeterRMID, AccurateMeterRMID, MeterSize, MeterStatus, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="MeterLOCNo1">园区</param>
        /// <param name="MeterLOCNo2">建设期</param>
        /// <param name="MeterLOCNo3">楼栋</param>
        /// <param name="MeterLOCNo4">楼层</param>
        /// <param name="MeterNo">表记编号</param>
        /// <param name="MeterType">表记类型</param>
        /// <param name="MeterUsageType">使用类型</param>
        /// <param name="MeterRMID">房间号</param>
        /// <param name="MeterSize">大小类型</param>
        /// <param name="MeterStatus">状态</param>
        /// <returns></returns>
        public int GetListCount(string MeterLOCNo1, string MeterLOCNo2, string MeterLOCNo3, string MeterLOCNo4, string MeterNo,
            string MeterType, string MeterUsageType, string MeterRMID, string AccurateMeterRMID, string MeterSize, string MeterStatus)
        {
            string wherestr = "";
            if (MeterLOCNo1 != string.Empty)
            {
                wherestr = wherestr + " and a.MeterLOCNo1 = '" + MeterLOCNo1 + "'";
            }
            if (MeterLOCNo2 != string.Empty)
            {
                wherestr = wherestr + " and a.MeterLOCNo2 = '" + MeterLOCNo2 + "'";
            }
            if (MeterLOCNo3 != string.Empty)
            {
                wherestr = wherestr + " and a.MeterLOCNo3 = '" + MeterLOCNo3 + "'";
            }
            if (MeterLOCNo4 != string.Empty)
            {
                wherestr = wherestr + " and a.MeterLOCNo4 = '" + MeterLOCNo4 + "'";
            }
            if (MeterNo != string.Empty)
            {
                wherestr = wherestr + " and a.MeterNo like '%" + MeterNo + "%'";
            }
            if (MeterType != string.Empty)
            {
                wherestr = wherestr + " and a.MeterType = '" + MeterType + "'";
            }
            if (MeterUsageType != string.Empty)
            {
                wherestr = wherestr + " and a.MeterUsageType = '" + MeterUsageType + "'";
            }
            if (MeterRMID != string.Empty)
            {
                wherestr = wherestr + " and a.MeterRMID like '%" + MeterRMID + "%'";
            }
            if (AccurateMeterRMID != string.Empty)
            {
                wherestr = wherestr + " and a.MeterRMID = '" + AccurateMeterRMID + "'";
            }
            if (MeterSize != string.Empty)
            {
                wherestr = wherestr + " and a.MeterSize = '" + MeterSize + "'";
            }
            if (MeterStatus != string.Empty)
            {
                wherestr = wherestr + " and a.MeterStatus = '" + MeterStatus + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_Meter a where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="MeterLOCNo1">园区</param>
        /// <param name="MeterLOCNo2">建设期</param>
        /// <param name="MeterLOCNo3">楼栋</param>
        /// <param name="MeterLOCNo4">楼层</param>
        /// <param name="MeterNo">表记编号</param>
        /// <param name="MeterType">表记类型</param>
        /// <param name="MeterUsageType">使用类型</param>
        /// <param name="MeterRMID">房间号</param>
        /// <param name="MeterSize">大小类型</param>
        /// <param name="MeterStatus">状态</param>
        /// <param name="startRow">页码</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string MeterLOCNo1, string MeterLOCNo2, string MeterLOCNo3, string MeterLOCNo4, string MeterNo,
            string MeterType, string MeterUsageType, string MeterRMID, string AccurateMeterRMID, string MeterSize, string MeterStatus, int startRow, int pageSize)
        {
            string wherestr = "";
            if (MeterLOCNo1 != string.Empty)
            {
                wherestr = wherestr + " and a.MeterLOCNo1 = '" + MeterLOCNo1 + "'";
            }
            if (MeterLOCNo2 != string.Empty)
            {
                wherestr = wherestr + " and a.MeterLOCNo2 = '" + MeterLOCNo2 + "'";
            }
            if (MeterLOCNo3 != string.Empty)
            {
                wherestr = wherestr + " and a.MeterLOCNo3 = '" + MeterLOCNo3 + "'";
            }
            if (MeterLOCNo4 != string.Empty)
            {
                wherestr = wherestr + " and a.MeterLOCNo4 = '" + MeterLOCNo4 + "'";
            }
            if (MeterNo != string.Empty)
            {
                wherestr = wherestr + " and a.MeterNo like '%" + MeterNo + "%'";
            }
            if (AccurateMeterRMID != string.Empty)
            {
                wherestr = wherestr + " and a.MeterRMID = '" + AccurateMeterRMID + "'";
            }
            if (MeterType != string.Empty)
            {
                wherestr = wherestr + " and a.MeterType = '" + MeterType + "'";
            }
            if (MeterUsageType != string.Empty)
            {
                wherestr = wherestr + " and a.MeterUsageType = '" + MeterUsageType + "'";
            }
            if (MeterRMID != string.Empty)
            {
                wherestr = wherestr + " and a.MeterRMID like '%" + MeterRMID + "%'";
            }
            if (MeterSize != string.Empty)
            {
                wherestr = wherestr + " and a.MeterSize = '" + MeterSize + "'";
            }
            if (MeterStatus != string.Empty)
            {
                wherestr = wherestr + " and a.MeterStatus = '" + MeterStatus + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_Meter a " +
                    "left join Mstr_Location c on c.LOCNo=a.MeterLOCNo1 " +
                    "left join Mstr_Location d on d.LOCNo=a.MeterLOCNo2 " +
                    "left join Mstr_Location e on e.LOCNo=a.MeterLOCNo3 " +
                    "left join Mstr_Location f on f.LOCNo=a.MeterLOCNo4 ",
                    "a.*,c.LOCName as MeterLOCNo1Name,d.LOCName as MeterLOCNo2Name,e.LOCName as MeterLOCNo3Name,f.LOCName as MeterLOCNo4Name ",
                    wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_Meter a " +
                    "left join Mstr_Location c on c.LOCNo=a.MeterLOCNo1 " +
                    "left join Mstr_Location d on d.LOCNo=a.MeterLOCNo2 " +
                    "left join Mstr_Location e on e.LOCNo=a.MeterLOCNo3 " +
                    "left join Mstr_Location f on f.LOCNo=a.MeterLOCNo4 ",
                    "a.*,c.LOCName as MeterLOCNo1Name,d.LOCName as MeterLOCNo2Name,e.LOCName as MeterLOCNo3Name,f.LOCName as MeterLOCNo4Name ",
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
                project.Entity.Base.EntityMeter entity = new project.Entity.Base.EntityMeter();
                entity.MeterNo = dr["MeterNo"].ToString();
                entity.MeterName = dr["MeterName"].ToString();
                entity.MeterType = dr["MeterType"].ToString();
                entity.MeterLOCNo1 = dr["MeterLOCNo1"].ToString();
                entity.MeterLOCNo1Name = dr["MeterLOCNo1Name"].ToString();
                entity.MeterLOCNo2 = dr["MeterLOCNo2"].ToString();
                entity.MeterLOCNo2Name = dr["MeterLOCNo2Name"].ToString();
                entity.MeterLOCNo3 = dr["MeterLOCNo3"].ToString();
                entity.MeterLOCNo3Name = dr["MeterLOCNo3Name"].ToString();
                entity.MeterLOCNo4 = dr["MeterLOCNo4"].ToString();
                entity.MeterLOCNo4Name = dr["MeterLOCNo4Name"].ToString();
                entity.MeterRate = ParseDecimalForString(dr["MeterRate"].ToString());
                entity.MeterDigit = ParseIntForString(dr["MeterDigit"].ToString());
                entity.MeterUsageType = dr["MeterUsageType"].ToString();
                entity.MeterNatureType = dr["MeterNatureType"].ToString();
                entity.MeterReadout = ParseDecimalForString(dr["MeterReadout"].ToString());
                entity.MeterReadoutDate = ParseDateTimeForString(dr["MeterReadoutDate"].ToString());
                entity.MeterRMID = dr["MeterRMID"].ToString();
                entity.MeterSize = dr["MeterSize"].ToString();
                entity.MeterRelatedMeterNo = dr["MeterRelatedMeterNo"].ToString();
                entity.Addr = dr["Addr"].ToString();
                entity.MeterStatus = dr["MeterStatus"].ToString();
                entity.MeterCreateDate = ParseDateTimeForString(dr["MeterCreateDate"].ToString());
                entity.MeterCreator = dr["MeterCreator"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
