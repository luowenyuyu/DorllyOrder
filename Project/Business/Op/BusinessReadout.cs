using System;
using System.Data;
using System.Data.SqlClient;
namespace project.Business.Op
{
    /// <summary>
    /// 抄表管理
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessReadout : project.Business.AbstractPmBusiness
    {
        private project.Entity.Op.EntityReadout _entity = new project.Entity.Op.EntityReadout();
        public string OrderField = "ROCreateDate desc";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessReadout() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessReadout(project.Entity.Op.EntityReadout entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityReadout)关联
        /// </summary>
        public project.Entity.Op.EntityReadout Entity
        {
            get { return _entity as project.Entity.Op.EntityReadout; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.MeterType,c.RMNo from Op_Readout a left join Mstr_Meter b on a.MeterNo=b.MeterNo left join Mstr_Room c on c.RMID=b.MeterRMID where a.RowPointer='" + id + "'").Tables[0].Rows[0];
            _entity.RowPointer = dr["RowPointer"].ToString();
            _entity.MeterNo = dr["MeterNo"].ToString();
            _entity.RMID = dr["RMID"].ToString();
            _entity.RMNo = dr["RMNo"].ToString();
            _entity.ReadoutType = dr["ReadoutType"].ToString();
            _entity.MeterType = dr["MeterType"].ToString();
            _entity.LastReadout = ParseDecimalForString(dr["LastReadout"].ToString());
            _entity.Readout = ParseDecimalForString(dr["Readout"].ToString());
            _entity.Readings = ParseDecimalForString(dr["Readings"].ToString());
            _entity.JoinReadings = ParseDecimalForString(dr["JoinReadings"].ToString());
            _entity.MeteRate = ParseDecimalForString(dr["MeteRate"].ToString());
            _entity.IsChange = bool.Parse(dr["IsChange"].ToString());
            _entity.CMRP = dr["CMRP"].ToString();
            _entity.OldMeterReadings = ParseDecimalForString(dr["OldMeterReadings"].ToString());
            _entity.AuditStatus = dr["AuditStatus"].ToString();
            _entity.Auditor = dr["Auditor"].ToString();
            _entity.AuditDate = ParseDateTimeForString(dr["AuditDate"].ToString());
            _entity.AuditReason = dr["AuditReason"].ToString();
            _entity.RODate = ParseDateTimeForString(dr["RODate"].ToString());
            _entity.ROOperator = dr["ROOperator"].ToString();
            _entity.ROCreateDate = ParseDateTimeForString(dr["ROCreateDate"].ToString());
            _entity.ROCreator = dr["ROCreator"].ToString();
            _entity.IsOrder = bool.Parse(dr["IsOrder"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Op_Readout(RowPointer,RMID,MeterNo,ReadoutType,LastReadout,Readout,JoinReadings,Readings,MeteRate,IsChange," +
                        "CMRP,OldMeterReadings,AuditStatus,Auditor,AuditDate,AuditReason,RODate,ROOperator,ROCreateDate,ROCreator,IsOrder)" +
                    "values('"+Entity.RowPointer +"'," + "'" + Entity.RMID + "'" + "," + "'" + Entity.MeterNo + "'" + "," + "'" + Entity.ReadoutType + "'" + "," +
                    Entity.LastReadout + "," + Entity.Readout + "," + Entity.JoinReadings + "," + Entity.Readings + "," + 
                    Entity.MeteRate + ","+(Entity.IsChange?"1":"0")+"," +
                    "'" + Entity.CMRP + "'" + "," + Entity.OldMeterReadings + "," + "'0','',null,''," +
                    "'" + Entity.RODate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + Entity.ROOperator + "'" + "," +
                    "'" + Entity.ROCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + Entity.ROCreator + "',0)";
            else
                sqlstr = "update Op_Readout" +
                    " set RMID=" + "'" + Entity.RMID + "'" + "," + "MeterNo=" + "'" + Entity.MeterNo + "'" + "," +
                    "ReadoutType=" + "'" + Entity.ReadoutType + "'" + "," + "LastReadout=" + Entity.LastReadout + "," +
                    "Readout=" + Entity.Readout + "," + "JoinReadings=" + Entity.JoinReadings + "," + 
                    "Readings=" + Entity.Readings + "," + "MeteRate=" + Entity.MeteRate + "," +
                    "RODate='" + Entity.RODate.ToString("yyyy-MM-dd HH:mm:ss") + "'," + "ROOperator='" + Entity.ROOperator + "'" + 
                    " where RowPointer='" + Entity.RowPointer + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Op_Readout where RowPointer='" + Entity.RowPointer + "'");
        }

        ///// </summary>
        /////Audit方法 
        ///// </summary>
        //public int unaudit()
        //{
        //    return objdata.ExecuteNonQuery("update Op_Readout set AuditStatus='" + Entity.AuditStatus + "',"+
        //        "Auditor='" + Entity.Auditor + "',AuditDate=GETDATE(),AuditReason='"+Entity.AuditReason+"' " +
        //        "where RowPointer='" + Entity.RowPointer + "'");
        //}

        /// </summary>
        ///Audit方法 
        /// </summary>
        public string unaudit(string UserName)
        {
            string InfoMsg = "";
            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = Data.Conn();
                command = new SqlCommand("ApproveReadout_Cancel", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@RMID", SqlDbType.NVarChar, 36).Value = Entity.RowPointer;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar, 30).Value = UserName;
                command.Parameters.Add("@InfoMsg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                con.Open();
                command.ExecuteNonQuery();
                InfoMsg = command.Parameters["@InfoMsg"].Value.ToString();

            }
            catch (Exception ex)
            {
                InfoMsg = ex.ToString();
            }
            finally
            {
                if (command != null)
                    command.Dispose();
                if (con != null)
                    con.Dispose();
            }
            return InfoMsg;
        }

        /// </summary>
        ///Audit方法 
        /// </summary>
        public string audit(string UserName)
        {
            string InfoMsg = "";
            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = Data.Conn();
                command = new SqlCommand("ApproveReadout", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@RMID", SqlDbType.NVarChar, 36).Value = Entity.RowPointer;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar, 30).Value = UserName;
                command.Parameters.Add("@InfoMsg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                con.Open();
                command.ExecuteNonQuery();
                InfoMsg = command.Parameters["@InfoMsg"].Value.ToString();

            }
            catch (Exception ex)
            {
                InfoMsg = ex.ToString();
            }
            finally
            {
                if (command != null)
                    command.Dispose();
                if (con != null)
                    con.Dispose();
            }
            return InfoMsg;
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="MeterNo">表记编号</param>
        /// <param name="RMID">房间号</param>
        /// <param name="ReadoutType">抄表类型</param>
        /// <param name="AuditStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string MeterNo, string RMID, string ReadoutType, string AuditStatus, string MeterType, DateTime MinRODate, DateTime MaxRODate, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(MeterNo, RMID, ReadoutType, AuditStatus, MeterType, MinRODate, MaxRODate, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="MeterNo">表记编号</param>
        /// <param name="RMID">房间号</param>
        /// <param name="ReadoutType">抄表类型</param>
        /// <param name="AuditStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string MeterNo, string RMID, string ReadoutType, string AuditStatus, string MeterType, DateTime MinRODate, DateTime MaxRODate)
        {
            return GetListHelper(MeterNo, RMID, ReadoutType, AuditStatus, MeterType, MinRODate, MaxRODate, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="MeterNo">表记编号</param>
        /// <param name="RMID">房间号</param>
        /// <param name="ReadoutType">抄表类型</param>
        /// <param name="AuditStatus">状态</param>
        /// <returns></returns>
        public int GetListCount(string MeterNo, string RMID, string ReadoutType, string AuditStatus, string MeterType, DateTime MinRODate, DateTime MaxRODate)
        {
            string wherestr = "";
            if (MeterNo != string.Empty)
            {
                wherestr = wherestr + " and a.MeterNo like '%" + MeterNo + "%'";
            }
            if (RMID != string.Empty)
            {
                wherestr = wherestr + " and a.RMID like '%" + RMID + "%'";
            }
            if (ReadoutType != string.Empty)
            {
                wherestr = wherestr + " and a.ReadoutType = '" + ReadoutType + "'";
            }
            if (AuditStatus != string.Empty)
            {
                wherestr = wherestr + " and a.AuditStatus = '" + AuditStatus + "'";
            }
            if (MeterType != string.Empty)
            {
                wherestr = wherestr + " and b.MeterType like '%" + MeterType + "%'";
            }
            if (MinRODate != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.RODate,121) >= '" + MinRODate.ToString("yyyy-MM-dd") + "'";
            }
            if (MaxRODate != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.RODate,121) <= '" + MaxRODate.ToString("yyyy-MM-dd") + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Op_Readout a left join Mstr_Meter b on a.MeterNo=b.MeterNo left join Mstr_Room c on c.RMID=b.MeterRMID where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="MeterNo">表记编号</param>
        /// <param name="RMID">房间号</param>
        /// <param name="ReadoutType">抄表类型</param>
        /// <param name="AuditStatus">状态</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string MeterNo, string RMID, string ReadoutType, string AuditStatus, string MeterType, DateTime MinRODate, DateTime MaxRODate, int startRow, int pageSize)
        {
            string wherestr = "";
            if (MeterNo != string.Empty)
            {
                wherestr = wherestr + " and a.MeterNo like '%" + MeterNo + "%'";
            }
            if (RMID != string.Empty)
            {
                wherestr = wherestr + " and a.RMID like '%" + RMID + "%'";
            }
            if (ReadoutType != string.Empty)
            {
                wherestr = wherestr + " and a.ReadoutType = '" + ReadoutType + "'";
            }
            if (AuditStatus != string.Empty)
            {
                wherestr = wherestr + " and a.AuditStatus = '" + AuditStatus + "'";
            }
            if (MeterType != string.Empty)
            {
                wherestr = wherestr + " and b.MeterType like '%" + MeterType + "%'";
            }
            if (MinRODate != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.RODate,121) >= '" + MinRODate.ToString("yyyy-MM-dd") + "'";
            }
            if (MaxRODate != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.RODate,121) <= '" + MaxRODate.ToString("yyyy-MM-dd") + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Op_Readout a left join Mstr_Meter b on a.MeterNo=b.MeterNo left join Mstr_Room c on c.RMID=b.MeterRMID", 
                    "a.*,b.MeterType,c.RMNo", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Op_Readout a left join Mstr_Meter b on a.MeterNo=b.MeterNo left join Mstr_Room c on c.RMID=b.MeterRMID", 
                    "a.*,b.MeterType,c.RMNo", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Op.EntityReadout entity = new project.Entity.Op.EntityReadout();
                entity.RowPointer = dr["RowPointer"].ToString();
                entity.MeterNo = dr["MeterNo"].ToString();
                entity.RMID = dr["RMID"].ToString();
                entity.RMNo = dr["RMNo"].ToString();
                entity.ReadoutType = dr["ReadoutType"].ToString();
                entity.MeterType = dr["MeterType"].ToString();
                entity.LastReadout = ParseDecimalForString(dr["LastReadout"].ToString());
                entity.Readout = ParseDecimalForString(dr["Readout"].ToString());
                entity.JoinReadings = ParseDecimalForString(dr["JoinReadings"].ToString());
                entity.Readings = ParseDecimalForString(dr["Readings"].ToString());
                entity.MeteRate = ParseDecimalForString(dr["MeteRate"].ToString());
                entity.IsChange = bool.Parse(dr["IsChange"].ToString());
                entity.CMRP = dr["CMRP"].ToString();
                entity.OldMeterReadings = ParseDecimalForString(dr["OldMeterReadings"].ToString());
                entity.AuditStatus = dr["AuditStatus"].ToString();
                entity.Auditor = dr["Auditor"].ToString();
                entity.AuditDate = ParseDateTimeForString(dr["AuditDate"].ToString());
                entity.AuditReason = dr["AuditReason"].ToString();
                entity.RODate = ParseDateTimeForString(dr["RODate"].ToString());
                entity.ROOperator = dr["ROOperator"].ToString();
                entity.ROCreateDate = ParseDateTimeForString(dr["ROCreateDate"].ToString());
                entity.ROCreator = dr["ROCreator"].ToString();
                entity.IsOrder = bool.Parse(dr["IsOrder"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
