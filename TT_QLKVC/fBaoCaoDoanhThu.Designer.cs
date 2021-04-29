namespace TT_QLKVC
{
    partial class fBaoCaoDoanhThu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxKhu = new System.Windows.Forms.ComboBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.datepkKT = new System.Windows.Forms.DateTimePicker();
            this.btnThongKeDT = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btTong = new Guna.UI2.WinForms.Guna2Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelThôngTinNhânViên = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxKhu);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.datepkKT);
            this.groupBox1.Controls.Add(this.btnThongKeDT);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.labelThôngTinNhânViên);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(948, 640);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Báo Cáo Doanh Thu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 74;
            this.label2.Text = "Khu";
            // 
            // comboBoxKhu
            // 
            this.comboBoxKhu.FormattingEnabled = true;
            this.comboBoxKhu.Items.AddRange(new object[] {
            "Tất cả"});
            this.comboBoxKhu.Location = new System.Drawing.Point(215, 46);
            this.comboBoxKhu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxKhu.Name = "comboBoxKhu";
            this.comboBoxKhu.Size = new System.Drawing.Size(160, 24);
            this.comboBoxKhu.TabIndex = 73;
            this.comboBoxKhu.SelectedIndexChanged += new System.EventHandler(this.comboBoxKhu_SelectedIndexChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(589, 47);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(98, 21);
            this.radioButton2.TabIndex = 72;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Trong năm";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(589, 18);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(107, 21);
            this.radioButton1.TabIndex = 71;
            this.radioButton1.Text = "Trong tháng";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // datepkKT
            // 
            this.datepkKT.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.datepkKT.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.datepkKT.CustomFormat = "MM/yyyy";
            this.datepkKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datepkKT.Location = new System.Drawing.Point(383, 47);
            this.datepkKT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datepkKT.Name = "datepkKT";
            this.datepkKT.Size = new System.Drawing.Size(199, 22);
            this.datepkKT.TabIndex = 70;
            // 
            // btnThongKeDT
            // 
            this.btnThongKeDT.BackColor = System.Drawing.SystemColors.Window;
            this.btnThongKeDT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKeDT.Location = new System.Drawing.Point(707, 18);
            this.btnThongKeDT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThongKeDT.Name = "btnThongKeDT";
            this.btnThongKeDT.Size = new System.Drawing.Size(119, 46);
            this.btnThongKeDT.TabIndex = 59;
            this.btnThongKeDT.Text = "Thống Kê";
            this.btnThongKeDT.UseVisualStyleBackColor = false;
            this.btnThongKeDT.Click += new System.EventHandler(this.btnThongKeDT_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(383, 28);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(37, 17);
            this.label31.TabIndex = 58;
            this.label31.Text = "Năm";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btTong);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.panel1.Location = new System.Drawing.Point(12, 71);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 564);
            this.panel1.TabIndex = 55;
            // 
            // btTong
            // 
            this.btTong.CheckedState.Parent = this.btTong;
            this.btTong.CustomImages.Parent = this.btTong;
            this.btTong.Enabled = false;
            this.btTong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTong.ForeColor = System.Drawing.Color.White;
            this.btTong.HoverState.Parent = this.btTong;
            this.btTong.Location = new System.Drawing.Point(611, 498);
            this.btTong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btTong.Name = "btTong";
            this.btTong.ShadowDecoration.Parent = this.btTong;
            this.btTong.Size = new System.Drawing.Size(309, 55);
            this.btTong.TabIndex = 1;
            this.btTong.Text = "Tổng:";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(4, 62);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Biểu đồ";
            series1.YValuesPerPoint = 4;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(916, 430);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "Doanh thu ";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.RoyalBlue;
            title1.Name = "title1";
            title1.Text = "Doanh thu ";
            this.chart1.Titles.Add(title1);
            // 
            // labelThôngTinNhânViên
            // 
            this.labelThôngTinNhânViên.AutoSize = true;
            this.labelThôngTinNhânViên.Location = new System.Drawing.Point(9, 153);
            this.labelThôngTinNhânViên.Name = "labelThôngTinNhânViên";
            this.labelThôngTinNhânViên.Size = new System.Drawing.Size(79, 17);
            this.labelThôngTinNhânViên.TabIndex = 54;
            this.labelThôngTinNhânViên.Text = "Doanh Thu";
            // 
            // fBaoCaoDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 654);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "fBaoCaoDoanhThu";
            this.Text = "fBaoCaoDoanhThu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnThongKeDT;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelThôngTinNhânViên;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DateTimePicker datepkKT;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxKhu;
        private Guna.UI2.WinForms.Guna2Button btTong;
    }
}