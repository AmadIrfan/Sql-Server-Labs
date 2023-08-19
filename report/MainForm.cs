using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace report
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void showChildForm(Object form ) {

            if (panel1.Controls.Count>0)
            {
                panel1.Controls.RemoveAt(0);
            }

            Form frm= (Form)form;
            frm.TopLevel = false;
           frm.FormBorderStyle= FormBorderStyle.None;
            frm.Dock= DockStyle.Fill;
            panel1.Controls.Add(frm);
            frm.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            showChildForm(new Form1());
        button1.Enabled= false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled= true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
