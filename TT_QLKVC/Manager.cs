using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TT_QLKVC
{


    public partial class Manager : Form
    {
        private Form activeForm = new Form();//Form đang dùng

        private Guna2Button currentBtn;//btuton đang ấn
        private Guna2Button subBtn;//Subbtuton đang ấn

        int thongTinTaiKhoan = 0;
        #region Status QuanLy
        int quanLy = 0;

        #endregion
        #region Status BaoCao

        int baoCao = 0;

        #endregion
        #region Status ThanhToan

        int thanhToan = 0;

        #endregion

        public Manager()
        {
            InitializeComponent();
            customizeDesing();
            statusSub();
            showStatus(btnStatus1);
        }

        private void openForm(Form form)
        {

            if (activeForm != null)
                activeForm.Close();
            activeForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelForm.Controls.Add(form);
            panelForm.Tag = form;
            form.BringToFront();
            form.Show();
        }
        #region Status in TillerBar
        public void statusSub()
        {
            btnStatus1.Visible = false;
            btnStatus2.Visible = false;
            btnStatus3.Visible = false;
            btnStatus4.Visible = false;
            btnStatus5.Visible = false;
        }
        public void hidenStatus()
        {
            if( btnStatus1.Visible == true)
                btnStatus1.Visible = false;
            if (btnStatus2.Visible == true)
                btnStatus2.Visible = false;
            if (btnStatus3.Visible == true)
                btnStatus3.Visible = false;
            if (btnStatus4.Visible == true)
                btnStatus4.Visible = false;
            if (btnStatus5.Visible == true)
                btnStatus5.Visible = false;
        }
        public void showStatus(Guna2Button status)
        {
            if (status.Visible == false)
            {
                hidenStatus();
                status.Visible = true;
            }
        }
        #endregion
        #region Show Submenu
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private void customizeDesing()
        {
            panelSubQuanLy.Visible = false;
            panelSubBaoCao.Visible = false;
            panelSubThanhToan.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelSubQuanLy.Visible == true)
                panelSubQuanLy.Visible = false;
            if (panelSubBaoCao.Visible == true)
                panelSubBaoCao.Visible = false;
            if (panelSubThanhToan.Visible == true)
                panelSubThanhToan.Visible = false;
        }

        #endregion
        #region Button đóng
        private void DisableButton()
        {
            if (currentBtn != null)//nếu btn đang ấn chưa đóng 
            {
                currentBtn.FillColor = CColor.color10;//Đưa màu về như cũ
                //currentBtn.ForeColor = Color.FromArgb(78, 184, 206);//màu chữ về như cũ

            }
        }
        private void DisableSubButton()
        {
            if (subBtn != null)//nếu btn đang ấn chưa đóng 
            {
                subBtn.FillColor = CColor.color10;//Đưa màu về như cũ
                //subBtn.ForeColor = Color.LightGray;//màu chữ về như cũ
            }
        }
        private void Reset()
        {
            DisableButton();
            DisableSubButton();
            lbTillerBar.Text = "Menu";
            if (activeForm != null)
                activeForm.Close();
        }

        #endregion
        #region Button mở
        private void ActivateButton(object senderBtn /*, Color color */)
        {
            if (senderBtn != null)
            {
                DisableButton();//hủy button hiện tại
                //Button
                currentBtn = (Guna2Button)senderBtn;
                currentBtn.FillColor = CColor.color8;
                lbTillerBar.Text = currentBtn.Text;
                //currentBtn.ForeColor = color;//Đổi Màu Chữ Thành Màu Được truyền Vào
                if (activeForm != null)
                    activeForm.Close();
            }
        }
        private void ActivateSubButton(object senderBtn /*, Color color */)
        {
            if (senderBtn != null)
            {
                DisableSubButton();//hủy button hiện tại
                //Button
                subBtn = (Guna2Button)senderBtn;
                subBtn.FillColor = CColor.color8;//Đổi Màu Back Color
            }
        }
        #endregion

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            if (thongTinTaiKhoan == 0)
            {
                thanhToan = 0;
                quanLy = 0;
                baoCao = 0;
                thongTinTaiKhoan = 1;

                showStatus(btnStatus5);
                ActivateButton(sender);
                hideSubMenu();
                openForm(new fThongTinTaiKhoan());
            }
            else
            {
                Reset();

                showStatus(btnStatus1);
                thongTinTaiKhoan = 0; 
            }    
        }
        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            if (quanLy == 0)
            {
                thanhToan = 0;
                thongTinTaiKhoan = 0;
                baoCao = 0;
                quanLy = 1;
                showStatus(btnStatus2);
                ActivateButton(sender);
            }//Đang đóng
            else
            {
                Reset();

                showStatus(btnStatus1);
                quanLy = 0;
            }//Đang Mở

            showSubMenu(panelSubQuanLy);
        }

        #region Quản Lý

        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnQLNhanVien);
            openForm(new fQuanLyNhanVien());
        }

        private void btnQLKhu_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnQLKhu);
            openForm(new fQuanLyKhu());
        }

        private void btnQLLoaiDichVu_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnQLLoaiDichVu);
            openForm(new fQuanLyLoaiDV());
        }

        private void btnQLDichVu_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnQLDichVu);
            openForm(new fQuanLyDichVu());
        }

        private void btnQLTroChoi_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnQLTroChoi);
            openForm(new fQuanLyTroChoi());
        }

        private void btnQuanLyVe_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnQuanLyVe);
            openForm(new fQuanLyVe());
        }
        #endregion

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            if (baoCao == 0)
            {
                thanhToan = 0;
                thongTinTaiKhoan = 0;
                quanLy = 0;
                baoCao = 1;

                showStatus(btnStatus3);
                ActivateButton(sender);

            }//Đang đóng
            else
            {
                Reset();

                showStatus(btnStatus1);
                baoCao = 0;
            }//Đang Mở
            showSubMenu(panelSubBaoCao);

        }

        #region Báo Cáo
        private void btnBCDoanhThu_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnBCDoanhThu);
            openForm(new fBaoCaoDoanhThu());
        }

        private void btnBCLuong_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnBCLuong);
            openForm(new fBaoCaoLuong());
        }

        #endregion

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if(thanhToan == 0)
            {
                thanhToan = 1;
                thongTinTaiKhoan = 0;
                quanLy = 0;
                baoCao = 0;

                showStatus(btnStatus4);
                ActivateButton(sender);
                
            }
            else
            {
                Reset();

                showStatus(btnStatus1);
                thanhToan = 0;
            }
            showSubMenu(panelSubThanhToan);
        }

        #region Thanh Toán
        private void btnTTHoaDon_Click(object sender, EventArgs e)
        {
            ActivateSubButton(btnTTHoaDon);
            openForm(new fThanhToanHoaDon());
        }

        #endregion

        #region Thanh Tiêu Đề, Thoát, Thu Nhỏ, Home, SignOut
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

        public void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            showStatus(btnStatus1);
            Reset();
            hideSubMenu();
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }

}
