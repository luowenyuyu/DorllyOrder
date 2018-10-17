using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 抄表人资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-09-27</date>
    public sealed class BusinessMeterReader : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityMeterReader _entity = new project.Entity.Base.EntityMeterReader();
        public string OrderField = "ReaderNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessMeterReader() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessMeterReader(project.Entity.Base.EntityMeterReader entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityMeterReader)关联
        /// </summary>
        public project.Entity.Base.EntityMeterReader Entity
        {
            get { return _entity as project.Entity.Base.EntityMeterReader; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Mstr_MeterReader where ReaderNo='" + id + "'").Tables[0].Rows[0];
            _entity.ReaderNo = dr["ReaderNo"].ToString();
            _entity.ReaderName = dr["ReaderName"].ToString();
            _entity.Status = dr["Status"].ToString();
            _entity.CreateUser = dr["CreateUser"].ToString();
            _entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_MeterReader(ReaderNo,ReaderName,Status,CreateUser,CreateDate)" +
                    "values('" + Entity.ReaderNo + "'" + "," + "'" + Entity.ReaderName + "'" + "," +
                    "'" + Entity.Status + "'" + "," + "'" + Entity.CreateUser + "'" + "," + "'" + Entity.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ")";
            else
                sqlstr = "update Mstr_MeterReader" +
                    " set ReaderName=" + "'" + Entity.ReaderName + "'" + "," +
                    "Status=" + "'" + Entity.Status + "'" +
                    " where ReaderNo='" + Entity.ReaderNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_MeterReader where ReaderNo='" + Entity.ReaderNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="ReaderNo">抄表人编号</param>
        /// <param name="ReaderName">抄表人名称</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string ReaderNo, string ReaderName, string Status, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(ReaderNo, ReaderName, Status, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="ReaderNo">抄表人编号</param>
        /// <param name="ReaderName">抄表人名称</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string ReaderNo, string ReaderName, string Status)
        {
            return GetListHelper(ReaderNo, ReaderName, Status, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="ReaderNo">抄表人编号</param>
        /// <param name="ReaderName">抄表人名称</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int GetListCount(string ReaderNo, string ReaderName, string Status)
        {
            string wherestr = "";
            if (ReaderNo != string.Empty)
            {
                wherestr = wherestr + " and ReaderNo like '%" + ReaderNo + "%'";
            }
            if (ReaderName != string.Empty)
            {
                wherestr = wherestr + " and ReaderName like '%" + ReaderName + "%'";
            }
            if (Status != string.Empty)
            {
                wherestr = wherestr + " and Status = '" + Status + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_MeterReader where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="ReaderNo">抄表人编号</param>
        /// <param name="ReaderName">抄表人名称</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string ReaderNo, string ReaderName, string Status, int startRow, int pageSize)
        {
            string wherestr = "";
            if (ReaderNo != string.Empty)
            {
                wherestr = wherestr + " and ReaderNo like '%" + ReaderNo + "%'";
            }
            if (ReaderName != string.Empty)
            {
                wherestr = wherestr + " and ReaderName like '%" + ReaderName + "%'";
            }
            if (Status != string.Empty)
            {
                wherestr = wherestr + " and Status = '" + Status + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_MeterReader", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_MeterReader", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityMeterReader entity = new project.Entity.Base.EntityMeterReader();
                entity.ReaderNo = dr["ReaderNo"].ToString();
                entity.ReaderName = dr["ReaderName"].ToString();
                entity.Status = dr["Status"].ToString();
                entity.CreateUser = dr["CreateUser"].ToString();
                entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
