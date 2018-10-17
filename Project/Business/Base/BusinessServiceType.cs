using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 服务类型资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessServiceType : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityServiceType _entity = new project.Entity.Base.EntityServiceType();
        public string OrderField = "SRVTypeNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessServiceType() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessServiceType(project.Entity.Base.EntityServiceType entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityServiceType)关联
        /// </summary>
        public project.Entity.Base.EntityServiceType Entity
        {
            get { return _entity as project.Entity.Base.EntityServiceType; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SPShortName from Mstr_ServiceType a left join Mstr_ServiceProvider b on a.SRVSPNo=b.SPNo where a.SRVTypeNo='" + id + "'").Tables[0].Rows[0];
            _entity.SRVTypeNo = dr["SRVTypeNo"].ToString();
            _entity.SRVTypeName = dr["SRVTypeName"].ToString();
            _entity.ParentTypeNo = dr["ParentTypeNo"].ToString();
            _entity.SRVSPNo = dr["SRVSPNo"].ToString();
            _entity.SRVSPName = dr["SPShortName"].ToString();
            _entity.Remark = dr["Remark"].ToString();
            _entity.SRVStatus = bool.Parse(dr["SRVStatus"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_ServiceType(SRVTypeNo,SRVTypeName,ParentTypeNo,SRVSPNo,Remark,SRVStatus)" +
                    "values('" + Entity.SRVTypeNo + "'" + "," + "'" + Entity.SRVTypeName + "'" + "," +
                    "'" + Entity.ParentTypeNo + "'" + "," + "'" + Entity.SRVSPNo + "'" + "," + "'" + Entity.Remark + "'" + ",1)";
            else
                sqlstr = "update Mstr_ServiceType" +
                    " set SRVTypeName=" + "'" + Entity.SRVTypeName + "'" + "," + 
                    "ParentTypeNo=" + "'" + Entity.ParentTypeNo + "'" + "," +
                    "SRVSPNo=" + "'" + Entity.SRVSPNo + "'" + "," +
                    "Remark=" + "'" + Entity.Remark + "'" +
                    " where SRVTypeNo='" + Entity.SRVTypeNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_ServiceType where SRVTypeNo='" + Entity.SRVTypeNo + "'");
        }

        /// </summary>
        /// valid方法 
        /// </summary>
        public int valid()
        {
            objdata.ExecuteNonQuery("update Mstr_ServiceType set SRVStatus=" + (Entity.SRVStatus == true ? "1" : "0") + " where ParentTypeNo='" + Entity.SRVTypeNo + "'");
            return objdata.ExecuteNonQuery("update Mstr_ServiceType set SRVStatus=" + (Entity.SRVStatus == true ? "1" : "0") + " where SRVTypeNo='" + Entity.SRVTypeNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="SRVTypeNo">服务类型编号</param>
        /// <param name="SRVTypeName">服务类型名称</param>
        /// <param name="ParentTypeNo">父项编号</param>
        /// <param name="SRVSPNo">服务商编号</param>
        /// <param name="SRVStatus">状态（true 可用，false不可用）</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string SRVTypeNo, string SRVTypeName, string ParentTypeNo, string SRVSPNo, bool? SRVStatus, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(SRVTypeNo, SRVTypeName, ParentTypeNo, SRVSPNo, SRVStatus, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="SRVTypeNo">服务类型编号</param>
        /// <param name="SRVTypeName">服务类型名称</param>
        /// <param name="ParentTypeNo">父项编号</param>
        /// <param name="SRVSPNo">服务商编号</param>
        /// <param name="SRVStatus">状态（true 可用，false不可用）</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string SRVTypeNo, string SRVTypeName, string ParentTypeNo, string SRVSPNo, bool? SRVStatus)
        {
            return GetListHelper(SRVTypeNo, SRVTypeName, ParentTypeNo, SRVSPNo, SRVStatus, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="SRVTypeNo">服务类型编号</param>
        /// <param name="SRVTypeName">服务类型名称</param>
        /// <param name="ParentTypeNo">父项编号</param>
        /// <param name="SRVSPNo">服务商编号</param>
        /// <param name="SRVStatus">状态（true 可用，false不可用）</param>
        /// <returns></returns>
        public int GetListCount(string SRVTypeNo, string SRVTypeName, string ParentTypeNo, string SRVSPNo, bool? SRVStatus)
        {
            string wherestr = "";
            if (SRVTypeNo != string.Empty)
            {
                wherestr = wherestr + " and SRVTypeNo like '%" + SRVTypeNo + "%'";
            }
            if (SRVTypeName != string.Empty)
            {
                wherestr = wherestr + " and SRVTypeName like '%" + SRVTypeName + "%'";
            }
            if (ParentTypeNo != string.Empty)
            {
                if (ParentTypeNo == "null")
                    wherestr = wherestr + " and isnull(ParentTypeNo,'')=''";
                else
                    wherestr = wherestr + " and ParentTypeNo = '" + ParentTypeNo + "'";
            }
            if (SRVSPNo != string.Empty)
            {
                wherestr = wherestr + " and SRVSPNo like '" + SRVSPNo + "'";
            }
            if (SRVStatus != null)
            {
                wherestr = wherestr + " and SRVStatus=" + (SRVStatus == true ? "1" : "0");
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_ServiceType where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="SRVTypeNo">服务类型编号</param>
        /// <param name="SRVTypeName">服务类型名称</param>
        /// <param name="ParentTypeNo">父项编号</param>
        /// <param name="SRVSPNo">服务商编号</param>
        /// <param name="SRVStatus">状态（true 可用，false不可用）</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string SRVTypeNo, string SRVTypeName, string ParentTypeNo, string SRVSPNo, bool? SRVStatus, int startRow, int pageSize)
        {
            string wherestr = "";
            if (SRVTypeNo != string.Empty)
            {
                wherestr = wherestr + " and SRVTypeNo like '%" + SRVTypeNo + "%'";
            }
            if (SRVTypeName != string.Empty)
            {
                wherestr = wherestr + " and SRVTypeName like '%" + SRVTypeName + "%'";
            }
            if (ParentTypeNo != string.Empty)
            {
                if (ParentTypeNo == "null")
                    wherestr = wherestr + " and isnull(ParentTypeNo,'')=''";
                else
                    wherestr = wherestr + " and ParentTypeNo = '" + ParentTypeNo + "'";
            }
            if (SRVSPNo != string.Empty)
            {
                wherestr = wherestr + " and SRVSPNo like '" + SRVSPNo + "'";
            }
            if (SRVStatus != null)
            {
                wherestr = wherestr + " and SRVStatus=" + (SRVStatus == true ? "1" : "0");
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_ServiceType a left join Mstr_ServiceProvider b on a.SRVSPNo=b.SPNo", "a.*,b.SPShortName", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_ServiceType a left join Mstr_ServiceProvider b on a.SRVSPNo=b.SPNo", "a.*,b.SPShortName", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityServiceType entity = new project.Entity.Base.EntityServiceType();
                entity.SRVTypeNo = dr["SRVTypeNo"].ToString();
                entity.SRVTypeName = dr["SRVTypeName"].ToString();
                entity.ParentTypeNo = dr["ParentTypeNo"].ToString();
                entity.SRVSPNo = dr["SRVSPNo"].ToString();
                entity.SRVSPName = dr["SPShortName"].ToString();
                entity.SRVStatus = bool.Parse(dr["SRVStatus"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
