using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TT_QLKVC
{
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool Login(string tk, string mk)
        {
            DataTable data = new DataTable();
            using (SqlConnection connec = new SqlConnection(ConnectionString.str))
            {
                connec.Open();
                string query = "select * from NHANVIEN where MANV = '" + tk + "' and MATKHAU = '" + mk + "'";
                SqlCommand com = new SqlCommand(query, connec);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                connec.Close();
                try { adapter.Fill(data); }
                catch { }
            }
            return data.Rows.Count > 0;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {

            if (Login(txbUsername.Text, txbPassword.Text) == true)
            {
                this.Hide();
                Manager f = new Manager();
                DataTable dt = new DataTable();
                using (SqlConnection connec = new SqlConnection(ConnectionString.str))
                {
                    connec.Open();
                    string query = "select *  from NHANVIEN where MANV = N'" + txbUsername.Text + "'";
                    SqlCommand com = new SqlCommand(query, connec);
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    connec.Close();
                    try { adapter.Fill(dt); }
                    catch { }
                }

                f.data = dt;
                f.ShowDialog();
                try { this.Show(); }
                catch { this.Close(); }
            }
            else
            {
                MessageBox.Show("Đăng Nhập Thất Bại");
            }

        }
        private void btnChangePasswordChar_Click(object sender, EventArgs e)
        {
            if (txbPassword.UseSystemPasswordChar == true)
                txbPassword.UseSystemPasswordChar = false;
            else
                txbPassword.UseSystemPasswordChar = true;
        }
        private void txbUsername_Click(object sender, EventArgs e)
        {
            if (txbUsername.Text == "Username")
                txbUsername.Clear();

        }

        private void txbPassword_Click(object sender, EventArgs e)
        {
            if (txbPassword.Text == "Password")
                txbPassword.Clear();
            txbPassword.UseSystemPasswordChar = true;
        }
    }
}
