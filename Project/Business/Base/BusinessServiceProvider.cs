using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 服务商资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessServiceProvider : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityServiceProvider _entity = new project.Entity.Base.EntityServiceProvider();
        public string OrderField = "SPNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessServiceProvider() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessServiceProvider(project.Entity.Base.EntityServiceProvider entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityServiceProvider)关联
        /// </summary>
        public project.Entity.Base.EntityServiceProvider Entity
        {
            get { return _entity as project.Entity.Base.EntityServiceProvider; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SRVTypeName from Mstr_ServiceProvider a left join Mstr_ServiceType b on a.MService=b.SRVTypeNo where a.SPNo='" + id + "'").Tables[0].Rows[0];
            _entity.SPNo = dr["SPNo"].ToString();
            _entity.SPName = dr["SPName"].ToString();
            _entity.SPShortName = dr["SPShortName"].ToString();
            _entity.MService = dr["MService"].ToString();
            _entity.MServiceName = dr["SRVTypeName"].ToString();
            _entity.SPLicenseNo = dr["SPLicenseNo"].ToString();
            _entity.SPContact = dr["SPContact"].ToString();
            _entity.SPContactMobile = dr["SPContactMobile"].ToString();
            _entity.SPTel = dr["SPTel"].ToString();
            _entity.SPEMail = dr["SPEMail"].ToString();
            _entity.SPAddr = dr["SPAddr"].ToString();
            _entity.SPBank = dr["SPBank"].ToString();
            _entity.SPBankAccount = dr["SPBankAccount"].ToString();
            _entity.SPBankTitle = dr["SPBankTitle"].ToString();
            _entity.SPStatus = bool.Parse(dr["SPStatus"].ToString());
            _entity.U8Account = dr["U8Account"].ToString();
            _entity.BankAccount = dr["BankAccount"].ToString();
            _entity.CashAccount = dr["CashAccount"].ToString();
            _entity.TaxAccount = dr["TaxAccount"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_ServiceProvider(SPNo,SPName,SPShortName,MService,SPLicenseNo,SPContact,SPContactMobile,SPTel,SPEMail,SPAddr,"+
                    "SPBank,SPBankAccount,SPBankTitle,SPStatus,U8Account,BankAccount,CashAccount,TaxAccount)" +
                    "values('" + Entity.SPNo + "'" + "," + "'" + Entity.SPName + "'" + "," + "'" + Entity.SPShortName + "'" + "," +
                    "'" + Entity.MService + "'" + "," + "'" + Entity.SPLicenseNo + "'" + "," + "'" + Entity.SPContact + "'" + "," + "'" + Entity.SPContactMobile + "'" + "," +
                    "'" + Entity.SPTel + "'" + "," + "'" + Entity.SPEMail + "'" + "," + "'" + Entity.SPAddr + "'" + "," + "'" + Entity.SPBank + "'" + "," +
                    "'" + Entity.SPBankAccount + "'" + "," + "'" + Entity.SPBankTitle + "'" + ",1,'" + Entity.U8Account + "',"+
                    "'" + Entity.BankAccount + "','" + Entity.CashAccount + "','" + Entity.TaxAccount + "')";
            else
                sqlstr = "update Mstr_ServiceProvider" +
                    " set SPName=" + "'" + Entity.SPName + "'" + "," + "SPShortName=" + "'" + Entity.SPShortName + "'" + "," + "MService=" + "'" + Entity.MService + "'" + "," +
                    "SPLicenseNo=" + "'" + Entity.SPLicenseNo + "'" + "," + "SPContact=" + "'" + Entity.SPContact + "'" + "," + "SPContactMobile=" + "'" + Entity.SPContactMobile + "'" + "," +
                    "SPTel=" + "'" + Entity.SPTel + "'" + "," + "SPEMail=" + "'" + Entity.SPEMail + "'" + "," + "SPAddr=" + "'" + Entity.SPAddr + "'" + "," + "SPBank=" + "'" + Entity.SPBank + "'" + "," +
                    "SPBankAccount=" + "'" + Entity.SPBankAccount + "'" + "," + "SPBankTitle=" + "'" + Entity.SPBankTitle + "'" + "," + "U8Account=" + "'" + Entity.U8Account + "'" + "," +
                    "BankAccount=" + "'" + Entity.BankAccount + "'" + "," + "CashAccount=" + "'" + Entity.CashAccount + "'" + "," + "TaxAccount=" + "'" + Entity.TaxAccount + "'" +
                    " where SPNo='" + Entity.SPNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_ServiceProvider where SPNo='" + Entity.SPNo + "'");
        }

        /// </summary>
        /// SPStatus方法 
        /// </summary>
        public int valid()
        {
            return objdata.ExecuteNonQuery("update Mstr_ServiceProvider set SPStatus=" + (Entity.SPStatus == true ? "1" : "0") + " where SPNo='" + Entity.SPNo + "'");
        }
        
        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="SPNo">服务商编号</param>
        /// <param name="SPName">服务商姓名</param>
        /// <param name="SPStatus">是否有效</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string SPNo, string SPName, bool? SPStatus, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(SPNo, SPName, SPStatus, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="SPNo">服务商编号</param>
        /// <param name="SPName">服务商姓名</param>
        /// <param name="SPStatus">是否有效</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string SPNo, string SPName, bool? SPStatus)
        {
            return GetListHelper(SPNo, SPName, SPStatus, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="SPNo">服务商编号</param>
        /// <param name="SPName">服务商姓名</param>
        /// <param name="SPStatus">是否有效</param>
        /// <returns></returns>
        public int GetListCount(string SPNo, string SPName, bool? SPStatus)
        {
            string wherestr = "";
            if (SPNo != string.Empty)
            {
                wherestr = wherestr + " and SPNo like '%" + SPNo + "%'";
            }
            if (SPName != string.Empty)
            {
                wherestr = wherestr + " and SPName like '%" + SPName + "%'";
            }
            if (SPStatus != null)
            {
                wherestr = wherestr + " and SPStatus=" + (SPStatus == true ? "1" : "0");
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_ServiceProvider where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="SPNo">服务商编号</param>
        /// <param name="SPName">服务商姓名</param>
        /// <param name="SPStatus">是否有效</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string SPNo, string SPName, bool? SPStatus, int startRow, int pageSize)
        {
            string wherestr = "";
            if (SPNo != string.Empty)
            {
                wherestr = wherestr + " and SPNo like '%" + SPNo + "%'";
            }
            if (SPName != string.Empty)
            {
                wherestr = wherestr + " and SPName like '%" + SPName + "%'";
            }
            if (SPStatus != null)
            {
                wherestr = wherestr + " and SPStatus=" + (SPStatus == true ? "1" : "0");
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_ServiceProvider a left join Mstr_ServiceType b on a.MService=b.SRVTypeNo", "a.*,b.SRVTypeName", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_ServiceProvider a left join Mstr_ServiceType b on a.MService=b.SRVTypeNo", "a.*,b.SRVTypeName", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityServiceProvider entity = new project.Entity.Base.EntityServiceProvider();
                entity.SPNo = dr["SPNo"].ToString();
                entity.SPName = dr["SPName"].ToString();
                entity.SPShortName = dr["SPShortName"].ToString();
                entity.MService = dr["MService"].ToString();
                entity.MServiceName = dr["SRVTypeName"].ToString();
                entity.SPLicenseNo = dr["SPLicenseNo"].ToString();
                entity.SPContact = dr["SPContact"].ToString();
                entity.SPContactMobile = dr["SPContactMobile"].ToString();
                entity.SPTel = dr["SPTel"].ToString();
                entity.SPEMail = dr["SPEMail"].ToString();
                entity.SPAddr = dr["SPAddr"].ToString();
                entity.SPBank = dr["SPBank"].ToString();
                entity.SPBankAccount = dr["SPBankAccount"].ToString();
                entity.SPBankTitle = dr["SPBankTitle"].ToString();
                entity.SPStatus = bool.Parse(dr["SPStatus"].ToString());
                entity.U8Account = dr["U8Account"].ToString();
                entity.BankAccount = dr["BankAccount"].ToString();
                entity.CashAccount = dr["CashAccount"].ToString();
                entity.TaxAccount = dr["TaxAccount"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
