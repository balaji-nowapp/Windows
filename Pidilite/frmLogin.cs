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
            if (txtUserName.Text.Trim() == "" || txtUserName.Text.Trim() == "Email or Mobile no." || txtPassword.Text.Trim() == "" || txtPassword.Text.Trim() == "Password")
            {
                status = "Your username/password was not entered";
                timerStatus.Enabled = true;
                timerStatus.Interval = 1000; // it will Tick in 1 seconds
                timerStatus.Tick += new EventHandler(timerStatus_Tick);
                timerStatus.Start();
            }
            else
            {
                if (txtUserName.Text.Trim() != "" && txtPassword.Text.Trim() != "")
                {
                    userName = txtUserName.Text;
                    userPwd = txtPassword.Text;
                    RegistryConfig.UserName = userName;
                    RegistryConfig.Password = userPwd;
                    RegistryConfig.LoadRegValues();
                    if (RegistryConfig.isRegEmpty == true)
                    {
                        userCredentialValidation(userName, userPwd);// validating in central server
                        frmServerDetails frmServer = new frmServerDetails(FirstName, LastName, DbName);
                        frmServer.frmReadPNG(Avatar);
                        frmServer.Show();
                        if (isHide == true)
                            Hide();
                        else
                        {
                           // status = respJson["message"];
                            timerStatus.Enabled = true;
                            timerStatus.Interval = 1000; // it will Tick in 1 seconds
                            timerStatus.Tick += new EventHandler(timerStatus_Tick);
                            timerStatus.Start();
                        }

                    }
                    else
                    {
                        RegistryConfig.myConn = "Server=" + RegistryConfig.serverName + ";Integrated security=SSPI;database=" + RegistryConfig.database + ";User Id= " + RegistryConfig.serverUser + ";Password =" + RegistryConfig.serverPwd;
                        userLoginCheck(userName, userPwd);
                        if (isHide == true)
                        {
                            frmMaster oMaster = new frmMaster(FirstName + " " + LastName, userPhoto);
                            oMaster.Show();
                        }
                        else
                        {
                          //status = respJson["message"];
                            timerStatus.Enabled = true;
                            timerStatus.Interval = 1000; // it will Tick in 1 seconds
                            timerStatus.Tick += new EventHandler(timerStatus_Tick);
                            timerStatus.Start();
                        }

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
                            RegistryConfig.userId = Convert.ToInt64(respJson["id"]);
                            isHide = true;
                        }
                        else
                        {
                            isHide = false;
                            status = Convert.ToString(respJson["message"]);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
                Log.LogData("Error in User Credential Validation: " + ex.Message + ex.StackTrace, Log.Status.Error);
                isHide = false;
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Select();
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
                                RegistryConfig.userId = Convert.ToInt64(vOpcode.Value);
                                status = "";// Login Success 
                                userDetails oDetails = new userDetails();
                               oDetails =  opGetUserDetails(Convert.ToInt64(vOpcode.Value));
                                FirstName = oDetails.FirstName;
                                LastName = oDetails.LastName;
                                if (oDetails.Photo != null )
                                userPhoto =RegistryConfig.ByteToImage( oDetails.Photo);
                                else
                                    try
                                    {
                                        userPhoto = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.User)
                            
                                    { ForeColor = SystemColors.ButtonFace, Size = 150 });
                                frmMaster oMaster = new frmMaster(FirstName+" "+LastName,userPhoto);
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                                        userPhoto = Properties.Resources.icon_profile ;
                                    }
                                isHide = true;
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
                            else if (Convert.ToInt64(vOpcode.Value) == -4)
                            {
                                Log.LogData("No Wpassword saved for this user", Log.Status.Info); // First Time Login
                                userCredentialValidation(UserID, Password);
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
                    status = "Database Error...";
                    Log.LogData("Error in User Login Check: " + ex.Message + ex.StackTrace, Log.Status.Error);
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
