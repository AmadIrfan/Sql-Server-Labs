using ABMProject.BL;
using ABMProject.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ABMProject.Forms
{
    public partial class CustEmail : Form
    {
        User user;
        public CustEmail(User user)
        {
            this.user = user;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                String mail = textBox1.Text;
                String subject = textBox2.Text;
                String body = textBox3.Text;
                DateTime time= DateTime.Now;
                DialogResult result = MessageBox.Show("Are you sure you want to send email. ", "Send Email ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    Email email =new Email(mail,user.Id,subject,body,time);
                    EmailDL emailDL = new EmailDL();
                    MessageBox.Show("Sent ", "Send Email ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show("Canceled ", "Send Email ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.None);
            }


        }
    }
}
