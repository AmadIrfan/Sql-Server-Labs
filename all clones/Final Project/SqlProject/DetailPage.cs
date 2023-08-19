using CRUDA;
using MaterialSkin.Controls;
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
using System.Windows.Media.Media3D;

namespace SqlProject
{
    public partial class DetailPage : MaterialForm
    {
        int id = -1;
        
        String tableName = "";
        String pageName = "";
        readonly MaterialSkin.MaterialSkinManager materialSkinManager;
        public DetailPage(int id , String tableName, string pageName)
        {
            InitializeComponent();
            materialSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            this.id = id;
            this.tableName = tableName;
            this.pageName = pageName;
            this.Name = pageName;
        }

        private void DetailPage_Load(object sender, EventArgs e)
        {
            Console.WriteLine(tableName);
            Console.WriteLine(id);
            SqlConnection con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clo WHERE Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

        }
    }
}
