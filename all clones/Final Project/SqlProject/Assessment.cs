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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlProject
{
    public partial class Assessment : Form
    {
        int sId = 0;
        public Assessment()
        {
            InitializeComponent();
         
        }

        private void Components_Load(object sender, EventArgs e)
        {
            refresher();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtTMarks.Clear();
            txtTWeightage.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTitle.Text) && !String.IsNullOrEmpty(txtTMarks.Text) && !String.IsNullOrWhiteSpace(txtTWeightage.Text))
            {
                String result = saveAssessment();

                if (result == "ok")
                {
                    MessageBox.Show("Successfully saved");
                    refresher();

                }
                else
                {
                    MessageBox.Show(result, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else
            {

                MessageBox.Show("Fill all text boxes... ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }

       private String saveAssessment()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into Assessment values ( @Title,@DateCreated,@TotalMarks,@TotalWeightage)", con);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", txtTMarks.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", txtTWeightage.Text);
                cmd.Parameters.AddWithValue("@DateCreated",DateTime.Parse(pickDateTime.Text));
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception exp)
            {
                return exp.Message.ToString();
            }
        }
        private bool refresher()
        {
            try
            {

            AssessmentView.DataSource = null;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            AssessmentView.DataSource = dt;
            AssessmentView.Refresh();
            return true;
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message,"Error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                return false;
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTitle.Text) && !String.IsNullOrEmpty(txtTMarks.Text) && !String.IsNullOrWhiteSpace(txtTWeightage.Text))
            {
                String result = updateAssessment();

                if (result == "ok")
                {
                    MessageBox.Show("Successfully update");
                    refresher();

                }
                else
                {
                    MessageBox.Show(result, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else
            {

                MessageBox.Show("Fill all text boxes... ", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private string updateAssessment()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE  Assessment SET Title=@Title,DateCreated=@DateCreated,TotalMarks=@TotalMarks,TotalWeightage=@TotalWeightage WHERE @Id = Id", con);
                cmd.Parameters.AddWithValue("@Id", sId);

                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", txtTMarks.Text);
                cmd.Parameters.AddWithValue("@TotalWeightage", txtTWeightage.Text);
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Parse(pickDateTime.Text));
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception exp)
            {
                return exp.Message.ToString();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
            if (!String.IsNullOrEmpty(txtTitle.Text) && !String.IsNullOrEmpty(txtTMarks.Text) && !String.IsNullOrWhiteSpace(txtTWeightage.Text))
                {

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("delete from Assessment WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", sId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted  ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    refresher();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Error");
            }

        }

        private void AssessmentView_Click(object sender, EventArgs e)
        {
            try
            {
                
                sId = int.Parse(AssessmentView.CurrentRow.Cells[0].Value.ToString());
                txtTitle.Text = AssessmentView.CurrentRow.Cells[1].Value.ToString();
                pickDateTime.Text = DateTime.Parse(AssessmentView.CurrentRow.Cells[2].Value.ToString()).ToString();
                txtTMarks.Text = AssessmentView.CurrentRow.Cells[3].Value.ToString();
                txtTWeightage.Text = AssessmentView.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void txtTMarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtTWeightage_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTWeightage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
