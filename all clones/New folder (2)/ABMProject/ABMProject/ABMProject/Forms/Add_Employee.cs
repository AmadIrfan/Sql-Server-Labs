using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMProject.Forms
{
    public partial class Add_Employee : Form
    {
        public Add_Employee()
        {
            InitializeComponent();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn n = new LogIn();
            n.Show();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {

        }

        private void showcustomerGRID_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (showcustomerGRID.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = showcustomerGRID.SelectedRows[0];
                txtfirstname.Text = selectedRow.Cells[1].Value.ToString();
                txtlastname.Text = selectedRow.Cells[2].Value.ToString();
                txtaddress.Text = selectedRow.Cells[3].Value.ToString();
                txtemail.Text = selectedRow.Cells[5].Value.ToString();
                txtpassword.Text = selectedRow.Cells[6].Value.ToString();
                txtphonenumber.Text = selectedRow.Cells[7].Value.ToString();
                dateofbirth.Text = selectedRow.Cells[8].Value.ToString();
            }
        }
    }
}
