using Design_Login_Form.DAO;
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
using System.Globalization;
using System.Text.RegularExpressions;
//using s
namespace Design_Login_Form
{
    public partial class fQuanLyNhanVien : Form
    {
        string constr = @"Data Source=DESKTOP-7HIL2OS\SQLEXPRESS;Initial Catalog=KHUVUICHOIGIAITRI;Integrated Security=True";
        //string contr = ConnectionString.str; ghép chương trình làm ơn uncomment cái này
        public fQuanLyNhanVien()
        {
            InitializeComponent();
        }

        string manv, tennv, makhu, luong, diachi, sdt;
        string gioitinh;
        //DateTime ns;
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            ktgt();
            if (txbDiaChi.Text == "" || txbLuong_NV.Text == "" || comboBox1.Text == "" || txbMaNhanVien.Text == "" || txbMatKhau.Text == "" || txbSD_NV.Text == "" || txbTen_NV.Text == "")
            {
                MessageBox.Show("Mời nhập đầy đủ các thông tin của nhân viên này", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Regex.IsMatch(txbLuong_NV.Text, "\\d") == false)
                MessageBox.Show("Lương nhân viên phải là số tự nhiên lớn hơn 0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //else if(Regex.IsMatch(txbTen_NV.Text,"\\D")== true)
            //    MessageBox.Show("Tên nhân viên không thể có ký tự nằm ngoài a-z hoặc A-Z", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Regex.IsMatch(txbSD_NV.Text, "\\d") == false || txbSD_NV.Text.Length < 10)
                MessageBox.Show("Số điện thoại chưa chính xác", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlCommand command = new SqlCommand("execute themnv N'" + txbMaNhanVien.Text + "', N'" + txbTen_NV.Text + "', '" + dtpkNgaySinh_NV.Value + "', N'" + txbSD_NV.Text + "', N'" + gioitinh + "', N'" + txbLuong_NV.Text + "', N'" + txbMatKhau.Text + "', N'" + comboBox1.Text + "', N'" + txbDiaChi.Text + "'", sqlcon);
                    //sqlcon.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                    command.ExecuteNonQuery();
                }
                load();
            }

        }
        /*
        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            if (rbtnTheoMa.Checked == true)
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlDataAdapter sqlData = new SqlDataAdapter("execute timkiemnv N'" + txbTimKiemma.Text + "'", sqlcon);
                    DataTable dataTable = new DataTable();
                    sqlData.Fill(dataTable);
                    dtgvNV.DataSource = dataTable;
                }
            else if (rbtnTheoTen.Checked == true)
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlDataAdapter sqlData = new SqlDataAdapter("execute timkiemtennv N'" + txbTimKiem.Text + "'", sqlcon);
                    DataTable dataTable = new DataTable();
                    sqlData.Fill(dataTable);
                    dtgvNV.DataSource = dataTable;
                }
            else if (rbtnTheoTen.Checked == false && rbtnTheoMa.Checked == false || txbTimKiem.Text == "")
                MessageBox.Show("Chưa chọn điều kiện tìm kiếm", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }*/

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (txbMaNhanVien.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nhân viên", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlCommand command = new SqlCommand("execute xoa1NV N'" + txbMaNhanVien.Text + "'", sqlcon);
                    sqlcon.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                    command.ExecuteNonQuery();
                }
                load();
            }
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            ktgt();
            if (txbDiaChi.Text == "" || comboBox1.Text == "" || txbMaNhanVien.Text == "" || txbSD_NV.Text == "" || txbTen_NV.Text == "")
            {
                MessageBox.Show("Mời nhập đầy đủ các thông tin của nhân viên này", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Regex.IsMatch(txbLuong_NV.Text, "\\d") == false)
                MessageBox.Show("Lương nhân viên phải là số tự nhiên lớn hơn 0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //else if(Regex.IsMatch(txbTen_NV.Text,"\\D")== true)
            //    MessageBox.Show("Tên nhân viên không thể có ký tự nằm ngoài a-z hoặc A-Z", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Regex.IsMatch(txbSD_NV.Text, "\\d") == false || txbSD_NV.Text.Length < 10)
                MessageBox.Show("Số điện thoại chưa chính xác", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (rbtnNam.Checked == false && rbtnNu.Checked == false)
                MessageBox.Show("Chưa chọn giới tính", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DialogResult result = MessageBox.Show("Bạn có muốn thay đổi thông tin nhân viên này thành: " + txbMaNhanVien.Text + "|" + txbTen_NV.Text + "|" + dtpkNgaySinh_NV.Value + "|" + txbSD_NV.Text + "|" + gioitinh + "|" + txbDiaChi.Text + "|", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection sqlcon = new SqlConnection(constr))
                    {
                        sqlcon.Open();
                        SqlCommand command = new SqlCommand("execute Capnhatthongtin N'" + txbMaNhanVien.Text + "', N'" + txbTen_NV.Text + "', '" + dtpkNgaySinh_NV.Value + "', N'" + txbSD_NV.Text + "', N'" + gioitinh + "', N'" + txbDiaChi.Text + "'", sqlcon);
                        sqlcon.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                        command.ExecuteNonQuery();
                    }
                }
                load();
            }
        }

        private void comboBox1_Index()
        {
            using (SqlConnection sqlcon = new SqlConnection(constr))
            {
                sqlcon.Open();
                SqlCommand command = new SqlCommand("select makhu from khuvuichoi ", sqlcon);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["makhu"].ToString());
                    comboBox1.DisplayMember = (reader["makhu"].ToString());
                    comboBox1.ValueMember = (reader["makhu"].ToString());
                }
            }
        }

        private void fQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            comboBox1_Index();
        }

        private void btnTimKiemNV_Click_1(object sender, EventArgs e)
        {

        }

        DateTime ns;

        private void dtgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dtgvNV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                DataGridViewRow row = this.dtgvNV.Rows[e.RowIndex];
                txbMaNhanVien.Text = row.Cells[0].Value.ToString();
                manv = row.Cells[0].Value.ToString();
                txbTen_NV.Text = row.Cells[1].Value.ToString();
                tennv = row.Cells[1].Value.ToString();
                dtpkNgaySinh_NV.Value = Convert.ToDateTime(row.Cells[2].Value);
                ns = Convert.ToDateTime(row.Cells[2].Value);
                txbSD_NV.Text = row.Cells[3].Value.ToString();
                sdt = row.Cells[3].Value.ToString();
                txbLuong_NV.Text = row.Cells[4].Value.ToString();
                luong = row.Cells[4].Value.ToString();
                comboBox1.Text = row.Cells[5].Value.ToString();
                makhu = row.Cells[5].Value.ToString();
                txbDiaChi.Text = row.Cells[6].Value.ToString();
                diachi = row.Cells[6].Value.ToString();
                if (rbtnNam.Text == row.Cells[7].Value.ToString())
                    rbtnNam.Checked = true;
                else
                    rbtnNu.Checked = true;
                ktgt();
                //row.Cells[7].Value.ToString();
            }
        }
        void ktgt()
        {
            if (rbtnNam.Checked == true && rbtnNu.Checked == false)
                gioitinh = "Nam";
            else if (rbtnNam.Checked == false && rbtnNu.Checked == true)
                gioitinh = "Nữ";
        }

        void load()
        {
            using (SqlConnection sqlcon = new SqlConnection(constr))
            {
                sqlcon.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("select manv as N'Mã nhân viên', tennv as N'Tên nhân viên', ngaysinh as N'Ngày sinh', sdt as N'Số điện thoại', luong as N'Lương',makhu as N'Mã khu', diachi as N'Địa chỉ', gioitinh as N'Giới tính' from nhanvien", sqlcon);
                DataTable dataTable = new DataTable();
                sqlData.Fill(dataTable);
                dtgvNV.DataSource = dataTable;
            }
        }
        private void btnXemNV_Click(object sender, EventArgs e)
        {
            load();
        }

        public static void InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            MessageBox.Show(myEvent.Message);
        }
    }
}
