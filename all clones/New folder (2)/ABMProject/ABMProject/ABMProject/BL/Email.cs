using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMProject.BL
{
    public class Email
    {

        int uId;
        String emailId;
        String subject;
        String body;
        DateTime time;
        public Email(
        String emailId,
        int uId,
        String subject,
        String body,
        DateTime time
            ) { 
        this.emailId = emailId;
            this.uId = uId;
            this.subject =subject;
            this.body = body;
            this.time = time;
                
        }

 
        public DateTime Time { get => time; set => time = value; }
        public int UId { get => uId; set => uId = value; }
        public string EmailId { get => emailId; set => emailId = value; }
        public string Subject { get => subject; set => subject = value; }
        public string Body { get => body; set => body = value; }
    }
}
