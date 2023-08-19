using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ABMProject.BL
{
    public class User : Authentication
    {
       private int id;
       private String fName;
       private String lName;
       private String address;
       private String pNumber;
       private DateTime createDate;
       private int userTypeId;
       private String userId;
       private DateTime dob;
      public User(
            int id,
            String fName,
            String lName,
           String userId,
           String address,
           DateTime createDate,
       //    String ibn,
           DateTime dob,
           String email,
           int userTypeId,
           String password,
           string pNumber) : base(email: email, password: password)
        {
            this.id = id;
            this.UserTypeId = userTypeId;
            this.FName = fName;
            this.LName = lName;
            this.UserId = userId;
            this.Address = address;
            this.CreateDate = createDate;
         //   this.Ibn = ibn;
            this.Dob = dob;
            this.email = email;
             this.PNumber = pNumber;
        }
        public User():base()
        {

        }
public  int Id{
            get => id; set => id = value;}
        public string Address { get => address; set => address = value; }
        public string PNumber { get => pNumber; set =>pNumber = value; }
        public string UserId { get => userId; set => userId = value; }
        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
       // public string Ibn { get => ibn; set => ibn = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public int UserTypeId { get => userTypeId; set => userTypeId = value; }
    }
}
