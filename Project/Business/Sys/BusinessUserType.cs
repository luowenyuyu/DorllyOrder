using System;
using System.Data;
namespace project.Business.Sys
{
    /// <summary>
    /// 用户类型表的业务类
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-05-19</date>
    public sealed class BusinessUserType : project.Business.AbstractPmBusiness
    {
        private project.Entity.Sys.EntityUserType _entity = new project.Entity.Sys.EntityUserType();
        public string OrderField = "UserTypeNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessUserType() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessUserType(project.Entity.Sys.EntityUserType entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityUserType)关联
        /// </summary>
        public project.Entity.Sys.EntityUserType Entity
        {
            get { return _entity as project.Entity.Sys.EntityUserType; }
        }

        /// </summary>
        ///load 方法 pid主键
        /// </summary>
        public void load(string UserTypeNo)
        {
            DataRow dr = objdata.PopulateDataSet("select * from Sys_UserType where UserTypeNo='" + UserTypeNo + "'").Tables[0].Rows[0];
            _entity.UserTypeNo = dr["UserTypeNo"].ToString();
            _entity.UserTypeName = dr["UserTypeName"].ToString();
        }

        /// </summary>
        ///Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Sys_UserType(UserTypeNo,UserTypeName)" +
                    "values('" + Entity.UserTypeNo + "'," + "'" + Entity.UserTypeName + "'" + ")";
            else
                sqlstr = "update Sys_UserType" +
                    " set UserTypeName=" + "'" + Entity.UserTypeName + "'" + 
                    " where UserTypeNo='" + Entity.UserTypeNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete 方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Sys_UserType where UserTypeNo='" + Entity.UserTypeNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="UserTypeNoEquals">用户类型编号</param>
        /// <param name="UserTypeNameEquals">用户类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetUserTypeListQuery(string UserTypeNo, string UserTypeName, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(UserTypeNo, UserTypeName, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="UserTypeNoEquals">用户类型编号</param>
        /// <param name="UserTypeNameEquals">用户类型名称</param>
        /// <returns></returns>
        public System.Collections.ICollection GetUserTypeListQuery(string UserTypeNo, string UserTypeName)
        {
            return GetListHelper(UserTypeNo, UserTypeName, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="UserTypeNoEquals">用户类型编号</param>
        /// <param name="UserTypeNameEquals">用户类型名称</param>
        /// <returns></returns>
        public int GetUserTypeListCount(string UserTypeNo, string UserTypeName)
        {
            string wherestr = "";
            if (UserTypeNo != string.Empty)
            {
                wherestr = wherestr + " and UserTypeNo like '%" + UserTypeNo + "%'";
            }
            if (UserTypeName != string.Empty)
            {
                wherestr = wherestr + " and UserTypeName like '%" + UserTypeName + "%'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Sys_UserType where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="UserTypeNoEquals">用户类型编号</param>
        /// <param name="UserTypeNameEquals">用户类型名称</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string UserTypeNo, string UserTypeName, int startRow, int pageSize)
        {
            string wherestr = "";
            if (UserTypeNo != string.Empty)
            {
                wherestr = wherestr + " and UserTypeNo like '%" + UserTypeNo + "%'";
            }
            if (UserTypeName != string.Empty)
            {
                wherestr = wherestr + " and UserTypeName like '%" + UserTypeName + "%'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Sys_UserType", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Sys_UserType", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Sys.EntityUserType entity = new project.Entity.Sys.EntityUserType();

                entity.UserTypeNo = dr["UserTypeNo"].ToString();
                entity.UserTypeName = dr["UserTypeName"].ToString();
                result.Add(entity);
            }
            return result;
        }

    }
}
