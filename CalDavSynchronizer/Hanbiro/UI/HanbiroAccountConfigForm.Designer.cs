namespace CalDavSynchronizer.Hanbiro.UI
{
    partial class HanbiroAccountConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HanbiroAccountConfigForm));
            this.lbSyncInterval = new System.Windows.Forms.Label();
            this.cbSyncInterval = new System.Windows.Forms.ComboBox();
            this.btOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPastDay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFutureDay = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbSyncInterval
            // 
            this.lbSyncInterval.AutoSize = true;
            this.lbSyncInterval.Location = new System.Drawing.Point(40, 40);
            this.lbSyncInterval.Name = "lbSyncInterval";
            this.lbSyncInterval.Size = new System.Drawing.Size(207, 16);
            this.lbSyncInterval.TabIndex = 0;
            this.lbSyncInterval.Text = "Synchronization interval (minutes):";
            // 
            // cbSyncInterval
            // 
            this.cbSyncInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSyncInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSyncInterval.FormattingEnabled = true;
            this.cbSyncInterval.Location = new System.Drawing.Point(43, 75);
            this.cbSyncInterval.Name = "cbSyncInterval";
            this.cbSyncInterval.Size = new System.Drawing.Size(252, 24);
            this.cbSyncInterval.TabIndex = 1;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Location = new System.Drawing.Point(220, 272);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 30);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Synchronization timespan past (days):";
            // 
            // tbPastDay
            // 
            this.tbPastDay.Location = new System.Drawing.Point(43, 155);
            this.tbPastDay.Name = "tbPastDay";
            this.tbPastDay.Size = new System.Drawing.Size(100, 22);
            this.tbPastDay.TabIndex = 4;
            this.tbPastDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPastDay_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Synchronization timespan future (days):";
            // 
            // tbFutureDay
            // 
            this.tbFutureDay.Location = new System.Drawing.Point(43, 230);
            this.tbFutureDay.Name = "tbFutureDay";
            this.tbFutureDay.Size = new System.Drawing.Size(100, 22);
            this.tbFutureDay.TabIndex = 6;
            this.tbFutureDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFutureDay_KeyPress);
            // 
            // HanbiroAccountConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(338, 329);
            this.Controls.Add(this.tbFutureDay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPastDay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.cbSyncInterval);
            this.Controls.Add(this.lbSyncInterval);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "HanbiroAccountConfigForm";
            this.Text = "Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSyncInterval;
        private System.Windows.Forms.ComboBox cbSyncInterval;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPastDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFutureDay;
    }
}