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

           /* chart1.Series.Clear();
            chart1.Titles["title1"].Text = "Doanh thu hàng tháng" + " năm " + datepkKT.Value.Year.ToString() + "";
            chart1.Series.Add("Cột");
            chart1.Series["Cột"].Points.AddXY("1",10000 );*/
            if(radioButton1.Checked)
            {
                //theo tháng: 
                /*1 thống kê từng khu
                    2. thống kê tổng*/

            }    else
            {
                //theo năm

            }    
        }
    }
}
