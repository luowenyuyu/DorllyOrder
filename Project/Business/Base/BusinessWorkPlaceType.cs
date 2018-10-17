using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 工位类型资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessWorkPlaceType : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityWorkPlaceType _entity = new project.Entity.Base.EntityWorkPlaceType();
        public string OrderField = "WPTypeNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessWorkPlaceType() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessWorkPlaceType(project.Entity.Base.EntityWorkPlaceType entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityWorkPlaceType)关联
        /// </summary>
        public project.Entity.Base.EntityWorkPlaceType Entity
        {
            get { return _entity as project.Entity.Base.EntityWorkPlaceType; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Mstr_WorkPlaceType where WPTypeNo='" + id + "'").Tables[0].Rows[0];
            _entity.WPTypeNo = dr["WPTypeNo"].ToString();
            _entity.WPTypeName = dr["WPTypeName"].ToString();
            _entity.WPTypeSeat = ParseIntForString(dr["WPTypeSeat"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_WorkPlaceType(WPTypeNo,WPTypeName,WPTypeSeat)" +
                    "values('" + Entity.WPTypeNo + "'" + "," + "'" + Entity.WPTypeName + "'" + "," + Entity.WPTypeSeat + ")";
            else
                sqlstr = "update Mstr_WorkPlaceType" +
                    " set WPTypeName=" + "'" + Entity.WPTypeName + "'" + "," + "WPTypeSeat=" + Entity.WPTypeSeat + 
                    " where WPTypeNo='" + Entity.WPTypeNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_WorkPlaceType where WPTypeNo='" + Entity.WPTypeNo + "'");
        }
        
        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="WPTypeNo">类型编号</param>
        /// <param name="WPTypeName">类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string WPTypeNo, string WPTypeName, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(WPTypeNo, WPTypeName, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="WPTypeNo">类型编号</param>
        /// <param name="WPTypeName">类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string WPTypeNo, string WPTypeName)
        {
            return GetListHelper(WPTypeNo, WPTypeName, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="WPTypeNo">类型编号</param>
        /// <param name="WPTypeName">类型名称</param>
        /// <returns></returns>
        public int GetListCount(string WPTypeNo, string WPTypeName)
        {
            string wherestr = "";
            if (WPTypeNo != string.Empty)
            {
                wherestr = wherestr + " and WPTypeNo like '%" + WPTypeNo + "%'";
            }
            if (WPTypeName != string.Empty)
            {
                wherestr = wherestr + " and WPTypeName like '%" + WPTypeName + "%'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_WorkPlaceType where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="WPTypeNo">类型编号</param>
        /// <param name="WPTypeName">类型名称</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string WPTypeNo, string WPTypeName, int startRow, int pageSize)
        {
            string wherestr = "";
            if (WPTypeNo != string.Empty)
            {
                wherestr = wherestr + " and WPTypeNo like '%" + WPTypeNo + "%'";
            }
            if (WPTypeName != string.Empty)
            {
                wherestr = wherestr + " and WPTypeName like '%" + WPTypeName + "%'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_WorkPlaceType", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_WorkPlaceType", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityWorkPlaceType entity = new project.Entity.Base.EntityWorkPlaceType();
                entity.WPTypeNo = dr["WPTypeNo"].ToString();
                entity.WPTypeName = dr["WPTypeName"].ToString();
                entity.WPTypeSeat = ParseIntForString(dr["WPTypeSeat"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
