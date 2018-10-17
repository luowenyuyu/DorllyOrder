using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 订单类型资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessOrderType : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityOrderType _entity = new project.Entity.Base.EntityOrderType();
        public string OrderField = "OrderTypeNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessOrderType() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessOrderType(project.Entity.Base.EntityOrderType entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityOrderType)关联
        /// </summary>
        public project.Entity.Base.EntityOrderType Entity
        {
            get { return _entity as project.Entity.Base.EntityOrderType; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Mstr_OrderType where OrderTypeNo='" + id + "'").Tables[0].Rows[0];
            _entity.OrderTypeNo = dr["OrderTypeNo"].ToString();
            _entity.OrderTypeName = dr["OrderTypeName"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_OrderType(OrderTypeNo,OrderTypeName)" +
                    "values('" + Entity.OrderTypeNo + "'" + "," + "'" + Entity.OrderTypeName + "'" + ")";
            else
                sqlstr = "update Mstr_OrderType" +
                    " set OrderTypeName=" + "'" + Entity.OrderTypeName + "'" +
                    " where OrderTypeNo='" + Entity.OrderTypeNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_OrderType where OrderTypeNo='" + Entity.OrderTypeNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="OrderTypeNo">类型编号</param>
        /// <param name="OrderTypeName">类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string OrderTypeNo, string OrderTypeName, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(OrderTypeNo, OrderTypeName, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="OrderTypeNo">类型编号</param>
        /// <param name="OrderTypeName">类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string OrderTypeNo, string OrderTypeName)
        {
            return GetListHelper(OrderTypeNo, OrderTypeName, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="OrderTypeNo">类型编号</param>
        /// <param name="OrderTypeName">类型名称</param>
        /// <returns></returns>
        public int GetListCount(string OrderTypeNo, string OrderTypeName)
        {
            string wherestr = "";
            if (OrderTypeNo != string.Empty)
            {
                wherestr = wherestr + " and OrderTypeNo like '%" + OrderTypeNo + "%'";
            }
            if (OrderTypeName != string.Empty)
            {
                wherestr = wherestr + " and OrderTypeName like '%" + OrderTypeName + "%'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_OrderType where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="OrderTypeNo">类型编号</param>
        /// <param name="OrderTypeName">类型名称</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string OrderTypeNo, string OrderTypeName, int startRow, int pageSize)
        {
            string wherestr = "";
            if (OrderTypeNo != string.Empty)
            {
                wherestr = wherestr + " and OrderTypeNo like '%" + OrderTypeNo + "%'";
            }
            if (OrderTypeName != string.Empty)
            {
                wherestr = wherestr + " and OrderTypeName like '%" + OrderTypeName + "%'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_OrderType", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_OrderType", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityOrderType entity = new project.Entity.Base.EntityOrderType();
                entity.OrderTypeNo = dr["OrderTypeNo"].ToString();
                entity.OrderTypeName = dr["OrderTypeName"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
