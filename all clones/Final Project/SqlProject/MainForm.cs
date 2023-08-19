using CRUDA;
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

namespace SqlProject
{
    public partial class MainForm : Form
    {
        String[] formName= { "Welcome", "Student", "Assessment", "Student Attendance", "Clo's", "Rubric", "Assessment Componentes", "Rubric Level", "Student Result", "Generate Report" };
        public MainForm()
        {
            InitializeComponent();
            IsMdiContainer = true;

        }

        void showChildForm(object form)
        {
            Form frm= (Form)form;

            if (this.panel1.Controls.Count>0)
            {
                this.panel1.Controls.RemoveAt(0);
            }

            frm.TopLevel = false;
            DoubleBuffered= true;
            frm.Dock= DockStyle.Fill;
            frm.FormBorderStyle= FormBorderStyle.None;
            this.panel1.Controls.Add(frm);
            frm.Show();

        }
  

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblTopBar.Text = formName[0];
            showChildForm(new SplashForm());
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            lblTopBar.Text = formName[1];
            showChildForm(new Form1());
        }

       
        private void button5_Click(object sender, EventArgs e)
        {

            lblTopBar.Text = formName[2];
            showChildForm(new Assessment());
        }

        private void button6_Click(object sender, EventArgs e)
        {

            lblTopBar.Text = formName[3];
            showChildForm(new Attendance());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lblTopBar.Text = formName[4];
            showChildForm(new Clo());
        }

        private void button3_Click(object sender, EventArgs e)
        {

            lblTopBar.Text = formName[5];
            showChildForm(new Rubic());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblTopBar.Text = formName[6];
            showChildForm(new AssessmentComponent());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblTopBar.Text = formName[7];
            showChildForm(new RubricLevel());

        }

        private void button8_Click(object sender, EventArgs e)
        {
            lblTopBar.Text = formName[8];
            showChildForm(new Result());


        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you Sure you want to close Application??? ", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Close();
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            lblTopBar.Text = formName[9];
            showChildForm(new Report());
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            showChildForm(new SplashForm());
        }
    }

}
