using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Pidilite
{
    public partial class frmServerDetails : Form
    {
        #region [Declaration]

        public dynamic respJson;
        public int totalTable = 0;
        public string tableCreation, column;
        public int j = 1, isCreated = 0;
        public JObject ObjTable;
        public JToken value;
        public string[] tables;
        public DataTable dt, copyTable;
        public string tableName;
        public bool isAvailable = true;
        int second = 0;
        public static string userName { get; set; }
        public string status { get; set; }
        public string newDatabase { get; set; }
        #endregion
        public frmServerDetails(string dbName)
        {
            InitializeComponent();
            newDatabase = dbName;
        }
        public frmServerDetails(string firstName, string lastName, string dbName)
        {
            InitializeComponent();
            this.ActiveControl = txtServer;
            userName = firstName + " " + lastName;
            lblUserName.Text = userName;
            newDatabase = dbName;
            backgroundWorker1 = new BackgroundWorker();
            prgBar.Visible = false;
            prgBar.Style = ProgressBarStyle.Blocks;
            InitializeBackgroundWorker();


        }

        #region [Create Database]
        private void btnCreate_Click(object sender, EventArgs e)
        {

            RegistryConfig.myConn = "Server=" + txtServer.Text + ";Integrated security=SSPI;database=master;User Id= " + txtUserID.Text + ";Password =" + txtPwd.Text;


            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select count(*) from sysdatabases where name = '" + newDatabase + "'", con))
                {
                    var i = cmd.ExecuteScalar();
                    isCreated = Convert.ToInt32(i);

                }
            }


            if (isCreated == 0)
            {
                opDataBaseCreation();
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("DataBase already exists. Do you want delete and Create again?", "Message", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Drop database " + newDatabase, con);
                        cmd.ExecuteScalar();
                        opDataBaseCreation();
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }

        }

        private void opDataBaseCreation()
        {
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("CREATE DATABASE " + newDatabase, con))
                {
                    cmd.ExecuteScalar();
                }
                RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryConfig.registryName);
                key.SetValue("server_name", txtServer.Text);
                key.SetValue("server_user", txtUserID.Text);
                key.SetValue("server_Pwd", txtPwd.Text);
                key.SetValue("database", newDatabase);
                key.Close();


            }


            RegistryConfig.myConn = "Server=" + txtServer.Text + ";Integrated security=SSPI;database=" + newDatabase + ";User Id= " + txtUserID.Text + ";Password =" + txtPwd.Text;
            beGetTableDetails();
            ObjTable = respJson["dbinfo"];
            value = ObjTable["tables"];
            tables = value.ToObject<string[]>();
            totalTable = tables.Length;
            try
            {
                if (!backgroundWorker1.IsBusy)
                {

                    lblStatus.Text = "Creating tables...";
                    prgBar.Visible = true;
                    prgBar.Minimum = 0;
                    prgBar.Maximum = totalTable;
                    btnCreate.Enabled = false;
                    btnCancel.Enabled = true;
                    backgroundWorker1.RunWorkerAsync();

                }


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static DataTable toDataTable(JArray jArray)
        {
            var result = new DataTable();
            //Initialize the columns, If you know the row type, replace this   
            foreach (var row in jArray)
            {
                foreach (var jToken in row)
                {
                    var jproperty = jToken as JProperty;
                    if (jproperty == null) continue;
                    if (result.Columns[jproperty.Name] == null)
                        result.Columns.Add(jproperty.Name, typeof(string));
                }
            }
            foreach (var row in jArray)
            {
                var datarow = result.NewRow();
                foreach (var jToken in row)
                {
                    var jProperty = jToken as JProperty;
                    if (jProperty == null) continue;
                    datarow[jProperty.Name] = jProperty.Value.ToString();
                }
                result.Rows.Add(datarow);
            }

            return result;
        }
        #endregion

        #region [Cancel]
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation && backgroundWorker2.WorkerSupportsCancellation)
            {
                backgroundWorker1.CancelAsync();
                backgroundWorker2.CancelAsync();
                lblStatus.Text = "Task Cancelled...";
                btnCreate.Enabled = true;
                prgBar.Value = 0;
                j = 1;
                prgBar.Hide();

            }
        }
        #endregion

        #region [Get Table Details]
        public void beGetTableDetails()
        {
            Application.DoEvents();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Url"] + "/dbdetails?dbName=" + newDatabase);
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var theResponseStream = new StreamReader(response.GetResponseStream());
                        string result = theResponseStream.ReadToEnd();
                        respJson = JsonConvert.DeserializeObject<dynamic>(result);
                    }

                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [BackgroundWorker]
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
            backgroundWorker2.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker2_ProgressChanged);
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
        }

        #region [Table Creation]
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] fields, type, defaultValue, nullable, key, extra;
            foreach (var tab in tables)
            {
                Application.DoEvents();

                fields = (ObjTable[tab]["fields"].ToObject<string[]>());
                type = (ObjTable[tab]["Type"].ToObject<string[]>());
                defaultValue = (ObjTable[tab]["Default"].ToObject<string[]>());
                nullable = (ObjTable[tab]["Null"].ToObject<string[]>());
                key = (ObjTable[tab]["Key"].ToObject<string[]>());
                extra = (ObjTable[tab]["Extra"].ToObject<string[]>());
                tableCreation = "Create Table " + tab + "\n( \n";

                if (backgroundWorker1.CancellationPending == true)
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    break;
                }

                else
                {

                    for (int i = 0; i < fields.Length; i++)
                    {

                        tableCreation = tableCreation + fields[i] + " ";
                        if (type[i].ToLower().Contains("mediumint"))
                        {
                            tableCreation = tableCreation + "int ";
                        }
                        else if (type[i].ToLower().Contains("int"))
                        {
                            tableCreation = tableCreation + type[i].Split('(')[0] + " ";
                        }
                        else if (type[i].ToLower().Contains("text"))
                        {
                            type[i] = "nvarchar(max)";
                            tableCreation = tableCreation + type[i] + " ";
                        }
                        else if (type[i].ToLower().StartsWith("enum"))
                        {
                            string enumVal = type[i].Replace("enum", "");
                            tableCreation = tableCreation + "VARCHAR(max) CHECK(" + fields[i] + " IN" + enumVal + ") ";
                        }
                        else if (type[i].ToLower().StartsWith("double"))
                        {
                            type[i] = type[i].Replace("double", "float(24)");
                            tableCreation = tableCreation + type[i] + " ";
                        }
                        else if (type[i].ToLower().StartsWith("float("))
                        {
                            type[i] = "float(24)";
                            tableCreation = tableCreation + type[i] + " ";
                        }
                        else
                        {
                            tableCreation = tableCreation + type[i] + " ";
                        }
                        if ((key[i].Trim() != "") && (key[i].ToLower() == "pri"))
                        {
                            tableCreation = tableCreation + "Primary Key ";

                        }
                        if ((extra[i].Trim() != "") && (extra[i].ToLower() == "auto_increment"))
                        {
                            tableCreation = tableCreation + "Identity (1,1) ";
                        }
                        if (defaultValue[i] != null)
                        {

                            if (type[i].Split('(')[0] == "varchar")
                            {
                                if (defaultValue[i] == "")
                                {
                                    tableCreation = tableCreation + " ";
                                }
                                tableCreation = tableCreation + "default " + Convert.ToString(defaultValue[i]) + " ";
                            }
                            if (type[i].ToLower().StartsWith("int"))
                            {
                                tableCreation = tableCreation + "default " + Convert.ToInt16(defaultValue[i]) + " ";
                            }
                        }
                        if (nullable[i].ToLower() == "yes")
                        {
                            tableCreation = tableCreation + "null";
                        }
                        else
                        {
                            tableCreation = tableCreation + "not null";
                        }
                        if (i != fields.Length - 1)
                            tableCreation = tableCreation + ", \n";
                    }
                    tableCreation = tableCreation + "\n)";
                    using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                    {
                        SqlCommand myCommand = new SqlCommand(tableCreation);
                        myCommand.Connection = con;
                        try
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                            myCommand.ExecuteNonQuery();
                            Log.LogData("Table creation Completed", Log.Status.Debug);
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            Log.LogData("Error in Table Creation: " + ex.Message + ex.StackTrace + "Query:" + tableCreation, Log.Status.Error);
                        }
                    }
                }
                backgroundWorker1.ReportProgress(j);
                j++;
                Application.DoEvents();

                //}
                //try
                //{
                //    using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                //    {
                //        ServerConnection svrConnection = new ServerConnection(con);
                //        Server server = new Server(svrConnection);
                //        server.ConnectionContext.ExecuteNonQuery(StoreProcedure.str);
                //        Log.LogData("SP creation Completed", Log.Status.Debug);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Log.LogData("Error in Creating Store Procedures: " + ex.Message + ex.StackTrace, Log.Status.Error);
                //}
            }
                try
                {
                    using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                    {
                        con.Open();
                        foreach (var qry in StoreProcedure.spArray)
                        {
                            SqlCommand Cmd = new SqlCommand(qry);
                            Cmd.Connection = con;
                            Cmd.CommandType = CommandType.Text;
                            Cmd.ExecuteScalar();
                            Cmd.Dispose();
                            Cmd = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.LogData("Error in Creating Store Procedures: " + ex.Message + ex.StackTrace, Log.Status.Error);
                }            
        }
        #endregion
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            prgBar.Value = e.ProgressPercentage;

            System.Windows.Forms.Application.DoEvents();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {

                lblStatus.Text = "The task has been cancelled...";
            }
            else if (e.Error != null)
            {
                lblStatus.Text = "Error in creating tables...";
            }
            else
            {
                lblStatus.Text = "Tables got Created...";
                Thread.Sleep(1000);
                if (!backgroundWorker2.IsBusy)
                {
                    prgBar.Minimum = 0;

                    lblStatus.Text = "Configuring Default values...";
                    beGetDefaultValues();
                    prgBar.Maximum = ObjTable.Children<JToken>().Count();
                    prgBar.Visible = true;
                    btnCreate.Enabled = false;
                    btnCancel.Enabled = true;
                    backgroundWorker2.RunWorkerAsync();

                }

            }

        }
        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgBar.Value = e.ProgressPercentage;
            System.Windows.Forms.Application.DoEvents();
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                opGetUserDetails();
                Log.LogData("GetUserDetails Completed", Log.Status.Debug);
                DataTable dtUsers = new DataTable();
                dtUsers = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(respJson), (typeof(DataTable)));
                opBulkCopy(dtUsers, "tb_Users");
                opUpdateUserPassword();
                Log.LogData("opUpdateUserPassword Completed", Log.Status.Debug);
                opSaveMenuDetails();
                Log.LogData("opSaveMenuDetails Completed", Log.Status.Debug);
                opSaveModuleDetails();
                Log.LogData("opSaveModuleDetails Completed", Log.Status.Debug);
                beGetDefaultValues();
                Log.LogData("beGetDefaultValues Completed", Log.Status.Debug);
                int j = 0;
                ObjTable = respJson["tableInfo"];

                foreach (var info in ObjTable.Children<JToken>())
                {
                    tableName = ((JProperty)info).Name;
                    dt = new DataTable();
                    dt = toDataTable(((JProperty)info).Value.ToObject<JArray>());

                    DataTable formatDtble = dt.Clone();
                    if (dt.Rows.Count != 0)
                    {
                        object[] objRow = dt.Rows[0].ItemArray;
                        for (int i = 0; i < objRow.Length; i++)
                        {
                            DateTime date;
                            var val = objRow[i].ToString();
                            if (DateTime.TryParse(val, out date) || val == "0000-00-00 00:00:00")
                            {
                                formatDtble.Columns[i].DataType = typeof(DateTime);
                            }
                        }


                        foreach (DataRow dr in dt.Rows)
                        {
                            object[] objData = dr.ItemArray;
                            for (int i = 0; i < objData.Length; i++)
                            {


                                DateTime date;
                                var val = objData[i].ToString();
                                if (DateTime.TryParse(val, out date) || val == "0000-00-00 00:00:00")
                                {
                                    if (val == "0000-00-00 00:00:00")
                                    {
                                        date = Convert.ToDateTime("1753-01-01 00:00:00");

                                    }
                                    else
                                    {
                                        date = Convert.ToDateTime(val);
                                    }
                                    date = DateTime.ParseExact(date.ToString("dd-MM-yyyy HH:mm:ss"), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                                    objData[i] = date;

                                }
                            }
                            formatDtble.Rows.Add(objData);

                        }
                        opBulkCopy(formatDtble, tableName);
                        Log.LogData("opBulkCopy Completed", Log.Status.Debug);
                    }
                    backgroundWorker2.ReportProgress(j);
                    j++;
                }

            }
            catch(Exception ex)
            {
                Log.LogData("Error in Default Values Configuration: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {

                lblStatus.Text = "The task has been cancelled...";
            }
            else if (e.Error != null)
            {
                lblStatus.Text = "Error in configuring default tables values...";
            }
            else
            {
                lblStatus.Text = "configuration of table's default value completed...";
                Thread.Sleep(1000);
                this.Hide();
                frmMaster masterForm = new frmMaster(userName, RegistryConfig.userImage);
                masterForm.Show();

            }
        }
        #endregion
        public void opUpdateUserPassword()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                {
                    con.Open();
                    var vEncryptedPassword = EncryptionDecryption.SHA1HashStringForUTF8String(RegistryConfig.Password);
                    SqlCommand Cmd = new SqlCommand();
                    Cmd.Connection = con;
                    Cmd.CommandText = "usp_UpadteUserWPassword";
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@UserName", RegistryConfig.UserName);
                    Cmd.Parameters.AddWithValue("@Password", Convert.ToString(vEncryptedPassword));
                    if (isAvailable == true)
                        Cmd.Parameters.AddWithValue("@UserPhoto", ImageToByte(RegistryConfig.userImage));
                    else
                    {
                        Cmd.Parameters.Add("@UserPhoto", SqlDbType.VarBinary, -1);

                        Cmd.Parameters["@UserPhoto"].Value = DBNull.Value;
                    }
                    Cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Updating User WPassword: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        public void beGetDefaultValues()
        {
            Application.DoEvents();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Url"] + "/particulartableval?dbName=" + newDatabase);
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var theResponseStream = new StreamReader(response.GetResponseStream());
                        string result = theResponseStream.ReadToEnd();
                        respJson = JsonConvert.DeserializeObject<dynamic>(result);


                    }

                }
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Default Values: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
        }
        public void frmReadPNG(string avatar)
        {
            if (avatar != null && avatar != "")
            {
                RegistryConfig.opGetUserPhoto(avatar);
                isAvailable = true;
            }
            else
            {
                try
                {
                    RegistryConfig.userImage = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.UserCircle)
                    { ForeColor = SystemColors.ButtonFace, Size = 80 });
                }
                catch (Exception ex)
                {
                    RegistryConfig.userImage = Properties.Resources.icon_profile;
                    Log.LogData("Error in Loading FontAwesome icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                }
                isAvailable = false;
            }
            pbAvatar.Image = RegistryConfig.userImage;
        }




        private void txtServer_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            timerStatus.Stop();
            if (txtServer.Text.Trim() == "Server")
            {
                txtServer.Text = "";

            }
        }
        private void lblSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            txtPwd.PasswordChar = '*';
            lblStatus.Text = "";
            timerStatus.Stop();
            if (txtPwd.Text.Trim() == "Password")
            {
                txtPwd.Text = "";

            }
        }
        private void txtUserID_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            timerStatus.Stop();
            if (txtUserID.Text.Trim() == "User ID")
            {
                txtUserID.Text = "";

            }
        }
        private void txtPwd_Click(object sender, EventArgs e)
        {
            txtPwd.PasswordChar = '*';
            lblStatus.Text = "";
            timerStatus.Stop();
            if (txtPwd.Text.Trim() == "Password")
            {
                txtPwd.Text = "";

            }
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
        private void opGetDashbordMenu()
        {

            Application.DoEvents();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Url"] + "/menus?dbName=" + newDatabase);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 300000;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var theResponseStream = new StreamReader(response.GetResponseStream());
                        string result = theResponseStream.ReadToEnd();
                        respJson = JsonConvert.DeserializeObject<dynamic>(result);
                        respJson = respJson["dashboard"];
                    }

                }
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Dashboard Menus: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }


        }
        private void opGetUserDetails()
        {

            Application.DoEvents();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Url"] + "/tbusers?dbName=" + newDatabase);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 300000;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var theResponseStream = new StreamReader(response.GetResponseStream());
                        string result = theResponseStream.ReadToEnd();
                        respJson = JsonConvert.DeserializeObject<dynamic>(result);
                        respJson = respJson["tableInfo"];
                    }

                }
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Dashboard Menus: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }


        }

        private void opBulkCopy(DataTable formatDtble, string tableName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                {
                    using (var bulkCopy = new SqlBulkCopy(con))
                    {
                        con.Open();
                        // my DataTable column names match my SQL Column names, so I simply made this loop.
                        //However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                        foreach (DataColumn col in formatDtble.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.WriteToServer(formatDtble);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Bulk Copy: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
        }
        private void opSaveMenuDetails()

        {
            opGetDashbordMenu();

            try
            { 

            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
                    foreach (var obj in respJson)
                    {
                        SqlCommand Cmd = new SqlCommand();
                        Cmd.Connection = con;
                        Cmd.CommandText = "usp_InsertMenu";
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("@id", obj["id"].Value);
                        Cmd.Parameters.AddWithValue("@name", obj["name"].Value);
                        Cmd.Parameters.AddWithValue("@logoClass", obj["logoClass"].Value);
                        Cmd.Parameters.AddWithValue("@logo", obj["logo"].Value);
                        Cmd.Parameters.AddWithValue("@backgroundColor", obj["backgroundColor"].Value);
                        Cmd.Parameters.AddWithValue("@parentId", obj["parentId"].Value);
                        Cmd.Parameters.AddWithValue("@moduleName", obj["moduleName"].Value);
                        Cmd.Parameters.AddWithValue("@moduleId", obj["moduleId"].Value);
                        Cmd.Parameters.AddWithValue("@position", obj["position"].Value);
                        Cmd.Parameters.AddWithValue("@ordering", obj["ordering"].Value == null ? 0 : obj["ordering"].Value);
                        var vOpCode = Cmd.Parameters.Add("@OpCode", SqlDbType.SmallInt);
                        vOpCode.Direction = ParameterDirection.Output;
                        var vResults = Cmd.ExecuteNonQuery();
                        Cmd.Dispose();
                        Cmd = null;

                    }
                }
            }
            catch(Exception ex)
            {
                Log.LogData("Error in Inserting Menu: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }

        }
        public void opSaveModuleDetails()

        {
            try { 
            List<moduleDetails> oModuleDetails = new List<moduleDetails>();
            oModuleDetails = opGetModuleDetails();
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
                    foreach (var obj in oModuleDetails)
                    {
                        SqlCommand Cmd = new SqlCommand();
                        Cmd.Connection = con;
                        Cmd.CommandText = "usp_InsertModuleDetails";
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("@ModuleId", obj.module_id);
                        Cmd.Parameters.AddWithValue("@ModuleName", obj.module_name);
                        Cmd.Parameters.AddWithValue("@ModuleTitle", obj.module_title);
                        Cmd.Parameters.AddWithValue("@ModuleNote", obj.module_note);
                        Cmd.Parameters.AddWithValue("@ModuleDB", obj.module_db);
                        Cmd.Parameters.AddWithValue("@ModuleDBKey", obj.module_db_key);
                        Cmd.Parameters.AddWithValue("@ModuleType", obj.module_type);
                        Cmd.Parameters.AddWithValue("@ModuleDivision", obj.module_division);
                        Cmd.Parameters.AddWithValue("@ModuleAccessServer", obj.module_access_server);
                        Cmd.Parameters.AddWithValue("@ModuleForm", JsonConvert.SerializeObject(obj.forms));
                        Cmd.Parameters.AddWithValue("@ModuleGrid", JsonConvert.SerializeObject(obj.grid));
                        Cmd.Parameters.AddWithValue("@SqlSelect", obj.sql_select == null ? "" : obj.sql_select);
                        Cmd.Parameters.AddWithValue("@SqlWhere", obj.sql_where == null ? "" : obj.sql_where);
                        Cmd.Parameters.AddWithValue("@SqlGroup", obj.sql_group == null ? "" : obj.sql_group);
                        var vOpCode = Cmd.Parameters.Add("@OpCode", SqlDbType.SmallInt);
                        vOpCode.Direction = ParameterDirection.Output;
                        var vResults = Cmd.ExecuteNonQuery();
                        Cmd.Dispose();
                        Cmd = null;

                    }
                }
            }
            catch(Exception ex)
            {
                Log.LogData("Error in Inserting Module Details: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
        }
        private List<moduleDetails> opGetModuleDetails()
        {
            List<moduleDetails> oModuleDetails = new List<moduleDetails>();
            Application.DoEvents();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["Url"] + "/dbmoduledetailinfo?dbName=" + newDatabase);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 300000;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var theResponseStream = new StreamReader(response.GetResponseStream());
                        string result = theResponseStream.ReadToEnd();
                        respJson = JsonConvert.DeserializeObject<dynamic>(result);
                        // oModuleDetails = respJson["module"];
                        oModuleDetails = JsonConvert.DeserializeObject<List<moduleDetails>>(JsonConvert.SerializeObject(respJson["module"]));
                    }

                }
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Module Deatils: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
            return oModuleDetails;
        }

    }


    public class moduleDetails
    {
        public string module_id { get; set; }
        public string module_name { get; set; }
        public string module_title { get; set; }
        public string module_note { get; set; }
        public string module_db { get; set; }
        public string module_db_key { get; set; }
        public string module_type { get; set; }
        public string module_division { get; set; }
        public string module_access_server { get; set; }
        public JArray forms { get; set; }
        public JArray grid { get; set; }
        public string sql_select { get; set; }
        public string sql_where { get; set; }
        public string sql_group { get; set; }
    }
}


