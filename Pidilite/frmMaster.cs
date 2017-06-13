using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Pidilite
{
    public partial class frmMaster : Form
    {
        #region [Data Members]
        FlowLayoutPanel oExisting = null;
        string panelDetail = string.Empty, clickedBtn = string.Empty, table = string.Empty, subGridTable = string.Empty, primaryKey = string.Empty;
        FontAwesome.Type type;
        Control ctrl = new Control();
        Label lbl = new Label();
        CirclePictureBox pbUpload = new CirclePictureBox();
        JArray jForm = null;
        public dynamic respJson { get; set; }
        Dictionary<string, JObject> linkDic = null;
        Dictionary<string, JArray> calcDic = null;
        Dictionary<string, JArray> subCalcDic = null;
        Dictionary<string, Object> hiddenField = null;
        Dictionary<string, Object> formulaHiddenField = null;
        Dictionary<string, string> subGridLink = null;
        List<string> gstCal = null;
        List<string> requiredFields = null;
        List<LinkDetails> linkPk = null;
        DataTable dtSave;
        DataSet dtSubGrid;
        int count = 0, rowIndexVal = 0;
        Button btnCreate;

        #endregion

        #region[Constructor]
        public frmMaster(string userName, Bitmap userImage)
        {
            Log.LogData("Form Master", Log.Status.Debug);
            InitializeComponent();
            lblUserName.Text = userName;
            lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            pbAvatar.Image = userImage;
            pbAvatar.BackColor = pnlCentre.BackColor;
            try
            {
                btnModule.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Refresh)
                { ForeColor = Color.White, Size = 16 });
            }

            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnModule.Image = Properties.Resources.icon_Refresh;
            }
            opPreparemenu();
        }
        #endregion

        #region[Form Load]
        private void frmMaster_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            Left = Top = 0;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            WindowState = FormWindowState.Maximized;
        }
        #endregion

        #region[Form Close and Minimize]
        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void lblMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void lblMinimize_MouseEnter(object sender, EventArgs e)
        {
            lblMinimize.BackColor = Color.RoyalBlue;
        }
        private void lblMinimize_MouseLeave(object sender, EventArgs e)
        {
            lblMinimize.BackColor = Color.LightSkyBlue;
        }
        #endregion

        #region [Main Menu]
        public List<menuDetails> opGetMenubyParentId(short parentId)

        {
            List<menuDetails> oReturn = new List<menuDetails>();
            try
            {
                //RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
                using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                {
                    con.Open();
                    using (DataContext dx = new DataContext(con))
                    {
                        string tQuery = "usp_GetMenubyParent {0}";
                        var results = dx.ExecuteQuery<menuDetails>(tQuery, parentId);
                        oReturn = results.ToList<menuDetails>();
                    }
                }

            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Parent Menu Id's: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
            return oReturn;
        }
        private void opPreparemenu()
        {
            string logo = string.Empty;
            int K = 1;
            List<menuDetails> omenudetails = new List<menuDetails>();
            omenudetails = opGetMenubyParentId(Convert.ToInt16(0));
            foreach (var omenu in omenudetails)
            {
                FlowLayoutPanel pnlMenu = new FlowLayoutPanel();
                pnlMenu.BackColor = pnlMainMenu.BackColor;
                pnlMenu.AutoSize = true;
                pnlMenu.Margin = new Padding(0);
                if (K == 1)
                {
                    pnlMenu.Top = pnlMainMenu.Top;
                    pnlMenu.Left = pnlMainMenu.Left;
                }
                pnlMenu.Width = pnlMainMenu.Width;
                pnlMenu.Name = "pnlMenu" + K;
                pnlMenu.AutoSize = true;
                pnlMenu.Margin = new Padding(0);
                pnlMainMenu.Controls.Add(pnlMenu);
                pnlMenu.Height = 35;
                Button btnMenu = new Button();
                btnMenu.BackColor = pnlMainMenu.BackColor;
                btnMenu.ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153)));
                btnMenu.Height = 35;
                btnMenu.Width = 146;

                btnMenu.Font = RegistryConfig.myFontBold;
                btnMenu.Top = pnlMainMenu.Top;
                btnMenu.Left = pnlMainMenu.Left;
                btnMenu.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                logo = omenu.logoClass;
                logo = logo.Replace("fa-", "");
                logo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(logo);
                logo = logo.Replace("-", "");
                try
                {
                    type = new FontAwesome.Type();
                    type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), logo);
                    btnMenu.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                    { ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153))), Size = 18 });
                }
                catch (Exception ex)
                {
                    Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                    btnMenu.Image = Properties.Resources.icon_pencil;
                }
                btnMenu.Tag = logo;
                btnMenu.ImageAlign = ContentAlignment.TopLeft;
                btnMenu.TextAlign = ContentAlignment.MiddleLeft;
                btnMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnMenu.UseMnemonic = false;
                btnMenu.UseVisualStyleBackColor = false;
                btnMenu.FlatAppearance.BorderSize = 0;
                btnMenu.Name = "  " + omenu.id.ToString();
                btnMenu.FlatStyle = FlatStyle.Flat;
                btnMenu.Text = "  " + omenu.name;
                btnMenu.Margin = new Padding(0);
                btnMenu.Click += new EventHandler(btnMenu_Click);
                btnMenu.MouseEnter += new EventHandler(btnMenu_MouseEnter);
                btnMenu.MouseLeave += new EventHandler(btnMenu_MouseLeave);
                PictureBox pb = new PictureBox();
                pb.Width = 42;
                pb.BackColor = pnlMainMenu.BackColor;
                pb.Image = Properties.Resources.icon_plus;
                pb.Height = btnMenu.Height;
                pb.Margin = new Padding(0);
                pnlMenu.Controls.Add(btnMenu);
                pnlMenu.Controls.Add(pb);
                K++;

            }


        }
        private void btnMenu_MouseLeave(object sender, EventArgs e)
        {
            FlowLayoutPanel oParent = new FlowLayoutPanel();

            var button = (Button)sender;
            if (clickedBtn != button.Name)
            {
                oParent = (button.Parent as FlowLayoutPanel);
                try
                {
                    type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), button.Tag.ToString());
                    button.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                    { ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153))), Size = 18 });
                }
                catch (Exception ex)
                {
                    Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                    button.Image = Properties.Resources.icon_pencil;
                }

                button.BackColor = Color.FromArgb(((byte)(24)), ((byte)(40)), ((byte)(56)));
                button.ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153)));
                PictureBox pb1 = new PictureBox();
                pb1 = oParent.Controls.OfType<PictureBox>().ToList()[0];
                pb1.BackColor = button.BackColor;
                pb1.Image = Properties.Resources.icon_plus;
            }

        }
        private void btnMenu_MouseEnter(object sender, EventArgs e)
        {
            FlowLayoutPanel oParent = new FlowLayoutPanel();
            var button = (Button)sender;
            if (clickedBtn != button.Name)
            {
                try
                {
                    type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), button.Tag.ToString());
                    button.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                    { ForeColor = Color.White, Size = 18 });
                }
                catch (Exception ex)
                {
                    Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                    button.Image = Properties.Resources.icon_pencil;
                }


                oParent = (button.Parent as FlowLayoutPanel);
                button.ForeColor = Color.White;
                button.BackColor = Color.FromArgb(((byte)(17)), ((byte)(30)), ((byte)(43)));
                PictureBox pb1 = new PictureBox();
                pb1 = oParent.Controls.OfType<PictureBox>().ToList()[0];
                pb1.BackColor = button.BackColor;
                pb1.Image = Properties.Resources.icon_plus_white;

            }
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            int count = 0;
            var button = (Button)sender;
            oExisting = new FlowLayoutPanel();
            oExisting = (button.Parent as FlowLayoutPanel);

            count = oExisting.Controls.Count;
            if (panelDetail != string.Empty)
            {
                oExisting = new FlowLayoutPanel();
                oExisting = pnlMainMenu.Controls[panelDetail] as FlowLayoutPanel;
                List<Button> itemsToRemove = new List<Button>();
                foreach (Button btn in oExisting.Controls.OfType<Button>())
                {
                    if (btn.Height != 35)
                    {
                        itemsToRemove.Add(btn);
                    }
                    else
                    {
                        try
                        {
                            type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), btn.Tag.ToString());
                            btn.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                            { ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153))), Size = 18 });
                        }
                        catch (Exception ex)
                        {
                            Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                            button.Image = Properties.Resources.icon_pencil;
                        }

                        btn.BackColor = Color.FromArgb(((byte)(24)), ((byte)(40)), ((byte)(56)));
                        btn.ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153)));
                        PictureBox pb2 = new PictureBox();
                        pb2 = oExisting.Controls.OfType<PictureBox>().ToList()[0];
                        pb2.BackColor = btn.BackColor;
                        pb2.Image = Properties.Resources.icon_plus;

                    }

                }

                foreach (Button bttn in itemsToRemove)
                {
                    oExisting.Controls.Remove(bttn);
                    bttn.Dispose();
                }

            }
            panelDetail = button.Parent.Name;
            if (oExisting == null || ((oExisting != null) && count == 2))
            {

                clickedBtn = button.Name;
                List<menuDetails> omenudetails = new List<menuDetails>();
                omenudetails = opGetMenubyParentId(Convert.ToInt16(button.Name));
                oExisting = new FlowLayoutPanel();
                oExisting = (button.Parent as FlowLayoutPanel);
                opGenerateSubMenus(oExisting, omenudetails);
                oExisting.BackColor = Color.FromArgb(((byte)(17)), ((byte)(30)), ((byte)(43)));
                PictureBox pb1 = new PictureBox();
                pb1 = oExisting.Controls.OfType<PictureBox>().ToList()[0];
                pb1.BackColor = oExisting.BackColor;
                button.ForeColor = Color.White;

                try
                {
                    type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), button.Tag.ToString());
                    button.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                    { ForeColor = Color.White, Size = 18 });
                }
                catch (Exception ex)
                {
                    Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                    button.Image = Properties.Resources.icon_pencil;
                }
                button.BackColor = oExisting.BackColor;
                pb1.Image = Properties.Resources.icon_minus;

            }


        }
        #endregion

        #region[Sub Menu(Module Master)]
        private void opGenerateSubMenus(FlowLayoutPanel pnlMenu, List<menuDetails> omenudetails)
        {

            string logo = string.Empty;
            List<menuDetails> subMenu = omenudetails.Where(x => x.parentId != 0).ToList();
            subMenu = subMenu.OrderBy(x => x.ordering).ToList();
            foreach (var sub in subMenu)
            {
                Button btnSubMenu = new Button();
                btnSubMenu.BackColor = pnlMainMenu.BackColor;
                btnSubMenu.ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153)));
                btnSubMenu.Height = 28;

                btnSubMenu.Font = RegistryConfig.myFont;
                btnSubMenu.Width = pnlMainMenu.Width;
                btnSubMenu.Top = pnlMainMenu.Top;
                btnSubMenu.Left = pnlMainMenu.Left;
                btnSubMenu.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                if (sub.logoClass != "")
                {
                    logo = sub.logoClass;
                    logo = logo.Replace("fa-", "");
                    logo = logo.ToLower().Replace("(alias)", "").Trim();
                    logo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(logo);
                    logo = logo.Replace("-", "");
                    btnSubMenu.Tag = logo;
                    try
                    {
                        type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), logo);
                        btnSubMenu.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                        { ForeColor = Color.White, Size = 15 });
                    }
                    catch (Exception ex)
                    {
                        Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                        btnSubMenu.Image = Properties.Resources.icon_pencil;
                    }
                }
                else
                {
                    btnSubMenu.Image = Properties.Resources.icon_pencil;
                }
                btnSubMenu.Image = Properties.Resources.line_dotted;
                btnSubMenu.ImageAlign = ContentAlignment.MiddleLeft;
                btnSubMenu.TextAlign = ContentAlignment.BottomLeft;
                btnSubMenu.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnSubMenu.FlatAppearance.BorderSize = 0;
                btnSubMenu.Margin = new Padding(0);
                btnSubMenu.Name = sub.id.ToString();
                btnSubMenu.FlatStyle = FlatStyle.Flat;
                btnSubMenu.Text = sub.name;
                btnSubMenu.Click += new EventHandler(btnSubMenu_Click);
                pnlMenu.Controls.Add(btnSubMenu);

            }
            oExisting = pnlMenu;
        }
        private void btnSubMenu_Click(object sender, EventArgs e)
        {
            string logo = string.Empty;
            var button = (Button)sender;

            if (flPnlData.Controls.Count != 0)
            {
                List<Panel> itemsToRemove = new List<Panel>();
                foreach (Panel pnl in flPnlData.Controls.OfType<Panel>())
                {
                    itemsToRemove.Add(pnl);
                }
                foreach (Panel panel in itemsToRemove)
                {
                    pnlData.Controls.Remove(panel);
                    panel.Dispose();
                }
            }

            int k = 1;
            if (button.Tag != null && Convert.ToString(button.Tag) != "")
            {


                try
                {
                    type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), button.Tag.ToString());
                    pbBreadCrumb.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                    { ForeColor = SystemColors.ControlDarkDark, Size = 30 });
                }
                catch (Exception ex)
                {
                    Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                    pbBreadCrumb.Image = Properties.Resources.icon_pbPencil;
                }
            }
            lblBreadCrumb.Text = button.Text.ToUpper();
            lblBreadCrumb.Font = RegistryConfig.myBCFont;
            lblBreadCrumb.ForeColor = SystemColors.ControlDarkDark;
            List<menuDetails> oSubmenuDetails = new List<menuDetails>();
            oSubmenuDetails = opGetMenubyParentId(Convert.ToInt16(button.Name));
            foreach (var sub in oSubmenuDetails)
            {
                FlowLayoutPanel fpnlBox = new FlowLayoutPanel();
                fpnlBox.FlowDirection = FlowDirection.TopDown;
                fpnlBox.AutoSize = true;
                Label lblMenu = new Label();
                Panel pnlBox = new Panel();
                lblMenu.Text = sub.name;
                lblMenu.Font = RegistryConfig.myFont;
                lblMenu.AutoSize = true;
                lblMenu.Margin = new Padding(60, 0, 60, 0);
                pnlBox.Name = "pnlBox" + k;
                pnlBox.Size = new Size(75, 75);
                if (sub.backgroundColor != "")
                    pnlBox.BackColor = ColorTranslator.FromHtml(sub.backgroundColor);
                else
                    pnlBox.BackColor = Color.Gold;
                PictureBox picBoxView = new PictureBox();
                picBoxView.SizeMode = PictureBoxSizeMode.AutoSize;
                picBoxView.Anchor = AnchorStyles.None;
                pnlBox.Tag = sub.moduleId;
                picBoxView.Tag = sub.moduleId;
                pnlBox.Click += new EventHandler(pnlBox_Click);
                picBoxView.Click += new EventHandler(picBoxView_Click);
                pnlBox.Controls.Add(picBoxView);
                if (sub.logoClass != "")
                {
                    logo = sub.logoClass;
                    logo = logo.Replace("fa-", "");
                    logo = logo.ToLower().Replace("(alias)", "").Trim();
                    logo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(logo);
                    logo = logo.Replace("-", "");
                    pnlBox.Tag = logo;
                    try
                    {
                        type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), logo);
                        Bitmap bt = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                        { ForeColor = Color.White, Size = 30 });
                        CenterPictureBox(picBoxView, bt);
                    }
                    catch (Exception ex)
                    {
                        Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                        CenterPictureBox(picBoxView, Properties.Resources.icon_Cubes);
                    }
                }
                else
                {
                    CenterPictureBox(picBoxView, Properties.Resources.icon_Cubes);
                }
                pnlBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                pnlBox.Margin = new Padding(60, 25, 60, 5);
                fpnlBox.Margin = new Padding(0, 0, 0, 60);
                fpnlBox.Controls.Add(pnlBox);
                fpnlBox.Controls.Add(lblMenu);
                flPnlData.Controls.Add(fpnlBox);
                k++;
            }

        }
        private void picBoxView_Click(object sender, EventArgs e)
        {

            if (flPnlData.Controls.Count != 0)
            {

                List<Panel> itemsToRemove = new List<Panel>();
                foreach (Panel pnl in flPnlData.Controls.OfType<Panel>())
                {
                    itemsToRemove.Add(pnl);
                }
                foreach (Panel panel in itemsToRemove)
                {
                    pnlData.Controls.Remove(panel);
                    panel.Dispose();
                }
            }
            var pb = (PictureBox)sender;
            moduleValues oValues = new moduleValues();
            oValues = opGetModuleDetailsByID(Convert.ToInt16(pb.Tag), false);
            lblBreadCrumb.Text = oValues.Title;
            lblBreadCrumb.Font = RegistryConfig.myBCFont;
            lblBreadCrumb.ForeColor = SystemColors.ControlDarkDark;
            Panel oPanel = new Panel();
            oPanel.Width = pnlBreadCrumb.Width - 40;
            oPanel.Height = pnlBreadCrumb.Height + 20;
            oPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            flPnlData.Controls.Add(oPanel);
            List<girdDetails> girdDetails = opCreatingSearchPanel(oValues, oPanel);
            opCreatingActionPanel(pb, oPanel);
            opConstructingGrid(oValues, oPanel, girdDetails);
        }
        private void pnlBox_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void CenterPictureBox(PictureBox picBox, Bitmap picImage)
        {
            picBox.Image = picImage;
            picBox.Location = new Point((picBox.Parent.ClientSize.Width / 2) - (picImage.Width / 2),
                                        (picBox.Parent.ClientSize.Height / 2) - (picImage.Height / 2));
            picBox.Refresh();
        }
        #endregion

        #region[ Module Grid]
        private void opConstructingGrid(moduleValues oValues, Panel oPanel, List<girdDetails> girdDetails)
        {
            string columnBuild = "Select ";
            foreach (var obj in girdDetails)
            {
                if (obj.view == true)
                {
                    if (Convert.ToString(obj.conn["valid"]) == "0")
                    {
                        if (Convert.ToBoolean(obj.attribute["image"]["active"]) == false)
                            columnBuild = columnBuild + Convert.ToString(obj.field) + " as '" + Convert.ToString(obj.label) + "', ";
                    }
                    else
                    {
                        columnBuild = columnBuild + "(Isnull((select " + Convert.ToString(obj.conn["display"]) + " from " + Convert.ToString(obj.conn["db"]) + " where " + Convert.ToString(obj.conn["key"]) + " = " + Convert.ToString(obj.field) + "),'')) as '" + Convert.ToString(obj.label) + "', ";
                    }
                }
            }
            columnBuild = columnBuild.TrimEnd(',', ' ');
            columnBuild = columnBuild.TrimStart(',', ' '); ;
            columnBuild = columnBuild + " from " + Convert.ToString(oValues.TableName);
            Panel oPnlGrid = new Panel();
            oPnlGrid.Width = oPanel.Width;
            oPnlGrid.Height = 610;
            flPnlData.Controls.Add(oPnlGrid);
            DataTable dt = new DataTable();
            dt = opGridDataByModule(columnBuild);
            DataGridView oDataGrid = new DataGridView();
            oDataGrid.Height = oPnlGrid.Height - 20;
            oDataGrid.Width = oPnlGrid.Width;
            oDataGrid.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
            oPnlGrid.Controls.Add(oDataGrid);
            oDataGrid.DataSource = dt;
            //Add a CheckBox Column to the DataGridView at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Name = "checkBoxColumn";
            oDataGrid.Columns.Insert(0, checkBoxColumn);
            oDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            checkBoxColumn.Width = 60;
            oDataGrid.EnableHeadersVisualStyles = true;
            oDataGrid.ColumnHeadersHeight = 40;
            oDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
        }

        public DataTable opGridDataByModule(string queryString)
        {
            DataTable oGridData = new DataTable();
            //RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = queryString;
                    SqlDataAdapter sqlda = new SqlDataAdapter();
                    cmd.CommandType = CommandType.Text;
                    sqlda = new SqlDataAdapter(cmd);
                    sqlda.Fill(oGridData);
                }
            }
            return oGridData;
        }
        #endregion

        #region [Action Panel]
        private void opCreatingActionPanel(PictureBox pb, Panel oPanel)
        {
            FlowLayoutPanel oPnlAction = new FlowLayoutPanel();
            //  oPnlAction.Margin = new Padding(250, 3, 10, 3);
            oPnlAction.Dock = DockStyle.Right;
            oPnlAction.Width = 230;
            oPnlAction.AutoSize = false;
            oPnlAction.Height = pnlBreadCrumb.Height;
            oPnlAction.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            oPnlAction.Dock = DockStyle.Right;
            oPanel.Controls.Add(oPnlAction);
            Button btnSearch = new Button();
            btnSearch.Name = "btnSearch";
            btnSearch.Height = 26;
            btnSearch.Width = 40;
            btnSearch.BackColor = Color.FromArgb(((byte)(181)), ((byte)(94)), ((byte)(0)));


            try
            {
                btnSearch.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Search)
                { ForeColor = Color.White, Size = 18 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnSearch.Image = Properties.Resources.icon_Search;
            }
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ImageAlign = ContentAlignment.TopCenter;
            btnSearch.Margin = new Padding(10, 3, 0, 0);
            btnSearch.Click += new EventHandler(btnSearch_Click);
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 3000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(btnSearch, "Advance Search");
            oPnlAction.Controls.Add(btnSearch);
            Button btnDownLoad = new Button();
            btnDownLoad.Name = "btnDownLoad";
            btnDownLoad.Height = 26;
            btnDownLoad.Width = 40;
            btnDownLoad.BackColor = Color.FromArgb(((byte)(2)), ((byte)(154)), ((byte)(207)));


            try
            {
                btnDownLoad.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Download)
                { ForeColor = Color.White, Size = 18 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnDownLoad.Image = Properties.Resources.icon_download;
            }
            btnDownLoad.FlatAppearance.BorderSize = 0;
            btnDownLoad.FlatStyle = FlatStyle.Flat;
            btnDownLoad.ImageAlign = ContentAlignment.TopCenter;
            btnDownLoad.Margin = new Padding(10, 3, 0, 0);
            btnDownLoad.Click += new EventHandler(btnDownLoad_Click);
            ToolTip toolTip2 = new ToolTip();
            toolTip2.AutoPopDelay = 3000;
            toolTip2.InitialDelay = 500;
            toolTip2.ReshowDelay = 500;
            toolTip2.ShowAlways = true;
            toolTip2.SetToolTip(btnDownLoad, "Download");
            oPnlAction.Controls.Add(btnDownLoad);
            Button btnDelete = new Button();
            btnDelete.Margin = new Padding(10, 3, 0, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Height = 26;
            btnDelete.Width = 40;
            btnDelete.BackColor = Color.FromArgb(((byte)(217)), ((byte)(35)), ((byte)(15)));

            try
            {
                btnDelete.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.MinusCircle)
                { ForeColor = Color.White, Size = 18 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnDelete.Image = Properties.Resources.icon_minus_Circle;
            }
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ImageAlign = ContentAlignment.TopCenter;
            btnDelete.Click += new EventHandler(btnDelete_Click);
            ToolTip toolTip3 = new ToolTip();
            toolTip3.AutoPopDelay = 3000;
            toolTip3.InitialDelay = 500;
            toolTip3.ReshowDelay = 500;
            toolTip3.ShowAlways = true;
            toolTip3.SetToolTip(btnDelete, "Remove");
            oPnlAction.Controls.Add(btnDelete);
            Button btnCreate = new Button();
            btnCreate.Name = "btnCreate";
            btnCreate.Height = 26;
            btnCreate.Width = 40;
            btnCreate.BackColor = Color.FromArgb(((byte)(70)), ((byte)(148)), ((byte)(8)));

            try
            {
                btnCreate.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.PlusCircle)
                { ForeColor = Color.White, Size = 18 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnCreate.Image = Properties.Resources.icon_PlusCircle;
            }
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.ImageAlign = ContentAlignment.TopCenter;
            btnCreate.Tag = pb.Tag;
            btnCreate.Margin = new Padding(10, 3, 0, 0);
            btnCreate.Click += new EventHandler(btnCreate_Click);
            ToolTip toolTip4 = new ToolTip();
            toolTip4.AutoPopDelay = 3000;
            toolTip4.InitialDelay = 500;
            toolTip4.ReshowDelay = 500;
            toolTip4.ShowAlways = true;
            toolTip4.SetToolTip(btnCreate, "Create");
            oPnlAction.Controls.Add(btnCreate);
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [Form Create]
        private void btnCreate_Click(object sender, EventArgs e)
        {
            dtSave = new DataTable();
            dtSubGrid = new DataSet();
            DataColumn dtColumn = new DataColumn();
            dtColumn.ColumnName = "Field";
            dtColumn.MaxLength = int.MaxValue;
            dtColumn.DataType = typeof(string);
            dtSave.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.ColumnName = "Value";
            dtColumn.MaxLength = int.MaxValue;
            dtColumn.DataType = typeof(string);
            dtSave.Columns.Add(dtColumn);

            gstCal = new List<string>();
            linkDic = new Dictionary<string, JObject>();
            calcDic = new Dictionary<string, JArray>();
            subCalcDic = new Dictionary<string, JArray>();
            subGridLink = new Dictionary<string, string>();
            requiredFields = new List<string>();
            linkPk = new List<LinkDetails>();
            gstCal.Add("nxt_cmn_igst");
            gstCal.Add("nxt_cmn_cgst");
            gstCal.Add("nxt_cmn_sgst");
            var btn = (Button)sender;
            Panel oParentPnl = new Panel();
            oParentPnl = (btn.Parent.Parent.Parent as Panel);
            oParentPnl.Controls.Clear();
            oParentPnl.AutoScroll = true;
            oParentPnl.MouseWheel += new MouseEventHandler(oParentPnl_MouseWheel);
            moduleValues oModuleValues = new moduleValues();
            oModuleValues = opGetModuleDetailsByID(Convert.ToInt16(btn.Tag), true);
            List<formDetails> oFormFields = new List<formDetails>();
            jForm = JsonConvert.DeserializeObject<dynamic>(oModuleValues.Form);
            List<formDetails> fieldDetails = new List<formDetails>();
            table = oModuleValues.TableName;
            primaryKey = oModuleValues.PrimaryKey;
            btnCreate = new Button();
            btnCreate = btn;
            foreach (var obj in jForm)
            {
                fieldDetails = obj["fields"].ToObject<List<formDetails>>().OrderBy(x => x.sortlist).ToList();
                FlowLayoutPanel ofpnl = new FlowLayoutPanel();
                if (Convert.ToString(obj["column"]) == "2")
                {
                    opFormWith2BlockFormation(btn, oParentPnl, fieldDetails, obj);
                }
                else
                {
                    opFormFormation(btn, oParentPnl, fieldDetails, obj, ofpnl, Convert.ToInt16(obj["moduleid"]));
                }
            }
            Panel oPnlCreate = new Panel();
            oPnlCreate.BackColor = Color.White;
            oPnlCreate.Width = oParentPnl.Width - 50;
            oPnlCreate.Height = 100;
            FlowLayoutPanel ofnlCreate = new FlowLayoutPanel();
            ofnlCreate.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            ofnlCreate.Location = new Point((oPnlCreate.Bounds.Width - 10) - ofnlCreate.Width, 0);
            ofnlCreate.FlowDirection = FlowDirection.LeftToRight;
            ofnlCreate.AutoSize = true;
            Button btnSave = new Button();
            btnSave.UseMnemonic = false;
            btnSave.Text = " Save & Continue";
            btnSave.Font = RegistryConfig.myFontBold;
            btnSave.AutoSize = true;
            try
            {
                btnSave.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.CheckCircleO)
                { ForeColor = Color.White, Size = 17 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnSave.Image = Properties.Resources.icon_Check;
            }
            btnSave.TextAlign = ContentAlignment.MiddleCenter;
            btnSave.ImageAlign = ContentAlignment.TopCenter;
            btnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSave.BackColor = SystemColors.HotTrack;
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            ofnlCreate.Controls.Add(btnSave);
            Button btnClose = new Button();
            btnClose.UseMnemonic = false;
            btnClose.Text = " Save & Close";
            btnClose.Click += new EventHandler(btnClose_Click);
            btnClose.Font = RegistryConfig.myFontBold;
            btnClose.AutoSize = true;

            try
            {
                btnClose.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.FloppyO)
                { ForeColor = Color.White, Size = 17 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnClose.Image = Properties.Resources.icon_Floppy;
            }
            btnClose.TextAlign = ContentAlignment.MiddleCenter;
            btnClose.ImageAlign = ContentAlignment.MiddleCenter;
            btnClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClose.BackColor = SystemColors.HotTrack;
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            ofnlCreate.Controls.Add(btnClose);
            Button btnBack = new Button();
            btnBack.UseMnemonic = false;
            btnBack.Text = " Back";
            btnBack.Font = RegistryConfig.myFontBold;
            btnBack.AutoSize = true;
            try
            {
                btnBack.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.ArrowCircleOLeft)
                { ForeColor = Color.White, Size = 17 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnBack.Image = Properties.Resources.icon_ArrowCircle;
            }
            btnBack.TextAlign = ContentAlignment.MiddleCenter;
            btnBack.ImageAlign = ContentAlignment.MiddleCenter;
            btnBack.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBack.BackColor = SystemColors.HotTrack;
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            ofnlCreate.Controls.Add(btnBack);
            oPnlCreate.Controls.Add(ofnlCreate);
            oParentPnl.Controls.Add(oPnlCreate);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            Control[] ctrl = GetAll(btn.Parent.Parent.Parent);
            Int64 MappingId = 0;
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
                SqlTransaction oTransact = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.Transaction = oTransact;
                        cmd.CommandText = "usp_InsertFormData";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = con.ConnectionTimeout;// Setting command timeout for sqlcommand.
                        cmd.Parameters.AddWithValue("@ID", 0);
                        cmd.Parameters.AddWithValue("@UserID", 1);
                        cmd.Parameters.AddWithValue("@PrimaryKey", primaryKey);
                        cmd.Parameters.AddWithValue("@UDTT", dtSave);
                        cmd.Parameters.AddWithValue("@tableName", table);
                        var results = cmd.ExecuteScalar();
                        if (results != null)
                        MappingId = Convert.ToInt64(results);
                        if (Convert.ToInt64(results)<1)
                        {
                            oTransact.Rollback();
                          //  MessageBox.Show("Error in Saving", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Log.LogData("Error in Saving MainForm in DB ", Log.Status.Error);
                        }
                    }

                    opDetailFormSaving(MappingId, oTransact);
                }
                catch (Exception ex)
                {
                    Log.LogData("Error in Saving MainForm : " + ex.Message + ex.StackTrace, Log.Status.Error);
                }
            }
        }
        private void opDetailFormSaving(long masterFormId, SqlTransaction oTransact)
        {
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
                // SqlTransaction oTransact = con.BeginTransaction();
                string mainTablePK = string.Empty,tablePK= string.Empty ;
                try
                {
                    foreach (DataTable dt in dtSubGrid.Tables)
                    {
                        mainTablePK = linkPk.Single(s => s.TableName == dt.TableName).LinkPKId ;
                        tablePK = linkPk.Single(s => s.TableName == dt.TableName).TablePKId ;
                        using (SqlCommand cmd = con.CreateCommand())
                        {

                            cmd.Transaction = oTransact;
                            cmd.CommandText = "usp_InsertDetailFormData";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = con.ConnectionTimeout;// Setting command timeout for sqlcommand.
                            cmd.Parameters.AddWithValue("@MainFormID", masterFormId);
                            cmd.Parameters.AddWithValue("@MainTable", table);
                            cmd.Parameters.AddWithValue("@MainTablePK",mainTablePK);
                            cmd.Parameters.AddWithValue("@UserID", 1);
                            cmd.Parameters.AddWithValue("@UDTT", dt);
                            cmd.Parameters.AddWithValue("@tableName", dt.TableName);
                            cmd.Parameters.AddWithValue("@PrimaryKey", tablePK);
                            var vOpcode = cmd.Parameters.Add("@OPCode", SqlDbType.BigInt);
                            vOpcode.Direction = ParameterDirection.Output;
                            var results = cmd.ExecuteScalar();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.LogData("Error in Saving Detail Forms : " + ex.Message + ex.StackTrace, Log.Status.Error);
                }
            }
        }
        public moduleValues opGetModuleDetailsByID(int moduleId, bool isForm)
        {
            moduleValues oValues = null;
            DataTable dt = new DataTable();
            //RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "usp_GetModuleDetailsByID";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlda = new SqlDataAdapter();
                    cmd.Parameters.AddWithValue("@IsForm", isForm);
                    cmd.Parameters.AddWithValue("@ModuleId", moduleId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        oValues = new moduleValues();
                        while (dr.Read())
                        {
                            oValues.Title = Convert.ToString(dr[0]);
                            oValues.Form = Convert.ToString(dr[1]);
                            oValues.Grid = Convert.ToString(dr[2]);
                            oValues.TableName = Convert.ToString(dr[3]);
                            oValues.PrimaryKey = Convert.ToString(dr[4]);
                        }
                    }
                    dr.Close();
                }
            }
            return oValues;
        }
        private void opFormWith2BlockFormation(Button btn, Panel oParentPnl, List<formDetails> fieldDetails, JToken obj)
        {

            DataRow dr;
            FlowLayoutPanel ofpBack = new FlowLayoutPanel();
            ofpBack.Width = oParentPnl.Width - 40;
            ofpBack.BackColor = SystemColors.ButtonFace;

            oParentPnl.Controls.Add(ofpBack);
            TableLayoutPanel tPanel = new TableLayoutPanel();
            tPanel.ColumnCount = 2;
            tPanel.AutoSize = true;
            if (Convert.ToString(obj["showHeader"]) == "Y")
            {
                Panel oHeader = new Panel();
                oHeader.Width = oParentPnl.Width - 47;
                oHeader.Height = 35;
                oHeader.BackColor = SystemColors.ControlLight;
                oHeader.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                ofpBack.Controls.Add(oHeader);
                Label lblHeader = new Label();
                lblHeader.Font = RegistryConfig.myHeaderFont;
                lblHeader.Text = string.Concat(Convert.ToString(obj["title"]).Select(c => char.IsUpper(c) ? " " + c.ToString() : c.ToString())).TrimStart();
                lblHeader.AutoSize = true;
                oHeader.Controls.Add(lblHeader);
            }
            List<formDetails> valueList = new List<Pidilite.formDetails>();
            List<formDetails> hidddenList = new List<Pidilite.formDetails>();
            valueList = fieldDetails.Where(item => item.type != "hidden" && item.type != "formula_hidden").ToList();
            hidddenList = fieldDetails.Where(item => item.type == "hidden" || item.type == "formula_hidden").ToList();
            fieldDetails = new List<Pidilite.formDetails>();
            fieldDetails = fieldDetails.Concat(valueList)
                                    .Concat(hidddenList)
                                    .ToList();


            int r = 0, r2 = 0;
            foreach (var detail in fieldDetails)
            {

                if (detail.view == "1")
                {
                    lbl = new Label();
                    lbl.Text = detail.label;
                    if (detail.type == "hidden" || detail.type == "formula_hidden")
                    {
                        lbl.Visible = false;
                    }
                    if (detail.required == "required")
                        lbl.Text = lbl.Text + '*';
                    lbl.Font = RegistryConfig.myFont;
                    lbl.AutoSize = true;
                    ctrl = opDynamicControlSelection(btn, pbUpload, ctrl, detail);
                    dr = dtSave.NewRow();
                    dr["Field"] = detail.field;
                    if (detail.type == "text_datetime" || detail.type == "text_date")
                    {
                        dr["Value"] = Convert.ToString(DateTime.MinValue.ToString("dd-MM-yyyy HH:mm:ss"));
                    }
                    else
                    {
                        dr["Value"] = "";
                    }
                    dtSave.Rows.Add(dr);
                    if (detail.label != "")
                    {
                        tPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Convert.ToSingle(ofpBack.Width / 2)));
                    }

                    if (Convert.ToString(obj["title"]) == "cmntotalblock")
                    {
                        if (detail.wblock == "1")
                        {
                            FlowLayoutPanel fpnl = new FlowLayoutPanel();
                            fpnl.AutoSize = true;
                            fpnl.FlowDirection = FlowDirection.RightToLeft;
                            fpnl.Margin = new Padding(0, 0, 10, 0);
                            fpnl.Dock = DockStyle.Right;
                            fpnl.Controls.Add(ctrl);
                            fpnl.Controls.Add(lbl);
                            tPanel.Controls.Add(fpnl, 0, r);
                            r++;
                        }
                        else if (detail.wblock == "2")
                        {
                            FlowLayoutPanel fpnl = new FlowLayoutPanel();
                            fpnl.AutoSize = true;
                            fpnl.FlowDirection = FlowDirection.RightToLeft;
                            fpnl.Margin = new Padding(0, 0, 10, 0);
                            fpnl.Dock = DockStyle.Right;
                            fpnl.Controls.Add(ctrl);
                            fpnl.Controls.Add(lbl);
                            tPanel.Controls.Add(fpnl, 1, r2);
                            r2++;

                        }
                    }
                    else
                    {
                        if (detail.wblock == "1")
                        {
                            if (detail.label != "")
                            {
                                tPanel.Controls.Add(lbl, 0, r);
                                r++;
                            }
                            tPanel.Controls.Add(ctrl, 0, r);
                            r++;
                            if (detail.type == "file")
                            {
                                tPanel.Controls.Add(pbUpload, 0, r);
                                r++;
                            }

                        }
                        else if (detail.wblock == "2")
                        {
                            if (detail.label != "")
                            {
                                tPanel.Controls.Add(lbl, 1, r2);
                                r2++;
                            }
                            tPanel.Controls.Add(ctrl, 1, r2);
                            r2++;
                            if (detail.type == "file")
                            {
                                tPanel.Controls.Add(pbUpload, 1, r2);
                                r2++;
                            }
                        }
                    }

                    if (detail.fieldWidth == "")
                    {
                        if (detail.type == "file")
                        {
                            ctrl.AutoSize = true;
                        }
                        else
                            ctrl.Width = 200;
                    }
                    else
                    {
                        TableLayoutPanelCellPosition pos = tPanel.GetCellPosition(ctrl);
                        ctrl.Width = tPanel.GetColumnWidths()[pos.Column] * Convert.ToInt32(detail.fieldWidth) / 100;
                    }
                }
            }

            ofpBack.Controls.Add(tPanel);
            ofpBack.AutoSize = true;

        }
        private Control opDynamicControlSelection(Button btn, CirclePictureBox pbUpload, Control ctrl, formDetails detail, bool isSubGrid = false)
        {
            switch (Convert.ToString(detail.type))
            {
                case "autoNumber":
                    ctrl = new TextBox();
                    if (detail.wreadonly == true)
                    {
                        (ctrl as TextBox).ReadOnly = true;
                        (ctrl as TextBox).TabStop = false;
                    }
                   (ctrl as TextBox).Font = RegistryConfig.myFont;
                    JObject jAutoNo = detail.auto;
                    int i = 0;
                    DataTable dt = new DataTable();
                    int totaldigit = 0, num;
                    string autoNumber = string.Empty;
                    string[] dateArr = jAutoNo["autoDate"].ToObject<string[]>();
                    if (dateArr.Length == 1)
                    {
                        i = 0;
                    }
                    else if (dateArr.Length > 1)
                    {
                        for (int k = 0; k < dateArr.Length; k++)
                        {

                            if (Convert.ToDateTime(dateArr[k]) < System.DateTime.Now)
                            {
                                i = k;
                            }

                        }
                    }
                    if (dateArr.Length != 0)
                    {

                        string start = jAutoNo["autoFillzero"].ToObject<string[]>()[i];

                        dt = getPreviousAutoNumber(table, detail.field);
                        totaldigit = Convert.ToInt32(jAutoNo["autoDigits"].ToObject<string[]>()[i]);
                        if (dt.Rows.Count == 0)
                        {


                            if (jAutoNo["autoFillzero"].ToObject<string[]>()[i] == "1")
                            {
                                start = (Convert.ToInt32(start).ToString("D" + totaldigit.ToString()));

                            }
                            autoNumber = jAutoNo["autoPrex"].ToObject<string[]>()[i] + start + jAutoNo["autoSufx"].ToObject<string[]>()[i];
                        }
                        else
                        {

                            if (Convert.ToDateTime(dt.Rows[0]["CreatedDate"]) < Convert.ToDateTime(dateArr[i]))
                            {
                                autoNumber = jAutoNo["autoPrex"].ToObject<string[]>()[i] + start + jAutoNo["autoSufx"].ToObject<string[]>()[i];
                            }
                            else
                            {
                                autoNumber = Convert.ToString(dt.Rows[0]["Id"]).Replace(jAutoNo["autoPrex"].ToObject<string[]>()[i], "");
                                autoNumber = autoNumber.Replace(jAutoNo["autoSufx"].ToObject<string[]>()[i], "");
                                if (int.TryParse(autoNumber, out num))
                                {
                                    autoNumber = (Convert.ToInt32(autoNumber) + 1).ToString();
                                    autoNumber = (Convert.ToInt32(autoNumber).ToString("D" + totaldigit.ToString()));
                                    autoNumber = jAutoNo["autoPrex"].ToObject<string[]>()[i] + autoNumber + jAutoNo["autoSufx"].ToObject<string[]>()[i];
                                }
                                else
                                {
                                    autoNumber = jAutoNo["autoPrex"].ToObject<string[]>()[i] + start + jAutoNo["autoSufx"].ToObject<string[]>()[i];
                                }
                            }

                        }
                    }
                    break;
                case "text":
                case "string":
                case "formula_string":
                case "normal_string":
                case "number":
                case "googlelSearch":
                case "hidden":
                case "formula_hidden":

                    ctrl = new TextBox();
                    ctrl.Name = detail.field;
                    (ctrl as TextBox).Font = RegistryConfig.myFont;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as TextBox).ReadOnly = true;
                        (ctrl as TextBox).TabStop = false;
                    }
                    if (detail.type == "hidden" || detail.type == "formula_hidden")
                    {
                        (ctrl as TextBox).Visible = false;
                    }
                    if (detail.calc != null && detail.calc.Count != 0)
                    {
                        (ctrl as TextBox).Text = "0.00";
                        if (!(calcDic.ContainsKey(detail.field)))
                        {
                            calcDic.Add(detail.field, detail.calc);
                            if (isSubGrid == true)
                                subCalcDic.Add(detail.field, detail.calc);
                        }
                        (ctrl as TextBox).TextAlign = HorizontalAlignment.Right;
                        (ctrl as TextBox).ReadOnly = true;
                        (ctrl as TextBox).TabStop = false;

                    }
                    if (detail.type == "formula_hidden" || detail.type == "number" || detail.type == "formula_string")
                    {
                        (ctrl as TextBox).TextAlign = HorizontalAlignment.Right;
                    }
                    (ctrl as TextBox).TextChanged += new EventHandler(ctrl_TextChanged);
                    break;
                case "text_date":
                    ctrl = new DateTimePicker();
                    ctrl.Name = detail.field;
                    (ctrl as DateTimePicker).Format = DateTimePickerFormat.Custom;
                    (ctrl as DateTimePicker).CustomFormat = "dd-MM-yyyy";
                    (ctrl as DateTimePicker).Font = RegistryConfig.myFont;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as DateTimePicker).Enabled = true;
                        (ctrl as DateTimePicker).TabStop = false;
                    }
                    break;
                case "text_datetime":
                    ctrl = new DateTimePicker();
                    ctrl.Name = detail.field;
                    (ctrl as DateTimePicker).Format = DateTimePickerFormat.Custom;
                    (ctrl as DateTimePicker).CustomFormat = "dd-MM-yyyy HH:mm";
                    (ctrl as DateTimePicker).Font = RegistryConfig.myFont;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as DateTimePicker).Enabled = true;
                        (ctrl as DateTimePicker).TabStop = false;
                    }

                    break;
                case "textarea":
                case "textarea_editor":
                    ctrl = new RichTextBox();
                    ctrl.Name = detail.field;
                    (ctrl as RichTextBox).Font = RegistryConfig.myFont;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as RichTextBox).ReadOnly = true;
                        (ctrl as RichTextBox).TabStop = false;
                    }

                    break;
                case "select":
                    ctrl = new ComboBox();
                    ctrl.Tag = btn.Tag;
                    ctrl.Name = detail.field;
                    ctrl.Font = RegistryConfig.myFont;
                    string condition = String.Empty;
                    if (Convert.ToString(detail.option["opt_type"]) == "external")
                    {

                        RegistryConfig.OrgId = 16;
                        condition = Convert.ToString(detail.option["lookup_dependency_key"]).Replace(':', '=');
                        condition = condition.Replace("{user_id}", Convert.ToString(RegistryConfig.userId));
                        condition = condition.Replace("{org_id}", Convert.ToString(RegistryConfig.OrgId));
                        TableDetails oDetails = new TableDetails();
                        oDetails.TableName = Convert.ToString(detail.option["lookup_table"]);
                        oDetails.Key = Convert.ToString(detail.option["lookup_key"]);
                        oDetails.Value = Convert.ToString(detail.option["lookup_value"]);
                        oDetails.Condition = condition;
                        DataTable cbValues = new DataTable();
                        cbValues = comboBoxValues(oDetails);
                        DataRow dr = cbValues.NewRow();
                        dr["Value"] = "--Select--";
                        dr["Id"] = -1;
                        cbValues.Rows.InsertAt(dr, 0);
                        (ctrl as ComboBox).DisplayMember = "Value";
                        (ctrl as ComboBox).ValueMember = "Id";
                        (ctrl as ComboBox).DataSource = cbValues;
                        if (detail.links != null && detail.links.ToString() != "{}")
                        {
                            if (!(linkDic.ContainsKey(detail.field)))
                                linkDic.Add(detail.field, detail.links);
                            (ctrl as ComboBox).SelectedIndexChanged += new EventHandler(ctrl_SelectedIndexChanged);

                        }
                        else
                        {
                            (ctrl as ComboBox).SelectedIndexChanged += new EventHandler(ctrl_SelectedIndexChanged);

                        }

                    }

                    else if (Convert.ToString(detail.option["opt_type"]) == "datalist")
                    {
                        string[] option = Convert.ToString(detail.option["lookup_query"]).Split('|');
                        (ctrl as ComboBox).Items.Add((new Item("--Select--", -1)));
                        foreach (var opt in option)
                        {
                            string[] optionVal = opt.Split(':');
                            (ctrl as ComboBox).Items.Add((new Item(optionVal[1], optionVal[0])));
                        }
                    }
                    else if (Convert.ToString(detail.option["opt_type"]).StartsWith("angular"))
                    {
                        DataTable cbValues = new DataTable();
                        cbValues.Columns.Add("name");
                        cbValues.Columns.Add("id");
                        DataRow dr = cbValues.NewRow();
                        dr["name"] = "--Select--";
                        dr["id"] = -1;
                        cbValues.Rows.InsertAt(dr, 0);
                        (ctrl as ComboBox).DisplayMember = "name";
                        (ctrl as ComboBox).ValueMember = "id";
                        (ctrl as ComboBox).DataSource = cbValues;
                        if (detail.links != null && detail.links.ToString() != "{}")
                        {

                            linkDic.Add(detail.field, detail.links);
                            (ctrl as ComboBox).SelectedIndexChanged += new EventHandler(ctrl_SelectedIndexChanged);

                        }

                        else
                        {
                            (ctrl as ComboBox).SelectedIndexChanged += new EventHandler(ctrl_SelectedIndexChanged);

                        }
                    }

                    (ctrl as ComboBox).DropDownHeight = 100;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as ComboBox).Enabled = false;
                        (ctrl as ComboBox).TabStop = false;
                    }

                    break;
                case "radio":
                    ctrl = new RadioButton();
                    ctrl.Name = detail.field;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as RadioButton).Enabled = false;
                        (ctrl as RadioButton).TabStop = false;
                    }

                    break;
                case "checkbox":
                    ctrl = new CheckBox();
                    ctrl.Name = detail.field;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as CheckBox).Enabled = false;
                        (ctrl as CheckBox).TabStop = false;
                    }

                    break;
                case "file":
                    ctrl = new FlowLayoutPanel();
                    TextBox txt = new TextBox();
                    txt.Width = 150;
                    Button upload = new Button();
                    upload.Text = "Choose";
                    upload.Font = RegistryConfig.myFont;
                    upload.AutoSize = true;
                    pbUpload.Height = 45;
                    pbUpload.Width = 33;
                    pbUpload.BackColor = SystemColors.ButtonFace;
                    try
                    {
                        pbUpload.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Camera)
                        { ForeColor = Color.Gray });
                    }
                    catch (Exception ex)
                    {
                        Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                        pbUpload.Image = Properties.Resources.icon_PlusCircle;
                    }

                    ctrl.Controls.Add(txt);
                    ctrl.Controls.Add(upload);
                    ctrl.Name = detail.field;

                    break;
                case "password":
                    ctrl = new TextBox();
                    (ctrl as TextBox).PasswordChar = '*';
                    ctrl.Name = detail.field;
                    (ctrl as TextBox).Font = RegistryConfig.myFont;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as TextBox).ReadOnly = true;
                        (ctrl as TextBox).TabStop = false;
                    }

                    break;
            }
            if (detail.required == "required")
            {
                requiredFields.Add(detail.field);
            }
            return ctrl;
        }
        private void ctrl_TextChanged(object sender, EventArgs e)
        {
            string equation = string.Empty, value = string.Empty, fields = string.Empty, fValue = string.Empty, keyValue = string.Empty;
            decimal num;
            string[] sFields = null;
            int rowIndex = 0;
            int getVal = 0;
            var key = (TextBox)sender;
            if (key.Text == "")
                key.Text = "0";
            if (key.Parent != null && key.Parent.GetType().Name.ToLower() == "tablelayoutpanel" && ((TableLayoutPanel)key.Parent).ColumnCount > 2)
            {
                var tblpnl = ((TableLayoutPanel)key.Parent);
                rowIndex = tblpnl.GetCellPosition(key).Row;
                if (dtSubGrid.Tables[tblpnl.Name].Select().ToList().Exists(row => row["Field"].ToString() == key.Name && Convert.ToInt32(row["Index"]) == rowIndex))
                {
                    DataRow[] foundRows = dtSubGrid.Tables[tblpnl.Name].Select().ToList().Where(row => row["Field"].ToString() == key.Name && Convert.ToInt32(row["Index"]) == rowIndex).ToArray();
                    if (foundRows.Length > 0)
                    {

                        keyValue = key.Text.Replace(",", "");
                        if (decimal.TryParse(keyValue, out num))
                        {
                            foundRows[0]["Value"] = keyValue;
                        }
                        else
                        {
                            foundRows[0]["Value"] = key.Text;
                        }
                    }
                }
            }
            if (dtSave.Select().ToList().Exists(row => row["Field"].ToString() == key.Name))
            {
                DataRow[] foundRows = dtSave.Select().ToList().Where(row => row["Field"].ToString() == key.Name).ToArray();
                if (foundRows.Length > 0)
                    foundRows[0]["Value"] = key.Text;
                {
                    keyValue = key.Text.Replace(",", "");
                    if (decimal.TryParse(keyValue, out num))
                    {
                        foundRows[0]["Value"] = keyValue;
                    }
                    else
                    {
                        foundRows[0]["Value"] = key.Text;
                    }
                }
            }

            foreach (var obj in calcDic)
            {
                value = string.Empty;
                string[] val = obj.Value.ToObject<string[]>();
                if (val.Contains(key.Name))
                {
                    equation = string.Join(",", val);
                    equation = equation.Replace(key.Name, key.Text);
                    fields = Regex.Replace(Regex.Replace(equation, "[\\\\/]", "-"), @"[^a-zA-Z\.,_]", string.Empty);
                    sFields = fields.Split(',');
                    sFields = sFields.Where(s => s != "").ToArray();
                    equation = equation.Replace(",", "");

                    foreach (var ctrl in sFields)
                    {
                        Control[] fld = this.Controls.Find(ctrl.ToString(), true);

                        if (fld != null && fld.Length > 0)
                        {
                            if (key.Parent != null && key.Parent.GetType().Name.ToLower() == "tablelayoutpanel" && ((TableLayoutPanel)key.Parent).ColumnCount > 2)
                            {
                                var tbpnl = ((TableLayoutPanel)key.Parent);
                                int i = 0;
                                foreach (var item in fld)
                                {
                                    if (tbpnl.GetCellPosition(item).Row == rowIndex)
                                    {
                                        getVal = i;
                                    }
                                    i++;
                                }
                                if ((fld[getVal] as TextBox).Text == "")
                                    fValue = "0";
                                else
                                    fValue = (fld[getVal] as TextBox).Text;

                            }
                            else
                            {
                                if ((fld[0] as TextBox).Text == "")
                                    fValue = "0";
                                else
                                    fValue = (fld[0] as TextBox).Text;
                            }

                            equation = equation.Replace(ctrl.ToString(), fValue);
                        }
                    }
                    string[] eqn = equation.Split(')');
                    foreach (var item in eqn)
                    {
                        string val1 = "";
                        val1 = item.ToString();
                        DataTable dtCalc = new DataTable();
                        if (val1.Contains('('))
                        {
                            if (val1.Contains("(("))
                                val1 = val1 + "))";
                            else
                                val1 = val1 + ")";
                            if (val1.Contains("Math.round"))
                            {
                                val1 = val1.Replace("Math.round", "");
                                val1 = val1.Replace(",", "");
                                var objValue = dtCalc.Compute(val1, "");
                                val1 = objValue.ToString();
                                val1 = Math.Round(Convert.ToDecimal(val1)).ToString();
                                value = value + val1;
                            }
                            else
                            {
                                val1 = val1.Replace(",", "");
                                var objValue1 = dtCalc.Compute(val1, "");
                                val1 = objValue1.ToString();
                                value = value + val1;
                            }
                        }
                        else
                        {


                            val1 = value + val1;
                            val1 = val1.Replace(",", "");
                            var objValue2 = dtCalc.Compute(val1, "");
                            val1 = objValue2.ToString();
                            value = val1;
                        }
                    }


                    if (val.Length == 1)
                    {
                        Control[] calcArray = this.Controls.Find(val[0], true);
                        if (calcArray.Length > 1)
                        {
                            decimal iTotal = 0;
                            value = "0";
                            foreach (var item in calcArray)
                            {
                                iTotal = iTotal + Convert.ToDecimal(item.Text);
                            }
                            value = iTotal.ToString();
                        }
                    }

                    Control[] calc1 = this.Controls.Find(obj.Key, true);
                    if (calc1 != null && calc1.Length > 0)
                    {
                        if (calc1.Length == 1)
                            (calc1[0] as TextBox).Text = Convert.ToDecimal(value).ToString("N2");// decimal with 2 places
                        else
                            (calc1[getVal] as TextBox).Text = Convert.ToDecimal(value).ToString("N2");// decimal with 2 places 
                        ctrl_TextChanged((calc1[getVal] as TextBox), EventArgs.Empty);
                    }

                }
            }
        }
        private void opFormFormation(Button btn, Panel oParentPnl, List<formDetails> fieldDetails, JToken obj, FlowLayoutPanel ofpnl, Int16 moduleID = 0)

        {
            Panel opnlBack = new Panel();
            opnlBack.Width = oParentPnl.Width - 40;
            opnlBack.BackColor = SystemColors.ButtonFace;
            oParentPnl.Controls.Add(opnlBack);
            ofpnl.AutoSize = true;
            ofpnl.BackColor = SystemColors.ButtonFace;
            ofpnl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            opnlBack.Controls.Add(ofpnl);
            LinkDetails linkDetail = new LinkDetails();
            if (Convert.ToString(obj["showHeader"]) == "Y")
            {
                Panel oHeader = new Panel();
                oHeader.Width = oParentPnl.Width;
                oHeader.Height = 35;
                oHeader.BackColor = SystemColors.ControlLight;
                oHeader.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
                ofpnl.Controls.Add(oHeader);
                Label lblHeader = new Label();
                lblHeader.Font = RegistryConfig.myHeaderFont;
                lblHeader.Text = string.Concat(Convert.ToString(obj["title"]).Select(c => char.IsUpper(c) ? " " + c.ToString() : c.ToString())).TrimStart();
                lblHeader.AutoSize = true;
                oHeader.Controls.Add(lblHeader);
            }
            if (Convert.ToInt32(obj["isSubGrid"]) == 1)
            {
                subGridTable = (Convert.ToString(obj["module_db"]));
                linkDetail.LinkPKId = (Convert.ToString(obj["link_key"]));
                linkDetail.TableName = subGridTable;
                linkDetail.TablePKId = (Convert.ToString(obj["module_db_key"]));
                linkPk.Add(linkDetail);
                opSubGridControlFormations(btn, oParentPnl, fieldDetails, ofpnl, moduleID);
            }
            else
            {
                opControlFormation(btn, fieldDetails, ofpnl, opnlBack);

            }
        }
        private void opSubGridControlFormations(Button btn, Panel oParentPnl, List<formDetails> fieldDetails, FlowLayoutPanel ofpnl, Int16 moduleID)
        {
            DataRow dr;
            DataTable dt = new DataTable(subGridTable);

            DataColumn dtColumn = new DataColumn();
            dtColumn.ColumnName = "Field";
            dtColumn.MaxLength = int.MaxValue;
            dtColumn.DataType = typeof(string);
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.ColumnName = "Value";
            dtColumn.MaxLength = int.MaxValue;
            dtColumn.DataType = typeof(string);
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.ColumnName = "Index";
            dtColumn.MaxLength = int.MaxValue;
            dtColumn.DataType = typeof(Int64);
            dt.Columns.Add(dtColumn);
            dtSubGrid.Tables.Add(dt);

            ofpnl.FlowDirection = FlowDirection.TopDown;
            ofpnl.Width = oParentPnl.Width - 40;
            TableLayoutPanel tblPnl = new TableLayoutPanel();
            tblPnl.Width = ofpnl.Width;
            tblPnl.VerticalScroll.Maximum = 0;
            tblPnl.AutoScroll = true;
            tblPnl.Tag = 2;
            int c = 0;
            tblPnl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tblPnl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            foreach (var detail in fieldDetails)
            {
                bool has = gstCal.Contains(detail.field);
                if (has == true)
                {
                    count++;
                }
                if (detail.view == "1")
                {
                    lbl = new Label();
                    lbl.Text = detail.label;
                    if (detail.type == "hidden" || detail.type == "formula_hidden")
                    {
                        lbl.Visible = false;
                    }
                    if (detail.required == "required")
                        lbl.Text = lbl.Text + '*';
                    lbl.Font = RegistryConfig.myFont;
                    lbl.AutoSize = true;
                    ctrl = opDynamicControlSelection(btn, pbUpload, ctrl, detail, true);
                    dr = dt.NewRow();
                    dr["Field"] = detail.field;

                    if (detail.type == "text_datetime" || detail.type == "text_date")
                    {
                        dr["Value"] = Convert.ToString(DateTime.MinValue.ToString("dd-MM-yyyy HH:mm:ss"));
                    }

                    else
                    {
                        dr["Value"] = "";
                    }


                    if (detail.type != "hidden" && detail.type != "formula_hidden")
                    {
                        if (detail.fieldWidth == "")
                        {
                            tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
                        }
                        else
                        {
                            tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, float.Parse(detail.fieldWidth)));
                        }
                    }
                    else
                    {
                        tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0F));
                    }
                    tblPnl.Controls.Add(lbl, c, 0);
                    tblPnl.Controls.Add(ctrl, c, 1);
                    dr["Index"] = 1;
                    dt.Rows.Add(dr);
                    if (detail.fieldWidth == "")
                    {
                        ctrl.Width = 200;
                    }
                    else
                    {
                        if (detail.type != "hidden" && detail.type != "formula_hidden")
                        {
                            ctrl.Width = Convert.ToInt32(detail.fieldWidth);
                        }
                        else
                        {
                            ctrl.Width = 0;
                        }
                    }

                    c++;
                }

            }
            tblPnl.ColumnCount = fieldDetails.Count;
            tblPnl.RowCount = Convert.ToInt32(tblPnl.Tag);
            tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            PictureBox pbDelete = new PictureBox();
            pbDelete.Name = subGridTable;
            pbDelete.Dock = DockStyle.Right;
            try
            {
                pbDelete.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.TrashO)
                { ForeColor = Color.DarkGray, Size = 18 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                pbDelete.Image = Properties.Resources.icon_trash;
            }
            tblPnl.ColumnCount = tblPnl.ColumnCount + 1;
            pbDelete.Click += new EventHandler(pbDelete_Click);
            tblPnl.Controls.Add(pbDelete, c, 1);
            tblPnl.Width = ofpnl.Width;
            tblPnl.Name = subGridTable;
            ofpnl.Controls.Add(tblPnl);
            Button btnAddNew = new Button();
            btnAddNew.UseMnemonic = false;
            btnAddNew.Name = subGridTable;
            btnAddNew.Text = " New Item";
            btnAddNew.Font = RegistryConfig.myFontBold;
            btnAddNew.AutoSize = true;
            try
            {
                btnAddNew.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Plus)
                { ForeColor = Color.White, Size = 12 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnAddNew.Image = Properties.Resources.icon_plus;
            }

            btnAddNew.TextAlign = ContentAlignment.TopCenter;
            btnAddNew.ImageAlign = ContentAlignment.MiddleCenter;
            btnAddNew.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddNew.BackColor = SystemColors.HotTrack;
            btnAddNew.ForeColor = Color.White;
            btnAddNew.FlatStyle = FlatStyle.Flat;
            btnAddNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddNew.Dock = DockStyle.Right;
            btnAddNew.Tag = moduleID;
            btnAddNew.Click += new EventHandler(btnAddNew_Click);
            Panel pnlBtn = new Panel();
            pnlBtn.Height = btnAddNew.Height;
            pnlBtn.BackColor = SystemColors.ButtonFace;
            pnlBtn.Width = ofpnl.Width - 35;
            pnlBtn.Controls.Add(btnAddNew);
            ofpnl.Controls.Add(pnlBtn);
            ofpnl.Parent.Height = ofpnl.Height;
        }
        private void pbDelete_Click(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;
            var parentPnl = (TableLayoutPanel)pb.Parent;
            if (parentPnl.RowCount > 2)
            {
                int rowIndex = parentPnl.GetCellPosition(pb).Row;
                for (int columnIndex = 0; columnIndex < parentPnl.ColumnCount; columnIndex++)
                {
                    var control = parentPnl.GetControlFromPosition(columnIndex, rowIndex);
                    parentPnl.Controls.Remove(control);
                }
                Control[] numCtrl = parentPnl.Controls.OfType<TextBox>().ToArray();
                foreach (var ctrl in numCtrl)
                {
                    string valChanged = ctrl.Text;
                    ctrl.Text = "";
                    ctrl.Text = valChanged;
                }
                parentPnl.RowCount -= 1;
                parentPnl.Tag = parentPnl.RowCount;
                parentPnl.Height = parentPnl.Height - 50;
                parentPnl.Parent.Height = parentPnl.Parent.Height - 50;
                parentPnl.Parent.Parent.Height = parentPnl.Parent.Height;
            }
            else
            {
                MessageBox.Show("Form must have at least one row", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            DataRow dr;
            var button = (Button)sender;
            count = 0;
            string stateFrom = string.Empty, stateTo = string.Empty, values = string.Empty;
            DataTable dt = new DataTable();
            FlowLayoutPanel oParentPnl = new FlowLayoutPanel();
            oParentPnl = (button.Parent.Parent as FlowLayoutPanel);
            Control[] tblCtrl = oParentPnl.Controls.OfType<TableLayoutPanel>().Cast<Control>().ToArray();
            moduleValues oModuleValues = new moduleValues();
            oModuleValues = opGetModuleDetailsByID(Convert.ToInt16(button.Tag), true);
            List<formDetails> oFormFields = new List<formDetails>();
            jForm = JsonConvert.DeserializeObject<dynamic>(oModuleValues.Form);
            List<formDetails> fieldDetails = new List<formDetails>();
            fieldDetails = jForm[0]["fields"].ToObject<List<formDetails>>();
            TableLayoutPanel tblPnl = new TableLayoutPanel();
            tblPnl = (TableLayoutPanel)tblCtrl[0];
            int c = 0;
            int row = 0;
            bool isMulti = false;
            row = (Convert.ToInt32(tblPnl.Tag));
            rowIndexVal = tblPnl.RowCount + 1;
            foreach (var detail in fieldDetails)
            {
                bool has = gstCal.Contains(detail.field);
                if (has == true)
                {
                    count++;
                }
                if (detail.view == "1")
                {

                    ctrl = opDynamicControlSelection(btnCreate, pbUpload, ctrl, detail, true);
                    dr = dtSubGrid.Tables[button.Name].NewRow();
                    dr["Field"] = detail.field;

                    if (detail.type == "text_datetime" || detail.type == "text_date")
                    {
                        dr["Value"] = Convert.ToString(DateTime.MinValue.ToString("dd-MM-yyyy HH:mm:ss"));
                    }

                    else
                    {
                        dr["Value"] = "";
                    }

                    Control[] tbxs = { ctrl };
                    ctrl.Parent = tblPnl;
                    if (subGridLink.ContainsKey(ctrl.Name))
                    {

                        values = subGridLink.Single(x => x.Key == ctrl.Name).Value;
                        dt = comboBoxValuesforLinks(values);
                        linkCtrlValues(ref stateFrom, ref stateTo, dt, isMulti, tbxs);
                    }



                    if (detail.type != "hidden" && detail.type != "formula_hidden")
                    {
                        if (detail.fieldWidth == "")
                        {
                            tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
                        }
                        else
                        {
                            tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, float.Parse(detail.fieldWidth)));
                        }
                    }
                    else
                    {
                        tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0F));
                    }

                    tblPnl.Controls.Add(ctrl, c, row);
                    dr["Index"] = Convert.ToInt64(row);
                    dtSubGrid.Tables[button.Name].Rows.Add(dr);
                    if (detail.fieldWidth == "")
                    {
                        ctrl.Width = 200;
                    }
                    else
                    {
                        if (detail.type != "hidden" && detail.type != "formula_hidden")
                        {
                            ctrl.Width = Convert.ToInt32(detail.fieldWidth);
                        }
                        else
                        {
                            ctrl.Width = 0;
                        }
                    }

                    c++;
                }
            }
            tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            PictureBox pbDelete = new PictureBox();
            pbDelete.Dock = DockStyle.Right;
            try
            {
                pbDelete.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.TrashO)
                { ForeColor = Color.DarkGray, Size = 18 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                pbDelete.Image = Properties.Resources.icon_trash;
            }
            pbDelete.Click += new EventHandler(pbDelete_Click);
            tblPnl.Controls.Add(pbDelete, c, row);
            tblPnl.ColumnCount = fieldDetails.Count + 1;
            tblPnl.Tag = Convert.ToInt32(tblPnl.Tag) + 1;
            tblPnl.RowCount = Convert.ToInt32(tblPnl.Tag);
            tblPnl.Width = tblPnl.Parent.Width - 50;
            tblPnl.Height = tblPnl.Height + 50;
            oParentPnl.Height = tblPnl.Height + button.Parent.Height;
            oParentPnl.Parent.Height = oParentPnl.Height;
        }

        //private void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    List<HoldingCell> tempHolding = new List<HoldingCell>();
        //    HoldingCell cell = default(HoldingCell);
        //      var button = (Button)sender;

        //    FlowLayoutPanel oParentPnl = new FlowLayoutPanel();
        //    oParentPnl = (button.Parent.Parent as FlowLayoutPanel);
        //    Control[] tblCtrl = oParentPnl.Controls.OfType<TableLayoutPanel>().Cast<Control>().ToArray();
        //    TableLayoutPanel tblPnl = new TableLayoutPanel();
        //    tblPnl = (TableLayoutPanel)tblCtrl[0];
        //    tblPnl.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        //    var _with2 = tblPnl;
        //    Int32 insertPoint = tblPnl.RowCount;
        //    for (Int32 row = insertPoint; row <= tblPnl.RowCount+1; row++)
        //    {
        //        for (Int32 col = 0; col <= tblPnl.ColumnCount - 1; col++)
        //        {
        //            cell = new HoldingCell();
        //            cell.cntrl = _with2.GetControlFromPosition(col, row-1);
        //            //setup position for restore = current row + 1 
        //            cell.pos = new TableLayoutPanelCellPosition(col, row );
        //            tempHolding.Add(cell);
        //        }
        //    }

        //    ////insert new row 
        //    //_with2.RowStyles.Insert(insertPoint, new RowStyle(SizeType.Absolute, 30f));
        //    //_with2.RowCount += 1;

        //    //adjust control positions 
        //    foreach (HoldingCell cell_loopVariable in tempHolding)
        //    {
        //        cell = cell_loopVariable;
        //        if (cell.cntrl != null)
        //        {
        //          //  _with2.SetCellPosition(cell.cntrl, cell.pos);
        //            _with2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cell_loopVariable.cntrl.Width));
        //            _with2.Controls.Add(cell_loopVariable.cntrl, cell_loopVariable.pos.Column, cell_loopVariable.pos.Row);

        //        }
        //    }
        //    tempHolding = null;
        //    //for (Int32 col = 0; col <= tblPnl.ColumnCount - 1; col++)
        //    //    {
        //    //        cell = new HoldingCell();
        //    //        cell.cntrl = tblPnl.GetControlFromPosition(col, 1);
        //    //        cell.pos = new TableLayoutPanelCellPosition(col, row +1 );
        //    //        tempHolding.Add(cell);
        //    //    }



        //    //////insert new row 

        //    //tblPnl.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        //    ////adjust control positions 
        //    //foreach (HoldingCell cell_loopVariable in tempHolding)
        //    //{
        //    //    //  cell = cell_loopVariable;
        //    //    if (cell_loopVariable.cntrl != null)
        //    //    {
        //    //        tblPnl.SetCellPosition(cell_loopVariable.cntrl, cell_loopVariable.pos);
        //    //        //tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cell_loopVariable.cntrl.Width));
        //    //        //tblPnl.Controls.Add(cell_loopVariable.cntrl, cell_loopVariable.pos.Column, cell_loopVariable.pos.Row);

        //    //    }

        //    //}
        //    //tblPnl.ColumnCount = fieldDetails.Count + 1;
        //    //tblPnl.Tag = Convert.ToInt32(tblPnl.Tag) + 1;
        //    tblPnl.RowCount = Convert.ToInt32(tblPnl.Tag);
        //    tblPnl.Width = tblPnl.Parent.Width - 50;
        //    tblPnl.Height = tblPnl.Height + 50;
        //    oParentPnl.Height = tblPnl.Height + button.Parent.Height;
        //    oParentPnl.Parent.Height = oParentPnl.Height;
        //    //tempHolding = null;
        //}
        private void opControlFormation(Button btn, List<formDetails> fieldDetails, FlowLayoutPanel ofpnl, Panel opnlBack)
        {
            DataRow dr;
            ofpnl.FlowDirection = FlowDirection.TopDown;
            hiddenField = new Dictionary<string, object>();
            formulaHiddenField = new Dictionary<string, object>();
            foreach (var detail in fieldDetails)
            {

                if (detail.view == "1")
                {

                    lbl = new Label();
                    lbl.Text = detail.label;
                    if (detail.type == "hidden" || detail.type == "formula_hidden")
                    {
                        lbl.Visible = false;
                    }
                    if (detail.required == "required")
                        lbl.Text = lbl.Text + '*';
                    lbl.AutoSize = true;
                    lbl.Font = RegistryConfig.myFont;
                    ofpnl.Controls.Add(lbl);
                    ctrl = opDynamicControlSelection(btn, pbUpload, ctrl, detail);
                    dr = dtSave.NewRow();
                    dr["Field"] = detail.field;

                    if (detail.type == "text_datetime" || detail.type == "text_date")
                    {
                        dr["Value"] = Convert.ToString(DateTime.MinValue.ToString("dd-MM-yyyy HH:mm:ss"));
                    }

                    else
                    {
                        dr["Value"] = "";
                    }
                    dtSave.Rows.Add(dr);
                    if (detail.fieldWidth != "")
                        ctrl.Width = opnlBack.Width * Convert.ToInt32(detail.fieldWidth) / 100;
                    else
                        ctrl.Width = opnlBack.Width - 20;
                    if (detail.fAlign == "R")
                    {
                        ctrl.Anchor = AnchorStyles.Right;
                        ctrl.Dock = DockStyle.Right;
                        ctrl.Width = 250;
                        if (detail.type == "file")
                        {
                            ofpnl.Controls.Add(pbUpload);
                            pbUpload.Anchor = AnchorStyles.Right;
                            pbUpload.Dock = DockStyle.Right;
                        }
                    }
                    ofpnl.Controls.Add(ctrl);
                    if (detail.type == "file")
                    {
                        ofpnl.Controls.Add(pbUpload);
                    }
                    opnlBack.Height = ofpnl.Height;
                }

            }
        }
        private void ctrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ctrl = (ComboBox)sender;
            JObject jobj = null;
            string str = string.Empty, moreFields = string.Empty, stateFrom = string.Empty, stateTo = string.Empty;
            DataTable dt = new DataTable();
            int c = 0;
            if (linkDic.ContainsKey(ctrl.Name))
            {
                jobj = linkDic.Single(x => x.Key == ctrl.Name).Value;
                c = jobj["ofield"].ToArray().Length;
            }
            double num = 0;
            bool isMulti = false;

            if (dtSave.Select().ToList().Exists(row => row["Field"].ToString() == ctrl.Name))
            {
                DataRow[] foundRows = dtSave.Select().ToList().Where(row => row["Field"].ToString() == ctrl.Name).ToArray();
                if (foundRows.Length > 0 && Convert.ToInt16((ctrl as ComboBox).SelectedValue) != -1)
                    foundRows[0]["Value"] = (ctrl as ComboBox).SelectedValue;
            }

            if (ctrl.Parent.GetType().Name.ToLower() == "tablelayoutpanel" && ((TableLayoutPanel)ctrl.Parent).ColumnCount > 2)
            {
                var tbpnl = (TableLayoutPanel)ctrl.Parent;
                int rowIndex = tbpnl.GetCellPosition(ctrl).Row;

                if (dtSubGrid.Tables[tbpnl.Name].Select().ToList().Exists(row => row["Field"].ToString() == ctrl.Name && Convert.ToInt32(row["Index"]) == rowIndex))
                {
                    DataRow[] foundRows = dtSubGrid.Tables[tbpnl.Name].Select().ToList().Where(row => row["Field"].ToString() == ctrl.Name && Convert.ToInt32(row["Index"]) == rowIndex).ToArray();
                    if (foundRows.Length > 0 && Convert.ToInt16((ctrl as ComboBox).SelectedValue) != -1)
                        foundRows[0]["Value"] = (ctrl as ComboBox).SelectedValue;
                }
                for (int t = 0; t <= c - 1; t++)
                {
                    Control[] tbctrl = tbpnl.Controls.Find(jobj["ofield"][t].ToString(), true);
                    //  Control[] tbxs = { tbctrl[rowIndex - 1] };
                    for (int v = 0; v < tbctrl.Length; v++)
                    {

                        if (tbctrl != null && tbctrl.Length > 0 && Convert.ToString(jobj["ftable"][t]) != "")
                        {
                            qryFormation(ctrl, out str, ref moreFields, jobj, num, ref isMulti, t);
                            dt = comboBoxValuesforLinks(str);
                            Control[] loopCtrl = { tbctrl[v] };
                            linkCtrlValues(ref stateFrom, ref stateTo, dt, isMulti, loopCtrl);
                            dt = new DataTable();
                        }
                    }
                }
            }
            else
            {
                for (int t = 0; t <= c - 1; t++)
                {
                    Control[] tbxs = this.Controls.Find(jobj["ofield"][t].ToString(), true);
                    for (int v = 0; v < tbxs.Length; v++)
                    {

                        if (tbxs != null && tbxs.Length > 0 && Convert.ToString(jobj["ftable"][t]) != "")
                        {
                            qryFormation(ctrl, out str, ref moreFields, jobj, num, ref isMulti, t);
                            if (!subGridLink.ContainsKey(tbxs[v].Name))
                            {
                                subGridLink.Add(tbxs[v].Name, str);
                            }
                            dt = comboBoxValuesforLinks(str);
                            Control[] loopCtrl = { tbxs[v] };
                            linkCtrlValues(ref stateFrom, ref stateTo, dt, isMulti, loopCtrl);
                        }
                    }
                }
            }
            for (int t = 0; t <= c - 1; t++)
            {

                if (jobj["trigerfield"] != null && jobj["triggerfield"][t].ToString() != "")
                {
                    Control[] tbxs = this.Controls.Find(jobj["triggerfield"][t].ToString(), true);
                    for (int v = 0; v < tbxs.Length; v++)
                    {
                        if (tbxs != null && tbxs.Length > 0)
                        {
                            if (tbxs[v].GetType().Name == "ComboBox")
                            {
                                int iSelectedItem = Convert.ToInt32((tbxs[v] as ComboBox).SelectedIndex);
                                (tbxs[v] as ComboBox).SelectedIndex = 0;
                                (tbxs[v] as ComboBox).SelectedIndex = iSelectedItem;
                                ctrl_SelectedIndexChanged((tbxs[v] as ComboBox), EventArgs.Empty);
                            }
                        }
                    }
                }
            }
        }
        private void linkCtrlValues(ref string stateFrom, ref string stateTo, DataTable dt, bool isMulti, Control[] tbxs)
        {
            if (tbxs[0].GetType().Name == "ComboBox")
            {

                if (dt.Rows.Count != 0)
                {
                    if (isMulti == true || dt.Columns.Count > 1)
                    {
                        (tbxs[0] as ComboBox).ValueMember = "id";
                        (tbxs[0] as ComboBox).DisplayMember = "name";
                        (tbxs[0] as ComboBox).DataSource = dt;
                        DataRow dr = dt.NewRow();
                        dr["name"] = "--Select--";
                        dr["id"] = -1;
                        DataRow[] rows = dt.Select("id= -1");
                        if (rows.Length == 0)
                        {
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                    else
                    {
                        (tbxs[0] as ComboBox).SelectedValue = Convert.ToInt32(dt.Rows[0][0]);
                    }
                }
                else
                {
                    if (!(dt.Columns.Contains("name") && dt.Columns.Contains("id")))
                    {
                        dt.Columns.AddRange(new DataColumn[2] { new DataColumn("name", typeof(string)),
                                                   new DataColumn("id",typeof(string)) });
                    }
                (tbxs[0] as ComboBox).ValueMember = "id";
                    (tbxs[0] as ComboBox).DisplayMember = "name";
                    (tbxs[0] as ComboBox).DataSource = dt;
                    DataRow dr = dt.NewRow();
                    dr["name"] = "--Select--";
                    dr["id"] = -1;
                    DataRow[] rows = dt.Select("id= -1");
                    if (rows.Length == 0)
                    {
                        dt.Rows.InsertAt(dr, 0);
                    }
                (tbxs[0] as ComboBox).SelectedIndex = 0;
                    dt = null;

                }

            }

            else if (tbxs[0].GetType().Name == "RichTextBox")
            {
                if (dt.Rows.Count != 0)
                {
                    (tbxs[0] as RichTextBox).Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    (tbxs[0] as RichTextBox).Text = "";
                }

            }
            else if (tbxs[0].GetType().Name == "TextBox")
            {

                if (count == 3 && (tbxs[0] as TextBox).Name == "nxt_cmn_igst")
                {
                    Control[] gst = this.Controls.Find("nxt_state_gst_from", true);
                    if (gst != null && gst.Length > 0)
                    {
                        stateFrom = (gst[0] as TextBox).Text;
                    }
                    Control[] ctrl1 = this.Controls.Find("nxt_state_gst_to", true);
                    if (ctrl1 != null && ctrl1.Length > 0)
                    {
                        stateTo = (ctrl1[0] as TextBox).Text;
                    }
                    if (stateFrom != stateTo && dt.Rows.Count != 0)
                    {
                        (tbxs[0] as TextBox).Text = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        (tbxs[0] as TextBox).Text = "0";
                    }
                }
                else if (count == 3 && (tbxs[0] as TextBox).Name == "nxt_cmn_cgst")
                {
                    Control[] gst = this.Controls.Find("nxt_state_gst_from", true);
                    if (gst != null && gst.Length > 0)
                    {
                        stateFrom = (gst[0] as TextBox).Text;
                    }
                    Control[] ctrl1 = this.Controls.Find("nxt_state_gst_to", true);
                    if (ctrl1 != null && ctrl1.Length > 0)
                    {
                        stateTo = (ctrl1[0] as TextBox).Text;
                    }
                    if (stateFrom == stateTo && dt.Rows.Count != 0)
                    {
                        (tbxs[0] as TextBox).Text = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        (tbxs[0] as TextBox).Text = "0";
                    }
                }
                else if (count == 3 && (tbxs[0] as TextBox).Name == "nxt_cmn_sgst")
                {
                    Control[] gst = this.Controls.Find("nxt_state_gst_from", true);
                    if (gst != null && gst.Length > 0)
                    {
                        stateFrom = (gst[0] as TextBox).Text;
                    }
                    Control[] ctrl1 = this.Controls.Find("nxt_state_gst_to", true);
                    if (ctrl1 != null && ctrl1.Length > 0)
                    {
                        stateTo = (ctrl1[0] as TextBox).Text;
                    }
                    if (stateFrom == stateTo && dt.Rows.Count != 0)
                    {
                        (tbxs[0] as TextBox).Text = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        (tbxs[0] as TextBox).Text = "0";
                    }
                }
                else if (dt.Rows.Count != 0)
                {
                    (tbxs[0] as TextBox).Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    (tbxs[0] as TextBox).Text = "";
                }
            }

            else if (tbxs[0].GetType().Name == "DateTime")
            {
                if (dt.Rows.Count != 0)
                {
                    (tbxs[0] as DateTimePicker).Text = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    (tbxs[0] as DateTimePicker).Text = "";
                }
            }

            if (tbxs[0].Parent.GetType().Name.ToLower() == "tablelayoutpanel" && ((TableLayoutPanel)tbxs[0].Parent).ColumnCount > 2)
            {
                var tbpnl = (TableLayoutPanel)tbxs[0].Parent;
                int rowIndex = tbpnl.GetCellPosition(tbxs[0]).Row;
                if (tbxs[0].GetType().Name == "ComboBox")
                {
                    if (dtSubGrid.Tables[tbpnl.Name].Select().ToList().Exists(row => row["Field"].ToString() == tbxs[0].Name && Convert.ToInt32(row["Index"]) == rowIndex))
                    {
                        DataRow[] foundRows = dtSubGrid.Tables[tbpnl.Name].Select().ToList().Where(row => row["Field"].ToString() == tbxs[0].Name && Convert.ToInt32(row["Index"]) == rowIndex).ToArray();
                        if (foundRows.Length > 0 && Convert.ToInt16((tbxs[0] as ComboBox).SelectedValue) != -1)
                            foundRows[0]["Value"] = (tbxs[0] as ComboBox).SelectedValue;
                    }
                }
                else
                {
                    DataRow[] foundRows = dtSubGrid.Tables[tbpnl.Name].Select().ToList().Where(row => row["Field"].ToString() == tbxs[0].Name && Convert.ToInt32(row["Index"]) == rowIndex).ToArray();
                    if (foundRows.Length > 0)
                        foundRows[0]["Value"] = tbxs[0].Text;
                }
            }
            else
            {
                if (tbxs[0].GetType().Name == "ComboBox")
                {
                    if (dtSave.Select().ToList().Exists(row => row["Field"].ToString() == tbxs[0].Name))
                    {
                        DataRow[] foundRows = dtSave.Select().ToList().Where(row => row["Field"].ToString() == tbxs[0].Name).ToArray();
                        if (foundRows.Length > 0 && Convert.ToInt16((tbxs[0] as ComboBox).SelectedValue) != -1)
                            foundRows[0]["Value"] = (tbxs[0] as ComboBox).SelectedValue;
                    }

                }

                else
                {
                    if (dtSave.Select().ToList().Exists(row => row["Field"].ToString() == tbxs[0].Name))
                    {
                        DataRow[] foundRows = dtSave.Select().ToList().Where(row => row["Field"].ToString() == tbxs[0].Name).ToArray();
                        if (foundRows.Length > 0)

                            foundRows[0]["Value"] = tbxs[0].Text;

                    }
                }
            }
        }
        private static void qryFormation(ComboBox ctrl, out string str, ref string moreFields, JObject jobj, double num, ref bool isMulti, int t)
        {
            str = "";
            if (Convert.ToInt32(ctrl.SelectedValue) != -1)
            {
                str = "select  distinct ";
                if (Convert.ToString(jobj["ffield"][t]) != "")
                {
                    str = str + Convert.ToString(jobj["ffield"][t]) + " as name , ";

                }
                if (Convert.ToString(jobj["vfield"][t]) != "")
                {
                    str = str + Convert.ToString(jobj["vfield"][t]);
                    if (Convert.ToString(jobj["ffield"][t]) != "")
                    {
                        str = str + " as id";
                        isMulti = true;
                    }
                    else
                    {
                        isMulti = false;
                    }
                }
                if (Convert.ToString(jobj["ftable"][t]) != "")
                {
                    str = str + " from " + Convert.ToString(jobj["ftable"][t]);
                }
                if (Convert.ToString(jobj["morefield"][t]) != "")
                {
                    moreFields = ((Convert.ToString(jobj["morefield"][t]).Replace(",", " ")).Replace("(", "")).Replace(")", "");
                    moreFields = moreFields.Replace("f#", "").Replace("d#", "");
                }
                if (Convert.ToString(jobj["gfield"][t]) != "")
                {
                    if (double.TryParse(Convert.ToString(ctrl.SelectedValue), out num))
                        str = str + " where " + Convert.ToString(jobj["gfield"][t]) + " = " + num;
                    else
                        str = str + " where " + Convert.ToString(jobj["gfield"][t]) + " = '" + ctrl.SelectedValue + "'";
                    str = str + moreFields;


                }
            }
        }
        private DataTable getPreviousAutoNumber(string tableName, string colName)
        {
            DataTable dt = new DataTable();
            try
            {

                //RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
                using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "usp_GetRecentAutonumber";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlda = new SqlDataAdapter();
                        cmd.Parameters.AddWithValue("@TableName", tableName);
                        cmd.Parameters.AddWithValue("@Column", colName);
                        sqlda = new SqlDataAdapter(cmd);
                        sqlda.Fill(dt);

                    }
                }


            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Values for Drop Down Control: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
            return dt;
        }
        private DataTable comboBoxValuesforLinks(string qry)
        {
            DataTable dt = new DataTable();
            try
            {

                //RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
                using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = qry;
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter sqlda = new SqlDataAdapter();
                        sqlda = new SqlDataAdapter(cmd);
                        sqlda.Fill(dt);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Values for Drop Down Control(link): " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
            return dt;
        }
        private DataTable comboBoxValues(TableDetails oDetails)
        {
            DataTable dt = new DataTable();
            try
            {

                using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "usp_GetValuesforDropDown";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlda = new SqlDataAdapter();
                        cmd.Parameters.AddWithValue("@TableName", oDetails.TableName);
                        cmd.Parameters.AddWithValue("@Key", oDetails.Key);
                        cmd.Parameters.AddWithValue("@Value", oDetails.Value);
                        cmd.Parameters.AddWithValue("@Condition", oDetails.Condition);
                        sqlda = new SqlDataAdapter(cmd);
                        sqlda.Fill(dt);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.LogData("Error in Getting Values for Drop Down Control: " + ex.Message + ex.StackTrace, Log.Status.Error);
            }
            return dt;
        }
        private void oParentPnl_MouseWheel(object sender, EventArgs e)
        {
            var pnl = (Panel)sender;
            pnl.Focus();
        }
        #endregion

        #region [Search panel]
        private List<girdDetails> opCreatingSearchPanel(moduleValues oValues, Panel oPanel)
        {
            FlowLayoutPanel oPnlSearch = new FlowLayoutPanel();
            oPnlSearch.Width = 350;
            oPnlSearch.Height = pnlBreadCrumb.Height;
            oPnlSearch.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            oPnlSearch.Dock = DockStyle.None;
            ComboBox cmbSearch = new ComboBox();
            cmbSearch.Width = 130;
            cmbSearch.Font = RegistryConfig.myFont;
            List<girdDetails> girdDetails = new List<girdDetails>();
            girdDetails = JsonConvert.DeserializeObject<List<girdDetails>>(oValues.Grid);
            string[] searchFields = girdDetails.Where(q => q.view == true && q.label != "").Select(q => q.label).ToArray();
            if (searchFields.Length > 0)
            {
                cmbSearch.Items.Add("Sort");
                for (int i = 0; i < searchFields.Length; i++)
                {
                    cmbSearch.Items.Add(searchFields[i]);
                }
            }
            cmbSearch.SelectedText = "Sort";
            cmbSearch.DropDownHeight = 100;
            ComboBox cmbOrder = new ComboBox();
            cmbOrder.Width = 70;
            cmbOrder.Font = RegistryConfig.myFont;
            cmbOrder.Items.Add("Order");
            cmbOrder.Items.Add("Desc");
            cmbOrder.Items.Add("Asc");
            cmbOrder.SelectedText = "Order";
            oPanel.Controls.Add(oPnlSearch);
            oPnlSearch.Controls.Add(cmbSearch);
            oPnlSearch.Controls.Add(cmbOrder);
            Button btnGo = new Button();
            btnGo.Width = 43;
            btnGo.Height = 26;
            btnGo.BackColor = Color.FromArgb(((byte)(3)), ((byte)(101)), ((byte)(192)));
            btnGo.ForeColor = Color.White;
            btnGo.Font = new Font("Calibri", 12.00F, System.Drawing.FontStyle.Bold);
            btnGo.Name = "btnGo";
            btnGo.Text = "Go";
            btnGo.FlatAppearance.BorderSize = 0;
            btnGo.FlatStyle = FlatStyle.Flat;
            oPnlSearch.Controls.Add(btnGo);
            Button btnReset = new Button();
            btnReset.Name = "btnReset";
            btnReset.Height = 26;
            btnReset.BackColor = Color.FromArgb(((byte)(3)), ((byte)(101)), ((byte)(192)));
            btnReset.ForeColor = Color.White;
            btnReset.Font = new Font("Calibri", 12.00F, System.Drawing.FontStyle.Bold);
            btnReset.Text = " Reset";
            try
            {
                btnReset.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Refresh)
                { ForeColor = Color.White, Size = 16 });
            }
            catch (Exception ex)
            {
                Log.LogData("Error in Loading Font Awesome Icon: " + ex.Message + ex.StackTrace, Log.Status.Error);
                btnReset.Image = Properties.Resources.icon_Refresh;
            }
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.ImageAlign = ContentAlignment.MiddleCenter;
            btnReset.TextAlign = ContentAlignment.MiddleCenter;
            btnReset.AutoSize = true;
            btnReset.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReset.Click += new EventHandler(btnReset_Click);
            oPnlSearch.Controls.Add(btnReset);
            return girdDetails;
        }
        private void btnModule_Click(object sender, EventArgs e)
        {
            frmServerDetails oServer = new frmServerDetails(RegistryConfig.database);
            oServer.opSaveModuleDetails();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            var btnReset = (Button)sender;
            if (btnReset.Parent.Controls.Count != 0)
            {
                List<ComboBox> items = new List<ComboBox>();
                foreach (ComboBox cmb in btnReset.Parent.Controls.OfType<ComboBox>())
                {
                    cmb.SelectedIndex = 0;
                }
            }
        }
        #endregion

        public Control[] GetAll(Control control)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl))
                                      .Concat(controls)
                                      .Where(c => c.GetType() != typeof(Label) && c.GetType() != typeof(Panel) && c.GetType() != typeof(FlowLayoutPanel) && c.GetType() != typeof(TableLayoutPanel)).ToArray();
        }
    }

    #region [Form & Grid Objects]
    public struct HoldingCell
    {
        public Control cntrl;
        public TableLayoutPanelCellPosition pos;
    }
    public class menuDetails
    {

        public int id { get; set; }
        public string name { get; set; }
        public string logoClass { get; set; }
        public string logo { get; set; }
        public string backgroundColor { get; set; }
        public int parentId { get; set; }
        public string moduleName { get; set; }
        public int moduleId { get; set; }
        public string position { get; set; }
        public int ordering { get; set; }

    }
    public class moduleValues
    {
        public string Title { get; set; }
        public string Form { get; set; }
        public string Grid { get; set; }
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
    }
    public class girdDetails
    {
        public string field { get; set; }
        public string alias { get; set; }
        public JArray language { get; set; }
        public string label { get; set; }
        public bool view { get; set; }
        public bool viewmobile { get; set; }
        public bool detail { get; set; }
        public bool highlight { get; set; }
        public bool sortable { get; set; }
        public bool search { get; set; }
        public bool download { get; set; }
        public bool frozen { get; set; }
        public string limited { get; set; }
        public string width { get; set; }
        public string align { get; set; }
        public string sortlist { get; set; }
        public JObject auto { get; set; }
        public JArray calc { get; set; }
        public JObject links { get; set; }
        public JObject conn { get; set; }
        public JObject attribute { get; set; }
        public string type { get; set; }
    }
    public class formDetails
    {
        public string field { get; set; }
        public string alias { get; set; }
        public JArray language { get; set; }
        public string label { get; set; }
        public string form_group { get; set; }
        public string required { get; set; }
        public string duplicate { get; set; }
        public string tabOrder { get; set; }
        public string fAlign { get; set; }
        public string fieldWidth { get; set; }
        public string view { get; set; }
        public string viewmobile { get; set; }
        public string type { get; set; }
        public short add { get; set; }
        public string size { get; set; }
        public short edit { get; set; }
        public short search { get; set; }
        public bool wreadonly { get; set; }
        public string access_server { get; set; }
        public short sortlist { get; set; }
        public string limited { get; set; }
        public JObject option { get; set; }
        public string wblock { get; set; }
        public JObject auto { get; set; }
        public JArray calc { get; set; }
        public JObject links { get; set; }
        public JObject attribute { get; set; }
    }
    public class cbValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
    public class TableDetails
    {
        public string TableName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Condition { get; set; }
    }
    public class Item
    {
        public string Name;
        public dynamic Value;
        public Item(string name, dynamic value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
    }

    public class LinkDetails
    {
        public string LinkPKId { get; set; }
        public string TableName { get; set; }
        public string TablePKId { get; set; }
    }
    #endregion
}
