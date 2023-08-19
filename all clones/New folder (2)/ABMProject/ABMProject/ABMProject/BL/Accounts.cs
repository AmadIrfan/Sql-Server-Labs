using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ABMProject.BL
{
    public class Accounts
    {
        int id;
        String name;
        int type;
        DateTime createDate;
        int customerId;
        String accountNumber;
        double amount;

        public Accounts() { }
        public Accounts(String name,int id, DateTime createDate, int type, int customerId, string accountNumber,double amount)
        {
            this.Name = name;
            this.Id = id;
            this.Type = type;
            this.CreateDate = createDate;
            this.customerId = customerId;
            this.accountNumber = accountNumber;
            this.amount = amount;   
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Type { get => type; set => type = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
        public double Amount { get => amount; set => amount = value; }
    }
}
