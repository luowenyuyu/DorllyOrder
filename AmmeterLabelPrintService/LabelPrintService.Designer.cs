namespace AmmeterLabelPrintService
{
    partial class LabelPrintService
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            //this.components = new System.ComponentModel.Container();
            this.PrintTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.PrintTimer)).BeginInit();
            //this.PrintTimer = new System.Windows.Forms.Timer(this.components);
            // 
            // PrintTimer
            // 
            this.PrintTimer.Interval = 2000;
            //this.PrintTimer.Enabled += new System.Timers.ElapsedEventHandler(this.Print_Elapse);
            // 
            // LabelPrintService
            // 
            this.ServiceName = "AmmeterLabelPrintService";
            ((System.ComponentModel.ISupportInitialize)(this.PrintTimer)).EndInit();

        }

        #endregion

        private System.Timers.Timer PrintTimer;
    }
}
