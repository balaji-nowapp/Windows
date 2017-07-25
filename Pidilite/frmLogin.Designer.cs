namespace Pidilite
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnlSec = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblForgotPwd = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlPwd = new System.Windows.Forms.Panel();
            this.pnlTextPwd = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pbPwd = new System.Windows.Forms.PictureBox();
            this.pnlUserName = new System.Windows.Forms.Panel();
            this.pbMail = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlTextUser = new System.Windows.Forms.Panel();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pnlSec.SuspendLayout();
            this.pnlPwd.SuspendLayout();
            this.pnlTextPwd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPwd)).BeginInit();
            this.pnlUserName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlTextUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerStatus
            // 
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pnlSec);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 332);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.pbLogo);
            this.panel2.Location = new System.Drawing.Point(7, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(380, 90);
            this.panel2.TabIndex = 2;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(42, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(302, 99);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // pnlSec
            // 
            this.pnlSec.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlSec.Controls.Add(this.lblStatus);
            this.pnlSec.Controls.Add(this.lblForgotPwd);
            this.pnlSec.Controls.Add(this.btnLogin);
            this.pnlSec.Controls.Add(this.pnlPwd);
            this.pnlSec.Controls.Add(this.pnlUserName);
            this.pnlSec.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSec.ForeColor = System.Drawing.SystemColors.Window;
            this.pnlSec.Location = new System.Drawing.Point(7, 98);
            this.pnlSec.Name = "pnlSec";
            this.pnlSec.Size = new System.Drawing.Size(380, 226);
            this.pnlSec.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 9.25F);
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(36, 195);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 5;
            // 
            // lblForgotPwd
            // 
            this.lblForgotPwd.AutoSize = true;
            this.lblForgotPwd.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForgotPwd.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblForgotPwd.Location = new System.Drawing.Point(158, 173);
            this.lblForgotPwd.Name = "lblForgotPwd";
            this.lblForgotPwd.Size = new System.Drawing.Size(85, 13);
            this.lblForgotPwd.TabIndex = 4;
            this.lblForgotPwd.Text = "Forgot Password";
            this.lblForgotPwd.Click += new System.EventHandler(this.lblForgotPwd_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Gold;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogin.Image = global::Pidilite.Properties.Resources.Login;
            this.btnLogin.Location = new System.Drawing.Point(143, 143);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(113, 27);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "    Login";
            this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogin.UseMnemonic = false;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pnlPwd
            // 
            this.pnlPwd.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlPwd.Controls.Add(this.pnlTextPwd);
            this.pnlPwd.Controls.Add(this.pbPwd);
            this.pnlPwd.Location = new System.Drawing.Point(39, 83);
            this.pnlPwd.Name = "pnlPwd";
            this.pnlPwd.Size = new System.Drawing.Size(298, 46);
            this.pnlPwd.TabIndex = 1;
            // 
            // pnlTextPwd
            // 
            this.pnlTextPwd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlTextPwd.Controls.Add(this.txtPassword);
            this.pnlTextPwd.Location = new System.Drawing.Point(51, 7);
            this.pnlTextPwd.Name = "pnlTextPwd";
            this.pnlTextPwd.Size = new System.Drawing.Size(238, 33);
            this.pnlTextPwd.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtPassword.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtPassword.Location = new System.Drawing.Point(2, 7);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(234, 19);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "Password";
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassword.Click += new System.EventHandler(this.txtPassword_Click);
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // pbPwd
            // 
            this.pbPwd.Image = global::Pidilite.Properties.Resources.icon_password;
            this.pbPwd.Location = new System.Drawing.Point(21, 17);
            this.pbPwd.Name = "pbPwd";
            this.pbPwd.Size = new System.Drawing.Size(22, 23);
            this.pbPwd.TabIndex = 1;
            this.pbPwd.TabStop = false;
            // 
            // pnlUserName
            // 
            this.pnlUserName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlUserName.Controls.Add(this.pbMail);
            this.pnlUserName.Controls.Add(this.pictureBox2);
            this.pnlUserName.Controls.Add(this.pnlTextUser);
            this.pnlUserName.Location = new System.Drawing.Point(39, 18);
            this.pnlUserName.Name = "pnlUserName";
            this.pnlUserName.Size = new System.Drawing.Size(298, 45);
            this.pnlUserName.TabIndex = 0;
            // 
            // pbMail
            // 
            this.pbMail.Image = global::Pidilite.Properties.Resources.icon_mail;
            this.pbMail.Location = new System.Drawing.Point(21, 16);
            this.pbMail.Name = "pbMail";
            this.pbMail.Size = new System.Drawing.Size(22, 20);
            this.pbMail.TabIndex = 2;
            this.pbMail.TabStop = false;
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
            this.pnlTextUser.Controls.Add(this.txtUserName);
            this.pnlTextUser.Location = new System.Drawing.Point(49, 7);
            this.pnlTextUser.Name = "pnlTextUser";
            this.pnlTextUser.Size = new System.Drawing.Size(238, 33);
            this.pnlTextUser.TabIndex = 1;
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtUserName.Location = new System.Drawing.Point(2, 7);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(233, 19);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "Email or Mobile no.";
            this.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUserName.Click += new System.EventHandler(this.txtUserName_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(393, 332);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pnlSec.ResumeLayout(false);
            this.pnlSec.PerformLayout();
            this.pnlPwd.ResumeLayout(false);
            this.pnlTextPwd.ResumeLayout(false);
            this.pnlTextPwd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPwd)).EndInit();
            this.pnlUserName.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlTextUser.ResumeLayout(false);
            this.pnlTextUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel pnlSec;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblForgotPwd;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel pnlPwd;
        private System.Windows.Forms.Panel pnlTextPwd;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pbPwd;
        private System.Windows.Forms.Panel pnlUserName;
        private System.Windows.Forms.PictureBox pbMail;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel pnlTextUser;
        private System.Windows.Forms.TextBox txtUserName;
    }
}

