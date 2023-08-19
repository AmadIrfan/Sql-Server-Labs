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
using System.Windows.Controls;
using System.Windows.Forms;

namespace SqlProject
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
    
            label3.Visible = false;
            materialComboBox1.Visible = false;
        }

        private void Report_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Student");
            comboBox1.Items.Add("Assessment");
            comboBox1.Items.Add("Attendance");
            comboBox1.Items.Add("Clo wise");
            comboBox1.Items.Add("Students Result");
   
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {



            try
            {

                    String name = textBox1.Text;
                    switch (comboBox1.Text.ToString())
                    {
                    case "Student":
                        studentReport();
                        Pdf.GenerateReport(name: "Students", dataGridView: dataGridView1, fileName: name);
                        break;
                    case "Assessment":
                        assessmentReport();
                        Pdf.GenerateReport(dataGridView1,name, "Assessment ");
                        break;
                    case "Attendance":
                        attendanceReport();
                        Pdf.GenerateReport(dataGridView1,name, "Attendance ");
                        break;
                    case "Clo wise":
                        cloWiseData();
                        Pdf.GenerateReport(dataGridView1,name, "Clo Wise ");
                        break;
                    case "Students Result":
                         studentResultReport();
                        Pdf.GenerateReport(dataGridView1,name, "Results ");
                        break;
                    default:
                        MessageBox.Show("Please Select Entity you want to Generate Attribute....", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        break;
                    }
            }

            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }
        void studentReport()
         {
            dataGridView1.DataSource = null;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("    Select s.Id ,s.FirstName,s.LastName,s.Contact,s.Email,s.RegistrationNumber,l.Name as Status from Student s JOIN Lookup l On s.Status=l.LookupId ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();         
        }
        void assessmentReport()
        {
                dataGridView1.DataSource = null;
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT AC.Name AS [AssessmentComponent Name] ,AC.TotalMarks [AssessmentComponent Marks],A.Title[Assessment Title] ,A.TotalMarks,A.TotalWeightage  FROM Assessment A  JOIN AssessmentComponent AC  On A.Id=AC.AssessmentId", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();
               
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text.Trim();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
            materialComboBox1.Visible = false;
            materialComboBox1.ResetText();
            materialComboBox1.DataSource=null;

            switch (comboBox1.Text.ToString())
            {
                case "Student":
                    studentReport();
                    break;
                case "Assessment":
                    assessmentReport();
                    break;
                case "Students Result":
                    studentResultReport();
                    break;
                case "Attendance":
                    materialComboBox1.ResetText();
                    label3.Text = "Attendance Date";
                    label3.Visible = true;
                    materialComboBox1.Visible = true;
                    classAttendance();
                    attendanceReport();
                    break;
                  case  "Clo wise":
                    dataGridView1.DataSource = null;
                    label3.Text = "Clo\'s";
                    label3.Visible = true;
                    materialComboBox1.Visible = true;
                    cloData();
                    cloWiseData();
                    break;
                default:
                    dataGridView1.DataSource = null;
                    break;
            }
        }

        private void cloWiseData()
        {

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(
"SELECT    S.FirstName AS StudentName,     MAX(R.Details) AS RubricDetails,      Ass.Title AS AssessmentTitle,   SUM(AC.TotalMarks) AS TotalMarks,   SUM(CAST(RL.MeasurementLevel AS float) /  [measurement level] * AC.TotalMarks) AS ObtainedMarks,  cl.Name AS CloDetails  FROM Student S   JOIN StudentResult   SR ON S.Id = SR.StudentId  JOIN AssessmentComponent AC   ON AC.Id = SR.AssessmentComponentId   JOIN Assessment Ass   ON Ass.Id = AC.AssessmentId   JOIN RubricLevel RL   ON RL.Id = SR.RubricMeasurementId   JOIN Rubric R    ON R.Id = RL.RubricId   JOIN Clo cl    ON cl.Id = R.CloId   CROSS JOIN (SELECT MAX(MeasurementLevel) AS [measurement level] FROM RubricLevel) MXL   WHERE cl.Id = @id   GROUP BY S.FirstName, cl.Name, Ass.Title  ", con);
            cmd.Parameters.AddWithValue("@id",int.Parse(materialComboBox1.SelectedValue.ToString()));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

        }

        private void attendanceReport()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(
                " SELECT s.Id,CONCAT(s.FirstName,' ',s.LastName)as Name  ,s.RegistrationNumber,s.Contact ,l.Name AS [Attendance Status] ,ca.AttendanceDate FROM Student s JOIN StudentAttendance sa ON sa.StudentId=s.id JOIN  ClassAttendance ca ON ca.Id=sa.AttendanceId JOIN Lookup l ON sa.AttendanceStatus=l.LookupId WHERE ca.AttendanceDate = @AttendanceDate AND s.Status=5 ", con);
            cmd.Parameters.AddWithValue("@AttendanceDate", DateTime.Parse(materialComboBox1.Text.ToString()));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

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
                materialComboBox1.ValueMember = "AttendanceDate";
                materialComboBox1.DataSource = dt;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        void cloData ()
        {
            try
            {
                SqlConnection con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Id,Name FROM Clo", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(String));
                da.Fill(dt);
                materialComboBox1.DisplayMember = "Name";
                materialComboBox1.ValueMember = "Id";
                materialComboBox1.DataSource = dt;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void materialComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text== "Attendance")
            {

            if (!String.IsNullOrEmpty(materialComboBox1.Text))
            {
              attendanceReport();
            }
            }
            else if(comboBox1.Text == "Clo wise")
            {
                if (!String.IsNullOrEmpty(materialComboBox1.Text))
                {
                    cloWiseData(); 
                }
            }
        }
        private void studentResultReport()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(
            "SELECT CONCAT(S.FirstName,' ',S.LastName) AS Name, S.RegistrationNumber ,A.Title  AS [Assessment Name], AC.Name AS [Assessment Component] ,   RL.Details AS [Rubric Detail], AC.TotalMarks,  RL.MeasurementLevel ,  cast( RL.MeasurementLevel*AC.TotalMarks/4 as decimal(10,2)) AS [Student Obtained Marks]    FROM  StudentResult AS SR     JOIN Student AS S     ON S.Id  = SR.StudentId    JOIN RubricLevel AS RL     ON RL.Id = SR.RubricMeasurementId     JOIN AssessmentComponent AS AC     ON  AC.Id = SR.AssessmentComponentId    JOIN Assessment AS A    ON A.Id = AC.AssessmentId  ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

        }

    }
}
