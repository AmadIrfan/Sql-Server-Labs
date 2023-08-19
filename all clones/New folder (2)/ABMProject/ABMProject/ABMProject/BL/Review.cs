using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ABMProject.BL
{
    public class Review
    {
        String rId;
        String uId;
        String pId;
        int rating;
        String comment;
        DateTime time;
        public Review(String rId,
        String uId,
        String pId,
        int rating,
        DateTime time,
        String comment)
        {
            this.RId = rId;
                this.UId = uId;
                this.PId = pId;
            this.Rating = rating;   
            this.Comment = comment;
            this.Time=time;

        }

        public string RId { get => rId; set => rId = value; }
        public string UId { get => uId; set => uId = value; }
        public string PId { get => pId; set => pId = value; }
        public int Rating { get => rating; set => rating = value; }
        public string Comment { get => comment; set => comment = value; }
        public DateTime Time { get => time; set => time = value; }
    }
}
