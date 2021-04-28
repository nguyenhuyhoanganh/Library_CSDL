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
using TT_QLKVC.DTO;

namespace TT_QLKVC
{
    public partial class fQuanLyDichVu : Form
    {
        BindingSource DVlist = new BindingSource();
        public fQuanLyDichVu()
        {
            InitializeComponent();
        }


        void DVBinding()
        {
            txbMaDV.DataBindings.Add(new Binding("Text", dtgvDichVu.DataSource, "Mã dịch vụ", true, DataSourceUpdateMode.Never));
            txbTenDV.DataBindings.Add(new Binding("Text", dtgvDichVu.DataSource, "Tên dịch vụ", true, DataSourceUpdateMode.Never));
            //cb_LDV.DataBindings.Add(new Binding("Text", dtgvDichVu.DataSource, "MALDV", true, DataSourceUpdateMode.Never));
            txbDonGia.DataBindings.Add(new Binding("Text", dtgvDichVu.DataSource, "Đơn giá", true, DataSourceUpdateMode.Never));
        }

        void LoadLDVintocb(ComboBox cb)
        {
            cb.DataSource = LoaiDVDAO.Instance.GetListLoaiDV();
            cbMaLDV.ValueMember = "MALDV";
            cb.DisplayMember = "TENLDV";
        }

        void load()
        {
            string query = "exec ThongTinDV";
            dtgvDichVu.DataSource = DVlist;
            DVlist.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void txbMaDV_TextChanged(object sender, EventArgs e)
        {
            if (dtgvDichVu.SelectedCells.Count > 0)
            {
                string id = dtgvDichVu.SelectedCells[0].OwningRow.Cells["Mã loại dịch vụ"].Value.ToString();
                LoaiDV loaiDV = LoaiDVDAO.Instance.GetListLoaiDichVuById(id);
                cbMaLDV.SelectedItem = loaiDV;
                int index = -1;
                int i = 0;
                foreach (LoaiDV item in cbMaLDV.Items)
                {
                    if (item.MALDV == loaiDV.MALDV)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                cbMaLDV.SelectedIndex = index;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txbTK_MaDV.Text == "" && txbTK_TenDV.Text == "")
                MessageBox.Show("Chưa nhập thông tin tìm kiếm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (txbTK_MaDV.Text == "" && txbTK_TenDV.Text != "")
                {
                    string query = string.Format("exec TimKiemDV_Ten N'{0}'", txbTK_TenDV.Text);
                    dtgvDichVu.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    int count = dtgvDichVu.Rows.Count - 1;
                    MessageBox.Show("Tìm thấy " + count + " kết quả", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txbTK_MaDV.Text != "" && txbTK_TenDV.Text == "")
                {
                    string query = string.Format("exec TimKiemDV_Ma N'{0}'", txbTK_MaDV.Text);
                    dtgvDichVu.DataSource = DataProvider.Instance.ExecuteQuery(query);
                    int count = dtgvDichVu.Rows.Count - 1;
                    MessageBox.Show("Tìm thấy " + count + " kết quả", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txbTK_TenDV_TextChanged(object sender, EventArgs e)
        {
            if (txbTK_TenDV.Text != "")
            {
                txbTK_MaDV.ReadOnly = true;
            }
            else
            {
                txbTK_MaDV.ReadOnly = false;
            }
        }

        private void txbTK_MaDV_TextChanged(object sender, EventArgs e)
        {
            if (txbTK_MaDV.Text != "")
            {
                txbTK_TenDV.ReadOnly = true;
            }
            else
            {
                txbTK_TenDV.ReadOnly = false;
            }
        }

        private void fQuanLyDichVu_Load(object sender, EventArgs e)
        {
            load();
            DVBinding();
            LoadLDVintocb(cbMaLDV);
            readmode();
            groupBox3.Text = "Thông tin dịch vụ";
            txbTenDV.ReadOnly = true;
        }

        void readmode()
        {
            if (groupBox3.Text == "Thông tin dịch vụ")
            {
                txbMaDV.ReadOnly = true;
                txbMaDV.BackColor = Color.White;
                txbTenDV.ReadOnly = true;
                txbTenDV.BackColor = Color.White;
                txbDonGia.ReadOnly = true;
                txbDonGia.BackColor = Color.White;
                cbMaLDV.Enabled = false;
                cbMaLDV.BackColor = Color.White;
                btnLuu.Visible = false;
                btnHuy.Visible = false;
            }
            else if (groupBox3.Text == "Xóa thông tin dịch vụ")
            {
                txbMaDV.ReadOnly = false;
                txbTenDV.ReadOnly = true;
                txbDonGia.ReadOnly = true;
                cbMaLDV.Enabled = false;
                btnLuu.Visible = true;
                btnLuu.Text = "Xác nhận";
                btnLuu.BackColor = Color.Red;
                btnHuy.Visible = true;
            }
            else
            {
                txbMaDV.ReadOnly = true;
                txbTenDV.ReadOnly = false;
                txbDonGia.ReadOnly = false;
                cbMaLDV.Enabled = true;
                btnLuu.Visible = true;
                btnHuy.Visible = true;
                btnLuu.Text = "Lưu";
                btnLuu.BackColor = Color.White;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            groupBox3.Text = "Thông tin dịch vụ";
            groupBox3.ForeColor = Color.Black;
            readmode();
            load();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            txbMaDV.Text = "";
            txbTenDV.Text = "";
            txbDonGia.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            groupBox3.Text = "Thêm dịch vụ";
            groupBox3.ForeColor = Color.Green;
            btnLuu.ForeColor = groupBox3.ForeColor;
            readmode();
            txbMaDV.ReadOnly = true;
            string query = string.Format("exec auto_MaDV");
            txbMaDV.Text = DataProvider.Instance.ExecuteScalar(query).ToString();
            txbTenDV.Text = "";
            txbDonGia.Text = "";
            cbMaLDV.SelectedIndex = 0;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            groupBox3.Text = "Sửa thông tin dịch vụ";
            groupBox3.ForeColor = Color.Orange;
            btnLuu.ForeColor = groupBox3.ForeColor;
            readmode();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txbMaDV.ReadOnly = false;
            groupBox3.Text = "Xóa thông tin dịch vụ";
            groupBox3.ForeColor = Color.Red;
            btnLuu.ForeColor = Color.Black;
            readmode();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (groupBox3.Text == "Xóa thông tin dịch vụ")
            {
                if (txbMaDV.Text == "")
                {
                    MessageBox.Show("Chưa nhập mã dịch vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn xóa nhân viên có mã " + txbMaDV.Text, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.Yes)
                    {
                        if (DichVuDAO.Instance.DeleteDV(txbMaDV.Text))
                        {
                            MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            load();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Xóa dịch vụ không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else if (groupBox3.Text == "Sửa thông tin dịch vụ")
            {
                if (cbMaLDV.Text == "" || txbMaDV.Text == "" || txbDonGia.Text == "" || txbTenDV.Text == "")
                {
                    MessageBox.Show("Mời nhập đầy đủ các thông tin của dịch vụ này", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Regex.IsMatch(txbDonGia.Text, "\\d") == false)
                    MessageBox.Show("Đơn giá phải là số tự nhiên lớn hơn 0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //else if(Regex.IsMatch(txbTenDV.Text,"\\D")== true)
                //    MessageBox.Show("Tên dịch vụ không thể có ký tự nằm ngoài a-z hoặc A-Z", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn thay đổi thông tin dịch vụ này thành: " + txbMaDV.Text + "|" + txbTenDV.Text + "|" + txbDonGia.Text + "|" + cbMaLDV.Text  , "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (DichVuDAO.Instance.UpdateDV(txbMaDV.Text, txbTenDV.Text, Convert.ToDecimal(txbDonGia.Text), (cbMaLDV.SelectedItem as LoaiDV).MALDV))
                        {
                            MessageBox.Show("Sửa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            load();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Sửa dịch vụ không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }

            else if (groupBox3.Text == "Thêm dịch vụ")
            {
                if (cbMaLDV.Text == "" || txbMaDV.Text == "" || txbDonGia.Text == "" || txbTenDV.Text == "")
                {
                    MessageBox.Show("Mời nhập đầy đủ các thông tin của dịch vụ này", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Regex.IsMatch(txbDonGia.Text, "\\d") == false)
                    MessageBox.Show("Đơn giá dịch vụ phải là số tự nhiên lớn hơn 0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (Regex.IsMatch(txbTenDV.Text, "\\d") == true)
                    MessageBox.Show("Tên nhân viên không thể có ký tự nằm ngoài a-z hoặc A-Z", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    if (DichVuDAO.Instance.InsertDV(txbMaDV.Text, txbTenDV.Text, Convert.ToDecimal(txbDonGia.Text), (cbMaLDV.SelectedItem as LoaiDV).MALDV))
                    {
                        MessageBox.Show("Thêm loại dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        load();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Thêm dịch vụ không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
