using CRUDA;
using SqlProject.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SqlProject
{
    public partial class Form1 : Form
    {
        String sId = "";
            public Form1()
        {
            InitializeComponent();
            refresher();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtContact.Clear();
            txtEmail.Clear();
            txtFName.Clear();
            txtYear.Clear();
            txtBoxNo.Clear();
            txtLName.Clear();
            cBox1.Text= "Select...";
            cBoxDept.Text= "Select...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
         if (txtFName.Text != ""&& txtYear.Text != "" && txtEmail.Text != ""&& cBox1.Text != "Select..." && cBoxDept.Text != "Select..." && txtBoxNo.Text != ""&& validEmail(txtEmail.Text) && validContactnumber(txtContact.Text))
            {
                String contant = txtContact.Text;
                String email = txtEmail.Text;
                String fName = txtFName.Text;
                String regYear = txtYear.Text;
                String dept=cBoxDept.Text;
                String rNo = txtBoxNo.Text;
                String lName = txtLName.Text;
                int status = cBox1.Text == "Active" ? 5 : 6;
                String regNumber =regYear+"-"+dept+"-"+rNo ; 
              String result= addStudents(sId,fName,lName,contant,email,regNumber,status);
                if (result=="ok")
                {
                   refresher();

                }
                else if (!validEmail(txtEmail.Text) || !validContactnumber(txtContact.Text))
                {

                    MessageBox.Show("Enter valid email or phonenumber", "Valid", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Fill all text boxes", "Valid", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Empty","Fill all Inputs.....",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            }
        }
        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        private String addStudents(String id, String fName, String lName, String contant, String email, String regNo, int status )
        {
            try
            { 
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Student values ( @FirstName, @LastName,@Contact,@Email,@RegistrationNumber,@Status)", con);
          //  cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@FirstName", fName);
            cmd.Parameters.AddWithValue("@LastName",lName);
            cmd.Parameters.AddWithValue("@Contact", contant);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@RegistrationNumber", regNo);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");

                return "ok";
             }
            catch (Exception exp) {
                 return exp.Message.ToString();
            }
        }
        private bool refresher()
        {
            studentView.DataSource = null;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("\r\n  Select s.Id ,s.FirstName,s.LastName,s.Contact,s.Email,s.RegistrationNumber,l.Name as Status from Student s JOIN Lookup l On s.Status=l.LookupId ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            studentView.DataSource = dt;
            studentView.Refresh();
            return true;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void studentView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        
            String id= studentView.CurrentRow.Cells["Id"].Value.ToString();
            this.sId= id;
                try
                {

                    SqlConnection con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE Id =@Id ", con);
                    cmd.Parameters.AddWithValue("Id", int.Parse(id));
                    SqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                    //  (da.GetValue(0).ToString());
                       txtFName.Text=(da.GetValue(1).ToString());
                       txtLName.Text=(da.GetValue(2).ToString());
                       txtContact.Text=(da.GetValue(3).ToString());
                       txtEmail.Text = (da.GetValue(4).ToString());
                    String[] regNo=da.GetValue(5).ToString().Split('-');
                    txtBoxNo.Text = regNo[2];
                    cBoxDept.Text = regNo[1];
                    txtYear.Text = regNo[0];

                   int statusCode=int.Parse(da.GetValue(6).ToString());
                    String status = statusCode == 5 ? "Active" : "InActive";
                    cBox1.Text = status;
                }
                da.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
           
                /*String contant = studentView.CurrentRow.Cells["Contact"].Value.ToString();
                String email = studentView.CurrentRow.Cells["Email"].Value.ToString();
                String fName = studentView.CurrentRow.Cells["FirstName"].Value.ToString();
                String[] regNo = studentView.CurrentRow.Cells["RegistrationNumber"].Value.ToString().Split('-');
                String lName = studentView.CurrentRow.Cells["LastName"].Value.ToString();
                int statusCode = studentView.CurrentRow.Cells["Status"].Value.ToString() == "" ? 0 : int.Parse(studentView.CurrentRow.Cells["Status"].Value.ToString());
                */
            /*     String status = statusCode == 5 ? "Active":"InActive" ;
                 txtBoxNo.Text =regNo[2];
                 cBoxDept.Text =regNo[1];
                 txtYear.Text =regNo[0];
                 txtLName.Text = lName;
                 txtFName.Text = fName;
                 txtEmail.Text = email;
                 txtContact.Text = contant;
                 cBox1.Text = status;*/
           //     getStudentDataFromTable(int.Parse(id));

           // }
           // catch(Exception exp)
           // {
           //     MessageBox.Show(exp.Message.ToString(), "Error");

           // }
        }
        private String updateStudents(String id, String fName, String lName, String contant, String email, String regNo, int status)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE Student SET  FirstName=@FirstName, LastName=@LastName,Contact=@Contact,Email=@Email,RegistrationNumber=@RegistrationNumber,Status=@Status WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FirstName", fName);
                cmd.Parameters.AddWithValue("@LastName", lName);
                cmd.Parameters.AddWithValue("@Contact", contant);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@RegistrationNumber", regNo);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception exp)
            {
            return exp.Message.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txtFName.Text != "" && txtYear.Text != "" && txtEmail.Text != ""  && cBox1.Text != "Select..." && cBoxDept.Text != "Select..." && txtBoxNo.Text != "" && validEmail(txtEmail.Text) && validContactnumber(txtContact.Text))
            {
                String contant = txtContact.Text;
                String email = txtEmail.Text;
                String fName = txtFName.Text;
                String regYear = txtYear.Text;
                String dept = cBoxDept.Text;
                String rNo = txtBoxNo.Text;
                String lName = txtLName.Text;
                int status = cBox1.Text == "Active" ? 5 : 6;
                String regNumber = regYear + "-" + dept + "-" + rNo;
             String result=   updateStudents(sId, fName, lName, contant, email, regNumber, status);
                if (result=="ok")
                {
                MessageBox.Show("Data Updated", "save");
                refresher();

                }
                else
                {

                MessageBox.Show(result, "Error");
                }
            }
            else if (!validEmail(txtEmail.Text) || !validContactnumber(txtContact.Text))
            {

                MessageBox.Show("Enter valid email or phonenumber", "Valid", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Fill all text boxes", "Valid", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFName.Text != "" && txtYear.Text != "" && txtEmail.Text != "" && cBox1.Text != "Select..." && cBoxDept.Text != "Select..." && txtBoxNo.Text != "")
                {

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("delete from Student WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", sId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete item ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    refresher();
                    txtContact.Clear();
                    txtEmail.Clear();
                    txtFName.Clear();
                    txtYear.Clear();
                    txtBoxNo.Clear();
                    txtLName.Clear();
                    cBox1.Text = "Select...";
                    cBoxDept.Text = "Select...";
                }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(),"Error");
            }
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {
            txtContact.MaxLength = 13;
            
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            txtYear.MaxLength = 4;
        }

        private void txtFName_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        void getStudentDataFromTable(int value1)
        {
            try
            {

                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE Id =@Id ", con);
                cmd.Parameters.AddWithValue("Id", value1);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                     Console.WriteLine( da.GetValue(0).ToString());
                     Console.WriteLine( da.GetValue(1).ToString());
                     Console.WriteLine( da.GetValue(2).ToString());
                     Console.WriteLine( da.GetValue(3).ToString());
                     Console.WriteLine( da.GetValue(4).ToString());
                     Console.WriteLine( da.GetValue(5).ToString());
                     Console.WriteLine( da.GetValue(6).ToString());
                    }
                da.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtBoxNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
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

        private void button1_Click_1(object sender, EventArgs e)
        {
          
        }
    }
}
