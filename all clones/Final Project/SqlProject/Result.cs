using CRUDA;
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
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace SqlProject
{
    public partial class Result : Form
    {
    int asId = -1;
        public Result()
        {
            InitializeComponent();
            comboBox4.ResetText();
            dataGridView1.ReadOnly=true;
            refresher();
        }

        private void materialTextBox21_Click(object sender, EventArgs e)
        {

        }

        private void Result_Load(object sender, EventArgs e)
        {
            getStudent();
            assessment();
            getRubricLevel();
        }

        private void getRubricLevel()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Details,Id FROM RubricLevel ;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("Details", typeof(String));
                da.Fill(dt);
                comboBox3.DisplayMember= "Details";
                comboBox3.ValueMember = "Id";
                comboBox3.DataSource = dt;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

    
        void getStudent()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT (RegistrationNumber) FROM Student WHERE status=5; ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("RegistrationNumber", typeof(String));
                da.Fill(dt);
                comboBox1.ValueMember = "RegistrationNumber";
                comboBox1.DataSource = dt;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        void assessment()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id,Title FROM Assessment ;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("Title", typeof(String));
                da.Fill(dt);
                comboBox2.DisplayMember = "Title";
                comboBox2.ValueMember = "Id";
                comboBox2.DataSource = dt;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        void getStudata(String value1)
        {
            try
            {

                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE  RegistrationNumber=@RegistrationNumber ", con);
                cmd.Parameters.AddWithValue("RegistrationNumber", value1);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtStuId.Text = da.GetValue(0).ToString();
                    txtStuName.Text = da.GetValue(01).ToString();
                }
                da.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }
        void getAssesmentComponent()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT AssessmentComponent.Id, AssessmentComponent.Name FROM AssessmentComponent JOIN Assessment on Assessment.Id=AssessmentComponent.AssessmentId  WHERE Assessment.Id=@Id ", con);
                cmd.Parameters.AddWithValue("@Id", int.Parse(comboBox2.SelectedValue.ToString()));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(String));
                da.Fill(dt);
                comboBox4.DisplayMember = "Name";
                comboBox4.ValueMember = "Id";
                comboBox4.DataSource = dt;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void btnPress_Click(object sender, EventArgs e)
        {
            getStudata(comboBox1.Text);
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            txtRubricMeasurment.Clear();
            txtStuId.Clear();
            txtStuName.Clear();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            txtRubricMeasurment.Clear();
            txtStuId.Clear();
            txtStuName.Clear();
        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            this.asId = int.Parse(comboBox2.SelectedValue.ToString());
            comboBox4.ResetText();

            getAssesmentComponent();
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            txtRubricMeasurment.Clear();
            txtRubricMeasurment.Text=comboBox3.SelectedValue.ToString();

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtStuId.Text) && !String.IsNullOrEmpty(txtStuName.Text) && !String.IsNullOrEmpty(txtRubricMeasurment.Text) && !String.IsNullOrEmpty(comboBox2.Text) && !String.IsNullOrEmpty(comboBox3.Text) && !String.IsNullOrEmpty(comboBox4.Text))
            {
            addStudentResult();

            }
            else
            {
                MessageBox.Show("Field to empty", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

        }
        private bool refresher()
        {
            dataGridView1.DataSource = null;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("\r\n  Select * From StudentResult", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            return true;

        }

        void addStudentResult()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into StudentResult values ( @StudentId, @AssessmentComponentId,@RubricMeasurementId,@EvaluationDate)", con);
                cmd.Parameters.AddWithValue("@StudentId", int.Parse(txtStuId.Text));
                cmd.Parameters.AddWithValue("@AssessmentComponentId", int.Parse(comboBox4.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@RubricMeasurementId", int.Parse(comboBox3.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@EvaluationDate", DateTime.Parse(dateTimePicker1.Value.ToString()));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");

                refresher();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void txtStuId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtRubricMeasurment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }

}
