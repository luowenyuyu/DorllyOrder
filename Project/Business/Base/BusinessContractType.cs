using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 合同类型资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessContractType : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityContractType _entity = new project.Entity.Base.EntityContractType();
        public string OrderField = "ContractTypeNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessContractType() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessContractType(project.Entity.Base.EntityContractType entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityContractType)关联
        /// </summary>
        public project.Entity.Base.EntityContractType Entity
        {
            get { return _entity as project.Entity.Base.EntityContractType; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Mstr_ContractType where ContractTypeNo='" + id + "'").Tables[0].Rows[0];
            _entity.ContractTypeNo = dr["ContractTypeNo"].ToString();
            _entity.ContractTypeName = dr["ContractTypeName"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_ContractType(ContractTypeNo,ContractTypeName)" +
                    "values('" + Entity.ContractTypeNo + "'" + "," + "'" + Entity.ContractTypeName + "'" + ")";
            else
                sqlstr = "update Mstr_ContractType" +
                    " set ContractTypeName=" + "'" + Entity.ContractTypeName + "'" +
                    " where ContractTypeNo='" + Entity.ContractTypeNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_ContractType where ContractTypeNo='" + Entity.ContractTypeNo + "'");
        }
        
        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="ContractTypeNo">类型编号</param>
        /// <param name="ContractTypeName">类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string ContractTypeNo, string ContractTypeName, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(ContractTypeNo, ContractTypeName, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="ContractTypeNo">类型编号</param>
        /// <param name="ContractTypeName">类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string ContractTypeNo, string ContractTypeName)
        {
            return GetListHelper(ContractTypeNo, ContractTypeName, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="ContractTypeNo">类型编号</param>
        /// <param name="ContractTypeName">类型名称</param>
        /// <returns></returns>
        public int GetListCount(string ContractTypeNo, string ContractTypeName)
        {
            string wherestr = "";
            if (ContractTypeNo != string.Empty)
            {
                wherestr = wherestr + " and ContractTypeNo like '%" + ContractTypeNo + "%'";
            }
            if (ContractTypeName != string.Empty)
            {
                wherestr = wherestr + " and ContractTypeName like '%" + ContractTypeName + "%'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_ContractType where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="ContractTypeNo">类型编号</param>
        /// <param name="ContractTypeName">类型名称</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string ContractTypeNo, string ContractTypeName, int startRow, int pageSize)
        {
            string wherestr = "";
            if (ContractTypeNo != string.Empty)
            {
                wherestr = wherestr + " and ContractTypeNo like '%" + ContractTypeNo + "%'";
            }
            if (ContractTypeName != string.Empty)
            {
                wherestr = wherestr + " and ContractTypeName like '%" + ContractTypeName + "%'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_ContractType", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_ContractType", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityContractType entity = new project.Entity.Base.EntityContractType();
                entity.ContractTypeNo = dr["ContractTypeNo"].ToString();
                entity.ContractTypeName = dr["ContractTypeName"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
