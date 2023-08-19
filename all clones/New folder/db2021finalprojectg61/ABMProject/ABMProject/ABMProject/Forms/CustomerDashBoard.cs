using ABMProject.BL;
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
    public partial class CustomerDashBoard : Form
    {
        User u;
        public CustomerDashBoard(User user)
        {
            this.u = user;

            /*            Console.WriteLine(u.FName + " " + u.LName);
            */

            InitializeComponent();
            lblCName.Text = u.FName+" "+u.LName;
        }

        void showChildForm(object form)
        {
            Form frm = (Form)form;

            if (this.mainPanel.Controls.Count > 0)
            {
                this.mainPanel.Controls.RemoveAt(0);
            }

            frm.TopLevel = false;
            DoubleBuffered = true;
            frm.Dock = DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.mainPanel.Controls.Add(frm);
            frm.Show();

        }

        private void CustomerDashBoard_Load(object sender, EventArgs e)
        {
            LblTopText.Text = "Dashboard";
            showChildForm(new SplashScreen());
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
                    }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Log out ???","Question",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Hide();
                LogIn log = new LogIn();
                log.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LblTopText.Text = "Beneficiary";
            showChildForm(new Beneficiary(u));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LblTopText.Text = "Account";

            showChildForm(new Account(u));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
