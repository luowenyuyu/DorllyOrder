using System;
using System.Data;
using System.Data.SqlClient;
namespace project.Business.Op
{
    /// <summary>
    /// 订单信息
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-08-01</date>
    public sealed class BusinessOrderFeeReceiver : project.Business.AbstractPmBusiness
    {
        private project.Entity.Op.EntityOrderFeeReceiver _entity = new project.Entity.Op.EntityOrderFeeReceiver();
        public string OrderField = "ODCreateDate desc";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessOrderFeeReceiver() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessOrderFeeReceiver(project.Entity.Op.EntityOrderFeeReceiver entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityOrderFeeReceiver)关联
        /// </summary>
        public project.Entity.Op.EntityOrderFeeReceiver Entity
        {
            get { return _entity as project.Entity.Op.EntityOrderFeeReceiver; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Op_OrderFeeReceiver where RowPointer='" + id + "'").Tables[0].Rows[0];
            _entity.RowPointer = dr["RowPointer"].ToString();
            _entity.RefRP = dr["RefRP"].ToString();
            _entity.ODPaidAmount = ParseDecimalForString(dr["ODPaidAmount"].ToString());
            _entity.ODPaidDate = ParseDateTimeForString(dr["ODPaidDate"].ToString());
            _entity.ODFeeReceiver = dr["ODFeeReceiver"].ToString();
            _entity.ODFeeReceiveRemark = dr["ODFeeReceiveRemark"].ToString();
            _entity.ODCreator = dr["ODCreator"].ToString();
            _entity.ODCreateDate = ParseDateTimeForString(dr["ODCreateDate"].ToString());
            _entity.ODLastReviser = dr["ODLastReviser"].ToString();
            _entity.ODLastRevisedDate = ParseDateTimeForString(dr["ODLastRevisedDate"].ToString());
            _entity.ODPaidType = dr["ODPaidType"].ToString();
            _entity.ODPaidBank = dr["ODPaidBank"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string id,string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Op_OrderFeeReceiver(RowPointer,RefRP,ODPaidAmount,ODPaidDate,ODFeeReceiver,ODFeeReceiveRemark," +
                        "ODCreator,ODCreateDate,ODLastReviser,ODLastRevisedDate,ODPaidType,ODPaidBank)" +
                    "values('" + id + "'," + "'" + Entity.RefRP + "'" + "," + Entity.ODPaidAmount + "," + "'" + Entity.ODPaidDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "'" + Entity.ODFeeReceiver + "'" + "," + "'" + Entity.ODFeeReceiveRemark + "'" + "," +
                    "'" + Entity.ODCreator + "'" + "," + "'" + Entity.ODCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "'" + Entity.ODLastReviser + "'" + "," + "'" + Entity.ODLastRevisedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "'" + Entity.ODPaidType + "'" + "," + "'" + Entity.ODPaidBank + "'" + 
                    ")";
            else
                sqlstr = "update Op_OrderFeeReceiver" +
                    " set ODPaidAmount=" + Entity.ODPaidAmount + "," + "ODPaidDate=" + "'" + Entity.ODPaidDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "ODFeeReceiver=" + "'" + Entity.ODFeeReceiver + "'" + "," + "ODFeeReceiveRemark=" + "'" + Entity.ODFeeReceiveRemark + "'" + "," +
                    "ODLastReviser=" + "'" + Entity.ODLastReviser + "'" + "," +
                    "ODLastRevisedDate=" + "'" + Entity.ODLastRevisedDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," +
                    "ODPaidType=" + "'" + Entity.ODPaidType + "'" + "," + "ODPaidBank=" + "'" + Entity.ODPaidBank + "'" + 
                    " where RowPointer='" + Entity.RowPointer + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Op_OrderFeeReceiver where RowPointer='" + Entity.RowPointer + "'");
        }
        
        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="RefRP">外键</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string RefRP, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(RefRP, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="RefRP">外键</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string RefRP)
        {
            return GetListHelper(RefRP, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="RefRP">外键</param>
        /// <returns></returns>
        public int GetListCount(string RefRP)
        {
            string wherestr = "";
            if (RefRP != string.Empty)
            {
                wherestr = wherestr + " and RefRP = '" + RefRP + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Op_OrderFeeReceiver  where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="RefRP">外键</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string RefRP, int startRow, int pageSize)
        {
            string wherestr = "";
            if (RefRP != string.Empty)
            {
                wherestr = wherestr + " and RefRP = '" + RefRP + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Op_OrderFeeReceiver", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Op_OrderFeeReceiver", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Op.EntityOrderFeeReceiver entity = new project.Entity.Op.EntityOrderFeeReceiver();
                entity.RowPointer = dr["RowPointer"].ToString();
                entity.RefRP = dr["RefRP"].ToString();
                entity.ODPaidAmount = ParseDecimalForString(dr["ODPaidAmount"].ToString());
                entity.ODPaidDate = ParseDateTimeForString(dr["ODPaidDate"].ToString());
                entity.ODFeeReceiver = dr["ODFeeReceiver"].ToString();
                entity.ODFeeReceiveRemark = dr["ODFeeReceiveRemark"].ToString();
                entity.ODCreator = dr["ODCreator"].ToString();
                entity.ODCreateDate = ParseDateTimeForString(dr["ODCreateDate"].ToString());
                entity.ODLastReviser = dr["ODLastReviser"].ToString();
                entity.ODLastRevisedDate = ParseDateTimeForString(dr["ODLastRevisedDate"].ToString());
                entity.ODPaidType = dr["ODPaidType"].ToString();
                entity.ODPaidBank = dr["ODPaidBank"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
