using System;
using System.Data;
namespace project.Business.Op
{
    /// <summary>
    /// 合同租金费用清单
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-07-31</date>
    public sealed class BusinessContractRMRentList : project.Business.AbstractPmBusiness
    {
        private project.Entity.Op.EntityContractRMRentList _entity = new project.Entity.Op.EntityContractRMRentList();
        public string OrderField = "A.SRVNo,a.RMID,a.WPNo,a.FeeEndDate";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessContractRMRentList() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessContractRMRentList(project.Entity.Op.EntityContractRMRentList entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityContractRMRentList)关联
        /// </summary>
        public project.Entity.Op.EntityContractRMRentList Entity
        {
            get { return _entity as project.Entity.Op.EntityContractRMRentList; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SRVName from Op_ContractRMRentList a left join Mstr_Service b on a.SRVNo=b.SRVNo where a.RowPointer='" + id + "'").Tables[0].Rows[0];
            _entity.RowPointer = dr["RowPointer"].ToString();
            _entity.RefRP = dr["RefRP"].ToString();
            _entity.RMID = dr["RMID"].ToString();
            _entity.WPNo = dr["WPNo"].ToString();
            _entity.SRVNo = dr["SRVNo"].ToString();
            _entity.SRVName = dr["SRVName"].ToString();
            _entity.FeeStartDate = ParseDateTimeForString(dr["FeeStartDate"].ToString());
            _entity.FeeEndDate = ParseDateTimeForString(dr["FeeEndDate"].ToString());
            _entity.FeeAmount = ParseDecimalForString(dr["FeeAmount"].ToString());
            _entity.FeeStatus = dr["FeeStatus"].ToString();
            _entity.Creator = dr["Creator"].ToString();
            _entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save()
        {
            string sqlstr = "";
            if (Entity.RowPointer == null)
                sqlstr = "insert into Op_ContractRMRentList(RowPointer,RefRP,RMID,WPNo,SRVNo,FeeStartDate,FeeEndDate,FeeAmount,FeeStatus,CreateDate,Creator)" +
                    "values(NEWID()," + "'" + Entity.RefRP + "'" + "," + "'" + Entity.RMID + "'" + "," +
                    "'" + Entity.WPNo + "'" + "," + "'" + Entity.SRVNo + "'" + "," +
                    "'" + Entity.FeeStartDate.ToString("yyyy-MM-dd") + "'" + "," + 
                    "'" + Entity.FeeEndDate.ToString("yyyy-MM-dd") + "'" + "," + 
                    Entity.FeeAmount + "," + "'" + Entity.FeeStatus + "'" + "," +
                    "'" + Entity.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + 
                    "'" + Entity.Creator + "')";
            else
                sqlstr = "update Op_ContractRMRentList" +
                    " set RMID=" + "'" + Entity.RMID + "'" + "," + "WPNo=" + "'" + Entity.WPNo + "'" + "," + "SRVNo=" + "'" + Entity.SRVNo + "'" + "," +
                    "FeeStartDate=" + "'" + Entity.FeeStartDate.ToString("yyyy-MM-dd") + "'" + "," +
                    "FeeEndDate=" + "'" + Entity.FeeEndDate.ToString("yyyy-MM-dd") + "'" + "," + 
                    "FeeAmount=" + Entity.FeeAmount + "," +
                    "FeeStatus=" + "'" + Entity.FeeStatus + "'" + 
                    " where RowPointer='" + Entity.RowPointer + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Op_ContractRMRentList where RowPointer='" + Entity.RowPointer + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="RefRP">合同外键</param>
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
        /// <param name="RefRP">合同外键</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string RefRP)
        {
            return GetListHelper(RefRP, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="RefRP">合同外键</param>
        /// <returns></returns>
        public int GetListCount(string RefRP)
        {
            string wherestr = "";
            if (RefRP != string.Empty)
            {
                wherestr = wherestr + " and a.RefRP = '" + RefRP + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Op_ContractRMRentList a where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="RefRP">合同外键</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string RefRP, int startRow, int pageSize)
        {
            string wherestr = "";
            if (RefRP != string.Empty)
            {
                wherestr = wherestr + " and a.RefRP = '" + RefRP + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Op_ContractRMRentList a left join Mstr_Service b on a.SRVNo=b.SRVNo", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Op_ContractRMRentList a left join Mstr_Service b on a.SRVNo=b.SRVNo", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Op.EntityContractRMRentList entity = new project.Entity.Op.EntityContractRMRentList();
                entity.RowPointer = dr["RowPointer"].ToString();
                entity.RefRP = dr["RefRP"].ToString();
                entity.RMID = dr["RMID"].ToString();
                entity.WPNo = dr["WPNo"].ToString();
                entity.SRVNo = dr["SRVNo"].ToString();
                entity.SRVName = dr["SRVName"].ToString();
                entity.FeeStartDate = ParseDateTimeForString(dr["FeeStartDate"].ToString());
                entity.FeeEndDate = ParseDateTimeForString(dr["FeeEndDate"].ToString());
                entity.FeeAmount = ParseDecimalForString(dr["FeeAmount"].ToString());
                entity.FeeStatus = dr["FeeStatus"].ToString();
                entity.Creator = dr["Creator"].ToString();
                entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}