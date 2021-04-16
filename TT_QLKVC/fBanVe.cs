using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TT_QLKVC.DAO;

namespace TT_QLKVC
{
    public partial class fBanVe : Form
    {
        public DataTable data;
        decimal nl, te;
        public fBanVe()
        {
            InitializeComponent();
            txbNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        public DataTable loadDL(string a)
        {
            string que = "select * from KHUVUICHOI where MAKHU = N'" + a + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            return tb;
        }
        private void fBanVe_Load(object sender, EventArgs e)
        {
            string a = data.Rows[0]["MAKHU"].ToString();
            if (a != "")
            {
                cbKhu.Visible = false;

                DataTable tb = loadDL(a);

                label3.Text = "Khu: " + tb.Rows[0]["TENKHU"].ToString();
                label4.Text = "Giá Vé Người Lớn: " + tb.Rows[0]["GIAVENL"].ToString();
                label5.Text = "Giá Vé Trẻ Em: " + tb.Rows[0]["GIAVETE"].ToString();
                nl = Convert.ToDecimal(tb.Rows[0]["GIAVENL"]);
                te = Convert.ToDecimal(tb.Rows[0]["GIAVETE"]);
            }
            else
            {
                cbKhu.Visible = true;
                string que = "select * from KHUVUICHOI";
                DataTable tb = DataProvider.Instance.ExecuteQuery(que);
                cbKhu.DataSource = tb;
                cbKhu.DisplayMember = "TENKHU";

                label4.Text = "Giá Vé Người Lớn: " + tb.Rows[0]["GIAVENL"].ToString();
                label5.Text = "Giá Vé Trẻ Em: " + tb.Rows[0]["GIAVETE"].ToString();
                nl = Convert.ToDecimal(tb.Rows[0]["GIAVENL"]);
                te = Convert.ToDecimal(tb.Rows[0]["GIAVETE"]);
            }
        }

        private void cbKhu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string que = "select * from KHUVUICHOI where TENKHU = N'" + cbKhu.Text + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            label4.Text = "Giá Vé Người Lớn: " + tb.Rows[0]["GIAVENL"].ToString();
            label5.Text = "Giá Vé Trẻ Em: " + tb.Rows[0]["GIAVETE"].ToString();
            nl = Convert.ToDecimal(tb.Rows[0]["GIAVENL"]);
            te = Convert.ToDecimal(tb.Rows[0]["GIAVETE"]);

            numericUpDown4_ValueChanged(sender, e);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            decimal tong = (decimal)numericUpDown4.Value * nl + (decimal)numericUpDown3.Value * te;
            txbTong.Text = tong.ToString();
        }
    }
}
