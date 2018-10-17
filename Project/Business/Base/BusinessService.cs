using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 服务商资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessService : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntityService _entity = new project.Entity.Base.EntityService();
        public string OrderField = "b.SPNo,a.SRVNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessService() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessService(project.Entity.Base.EntityService entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntityService)关联
        /// </summary>
        public project.Entity.Base.EntityService Entity
        {
            get { return _entity as project.Entity.Base.EntityService; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SPShortName as SRVSPName,c.SRVTypeName as SRVTypeNo1Name,d.SRVTypeName as SRVTypeNo2Name,e.CAName "+
                "from Mstr_Service a " +
                "left join Mstr_ServiceProvider b on a.SRVSPNo=b.SPNo " +
                "left join Mstr_ServiceType c on a.SRVTypeNo1=c.SRVTypeNo " +
                "left join Mstr_ServiceType d on a.SRVTypeNo2=d.SRVTypeNo " +
                "left join Mstr_ChargeAccount e on a.CANo=e.CANo and a.SRVSPNo=e.CASPNo " +
                "where a.SRVNo='" + id + "'").Tables[0].Rows[0];
            _entity.SRVNo = dr["SRVNo"].ToString();
            _entity.SRVName = dr["SRVName"].ToString();
            _entity.SRVTypeNo1 = dr["SRVTypeNo1"].ToString();
            _entity.SRVTypeNo1Name = dr["SRVTypeNo1Name"].ToString();
            _entity.SRVTypeNo2 = dr["SRVTypeNo2"].ToString();
            _entity.SRVTypeNo2Name = dr["SRVTypeNo2Name"].ToString();
            _entity.SRVSPNo = dr["SRVSPNo"].ToString();
            _entity.CANo = dr["CANo"].ToString();
            _entity.CAName = dr["CAName"].ToString();
            _entity.SRVSPName = dr["SRVSPName"].ToString();
            _entity.SRVCalType = dr["SRVCalType"].ToString();
            _entity.SRVRoundType = dr["SRVRoundType"].ToString();
            _entity.SRVDecimalPoint = ParseIntForString(dr["SRVDecimalPoint"].ToString());
            _entity.SRVRate = ParseDecimalForString(dr["SRVRate"].ToString());
            _entity.SRVTaxRate = ParseDecimalForString(dr["SRVTaxRate"].ToString());
            _entity.SRVStatus = bool.Parse(dr["SRVStatus"].ToString());
            _entity.SRVRemark = dr["SRVRemark"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "";
            if (type == "insert")
                sqlstr = "insert into Mstr_Service(SRVNo,SRVName,SRVTypeNo1,SRVTypeNo2,SRVSPNo,CANo,SRVCalType,SRVRoundType,SRVDecimalPoint,SRVRate,SRVTaxRate,SRVRemark,SRVStatus)" +
                    "values('" + Entity.SRVNo + "'" + "," + "'" + Entity.SRVName + "'" + "," + "'" + Entity.SRVTypeNo1 + "'" + "," +
                    "'" + Entity.SRVTypeNo2 + "'" + "," + "'" + Entity.SRVSPNo + "'" + "," + "'" + Entity.CANo + "'" + "," + 
                    "'" + Entity.SRVCalType + "'" + "," + "'" + Entity.SRVRoundType + "'" + "," + Entity.SRVDecimalPoint + ","+
                    Entity.SRVRate + "," + Entity.SRVTaxRate + "," + "'" + Entity.SRVRemark + "'" + "," + "1)";
            else
                sqlstr = "update Mstr_Service" +
                    " set SRVName=" + "'" + Entity.SRVName + "'" + "," + "SRVTypeNo1=" + "'" + Entity.SRVTypeNo1 + "'" + "," +
                    "SRVTypeNo2=" + "'" + Entity.SRVTypeNo2 + "'" + "," + "SRVSPNo=" + "'" + Entity.SRVSPNo + "'" + "," + "CANo=" + "'" + Entity.CANo + "'" + "," + 
                    "SRVCalType=" + "'" + Entity.SRVCalType + "'" + "," + "SRVRoundType=" + "'" + Entity.SRVRoundType + "'" + "," + 
                    "SRVDecimalPoint=" + Entity.SRVDecimalPoint + "," + "SRVRate=" + Entity.SRVRate + "," + 
                    "SRVTaxRate=" + Entity.SRVTaxRate + "," + "SRVRemark=" + "'" + Entity.SRVRemark + "'" +
                    " where SRVNo='" + Entity.SRVNo + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// </summary>
        ///Delete方法 
        /// </summary>
        public int delete()
        {
            return objdata.ExecuteNonQuery("delete from Mstr_Service where SRVNo='" + Entity.SRVNo + "'");
        }

        /// </summary>
        /// valid方法 
        /// </summary>
        public int valid()
        {
            return objdata.ExecuteNonQuery("update Mstr_Service set SRVStatus=" + (Entity.SRVStatus == true ? "1" : "0") + " where SRVNo='" + Entity.SRVNo + "'");
        }

        /// <summary>
        /// 按条件查询，支持分页
        /// </summary>
        /// <param name="SRVNo">收费项目编号</param>
        /// <param name="SRVName">收费项目名称</param>
        /// <param name="SRVTypeNo1">所属服务大类</param>
        /// <param name="SRVTypeNo2">所属服务小类</param>
        /// <param name="SRVSPNo">服务商</param>
        /// <param name="CANo">收费科目</param>
        /// <param name="SRVCalType">收费方式</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string SRVNo, string SRVName, string SRVTypeNo1, string SRVTypeNo2, string SRVSPNo, string CANo, string SRVCalType, int startRow, int pageSize)
        {
            if (startRow < 0 || pageSize <= 0)
            {
                throw new Exception();
            }

            return GetListHelper(SRVNo, SRVName, SRVTypeNo1, SRVTypeNo2, SRVSPNo, CANo, SRVCalType, startRow, pageSize);
        }

        /// <summary>
        /// 按条件查询，不支持分页
        /// </summary>
        /// <param name="SRVNo">收费项目编号</param>
        /// <param name="SRVName">收费项目名称</param>
        /// <param name="SRVTypeNo1">所属服务大类</param>
        /// <param name="SRVTypeNo2">所属服务小类</param>
        /// <param name="SRVSPNo">服务商</param>
        /// <param name="CANo">收费科目</param>
        /// <param name="SRVCalType">收费方式</param>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery(string SRVNo, string SRVName, string SRVTypeNo1, string SRVTypeNo2, string SRVSPNo, string CANo, string SRVCalType)
        {
            return GetListHelper(SRVNo, SRVName, SRVTypeNo1, SRVTypeNo2, SRVSPNo, CANo, SRVCalType, START_ROW_INIT, START_ROW_INIT);
        }

        /// <summary>
        /// 返回集合的大小
        /// </summary>
        /// <param name="SRVNo">收费项目编号</param>
        /// <param name="SRVName">收费项目名称</param>
        /// <param name="SRVTypeNo1">所属服务大类</param>
        /// <param name="SRVTypeNo2">所属服务小类</param>
        /// <param name="SRVSPNo">服务商</param>
        /// <param name="CANo">收费科目</param>
        /// <param name="SRVCalType">收费方式</param>
        /// <returns></returns>
        public int GetListCount(string SRVNo, string SRVName, string SRVTypeNo1, string SRVTypeNo2, string SRVSPNo, string SRVCalType, string CANo)
        {
            string wherestr = "";
            if (SRVNo != string.Empty)
            {
                wherestr = wherestr + " and SRVNo like '%" + SRVNo + "%'";
            }
            if (SRVName != string.Empty)
            {
                wherestr = wherestr + " and SRVName like '%" + SRVName + "%'";
            }
            if (SRVTypeNo1 != string.Empty)
            {
                wherestr = wherestr + " and SRVTypeNo1 = '" + SRVTypeNo1 + "'";
            }
            if (SRVTypeNo2 != string.Empty)
            {
                wherestr = wherestr + " and SRVTypeNo2 = '" + SRVTypeNo2 + "'";
            }
            if (SRVSPNo != string.Empty)
            {
                wherestr = wherestr + " and SRVSPNo = '" + SRVSPNo + "'";
            }
            if (CANo != string.Empty)
            {
                wherestr = wherestr + " and CANo = '" + CANo + "'";
            }
            if (SRVCalType != string.Empty)
            {
                wherestr = wherestr + " and SRVCalType = '" + SRVCalType + "'";
            }

            string count = objdata.PopulateDataSet("select count(*) as cnt from Mstr_Service where 1=1 " + wherestr).Tables[0].Rows[0]["cnt"].ToString();
            return int.Parse(count);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <param name="SRVNo">收费项目编号</param>
        /// <param name="SRVName">收费项目名称</param>
        /// <param name="SRVTypeNo1">所属服务大类</param>
        /// <param name="SRVTypeNo2">所属服务小类</param>
        /// <param name="SRVSPNo">服务商</param>
        /// <param name="CANo">收费科目</param>
        /// <param name="SRVCalType">收费方式</param>
        /// <returns></returns>
        private System.Collections.ICollection GetListHelper(string SRVNo, string SRVName, string SRVTypeNo1, string SRVTypeNo2, string SRVSPNo, string CANo, string SRVCalType, int startRow, int pageSize)
        {
            string wherestr = "";
            if (SRVNo != string.Empty)
            {
                wherestr = wherestr + " and a.SRVNo like '%" + SRVNo + "%'";
            }
            if (SRVName != string.Empty)
            {
                wherestr = wherestr + " and a.SRVName like '%" + SRVName + "%'";
            }
            if (SRVTypeNo1 != string.Empty)
            {
                wherestr = wherestr + " and a.SRVTypeNo1 = '" + SRVTypeNo1 + "'";
            }
            if (SRVTypeNo2 != string.Empty)
            {
                wherestr = wherestr + " and a.SRVTypeNo2 = '" + SRVTypeNo2 + "'";
            }
            if (SRVSPNo != string.Empty)
            {
                wherestr = wherestr + " and a.SRVSPNo = '" + SRVSPNo + "'";
            }
            if (CANo != string.Empty)
            {
                wherestr = wherestr + " and a.CANo = '" + CANo + "'";
            }
            if (SRVCalType != string.Empty)
            {
                wherestr = wherestr + " and a.SRVCalType = '" + SRVCalType + "'";
            }

            System.Collections.IList entitys = null;
            if (startRow > START_ROW_INIT && pageSize > START_ROW_INIT)
            {
                entitys = Query(objdata.ExecSelect("Mstr_Service a left join Mstr_ServiceProvider b on a.SRVSPNo=b.SPNo left join Mstr_ServiceType c on a.SRVTypeNo1=c.SRVTypeNo "+
                    "left join Mstr_ServiceType d on a.SRVTypeNo2=d.SRVTypeNo left join Mstr_ChargeAccount e on a.CANo=e.CANo and a.SRVSPNo=e.CASPNo ",
                    "a.*,b.SPShortName as SRVSPName,c.SRVTypeName as SRVTypeNo1Name,d.SRVTypeName as SRVTypeNo2Name,e.CAName", wherestr, startRow, pageSize, OrderField));
            }
            else
            {
                entitys = Query(objdata.ExecSelect("Mstr_Service a left join Mstr_ServiceProvider b on a.SRVSPNo=b.SPNo left join Mstr_ServiceType c on a.SRVTypeNo1=c.SRVTypeNo "+
                    "left join Mstr_ServiceType d on a.SRVTypeNo2=d.SRVTypeNo left join Mstr_ChargeAccount e on a.CANo=e.CANo and a.SRVSPNo=e.CASPNo ",
                    "a.*,b.SPShortName as SRVSPName,c.SRVTypeName as SRVTypeNo1Name,d.SRVTypeName as SRVTypeNo2Name,e.CAName", wherestr, START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntityService entity = new project.Entity.Base.EntityService();
                entity.SRVNo = dr["SRVNo"].ToString();
                entity.SRVName = dr["SRVName"].ToString();
                entity.SRVTypeNo1 = dr["SRVTypeNo1"].ToString();
                entity.SRVTypeNo1Name = dr["SRVTypeNo1Name"].ToString();
                entity.SRVTypeNo2 = dr["SRVTypeNo2"].ToString();
                entity.SRVTypeNo2Name = dr["SRVTypeNo2Name"].ToString();
                entity.SRVSPNo = dr["SRVSPNo"].ToString();
                entity.SRVSPName = dr["SRVSPName"].ToString();
                entity.CANo = dr["CANo"].ToString();
                entity.CAName = dr["CAName"].ToString();
                entity.SRVCalType = dr["SRVCalType"].ToString();
                entity.SRVRoundType = dr["SRVRoundType"].ToString();
                entity.SRVDecimalPoint = ParseIntForString(dr["SRVDecimalPoint"].ToString());
                entity.SRVRate = ParseDecimalForString(dr["SRVRate"].ToString());
                entity.SRVTaxRate = ParseDecimalForString(dr["SRVTaxRate"].ToString());
                entity.SRVStatus = bool.Parse(dr["SRVStatus"].ToString());
                entity.SRVRemark = dr["SRVRemark"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
