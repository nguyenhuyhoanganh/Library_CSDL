//using Design_Login_Form.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
//using System.Timers;
//using s
namespace TT_QLKVC
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
        string gioitinh, ns;
        //DateTime ns;
        /*
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            ktgt();
            if (txbDiaChi.Text == "" || txbLuong_NV.Text == "" || comboBox1.Text == "" || txbMaNhanVien.Text == "" || txbSD_NV.Text == "" || txbTen_NV.Text == "")
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
                    SqlCommand command = new SqlCommand("execute themnv N'" + txbMaNhanVien.Text + "', N'" + txbTen_NV.Text + "', '" + dtpkNgaySinh_NV.Value + "', N'" + txbSD_NV.Text + "', N'" + gioitinh + "', N'" + txbLuong_NV.Text + "', N'" + comboBox1.Text + "', N'" + txbDiaChi.Text + "'", sqlcon);
                    //sqlcon.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                    command.ExecuteNonQuery();
                }
                load();
            }

        }
        */
        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            if (txbTimKiemten.Text == "" && txbTimKiemma.Text == "")
                MessageBox.Show("Chưa nhập điều kiện tìm kiếm", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlDataAdapter sqlData = new SqlDataAdapter("execute timkiemnv N'" + txbTimKiemma.Text + "',N'" + txbTimKiemten.Text + "'", sqlcon);
                    DataTable dataTable = new DataTable();
                    sqlData.Fill(dataTable);
                    dtgvNV.DataSource = dataTable;
                }
        }
        /*
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
        }*/

        private void comboBox1_Index()
        {
            comboBox1.Items.Clear();
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
            groupBox3.Text = "Thông tin nhân viên";
            readmode();
            comboBox1_Index();
            label_gt.Visible = false;
            //label_gt.Visible = false;
            rbtnNu.Visible = false;
            rbtnNam.Visible = false;
            load();
        }



        //DateTime ns;

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox3.Text = "Thông tin nhân viên";
            groupBox3.ForeColor = Color.Black;
            groupBox4.ForeColor = groupBox3.ForeColor;
            label_gt.Visible = false;
            rbtnNu.Visible = false;
            rbtnNam.Visible = false;
            readmode();
            load();

        }

        void readmode()
        {
            if (groupBox3.Text == "Thông tin nhân viên" || groupBox3.Text == "")
            {
                txbDiaChi.ReadOnly = true;
                txbDiaChi.BackColor = Color.White;
                txbLuong_NV.ReadOnly = true;
                txbLuong_NV.BackColor = Color.White;
                txbMaNhanVien.ReadOnly = true;
                txbMaNhanVien.BackColor = Color.White;
                txbSD_NV.ReadOnly = true;
                txbSD_NV.BackColor = Color.White;
                txbTen_NV.ReadOnly = true;
                txbTen_NV.BackColor = Color.White;
                dtpkNgaySinh_NV.Enabled = false;
                rbtnNam.Enabled = false;
                rbtnNu.Enabled = false;
                comboBox1.Enabled = false;
                comboBox1.BackColor = Color.White;
                button6.Visible = false;
                button7.Visible = false;
            }
            else if (groupBox3.Text == "Xóa thông tin nhân viên")
            {
                txbDiaChi.ReadOnly = true;
                txbLuong_NV.ReadOnly = true;
                txbMaNhanVien.ReadOnly = false;
                txbSD_NV.ReadOnly = true;
                txbTen_NV.ReadOnly = false;
                dtpkNgaySinh_NV.Enabled = false;
                rbtnNam.Enabled = false;
                rbtnNu.Enabled = false;
                comboBox1.Enabled = false;
                button6.Visible = true;
                button6.Text = "Xác nhận";
                button6.BackColor = Color.Red;
                button7.Visible = true;
            }
            else
            {
                txbDiaChi.ReadOnly = false;
                txbLuong_NV.ReadOnly = false;
                txbMaNhanVien.ReadOnly = false;
                txbSD_NV.ReadOnly = false;
                txbTen_NV.ReadOnly = false;
                dtpkNgaySinh_NV.Enabled = true;
                rbtnNam.Enabled = true;
                rbtnNu.Enabled = true;
                comboBox1.Enabled = true;
                button6.Visible = true;
                button7.Visible = true;
                button6.Text = "Lưu";
                button6.BackColor = Color.White;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txbDiaChi.Text = "";
            txbLuong_NV.Text = "";
            txbMaNhanVien.Text = "";
            txbSD_NV.Text = "";
            txbTen_NV.Text = "";
            dtpkNgaySinh_NV.Value = DateTime.Today;
            rbtnNam.Checked = true;
            rbtnNu.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox3.Text = "Thêm nhân viên";
            groupBox3.ForeColor = Color.Green;
            groupBox4.ForeColor = Color.Green;
            button6.ForeColor = groupBox4.ForeColor;
            readmode();
            using (SqlConnection sqlcon = new SqlConnection(constr))
            {
                sqlcon.Open();
                SqlCommand command = new SqlCommand("exec auto_manv", sqlcon);
                txbMaNhanVien.Text = command.ExecuteScalar().ToString(); 
            }
                txbDiaChi.Text = "";
            txbLuong_NV.Text = "";
            //txbMaNhanVien.Text = "";
            txbSD_NV.Text = "";
            txbTen_NV.Text = "";
            dtpkNgaySinh_NV.Value = DateTime.Today;
            label_gt.Visible = false;
            rbtnNu.Visible = true;
            rbtnNam.Visible = true;
            rbtnNam.Checked = true;
            rbtnNu.Checked = false;
            //comboBox1_Index();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox3.Text = "Sửa thông tin nhân viên";
            groupBox3.ForeColor = Color.Orange;
            groupBox4.ForeColor = groupBox3.ForeColor;
            button6.ForeColor = groupBox4.ForeColor;
            label_gt.Visible = false;
            rbtnNu.Visible = true;
            rbtnNam.Visible = true;
            readmode();
            comboBox1_Index();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (groupBox3.Text == "Xóa thông tin nhân viên")
            {
                if (txbMaNhanVien.Text == "")
                {
                    MessageBox.Show("Chưa nhập mã nhân viên", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn xóa nhân viên có mã " + txbMaNhanVien.Text, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.Yes)
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
            }
            else if (groupBox3.Text == "Sửa thông tin nhân viên")
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
                    DialogResult result = MessageBox.Show("Bạn có muốn thay đổi thông tin nhân viên này thành: " + txbMaNhanVien.Text + "|" + txbTen_NV.Text + "|" + dtpkNgaySinh_NV.Value + "|" + txbSD_NV.Text + "|" + gioitinh + "|" + txbDiaChi.Text + "|" + txbLuong_NV.Text + "|" + comboBox1.Text + "|" + comboBox_cv.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection sqlcon = new SqlConnection(constr))
                        {
                            sqlcon.Open();
                            SqlCommand command = new SqlCommand("execute Capnhatthongtin N'" + txbMaNhanVien.Text + "', N'" + txbTen_NV.Text + "', '" + dtpkNgaySinh_NV.Value + "', N'" + txbSD_NV.Text + "', N'" + gioitinh + "', N'" + txbDiaChi.Text + "', N'" + txbLuong_NV.Text + "', N'" + comboBox1.Text + "', N'" + comboBox_cv.Text + "', N'" + manv + "'", sqlcon);
                            sqlcon.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                            command.ExecuteNonQuery();
                        }
                    }
                    load();
                }
            }

            else if (groupBox3.Text == "Thêm nhân viên")
            {
                ktgt();
                if (txbDiaChi.Text == "" || txbLuong_NV.Text == "" || comboBox1.Text == "" || txbMaNhanVien.Text == "" || txbSD_NV.Text == "" || txbTen_NV.Text == "")
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
                        SqlCommand command = new SqlCommand("execute themnv N'" + txbMaNhanVien.Text + "', N'" + txbTen_NV.Text + "', '" + dtpkNgaySinh_NV.Value + "', N'" + txbSD_NV.Text + "', N'" + gioitinh + "', N'" + txbLuong_NV.Text + "', N'" + comboBox_cv.Text + "', N'" + comboBox1.Text + "', N'" + txbDiaChi.Text + "'", sqlcon);
                        sqlcon.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                        command.ExecuteNonQuery();
                    }
                    load();
                }
            }
            //groupBox3.Text = "Thông tin nhân viên";

            //readmode();
        }
        //private Timer aTimer;
        private void button4_Click(object sender, EventArgs e)
        {
            //if(groupBox3.Text == "Xóa thông tin nhân viên")
            //{
            //    aTimer = new System.Timers.Timer();
            //    aTimer.Interval = 2000;
            //    aTimer.button6.BackColor = Color.White;
            //    button6.BackColor=Color.Red;
            //    aTimer.button6.BackColor = Color.Red;
            //    button6.BackColor = Color.Red;
            //}    
            groupBox3.Text = "Xóa thông tin nhân viên";
            groupBox3.ForeColor = Color.Red;
            groupBox4.ForeColor = groupBox3.ForeColor;
            button6.ForeColor = Color.Black;
            label_gt.Visible = false;
            rbtnNu.Visible = true;
            rbtnNam.Visible = true;
            readmode();
        }

        private void fQuanLyNhanVien_Load_1(object sender, EventArgs e)
        {

            load();
            comboBox1_Index();
        }
        //DataGridViewCellFormattingEventArgs e;
        private void dtgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dtgvNV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                DataGridViewRow row = this.dtgvNV.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    txbMaNhanVien.Text = row.Cells[0].Value.ToString();
                    manv = row.Cells[0].Value.ToString();
                }
                if (row.Cells[1].Value != null)
                {
                    txbTen_NV.Text = row.Cells[1].Value.ToString();
                    tennv = row.Cells[1].Value.ToString();
                }
                if (row.Cells[3].Value != null)
                {
                    ns = row.Cells[3].Value.ToString();
                    if (ns != "")
                    {
                        dtpkNgaySinh_NV.Value = Convert.ToDateTime(ns);
                    }
                    else
                        dtpkNgaySinh_NV.Value = DateTime.Today;
                }
                //else
                //{
                //    dtpkNgaySinh_NV.Value = Convert.ToDateTime(row.Cells[2].Value);
                //    ns = " ";//Convert.ToDateTime(row.Cells[2].Value);
                //}
                if (row.Cells[4].Value != null)
                {
                    txbSD_NV.Text = row.Cells[4].Value.ToString();
                    sdt = row.Cells[4].Value.ToString();
                }
                if (row.Cells[5].Value != null)
                {
                    txbLuong_NV.Text = row.Cells[5].Value.ToString();
                    luong = row.Cells[5].Value.ToString();
                }
                if (row.Cells[6].Value != null)
                {
                    comboBox1.Text = row.Cells[6].Value.ToString();
                    makhu = row.Cells[6].Value.ToString();
                }
                if (row.Cells[7].Value != null)
                {
                    txbDiaChi.Text = row.Cells[7].Value.ToString();
                    diachi = row.Cells[7].Value.ToString();
                }
                if (row.Cells[8].Value != null)
                {
                    if (rbtnNam.Text == row.Cells[8].Value.ToString())
                        rbtnNam.Checked = true;
                    else
                        rbtnNu.Checked = true;
                    ktgt();
                }
                if (row.Cells[2].Value != null)
                {
                    comboBox_cv.Text = row.Cells[2].Value.ToString();
                }
                if (groupBox3.Text == "Thông tin nhân viên")
                {
                    label_gt.Visible = true;
                    rbtnNam.Visible = false;
                    rbtnNu.Visible = false;
                    if (rbtnNam.Checked == true)
                    {
                        label_gt.Text = rbtnNam.Text;
                    }
                    else
                    {
                        label_gt.Text = rbtnNu.Text;
                    }
                }
                else
                {
                    label_gt.Visible = false;
                    rbtnNam.Visible = true;
                    rbtnNu.Visible = true;
                }


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
                    SqlDataAdapter sqlData = new SqlDataAdapter("select manv as N'Mã nhân viên', tennv as N'Tên nhân viên', chucvu as N'Chức vụ', ngaysinh as N'Ngày sinh', sdt as N'Số điện thoại', luong as N'Lương',makhu as N'Mã khu', diachi as N'Địa chỉ', gioitinh as N'Giới tính' from nhanvien", sqlcon);
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

