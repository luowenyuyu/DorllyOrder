using System;
using System.Data;
using System.Data.SqlClient;
namespace project.Business.Op
{
    /// <summary>
    /// 订单信息
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-08-01</date>
    public sealed class BusinessOrderDetail : project.Business.AbstractPmBusiness
    {
        private project.Entity.Op.EntityOrderDetail _entity = new project.Entity.Op.EntityOrderDetail();
        public string OrderField = "ODCreateDate desc";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessOrderDetail() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessOrderDetail(project.Entity.Op.EntityOrderDetail entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityOrderDetail)关联
        /// </summary>
        public project.Entity.Op.EntityOrderDetail Entity
        {
            get { return _entity as project.Entity.Op.EntityOrderDetail; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SRVName as ODSRVName,c.SPShortName as ODContractSPName,d.CAName as ODCAName from Op_OrderDetail a " +
                "left join Mstr_Service b on a.ODSRVNo=b.SRVNo "+
                "left join Mstr_ServiceProvider c on a.ODContractSPNo=c.SPNo "+
                "left join Mstr_ChargeAccount d on a.ODCANo=d.CANo and d.CASPNo=b.SRVSPNo " +
                "where a.RowPointer='" + id + "'").Tables[0].Rows[0];
            _entity.RowPointer = dr["RowPointer"].ToString();
            _entity.RefRP = dr["RefRP"].ToString();
            _entity.ODSRVNo = dr["ODSRVNo"].ToString();
            _entity.ODSRVTypeNo1 = dr["ODSRVTypeNo1"].ToString();
            _entity.ODSRVTypeNo2 = dr["ODSRVTypeNo2"].ToString();
            _entity.ODSRVName = dr["ODSRVName"].ToString();
            _entity.ODSRVRemark = dr["ODSRVRemark"].ToString();
            _entity.ODSRVCalType = dr["ODSRVCalType"].ToString();
            _entity.ODContractSPNo = dr["ODContractSPNo"].ToString();
            _entity.ODContractSPName = dr["ODContractSPName"].ToString();
            _entity.ODContractNo = dr["ODContractNo"].ToString();
            _entity.ODContractNoManual = dr["ODContractNoManual"].ToString();
            _entity.ResourceNo = dr["ResourceNo"].ToString();
            _entity.ResourceName = dr["ResourceName"].ToString();
            _entity.ODFeeStartDate = ParseDateTimeForString(dr["ODFeeStartDate"].ToString());
            _entity.ODFeeEndDate = ParseDateTimeForString(dr["ODFeeEndDate"].ToString());
            _entity.BillingDays = ParseIntForString(dr["BillingDays"].ToString());
            _entity.ODQTY = ParseDecimalForString(dr["ODQTY"].ToString());
            _entity.ODUnit = dr["ODUnit"].ToString();
            _entity.ODUnitPrice = ParseDecimalForString(dr["ODUnitPrice"].ToString());
            _entity.ODARAmount = ParseDecimalForString(dr["ODARAmount"].ToString());
            _entity.ODPaidAmount = ParseDecimalForString(dr["ODPaidAmount"].ToString());
            //_entity.ODPaidinAmount = ParseDecimalForString(dr["ODPaidinAmount"].ToString());
            //_entity.ODPaidDate = ParseDateTimeForString(dr["ODPaidDate"].ToString());
            //_entity.ODFeeReceiver = dr["ODFeeReceiver"].ToString();
            //_entity.ODFeeReceiveRemark = dr["ODFeeReceiveRemark"].ToString();
            //_entity.ODReduceAmount = ParseDecimalForString(dr["ODReduceAmount"].ToString());
            //_entity.ODReduceStartDate = ParseDateTimeForString(dr["ODReduceStartDate"].ToString());
            //_entity.ODReduceEndDate = ParseDateTimeForString(dr["ODReduceEndDate"].ToString());
            //_entity.ODReducedDate = ParseDateTimeForString(dr["ODReducedDate"].ToString());
            //_entity.ODReducePerson = dr["ODReducePerson"].ToString();
            //_entity.ODReduceReason = dr["ODReduceReason"].ToString();
            _entity.ODCANo = dr["ODCANo"].ToString();
            _entity.ODCAName = dr["ODCAName"].ToString();
            _entity.ODCreator = dr["ODCreator"].ToString();
            _entity.ODCreateDate = ParseDateTimeForString(dr["ODCreateDate"].ToString());
            _entity.ODLastReviser = dr["ODLastReviser"].ToString();
            _entity.ODLastRevisedDate = ParseDateTimeForString(dr["ODLastRevisedDate"].ToString());
            _entity.ODTaxRate = ParseDecimalForString(dr["ODTaxRate"].ToString());
            _entity.ODTaxAmount = ParseDecimalForString(dr["ODTaxAmount"].ToString());
            _entity.ReduceAmount = ParseDecimalForString(dr["ReduceAmount"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save()
        {
            string sqlstr = "";
            if (Entity.RowPointer == null)
                sqlstr = "insert into Op_OrderDetail(RowPointer,RefRP,ODSRVTypeNo1,ODSRVTypeNo2,ODSRVNo,ODSRVRemark,ODSRVCalType,ODContractSPNo,ODContractNo,ODContractNoManual," +
                        "ResourceNo,ResourceName,ODFeeStartDate,ODFeeEndDate,BillingDays,ODQTY,ODUnit,ODUnitPrice,ODARAmount,ODPaidAmount," +
                        "ODCANo,ODCreator,ODCreateDate,ODLastReviser,ODLastRevisedDate,ODTaxRate,ODTaxAmount,ReduceAmount)" +
                    "values(NEWID()," + "'" + Entity.RefRP + "'" + "," +
                    "'" + Entity.ODSRVTypeNo1 + "'" + "," + "'" + Entity.ODSRVTypeNo2 + "'" + "," +
                    "'" + Entity.ODSRVNo + "'" + "," + "'" + Entity.ODSRVRemark + "'" + "," +
                    "'" + Entity.ODSRVCalType + "'" + "," + "'" + Entity.ODContractSPNo + "'" + "," +
                    "'" + Entity.ODContractNo + "'" + "," + "'" + Entity.ODContractNoManual + "'" + "," +

                    "'" + Entity.ResourceNo + "'" + "," + "'" + Entity.ResourceName + "'" + "," +
                    "'" + Entity.ODFeeStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "'" + Entity.ODFeeEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + Entity.BillingDays + "," +
                    Entity.ODQTY + "," + "'" + Entity.ODUnit + "'" + "," + Entity.ODUnitPrice + "," + Entity.ODARAmount + "," + Entity.ODPaidAmount + "," + 
                    
                    //Entity.ODPaidinAmount + "," +
                    //"'" + Entity.ODPaidDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + "'" + Entity.ODFeeReceiver + "'" + "," +
                    //"'" + Entity.ODFeeReceiveRemark + "'" + "," + Entity.ODReduceAmount + "," +
                    //"'" + Entity.ODReduceStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    //"'" + Entity.ODReduceEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    //"'" + Entity.ODReducedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + "'" + Entity.ODReducePerson + "'" + "," +
                    //"'" + Entity.ODReduceReason + "'" + "," + 
                    
                    "'" + Entity.ODCANo + "'" + "," +
                    "'" + Entity.ODCreator + "'" + "," + "'" + Entity.ODCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "'" + Entity.ODLastReviser + "'" + "," + "'" + Entity.ODLastRevisedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    Entity.ODTaxRate + "," + Entity.ODTaxAmount + "," + Entity.ReduceAmount + 
                    ")";
            else
                sqlstr = "update Op_OrderDetail" +
                    " set ODSRVTypeNo1=" + "'" + Entity.ODSRVTypeNo1 + "'" + "," + "ODSRVTypeNo2=" + "'" + Entity.ODSRVTypeNo2 + "'" + "," +
                    "ODSRVNo=" + "'" + Entity.ODSRVNo + "'" + "," + "ODSRVRemark=" + "'" + Entity.ODSRVRemark + "'" + "," +
                    "ODSRVCalType=" + "'" + Entity.ODSRVCalType + "'" + "," + "ODContractSPNo=" + "'" + Entity.ODContractSPNo + "'" + "," +
                    "ODContractNo=" + "'" + Entity.ODContractNo + "'" + "," + "ODContractNoManual=" + "'" + Entity.ODContractNoManual + "'" + "," +

                    "ResourceNo=" + "'" + Entity.ResourceNo + "'" + "," + "ResourceName=" + "'" + Entity.ResourceName + "'" + "," +
                    "ODFeeStartDate=" + "'" + Entity.ODFeeStartDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," +
                    "ODFeeEndDate=" + "'" + Entity.ODFeeEndDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," +
                    "BillingDays=" + Entity.BillingDays + "," + "ODQTY=" + Entity.ODQTY + "," +
                    "ODUnit=" + "'" + Entity.ODUnit + "'" + "," + "ODUnitPrice=" + Entity.ODUnitPrice + "," +
                    "ODARAmount=" + Entity.ODARAmount + "," + "ODPaidAmount=" + Entity.ODPaidAmount + "," + 
                    
                    //"ODPaidinAmount=" + Entity.ODPaidinAmount + "," + "ODReduceAmount=" + Entity.ODReduceAmount + "," +
                    //"ODPaidDate=" + "'" + Entity.ODPaidDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," +
                    //"ODFeeReceiver=" + "'" + Entity.ODFeeReceiver + "'" + "," +
                    //"ODReduceStartDate=" + "'" + Entity.ODReduceStartDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," +
                    //"ODReduceEndDate=" + "'" + Entity.ODReduceEndDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," +
                    //"ODReducedDate=" + "'" + Entity.ODReducedDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," +
                    //"ODReducePerson=" + "'" + Entity.ODReducePerson + "'" + "," + "ODReduceReason=" + "'" + Entity.ODReduceReason + "'" + "," +

                    "ODCANo=" + "'" + Entity.ODCANo + "'" + "," + "ODLastReviser=" + "'" + Entity.ODLastReviser + "'" + "," +
                    "ODLastRevisedDate=" + "'" + Entity.ODLastRevisedDate.ToString("yyy-MM-dd HH:mm:ss") + "'" + "," +
                    "ODTaxRate=" + Entity.ODTaxRate + "," + "ODTaxAmount=" + Entity.ODTaxAmount + "," + "ReduceAmount=" + Entity.ReduceAmount + 
                    " where RowPointer='" + Entity.RowPointer + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public string delete()
        {
            //return objdata.ExecuteNonQuery("delete from Op_OrderDetail where RowPointer='" + Entity.RowPointer + "'");

            string InfoMsg = "";
            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = Data.Conn();
                command = new SqlCommand("DeleteOrderDetail", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DetailID", SqlDbType.NVarChar, 36).Value = Entity.RowPointer;
                command.Parameters.Add("@InfoMsg", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                con.Open();
                command.ExecuteNonQuery();
                InfoMsg = command.Parameters["@InfoMsg"].Value.ToString();

            }
            catch (Exception ex)
            {
                InfoMsg = ex.ToString();
            }
            finally
            {
                if (command != null)
                    command.Dispose();
                if (con != null)
                    con.Dispose();
            }
            return InfoMsg;
        }
        
        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="RefRP">外键</param>
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
        /// <param name="RefRP">外键</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string RefRP)
        {
            return GetListHelper(RefRP, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="RefRP">外键</param>
        /// <returns></returns>
        public int GetListCount(string RefRP)
        {
            string wherestr = "";
            if (RefRP != string.Empty)
            {
                wherestr = wherestr + " and a.RefRP = '" + RefRP + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Op_OrderDetail a  where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="RefRP">外键</param>
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
                entitys = Query(objdata.ExecSelect("Op_OrderDetail a left join Mstr_Service b on a.ODSRVNo=b.SRVNo "+
                    "left join Mstr_ServiceProvider c on a.ODContractSPNo=c.SPNo left join Mstr_ChargeAccount d on a.ODCANo=d.CANo and d.CASPNo=b.SRVSPNo",
                    "a.*,b.SRVName as ODSRVName,c.SPShortName as ODContractSPName,d.CAName as ODCAName", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Op_OrderDetail a left join Mstr_Service b on a.ODSRVNo=b.SRVNo " +
                    "left join Mstr_ServiceProvider c on a.ODContractSPNo=c.SPNo left join Mstr_ChargeAccount d on a.ODCANo=d.CANo and d.CASPNo=b.SRVSPNo",
                    "a.*,b.SRVName as ODSRVName,c.SPShortName as ODContractSPName,d.CAName as ODCAName", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Op.EntityOrderDetail entity = new project.Entity.Op.EntityOrderDetail();
                entity.RowPointer = dr["RowPointer"].ToString();
                entity.RefRP = dr["RefRP"].ToString();
                entity.ODSRVTypeNo1 = dr["ODSRVTypeNo1"].ToString();
                entity.ODSRVTypeNo2 = dr["ODSRVTypeNo2"].ToString();
                entity.ODSRVNo = dr["ODSRVNo"].ToString();
                entity.ODSRVName = dr["ODSRVName"].ToString();
                entity.ODSRVRemark = dr["ODSRVRemark"].ToString();
                entity.ODSRVCalType = dr["ODSRVCalType"].ToString();
                entity.ODContractSPNo = dr["ODContractSPNo"].ToString();
                entity.ODContractSPName = dr["ODContractSPName"].ToString();
                entity.ODContractNo = dr["ODContractNo"].ToString();
                entity.ODContractNoManual = dr["ODContractNoManual"].ToString();
                entity.ResourceNo = dr["ResourceNo"].ToString();
                entity.ResourceName = dr["ResourceName"].ToString();
                entity.ODFeeStartDate = ParseDateTimeForString(dr["ODFeeStartDate"].ToString());
                entity.ODFeeEndDate = ParseDateTimeForString(dr["ODFeeEndDate"].ToString());
                entity.BillingDays = ParseIntForString(dr["BillingDays"].ToString());
                entity.ODQTY = ParseDecimalForString(dr["ODQTY"].ToString());
                entity.ODUnit = dr["ODUnit"].ToString();
                entity.ODUnitPrice = ParseDecimalForString(dr["ODUnitPrice"].ToString());
                entity.ODARAmount = ParseDecimalForString(dr["ODARAmount"].ToString());
                entity.ODPaidAmount = ParseDecimalForString(dr["ODPaidAmount"].ToString());

                //entity.ODPaidinAmount = ParseDecimalForString(dr["ODPaidinAmount"].ToString());
                //entity.ODPaidDate = ParseDateTimeForString(dr["ODPaidDate"].ToString());
                //entity.ODFeeReceiver = dr["ODFeeReceiver"].ToString();
                //entity.ODFeeReceiveRemark = dr["ODFeeReceiveRemark"].ToString();
                //entity.ODReduceAmount = ParseDecimalForString(dr["ODReduceAmount"].ToString());
                //entity.ODReduceStartDate = ParseDateTimeForString(dr["ODReduceStartDate"].ToString());
                //entity.ODReduceEndDate = ParseDateTimeForString(dr["ODReduceEndDate"].ToString());
                //entity.ODReducedDate = ParseDateTimeForString(dr["ODReducedDate"].ToString());
                //entity.ODReducePerson = dr["ODReducePerson"].ToString();
                //entity.ODReduceReason = dr["ODReduceReason"].ToString();
                entity.ODCANo = dr["ODCANo"].ToString();
                entity.ODCAName = dr["ODCAName"].ToString();
                entity.ODCreator = dr["ODCreator"].ToString();
                entity.ODCreateDate = ParseDateTimeForString(dr["ODCreateDate"].ToString());
                entity.ODLastReviser = dr["ODLastReviser"].ToString();
                entity.ODLastRevisedDate = ParseDateTimeForString(dr["ODLastRevisedDate"].ToString());
                entity.ODTaxRate = ParseDecimalForString(dr["ODTaxRate"].ToString());
                entity.ODTaxAmount = ParseDecimalForString(dr["ODTaxAmount"].ToString());
                entity.ReduceAmount = ParseDecimalForString(dr["ReduceAmount"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
