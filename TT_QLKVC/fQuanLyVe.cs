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
    public partial class fQuanLyVe : Form
    {
        public DataTable dt;
        decimal nl, te;
        public fQuanLyVe()
        {
            InitializeComponent();
        }

        private void fQuanLyVe_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            loadGiaVE();

        }

        public DataTable loadDL(string a)
        {
            string que = "select * from KHUVUICHOI where MAKHU = N'" + a + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            return tb;
        }
        private void loadGiaVE()
        {
            string a = dt.Rows[0]["MAKHU"].ToString();
            if (a == "")
            {
                string que = "select * from KHUVUICHOI";
                DataTable tb = DataProvider.Instance.ExecuteQuery(que);
                a = tb.Rows[0]["MAKHU"].ToString();
            }
            DataTable dta = loadDL(a);
            nl = Convert.ToDecimal(dta.Rows[0]["GIAVENL"]);
            te = Convert.ToDecimal(dta.Rows[0]["GIAVETE"]);
            lbGVNL.Text = "Giá Vé Người Lớn: " + dta.Rows[0]["GIAVENL"].ToString();
            lbGVTE.Text = "Giá Vé Trẻ Em: " + dta.Rows[0]["GIAVETE"].ToString();

        }

        private void cbKhu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string que = "select * from KHUVUICHOI where TENKHU = N'" + cbMaKhu.Text + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            lbGVNL.Text = "Giá Vé Người Lớn: " + tb.Rows[0]["GIAVENL"].ToString();
            lbGVTE.Text = "Giá Vé Trẻ Em: " + tb.Rows[0]["GIAVETE"].ToString();
            nl = Convert.ToDecimal(tb.Rows[0]["GIAVENL"]);
            te = Convert.ToDecimal(tb.Rows[0]["GIAVETE"]);

            numericUpDown4_ValueChanged(sender, e);
        }

        private void loadDuLieuNV()
        {
            cbMaNV.Visible = false;
            txbMaNV.Visible = true;

            txbMaNV.Text = dt.Rows[0]["MANV"].ToString();
            string a = dt.Rows[0]["MAKHU"].ToString();
            if (a == "")
            {
                txbMaKhu.Visible = false;
                cbMaKhu.Visible = true;
                string que = "select * from KHUVUICHOI";
                DataTable tb = DataProvider.Instance.ExecuteQuery(que);
                cbMaKhu.DataSource = tb;
                cbMaKhu.DisplayMember = "TENKHU";
            }
            else 
            { 
                txbMaKhu.Text = a;
                txbMaKhu.Visible = true;
                cbMaKhu.Visible = false;
            }
        }
        private string loadMV()
        {
            DataTable b = DataProvider.Instance.ExecuteQuery("select dbo.at_ma_ve() as N'Mã Vé'");
            return b.Rows[0]["Mã Vé"].ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            decimal tong = (decimal)nmNL.Value * nl + (decimal)nmTE.Value * te;
            txbTongTien.Text = tong.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string que = "select * from VE";
            dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery(que);
        }

        private void button3_Click(object sender, EventArgs e)
            //button Thêm
        {
            txbMaVe.ReadOnly = false;
            txbMaKhu.ReadOnly = false;
            txbMaNV.ReadOnly = false;
            loadDuLieuNV();
            txbMaVe.Text = loadMV();

        }
    }
}
