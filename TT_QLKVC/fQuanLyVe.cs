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
    // vhir dành cho quản lý
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
            btnHuy_Click(sender, e);
            loadMAVETk();
            rbtnMaVe.Checked = true;
            dtpkTimKiem.Visible = false;
        }

        
        public DataTable loadDL(string a)
        {
            string que = "select * from KHUVUICHOI where MAKHU = N'" + a + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            return tb;
        }
        private void loadGiaVE()
        {
            string que = "select * from KHUVUICHOI";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            string a = tb.Rows[0]["MAKHU"].ToString();
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
            txbMaNV.Text = dt.Rows[0]["MANV"].ToString();
            string que = "select * from KHUVUICHOI";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            cbMaKhu.DataSource = tb;
            cbMaKhu.DisplayMember = "TENKHU";
        }
        private void loadDuLieuVeTheoMANV()
        {
            txbMaNV.Text = dt.Rows[0]["MANV"].ToString();
            string que = "select * from VE where MANV = '"+txbMaNV.Text+"'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            cbMaVE.DataSource = tb;
            cbMaVE.DisplayMember = "MAVE";
            string que2 = @"select TENKHU from KHUVUICHOI where MAKHU =N'" + tb.Rows[0]["MAKHU"].ToString() + "'";
            cbMaKhu.Text = DataProvider.Instance.ExecuteQuery(que2).Rows[0]["TENKHU"].ToString();
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(button3.Enabled == true)
            {
                if (nmNL.Value == 0 && nmTE.Value == 0)
                {
                    MessageBox.Show("Chưa Thêm Lượng Khách Vào Vé");
                    return;
                }
                string maNV = dt.Rows[0]["MANV"].ToString();
                string que = "select * from KHUVUICHOI where TENKHU = N'" + cbMaKhu.Text + "'";
                DataTable tb = DataProvider.Instance.ExecuteQuery(que);
                string maKHU = tb.Rows[0]["MAKHU"].ToString(); 
                string gVNL = tb.Rows[0]["GIAVENL"].ToString();
                string gVTE = tb.Rows[0]["GIAVETE"].ToString();
                string query = @"Insert into VE  (MAVE, SOLUONGNL, SOLUONGTE, MAKHU, MANV, TONGTIEN, NGAYBAN , GIAVENL, GIAVETE) values( N'"
                + txbMaVe.Text + "', '" + nmNL.Value.ToString() + "', '"
                + nmTE.Value.ToString() + "', N'" + maKHU + "', N'" + maNV + "', "
                + txbTongTien.Text + ", '" + dtpkNgayBan.Value.ToString("MM/dd/yyyy") + "', " + gVNL + ", " + gVTE + ")";
                int i = DataProvider.Instance.ExecuteNonQuery(query);
                if (i != 0)
                {
                    MessageBox.Show("Thêm vé Thành Công");
                    button1_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Thêm Vé Không Thành Công!!!");
                }
            }
            if(button2.Enabled == true)
            {
                if (nmNL.Value == 0 && nmTE.Value == 0)
                {
                    MessageBox.Show("Thêm Số Lượng Người Lớn Và Trẻ Em");
                    return;
                }
                string que = "select * from KHUVUICHOI where TENKHU = N'" + cbMaKhu.Text + "'";
                DataTable tb = DataProvider.Instance.ExecuteQuery(que);
                string maKHU = tb.Rows[0]["MAKHU"].ToString();
                string gVNL = tb.Rows[0]["GIAVENL"].ToString();
                string gVTE = tb.Rows[0]["GIAVETE"].ToString();
                string query = "UPDATE VE SET MAKHU =N'" + maKHU + "', NGAYBAN ='" + dtpkNgayBan.Value.ToString("MM/dd/yyyy") + "', SOLUONGNL ='"
                    + nmNL.Value.ToString() + "', SOLUONGTE ='" + nmTE.Value.ToString() + "', TONGTIEN =" + txbTongTien.Text + ", GIAVENL ="
                    + gVNL + ", GIAVETE= " + gVTE + " WHERE MAVE =N'" + cbMaVE.Text + "'";
                int i = DataProvider.Instance.ExecuteNonQuery(query);
                if (i != 0)
                {
                    MessageBox.Show("Sửa vé Thành Công");
                    button1_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Sửa Vé Không Thành Công!!!");
                }
            }
            if(button4.Enabled == true)
            {
                string query = "DELETE FROM VE WHERE MAVE =N'" + cbMaVE.Text + "'";
                int i = DataProvider.Instance.ExecuteNonQuery(query);
                if (i != 0)
                {
                    MessageBox.Show("Xóa vé Thành Công");
                    button1_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa Vé Không Thành Công!!!");
                }
            }
            btnHuy_Click(sender, e);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;

            btnHuy.Visible = false;
            btnLuu.Visible = false;

            txbMaKhu.Text = "";
            txbMaNV.Text = "";
            txbMaVe.Text = "";
            nmNL.Value = 0;
            nmTE.Value = 0;
            txbTongTien.Text = "";

            cbMaKhu.Visible = false;
            txbMaKhu.Visible = true;
            txbMaNV.Visible = true;
            txbMaVe.Visible = true;
            cbMaVE.Visible = false;
            btnLuu.Text = "Lưu";
            loadMAVETk();
        }

        private void button2_Click(object sender, EventArgs e)
        {//sửa
            cbMaVE.Visible = true;
            cbMaKhu.Visible = true;

            txbMaVe.Visible = false;
            txbMaNV.Visible = true;
            txbMaKhu.Visible = false;

            txbTongTien.ReadOnly = true;

            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            btnLuu.Text = "Sửa";

            btnHuy.Visible = true;
            btnLuu.Visible = true;
            loadDuLieuNV();
            loadDuLieuVeTheoMANV();
        }

        private void cbMaVE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string que = "select * from VE where MAVE = N'"+cbMaVE.Text+"'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            cbMaKhu.Text = tb.Rows[0]["MAKHU"].ToString();
            nmNL.Value = Convert.ToInt32(tb.Rows[0]["SOLUONGNL"]);
            nmTE.Value = Convert.ToInt32(tb.Rows[0]["SOLUONGTE"]);
            txbTongTien.Text = tb.Rows[0]["TONGTIEN"].ToString();
            string que2 = @"select TENKHU from KHUVUICHOI where MAKHU =N'" + tb.Rows[0]["MAKHU"].ToString() + "'";
            cbMaKhu.Text = DataProvider.Instance.ExecuteQuery(que2).Rows[0]["TENKHU"].ToString();
            txbMaKhu.Text = cbMaKhu.Text;
            txbMaNV.Text = tb.Rows[0]["MANV"].ToString();
        }
        private void loadMAVETk()
        {
            string que = "Select MAVE from VE";
            cbMaVETK.DataSource = DataProvider.Instance.ExecuteQuery(que);
            cbMaVETK.DisplayMember = "MAVE";
        }
        private void button4_Click(object sender, EventArgs e)
        {//Xóa
            cbMaVE.Visible = true;

            txbMaVe.Visible = false;
            txbMaNV.Visible = true;
            txbMaKhu.Visible = true;

            txbTongTien.ReadOnly = true;

            button1.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
            button5.Enabled = false;

            btnHuy.Visible = true;
            btnLuu.Visible = true;

            btnLuu.Text = "Xóa";

            string que = "select * from VE ";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            cbMaVE.DataSource = tb;
            cbMaVE.DisplayMember = "MAVE";
            string que2 = @"select TENKHU from KHUVUICHOI where MAKHU =N'" + tb.Rows[0]["MAKHU"].ToString() + "'";
            txbMaKhu.Text = DataProvider.Instance.ExecuteQuery(que2).Rows[0]["TENKHU"].ToString();
            txbMaNV.Text = tb.Rows[0]["MANV"].ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string que;
            if (rbtnMaVe.Checked == true)
            {
                que = "Select * from VE where MAVE =N'" + cbMaVETK.Text + "'";
                dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery(que);
            }else if (rbtnNgayBan.Checked == true)
            {
                que = "Select * from VE where NGAYBAN ='" + dtpkTimKiem.Value.ToString("MM/dd/yyyy") + "'";
                dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery(que);
            }
        }

        private void rbtnMaVe_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnMaVe.Checked == true)
            {
                cbMaVETK.Visible = true;
                dtpkTimKiem.Visible = false;
            }
            else
            {
                cbMaVETK.Visible = false;
                dtpkTimKiem.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
            //button Thêm
        {
            btnLuu.Text = "Thêm";
            cbMaKhu.Visible = true;
            txbMaKhu.Visible = false;

            cbMaVE.Visible = false;

            txbMaVe.ReadOnly = true;
            txbMaNV.ReadOnly = true;
            txbTongTien.ReadOnly = true;

            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            btnHuy.Visible = true;
            btnLuu.Visible = true;
            loadDuLieuNV();
            txbMaVe.Text = loadMV();
        }
    }
}
