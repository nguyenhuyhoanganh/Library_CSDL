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
                /*cbKhu.DataSource = tb;
                cbKhu.DisplayMember = "TENKHU";*/
                foreach (DataRow item in tb.Rows)
                {
                    cbKhu.Items.Add(item["TenKHU"]);
                }
                cbKhu.SelectedIndex = 0;
                label4.Text = "Giá Vé Người Lớn: " + tb.Rows[0]["GIAVENL"].ToString();
                label5.Text = "Giá Vé Trẻ Em: " + tb.Rows[0]["GIAVETE"].ToString();
                nl = Convert.ToDecimal(tb.Rows[0]["GIAVENL"]);
                te = Convert.ToDecimal(tb.Rows[0]["GIAVETE"]);
            }

            txbMaVe.Text = loadMV();
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

        private void btnXuatVe_Click(object sender, EventArgs e)
        {
            if(numericUpDown4.Value == 0 && numericUpDown3.Value == 0)
            {
                MessageBox.Show("Chưa Thêm Lượng Khách Vào Vé");
                return;
            }
            string maNV = data.Rows[0]["MANV"].ToString();
            string maKHU = data.Rows[0]["MAKHU"].ToString();
            DataTable tb = new DataTable();
            if (cbKhu.Visible == true)
            {
                string que = "select * from KHUVUICHOI where TENKHU = N'" + cbKhu.Text + "'";
                tb = DataProvider.Instance.ExecuteQuery(que);
            }
            else
            {
                string que = "select * from KHUVUICHOI where MAKHU = N'" + data.Rows[0]["MAKHU"].ToString() + "'";
                tb = DataProvider.Instance.ExecuteQuery(que);
            }
           
            string gVNL = tb.Rows[0]["GIAVENL"].ToString();
            string gVTE = tb.Rows[0]["GIAVETE"].ToString();

            if (maKHU == "")
            {
                maKHU = tb.Rows[0]["MAKHU"].ToString();
            }

            string query = @"Insert into VE  (MAVE, SOLUONGNL, SOLUONGTE, MAKHU, MANV, TONGTIEN, NGAYBAN , GIAVENL, GIAVETE) values( N'" 
            + txbMaVe.Text + "', '" + numericUpDown4.Value.ToString() + "', '"
            + numericUpDown3.Value.ToString() + "', N'" + maKHU + "', N'" + maNV + "', "
            + txbTong.Text + ", '" + DateTime.Now.ToString("MM/dd/yyyy") + "', " + gVNL + ", " + gVTE + ")";
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            if (i != 0)
            {
                MessageBox.Show("Thêm vé Thành Công");
            }
            else
            {
                MessageBox.Show("Thêm Vé Không Thành Công!!!");
            }
            fBanVe_Load(sender, e);
        }

        private string loadMV()
        {
            DataTable b = DataProvider.Instance.ExecuteQuery("select dbo.at_ma_ve() as N'Mã Vé'");
            return b.Rows[0]["Mã Vé"].ToString();
        }
    }
}
