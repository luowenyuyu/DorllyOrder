using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 费用项目税率
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessTaxRate : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityTaxRate _entity = new project.Entity.Base.EntityTaxRate();
        public string OrderField = "a.CreateDate desc";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessTaxRate() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessTaxRate(project.Entity.Base.EntityTaxRate entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityTaxRate)关联
        /// </summary>
        public project.Entity.Base.EntityTaxRate Entity
        {
            get { return _entity as project.Entity.Base.EntityTaxRate; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SRVName,c.SPShortName as SPName from Mstr_TaxRate a " +
                "left join Mstr_Service b on a.SRVNo=b.SRVNo "+
                "left join Mstr_ServiceProvider c on c.SPNo=a.SPNo " +
                "where a.RP='" + id + "'").Tables[0].Rows[0];
            _entity.RP = dr["RP"].ToString();
            _entity.SPNo = dr["SPNo"].ToString();
            _entity.SPName = dr["SPName"].ToString();
            _entity.SRVNo = dr["SRVNo"].ToString();
            _entity.SRVName = dr["SRVName"].ToString();
            _entity.Rate = ParseDecimalForString(dr["Rate"].ToString());
            _entity.CreateUser = dr["CreateUser"].ToString();
            _entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
            _entity.UpdateUser = dr["UpdateUser"].ToString();
            _entity.UpdateDate = ParseDateTimeForString(dr["UpdateDate"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save()
        {
            string sqlstr = "";
            if (Entity.RP == null)
                sqlstr = "insert into Mstr_TaxRate(RP,SPNo,SRVNo,Rate,CreateUser,CreateDate,UpdateUser,UpdateDate)" +
                    "values(NEWID(),'" + Entity.SPNo + "'" + "," + "'" + Entity.SRVNo + "'" + "," + Entity.Rate + "," +
                    "'" + Entity.CreateUser + "'" + "," + "'" + Entity.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "'" + Entity.UpdateUser + "'" + "," + "'" + Entity.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ")";
            else
                sqlstr = "update Mstr_TaxRate" +
                    " set SPNo=" + "'" + Entity.SPNo + "'" +"," +
                    "SRVNo=" + "'" + Entity.SRVNo + "'" + "," +
                    "Rate=" + Entity.Rate + "," +
                    "UpdateUser=" + "'" + Entity.UpdateUser + "'" + "," +
                    "UpdateDate=" + "'" + Entity.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + 
                    " where RP='" + Entity.RP + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }
        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_TaxRate where RP='" + Entity.RP + "'");
        }

        ///<summary>
        ///按条件查询，支持分页
        ///</summary>
        ///<param name="SPNo">服务商</param>
        ///<param name="SRVNo">服务项目</param>
        ///<returns></returns>
        public System.Collections.ICollection GetListQuery(string SPNo, string SRVNo, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(SPNo, SRVNo, startRow, pageSize);
        }

        ///<summary>
        ///按条件查询，不支持分页
        ///</summary>
        ///<param name="SPNo">服务商</param>
        ///<param name="SRVNo">服务项目</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string SPNo, string SRVNo)
        {
            return GetListHelper(SPNo, SRVNo, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        ///<param name="SPNo">服务商</param>
        ///<param name="SRVNo">服务项目</param>
        /// <returns></returns>
        public int GetListCount(string SPNo, string SRVNo)
        {
            string wherestr = "";
            if (SPNo != string.Empty)
            {
                wherestr = wherestr + " and SPNo = '" + SPNo + "'";
            }
            if (SRVNo != string.Empty)
            {
                wherestr = wherestr + " and SRVNo = '" + SRVNo + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_TaxRate where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        ///<param name="SPNo">服务商</param>
        ///<param name="SRVNo">服务项目</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string SPNo, string SRVNo, int startRow, int pageSize)
        {
            string wherestr = "";
            if (SPNo != string.Empty)
            {
                wherestr = wherestr + " and a.SPNo = '" + SPNo + "'";
            }
            if (SRVNo != string.Empty)
            {
                wherestr = wherestr + " and a.SRVNo = '" + SRVNo + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_TaxRate a "+
                    "left join Mstr_Service b on a.SRVNo=b.SRVNo "+
                    "left join Mstr_ServiceProvider c on c.SPNo=a.SPNo",
                    "a.*,b.SRVName,c.SPShortName as SPName", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_TaxRate a " +
                    "left join Mstr_Service b on a.SRVNo=b.SRVNo " +
                    "left join Mstr_ServiceProvider c on c.SPNo=a.SPNo",
                    "a.*,b.SRVName,c.SPShortName as SPName", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityTaxRate entity = new project.Entity.Base.EntityTaxRate();
                entity.RP = dr["RP"].ToString();
                entity.SPNo = dr["SPNo"].ToString();
                entity.SPName = dr["SPName"].ToString();
                entity.SRVNo = dr["SRVNo"].ToString();
                entity.SRVName = dr["SRVName"].ToString();
                entity.Rate = ParseDecimalForString(dr["Rate"].ToString());
                entity.CreateUser = dr["CreateUser"].ToString();
                entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
                entity.UpdateUser = dr["UpdateUser"].ToString();
                entity.UpdateDate = ParseDateTimeForString(dr["UpdateDate"].ToString());
                entity.CreateUser = dr["CreateUser"].ToString();
                entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
                entity.UpdateUser = dr["UpdateUser"].ToString();
                entity.UpdateDate = ParseDateTimeForString(dr["UpdateDate"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
