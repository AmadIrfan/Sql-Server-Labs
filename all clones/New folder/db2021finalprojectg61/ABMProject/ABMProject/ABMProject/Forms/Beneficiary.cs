using ABMProject.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMProject.Forms
{
    public partial class Beneficiary : Form
    {
        User user;
        public Beneficiary(User u)

        {
            this.user = u;
            InitializeComponent();
        }

        private void Beneficiary_Load(object sender, EventArgs e)
        {

        }
    }
}
