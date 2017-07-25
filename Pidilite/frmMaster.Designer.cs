using System.Drawing;
using System.Windows.Forms;

namespace Pidilite
{
    partial class frmMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaster));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.pnlData = new System.Windows.Forms.Panel();
            this.flPnlData = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlBreadCrumb = new System.Windows.Forms.Panel();
            this.lblSignOut = new System.Windows.Forms.Label();
            this.lblBreadCrumb = new System.Windows.Forms.Label();
            this.pbBreadCrumb = new System.Windows.Forms.PictureBox();
            this.pnlMainMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.pbCornorLogo = new System.Windows.Forms.PictureBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pbOrg = new System.Windows.Forms.PictureBox();
            this.tbpnlOrg = new System.Windows.Forms.TableLayoutPanel();
            this.cmbOrg = new System.Windows.Forms.ComboBox();
            this.btnModule = new System.Windows.Forms.Button();
            this.pnlCentre = new System.Windows.Forms.Panel();
            this.tpnlUserName = new System.Windows.Forms.TableLayoutPanel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pbAvatar = new Pidilite.CirclePictureBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.pnlData.SuspendLayout();
            this.pnlBreadCrumb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBreadCrumb)).BeginInit();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCornorLogo)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrg)).BeginInit();
            this.tbpnlOrg.SuspendLayout();
            this.pnlCentre.SuspendLayout();
            this.tpnlUserName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.pnlHead);
            this.pnlMain.Controls.Add(this.pnlData);
            this.pnlMain.Controls.Add(this.pnlMainMenu);
            this.pnlMain.Controls.Add(this.pnlLogo);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Controls.Add(this.pnlCentre);
            this.pnlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.Location = new System.Drawing.Point(0, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1120, 658);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlHead
            // 
            this.pnlHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHead.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlHead.Controls.Add(this.pbIcon);
            this.pnlHead.Controls.Add(this.lblMinimize);
            this.pnlHead.Controls.Add(this.lblClose);
            this.pnlHead.ForeColor = System.Drawing.Color.Black;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1122, 24);
            this.pnlHead.TabIndex = 19;
            // 
            // pbIcon
            // 
            this.pbIcon.Image = global::Pidilite.Properties.Resources.favicon;
            this.pbIcon.Location = new System.Drawing.Point(3, 5);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(24, 20);
            this.pbIcon.TabIndex = 3;
            this.pbIcon.TabStop = false;
            // 
            // lblMinimize
            // 
            this.lblMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMinimize.BackColor = System.Drawing.Color.LightSkyBlue;
            this.lblMinimize.Image = global::Pidilite.Properties.Resources.icon_minimize;
            this.lblMinimize.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblMinimize.Location = new System.Drawing.Point(1057, 1);
            this.lblMinimize.Name = "lblMinimize";
            this.lblMinimize.Size = new System.Drawing.Size(29, 22);
            this.lblMinimize.TabIndex = 2;
            this.lblMinimize.Click += new System.EventHandler(this.lblMinimize_Click);
            this.lblMinimize.MouseEnter += new System.EventHandler(this.lblMinimize_MouseEnter);
            this.lblMinimize.MouseLeave += new System.EventHandler(this.lblMinimize_MouseLeave);
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.BackColor = System.Drawing.Color.Red;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblClose.Location = new System.Drawing.Point(1089, 1);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(29, 22);
            this.lblClose.TabIndex = 2;
            this.lblClose.Text = "X";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // pnlData
            // 
            this.pnlData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlData.AutoSize = true;
            this.pnlData.Controls.Add(this.flPnlData);
            this.pnlData.Controls.Add(this.pnlBreadCrumb);
            this.pnlData.Location = new System.Drawing.Point(202, 127);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(920, 543);
            this.pnlData.TabIndex = 18;
            // 
            // flPnlData
            // 
            this.flPnlData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flPnlData.Location = new System.Drawing.Point(0, 45);
            this.flPnlData.Margin = new System.Windows.Forms.Padding(0);
            this.flPnlData.Name = "flPnlData";
            this.flPnlData.Padding = new System.Windows.Forms.Padding(15);
            this.flPnlData.Size = new System.Drawing.Size(920, 486);
            this.flPnlData.TabIndex = 1;
            // 
            // pnlBreadCrumb
            // 
            this.pnlBreadCrumb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBreadCrumb.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlBreadCrumb.Controls.Add(this.lblSignOut);
            this.pnlBreadCrumb.Controls.Add(this.lblBreadCrumb);
            this.pnlBreadCrumb.Controls.Add(this.pbBreadCrumb);
            this.pnlBreadCrumb.Location = new System.Drawing.Point(-2, 1);
            this.pnlBreadCrumb.Name = "pnlBreadCrumb";
            this.pnlBreadCrumb.Size = new System.Drawing.Size(923, 46);
            this.pnlBreadCrumb.TabIndex = 0;
            // 
            // lblSignOut
            // 
            this.lblSignOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSignOut.AutoSize = true;
            this.lblSignOut.Font = new System.Drawing.Font("Calibri", 10.75F, System.Drawing.FontStyle.Bold);
            this.lblSignOut.Image = ((System.Drawing.Image)(resources.GetObject("lblSignOut.Image")));
            this.lblSignOut.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSignOut.Location = new System.Drawing.Point(826, 10);
            this.lblSignOut.Name = "lblSignOut";
            this.lblSignOut.Size = new System.Drawing.Size(81, 18);
            this.lblSignOut.TabIndex = 4;
            this.lblSignOut.Text = "       Sign Out";
            // 
            // lblBreadCrumb
            // 
            this.lblBreadCrumb.AutoSize = true;
            this.lblBreadCrumb.Location = new System.Drawing.Point(41, 10);
            this.lblBreadCrumb.Name = "lblBreadCrumb";
            this.lblBreadCrumb.Size = new System.Drawing.Size(0, 13);
            this.lblBreadCrumb.TabIndex = 1;
            // 
            // pbBreadCrumb
            // 
            this.pbBreadCrumb.Location = new System.Drawing.Point(3, 6);
            this.pbBreadCrumb.Name = "pbBreadCrumb";
            this.pbBreadCrumb.Size = new System.Drawing.Size(37, 35);
            this.pbBreadCrumb.TabIndex = 0;
            this.pbBreadCrumb.TabStop = false;
            // 
            // pnlMainMenu
            // 
            this.pnlMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
            this.pnlMainMenu.Location = new System.Drawing.Point(0, 127);
            this.pnlMainMenu.Name = "pnlMainMenu";
            this.pnlMainMenu.Size = new System.Drawing.Size(200, 553);
            this.pnlMainMenu.TabIndex = 17;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(78)))), ((int)(((byte)(132)))));
            this.pnlLogo.Controls.Add(this.pbCornorLogo);
            this.pnlLogo.Location = new System.Drawing.Point(0, 54);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(200, 73);
            this.pnlLogo.TabIndex = 15;
            // 
            // pbCornorLogo
            // 
            this.pbCornorLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbCornorLogo.Image")));
            this.pbCornorLogo.Location = new System.Drawing.Point(50, 8);
            this.pbCornorLogo.Name = "pbCornorLogo";
            this.pbCornorLogo.Size = new System.Drawing.Size(99, 59);
            this.pbCornorLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCornorLogo.TabIndex = 0;
            this.pbCornorLogo.TabStop = false;
            // 
            // pnlTop
            // 
            this.pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(93)))));
            this.pnlTop.Controls.Add(this.pbOrg);
            this.pnlTop.Controls.Add(this.tbpnlOrg);
            this.pnlTop.Controls.Add(this.btnModule);
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 24);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1122, 31);
            this.pnlTop.TabIndex = 14;
            // 
            // pbOrg
            // 
            this.pbOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOrg.Image = ((System.Drawing.Image)(resources.GetObject("pbOrg.Image")));
            this.pbOrg.Location = new System.Drawing.Point(1094, 4);
            this.pbOrg.Name = "pbOrg";
            this.pbOrg.Size = new System.Drawing.Size(24, 24);
            this.pbOrg.TabIndex = 8;
            this.pbOrg.TabStop = false;
            // 
            // tbpnlOrg
            // 
            this.tbpnlOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbpnlOrg.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tbpnlOrg.ColumnCount = 1;
            this.tbpnlOrg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.41104F));
            this.tbpnlOrg.Controls.Add(this.cmbOrg, 0, 0);
            this.tbpnlOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbpnlOrg.Location = new System.Drawing.Point(845, -1);
            this.tbpnlOrg.Margin = new System.Windows.Forms.Padding(0);
            this.tbpnlOrg.Name = "tbpnlOrg";
            this.tbpnlOrg.RowCount = 1;
            this.tbpnlOrg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbpnlOrg.Size = new System.Drawing.Size(246, 32);
            this.tbpnlOrg.TabIndex = 7;
            // 
            // cmbOrg
            // 
            this.cmbOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOrg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(93)))));
            this.cmbOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOrg.ForeColor = System.Drawing.Color.LightGray;
            this.cmbOrg.FormattingEnabled = true;
            this.cmbOrg.Location = new System.Drawing.Point(3, 3);
            this.cmbOrg.Name = "cmbOrg";
            this.cmbOrg.Size = new System.Drawing.Size(240, 21);
            this.cmbOrg.TabIndex = 0;
            this.cmbOrg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbOrg_DrawItem);
            // 
            // btnModule
            // 
            this.btnModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(52)))), ((int)(((byte)(93)))));
            this.btnModule.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnModule.FlatAppearance.BorderSize = 0;
            this.btnModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModule.Location = new System.Drawing.Point(0, 0);
            this.btnModule.Name = "btnModule";
            this.btnModule.Size = new System.Drawing.Size(30, 31);
            this.btnModule.TabIndex = 0;
            this.btnModule.UseVisualStyleBackColor = true;
            this.btnModule.Click += new System.EventHandler(this.btnModule_Click);
            // 
            // pnlCentre
            // 
            this.pnlCentre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCentre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(69)))), ((int)(((byte)(117)))));
            this.pnlCentre.Controls.Add(this.tpnlUserName);
            this.pnlCentre.Controls.Add(this.pbAvatar);
            this.pnlCentre.Location = new System.Drawing.Point(200, 54);
            this.pnlCentre.Name = "pnlCentre";
            this.pnlCentre.Size = new System.Drawing.Size(922, 74);
            this.pnlCentre.TabIndex = 16;
            // 
            // tpnlUserName
            // 
            this.tpnlUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tpnlUserName.AutoSize = true;
            this.tpnlUserName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tpnlUserName.ColumnCount = 1;
            this.tpnlUserName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlUserName.Controls.Add(this.lblUserName, 0, 0);
            this.tpnlUserName.Location = new System.Drawing.Point(830, 52);
            this.tpnlUserName.Name = "tpnlUserName";
            this.tpnlUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tpnlUserName.RowCount = 1;
            this.tpnlUserName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlUserName.Size = new System.Drawing.Size(6, 15);
            this.tpnlUserName.TabIndex = 6;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(3, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 15);
            this.lblUserName.TabIndex = 6;
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbAvatar
            // 
            this.pbAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAvatar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pbAvatar.Location = new System.Drawing.Point(842, 3);
            this.pbAvatar.Name = "pbAvatar";
            this.pbAvatar.Size = new System.Drawing.Size(68, 67);
            this.pbAvatar.TabIndex = 5;
            this.pbAvatar.TabStop = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1122, 658);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pidilite";
            this.Load += new System.EventHandler(this.frmMaster_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.pnlData.ResumeLayout(false);
            this.pnlBreadCrumb.ResumeLayout(false);
            this.pnlBreadCrumb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBreadCrumb)).EndInit();
            this.pnlLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCornorLogo)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOrg)).EndInit();
            this.tbpnlOrg.ResumeLayout(false);
            this.pnlCentre.ResumeLayout(false);
            this.pnlCentre.PerformLayout();
            this.tpnlUserName.ResumeLayout(false);
            this.tpnlUserName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Panel pnlCentre;
        private System.Windows.Forms.PictureBox pbCornorLogo;
        private System.Windows.Forms.FlowLayoutPanel  pnlMainMenu;
        private System.Windows.Forms.Panel  pnlData;
        private System.Windows.Forms.Panel pnlBreadCrumb;
        private System.Windows.Forms.Label lblBreadCrumb;
        private System.Windows.Forms.PictureBox pbBreadCrumb;
        private FlowLayoutPanel flPnlData;
        public CirclePictureBox pbAvatar;
        public Label lblUserName;
        private Label lblSignOut;
        private Panel pnlHead;
        private Label lblMinimize;
        private Label lblClose;
        private PictureBox pbIcon;
        private Button btnModule;
        private TableLayoutPanel tpnlUserName;
        private ErrorProvider errorProvider1;
        private TableLayoutPanel tbpnlOrg;
        private PictureBox pbOrg;
        private ComboBox cmbOrg;
    }
}