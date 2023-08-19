using ABMProject.BL;
using ABMProject.DL;
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

namespace ABMProject.Forms
{
    public partial class Account : Form
    {
        User user;
        public Account(User u)
        {
            this.user = u;
            InitializeComponent();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            dataView();

        }
       void dataView()
        {
            try
            {

                AccountDL acd = new AccountDL();
                SqlDataReader da = acd.accountDetails(user.Id);
                while (da.Read())
                {
                    textBox6.Text=da.GetValue(0).ToString();
                    textBox1.Text=da.GetValue(6).ToString();
                    textBox7.Text=da.GetValue(8).ToString();
                    dateTimePicker2.Text=da.GetValue(7).ToString();
                    textBox2.Text=da.GetValue(5).ToString();
                    textBox3.Text=da.GetValue(1).ToString();
                    textBox4.Text=da.GetValue(2).ToString();
                    textBox5.Text=da.GetValue(3).ToString();

                    dateTimePicker1.Text=da.GetValue(4).ToString();
                }
                da.Close();
            }
            catch (Exception exp) 
            {
                MessageBox.Show(exp.Message.ToString(),"Error");
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
