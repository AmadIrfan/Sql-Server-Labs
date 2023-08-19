using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ABMProject.BL
{
    public class Beneficiary
    {
        int id;
        int customerId;
        String beneficiaryName;
        String accountNumber;
        public Beneficiary(int id,int customerId, String beneficiaryName,String accountNumber)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.BeneficiaryName = beneficiaryName;
            this.AccountNumber = accountNumber;
        }

        public Beneficiary(String beneficiaryName, String accountNumber)
        {
            this.BeneficiaryName = beneficiaryName;
            this.AccountNumber = accountNumber;
        }
        public int Id { get => id; set => id = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public string BeneficiaryName { get => beneficiaryName; set => beneficiaryName = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
    }
}
