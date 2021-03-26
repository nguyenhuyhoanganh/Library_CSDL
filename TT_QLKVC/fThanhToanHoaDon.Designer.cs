namespace TT_QLKVC
{
    partial class fThanhToanHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnXemHoaDon = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nmudGiamGia = new System.Windows.Forms.NumericUpDown();
            this.btnXuatHoaDon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelThôngTinNhânViên = new System.Windows.Forms.Label();
            this.dtgvDanhSachHoaDon = new System.Windows.Forms.DataGridView();
            this.dtgvThongTinCuThe = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmudGiamGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDanhSachHoaDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvThongTinCuThe)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnXemHoaDon);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nmudGiamGia);
            this.groupBox1.Controls.Add(this.btnXuatHoaDon);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelThôngTinNhânViên);
            this.groupBox1.Controls.Add(this.dtgvDanhSachHoaDon);
            this.groupBox1.Controls.Add(this.dtgvThongTinCuThe);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(948, 640);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thanh Toán Hóa Đơn";
            // 
            // btnXemHoaDon
            // 
            this.btnXemHoaDon.BackColor = System.Drawing.SystemColors.Window;
            this.btnXemHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemHoaDon.Location = new System.Drawing.Point(50, 540);
            this.btnXemHoaDon.Name = "btnXemHoaDon";
            this.btnXemHoaDon.Size = new System.Drawing.Size(114, 46);
            this.btnXemHoaDon.TabIndex = 71;
            this.btnXemHoaDon.Text = "Xem";
            this.btnXemHoaDon.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(491, 535);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 70;
            this.label2.Text = "Giảm Giá";
            // 
            // nmudGiamGia
            // 
            this.nmudGiamGia.Location = new System.Drawing.Point(495, 557);
            this.nmudGiamGia.Name = "nmudGiamGia";
            this.nmudGiamGia.Size = new System.Drawing.Size(198, 22);
            this.nmudGiamGia.TabIndex = 69;
            // 
            // btnXuatHoaDon
            // 
            this.btnXuatHoaDon.BackColor = System.Drawing.SystemColors.Window;
            this.btnXuatHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatHoaDon.Location = new System.Drawing.Point(740, 540);
            this.btnXuatHoaDon.Name = "btnXuatHoaDon";
            this.btnXuatHoaDon.Size = new System.Drawing.Size(160, 46);
            this.btnXuatHoaDon.TabIndex = 68;
            this.btnXuatHoaDon.Text = "Xuất Hóa Đơn";
            this.btnXuatHoaDon.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(492, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 67;
            this.label1.Text = "Thông Tin Cụ Thể";
            // 
            // labelThôngTinNhânViên
            // 
            this.labelThôngTinNhânViên.AutoSize = true;
            this.labelThôngTinNhânViên.Location = new System.Drawing.Point(47, 78);
            this.labelThôngTinNhânViên.Name = "labelThôngTinNhânViên";
            this.labelThôngTinNhânViên.Size = new System.Drawing.Size(138, 17);
            this.labelThôngTinNhânViên.TabIndex = 66;
            this.labelThôngTinNhânViên.Text = "Danh Sách Hóa Đơn";
            // 
            // dtgvDanhSachHoaDon
            // 
            this.dtgvDanhSachHoaDon.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgvDanhSachHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvDanhSachHoaDon.Location = new System.Drawing.Point(50, 100);
            this.dtgvDanhSachHoaDon.Name = "dtgvDanhSachHoaDon";
            this.dtgvDanhSachHoaDon.RowHeadersWidth = 51;
            this.dtgvDanhSachHoaDon.RowTemplate.Height = 24;
            this.dtgvDanhSachHoaDon.Size = new System.Drawing.Size(407, 402);
            this.dtgvDanhSachHoaDon.TabIndex = 65;
            // 
            // dtgvThongTinCuThe
            // 
            this.dtgvThongTinCuThe.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgvThongTinCuThe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvThongTinCuThe.Location = new System.Drawing.Point(495, 100);
            this.dtgvThongTinCuThe.Name = "dtgvThongTinCuThe";
            this.dtgvThongTinCuThe.RowHeadersWidth = 51;
            this.dtgvThongTinCuThe.RowTemplate.Height = 24;
            this.dtgvThongTinCuThe.Size = new System.Drawing.Size(405, 402);
            this.dtgvThongTinCuThe.TabIndex = 64;
            // 
            // fThanhToanHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 653);
            this.Controls.Add(this.groupBox1);
            this.Name = "fThanhToanHoaDon";
            this.Text = "fThanhToanHoaDon";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmudGiamGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDanhSachHoaDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvThongTinCuThe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnXemHoaDon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmudGiamGia;
        private System.Windows.Forms.Button btnXuatHoaDon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelThôngTinNhânViên;
        private System.Windows.Forms.DataGridView dtgvDanhSachHoaDon;
        private System.Windows.Forms.DataGridView dtgvThongTinCuThe;
    }
}