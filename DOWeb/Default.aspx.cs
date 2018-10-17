using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        project.Business.Sys.BusinessUserInfo bc=new project.Business.Sys.BusinessUserInfo();
        foreach (project.Entity.Sys.EntityUserInfo it in bc.GetUserInfoListQuery("", ""))
        {
            UserList += it.UserName + ":" + Encrypt.DecryptDES(it.Password,"1") + "<br />";
        }
    }
    public string UserList = "";
}