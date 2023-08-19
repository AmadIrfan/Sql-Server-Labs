using ABMProject.BL;
using ABMProject.Forms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ABMProject.DL
{
    internal class CustomerDL
    {

        public void addCustomer(String email,String password)
        {
            int userId = -1;
            String id = "";
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT userId , [uId] From Users WHERE (email=@email and password=@password)", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                userId =int.Parse(da.GetValue(0).ToString());
                id =da.GetValue(1).ToString();
            }
            da.Close();
           // Console.WriteLine(id);
           // Console.WriteLine(userId);
            if (id != "" && userId!=-1)
            {

            SqlCommand cmd1 = new SqlCommand("INSERT into Customers values (@userId,@customerTypeId ,@uid)", con);
            cmd1.Parameters.AddWithValue("@userId", id);
            cmd1.Parameters.AddWithValue("@customerTypeId", 1);
            cmd1.Parameters.AddWithValue("@uid", userId);
            cmd1.ExecuteNonQuery();

            }


        }
         public int getCustomers(String userId)
        {
            int id = -1;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * From [Customers] WHERE (userId=@userId)", con);
            cmd.Parameters.AddWithValue("@userId", userId);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                id = int.Parse(da.GetValue(0).ToString());
             }
            da.Close();
            return id;

        }
    }   
}
