namespace ParkingOrder
{
    partial class ParkingInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ParkingServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ParkingServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ParkingServiceProcessInstaller
            // 
            this.ParkingServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ParkingServiceProcessInstaller.Password = null;
            this.ParkingServiceProcessInstaller.Username = null;
            // 
            // ParkingServiceInstaller
            // 
            this.ParkingServiceInstaller.Description = "对接一道通平台,同步其数据生成订单";
            this.ParkingServiceInstaller.DisplayName = "AParking";
            this.ParkingServiceInstaller.ServiceName = "ParkingOrder";
            // 
            // ParkingInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ParkingServiceProcessInstaller,
            this.ParkingServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ParkingServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ParkingServiceInstaller;
    }
}