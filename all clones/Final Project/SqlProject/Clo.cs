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

namespace SqlProject
{
    public partial class Clo : Form
    {
        DateTime cDate=DateTime.Now;
        int sId =-1;
        public Clo()
        {
            InitializeComponent();
        }

        private void Clo_Load(object sender, EventArgs e)
        {
            cloView.ReadOnly= true;
            refresher();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCloName.Text))
            {

                createClo();
            MessageBox.Show("successFully Added");
                refresher();
            }
            else
            {

            MessageBox.Show("Empty Fields","Error");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                if (!String.IsNullOrEmpty(txtCloName.Text))
                {
                    updateClos(sId);
                
                }
                else
                {
                MessageBox.Show("Box is Empty", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                }
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
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("delete from Clo WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", sId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete item ", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                refresher();
                txtCloName.Clear();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCloName.Clear();
        }

        private void cloView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                 int id =int.Parse( cloView.CurrentRow.Cells["Id"].Value.ToString());
                sId = id;
                 String cloName = cloView.CurrentRow.Cells["Name"].Value.ToString();
                String cDate = cloView.CurrentRow.Cells["DateCreated"].Value.ToString();
                String uDate = cloView.CurrentRow.Cells["DateUpdated"].Value.ToString();
            txtCloName.Text = cloName;
                this.cDate = DateTime.Parse(cDate);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }
        private bool refresher()
        {

            try
            {

            cloView.DataSource = null;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cloView.DataSource = dt;
            cloView.Refresh();
            return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                return false;
            }

        }

        void updateClos(int sId)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE Clo SET Name=@Name, DateCreated=@DateCreated ,DateUpdated=@DateUpdated  WHERE Id=@Id ", con);
                cmd.Parameters.AddWithValue("@Id", sId);
                cmd.Parameters.AddWithValue("@Name", txtCloName.Text);
                cmd.Parameters.AddWithValue("@DateCreated",cDate);
                cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update item ", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresher();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }
        void createClo()
        {
            try
            {

            var con = Configuration.getInstance().getConnection();

            cDate = DateTime.Now;
            SqlCommand cmd = new SqlCommand("INSERt INTO Clo VALUES (@Name,@DateCreated,@DateUpdated)", con);
            cmd.Parameters.AddWithValue("@Name", txtCloName.Text);
            cmd.Parameters.AddWithValue("@DateCreated", cDate);
            cmd.Parameters.AddWithValue("@DateUpdated",cDate);
            cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                //          return exp.Message.ToString();
            }
        }
    }
}
