using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TT_QLKVC
{
    public partial class Form_doanhthunv : Form
    {
        public Form_doanhthunv()
        {
            InitializeComponent();
        }
        string constr = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=KHUVUICHOIGIAITRI;Integrated Security=True";
        string[] type = {"Tổng doanh thu","Doanh thu bán vé", "Doanh thu dịch vụ" };

        private void Form_doanhthunv_Load(object sender, EventArgs e)
        {
            comboBox1.Text = type[0];
            for(int i=0; i<type.Length; i++){
                comboBox1.Items.Add(type[i]);
            }
            dateTimePicker_t_s.MaxDate = DateTime.Today;
            dateTimePicker_t_e.MaxDate = DateTime.Today;
            //this.dateTimePicker_t_e.Da dateTimePicker_t_e.
        }
        void change()
        {
            if(radioButton1.Checked==true)
            {
                dateTimePicker_t_e.Enabled = false;
                dateTimePicker_t_e.Visible = false;
                label1.Text = "Tháng:";
                label2.Visible = false;
                label1.Location = new Point(365, 14);
            }
            else
            {
                label2.Visible = true;
                label1.Text = "Tháng bắt đầu:";
                label1.Location = new Point(326,14);
                dateTimePicker_t_e.Enabled = true;
                dateTimePicker_t_e.Visible = true;
            }    
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            change();
        }

        private void dateTimePicker_t_s_ValueChanged(object sender, EventArgs e)
        {
            //if(dateTimePicker_t_s.Value> dateTimePicker_t_e.Value)
            //{
               // MessageBox.Show("Thời gian bắt đầu thống kê không được lớn hơn Thời gian kết thúc thống kê");
                //dateTimePicker_t_s.Value = dateTimePicker_t_e.Value;
                dateTimePicker_t_e.MinDate = dateTimePicker_t_s.Value;
                //label3.Text = dateTimePicker_t_s.Value.ToString("MM") + "-30-" + dateTimePicker_t_s.Value.ToString("yyyy");
            //}    
        }

        private void dateTimePicker_t_e_ValueChanged(object sender, EventArgs e)
        {
            //if (dateTimePicker_t_s.Value > dateTimePicker_t_e.Value)
            //{
                //MessageBox.Show("Thời gian bắt đầu thống kê không được lớn hơn Thời gian kết thúc thống kê");
                dateTimePicker_t_s.Value = dateTimePicker_t_e.Value;
            //label3.Text = cvdt(dateTimePicker_t_s.Value.ToString("yyyy"));
            //}
        }

        //string cvdt(string d)
        //{
        //    //DateTime dateTime = Convert.ToDateTime(d);
        //   // d = dateTime.ToString("MM-dd-yyyy");
        //    return d;
        //}
        //string start = ""; string end = "";
        private void button1_Click(object sender, EventArgs e)
        {
            string start=""; string end="";
            if(radioButton1.Checked==true)
            {
                
                if (Convert.ToInt32(dateTimePicker_t_s.Value.ToString("MM")) == 1 || Convert.ToInt32(dateTimePicker_t_s.Value.ToString("MM")) == 3 || Convert.ToInt32(dateTimePicker_t_s.Value.ToString("MM")) == 5 || Convert.ToInt32(dateTimePicker_t_s.Value.ToString("MM")) == 7 || Convert.ToInt32(dateTimePicker_t_s.Value.ToString("MM")) == 8 || Convert.ToInt32(dateTimePicker_t_s.Value.ToString("MM")) == 10 || Convert.ToInt32(dateTimePicker_t_s.Value.ToString("MM")) == 12)
                {
                    end = dateTimePicker_t_s.Value.ToString("MM") + "-31-" + dateTimePicker_t_s.Value.ToString("yyyy");
                }
                else if (Convert.ToInt32(dateTimePicker_t_s.Value.ToString("MM")) == 2)
                {
                    end = dateTimePicker_t_s.Value.ToString("MM")+"-28-" + dateTimePicker_t_s.Value.ToString("yyyy");
                }
                else
                {
                    end = dateTimePicker_t_s.Value.ToString("MM") + "-30-" + dateTimePicker_t_s.Value.ToString("yyyy");
                }

                

            }
            else
            {
                if (Convert.ToInt32(dateTimePicker_t_e.Value.ToString("MM")) == 1 || Convert.ToInt32(dateTimePicker_t_e.Value.ToString("MM")) == 3 || Convert.ToInt32(dateTimePicker_t_e.Value.ToString("MM")) == 5 || Convert.ToInt32(dateTimePicker_t_e.Value.ToString("MM")) == 7 || Convert.ToInt32(dateTimePicker_t_e.Value.ToString("MM")) == 8 || Convert.ToInt32(dateTimePicker_t_e.Value.ToString("MM")) == 10 || Convert.ToInt32(dateTimePicker_t_e.Value.ToString("MM")) == 12)
                {
                    end = dateTimePicker_t_e.Value.ToString("MM") + "-31-" + dateTimePicker_t_e.Value.ToString("yyyy");
                }
                else if (Convert.ToInt32(dateTimePicker_t_e.Value.ToString("MM")) == 2)
                {
                    end = dateTimePicker_t_e.Value.ToString("MM") + "-28-" + dateTimePicker_t_e.Value.ToString("yyyy");
                }
                else
                {
                    end = dateTimePicker_t_e.Value.ToString("MM") + "-30-" + dateTimePicker_t_e.Value.ToString("yyyy");
                }
                //dateTimePicker_t_s.Value.ToString("MM-yyyy");
            }
            start = dateTimePicker_t_s.Value.ToString("MM") + "-01-" + dateTimePicker_t_s.Value.ToString("yyyy");
            //label3.Text = "execute tongdoanhthunv '" + start + "','" + end + "'";
            //label4.Text = end;
            if (comboBox1.Text==type[0])
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlDataAdapter sqlData = new SqlDataAdapter("EXECUTE tongdoanhthunv '" + start + "','" + end + "'", sqlcon);
                    DataTable dataTable = new DataTable();
                    sqlData.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    MessageBox.Show("EXECUTE tongdoanhthunv '" + start + "','" + end + "");
                }
            }
            else if(comboBox1.Text==type[1])
            {

            }   
            else if(comboBox1.Text==type[2])
            {

            }

            private DataTable loadchart()
            {

            }


        }
    }
}
