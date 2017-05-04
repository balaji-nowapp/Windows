using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Pidilite
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {

            InitializeComponent();

        }
        int second = 0;
        public static string status { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string DbName { get; set; }
        public static string Avatar { get; set; }
        public static Bitmap userPhoto { get; set; }
        public string userName = string.Empty, userPwd = string.Empty, str = string.Empty;
        dynamic respJson;
        bool isHide = false;
        private void txtUserName_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            timerStatus.Stop();
            if (txtUserName.Text.Trim() == "Email or Mobile no.")
            {
                txtUserName.Text = "";

            }
        }
        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            lblStatus.Text = "";
            timerStatus.Stop();
            if (txtPassword.Text.Trim() == "Password")
            {
                txtPassword.Text = "";

            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            second = 0;
            if (txtUserName.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                userName = txtUserName.Text;
                userPwd = txtPassword.Text;

                RegistryConfig.LoadRegValues();
                if (RegistryConfig.isRegEmpty == true)
                {
                    userCredentialValidation(userName, userPwd);// validating in central server
                    frmServerDetails frmServer = new frmServerDetails(FirstName, LastName, DbName);
                    frmServer.frmReadPNG(Avatar);
                    frmServer.Show();
                    if (isHide == true)
                        this.Hide();
                    else
                    {
                        status = respJson["message"];
                        timerStatus.Enabled = true;
                        timerStatus.Interval = 1000; // it will Tick in 1 seconds
                        timerStatus.Tick += new EventHandler(timerStatus_Tick);
                        timerStatus.Start();
                    }

                }
                else
                {

                    //    userLoginCheck(userName, userPwd);
                    Hide();
                    RegistryConfig.myConn = "Server=" + RegistryConfig.serverName  + ";Integrated security=SSPI;database=" + RegistryConfig.database  + ";User Id= " + RegistryConfig.serverUser  + ";Password =" + RegistryConfig.serverPwd ;
                    frmMaster oMaster = new frmMaster(FirstName + " " + LastName, userPhoto);
                    oMaster.Show();

                }
            }
            else
            {
                status = "Invalid Credentials...";
                timerStatus.Enabled = true;
                timerStatus.Interval = 1000; // it will Tick in 1 seconds
                timerStatus.Tick += new EventHandler(timerStatus_Tick);
                timerStatus.Start();

            }



        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
            lblStatus.Text = "";
            timerStatus.Stop();
            if (txtPassword.Text.Trim() == "Password")
            {
                txtPassword.Text = "";

            }
        }
        private void lblForgotPwd_Click(object sender, EventArgs e)
        {

            this.Hide();
            frmForgotPassword frmPwd = new Pidilite.frmForgotPassword();
            frmPwd.ShowDialog();
        }
        private void timerStatus_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = status;
            second++;
            if (second >= 30)
            {
                timerStatus.Stop();
                status = "";
                lblStatus.Text = status;
            }

        }
        private void userCredentialValidation(string UserID, string Password)
        {

            Application.DoEvents();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Url"] + "/signin");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var postData = "userId=" + UserID;
            postData += "&pswd=" + Password;
            var data = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = data.Length; request.Timeout = 300000;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }


            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var theResponseStream = new StreamReader(response.GetResponseStream());
                        string result = theResponseStream.ReadToEnd();
                        respJson = JsonConvert.DeserializeObject<dynamic>(result);
                        if (Convert.ToInt16(respJson["status"]) == 0)
                        {
                            Avatar = Convert.ToString(respJson["avatar"]);
                            FirstName = Convert.ToString(respJson["firstname"]);
                            LastName = Convert.ToString(respJson["lastname"]);
                            DbName = Convert.ToString(respJson["dbName"]);
                            isHide = true;

                        }
                        else
                        {
                            isHide = false;
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                isHide = false;
            }


        }
        private void userLoginCheck(string UserID, string Password)
        {
            if (UserID.Trim() == null || UserID.Trim() == "" || Password.Trim() == null || Password.Trim() == "")
            {
                status = "Your username/password was not entered";
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                    {
                        con.Open();
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            var vEncryptedPassword = EncryptionDecryption.SHA1HashStringForUTF8String(Password);

                            cmd.CommandText = "usp_LoginCheck";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserName", UserID);
                            cmd.Parameters.AddWithValue("@Password", Convert.ToString(vEncryptedPassword));
                            var vOpcode = cmd.Parameters.Add("@OPCode", SqlDbType.BigInt);
                            vOpcode.Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            if (vOpcode.Value != null && Convert.ToString(vOpcode.Value).Trim() != "" && !Convert.ToString(vOpcode.Value).Trim().StartsWith("-"))
                            {
                                status = "";// Login Success 
                                userDetails oDetails = new userDetails();
                              oDetails =  opGetUserDetails(Convert.ToInt64(vOpcode.Value));
                                FirstName = oDetails.FirstName;
                                LastName = oDetails.LastName;
                                userPhoto = frmServerDetails.ByteToImage( oDetails.Photo);

                                frmMaster oMaster = new frmMaster(FirstName+" "+LastName,userPhoto);
                                

                            }
                            else if (Convert.ToInt64(vOpcode.Value) == -1)
                            {
                                status = "Your Account is not active"; // Inactive User 
                            }

                            else if (Convert.ToInt64(vOpcode.Value) == -2)
                            {
                                status = "Your Account is not active"; // Inactive User 
                            }

                            else if (Convert.ToInt64(vOpcode.Value) == -3)
                            {
                                status = "Your username/password combination was incorrect"; // InCorrect Inputs 
                            }
                            timerStatus.Enabled = true;
                            timerStatus.Interval = 1000; // it will Tick in 1 seconds
                            timerStatus.Tick += new EventHandler(timerStatus_Tick);
                            timerStatus.Start();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public userDetails opGetUserDetails (Int64 UserId)
        {
           List<userDetails> oDetail = new List<Pidilite.userDetails>();
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
            
                    using (DataContext dx = new DataContext(con))
                    {
                        string tQuery = "usp_GetUserDetail {0}";
                        var results = dx.ExecuteQuery<userDetails>(tQuery, UserId);
                        oDetail = results.ToList<userDetails>();
                    }
                
            }
            return oDetail[0];
                }

    }

    public class userDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
    }

}
