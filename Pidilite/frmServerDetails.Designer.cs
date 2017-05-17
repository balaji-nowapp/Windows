namespace Pidilite
{
    partial class frmServerDetails
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServerDetails));
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblSignOut = new System.Windows.Forms.Label();
            this.pbAvatar = new Pidilite.CirclePictureBox();
            this.pbCornorLogo = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.pbPwd = new System.Windows.Forms.PictureBox();
            this.pnlPwd = new System.Windows.Forms.Panel();
            this.pnlTextPwd = new System.Windows.Forms.Panel();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.pbUserID = new System.Windows.Forms.PictureBox();
            this.pnlUserName = new System.Windows.Forms.Panel();
            this.pbServer = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlTextUser = new System.Windows.Forms.Panel();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.pnlMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCornorLogo)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPwd)).BeginInit();
            this.pnlPwd.SuspendLayout();
            this.pnlTextPwd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserID)).BeginInit();
            this.pnlUserName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlTextUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Controls.Add(this.panel7);
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(443, 450);
            this.pnlMain.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.pbAvatar);
            this.panel2.Controls.Add(this.pbCornorLogo);
            this.panel2.Location = new System.Drawing.Point(7, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 112);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblUserName);
            this.panel3.Controls.Add(this.lblSignOut);
            this.panel3.Location = new System.Drawing.Point(0, 70);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(427, 41);
            this.panel3.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(344, 9);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(51, 15);
            this.lblUserName.TabIndex = 3;
            this.lblUserName.Text = "lblUserName";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSignOut
            // 
            this.lblSignOut.AutoSize = true;
            this.lblSignOut.Font = new System.Drawing.Font("Calibri", 10.75F, System.Drawing.FontStyle.Bold);
            this.lblSignOut.Image = ((System.Drawing.Image)(resources.GetObject("lblSignOut.Image")));
            this.lblSignOut.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSignOut.Location = new System.Drawing.Point(345, 24);
            this.lblSignOut.Name = "lblSignOut";
            this.lblSignOut.Size = new System.Drawing.Size(81, 18);
            this.lblSignOut.TabIndex = 0;
            this.lblSignOut.Text = "       Sign Out";
            this.lblSignOut.Click += new System.EventHandler(this.lblSignOut_Click);
            // 
            // pbAvatar
            // 
            this.pbAvatar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pbAvatar.Location = new System.Drawing.Point(354, 3);
            this.pbAvatar.Name = "pbAvatar";
            this.pbAvatar.Size = new System.Drawing.Size(68, 67);
            this.pbAvatar.TabIndex = 2;
            this.pbAvatar.TabStop = false;
            // 
            // pbCornorLogo
            // 
            this.pbCornorLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbCornorLogo.Image")));
            this.pbCornorLogo.Location = new System.Drawing.Point(3, 0);
            this.pbCornorLogo.Name = "pbCornorLogo";
            this.pbCornorLogo.Size = new System.Drawing.Size(118, 70);
            this.pbCornorLogo.TabIndex = 0;
            this.pbCornorLogo.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Menu;
            this.panel7.Controls.Add(this.prgBar);
            this.panel7.Controls.Add(this.panel6);
            this.panel7.Location = new System.Drawing.Point(7, 120);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(430, 323);
            this.panel7.TabIndex = 2;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(0, 306);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(430, 14);
            this.prgBar.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Window;
            this.panel6.Controls.Add(this.lblStatus);
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Controls.Add(this.btnCreate);
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Controls.Add(this.pnlPwd);
            this.panel6.Controls.Add(this.pnlUserName);
            this.panel6.Location = new System.Drawing.Point(32, 28);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(365, 272);
            this.panel6.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(4, 256);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancel.Image = global::Pidilite.Properties.Resources.icon_Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(205, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "      Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.UseMnemonic = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.Gold;
            this.btnCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCreate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCreate.Image = global::Pidilite.Properties.Resources.icon_create;
            this.btnCreate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCreate.Location = new System.Drawing.Point(97, 220);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(89, 27);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "       Create";
            this.btnCreate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCreate.UseMnemonic = false;
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.pbPwd);
            this.panel5.Location = new System.Drawing.Point(34, 147);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(298, 46);
            this.panel5.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel4.Controls.Add(this.txtPwd);
            this.panel4.Location = new System.Drawing.Point(51, 7);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(238, 33);
            this.panel4.TabIndex = 2;
            // 
            // txtPwd
            // 
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtPwd.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtPwd.Location = new System.Drawing.Point(2, 7);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(234, 19);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.Text = "Password";
            this.txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPwd.Click += new System.EventHandler(this.txtPwd_Click);
            this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
            // 
            // pbPwd
            // 
            this.pbPwd.Image = global::Pidilite.Properties.Resources.icon_password1;
            this.pbPwd.Location = new System.Drawing.Point(21, 17);
            this.pbPwd.Name = "pbPwd";
            this.pbPwd.Size = new System.Drawing.Size(22, 23);
            this.pbPwd.TabIndex = 1;
            this.pbPwd.TabStop = false;
            // 
            // pnlPwd
            // 
            this.pnlPwd.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlPwd.Controls.Add(this.pnlTextPwd);
            this.pnlPwd.Controls.Add(this.pbUserID);
            this.pnlPwd.Location = new System.Drawing.Point(34, 86);
            this.pnlPwd.Name = "pnlPwd";
            this.pnlPwd.Size = new System.Drawing.Size(298, 46);
            this.pnlPwd.TabIndex = 8;
            // 
            // pnlTextPwd
            // 
            this.pnlTextPwd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlTextPwd.Controls.Add(this.txtUserID);
            this.pnlTextPwd.Location = new System.Drawing.Point(51, 7);
            this.pnlTextPwd.Name = "pnlTextPwd";
            this.pnlTextPwd.Size = new System.Drawing.Size(238, 33);
            this.pnlTextPwd.TabIndex = 2;
            // 
            // txtUserID
            // 
            this.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserID.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtUserID.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtUserID.Location = new System.Drawing.Point(2, 7);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(234, 19);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Text = "User ID";
            this.txtUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUserID.Click += new System.EventHandler(this.txtUserID_Click);
            // 
            // pbUserID
            // 
            this.pbUserID.Image = global::Pidilite.Properties.Resources.icon_lock;
            this.pbUserID.Location = new System.Drawing.Point(21, 17);
            this.pbUserID.Name = "pbUserID";
            this.pbUserID.Size = new System.Drawing.Size(22, 23);
            this.pbUserID.TabIndex = 1;
            this.pbUserID.TabStop = false;
            // 
            // pnlUserName
            // 
            this.pnlUserName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlUserName.Controls.Add(this.pbServer);
            this.pnlUserName.Controls.Add(this.pictureBox2);
            this.pnlUserName.Controls.Add(this.pnlTextUser);
            this.pnlUserName.Location = new System.Drawing.Point(34, 25);
            this.pnlUserName.Name = "pnlUserName";
            this.pnlUserName.Size = new System.Drawing.Size(298, 45);
            this.pnlUserName.TabIndex = 7;
            // 
            // pbServer
            // 
            this.pbServer.Image = global::Pidilite.Properties.Resources.icon_Sever;
            this.pbServer.Location = new System.Drawing.Point(21, 16);
            this.pbServer.Name = "pbServer";
            this.pbServer.Size = new System.Drawing.Size(22, 20);
            this.pbServer.TabIndex = 2;
            this.pbServer.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 40);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pnlTextUser
            // 
            this.pnlTextUser.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlTextUser.Controls.Add(this.txtServer);
            this.pnlTextUser.Location = new System.Drawing.Point(49, 7);
            this.pnlTextUser.Name = "pnlTextUser";
            this.pnlTextUser.Size = new System.Drawing.Size(238, 33);
            this.pnlTextUser.TabIndex = 1;
            // 
            // txtServer
            // 
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtServer.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtServer.Location = new System.Drawing.Point(2, 7);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(233, 19);
            this.txtServer.TabIndex = 0;
            this.txtServer.Text = "Server ";
            this.txtServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtServer.Click += new System.EventHandler(this.txtServer_Click);
            // 
            // frmServerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(443, 450);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmServerDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmServerDetails";
            this.pnlMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCornorLogo)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPwd)).EndInit();
            this.pnlPwd.ResumeLayout(false);
            this.pnlTextPwd.ResumeLayout(false);
            this.pnlTextPwd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserID)).EndInit();
            this.pnlUserName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlTextUser.ResumeLayout(false);
            this.pnlTextUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblSignOut;
        public CirclePictureBox pbAvatar;
        private System.Windows.Forms.PictureBox pbCornorLogo;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.PictureBox pbPwd;
        private System.Windows.Forms.Panel pnlPwd;
        private System.Windows.Forms.Panel pnlTextPwd;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.PictureBox pbUserID;
        private System.Windows.Forms.Panel pnlUserName;
        private System.Windows.Forms.PictureBox pbServer;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel pnlTextUser;
        private System.Windows.Forms.TextBox txtServer;
        public System.Windows.Forms.Label lblUserName;
    }
}