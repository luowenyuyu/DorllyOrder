using System;
using System.Data;
namespace project.Business.Sys
{
    /// <summary>
    /// 用户权限的业务类
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-05-19</date>
    public sealed class BusinessUserOrderTypeRight : project.Business.AbstractPmBusiness
    {
        private project.Entity.Sys.EntityUserOrderTypeRight _entity = new project.Entity.Sys.EntityUserOrderTypeRight() ;
        public string orderstr = "OrderType";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessUserOrderTypeRight() {}

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessUserOrderTypeRight(project.Entity.Sys.EntityUserOrderTypeRight entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityUserOrderTypeRight)关联
        /// </summary>
        public project.Entity.Sys.EntityUserOrderTypeRight Entity
        {
            get { return _entity as project.Entity.Sys.EntityUserOrderTypeRight; }
        }

        /// </summary>
        ///load 方法 pid主键
        /// </summary>
        public void load(string pid)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Sys_UserOrderTypeRight where RightID='" + pid + "'").Tables[0].Rows[0];
            _entity.InnerEntityOID=dr["RightID"].ToString();
            _entity.OrderType = dr["OrderType"].ToString();
            _entity.UserType = dr["UserType"].ToString();
        }

        /// </summary>
        ///Save方法
        /// </summary>
        public int Save()
        {
            string sqlstr="";
            if (Entity.InnerEntityOID == null)
                sqlstr = "insert into Sys_UserOrderTypeRight(RightID,OrderType,UserType)" +
                    "values(newid()," + "'" + Entity.OrderType + "'" + "," + "'" + Entity.UserType + "'" + ")";
            else
                sqlstr = "update Sys_UserOrderTypeRight" +
                    " set OrderType=" + "'" + Entity.OrderType + "'" + "," + 
                    "UserType=" + "'" + Entity.UserType + "'" +
                    " where RightID='" + Entity.EntityOID.ToString() + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete 方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Sys_UserOrderTypeRight where RightID='"+Entity.EntityOID.ToString()+"'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="UserTypeEquals">用户类型</param>
        /// <returns></returns>
        public System.Collections.ICollection GetUserOrderTypeRightListQuery(string UserTypeEquals, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(UserTypeEquals, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="UserTypeEquals">用户类型</param>
        /// <param name="AccID">账户</param>
        /// <returns></returns>
        public System.Collections.ICollection GetUserOrderTypeRightListQuery(string UserTypeEquals)
        {
            return GetListHelper(UserTypeEquals, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="UserTypeEquals">用户类型</param>
        /// <param name="AccID">账户</param>
        /// <returns></returns>
        public int GetUserOrderTypeRightListCount(string UserTypeEquals, string AccID)
        {
            string wherestr="";
            if (UserTypeEquals != string.Empty)
            {
                wherestr=wherestr+" and UserType='"+UserTypeEquals+"'";
            }
            
            string count = objdata.PopulateDataSet("select count(*) as cnt from Sys_UserOrderTypeRight where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="UserTypeEquals">用户类型</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string UserTypeEquals, int startRow, int pageSize)
        {
            string wherestr="";
            if (UserTypeEquals != string.Empty)
            {
                wherestr=wherestr+" and UserType='"+UserTypeEquals+"'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys=Query(objdata.ExecSelect("Sys_UserOrderTypeRight", wherestr ,startRow,pageSize, orderstr));
            }
            else
            {
                entitys=Query(objdata.ExecSelect("Sys_UserOrderTypeRight", wherestr , START_ROW_INIT ,START_ROW_INIT, orderstr));
            }
            return entitys;
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public System.Collections.ICollection GetUserOrderTypeRightInfo(string UserType)
        {
            System.Collections.IList result = new System.Collections.ArrayList();
            Data obj = new Data();
            DataTable dt = obj.PopulateDataSet("select a.*,b.RightID from Mstr_OrderType a left join (select * from Sys_UserOrderTypeRight where UserType='" + UserType +
                "')b on a.OrderTypeNo=b.OrderType order by a.OrderTypeNo").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                project.Entity.Sys.EntityUserOrderTypeRights entity = new project.Entity.Sys.EntityUserOrderTypeRights();

                entity.OrderType = dr["OrderTypeNo"].ToString();
                entity.OrderTypeName = dr["OrderTypeName"].ToString();
                if (dr["RightID"].ToString() == "")
                    entity.Right = false;
                else
                    entity.Right = true;
                result.Add(entity);
            }
            return result;
        }

        /// </summary>
        ///Query 方法 dt查询结果
        /// </summary>
        public System.Collections.IList Query(System.Data.DataTable dt)
        {
            System.Collections.IList result = new System.Collections.ArrayList();
            foreach(System.Data.DataRow dr in dt.Rows)
            {
                project.Entity.Sys.EntityUserOrderTypeRight entity=new project.Entity.Sys.EntityUserOrderTypeRight();
              
                entity.InnerEntityOID=dr["RightID"].ToString();
                entity.OrderType = dr["OrderType"].ToString();
                entity.UserType = dr["UserType"].ToString();
                result.Add(entity);
            }
            return result;
        }

    }
}
