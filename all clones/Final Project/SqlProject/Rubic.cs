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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlProject
{
    public partial class Rubic : Form
    {
        int cloId = -1;
        int rubId = -1;
        int rubBackId = -1;
        public Rubic()
        {
            InitializeComponent();
        }

        private void Rubic_Load(object sender, EventArgs e)
        {
            showClo();
            reFresh();
        }

        private void showClo()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT (Name) FROM Clo ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(String));
                da.Fill(dt);
                cBoxClo.ValueMember = "Name";
                cBoxClo.DataSource = dt;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

       
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(richTextBox1.Text) && cloId>=0)
            {
                addStudents();

            }
            else
            {
                MessageBox.Show("Empty Fields","Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void addStudents()
        {
            try
            {
                int noOfRow = rubricView.RowCount;
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Rubric values (@Id,@Details,@CLoId)", con);
                cmd.Parameters.AddWithValue("@Details",richTextBox1.Text);
                cmd.Parameters.AddWithValue("@CloId",cloId);
                cmd.Parameters.AddWithValue("@Id",(noOfRow));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully","Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                reFresh();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }

        private void cBoxClo_TextChanged(object sender, EventArgs e)
        {
            getCloFromTable(cBoxClo.Text);
        }
        void getCloFromTable(String value1)
        {
            try
            {

                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clo WHERE  Name=@Name ", con);
                cmd.Parameters.AddWithValue("Name", value1);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    cloId = int.Parse(da.GetValue(0).ToString());
                    textBox1.Text = cloId.ToString();
                    Console.WriteLine(cloId);
                }
                da.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        void reFresh()
        {
            SqlConnection con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Rubric", con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rubricView.DataSource = dt;
            rubricView.Refresh();
            
        }

        private void studentView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!String.IsNullOrEmpty(rubricView.CurrentRow.Cells["Id"].Value.ToString()))
            {

            if (e.ColumnIndex == rubricView.Columns["btnCloView"].Index)  {
                    int id= int.Parse(rubricView.CurrentRow.Cells["CloId"].Value.ToString());
                    DetailPage page = new DetailPage(id:id ,pageName:"Clo Details",tableName:"Clo" );
                    page.Show();
            }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
            SqlConnection con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE Rubric SET Id=@Id,Details=@Details,CLoId=@CLoId WHERE Id=@Id ", con);
            cmd.Parameters.AddWithValue("@Id", rubBackId);
            cmd.Parameters.AddWithValue("@Details", richTextBox1.Text);
            cmd.Parameters.AddWithValue("@CLoId", cloId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Update","Update");
                reFresh();
            }
            catch(Exception exp)
            {
            MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void rubricView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            rubId = int.Parse(rubricView.CurrentRow.Cells["Id"].Value.ToString());
            richTextBox1.Text = rubricView.CurrentRow.Cells["Details"].Value.ToString();
            rubBackId = int.Parse(rubricView.CurrentRow.Cells["CloId"].Value.ToString());
            idBase();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        void idBase()
        {
            String text = "";
            try
            {

                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clo WHERE  Id=@id ", con);
                cmd.Parameters.AddWithValue("@Id",rubBackId);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    text = da.GetValue(01).ToString();
              
                }
                da.Close();
                cBoxClo.Text = text;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from Rubric WHERE Id=@Id", con);
            cmd.Parameters.AddWithValue("@Id", rubId);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Delete item ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            reFresh();

        }

        private void rubricView_RowHeightInfoPushed(object sender, DataGridViewRowHeightInfoPushedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            textBox1.Clear();
        }
    }
}
