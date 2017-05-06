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

        FlowLayoutPanel oExisting = null;
        string panelDetail = string.Empty, clickedBtn = string.Empty;
        FontAwesome.Type type;
        Point lastPoint;
        public dynamic respJson { get; set; }
        public frmMaster(string userName, Bitmap userImage)
        {
            InitializeComponent();
            lblUserName.Text = userName;
            lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            pbAvatar.Image = userImage;
            pbAvatar.BackColor = pnlCentre.BackColor;
            opPreparemenu();
        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            // this.TopMost = true;
            Application.DoEvents();
            //    this.FormBorderStyle = FormBorderStyle.None;
            Left = Top = 0;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            WindowState = FormWindowState.Maximized;


        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void pbRestore_Click(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                int boundWidth = Screen.PrimaryScreen.Bounds.Width;
                int boundHeight = Screen.PrimaryScreen.Bounds.Height;
                int x = boundWidth - Width;
                int y = boundHeight - Height;
                Location = new Point(x / 2, y / 2);
                pb.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.WindowMaximize)
                { ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(222)))), ((int)(((byte)(222))))), Size = 12 });
            }

            else
            {
                WindowState = FormWindowState.Maximized;
                pb.Image = Properties.Resources.icon_Restore;
            }

        }
        private void pbMinimize_Click(object sender, EventArgs e)
        {

            WindowState = FormWindowState.Minimized;

        }
        public List<menuDetails> opGetMenubyParentId(short parentId)

        {
            List<menuDetails> oReturn = new List<Pidilite.menuDetails>();
            try
            {

                RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
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

            }
            return oReturn;
        }
        public string opGetParentMenu()
        {
            string ids = string.Empty;
            RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandTimeout = 60;
                    cmd.CommandText = "Select dbo.ufn_GetParentIdsofMenu ()";
                    cmd.CommandType = CommandType.Text;
                    var result = cmd.ExecuteScalar();
                    ids = Convert.ToString(result).Trim();
                }
            }
            return ids;
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
                btnMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(153)))));
                btnMenu.Height = 35;
                btnMenu.Width = 146;

                btnMenu.Font = RegistryConfig.myFontBold;
                btnMenu.Top = pnlMainMenu.Top;
                btnMenu.Left = pnlMainMenu.Left;
                btnMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                logo = omenu.logoClass;
                logo = logo.Replace("fa-", "");
                logo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(logo);
                logo = logo.Replace("-", "");
                type = new FontAwesome.Type();
                type = (FontAwesome.Type)Enum.Parse(typeof(FontAwesome.Type), logo);
                btnMenu.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(type)
                { ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(153))))), Size = 18 });
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
                btnMenu.Click += new System.EventHandler(btnMenu_Click);
                btnMenu.MouseEnter += new System.EventHandler(btnMenu_MouseEnter);
                btnMenu.MouseLeave += new System.EventHandler(btnMenu_MouseLeave);
                PictureBox pb = new PictureBox();
                pb.Width = 42;
                pb.BackColor = pnlMainMenu.BackColor;
                pb.Image = Properties.Resources.icon_plus;
                pb.Height = btnMenu.Height;
                pb.Margin = new Padding(0);

                //pb.Click += new System.EventHandler(this.btnMenu_Click);
                //pb.MouseEnter += new System.EventHandler(this.btnMenu_MouseEnter);
                //pb.MouseLeave += new System.EventHandler(this.btnMenu_MouseLeave);
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
                { ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(153))))), Size = 18 });
                button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
                button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(153)))));
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
                button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(30)))), ((int)(((byte)(43)))));
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
                        { ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(153))))), Size = 18 });
                        btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(40)))), ((int)(((byte)(56)))));
                        btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(153)))));
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
                oExisting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(30)))), ((int)(((byte)(43)))));
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
        private void opGenerateSubMenus(FlowLayoutPanel pnlMenu, List<menuDetails> omenudetails)
        {

            string logo = string.Empty;
            List<menuDetails> subMenu = omenudetails.Where(x => x.parentId != 0).ToList();
            subMenu = subMenu.OrderBy(x => x.ordering).ToList();
            foreach (var sub in subMenu)
            {
                Button btnSubMenu = new Button();
                btnSubMenu.BackColor = pnlMainMenu.BackColor;
                btnSubMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(153)))));
                btnSubMenu.Height = 28;

                btnSubMenu.Font = RegistryConfig.myFont;
                btnSubMenu.Width = pnlMainMenu.Width;
                btnSubMenu.Top = pnlMainMenu.Top;
                btnSubMenu.Left = pnlMainMenu.Left;
                btnSubMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
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
                btnSubMenu.Click += new System.EventHandler(btnSubMenu_Click);
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
                { ForeColor = System.Drawing.SystemColors.ControlDarkDark, Size = 30 });
            }
            lblBreadCrumb.Text = button.Text.ToUpper();
            lblBreadCrumb.Font = RegistryConfig.myBCFont;
            lblBreadCrumb.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            List<menuDetails> oSubmenuDetails = new List<menuDetails>();
            oSubmenuDetails = opGetMenubyParentId(Convert.ToInt16(button.Name));
            foreach (var sub in oSubmenuDetails)
            {
                Panel pnlBox = new Panel();
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
                pnlBox.Click += new System.EventHandler(pnlBox_Click);
                picBoxView.Click += new System.EventHandler(picBoxView_Click);
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

                pnlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                pnlBox.Margin = new System.Windows.Forms.Padding(60, 25, 60, 60);
                flPnlData.Controls.Add(pnlBox);
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
            lblBreadCrumb.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            Panel oPanel = new Panel();
            oPanel.Width = pnlBreadCrumb.Width - 40;
            oPanel.Height = pnlBreadCrumb.Height + 20;
            oPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            flPnlData.Controls.Add(oPanel);
            List<girdDetails> girdDetails = opCreatingSearchPanel(oValues, oPanel);
            opCreatingActionPanel(pb, oPanel);
            opConstructingGrid(oValues, oPanel, girdDetails);
        }

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
            oDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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

        private void opCreatingActionPanel(PictureBox pb, Panel oPanel)
        {
            FlowLayoutPanel oPnlAction = new FlowLayoutPanel();
            //  oPnlAction.Margin = new Padding(250, 3, 10, 3);
            oPnlAction.Dock = DockStyle.Right;
            oPnlAction.Width = 230;
            oPnlAction.AutoSize = false;
            oPnlAction.Height = pnlBreadCrumb.Height;
            oPnlAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right )));
           oPnlAction.Location = new System.Drawing.Point(1000, 5); ;
            oPanel.Controls.Add(oPnlAction);
            Button btnSearch = new Button();
            btnSearch.Name = "btnSearch";
            btnSearch.Height = 26;
            btnSearch.Width = 40;
            btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(94)))), ((int)(((byte)(0)))));
            btnSearch.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Search)
            { ForeColor = System.Drawing.Color.White, Size = 18 });
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.ImageAlign = ContentAlignment.TopCenter;
            btnSearch.Margin = new System.Windows.Forms.Padding(10, 3, 0, 0);
            btnSearch.Click += new System.EventHandler(btnSearch_Click);
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
            btnDownLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(154)))), ((int)(((byte)(207)))));
            btnDownLoad.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Download)
            { ForeColor = System.Drawing.Color.White, Size = 18 });
            btnDownLoad.FlatAppearance.BorderSize = 0;
            btnDownLoad.FlatStyle = FlatStyle.Flat;
            btnDownLoad.ImageAlign = ContentAlignment.TopCenter;
            btnDownLoad.Margin = new System.Windows.Forms.Padding(10, 3, 0, 0);
            btnDownLoad.Click += new System.EventHandler(btnDownLoad_Click);
            ToolTip toolTip2 = new ToolTip();
            toolTip2.AutoPopDelay = 3000;
            toolTip2.InitialDelay = 500;
            toolTip2.ReshowDelay = 500;
            toolTip2.ShowAlways = true;
            toolTip2.SetToolTip(btnDownLoad, "Download");
            oPnlAction.Controls.Add(btnDownLoad);
            Button btnDelete = new Button();
            btnDelete.Margin = new System.Windows.Forms.Padding(10, 3, 0, 0);
            btnDelete.Name = "btnDelete";
            btnDelete.Height = 26;
            btnDelete.Width = 40;
            btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(35)))), ((int)(((byte)(15)))));
            btnDelete.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.MinusCircle)
            { ForeColor = System.Drawing.Color.White, Size = 18 });
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ImageAlign = ContentAlignment.TopCenter;
            btnDelete.Click += new System.EventHandler(btnDelete_Click);
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
            btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(148)))), ((int)(((byte)(8)))));
            btnCreate.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.PlusCircle)
            { ForeColor = System.Drawing.Color.White, Size = 18 });
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.ImageAlign = ContentAlignment.TopCenter;
            btnCreate.Tag = pb.Tag;
            btnCreate.Margin = new System.Windows.Forms.Padding(10, 3, 0, 0);
            btnCreate.Click += new System.EventHandler(btnCreate_Click);
            ToolTip toolTip4 = new ToolTip();
            toolTip4.AutoPopDelay = 3000;
            toolTip4.InitialDelay = 500;
            toolTip4.ReshowDelay = 500;
            toolTip4.ShowAlways = true;
            toolTip4.SetToolTip(btnCreate, "Create");
            oPnlAction.Controls.Add(btnCreate);
        }

        private List<girdDetails> opCreatingSearchPanel(moduleValues oValues, Panel oPanel)
        {
            FlowLayoutPanel oPnlSearch = new FlowLayoutPanel();
            oPnlSearch.Width = 350;
            oPnlSearch.Height = pnlBreadCrumb.Height;
            oPnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
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
            btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            btnGo.ForeColor = Color.White;
            btnGo.Font = new System.Drawing.Font("Calibri", 12.00F, System.Drawing.FontStyle.Bold);
            btnGo.Name = "btnGo";
            btnGo.Text = "Go";
            btnGo.FlatAppearance.BorderSize = 0;
            btnGo.FlatStyle = FlatStyle.Flat;
            oPnlSearch.Controls.Add(btnGo);
            Button btnReset = new Button();
            btnReset.Name = "btnReset";
            btnReset.Height = 26;
            btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            btnReset.ForeColor = Color.White;
            btnReset.Font = new System.Drawing.Font("Calibri", 12.00F, System.Drawing.FontStyle.Bold);
            btnReset.Text = " Reset";
            btnReset.Image = FontAwesome.Instance.GetImage(new FontAwesome.Properties(FontAwesome.Type.Refresh)
            { ForeColor = System.Drawing.Color.White, Size = 16 });
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.ImageAlign = ContentAlignment.TopCenter;
            btnReset.TextAlign = ContentAlignment.MiddleCenter;
            btnReset.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReset.Click += new System.EventHandler(btnReset_Click);
            oPnlSearch.Controls.Add(btnReset);
            return girdDetails;
        }

        public DataTable opGridDataByModule(string queryString)
        {
            DataTable oGridData = new DataTable();
            RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
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
        private void btnCreate_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            moduleValues oModuleValues = new moduleValues();
            oModuleValues = opGetModuleDetailsByID(Convert.ToInt16(btn.Tag), true);

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
        private void pnlBox_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }
        private void CenterPictureBox(PictureBox picBox, Bitmap picImage)
        {
            picBox.Image = picImage;
            picBox.Location = new Point((picBox.Parent.ClientSize.Width / 2) - (picImage.Width / 2),
                                        (picBox.Parent.ClientSize.Height / 2) - (picImage.Height / 2));
            picBox.Refresh();
        }
        public moduleValues opGetModuleDetailsByID(int moduleId, bool isForm)
        {
            moduleValues oValues = null;
            DataTable dt = new DataTable();
            RegistryConfig.myConn = "Server=NOWAPPSLENOVO1\\SQLEXPRESS; Integrated security=SSPI;database=nxton_pidilite;User Id= sa;Password =sam@123";
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
        public string language { get; set; }
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
        //public Int16 readOnly {get;set;}
        public string access_server { get; set; }
        public short sortlist { get; set; }
        public string limited { get; set; }
        public JObject option { get; set; }
    }
}
