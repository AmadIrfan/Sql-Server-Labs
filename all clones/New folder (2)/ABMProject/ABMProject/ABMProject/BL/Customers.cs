using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ABMProject.BL
{
    public class Customers : User
    {
        String role;
        public Customers(
         int id,
         int userTypeId,
         String lName,
         String userId,
         String address,
         DateTime createDate,
         DateTime dob,
         String email,
         String fName,
         String password,
         String pNumber,
         String role
            ):base(
          id,
        
          fName,
          lName,
          userId,
          address,
          createDate,
          dob,
          email,
         userTypeId,
          password,
          pNumber
             ) { }
        public Customers():base() { }
        public string Role { get => role; set => role = value; }


    }
}
