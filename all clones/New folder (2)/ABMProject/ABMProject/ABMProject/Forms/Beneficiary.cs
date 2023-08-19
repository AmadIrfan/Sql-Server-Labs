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
using ABMProject.DL;
using ABMProject.BL;
using System.Xml.Linq;

namespace ABMProject.Forms
{
    public partial class Beneficiary : Form
    {
        User user;
        int id = -1;
        public Beneficiary(User u)

        {
            this.user = u;
            InitializeComponent();
        }

        private void Beneficiary_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            viewGrid();
            textBox2.MaxLength = 16;
        }

        void viewGrid()
        {
            try
            {
                int custId = user.Id;
                BeneficiaryDL bne = new BeneficiaryDL();
                dataGridView1.DataSource = null;
                SqlDataAdapter da = bne.viewBeneficirys(customerId: custId);
                DataTable dt = new DataTable();
                Console.WriteLine();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String name=textBox1.Text;
                String accountNo=textBox2.Text;
                if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
                {
                    BL.Beneficiary bb = new BL.Beneficiary(1, user.Id,name,accountNo);
                    BeneficiaryDL bne=new BeneficiaryDL();
                    bne.addBeneficiry(bb);
                    MessageBox.Show("Successfully Added","Alert",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    viewGrid();
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                try
                {
                    BeneficiaryDL bne = new BeneficiaryDL();
                    bne.deleteBeneficiry(id);
                    MessageBox.Show("Deleted Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    viewGrid();

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
            try
            {

                BeneficiaryDL bne = new BeneficiaryDL();
                BL.Beneficiary bb = new BL.Beneficiary(this.id, user.Id, textBox1.Text, textBox2.Text);
                bne.updateBeneficiry(bb);
                MessageBox.Show("Updated Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                viewGrid();
            }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                if (!String.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()) && !String.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[1].Value.ToString()))
                {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                BL.Beneficiary bb = new BL.Beneficiary(textBox1.Text, textBox2.Text);
                BeneficiaryDL bne = new BeneficiaryDL();
                this.id = bne.getBeneficiryId(bb);
                Console.WriteLine(id);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
                }
                else
                {
                MessageBox.Show("Selected velue is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
        }
    }
}
