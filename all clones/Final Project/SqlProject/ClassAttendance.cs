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

namespace SqlProject
{
    public partial class ClassAttendance : Form
    {
        int sid = 0;
        public ClassAttendance()
        {
            InitializeComponent();
            refresher();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO ClassAttendance VALUES (@AttendanceDate)", con);
                cmd.Parameters.AddWithValue("@AttendanceDate",DateTime.Parse(pickDateTime.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully");
                refresher();
            }
            catch(Exception exp) {
            
                MessageBox.Show(exp.Message.ToString(),"error",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
            }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("DELETE FROM ClassAttendance WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", sid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Successfully");
                refresher();
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

           int id= int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            sid = id;
            pickDateTime.Text=dataGridView1.CurrentRow.Cells[1].Value.ToString();
     
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private bool refresher()
        {
            dataGridView1.DataSource = null;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from ClassAttendance", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            return true;

        }
    }

}
