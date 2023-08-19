using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ABMProject.BL;
using ABMProject.Forms;

namespace ABMProject.DL
{
    internal class AccountDL
    {
        public void addAccountData(BL.Accounts a) 
        {
           
            var con=Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("INSERT into Accounts  values(@accountId,@accountName,@accountTypeId,@customerId,@Amounts,@dateOpened,@accountNumber)", con);
            cmd.Parameters.AddWithValue("@accountName", a.Name);
            cmd.Parameters.AddWithValue("@accountTypeId", a.Type);
            cmd.Parameters.AddWithValue("@customerId", a.CustomerId);
            cmd.Parameters.AddWithValue("@Amounts", a.Amount);
            cmd.Parameters.AddWithValue("@dateOpened", a.CreateDate);
            cmd.Parameters.AddWithValue("@accountNumber", a.AccountNumber);
         /*   Console.WriteLine(a.CustomerId+12);
            Console.WriteLine(a.Name);
            Console.WriteLine(a.Type);
            Console.WriteLine(a.CustomerId);
            Console.WriteLine(a.Amount);
            Console.WriteLine(a.CreateDate);
            Console.WriteLine(a.AccountNumber);
         */   cmd.ExecuteNonQuery();
        }

        public static String accountNumber()
        {
            String bCode = "9867";
            String accountNumber = "";
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            Random r = new Random();
            foreach (int i in arr)
            {
                int a = r.Next(0, 9);
                accountNumber = accountNumber + arr[a].ToString();
            }
            String accountNo = bCode + accountNumber;

            return accountNo;
        }

        public SqlDataReader accountDetails(int id)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(
                " SELECT CONCAT(U.firstName,' ',U.lastName), U.email,U.address,U.phoneNumbers,A.dateOpened,A.Amounts,A.accountNumber,U.dateOfBirth,ATT.accountTypeName FROM Users U  JOIN Customers C ON C.uid= U.userId  JOIN Accounts A ON A.customerId= C.CustomerId  JOIN AccountType ATT ON A.accountTypeId=ATT.accountType where  U.userId= @id", con);
            cmd.Parameters.AddWithValue("@id",id);
            SqlDataReader da = cmd.ExecuteReader();
    
           return da;
        }

        }
}
