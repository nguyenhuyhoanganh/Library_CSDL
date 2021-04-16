using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TT_QLKVC.DAO;

namespace TT_QLKVC
{
    public partial class fThongTinTaiKhoan : Form
    {
        public DataTable dt;
        public fThongTinTaiKhoan()
        {
            InitializeComponent();
        }

        public DataTable loadThongTin(string manv)
        {
            DataTable data = new DataTable();
            using (SqlConnection connec = new SqlConnection(ConnectionString.str))
            {
                connec.Open();
                string query = "select *  from NHANVIEN where MANV = '" + manv + "'";
                SqlCommand com = new SqlCommand(query, connec);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                connec.Close();
                try { adapter.Fill(data); }
                catch { }
            }
            return data;
        }

        private void fThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            string a = dt.Rows[0]["MANV"].ToString();
            DataTable data = loadThongTin(a);
            txbHoTen_TK.Text = data.Rows[0]["TENNV"].ToString();
            txbDiaChi_TK.Text = data.Rows[0]["DIACHI"].ToString();
            txbMaKhu_TK.Text = data.Rows[0]["MAKHU"].ToString();
            if (txbMaKhu_TK.Text == "" || txbMaKhu_TK.Text == "Quản Lý")
            {
                txbMaKhu_TK.Text = "Quản Lý";
            }
            txbMaNV_TK.Text = data.Rows[0]["MANV"].ToString();
            txbSDT_TK.Text = data.Rows[0]["SDT"].ToString();
            dtpkNgaySinh_TK.Value = (DateTime)data.Rows[0]["NGAYSINH"];
            if(data.Rows[0]["GIOITINH"].ToString()== "Nam")
            {
                rbtnNam_TK.Checked = true;
            }
            else
            {
                rbtnNu_TK.Checked = false;
            }
            btnHuy.Visible = false;
            btnLuu.Visible = false;
            txbHoTen_TK.ReadOnly = true;
            txbDiaChi_TK.ReadOnly = true;
            txbSDT_TK.ReadOnly = true;
            //string a = dtpkNgaySinh_TK.Value.ToString();
            //MessageBox.Show(a);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fThongTinTaiKhoan_Load(sender, e);
            txbHoTen_TK.ReadOnly = false;
            txbDiaChi_TK.ReadOnly = false;
            txbSDT_TK.ReadOnly = false;
            btnHuy.Visible = true;
            btnLuu.Visible = true;


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string gt;
            if (rbtnNam_TK.Checked == true)
            {
                gt = "Nam";
            }
            else
            {
                gt = "Nữ";
            }

            string que = @"update NHANVIEN set TENNV= N'" + txbHoTen_TK.Text + "', NGAYSINH='"
                + dtpkNgaySinh_TK.Value.ToString() + "', GIOITINH = N'" + gt + "', DIACHI = N'"
                + txbDiaChi_TK.Text + "', SDT = N'" + txbSDT_TK.Text + "' where MANV='" + txbMaNV_TK.Text + "'";

            int i= DataProvider.Instance.ExecuteNonQuery(que);

            if (i != 0)
            {
                MessageBox.Show("Sửa Thành Công");
            }
            else
            {
                MessageBox.Show("Sửa Không Thành Công");
            }

            fThongTinTaiKhoan_Load(sender, e);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            fThongTinTaiKhoan_Load(sender, e);
        }

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            fDoiMatKhau f = new fDoiMatKhau();
            f.a = dt.Rows[0]["MANV"].ToString();
            f.Show();
        }
    }
}
