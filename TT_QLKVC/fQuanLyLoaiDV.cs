using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TT_QLKVC.DAO;

namespace TT_QLKVC
{
    public partial class fQuanLyLoaiDV : Form
    {
        string maldv, tenldv;
        public fQuanLyLoaiDV()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txbTK_MaLDV.Text == "" && txbTK_TenLDV.Text == "")
                MessageBox.Show("Chưa nhập thông tin tìm kiếm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (txbTK_MaLDV.Text == "" && txbTK_TenLDV.Text != "")
                {
                    string query = string.Format("exec TimKiemLDV_Ten N'{0}'", txbTK_TenLDV.Text);
                    dtgvLoaiDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    int count = dtgvLoaiDV.Rows.Count - 1;
                    MessageBox.Show("Tìm thấy " + count + " kết quả", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txbTK_MaLDV.Text != "" && txbTK_TenLDV.Text == "")
                {
                    string query = string.Format("exec TimKiemLDV_Ma N'{0}'", txbTK_MaLDV.Text);
                    dtgvLoaiDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    int count = dtgvLoaiDV.Rows.Count - 1;
                    MessageBox.Show("Tìm thấy " + count + " kết quả", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txbTK_TenLDV_TextChanged(object sender, EventArgs e)
        {
            if (txbTK_TenLDV.Text != "")
            {
                txbTK_MaLDV.ReadOnly = true;
            }
            else
            {
                txbTK_MaLDV.ReadOnly = false;
            }
        }

        private void txbTK_MaLDV_TextChanged(object sender, EventArgs e)
        {
            if (txbTK_MaLDV.Text != "")
            {
                txbTK_TenLDV.ReadOnly = true;
            }
            else
            {
                txbTK_TenLDV.ReadOnly = false;
            }
        }

        void load()
        {
            string query = "exec ThongTinLDV";
            dtgvLoaiDV.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void dtgvLoaiDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dtgvLoaiDV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                DataGridViewRow row = this.dtgvLoaiDV.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    txbMaLDV.Text = row.Cells[0].Value.ToString();
                    maldv = row.Cells[0].Value.ToString();
                }
                if (row.Cells[1].Value != null)
                {
                    txbTenLDV.Text = row.Cells[1].Value.ToString();
                    tenldv = row.Cells[1].Value.ToString();
                }
            }
        }

        void clear()
        {
            txbMaLDV.Text = "";
            txbTenLDV.Text = "";
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            groupBox3.Text = "Thông tin loại dịch vụ";
            groupBox3.ForeColor = Color.Black;
            btnLuu.Visible = false;
            btnHuy.Visible = false;
            readmode();
            load();
            clear();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            groupBox3.Text = "Thêm loại dịch vụ";
            groupBox3.ForeColor = Color.Green;
            btnLuu.ForeColor = groupBox3.ForeColor;
            readmode();
            txbMaLDV.ReadOnly = true;
            string query = string.Format("exec auto_MaLDV");
            txbMaLDV.Text = DataProvider.Instance.ExecuteScalar(query).ToString();
            txbTenLDV.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txbMaLDV.ReadOnly = true;
            txbTenLDV.ReadOnly = false;
            groupBox3.Text = "Sửa thông tin loại dịch vụ";
            groupBox3.ForeColor = Color.Orange;
            btnLuu.ForeColor = groupBox3.ForeColor;
            readmode();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txbMaLDV.ReadOnly = false;
            groupBox3.Text = "Xóa thông tin loại dịch vụ";
            groupBox3.ForeColor = Color.Red;
            btnLuu.ForeColor = Color.Black;
            readmode();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (groupBox3.Text == "Xóa thông tin loại dịch vụ")
            {
                if (txbMaLDV.Text == "")
                {
                    MessageBox.Show("Chưa nhập mã loại dịch vụ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn xóa loại dịch vụ có mã " + txbMaLDV.Text, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.Yes)
                    {
                        maldv = txbMaLDV.Text;

                        if (LoaiDVDAO.Instance.DeleteLoaiDV(txbMaLDV.Text))
                        {
                            MessageBox.Show("Xóa loại dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            load();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Xóa loại dịch vụ không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        

                    }
                }
            }
            else if (groupBox3.Text == "Sửa thông tin loại dịch vụ")
            {
                if (txbMaLDV.Text == "" || txbTenLDV.Text == "")
                {
                    MessageBox.Show("Mời nhập đầy đủ các thông tin của loại dịch vụ này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //else if(Regex.IsMatch(txbMaLDV.Text,"\\D")== true)
                //    MessageBox.Show("Tên nhân viên không thể có ký tự nằm ngoài a-z hoặc A-Z", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn thay đổi thông tin loại dịch vụ này thành: " + txbMaLDV.Text + "|" + txbTenLDV.Text , "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (LoaiDVDAO.Instance.UpdateLoaiDV(txbMaLDV.Text, txbTenLDV.Text))
                        {
                            MessageBox.Show("Sửa loại dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            load();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Sửa loại dịch vụ không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }

            else if (groupBox3.Text == "Thêm loại dịch vụ")
            {
                if (txbMaLDV.Text == "" || txbTenLDV.Text == "")
                {
                    MessageBox.Show("Mời nhập đầy đủ các thông tin của loại dịch vụ này", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Regex.IsMatch(txbTenLDV.Text, "\\d") == true)
                    MessageBox.Show("Tên loại dịch vụ không thể có ký tự nằm ngoài a-z hoặc A-Z", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {

                    if (LoaiDVDAO.Instance.InsertLoaiDV(txbMaLDV.Text, txbTenLDV.Text))
                    {
                        MessageBox.Show("Thêm loại dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clear();
                        load();
                    }
                    else
                    {
                        MessageBox.Show("Thêm loại dịch vụ không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                 
                }
            }
        }

        private void fQuanLyLoaiDV_Load(object sender, EventArgs e)
        {
            readmode();
            groupBox3.Text = "Thông tin loại dịch vụ";
            load();
            txbMaLDV.ReadOnly = true;
        }


        void readmode()
        {
            if (groupBox3.Text == "Thông tin loại dịch vụ" )
            {
                txbMaLDV.ReadOnly = true;
                txbMaLDV.BackColor = Color.White;
                txbTenLDV.ReadOnly = true;
                txbTenLDV.BackColor = Color.White;
                btnLuu.Visible = false;
                btnHuy.Visible = false;
            }
            else if (groupBox3.Text == "Xóa thông tin loại dịch vụ")
            {
                txbMaLDV.ReadOnly = false;
                txbTenLDV.ReadOnly = false;
                btnLuu.Visible = true;
                btnLuu.Text = "Xác nhận";
                btnLuu.BackColor = Color.Red;
                btnHuy.Visible = true;
            }
            else
            {
                txbMaLDV.ReadOnly = false;
                txbTenLDV.ReadOnly = false;
                btnLuu.Visible = true;
                btnHuy.Visible = true;
                btnLuu.Text = "Lưu";
                btnLuu.BackColor = Color.White;
            }


        }
    }
}
