using System;
using System.Data;
using System.Data.SqlClient;
namespace project.Business.Op
{
    /// <summary>
    /// 转表管理
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessChangeMeter : project.Business.AbstractPmBusiness
    {
        private project.Entity.Op.EntityChangeMeter _entity = new project.Entity.Op.EntityChangeMeter();
        public string OrderField = "CMCreateDate desc";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessChangeMeter() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessChangeMeter(project.Entity.Op.EntityChangeMeter entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityChangeMeter)关联
        /// </summary>
        public project.Entity.Op.EntityChangeMeter Entity
        {
            get { return _entity as project.Entity.Op.EntityChangeMeter; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.MeterType from Op_ChangeMeter a left join Mstr_Meter b on a.OldMeterNo=b.MeterNo where a.RowPointer='" + id + "'").Tables[0].Rows[0];
            _entity.RowPointer = dr["RowPointer"].ToString();
            _entity.RMID = dr["RMID"].ToString();
            _entity.OldMeterNo = dr["OldMeterNo"].ToString();
            _entity.OldMeterType = dr["MeterType"].ToString();
            _entity.OldMeterLastReadout = ParseDecimalForString(dr["OldMeterLastReadout"].ToString());
            _entity.OldMeterReadout = ParseDecimalForString(dr["OldMeterReadout"].ToString());
            _entity.OldMeterReadings = ParseDecimalForString(dr["OldMeterReadings"].ToString());
            _entity.NewMeterNo = dr["NewMeterNo"].ToString();
            _entity.NewMeterName = dr["NewMeterName"].ToString();
            _entity.NewMeterSize = dr["NewMeterSize"].ToString();
            _entity.NewMeterReadout = ParseDecimalForString(dr["NewMeterReadout"].ToString());
            _entity.NewMeterRate = ParseDecimalForString(dr["NewMeterRate"].ToString());
            _entity.NewMeterDigit = ParseIntForString(dr["NewMeterDigit"].ToString());
            _entity.CMDate = ParseDateTimeForString(dr["CMDate"].ToString());
            _entity.CMOperator = dr["CMOperator"].ToString();
            _entity.CMCreateDate = ParseDateTimeForString(dr["CMCreateDate"].ToString());
            _entity.CMCreator = dr["CMCreator"].ToString();
            _entity.AuditStatus = dr["AuditStatus"].ToString();
            _entity.Auditor = dr["Auditor"].ToString();
            _entity.AuditDate = ParseDateTimeForString(dr["AuditDate"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save()
        {
            string sqlstr = "";
            if (Entity.RowPointer == null)
                sqlstr = "insert into Op_ChangeMeter(RowPointer,RMID,OldMeterNo,OldMeterLastReadout,OldMeterReadout,OldMeterReadings,NewMeterNo,NewMeterName,"+
                        "NewMeterSize,NewMeterReadout,NewMeterRate,NewMeterDigit,CMDate,CMOperator,CMCreateDate,CMCreator,AuditStatus,Auditor,AuditDate)" +
                    "values(NEWID()," + "'" + Entity.RMID + "'" + "," + "'" + Entity.OldMeterNo + "'" + "," + Entity.OldMeterLastReadout + "," +
                    Entity.OldMeterReadout + "," + Entity.OldMeterReadings + "," + "'" + Entity.NewMeterNo + "'" + "," + "'" + Entity.NewMeterName + "'" + "," +
                    "'" + Entity.NewMeterSize + "'" + "," + Entity.NewMeterReadout + "," + Entity.NewMeterRate + "," +
                    Entity.NewMeterDigit + "," + "'" + Entity.CMDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + Entity.CMOperator + "'" + "," +
                    "'" + Entity.CMCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",'" + Entity.CMCreator + "','0','',null)";
            else
                sqlstr = "update Op_ChangeMeter" +
                    " set RMID=" + "'" + Entity.RMID + "'" + "," + "OldMeterNo=" + "'" + Entity.OldMeterNo + "'" + "," +
                    "OldMeterLastReadout=" + Entity.OldMeterLastReadout + "," + "OldMeterReadout=" + Entity.OldMeterReadout + "," +
                    "OldMeterReadings=" + Entity.OldMeterReadings + "," + "NewMeterNo=" + "'" + Entity.NewMeterNo + "'" + "," +
                    "NewMeterName=" + "'" + Entity.NewMeterName + "'" + "," + 
                    "NewMeterSize=" + "'" + Entity.NewMeterSize + "'" + "," + "NewMeterReadout=" + Entity.NewMeterReadout + "," +
                    "NewMeterRate=" + Entity.NewMeterRate + "," + "NewMeterDigit=" + Entity.NewMeterDigit + "," +
                    "CMDate=" + "'" + Entity.CMDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," + "CMOperator=" + "'" + Entity.CMOperator + "'" + 
                    " where RowPointer='" + Entity.RowPointer + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Op_ChangeMeter where RowPointer='" + Entity.RowPointer + "'");
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
                command = new SqlCommand("ApproveChangeMeter", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@CMID", SqlDbType.NVarChar, 36).Value = Entity.RowPointer;
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
        /// <param name="RMID">房间号</param>
        /// <param name="OldMeterNo">旧表记编号</param>
        /// <param name="NewMeterNo">新表记编号</param>
        /// <param name="OldMeterType">旧表记类型</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string RMID, string OldMeterNo, string NewMeterNo, string OldMeterType, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(RMID, OldMeterNo, NewMeterNo, OldMeterType, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="RMID">房间号</param>
        /// <param name="OldMeterNo">旧表记编号</param>
        /// <param name="NewMeterNo">新表记编号</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string RMID, string OldMeterNo, string NewMeterNo, string OldMeterType)
        {
            return GetListHelper(RMID, OldMeterNo, NewMeterNo, OldMeterType, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="RMID">房间号</param>
        /// <param name="OldMeterNo">旧表记编号</param>
        /// <param name="NewMeterNo">新表记编号</param>
        /// <returns></returns>
        public int GetListCount(string RMID, string OldMeterNo, string NewMeterNo, string OldMeterType)
        {
            string wherestr = "";
            if (RMID != string.Empty)
            {
                wherestr = wherestr + " and a.RMID like '%" + RMID + "%'";
            }
            if (OldMeterNo != string.Empty)
            {
                wherestr = wherestr + " and a.OldMeterNo like '%" + OldMeterNo + "%'";
            }
            if (NewMeterNo != string.Empty)
            {
                wherestr = wherestr + " and a.NewMeterNo like '%" + NewMeterNo + "%'";
            }
            if (OldMeterType != string.Empty)
            {
                wherestr = wherestr + " and b.MeterType like '%" + OldMeterType + "%'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Op_ChangeMeter a left join Mstr_Meter b on a.OldMeterNo=b.MeterNo where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="RMID">房间号</param>
        /// <param name="OldMeterNo">旧表记编号</param>
        /// <param name="NewMeterNo">新表记编号</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string RMID, string OldMeterNo, string NewMeterNo, string OldMeterType, int startRow, int pageSize)
        {
            string wherestr = "";
            if (RMID != string.Empty)
            {
                wherestr = wherestr + " and a.RMID like '%" + RMID + "%'";
            }
            if (OldMeterNo != string.Empty)
            {
                wherestr = wherestr + " and a.OldMeterNo like '%" + OldMeterNo + "%'";
            }
            if (NewMeterNo != string.Empty)
            {
                wherestr = wherestr + " and a.NewMeterNo like '%" + NewMeterNo + "%'";
            }
            if (OldMeterType != string.Empty)
            {
                wherestr = wherestr + " and b.MeterType like '%" + OldMeterType + "%'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Op_ChangeMeter a left join Mstr_Meter b on a.OldMeterNo=b.MeterNo", "a.*,b.MeterType", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Op_ChangeMeter a left join Mstr_Meter b on a.OldMeterNo=b.MeterNo", "a.*,b.MeterType", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Op.EntityChangeMeter entity = new project.Entity.Op.EntityChangeMeter();
                entity.RowPointer = dr["RowPointer"].ToString();
                entity.RMID = dr["RMID"].ToString();
                entity.OldMeterNo = dr["OldMeterNo"].ToString();
                entity.OldMeterType = dr["MeterType"].ToString();
                entity.OldMeterLastReadout = ParseDecimalForString(dr["OldMeterLastReadout"].ToString());
                entity.OldMeterReadout = ParseDecimalForString(dr["OldMeterReadout"].ToString());
                entity.OldMeterReadings = ParseDecimalForString(dr["OldMeterReadings"].ToString());
                entity.NewMeterNo = dr["NewMeterNo"].ToString();
                entity.NewMeterName = dr["NewMeterName"].ToString();
                entity.NewMeterSize = dr["NewMeterSize"].ToString();
                entity.NewMeterReadout = ParseDecimalForString(dr["NewMeterReadout"].ToString());
                entity.NewMeterRate = ParseDecimalForString(dr["NewMeterRate"].ToString());
                entity.NewMeterDigit = ParseIntForString(dr["NewMeterDigit"].ToString());
                entity.CMDate = ParseDateTimeForString(dr["CMDate"].ToString());
                entity.CMOperator = dr["CMOperator"].ToString();
                entity.CMCreateDate = ParseDateTimeForString(dr["CMCreateDate"].ToString());
                entity.CMCreator = dr["CMCreator"].ToString();
                entity.AuditStatus = dr["AuditStatus"].ToString();
                entity.Auditor = dr["Auditor"].ToString();
                entity.AuditDate = ParseDateTimeForString(dr["AuditDate"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
