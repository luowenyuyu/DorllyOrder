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
    public sealed class BusinessOrderHeader : project.Business.AbstractPmBusiness
    {
        private project.Entity.Op.EntityOrderHeader _entity = new project.Entity.Op.EntityOrderHeader();
        public string OrderField = "OrderCreateDate desc";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessOrderHeader() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessOrderHeader(project.Entity.Op.EntityOrderHeader entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityOrderHeader)关联
        /// </summary>
        public project.Entity.Op.EntityOrderHeader Entity
        {
            get { return _entity as project.Entity.Op.EntityOrderHeader; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.CustName,b.CustShortName,c.OrderTypeName from Op_OrderHeader a left join Mstr_Customer b on a.CustNo=b.CustNo " +
                "left join Mstr_OrderType c on c.OrderTypeNo=a.OrderType " +
                "where a.RowPointer='" + id + "'").Tables[0].Rows[0];
            _entity.RowPointer = dr["RowPointer"].ToString();
            _entity.OrderNo = dr["OrderNo"].ToString();
            _entity.OrderType = dr["OrderType"].ToString();
            _entity.OrderTypeName = dr["OrderTypeName"].ToString();
            _entity.CustNo = dr["CustNo"].ToString();
            _entity.CustName = dr["CustName"].ToString();
            _entity.CustShortName = dr["CustShortName"].ToString();
            _entity.OrderTime = ParseDateTimeForString(dr["OrderTime"].ToString());
            _entity.DaysofMonth = ParseIntForString(dr["DaysofMonth"].ToString());
            _entity.ARDate = ParseDateTimeForString(dr["ARDate"].ToString());
            _entity.ARAmount = ParseDecimalForString(dr["ARAmount"].ToString());
            _entity.ReduceAmount = ParseDecimalForString(dr["ReduceAmount"].ToString());
            _entity.PaidinAmount = ParseDecimalForString(dr["PaidinAmount"].ToString());
            _entity.OrderAuditor = dr["OrderAuditor"].ToString();
            _entity.OrderAuditDate = ParseDateTimeForString(dr["OrderAuditDate"].ToString());
            _entity.OrderAuditReason = dr["OrderAuditReason"].ToString();
            _entity.OrderRAuditor = dr["OrderRAuditor"].ToString();
            _entity.OrderRAuditDate = ParseDateTimeForString(dr["OrderRAuditDate"].ToString());
            _entity.OrderRAuditReason = dr["OrderRAuditReason"].ToString();
            _entity.Remark = dr["Remark"].ToString();
            _entity.OrderStatus = dr["OrderStatus"].ToString();
            _entity.OrderCreator = dr["OrderCreator"].ToString();
            _entity.OrderCreateDate = ParseDateTimeForString(dr["OrderCreateDate"].ToString());
            _entity.OrderLastReviser = dr["OrderLastReviser"].ToString();
            _entity.OrderLastRevisedDate = ParseDateTimeForString(dr["OrderLastRevisedDate"].ToString());
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Op_OrderHeader(RowPointer,OrderNo,OrderType,CustNo,OrderTime,DaysofMonth,ARDate,ARAmount,ReduceAmount," +
                        "PaidinAmount,OrderAuditor,OrderAuditDate,OrderAuditReason,OrderRAuditor,OrderRAuditDate,OrderRAuditReason,"+
                        "Remark,OrderStatus,OrderCreator,OrderCreateDate,OrderLastReviser,OrderLastRevisedDate)" +
                    "values('"+Entity.RowPointer+"'," + "'" + Entity.OrderNo + "'" + "," + "'" + Entity.OrderType + "'" + "," + "'" + Entity.CustNo + "'" + "," +
                    "'" + Entity.OrderTime.ToString("yyyy-MM-dd") + "'" + "," + Entity.DaysofMonth + "," +
                    "'" + Entity.ARDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + Entity.ARAmount + "," + Entity.ReduceAmount + "," +

                    Entity.PaidinAmount + ",'',null,'','',null,'','" + Entity.Remark + "','0',"+
                    "'" + Entity.OrderCreator + "'" + "," + "'" + Entity.OrderCreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "'" + Entity.OrderLastReviser + "'" + "," + "'" + Entity.OrderLastRevisedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ")";
            else
                sqlstr = "update Op_OrderHeader" +
                    " set OrderType=" + "'" + Entity.OrderType + "'" + "," + "CustNo=" + "'" + Entity.CustNo + "'" + "," +
                    "OrderTime=" + "'" + Entity.OrderTime.ToString("yyyy-MM-dd") + "'" + "," +
                    "DaysofMonth=" + Entity.DaysofMonth + "," + "ARDate=" + "'" + Entity.ARDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                    "ARAmount=" + Entity.ARAmount + "," + "ReduceAmount=" + Entity.ReduceAmount + "," + "PaidinAmount=" + Entity.PaidinAmount + "," +
                    "Remark=" + "'" + Entity.Remark + "'" +  "," + "OrderLastReviser=" + "'" + Entity.OrderLastReviser + "'" +"," +
                    "OrderLastRevisedDate=" + "'" + Entity.OrderLastRevisedDate.ToString("yyy-MM-dd HH:mm:ss") + "'" +
                    " where RowPointer='" + Entity.RowPointer + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public string delete()
        {
            string InfoMsg = "";
            SqlConnection con = null;
            SqlCommand command = null;
            try
            {
                con = Data.Conn();
                command = new SqlCommand("DeleteOrder", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@OrderID", SqlDbType.NVarChar, 36).Value = Entity.RowPointer;
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

        /// </summary>
        /// Approve方法 
        /// </summary>
        public int approve()
        {
            string sqlstr = "update Op_OrderHeader" +
                " set OrderAuditor=" + "'" + Entity.OrderAuditor + "'" + "," +
                "OrderAuditDate=" + "'" + Entity.OrderAuditDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                "OrderAuditReason=" + "'" + Entity.OrderAuditReason + "'" + "," +
                "OrderRAuditor=" + "'" + Entity.OrderRAuditor + "'" + "," +
                "OrderRAuditDate=" + "'" + Entity.OrderRAuditDate.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," +
                "OrderRAuditReason=" + "'" + Entity.OrderRAuditReason + "'" + "," +
                "OrderStatus=" + "'" + Entity.OrderStatus + "'" + "," + 
                "OrderLastReviser=" + "'" + Entity.OrderLastReviser + "'" + "," +
                "OrderLastRevisedDate=" + "'" + Entity.OrderLastRevisedDate.ToString("yyy-MM-dd HH:mm:ss") + "'" +
                " where RowPointer='" + Entity.RowPointer + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="OrderType">订单类型</param>
        /// <param name="CustNo">客户编号</param>
        /// <param name="OrderTime">所属年月</param>
        /// <param name="OrderCreateDate">创建日期</param>
        /// <param name="OrderStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string OrderNo, string OrderType, string OrderTypes, string CustNo, DateTime OrderTime, DateTime MinOrderCreateDate, DateTime MaxOrderCreateDate, string OrderStatus, int startRow, int pageSize, string serviceProvider)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(OrderNo, OrderType, OrderTypes, CustNo, OrderTime, MinOrderCreateDate, MaxOrderCreateDate, OrderStatus, startRow, pageSize, serviceProvider);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="OrderType">订单类型</param>
        /// <param name="CustNo">客户编号</param>
        /// <param name="OrderTime">所属年月</param>
        /// <param name="OrderCreateDate">创建日期</param>
        /// <param name="OrderStatus">状态</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string OrderNo, string OrderType, string OrderTypes, string CustNo, DateTime OrderTime, DateTime MinOrderCreateDate, DateTime MaxOrderCreateDate, string OrderStatus, string serviceProvider)
        {
            return GetListHelper(OrderNo, OrderType, OrderTypes, CustNo, OrderTime, MinOrderCreateDate, MaxOrderCreateDate, OrderStatus, START_ROW_INIT, START_ROW_INIT, serviceProvider);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="OrderType">订单类型</param>
        /// <param name="CustNo">客户编号</param>
        /// <param name="OrderTime">所属年月</param>
        /// <param name="OrderCreateDate">创建日期</param>
        /// <param name="OrderStatus">状态</param>
        /// <returns></returns>
        public int GetListCount(string OrderNo, string OrderType, string OrderTypes, string CustNo, DateTime OrderTime, DateTime MinOrderCreateDate, DateTime MaxOrderCreateDate, string OrderStatus, string serviceProvider)
        {
            string wherestr = "";
            if (OrderNo != string.Empty)
            {
                wherestr = wherestr + " and a.OrderNo like '%" + OrderNo + "%'";
            }
            if (OrderType != string.Empty)
            {
                wherestr = wherestr + " and a.OrderType = '" + OrderType + "'";
            }
            if (OrderTypes != string.Empty)
            {
                wherestr = wherestr + " and a.OrderType in (" + OrderTypes + ")";
            }
            if (CustNo != string.Empty)
            {
                wherestr = wherestr + " and b.CustName like '%" + CustNo + "%'";
            }
            if (OrderTime != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(7),a.OrderTime,121) = '" + OrderTime.ToString("yyyy-MM") + "'";
            }
            if (MinOrderCreateDate != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.ARDate,121) >= '" + MinOrderCreateDate.ToString("yyyy-MM-dd") + "'";
            }
            if (MaxOrderCreateDate != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.ARDate,121) <= '" + MaxOrderCreateDate.ToString("yyyy-MM-dd") + "'";
            }
            if (OrderStatus != string.Empty)
            {
                if (OrderStatus == "ALL")
                    wherestr = wherestr + " and a.OrderStatus IN ('1','2','3')";
                else
                    wherestr = wherestr + " and a.OrderStatus = '" + OrderStatus + "'";
            }
            if (serviceProvider != "")
            {
                wherestr += string.Format("and RowPointer in (select RefRP from Op_OrderDetail where ODContractSPNo='{0}' group by RefRP)", serviceProvider);
            }

            string count = objdata.PopulateDataSet("select count(1) as cnt from Op_OrderHeader a left join Mstr_Customer b on a.CustNo=b.CustNo " +
                    "left join (SELECT MIN(ResourceNo) AS ResourceNo,RefRP FROM Op_OrderDetail GROUP BY RefRP) d on d.RefRP=a.RowPointer "+
                    "where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="OrderNo">订单号</param>
        /// <param name="OrderType">订单类型</param>
        /// <param name="CustNo">客户编号</param>
        /// <param name="OrderTime">所属年月</param>
        /// <param name="OrderCreateDate">创建日期</param>
        /// <param name="OrderStatus">状态</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string OrderNo, string OrderType, string OrderTypes, string CustNo, DateTime OrderTime, DateTime MinOrderCreateDate, DateTime MaxOrderCreateDate, string OrderStatus, int startRow, int pageSize, string serviceProvider)
        {
            string wherestr = "";
            if (OrderNo != string.Empty)
            {
                wherestr = wherestr + " and a.OrderNo like '%" + OrderNo + "%'";
            }
            if (OrderType != string.Empty)
            {
                wherestr = wherestr + " and a.OrderType = '" + OrderType + "'";
            }
            if (OrderTypes != string.Empty)
            {
                wherestr = wherestr + " and a.OrderType in (" + OrderTypes + ")";
            }
            if (CustNo != string.Empty)
            {
                wherestr = wherestr + " and b.CustName like '%" + CustNo + "%'";
            }
            if (OrderTime != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(7),a.OrderTime,121) = '" + OrderTime.ToString("yyyy-MM") + "'";
            }
            if (MinOrderCreateDate != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.ARDate,121) >= '" + MinOrderCreateDate.ToString("yyyy-MM-dd") + "'";
            }
            if (MaxOrderCreateDate != default(DateTime))
            {
                wherestr = wherestr + " and convert(nvarchar(10),a.ARDate,121) <= '" + MaxOrderCreateDate.ToString("yyyy-MM-dd") + "'";
            }
            if (OrderStatus != string.Empty)
            {
                if (OrderStatus == "ALL")
                    wherestr = wherestr + " and a.OrderStatus IN ('1','2','3')";
                else
                    wherestr = wherestr + " and a.OrderStatus = '" + OrderStatus + "'";
            }
            if (serviceProvider != "")
            {
                wherestr += string.Format("and RowPointer in (select RefRP from Op_OrderDetail where ODContractSPNo='{0}' group by RefRP)", serviceProvider);
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Op_OrderHeader a left join Mstr_Customer b on a.CustNo=b.CustNo "+
                    "left join Mstr_OrderType c on c.OrderTypeNo=a.OrderType " +
                    "left join (SELECT MIN(ResourceNo) AS ResourceNo,RefRP FROM Op_OrderDetail GROUP BY RefRP) d on d.RefRP=a.RowPointer",
                    "a.*,b.CustName,b.CustShortName,c.OrderTypeName", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Op_OrderHeader a left join Mstr_Customer b on a.CustNo=b.CustNo "+
                    "left join Mstr_OrderType c on c.OrderTypeNo=a.OrderType "+
                    "left join (SELECT MIN(ResourceNo) AS ResourceNo,RefRP FROM Op_OrderDetail GROUP BY RefRP) d on d.RefRP=a.RowPointer",
                    "a.*,b.CustName,b.CustShortName,c.OrderTypeName", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Op.EntityOrderHeader entity = new project.Entity.Op.EntityOrderHeader();
                entity.RowPointer = dr["RowPointer"].ToString();
                entity.OrderNo = dr["OrderNo"].ToString();
                entity.OrderType = dr["OrderType"].ToString();
                entity.OrderTypeName = dr["OrderTypeName"].ToString();
                entity.CustNo = dr["CustNo"].ToString();
                entity.CustName = dr["CustName"].ToString();
                entity.CustShortName = dr["CustShortName"].ToString();
                entity.OrderTime = ParseDateTimeForString(dr["OrderTime"].ToString());
                entity.DaysofMonth = ParseIntForString(dr["DaysofMonth"].ToString());
                entity.ARDate = ParseDateTimeForString(dr["ARDate"].ToString());
                entity.ARAmount = ParseDecimalForString(dr["ARAmount"].ToString());
                entity.ReduceAmount = ParseDecimalForString(dr["ReduceAmount"].ToString());
                entity.PaidinAmount = ParseDecimalForString(dr["PaidinAmount"].ToString());
                entity.OrderAuditor = dr["OrderAuditor"].ToString();
                entity.OrderAuditDate = ParseDateTimeForString(dr["OrderAuditDate"].ToString());
                entity.OrderAuditReason = dr["OrderAuditReason"].ToString();
                entity.OrderRAuditor = dr["OrderRAuditor"].ToString();
                entity.OrderRAuditDate = ParseDateTimeForString(dr["OrderRAuditDate"].ToString());
                entity.OrderRAuditReason = dr["OrderRAuditReason"].ToString();
                entity.Remark = dr["Remark"].ToString();
                entity.OrderStatus = dr["OrderStatus"].ToString();
                entity.OrderCreator = dr["OrderCreator"].ToString();
                entity.OrderCreateDate = ParseDateTimeForString(dr["OrderCreateDate"].ToString());
                entity.OrderLastReviser = dr["OrderLastReviser"].ToString();
                entity.OrderLastRevisedDate = ParseDateTimeForString(dr["OrderLastRevisedDate"].ToString());
                result.Add(entity);
            }
            return result;
        }
    }
}
