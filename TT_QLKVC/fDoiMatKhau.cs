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

namespace TT_QLKVC
{
    public partial class fDoiMatKhau : Form
    {
        public string a;
        public fDoiMatKhau()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable data = new DataTable();
            using (SqlConnection connec = new SqlConnection(ConnectionString.str))
            {
                connec.Open();
                string query = "select * from NHANVIEN where MANV = '" + a + "'";
                SqlCommand com = new SqlCommand(query, connec);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                connec.Close();
                try { adapter.Fill(data); }
                catch { }
            }
            if (data.Rows[0]["MATKHAU"].ToString() == txbMKcu.Text)
            {
                if (txbMKmoi.Text != txbNhaplai.Text)
                {
                    MessageBox.Show("Mật Khẩu Nhập Lại Không Đúng!");
                }
                else
                {
                    int acceptedRows = -1;
                    string query = "Update NHANVIEN set MATKHAU = N'" + txbMKmoi.Text + "' where MANV = N'"
                        +a+"'";
                    using (SqlConnection connection = new SqlConnection(ConnectionString.str))
                    {

                        connection.Open();

                        SqlCommand command = new SqlCommand(query, connection);

                        try
                        {
                            acceptedRows = command.ExecuteNonQuery();
                        }
                        catch { }
                        connection.Close();
                    }
                    if (acceptedRows > 0)
                    {
                        MessageBox.Show("Đổi Mật Khẩu Thành Công");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Đổi Mật Khẩu Thất Bại");
                    }
                }
            }
            else
            {
                MessageBox.Show("Sai Mật Khẩu");
            }
        }
    }
    
}
