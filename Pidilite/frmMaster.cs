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

namespace Pidilite
{
    public partial class frmMaster : Form
    {
        #region [Data Members]
        FlowLayoutPanel oExisting = null;
        string panelDetail = string.Empty, clickedBtn = string.Empty;
        FontAwesome.Type type;
        Control ctrl = new Control();
        Label lbl = new Label();
        CirclePictureBox pbUpload = new CirclePictureBox();
        public dynamic respJson { get; set; }
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
            btnModule.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Refresh)
            { ForeColor = Color.White, Size = 16 });
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
                type = new FontAwesome.Type();
                type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), logo);
                btnMenu.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                { ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153))), Size = 18 });
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
                type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), button.Tag.ToString());
                button.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                { ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153))), Size = 18 });
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
                type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), button.Tag.ToString());
                button.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                { ForeColor = Color.White, Size = 18 });
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
                        type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), btn.Tag.ToString());
                        btn.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                        { ForeColor = Color.FromArgb(((byte)(107)), ((byte)(130)), ((byte)(153))), Size = 18 });
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
                type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), button.Tag.ToString());
                button.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                { ForeColor = Color.White, Size = 18 });
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
                    //type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), logo);
                    //btnSubMenu.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                    //{ ForeColor = Color.White, Size = 15 });
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
                type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), button.Tag.ToString());
                pbBreadCrumb.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                { ForeColor = SystemColors.ControlDarkDark, Size = 30 });
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
                    //type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), logo);
                    //btnSubMenu.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                    //{ ForeColor = Color.White, Size = 15 });
                    type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), logo);
                    Bitmap bt = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                    { ForeColor = Color.White, Size = 30 });
                    CenterPictureBox(picBoxView, bt);
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
            btnSearch.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Search)
            { ForeColor = Color.White, Size = 18 });
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
            btnDownLoad.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Download)
            { ForeColor = Color.White, Size = 18 });
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
            btnDelete.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.MinusCircle)
            { ForeColor = Color.White, Size = 18 });
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
            btnCreate.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.PlusCircle)
            { ForeColor = Color.White, Size = 18 });
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

        #region [Form Create]
        private void btnCreate_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            Panel oParentPnl = new Panel();
            oParentPnl = (btn.Parent.Parent.Parent as Panel);
            oParentPnl.Controls.Clear();
            oParentPnl.AutoScroll = true;
            oParentPnl.MouseWheel += new MouseEventHandler(oParentPnl_MouseWheel);
            moduleValues oModuleValues = new moduleValues();
            oModuleValues = opGetModuleDetailsByID(Convert.ToInt16(btn.Tag), true);
            List<formDetails> oFormFields = new List<formDetails>();
            JArray jForm = JsonConvert.DeserializeObject<dynamic>(oModuleValues.Form);
            List<formDetails> fieldDetails = new List<formDetails>();

            foreach (var obj in jForm)
            {
                fieldDetails = obj["fields"].ToObject<List<formDetails>>();

                FlowLayoutPanel ofpnl = new FlowLayoutPanel();
                if (Convert.ToString(obj["column"]) == "2")
                {
                    opFormWith2BlockFormation(btn, oParentPnl, fieldDetails, obj);
                }
                else
                {
                    opFormFormation(btn, oParentPnl, fieldDetails, obj, ofpnl);
                }

            }
            Panel oPnlCreate = new Panel();
            oPnlCreate.BackColor = Color.White;
            oPnlCreate.Width = oParentPnl.Width - 50;
            oPnlCreate.Height = 100;
            FlowLayoutPanel ofnlCreate = new FlowLayoutPanel();
            ofnlCreate.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            ofnlCreate.Location = new Point((oPnlCreate.Bounds.Width - 10) - ofnlCreate.Width, 0); ;
            ofnlCreate.FlowDirection = FlowDirection.LeftToRight;
            ofnlCreate.AutoSize = true;
            Button btnSave = new Button();
            btnSave.UseMnemonic = false;
            btnSave.Text = " Save & Continue";
            btnSave.Font = RegistryConfig.myFontBold;
            btnSave.AutoSize = true;
            btnSave.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.CheckCircleO)
            { ForeColor = Color.White, Size = 17 });
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
            btnClose.Font = RegistryConfig.myFontBold;
            btnClose.AutoSize = true;
            btnClose.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.FloppyO)
            { ForeColor = Color.White, Size = 17 });
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
            btnBack.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.ArrowCircleOLeft)
            { ForeColor = Color.White, Size = 17 });
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
                        }
                    }
                    dr.Close();
                }
            }
            return oValues;
        }
        private void opFormWith2BlockFormation(Button btn, Panel oParentPnl, List<formDetails> fieldDetails, JToken obj)
        {
            FlowLayoutPanel ofpBack = new FlowLayoutPanel();
            ofpBack.Width = oParentPnl.Width - 40;
            ofpBack.BackColor = SystemColors.ButtonFace;
            ofpBack.AutoScroll = true;
            oParentPnl.Controls.Add(ofpBack);

            TableLayoutPanel tPanel = new TableLayoutPanel();
            tPanel.ColumnCount = 2;
            tPanel.Width = oParentPnl.Width - 47;
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

            string[] strLabels = fieldDetails.Where(x => x.view == "1").Select(x => x.label).ToArray();
            int r = 0, r2 = 0;
            foreach (var detail in fieldDetails)
            {

                if (detail.type != "hidden" && detail.type != "formula_hidden" && detail.view == "1")
                {
                    lbl = new Label();
                    lbl.Text = detail.label;
                    if (detail.required == "required")
                        lbl.Text = lbl.Text + '*';
                    lbl.Font = RegistryConfig.myFont;
                    lbl.AutoSize = true;
                    ctrl = opDynamiccontrolSelection(btn, pbUpload, ctrl, detail);

                    if (detail.label != "")
                    {
                        if (detail.fieldWidth == "")
                            tPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Convert.ToSingle(ofpBack.Width / 2)));
                        else
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
                                tPanel.Controls.Add(lbl, 0, r2);
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
        private Control opDynamiccontrolSelection(Button btn, CirclePictureBox pbUpload, Control ctrl, formDetails detail)
        {
            switch (Convert.ToString(detail.type))
            {
                case "text":
                case "string":
                case "formula_string":
                case "normal_string":
                case "number":
                case "autoNumber":
                case "googlelSearch":

                    ctrl = new TextBox();
                    ctrl.Name = detail.field;
                    (ctrl as TextBox).Font = RegistryConfig.myFont;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as TextBox).ReadOnly = true;
                    }
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
                    }
                    break;
                case "text_datetime":
                    ctrl = new DateTimePicker();
                    ctrl.Name = detail.field;
                    (ctrl as DateTimePicker).Format = DateTimePickerFormat.Custom;
                    (ctrl as DateTimePicker).CustomFormat = "dd-MM-yyyy hh:mm";
                    (ctrl as DateTimePicker).Font = RegistryConfig.myFont;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as DateTimePicker).Enabled = true;
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
                    }
                    break;
                case "select":
                    ctrl = new ComboBox();
                    //ctrl.Click += new EventHandler(ctrl_Click);
                    ctrl.Tag = btn.Tag;
                    ctrl.Name = detail.field;
                    ctrl.Font = RegistryConfig.myFont;
                    if (Convert.ToString(detail.option["opt_type"]) == "external")
                    {
                        TableDetails oDetails = new TableDetails();
                        oDetails.TableName = Convert.ToString(detail.option["lookup_table"]);
                        oDetails.Key = Convert.ToString(detail.option["lookup_key"]);
                        oDetails.Value = Convert.ToString(detail.option["lookup_value"]);
                        DataTable cbValues = new DataTable();
                        cbValues = comboBoxValues(oDetails);

                        DataRow dr = cbValues.NewRow();
                        dr["Value"] = "--Select--";
                        dr["Id"] = -1;

                        cbValues.Rows.InsertAt(dr, 0);
                        (ctrl as ComboBox).DisplayMember = "Value";
                        (ctrl as ComboBox).ValueMember = "Id";
                        (ctrl as ComboBox).DataSource = cbValues;


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
                                           (ctrl as ComboBox).DropDownHeight = 100;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as ComboBox).Enabled = false;
                    }

                    break;
                case "radio":
                    ctrl = new RadioButton();
                    ctrl.Name = detail.field;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as RadioButton).Enabled = false;
                    }

                    break;
                case "checkbox":
                    ctrl = new CheckBox();
                    ctrl.Name = detail.field;
                    if (detail.wreadonly == true)
                    {
                        (ctrl as CheckBox).Enabled = false;
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
                    pbUpload.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Camera)
                    { ForeColor = Color.Gray });
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
                    }
                    break;


            }

            return ctrl;
        }
        private void opFormFormation(Button btn, Panel oParentPnl, List<formDetails> fieldDetails, JToken obj, FlowLayoutPanel ofpnl)
        {
            Panel opnlBack = new Panel();
            opnlBack.Width = oParentPnl.Width - 40;
            opnlBack.BackColor = SystemColors.ButtonFace;
            oParentPnl.Controls.Add(opnlBack);
            ofpnl.AutoSize = true;
            ofpnl.BackColor = SystemColors.ButtonFace;
            ofpnl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            opnlBack.Controls.Add(ofpnl);

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
                opSubGridControlFormations(btn, oParentPnl, fieldDetails, ofpnl);
            }
            else
            {
                opControlFormation(btn, fieldDetails, ofpnl, opnlBack);

            }
        }
        private void opSubGridControlFormations(Button btn, Panel oParentPnl, List<formDetails> fieldDetails, FlowLayoutPanel ofpnl)
        {
            ofpnl.FlowDirection = FlowDirection.LeftToRight;
            ofpnl.Width = oParentPnl.Width - 40;         
            ofpnl.WrapContents = false;
            TableLayoutPanel tblPnl = new TableLayoutPanel();
            tblPnl.AutoSize = true;
            string[] strLabels = fieldDetails.Where(x => x.view == "1").Select(x => x.label).ToArray();
            int c = 0;
            foreach (var detail in fieldDetails)
            {

                if (detail.type != "hidden" && detail.type != "formula_hidden" && detail.view == "1")
                {
                    lbl = new Label();
                    lbl.Text = detail.label;
                    if (detail.required == "required")
                        lbl.Text = lbl.Text + '*';
                    lbl.Font = RegistryConfig.myFont;
                    lbl.AutoSize = true;
                    ctrl = opDynamiccontrolSelection(btn, pbUpload, ctrl, detail);
                    tblPnl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    if (detail.fieldWidth == "")
                    {
                        tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
                    }
                    else
                    {
                        tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    }
                    tblPnl.Controls.Add(lbl, c, 0);
                    tblPnl.Controls.Add(ctrl, c, 1);
                    if (detail.fieldWidth == "")
                    {
                        ctrl.Width = 200;
                    }
                    else
                    {
                        TableLayoutPanelCellPosition pos = tblPnl.GetCellPosition(ctrl);
                        ctrl.Width = tblPnl.GetColumnWidths()[pos.Column] * Convert.ToInt32(detail.fieldWidth) / 100;
                    }
                    tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    c++;
                }
            }
            tblPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            
            PictureBox pbDelete = new PictureBox();
            pbDelete.Dock = DockStyle.Right;
            pbDelete.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.TrashO)
            { ForeColor = Color.DarkGray, Size = 18 });
            tblPnl.Controls.Add(pbDelete, c, 1);
            ofpnl.Controls.Add(tblPnl);
            ofpnl.AutoScroll = true;
          }
        private void opControlFormation(Button btn, List<formDetails> fieldDetails, FlowLayoutPanel ofpnl, Panel opnlBack)
        {
            ofpnl.FlowDirection = FlowDirection.TopDown;
            foreach (var detail in fieldDetails)
            {
                if (detail.type != "hidden" && detail.type != "formula_hidden" && detail.view == "1")
                {
                  
                   lbl = new Label();
                    lbl.Text = detail.label;
                    if (detail.required == "required")
                        lbl.Text = lbl.Text + '*';
                    lbl.AutoSize = true;
                    lbl.Font = RegistryConfig.myFont;
                    ofpnl.Controls.Add(lbl);
                    ctrl = opDynamiccontrolSelection(btn, pbUpload, ctrl, detail);
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
        private DataTable comboBoxValues(TableDetails oDetails)
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
                        cmd.CommandText = "usp_GetValuesforDropDown";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlda = new SqlDataAdapter();
                        cmd.Parameters.AddWithValue("@TableName", oDetails.TableName);
                        cmd.Parameters.AddWithValue("@Key", oDetails.Key);
                        cmd.Parameters.AddWithValue("@Value", oDetails.Value);
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
            btnReset.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Refresh)
            { ForeColor = Color.White, Size = 16 });
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
    }

    #region [Form & Grid Objects]
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
    #endregion
}
