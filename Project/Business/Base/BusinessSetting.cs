using System;
using System.Data;
namespace project.Business.Base
{
    /// <summary>
    /// 园区/建设期/楼栋/楼层资料
    /// </summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    public sealed class BusinessSetting : project.Business.AbstractPmBusiness
    {
        private project.Entity.Base.EntitySetting _entity = new project.Entity.Base.EntitySetting();
        public string OrderField = "a.OrderNo";
        Data objdata = new Data();

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public BusinessSetting() { }

        /// <summary>
        /// 带参数的构函数
        /// </summary>
        /// <param name="entity">实体类</param>
        public BusinessSetting(project.Entity.Base.EntitySetting entity)
        {
            this._entity = entity;
        }

        /// <summary>
        /// 与实体类(EntitySetting)关联
        /// </summary>
        public project.Entity.Base.EntitySetting Entity
        {
            get { return _entity as project.Entity.Base.EntitySetting; }
        }

        /// </summary>
        /// load方法
        /// </summary>
        public void load(string id)
        {
            DataRow dr = objdata.PopulateDataSet("select a.*,b.SRVName from Sys_Setting a left join Mstr_Service b on a.SRVNo=b.SRVNo where a.SettingCode='" + id + "'").Tables[0].Rows[0];
            _entity.SettingCode = dr["SettingCode"].ToString();
            _entity.SettingName = dr["SettingName"].ToString();
            _entity.SettingType = dr["SettingType"].ToString();
            _entity.StringValue = dr["StringValue"].ToString();
            _entity.IntValue = ParseIntForString(dr["IntValue"].ToString());
            _entity.DecimalValue = ParseDecimalForString(dr["DecimalValue"].ToString());
            _entity.OrderNo = dr["OrderNo"].ToString();
            _entity.SRVNo = dr["SRVNo"].ToString();
            _entity.SRVName = dr["SRVName"].ToString();
        }

        /// </summary>
        /// Save方法
        /// </summary>
        public int Save(string type)
        {
            string sqlstr = "update Sys_Setting" +
                    " set StringValue=" + "'" + Entity.StringValue + "'" + "," +
                    "IntValue=" + Entity.IntValue + "," +
                    "DecimalValue=" + Entity.DecimalValue +"," +
                    "SRVNo=" + "'" + Entity.SRVNo + "'" +
                    " where SettingCode='" + Entity.SettingCode + "'";
            return objdata.ExecuteNonQuery(sqlstr);
        }

        /// <summary>
        /// 按条件查询，返回符合条件的集合
        /// </summary>
        /// <returns></returns>
        public System.Collections.ICollection GetListQuery()
        {
            System.Collections.IList entitys = null;
            entitys = Query(objdata.ExecSelect("Sys_Setting a left join Mstr_Service b on a.SRVNo=b.SRVNo", "a.*,b.SRVName", "", START_ROW_INIT, START_ROW_INIT, OrderField));
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
                project.Entity.Base.EntitySetting entity = new project.Entity.Base.EntitySetting();
                entity.SettingCode = dr["SettingCode"].ToString();
                entity.SettingName = dr["SettingName"].ToString();
                entity.SettingType = dr["SettingType"].ToString();
                entity.StringValue = dr["StringValue"].ToString();
                entity.IntValue = ParseIntForString(dr["IntValue"].ToString());
                entity.DecimalValue = ParseDecimalForString(dr["DecimalValue"].ToString());
                entity.OrderNo = dr["OrderNo"].ToString();
                entity.SRVNo = dr["SRVNo"].ToString();
                entity.SRVName = dr["SRVName"].ToString();
                result.Add(entity);
            }
            return result;
        }
    }
}
