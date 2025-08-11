namespace CalDavSynchronizer
{
    partial class HanbiroCaldavSynchronizerRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public HanbiroCaldavSynchronizerRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btLogin = this.Factory.CreateRibbonButton();
            this.btConfig = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "CalDav Synchronizer";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btLogin);
            this.group1.Items.Add(this.btConfig);
            this.group1.Label = "CalDav Synchronizer";
            this.group1.Name = "group1";
            // 
            // btLogin
            // 
            this.btLogin.Label = "Login";
            this.btLogin.Name = "btLogin";
            // 
            // btConfig
            // 
            this.btConfig.Label = "Config";
            this.btConfig.Name = "btConfig";
            // 
            // HanbiroCaldavSynchronizerRibbon
            // 
            this.Name = "HanbiroCaldavSynchronizerRibbon";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.HanbiroCaldavSynchronizerRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btLogin;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btConfig;
    }

    partial class ThisRibbonCollection
    {
        internal HanbiroCaldavSynchronizerRibbon HanbiroCaldavSynchronizerRibbon
        {
            get { return this.GetRibbon<HanbiroCaldavSynchronizerRibbon>(); }
        }
    }
}
