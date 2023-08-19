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

        String emailId;
        String uId;
        String sId;
        String title;
        String body;
        DateTime time;
        public Email(
        String emailId,
        String uId,
        String sId,
        String title,
        String body,
        DateTime time
            ) { 
        this.EmailId = emailId;
            this.UId = uId;
            this.SId = sId;
            this.Title = title;
            this.Body = body;
            this.Time = time;
                
        }

        public string EmailId { get => emailId; set => emailId = value; }
        public string UId { get => uId; set => uId = value; }
        public string SId { get => sId; set => sId = value; }
        public string Title { get => title; set => title = value; }
        public string Body { get => body; set => body = value; }
        public DateTime Time { get => time; set => time = value; }
    }
}
