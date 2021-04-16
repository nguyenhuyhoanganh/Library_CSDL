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
            this.label1 = new System.Windows.Forms.Label();
            this.txbTongDoanhThu = new System.Windows.Forms.TextBox();
            this.btnThongKeDT = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txbTongDoanhThu);
            this.groupBox1.Controls.Add(this.btnThongKeDT);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.labelThôngTinNhânViên);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.groupBox1.Location = new System.Drawing.Point(9, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(711, 520);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Báo Cáo Doanh Thu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 74;
            this.label2.Text = "Khu";
            // 
            // comboBoxKhu
            // 
            this.comboBoxKhu.FormattingEnabled = true;
            this.comboBoxKhu.Items.AddRange(new object[] {
            "Tất cả"});
            this.comboBoxKhu.Location = new System.Drawing.Point(161, 37);
            this.comboBoxKhu.Name = "comboBoxKhu";
            this.comboBoxKhu.Size = new System.Drawing.Size(121, 21);
            this.comboBoxKhu.TabIndex = 73;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(442, 38);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(76, 17);
            this.radioButton2.TabIndex = 72;
            this.radioButton2.Text = "Trong năm";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(442, 15);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 17);
            this.radioButton1.TabIndex = 71;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Trong tháng";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // datepkKT
            // 
            this.datepkKT.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.datepkKT.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.datepkKT.CustomFormat = "MM/yyyy";
            this.datepkKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datepkKT.Location = new System.Drawing.Point(287, 38);
            this.datepkKT.Margin = new System.Windows.Forms.Padding(2);
            this.datepkKT.Name = "datepkKT";
            this.datepkKT.Size = new System.Drawing.Size(150, 20);
            this.datepkKT.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(514, 496);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Tổng:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txbTongDoanhThu
            // 
            this.txbTongDoanhThu.Location = new System.Drawing.Point(553, 496);
            this.txbTongDoanhThu.Margin = new System.Windows.Forms.Padding(2);
            this.txbTongDoanhThu.Name = "txbTongDoanhThu";
            this.txbTongDoanhThu.Size = new System.Drawing.Size(149, 20);
            this.txbTongDoanhThu.TabIndex = 62;
            // 
            // btnThongKeDT
            // 
            this.btnThongKeDT.BackColor = System.Drawing.SystemColors.Window;
            this.btnThongKeDT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKeDT.Location = new System.Drawing.Point(530, 15);
            this.btnThongKeDT.Margin = new System.Windows.Forms.Padding(2);
            this.btnThongKeDT.Name = "btnThongKeDT";
            this.btnThongKeDT.Size = new System.Drawing.Size(89, 37);
            this.btnThongKeDT.TabIndex = 59;
            this.btnThongKeDT.Text = "Thống Kê";
            this.btnThongKeDT.UseVisualStyleBackColor = false;
            this.btnThongKeDT.Click += new System.EventHandler(this.btnThongKeDT_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(287, 23);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(29, 13);
            this.label31.TabIndex = 58;
            this.label31.Text = "Năm";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chart1);
            this.panel1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.panel1.Location = new System.Drawing.Point(9, 58);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 434);
            this.panel1.TabIndex = 55;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 82);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Biểu đồ";
            series1.YValuesPerPoint = 4;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(687, 349);
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
            this.labelThôngTinNhânViên.Location = new System.Drawing.Point(7, 124);
            this.labelThôngTinNhânViên.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelThôngTinNhânViên.Name = "labelThôngTinNhânViên";
            this.labelThôngTinNhânViên.Size = new System.Drawing.Size(61, 13);
            this.labelThôngTinNhânViên.TabIndex = 54;
            this.labelThôngTinNhânViên.Text = "Doanh Thu";
            // 
            // fBaoCaoDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 531);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbTongDoanhThu;
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
    }
}