using ABMProject.BL;
using ABMProject.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMProject
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
       
            Console.WriteLine(generateID());
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogIn lg=new LogIn();
            this.Hide();
            lg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String userId = generateID();
                int userTypeId = 1;
                String email = textBox1.Text;
                String password = textBox2.Text;
                String fName = textBox3.Text;
                String lName = textBox4.Text;
                String address = textBox5.Text;
                String phone = textBox6.Text;
                DateTime dob = DateTime.Parse(dateTimePicker1.Value.ToString());
                DateTime createDate = DateTime.Now;
                bool validPhone=validContactnumber(textBox6.Text);
                Console.WriteLine(validPhone);
                bool validemail=validEmail(textBox1.Text);
                Console.WriteLine(validemail);
                if ( validemail && validPhone && !String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text) && !String.IsNullOrEmpty(textBox5.Text) && !String.IsNullOrEmpty(textBox6.Text))
                {

                

                User user= new User(1,fName,lName,userId,address,createDate,dob,email,userTypeId,password,phone);
                AuthDL a =new AuthDL();
                a.registerUser(user);
                CustomerDL customer = new CustomerDL();
                customer.addCustomer(user.Email,user.Password);
                MessageBox.Show("Successfully saved");
                }
                else
                {
                MessageBox.Show("Text Field is Empty ....","error",MessageBoxButtons.OKCancel,MessageBoxIcon.Stop);

                }
            }
            catch (Exception exp)
            {
/*                Console.WriteLine(exp.ToString());*/
                MessageBox.Show(exp.ToString(),"error",MessageBoxButtons.OKCancel,MessageBoxIcon.Stop);
            }
        }
        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        
            /*if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }*/
        bool validEmail(string email)
        {
            bool isValidEmail = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (isValidEmail)
            {
                return true;
            }
            return false;
        }

        bool validContactnumber(string contact)
        {
            bool isValidPhoneNumber = Regex.IsMatch(contact, @"^\+92\d{10}$|^0\d{10}$");

            if (isValidPhoneNumber)
            {
                return true;
            }
            return false;
        }


    }
}
