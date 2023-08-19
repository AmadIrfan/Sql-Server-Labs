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
        String id;
        String name;
        String bankName;
        public Beneficiary(
            String id,String name,String bankName) {
        this.id = id;
            this.name = name;
            this.bankName = bankName;
        }
        public String Id {   get=> id;  set => id = value; }
        public String Name { get => name; set => name= value; }
        public String BankName
        {
            get => bankName;
            set => bankName = value;

        }
    }
}
