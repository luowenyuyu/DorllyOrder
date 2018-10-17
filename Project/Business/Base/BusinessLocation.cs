using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 园区/建设期/楼栋/楼层资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessLocation : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityLocation _entity = new project.Entity.Base.EntityLocation();
        public string OrderField = "LOCNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessLocation() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessLocation(project.Entity.Base.EntityLocation entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityLocation)关联
        /// </summary>
        public project.Entity.Base.EntityLocation Entity
        {
            get { return _entity as project.Entity.Base.EntityLocation; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Mstr_Location where LOCNo='" + id + "'").Tables[0].Rows[0];
            _entity.LOCNo = dr["LOCNo"].ToString();
            _entity.LOCName = dr["LOCName"].ToString();
            _entity.ParentLOCNo = dr["ParentLOCNo"].ToString();
            _entity.LOCLevel = ParseIntForString(dr["LOCLevel"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_Location(LOCNo,LOCName,ParentLOCNo,LOCLevel)" +
                    "values('" + Entity.LOCNo + "'" + "," + "'" + Entity.LOCName + "'" + "," +
                    "'" + Entity.ParentLOCNo + "'" + "," + Entity.LOCLevel + ")";
            else
                sqlstr = "update Mstr_Location" +
                    " set LOCName=" + "'" + Entity.LOCName + "'" + "," +
                    "ParentLOCNo=" + "'" + Entity.ParentLOCNo + "'" + "," +
                    "LOCLevel=" + Entity.LOCLevel + 
                    " where LOCNo='" + Entity.LOCNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_Location where LOCNo='" + Entity.LOCNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="LOCNo">编号</param>
        /// <param name="LOCName">名称</param>
        /// <param name="ParentLOCNo">父项编号</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string LOCNo, string LOCName, string ParentLOCNo, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(LOCNo, LOCName, ParentLOCNo, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="LOCNo">编号</param>
        /// <param name="LOCName">名称</param>
        /// <param name="ParentLOCNo">父项编号</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string LOCNo, string LOCName, string ParentLOCNo)
        {
            return GetListHelper(LOCNo, LOCName, ParentLOCNo, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="LOCNo">编号</param>
        /// <param name="LOCName">名称</param>
        /// <param name="ParentLOCNo">父项编号</param>
        /// <returns></returns>
        public int GetListCount(string LOCNo, string LOCName, string ParentLOCNo)
        {
            string wherestr = "";
            if (LOCNo != string.Empty)
            {
                wherestr = wherestr + " and LOCNo like '%" + LOCNo + "%'";
            }
            if (LOCName != string.Empty)
            {
                wherestr = wherestr + " and LOCName like '%" + LOCName + "%'";
            }
            if (ParentLOCNo != string.Empty)
            {
                if (ParentLOCNo == "null")
                    wherestr = wherestr + " and isnull(ParentLOCNo,'')=''";
                else
                    wherestr = wherestr + " and ParentLOCNo = '" + ParentLOCNo + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_Location where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="LOCNo">编号</param>
        /// <param name="LOCName">名称</param>
        /// <param name="ParentLOCNo">父项编号</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string LOCNo, string LOCName, string ParentLOCNo, int startRow, int pageSize)
        {
            string wherestr = "";
            if (LOCNo != string.Empty)
            {
                wherestr = wherestr + " and LOCNo like '%" + LOCNo + "%'";
            }
            if (LOCName != string.Empty)
            {
                wherestr = wherestr + " and LOCName like '%" + LOCName + "%'";
            }
            if (ParentLOCNo != string.Empty)
            {
                if (ParentLOCNo == "null")
                    wherestr = wherestr + " and isnull(ParentLOCNo,'')=''";
                else
                    wherestr = wherestr + " and ParentLOCNo = '" + ParentLOCNo + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_Location", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_Location", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityLocation entity = new project.Entity.Base.EntityLocation();
                entity.LOCNo = dr["LOCNo"].ToString();
                entity.LOCName = dr["LOCName"].ToString();
                entity.ParentLOCNo = dr["ParentLOCNo"].ToString();
                entity.LOCLevel = ParseIntForString(dr["LOCLevel"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
