using ABMProject.BL;
using ABMProject.DL;
using ABMProject.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMProject
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            SignUp sign=new SignUp();
            this.Hide();
            sign.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FPassword fPassword = new FPassword();
            this.Hide();
            fPassword.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               String email= textBox1.Text;
               String password= textBox2.Text;
                AuthDL a=new AuthDL();
           User u=a.login(email: email, password: password);
                if (u != null)
                {
                    if (u !=null && u.UserTypeId==1)
                    {
                       CustomerDashBoard custm=new CustomerDashBoard(u);
                        custm.Show();
                        this.Hide();
                    }
                    MessageBox.Show("User Found");
               
                    if (u != null && u.UserTypeId == 2)
                    {
                        //Employee
                    }
                    else if(u != null && u.UserTypeId == 3)
                    {
                        //Admin
                    MessageBox.Show("Admin Found");

                    }
                
                }
                else
                {
                    MessageBox.Show("User not Found");

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }
    }
}
