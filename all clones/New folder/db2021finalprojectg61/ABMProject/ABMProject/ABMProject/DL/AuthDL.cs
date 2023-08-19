
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ABMProject.BL;

namespace ABMProject.DL
{
    internal class AuthDL
    {
        public void registerUser(User u )
        {
            int tId = 1;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Users values(@firstName,@lastName,@address,@createDate,@email,@password,@phoneNumbers,@uId,@userTypeId,@dateOfBirth)", con);
            cmd.Parameters.AddWithValue("@firstName",u.FName); 
            cmd.Parameters.AddWithValue("@lastName",u.LName); 
            cmd.Parameters.AddWithValue("@address",u.Address); 
            cmd.Parameters.AddWithValue("@createDate",u.CreateDate);
            cmd.Parameters.AddWithValue("@email",u.Email); 
            cmd.Parameters.AddWithValue("@password",u.Password);  
            cmd.Parameters.AddWithValue("@phoneNumbers",u.PNumber);  
            cmd.Parameters.AddWithValue("@uId",u.UserId); 
            cmd.Parameters.AddWithValue("@userTypeId",tId); 
            cmd.Parameters.AddWithValue("@dateOfBirth",u.Dob); 
            cmd.ExecuteNonQuery();
            
        }

        public User login(string email,string password)
        {
            int id =-1;
            String fName="";
            String lName="";
            String address = "";
            DateTime cDate=DateTime.Now;
            String dEmail = "";
            String pass = "";
            String pNumber = "";
            String uId = "";
            int uTi=0;
            DateTime dob=DateTime.Now;
            User user =null;   
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE email=@email and password=@password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                 id         = int.Parse(da.GetValue(0).ToString());
                 fName   = da.GetValue(1).ToString();
                 lName   = da.GetValue(2).ToString();
                 address = da.GetValue(3).ToString();
                 cDate = DateTime.Parse(da.GetValue(4).ToString());
                 dEmail  = da.GetValue(5).ToString();
                 pass    = da.GetValue(6).ToString();
                 pNumber = da.GetValue(7).ToString();
                 uId     = da.GetValue(8).ToString();
                 uTi        = int.Parse(da.GetValue(9).ToString());
                 dob   = DateTime.Parse(da.GetValue(10).ToString());

            /*    Console.WriteLine( da.GetValue(0).ToString());
                Console.WriteLine( da.GetValue(1).ToString());
                Console.WriteLine( da.GetValue(2).ToString());
                Console.WriteLine( da.GetValue(3).ToString());
                Console.WriteLine( da.GetValue(4).ToString());
                Console.WriteLine( da.GetValue(5).ToString());
                Console.WriteLine( da.GetValue(6).ToString());
                Console.WriteLine( da.GetValue(7).ToString());
                Console.WriteLine( da.GetValue(8).ToString());
                Console.WriteLine( da.GetValue(9).ToString());
                Console.WriteLine( da.GetValue(10).ToString());
            */
            }
              da.Close();

            user = new User(id,fName,lName,uId,address,cDate,dob,email,uTi,pass,pNumber);
            if (user.Id != -1)
            {
            return user;
            }
            else
            {
            return null;
            }
        }
    }
}
