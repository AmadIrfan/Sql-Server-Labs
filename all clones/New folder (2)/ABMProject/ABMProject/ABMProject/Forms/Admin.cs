using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMProject.Forms
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnaddcustomer_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Customer add = new Add_Customer();
            add.Show();
        }

        private void btnaddemployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Employee adde = new Add_Employee();
            adde.Show();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn log = new LogIn();
            log.Show();
        }

        private void btncheckbalance_Click(object sender, EventArgs e)
        {
            this.Hide();
            Check_Balance check = new Check_Balance();
            check.Show();
        }
    }
}
