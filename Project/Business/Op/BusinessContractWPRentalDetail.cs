using System;
using System.Data;
namespace project.Business.Op
{
    /// <summary>
    /// 房屋租赁明细
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-07-20</date>
    public sealed class BusinessContractWPRentalDetail : project.Business.AbstractPmBusiness
    {
        private project.Entity.Op.EntityContractWPRentalDetail _entity = new project.Entity.Op.EntityContractWPRentalDetail();
        public string OrderField = "a.CreateDate desc";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessContractWPRentalDetail() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessContractWPRentalDetail(project.Entity.Op.EntityContractWPRentalDetail entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityContractWPRentalDetail)关联
        /// </summary>
        public project.Entity.Op.EntityContractWPRentalDetail Entity
        {
            get { return _entity as project.Entity.Op.EntityContractWPRentalDetail; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SRVName from Op_ContractWPRentalDetail a left join Mstr_Service b on a.SRVNo=b.SRVNo where a.RowPointer='" + id + "'").Tables[0].Rows[0];
            _entity.RowPointer = dr["RowPointer"].ToString();
            _entity.RefRP = dr["RefRP"].ToString();
            _entity.RMID = dr["RMID"].ToString();
            _entity.SRVNo = dr["SRVNo"].ToString();
            _entity.SRVName = dr["SRVName"].ToString();
            _entity.WPNo = dr["WPNo"].ToString();
            _entity.RMLoc = dr["RMLoc"].ToString();
            _entity.WPQTY = ParseIntForString(dr["WPQTY"].ToString());
            _entity.WPRentalUnitPrice = ParseDecimalForString(dr["WPRentalUnitPrice"].ToString());
            _entity.Remark = dr["Remark"].ToString();
            _entity.Creator = dr["Creator"].ToString();
            _entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
            _entity.LastReviser = dr["LastReviser"].ToString();
            _entity.LastReviseDate = ParseDateTimeForString(dr["LastReviseDate"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save()
        {
            string sqlstr = "";
            if (Entity.RowPointer == null)
            {
                sqlstr = "insert into Op_ContractWPRentalDetail(RowPointer,RefRP,RMID,SRVNo,WPNo,RMLoc,WPQTY,WPRentalUnitPrice,Remark," +
                        @"CreateDate,Creator,LastReviseDate,LastReviser,
                        IsFixedAmt,Amount,IncreaseType,IncreaseStartDate1,IncreaseRate1,IncreaseStartDate2,IncreaseRate2,IncreaseStartDate3,IncreaseRate3,
                        IncreaseStartDate4,IncreaseRate4)" +
                    "values(NEWID()," + "'" + Entity.RefRP + "'" + "," + "'" + Entity.RMID + "'" + "," + "'" + Entity.SRVNo + "'" + "," +
                    "'" + Entity.WPNo + "'" + "," + "'" + Entity.RMLoc + "'" + "," +
                    Entity.WPQTY + "," + Entity.WPRentalUnitPrice + "," + "'" + Entity.Remark + "'" + "," +
                    "'" + Entity.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + "'" + Entity.Creator + "'" + "," +
                    "'" + Entity.LastReviseDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + "'" + Entity.LastReviser + "'," +
                     //新增
                     (Entity.IsFixedAmt ? "1" : "0") + "," + Entity.Amount + "," +
                    "'" + Entity.IncreaseType + "'" + "," +
                    "'" + Entity.IncreaseStartDate1.ToString("yyyy-MM-dd") + "'" + "," + Entity.IncreaseRate1 + "," +
                    "'" + Entity.IncreaseStartDate2.ToString("yyyy-MM-dd") + "'" + "," + Entity.IncreaseRate2 + "," +
                    "'" + Entity.IncreaseStartDate3.ToString("yyyy-MM-dd") + "'" + "," + Entity.IncreaseRate3 + "," +
                    "'" + Entity.IncreaseStartDate4.ToString("yyyy-MM-dd") + "'" + "," + Entity.IncreaseRate4 + ")";
            }
                
            else
                sqlstr = "update Op_ContractWPRentalDetail" +
                    " set RMID=" + "'" + Entity.RMID + "'" + "," + "SRVNo=" + "'" + Entity.SRVNo + "'" + "," +
                    "WPNo=" + "'" + Entity.WPNo + "'" + "," + "RMLoc=" + "'" + Entity.RMLoc + "'" + "," +
                    "WPQTY=" + Entity.WPQTY + "," + "WPRentalUnitPrice=" + Entity.WPRentalUnitPrice + "," + "Remark=" + "'" + Entity.Remark + "'" + "," +
                    "LastReviseDate=" + "'" + Entity.LastReviseDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + "LastReviser=" + "'" + Entity.LastReviser + "'"+ ","+

                    //新增
                    "IsFixedAmt=" + "'" + (Entity.IsFixedAmt ? "1" : "0") + "'" + "," + "Amount=" + Entity.Amount + "," +
                    "IncreaseType=" + "'" + Entity.IncreaseType + "'" + "," +
                    "IncreaseStartDate1=" + "'" + Entity.IncreaseStartDate1.ToString("yyyy-MM-dd") + "'" + "," + "IncreaseRate1=" + Entity.IncreaseRate1 + "," +
                    "IncreaseStartDate2=" + "'" + Entity.IncreaseStartDate2.ToString("yyyy-MM-dd") + "'" + "," + "IncreaseRate2=" + Entity.IncreaseRate2 + "," +
                    "IncreaseStartDate3=" + "'" + Entity.IncreaseStartDate3.ToString("yyyy-MM-dd") + "'" + "," + "IncreaseRate3=" + Entity.IncreaseRate3 + "," +
                    "IncreaseStartDate4=" + "'" + Entity.IncreaseStartDate4.ToString("yyyy-MM-dd") + "'" + "," + "IncreaseRate4=" + Entity.IncreaseRate4 +

                    " where RowPointer='" + Entity.RowPointer + "'";

            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Op_ContractWPRentalDetail where RowPointer='" + Entity.RowPointer + "'");
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

            string count = objdata.PopulateDataSet("select count(*) as cnt from Op_ContractWPRentalDetail a where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
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
                entitys = Query(objdata.ExecSelect("Op_ContractWPRentalDetail a left join Mstr_Service b on a.SRVNo=b.SRVNo",
                    "a.*,b.SRVName", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Op_ContractWPRentalDetail a left join Mstr_Service b on a.SRVNo=b.SRVNo",
                    "a.*,b.SRVName", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Op.EntityContractWPRentalDetail entity = new project.Entity.Op.EntityContractWPRentalDetail();
                entity.RowPointer = dr["RowPointer"].ToString();
                entity.RefRP = dr["RefRP"].ToString();
                entity.RMID = dr["RMID"].ToString();
                entity.SRVNo = dr["SRVNo"].ToString();
                entity.SRVName = dr["SRVName"].ToString();
                entity.WPNo = dr["WPNo"].ToString();
                entity.RMLoc = dr["RMLoc"].ToString();
                entity.WPQTY = ParseIntForString(dr["WPQTY"].ToString());
                entity.WPRentalUnitPrice = ParseDecimalForString(dr["WPRentalUnitPrice"].ToString());
                entity.Remark = dr["Remark"].ToString();
                entity.Creator = dr["Creator"].ToString();
                entity.CreateDate = ParseDateTimeForString(dr["CreateDate"].ToString());
                entity.LastReviser = dr["LastReviser"].ToString();
                entity.LastReviseDate = ParseDateTimeForString(dr["LastReviseDate"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
