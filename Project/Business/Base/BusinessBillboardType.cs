using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 广告位类型资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessBillboardType : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityBillboardType _entity = new project.Entity.Base.EntityBillboardType();
        public string OrderField = "BBTypeNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessBillboardType() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessBillboardType(project.Entity.Base.EntityBillboardType entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityBillboardType)关联
        /// </summary>
        public project.Entity.Base.EntityBillboardType Entity
        {
            get { return _entity as project.Entity.Base.EntityBillboardType; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Mstr_BillboardType where BBTypeNo='" + id + "'").Tables[0].Rows[0];
            _entity.BBTypeNo = dr["BBTypeNo"].ToString();
            _entity.BBTypeName = dr["BBTypeName"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_BillboardType(BBTypeNo,BBTypeName)" +
                    "values('" + Entity.BBTypeNo + "'" + "," + "'" + Entity.BBTypeName + "'" + ")";
            else
                sqlstr = "update Mstr_BillboardType" +
                    " set BBTypeName=" + "'" + Entity.BBTypeName + "'" +
                    " where BBTypeNo='" + Entity.BBTypeNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_BillboardType where BBTypeNo='" + Entity.BBTypeNo + "'");
        }
        
        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="BBTypeNo">类型编号</param>
        /// <param name="BBTypeName">类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string BBTypeNo, string BBTypeName, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(BBTypeNo, BBTypeName, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="BBTypeNo">类型编号</param>
        /// <param name="BBTypeName">类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string BBTypeNo, string BBTypeName)
        {
            return GetListHelper(BBTypeNo, BBTypeName, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="BBTypeNo">类型编号</param>
        /// <param name="BBTypeName">类型名称</param>
        /// <returns></returns>
        public int GetListCount(string BBTypeNo, string BBTypeName)
        {
            string wherestr = "";
            if (BBTypeNo != string.Empty)
            {
                wherestr = wherestr + " and BBTypeNo like '%" + BBTypeNo + "%'";
            }
            if (BBTypeName != string.Empty)
            {
                wherestr = wherestr + " and BBTypeName like '%" + BBTypeName + "%'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_BillboardType where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="BBTypeNo">类型编号</param>
        /// <param name="BBTypeName">类型名称</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string BBTypeNo, string BBTypeName, int startRow, int pageSize)
        {
            string wherestr = "";
            if (BBTypeNo != string.Empty)
            {
                wherestr = wherestr + " and BBTypeNo like '%" + BBTypeNo + "%'";
            }
            if (BBTypeName != string.Empty)
            {
                wherestr = wherestr + " and BBTypeName like '%" + BBTypeName + "%'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_BillboardType", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_BillboardType", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityBillboardType entity = new project.Entity.Base.EntityBillboardType();
                entity.BBTypeNo = dr["BBTypeNo"].ToString();
                entity.BBTypeName = dr["BBTypeName"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
