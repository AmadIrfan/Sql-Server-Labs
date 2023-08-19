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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SqlProject
{
    public partial class Attendance : Form
    {
        int dateId;
        int stuId;
        public Attendance()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClassAttendance ct = new ClassAttendance();
            ct.ShowDialog();
            classAttendance();

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Attendance_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            studentView.ReadOnly = true;
            classAttendance();
            student();
            selectesDateStudentData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtID.Clear();
                txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["RegistrationNumber"].Value.ToString();
                //getDataFromTable(textBox2.Text);
                getStudentDataFromTable(textBox2.Text);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }


        private void studentView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = studentView.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = studentView.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = studentView.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = studentView.CurrentRow.Cells[4].Value.ToString();

        }
        
        private void datePick_TextChanged(object sender, EventArgs e)
        {

            getDataFromTable(DateTime.Parse(datePick.Text));
            selectesDateStudentData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtID.Text) && !String.IsNullOrEmpty(comboBox1.Text) && comboBox1.Text != "Select...")
            {
                int status;
                switch (comboBox1.Text)
                {
                    case "Present":
                        status = 1;
                        break;
                    case "Leave":
                        status = 3;
                        break;
                    case "Late":
                        status = 4;
                        break;
                    default:
                        status = 2;
                        break;
                }
                DialogResult result = MessageBox.Show("Name :" + txtName.Text + "\n RegNo :" + textBox2.Text + " Status : " + comboBox1.Text + "\n  Are you sure you wnat to Mark Attendance  ", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    studentAttendance(status);
                    selectesDateStudentData();
                }
                else
                {
                    MessageBox.Show("Canceled", "Cancel", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }

            }
            else
            {
                MessageBox.Show("textBox is Empty", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }

        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtID.Text) && !String.IsNullOrEmpty(comboBox1.Text) && comboBox1.Text != "Select...")
            {
                int status;
            switch (comboBox1.Text)
            {
                case "Present":
                    status = 1;
                    break;
                case "Leave":
                    status = 3;
                    break;
                case "Late":
                    status = 4;
                    break;
                default:
                    status = 2;
                    break;
            }
            DialogResult result = MessageBox.Show("Name :" + txtName.Text + "\n RegNo :" + textBox2.Text + "\n Status : " + comboBox1.Text + "\n  Are you sure you wnat to Mark Attendance  ", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE StudentAttendance SET AttendanceStatus=@AttendanceStatus WHERE StudentId=@StudentId AND AttendanceId= @AttendanceId", con);
                cmd.Parameters.AddWithValue("@StudentId", int.Parse(txtID.Text));
                cmd.Parameters.AddWithValue("@AttendanceId", this.dateId);
                cmd.Parameters.AddWithValue("@AttendanceStatus", status);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successfully", "Update");
                    selectesDateStudentData();
            }
            else
            {
                MessageBox.Show("Canceled", "Cancel", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            }
            else
            {
                MessageBox.Show("textBox is Empty", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

        }
        private bool student()
        {
            try
            {
                dataGridView1.DataSource = null;
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("  SELECT CONCAT(s.FirstName,' ',s.LastName) Name, s.RegistrationNumber  From Student s  Join Lookup  on s.Status=Lookup.LookupId  WHERE Lookup.Name <>'InActive'; ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                return false;
            }

        }
        void classAttendance()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT (AttendanceDate) FROM ClassAttendance ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("AttendanceDate", typeof(String));
                da.Fill(dt);
                datePick.ValueMember = "AttendanceDate";
                datePick.DataSource = dt;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        void getDataFromTable(DateTime value1)
        {
            try
            {

                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ClassAttendance WHERE  AttendanceDate=@AttendanceDate ", con);
                cmd.Parameters.AddWithValue("AttendanceDate", value1);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    dateId = int.Parse(da.GetValue(0).ToString());
                    //      txtID.Text = dateId.ToString();

                }
                da.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }
        void getStudentDataFromTable(String value1)
        {
            try
            {

                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE RegistrationNumber =@RegistrationNumber ", con);
                cmd.Parameters.AddWithValue("RegistrationNumber", value1);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    stuId = int.Parse(da.GetValue(0).ToString());
                    txtID.Text = da.GetValue(0).ToString();
                }
                da.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void studentAttendance(int status)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentAttendance VALUES (@AttendanceId,@StudentId,@AttendanceStatus)", con);
                cmd.Parameters.AddWithValue("@AttendanceId", this.dateId);
                cmd.Parameters.AddWithValue("@StudentId", this.stuId);
                cmd.Parameters.AddWithValue("@AttendanceStatus", status);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }
        void selectesDateStudentData()
        {
            try
            {

                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand(
                    " SELECT s.Id,CONCAT(s.FirstName,' ',s.LastName)as Name  ,s.RegistrationNumber,s.Contact ,l.Name AS [Attendance Status] ,ca.AttendanceDate FROM Student s JOIN StudentAttendance sa ON sa.StudentId=s.id JOIN  ClassAttendance ca ON ca.Id=sa.AttendanceId JOIN Lookup l ON sa.AttendanceStatus=l.LookupId WHERE ca.AttendanceDate = @AttendanceDate AND s.Status=5 ", con);
                cmd.Parameters.AddWithValue("@AttendanceDate", DateTime.Parse(datePick.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                studentView.DataSource = dt;
                studentView.Refresh();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtName.Clear();
            textBox2.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void studentView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}