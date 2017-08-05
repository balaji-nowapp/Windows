using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nxton
{
    public partial class ReportViewer : Form
    {
        public ReportViewer()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            print crystalReport = new print();
            JObject jObj = GetData();
            crystalReport.SetDataSource(jObj);
            this.crystalReportViewer1.ReportSource = crystalReport;
            this.crystalReportViewer1.RefreshReport();
        }

        private JObject GetData()
        {
            DataSet dsMain = new DataSet();
            using (SqlConnection con = new SqlConnection(RegistryConfig.myConn))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 20 * FROM Customers"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        JObject jObj = new JObject();
                        sda.Fill(dsMain, "DataTable1");
                        string json = JsonConvert.SerializeObject(dsMain, Formatting.Indented);
                        jObj =JsonConvert.DeserializeObject<JObject>(json);
                        return jObj;

                    }
                }
            }
        }
    }

}
