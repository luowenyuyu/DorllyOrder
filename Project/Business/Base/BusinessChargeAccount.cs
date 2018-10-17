using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 收费科目资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessChargeAccount : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityChargeAccount _entity = new project.Entity.Base.EntityChargeAccount();
        public string OrderField = "a.CASPNo,a.CANo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessChargeAccount() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessChargeAccount(project.Entity.Base.EntityChargeAccount entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityChargeAccount)关联
        /// </summary>
        public project.Entity.Base.EntityChargeAccount Entity
        {
            get { return _entity as project.Entity.Base.EntityChargeAccount; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SPShortName as CASPName from Mstr_ChargeAccount a left join Mstr_ServiceProvider b on a.CASPNo=b.SPNo where a.CANo='" + id + "'").Tables[0].Rows[0];
            _entity.CANo = dr["CANo"].ToString();
            _entity.CAName = dr["CAName"].ToString();
            _entity.CASPNo = dr["CASPNo"].ToString();
            _entity.CASPName = dr["CASPName"].ToString();
            _entity.APNo = dr["APNo"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_ChargeAccount(CANo,CAName,CASPNo,APNo)" +
                    "values('" + Entity.CANo + "'" + "," + "'" + Entity.CAName + "'" + "," + "'" + Entity.CASPNo + "'" + "," + "'" + Entity.APNo + "'" + ")";
            else
                sqlstr = "update Mstr_ChargeAccount" +
                    " set CAName=" + "'" + Entity.CAName + "'" + "," + "APNo=" + "'" + Entity.APNo + "'" +
                    " where CANo='" + Entity.CANo + "' and CASPNo=" + "'" + Entity.CASPNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_ChargeAccount where CANo='" + Entity.CANo + "' and CASPNo='" + Entity.CASPNo + "'");
        }
        
        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="CANo">收费科目编号</param>
        /// <param name="CAName">收费科目名称</param>
        /// <param name="CASPNo">服务商编号</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string CANo, string CAName, string CASPNo, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(CANo, CAName, CASPNo, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="CANo">收费科目编号</param>
        /// <param name="CAName">收费科目名称</param>
        /// <param name="CASPNo">服务商编号</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string CANo, string CAName, string CASPNo)
        {
            return GetListHelper(CANo, CAName, CASPNo, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="CANo">收费科目编号</param>
        /// <param name="CAName">收费科目名称</param>
        /// <param name="CASPNo">服务商编号</param>
        /// <returns></returns>
        public int GetListCount(string CANo, string CAName, string CASPNo)
        {
            string wherestr = "";
            if (CANo != string.Empty)
            {
                wherestr = wherestr + " and a.CANo like '%" + CANo + "%'";
            }
            if (CAName != string.Empty)
            {
                wherestr = wherestr + " and a.CAName like '%" + CAName + "%'";
            }
            if (CASPNo != string.Empty)
            {
                wherestr = wherestr + " and a.CASPNo = '" + CASPNo + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_ChargeAccount a where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="CANo">收费科目编号</param>
        /// <param name="CAName">收费科目名称</param>
        /// <param name="CASPNo">服务商编号</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string CANo, string CAName, string CASPNo, int startRow, int pageSize)
        {
            string wherestr = "";
            if (CANo != string.Empty)
            {
                wherestr = wherestr + " and a.CANo like '%" + CANo + "%'";
            }
            if (CAName != string.Empty)
            {
                wherestr = wherestr + " and a.CAName like '%" + CAName + "%'";
            }
            if (CASPNo != string.Empty)
            {
                wherestr = wherestr + " and a.CASPNo = '" + CASPNo + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_ChargeAccount a left join Mstr_ServiceProvider b on a.CASPNo=b.SPNo",
                    "a.*,b.SPShortName as CASPName",
                    wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_ChargeAccount a left join Mstr_ServiceProvider b on a.CASPNo=b.SPNo",
                    "a.*,b.SPShortName as CASPName",
                    wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityChargeAccount entity = new project.Entity.Base.EntityChargeAccount();
                entity.CANo = dr["CANo"].ToString();
                entity.CAName = dr["CAName"].ToString();
                entity.CASPNo = dr["CASPNo"].ToString();
                entity.CASPName = dr["CASPName"].ToString();
                entity.APNo = dr["APNo"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
