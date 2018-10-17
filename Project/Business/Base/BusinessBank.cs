using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 银行资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-10-18</date>
    public sealed class BusinessBank : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityBank _entity = new project.Entity.Base.EntityBank();
        public string OrderField = "BankNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessBank() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessBank(project.Entity.Base.EntityBank entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityBank)关联
        /// </summary>
        public project.Entity.Base.EntityBank Entity
        {
            get { return _entity as project.Entity.Base.EntityBank; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Mstr_Bank where BankNo='" + id + "'").Tables[0].Rows[0];
            _entity.BankNo = dr["BankNo"].ToString();
            _entity.BankName = dr["BankName"].ToString();
            _entity.BankAccount = dr["BankAccount"].ToString();
            _entity.Valid = bool.Parse(dr["Valid"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_Bank(BankNo,BankName,BankAccount,Valid)" +
                    "values('" + Entity.BankNo + "'" + "," + "'" + Entity.BankName + "'," + "'" + Entity.BankAccount + "',1)";
            else
                sqlstr = "update Mstr_Bank" +
                    " set BankName=" + "'" + Entity.BankName + "'" + "," +
                    "BankAccount=" + "'" + Entity.BankAccount + "'" + "," +
                    "Valid=" + (Entity.Valid ? "1" : "0") +
                    " where BankNo='" + Entity.BankNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_Bank where BankNo='" + Entity.BankNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="BankNo">编号</param>
        /// <param name="BankName">名称</param>
        /// <param name="Valid">是否有效</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string BankNo, string BankName, bool? Valid, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(BankNo, BankName, Valid, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="BankNo">编号</param>
        /// <param name="BankName">名称</param>
        /// <param name="Valid">是否有效</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string BankNo, string BankName, bool? Valid)
        {
            return GetListHelper(BankNo, BankName, Valid, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="BankNo">编号</param>
        /// <param name="BankName">名称</param>
        /// <param name="Valid">是否有效</param>
        /// <returns></returns>
        public int GetListCount(string BankNo, string BankName, bool? Valid)
        {
            string wherestr = "";
            if (BankNo != string.Empty)
            {
                wherestr = wherestr + " and BankNo like '%" + BankNo + "%'";
            }
            if (BankName != string.Empty)
            {
                wherestr = wherestr + " and BankName like '%" + BankName + "%'";
            }
            if (Valid != null)
            {
                wherestr = wherestr + " and Valid = '" + (Valid == true ? "1" : "0") + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_Bank where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="BankNo">编号</param>
        /// <param name="BankName">名称</param>
        /// <param name="Valid">是否有效</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string BankNo, string BankName, bool? Valid, int startRow, int pageSize)
        {
            string wherestr = "";
            if (BankNo != string.Empty)
            {
                wherestr = wherestr + " and BankNo like '%" + BankNo + "%'";
            }
            if (BankName != string.Empty)
            {
                wherestr = wherestr + " and BankName like '%" + BankName + "%'";
            }
            if (Valid != null)
            {
                wherestr = wherestr + " and Valid = '" + (Valid == true ? "1" : "0") + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_Bank", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_Bank", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityBank entity = new project.Entity.Base.EntityBank();
                entity.BankNo = dr["BankNo"].ToString();
                entity.BankName = dr["BankName"].ToString();
                entity.BankAccount = dr["BankAccount"].ToString();
                entity.Valid = bool.Parse(dr["Valid"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
