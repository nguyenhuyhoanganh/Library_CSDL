using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TT_QLKVC.DAO;

namespace TT_QLKVC
{
    public partial class fBaoCaoDoanhThu : Form
    {
        public fBaoCaoDoanhThu()
        {
            InitializeComponent();
            load();
        }
        #region load
        void load()
        {
            DataTable da = new DataTable();
            da = DataProvider.Instance.ExecuteQuery("Select makhu from khuvuichoi");
            foreach (DataRow item in da.Rows)
            {
                comboBoxKhu.Items.Add(item["makhu"].ToString());
            }
        }
        #endregion
        private void label1_Click(object sender, EventArgs e)
        {
            /*            chart1.Series["serie1"].Points.AddXY("1", 20002);
                        chart1.Series["serie1"].Points.AddXY("2", 12525);
                        chart1.Series["serie1"].Points.AddXY("3", 12525);
                        chart1.Series["serie1"].Points.AddXY("4", 12525);
                        chart1.Series["serie1"].Points.AddXY("5", 12525);
                        chart1.Series["serie1"].Points.AddXY("6", 12525);
                        chart1.Series["serie1"].Points.AddXY("7", 12525);
                        chart1.Series["serie1"].Points.AddXY("8", 12525);
                        chart1.Series["serie1"].Points.AddXY("9", 12525);
                        chart1.Series["serie1"].Points.AddXY("10", 12525);
                        chart1.Series["serie1"].Points.AddXY("11", 12525);
                        chart1.Series["serie1"].Points.AddXY("12", 12525);*/


        }
        private void btnThongKeDT_Click(object sender, EventArgs e)
        {
            double total = 0;
            btTong.Text = "Tổng:";
            chart1.Series.Clear();
            chart1.Series.Add("Biểu đồ");
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 12;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            if (comboBoxKhu.Text!="")
            {
                if (radioButton1.Checked)
                {
                    //theo tháng: 
                    /*1 thống kê từng khu
                        2. thống kê tổng*/
                    switch (comboBoxKhu.Text)
                    {
                        case "Tất cả":
                            {
                                int thang = datepkKT.Value.Month;
                                int year = datepkKT.Value.Year;
                                int max = 0;
                                DataTable dataTable = new DataTable();
                                //tìm max Y
                                try
                                {
                                    dataTable = DataProvider.Instance.ExecuteQuery("thongketatca_khu_thang @a , @b", new object[] { "" + thang + "/01/" + year + "", "" + (thang + 1) + "/01/" + year + "" });
                                    DataRow dataRow = dataTable.Rows[0];
                                    if (Convert.ToInt32(dataRow["DichVu"]) >= Convert.ToInt32(dataRow["TienVe"]))
                                    {
                                        max = Convert.ToInt32(dataRow["DichVu"]) + 100;
                                    }
                                    else
                                    {
                                        max = Convert.ToInt32(dataRow["TienVe"]) + 100;
                                    }

                                }
                                catch
                                {
                                    max = 1000;
                                }
                                for (int i = 1000; i <= 100000; i = i + 1000)
                                {
                                    if (max > 0 && max <= i)
                                    {
                                        max = i;
                                        break;
                                    }

                                }
                                //set max cho chart
                                chart1.ChartAreas[0].AxisY.Maximum = max;
                                chart1.ChartAreas[0].AxisY.Minimum = 0;
                                chart1.ChartAreas[0].AxisX.Maximum = 3;
                                chart1.ChartAreas[0].AxisX.Minimum = 0;
                                //MessageBox.Show("KP");
                                //tất cả các khu

                                if (thang < 12)
                                {
                                    dataTable = DataProvider.Instance.ExecuteQuery("thongketatca_khu_thang @a , @b", new object[] { "" + thang + "/01/" + year + "", "" + (thang + 1) + "/01/" + year + "" });
                                    foreach (DataRow item in dataTable.Rows)
                                    {
                                        chart1.Series["Biểu đồ"].Points.AddXY("Tiền Vé", item["TienVe"]);
                                        chart1.Series["Biểu đồ"].Points.AddXY("Dịch Vụ", item["DichVu"]);
                                        try
                                        {
                                            total = Convert.ToDouble(item["TienVe"]) + Convert.ToDouble(item["DichVu"]);
                                        }catch
                                        {
                                            if(item["TienVe"].ToString()!="")
                                            {
                                                total = total+Convert.ToDouble(item["TienVe"]);
                                            }  
                                            if(item["DichVu"].ToString()!="")
                                            {
                                                total = total + Convert.ToDouble(item["DichVu"]);
                                            }    
                                        }
                                       
                                    }
                                    btTong.Text = btTong.Text + " " + total.ToString();
                                }
                                else
                                {
                                    dataTable = DataProvider.Instance.ExecuteQuery("thongketaca_khu_thang @a , @b", new object[] { "" + thang + "/01/" + year + "", "01/01/" + (year + 1) + "" });
                                    foreach (DataRow item in dataTable.Rows)
                                    {
                                        chart1.Series["Biểu đồ"].Points.AddXY("Tiền Vé", item["TienVe"]);
                                        chart1.Series["Biểu đồ"].Points.AddXY("Dịch Vụ", item["DichVu"]);
                                        try
                                        {
                                            total = Convert.ToDouble(item["TienVe"]) + Convert.ToDouble(item["DichVu"]);
                                        }
                                        catch
                                        {
                                            if (item["TienVe"].ToString() != "")
                                            {
                                                total = total + Convert.ToDouble(item["TienVe"]);
                                            }
                                            if (item["DichVu"].ToString() != "")
                                            {
                                                total = total + Convert.ToDouble(item["DichVu"]);
                                            }
                                        }

                                    }
                                    btTong.Text = btTong.Text + " " + total.ToString();
                                }

                                break;
                            }
                        default:
                            {
                                //theo khu
                                double tongKhu = 0;
                                int thang = datepkKT.Value.Month;
                                int year = datepkKT.Value.Year;
                                double max = 0;
                                DataTable dataTable = new DataTable();
                                try
                                {
                                    dataTable = DataProvider.Instance.ExecuteQuery("thongketung_khu_thang @a , @b , @c", new object[] { "" + thang + "/01/" + year + "", "" + (thang + 1) + "/01/" + year + "", comboBoxKhu.Text });
                                    DataRow dataRow = dataTable.Rows[0];
                                    max = Convert.ToDouble(dataRow["TienVe"].ToString());

                                }
                                catch
                                {
                                    max = 1000;
                                }
                                for (int i = 1000; i <= 100000; i = i + 1000)
                                {
                                    if (max > 0 && max <= i)
                                    {
                                        max = i;
                                        break;
                                    }

                                }
                                //set max cho chart
                                chart1.ChartAreas[0].AxisY.Maximum = max;
                                chart1.ChartAreas[0].AxisY.Minimum = 0;
                                chart1.ChartAreas[0].AxisX.Maximum = 2;
                                chart1.ChartAreas[0].AxisX.Minimum = 0;
                                if (thang < 12)
                                {
                                    dataTable = DataProvider.Instance.ExecuteQuery("thongketung_khu_thang @a , @b , @c", new object[] { "" + thang + "/01/" + year + "", "" + (thang + 1) + "/01/" + year + "", comboBoxKhu.Text });
                                    foreach (DataRow item in dataTable.Rows)
                                    {
                                        chart1.Series["Biểu đồ"].Points.AddXY("Tiền Vé", item["TienVe"]);
                                        try
                                        {
                                            tongKhu = Convert.ToDouble(item["TienVe"].ToString());
                                        }
                                        catch
                                        {
                                            tongKhu = 0;
                                        }

                                    }
                                    
                                }
                                else
                                {
                                    dataTable = DataProvider.Instance.ExecuteQuery("thongketung_khu_thang @a , @b , @c", new object[] { "" + thang + "/01/" + year + "", "" + (thang + 1) + "/01/" + year + "", comboBoxKhu.Text });
                                    foreach (DataRow item in dataTable.Rows)
                                    {
                                        chart1.Series["Biểu đồ"].Points.AddXY("Tiền Vé", item["TienVe"]);
                                        try
                                        {
                                            tongKhu = Convert.ToDouble(item["TienVe"]);
                                        }
                                        catch
                                        {
                                            tongKhu = 0;
                                        }

                                    }     
                                }
                                btTong.Text = btTong.Text + " " + tongKhu.ToString();

                                break;
                            }
                    }
                }
                else
                {
                    //theo năm 12 tháng được liệt kê
                    if(datepkKT.Value.Year>=2021 && datepkKT.Value.Year<DateTime.Now.Year+1)
                    {
                        if (comboBoxKhu.Text == "Tất cả")
                        {
                            //thống kê taats ca
                            double max = 0;
                            double tong = 0;
                            DataTable data = new DataTable();
                            try
                            {
                                data = DataProvider.Instance.ExecuteQuery("dbo.thongketungthang @a", new object[] { datepkKT.Value.Year });
                                DataRow dataRow = data.Rows[0];
                                for (int i = 1; i <= 12; i++)
                                {
                                    string t = "T" + i;
                                    if (dataRow[t].ToString() != "")
                                    {
                                        if (Convert.ToDouble(dataRow[t]) >= max)
                                        {
                                            max = Convert.ToDouble(dataRow[t]);
                                        }
                                        tong = tong + Convert.ToDouble(dataRow[t]);
                                    }
                                }

                            }
                            catch
                            {
                                max = 1000;
                            }
                            for (int i = 1000; i <= 100000; i = i + 1000)
                            {
                                if (max > 0 && max <= i)
                                {
                                    max = i;
                                    break;
                                }

                            }
                            //set max cho chart
                            chart1.ChartAreas[0].AxisY.Maximum = max;
                            chart1.ChartAreas[0].AxisY.Minimum = 0;
                            chart1.ChartAreas[0].AxisX.Maximum = 12;
                            chart1.ChartAreas[0].AxisX.Minimum = 0;
                            data = DataProvider.Instance.ExecuteQuery("dbo.thongketungthang @a", new object[] { datepkKT.Value.Year });
                            if (data.Rows.Count > 0)
                            {
                                DataRow dataRow = data.Rows[0];
                                chart1.Series["Biểu đồ"].Points.AddXY("T1", dataRow["T1"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T2", dataRow["T2"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T3", dataRow["T3"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T4", dataRow["T4"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T5", dataRow["T5"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T6", dataRow["T6"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T7", dataRow["T7"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T8", dataRow["T8"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T9", dataRow["T9"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T10", dataRow["T10"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T11", dataRow["T11"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T12", dataRow["T12"]);
                            }
                            btTong.Text = btTong.Text + " " + tong.ToString();
                        }
                        else
                        {
                            //thông kê từng khu
                            double max = 0;
                            double tong = 0;
                            DataTable data = new DataTable();
                            try
                            {
                                data = DataProvider.Instance.ExecuteQuery("dbo.thongKeKhu_Nam @a , @b", new object[] { datepkKT.Value.Year, comboBoxKhu.Text });
                                DataRow dataRow = data.Rows[0];
                                for (int i = 1; i <= 12; i++)
                                {
                                    string t = "T" + i;
                                    if (dataRow[t].ToString() != "")
                                    {
                                        if (Convert.ToDouble(dataRow[t]) >= max)
                                        {
                                            max = Convert.ToDouble(dataRow[t]);
                                        }
                                        tong = tong + Convert.ToDouble(dataRow[t]);
                                    }
                                }

                            }
                            catch
                            {
                                max = 1000;
                            }
                            for (int i = 1000; i <= 100000; i = i + 1000)
                            {
                                if (max > 0 && max <= i)
                                {
                                    max = i;
                                    break;
                                }

                            }
                            //set max cho chart
                            chart1.ChartAreas[0].AxisY.Maximum = max;
                            chart1.ChartAreas[0].AxisY.Minimum = 0;
                            chart1.ChartAreas[0].AxisX.Maximum = 12;
                            chart1.ChartAreas[0].AxisX.Minimum = 0;
                            data = DataProvider.Instance.ExecuteQuery("dbo.thongKeKhu_Nam @a , @b", new object[] { datepkKT.Value.Year, comboBoxKhu.Text });
                            if (data.Rows.Count > 0)
                            {
                                DataRow dataRow = data.Rows[0];
                                chart1.Series["Biểu đồ"].Points.AddXY("T1", dataRow["T1"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T2", dataRow["T2"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T3", dataRow["T3"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T4", dataRow["T4"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T5", dataRow["T5"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T6", dataRow["T6"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T7", dataRow["T7"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T8", dataRow["T8"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T9", dataRow["T9"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T10", dataRow["T10"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T11", dataRow["T11"]);
                                chart1.Series["Biểu đồ"].Points.AddXY("T12", dataRow["T12"]);
                            }
                            //chart1.Serializer.Save();
                            btTong.Text = btTong.Text + " " + tong.ToString();
                        }
                    }else
                    {
                        MessageBox.Show("Năm chưa có dữ liệu");
                    }    
                    
                    
                    
                }
            } else
            {
                MessageBox.Show("Hãy chọn mã khu");
            }    
               
        }
    }
}
