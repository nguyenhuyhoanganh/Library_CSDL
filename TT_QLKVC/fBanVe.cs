using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TT_QLKVC
{
    public partial class fBanVe : Form
    {
        public fBanVe()
        {
            InitializeComponent();
            txbNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
