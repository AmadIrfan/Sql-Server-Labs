using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMProject.DL
{
    public class BeneficiaryDL
    {

        public void addBeneficiry(BL.Beneficiary a)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("INSERT into Beneficiary  values(@customerId,@beneficiaryName,@accountNumber)", con);
            cmd.Parameters.AddWithValue("@customerId", a.CustomerId);
            cmd.Parameters.AddWithValue("@beneficiaryName", a.BeneficiaryName);
            cmd.Parameters.AddWithValue("@accountNumber", a.AccountNumber);
            cmd.ExecuteNonQuery();
        }
        public void updateBeneficiry(BL.Beneficiary a)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE Beneficiary SET customerId=@customerId,beneficiaryName=@beneficiaryName,accountNumber=@accountNumber WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", a.Id);
            cmd.Parameters.AddWithValue("@customerId", a.CustomerId);
            cmd.Parameters.AddWithValue("@beneficiaryName", a.BeneficiaryName);
            cmd.Parameters.AddWithValue("@accountNumber", a.AccountNumber);
            cmd.ExecuteNonQuery();
        }
        public void deleteBeneficiry(int id)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM  Beneficiary WHERE id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        public SqlDataAdapter viewBeneficirys(int customerId)
        {
            Console.WriteLine(customerId);
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select beneficiaryName ,accountNumber FROM Beneficiary WHERE (customerId=@customerId)", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            return da;
        }
        public int getBeneficiryId(BL.Beneficiary a)
        {
          int  id = -1;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select id FROM Beneficiary WHERE (beneficiaryName=@beneficiaryName and accountNumber=@accountNumber )", con);
            cmd.Parameters.AddWithValue("@beneficiaryName", a.BeneficiaryName);
            cmd.Parameters.AddWithValue("@accountNumber", a.AccountNumber);
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
