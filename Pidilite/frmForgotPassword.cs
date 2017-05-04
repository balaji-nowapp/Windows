using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pidilite
{
    public partial class frmForgotPassword : Form
    {
        public frmForgotPassword()
        {
            InitializeComponent();
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frmLgn = new Pidilite.frmLogin();
            frmLgn.ShowDialog();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

        }

      
    }
}
