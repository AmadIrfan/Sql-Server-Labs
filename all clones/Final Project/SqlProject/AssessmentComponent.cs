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
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace SqlProject
{
    public partial class AssessmentComponent : Form
    {
        int id=-1;
        int rubricid=-1;
        int asId = -1;
        DateTime cDate = DateTime.Now;
        public AssessmentComponent()
        {
            InitializeComponent();
            refresher();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text) && !String.IsNullOrEmpty(txtTotalMarks.Text) &&  !String.IsNullOrEmpty(cBoxAssessmentId.Text) && !String.IsNullOrEmpty(comboBoxRubric.Text))
            {
                addAssessmentComponentes();
            }
            else
            {

            }

        }
        private void addAssessmentComponentes()
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into AssessmentComponent values (@Name,@RubricId,@TotalMarks,@DateCreated,@DateUpdated,@AssessmentId)", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(txtTotalMarks.Text));
                cmd.Parameters.AddWithValue("@DateCreated",DateTime.Now);
                cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                cmd.Parameters.AddWithValue("@AssessmentId", asId);
                cmd.Parameters.AddWithValue("@RubricId",int.Parse(comboBoxRubric.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Added","Added");
                refresher();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(),"Error");
            }
        }
        private bool refresher()
        {

            assessmentView.DataSource = null;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * FROM AssessmentComponent", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            assessmentView.DataSource = dt;
            assessmentView.Refresh();
            return true;

        }

    
        private void AssessmentComponent_Load(object sender, EventArgs e)
        {
            assessments();
            rubric();
        }
        void assessments()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT(Title) FROM Assessment ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("Title", typeof(String));
                da.Fill(dt);
                cBoxAssessmentId.ValueMember = "Title";
                cBoxAssessmentId.DataSource = dt;
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
                comboBoxRubric.ValueMember = "Id";
                comboBoxRubric.DataSource = dt;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        void getIdFromTable(String value1)
        {
            try
            {

                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Assessment WHERE Title=@Title ", con);
                cmd.Parameters.AddWithValue("@Title", value1);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    asId = int.Parse(da.GetValue(0).ToString());
                    Console.WriteLine(asId);
                }
                da.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtTotalMarks.Clear();
        }

        private void assessmentView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (!String.IsNullOrEmpty(assessmentView.CurrentRow.Cells[1].Value.ToString()))
            {

                if (e.ColumnIndex == assessmentView.Columns["btnAssessmentDetail"].Index)
                {
                    MessageBox.Show("","");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime date= DateTime.Now;
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE AssessmentComponent SET Name=@Name,RubricId=@RubricId,TotalMarks=@TotalMarks,DateCreated=@DateCreated,DateUpdated=@DateUpdated,AssessmentId=@AssessmentId WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(txtTotalMarks.Text));
                cmd.Parameters.AddWithValue("@DateCreated", cDate);
                cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                cmd.Parameters.AddWithValue("@AssessmentId", asId);
                cmd.Parameters.AddWithValue("@RubricId", int.Parse(comboBoxRubric.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully UpDated", "Update");
                refresher();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }

        private void assessmentView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            int id = 0, rubricId=0,assessmentId=0;
            txtName.Text = assessmentView.CurrentRow.Cells["Name"].Value.ToString();
            txtTotalMarks.Text = assessmentView.CurrentRow.Cells["TotalMarks"].Value.ToString();
            id= int.Parse(assessmentView.CurrentRow.Cells["Id"].Value.ToString());
            rubricId=int.Parse(assessmentView.CurrentRow.Cells["RubricId"].Value.ToString());
            assessmentId= int.Parse(assessmentView.CurrentRow.Cells["AssessmentId"].Value.ToString());
            cDate =DateTime.Parse(assessmentView.CurrentRow.Cells["DateCreated"].Value.ToString());
            this.id = id;
            this.rubricid=rubricId;
            this.asId= assessmentId;
         
         /*
             SqlConnection con = Configuration.getInstance().getConnection();
             SqlCommand cmd = new SqlCommand("SELECT Title FROM Assessment WHERE Id=@Id ", con);
             cmd.Parameters.AddWithValue("@Id", assessmentId);
             SqlDataReader da = cmd.ExecuteReader();
             while (da.Read())
             {
                  String txt = da.GetValue(0).ToString();
                  cBoxAssessmentId.Text = txt;
             }
              da.Close();
        */
         
            }catch(Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }
/*        void getTextFromTable(int value1)
        {
            try
            {
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
*/
  //      }

        private void cBoxAssessmentId_SelectedValueChanged(object sender, EventArgs e)
        {
            getIdFromTable(cBoxAssessmentId.Text);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtName.Text) && !String.IsNullOrEmpty(txtTotalMarks.Text))
                {

                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("delete from AssessmentComponent WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete item ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    refresher();
                    txtName.Clear();
                    txtTotalMarks.Clear();
                }
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error");
            }
        }

        private void txtTotalMarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
