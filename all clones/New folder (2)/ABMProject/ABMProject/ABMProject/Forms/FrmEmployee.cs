using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace ABMProject.Forms
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee(string UserName)
        {
            InitializeComponent();
            lblEmployeeName.Text = UserName;
            lblEName.Text = UserName;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
                // Create a new DataGridView control
                DataGridView dataGridView = new DataGridView();
                dataGridView.Location = new Point(210, 110);
                dataGridView.Size = new Size(1000, 400);
                dataGridView.Dock = DockStyle.Fill;
                dataGridView.ColumnCount = 6;

                // Add column headers to the DataGridView control
                dataGridView.Columns[0].HeaderText = "Id";
                dataGridView.Columns[1].HeaderText = "First Name";
                dataGridView.Columns[2].HeaderText = "Last Name";
                dataGridView.Columns[3].HeaderText = "Address";
                dataGridView.Columns[4].HeaderText = "Date of Birth";
                dataGridView.Columns[5].HeaderText = "Email";

                // Retrieve data from the database
                string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;";
                string query = "SELECT userId, firstName, lastName, address, dateOfBirth, email \r\n FROM Users";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView.DataSource = table;

            // Add the DataGridView to the panel's Controls collection
            panel1.Controls.Add(dataGridView);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Log out ???", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Hide();
                LogIn log = new LogIn();
                log.Show();
            }
        }
    }
}
