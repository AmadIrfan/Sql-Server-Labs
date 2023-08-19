using CRUDA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace SqlProject
{
    public partial class RubricLevel : Form
    {
        int id = -1;
        public RubricLevel()
        {
            InitializeComponent();
            refresher();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into RubricLevel values ( @RubricId, @Details,@MeasurementLevel)", con);
                cmd.Parameters.AddWithValue("@RubricId",int.Parse(cBox1.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@Details",richTextBox1.Text);
                cmd.Parameters.AddWithValue("@MeasurementLevel", int.Parse(comboBox1.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Added", "Added", MessageBoxButtons.OKCancel);
                refresher();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(comboBox1.Text) && !String.IsNullOrEmpty(richTextBox1.Text) && !String.IsNullOrEmpty(cBox1.SelectedValue.ToString()))
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("delete FROM RubricLevel WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", int.Parse(cBox1.SelectedValue.ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete item ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    refresher();
                  }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error");
            }
        }

        private void RubricLevel_Load(object sender, EventArgs e)
        {

            rubric();        
        }
        private bool refresher()
        {
            studentView.DataSource = null;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * From RubricLevel", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            studentView.DataSource = dt;
            studentView.Refresh();
            return true;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            richTextBox1.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(comboBox1.Text) && !String.IsNullOrEmpty(richTextBox1.Text) && !String.IsNullOrEmpty(cBox1.SelectedValue.ToString()))
                {

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("UPDATE RubricLevel SET  RubricId=@RubricId, Details=@Details,MeasurementLevel=@MeasurementLevel WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Details", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@MeasurementLevel", int.Parse(comboBox1.Text));
                    cmd.Parameters.AddWithValue("@RubricId", int.Parse(cBox1.SelectedValue.ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully UpDated", "Update");
                    refresher();
                }
                else
                {
                    MessageBox.Show("Fill all text box", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }

        }
        void rubric()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Rubric ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(String));
                da.Fill(dt);
                cBox1.ValueMember = "Id";
                cBox1.DataSource = dt;
             
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }
   
        private void cBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getIdFromTable();
        }
        void getIdFromTable()
        {
            try
            {

                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Details FROM Rubric WHERE Id=@Id ", con);
                cmd.Parameters.AddWithValue("@Id", cBox1.SelectedValue);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    textBox1.Text = da.GetValue(0).ToString();
              
                }
                da.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void studentView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            int id = 0;
            id = int.Parse(studentView.CurrentRow.Cells["Id"].Value.ToString());
            cBox1.Text = studentView.CurrentRow.Cells["RubricId"].Value.ToString();
            richTextBox1.Text = studentView.CurrentRow.Cells["Details"].Value.ToString();
            comboBox1.Text = studentView.CurrentRow.Cells["MeasurementLevel"].Value.ToString();
            this.id = id;
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
   
    }
}
