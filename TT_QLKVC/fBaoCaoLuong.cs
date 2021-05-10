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
    public partial class fBaoCaoLuong : Form
    {
        public fBaoCaoLuong()
        {
            InitializeComponent();
            loadData();
        }
        #region Load
        public string mave = "";
        public string sobl = "";
        void load()
        {
            textBox1.Text = "";
            if(radioButton1.Checked)
            {
                //tiền vé
                double total = 0;
                DataTable da = DataProvider.Instance.ExecuteQuery("Select ngayban, mave, makhu, tongtien from ve where makhu='" + comboBox1.Text + "' and ngayban between '" + dtpkThangLuong.Value.ToString("MM/dd/yyyy") + "' and '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "'");
                dataGridView1.DataSource = da;
                foreach (DataRow item in da.Rows)
                {
                    total += Convert.ToDouble(item["tongtien"].ToString());
                }
                textBox1.Text = total.ToString()+"000 vnd";
            }   else
            {
                //tiền dịch vụ
                double total = 0;
                DataTable da = new DataTable();
                string que = "Select ngaybl, sobl, tongtien from bienlai where ngaybl between '"+dtpkThangLuong.Value.ToString("MM/dd/yyyy")+"' and '"+dateTimePicker1.Value.ToString("MM/dd/yyyy")+"'";
                da = DataProvider.Instance.ExecuteQuery(que);
                dataGridView1.DataSource = da;
                foreach (DataRow item in da.Rows)
                {
                    total += Convert.ToDouble(item["tongtien"].ToString());
                }
                textBox1.Text = total.ToString() + "000 vnd";
            }    
        }
        string convertTien(double d)
        {
            string tong = d.ToString();
            int i = 0;
            for(i=0;i<tong.Length;i++)
            {
                if((i+1)%3==0)
                {
                    tong.Insert(i, " ");
                }
            }
            tong = tong + " 000 vnd";
            return tong;
        }
        void loadData()
        {
            comboBox1.Items.Clear();
            if(radioButton1.Checked)
            {
                DataTable da = DataProvider.Instance.ExecuteQuery("Select makhu from khuvuichoi");
                foreach (DataRow item in da.Rows)
                {
                    comboBox1.Items.Add(item["makhu"]);
                }
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Items.Add("Tất cả");
                comboBox1.SelectedIndex = 0;
            }
            
        }
        #endregion
        #region Event
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }
 

        private void button1_Click(object sender, EventArgs e)
        {
            if(dtpkThangLuong.Value<=dateTimePicker1.Value)
            {
                load();
            }
            else
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc");
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            listView1.Items.Clear();
            try
            {
                if(radioButton1.Checked)
                {
                    
                    mave = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    DataTable da = new DataTable();
                    da = DataProvider.Instance.ExecuteQuery("Select ngayban, mave, SOLUONGNL, SOLUONGTE, kv.GIAVENL, kv.GIAVETE, TONGTIEN from Ve v, KHUVUICHOI kv where v.MAKHU=kv.MAKHU and mave='"+mave+"'");
                    foreach (DataRow item in da.Rows)
                    {  
                        string[] st = new string[2];
                        st[0] = "Mã vé";
                        st[1] = item["mave"].ToString();
                        ListViewItem lsit = new ListViewItem(st);
                        listView1.Items.Add(lsit);
                        st[0] = "Ngày bán";
                        st[1] = item["ngayban"].ToString();
                        ListViewItem lsit2 = new ListViewItem(st);
                        listView1.Items.Add(lsit2);
                        st[0] = "Số lượng người lớn";
                        st[1] = item["soluongnl"].ToString();
                        ListViewItem lsit3 = new ListViewItem(st);
                        listView1.Items.Add(lsit3);
                        st[0] = "Số lượng trẻ em";
                        st[1] = item["soluongte"].ToString();
                        ListViewItem lsit4 = new ListViewItem(st);
                        listView1.Items.Add(lsit4);
                        st[0] = "Giá ve người lớn";
                        st[1] = item["giavenl"].ToString();
                        ListViewItem lsit5 = new ListViewItem(st);
                        listView1.Items.Add(lsit5);
                        st[0] = "Giá vé trẻ em";
                        st[1] = item["giavete"].ToString();
                        ListViewItem lsit6 = new ListViewItem(st);
                        listView1.Items.Add(lsit6);
                        st[0] = "Tổng tiền";
                        st[1] = item["tongtien"].ToString();
                        ListViewItem lsit7 = new ListViewItem(st);
                        listView1.Items.Add(lsit7);

                    }
                }   else
                {
                    sobl = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    DataTable da = new DataTable();
                    string que = "Select ngaybl, bl.sobl, tendv, ctbl.SOLUONG, dv.dongia " +
                                 "from bienlai bl, CHITIETBL ctbl, DICHVU dv "+
                                 "where bl.SOBL = ctbl.SOBL and ctbl.MADV = dv.MADV and bl.SOBL = '"+sobl+"'";
                    da = DataProvider.Instance.ExecuteQuery(que);
                    int i = 0;
                    foreach (DataRow item in da.Rows)
                    {
                        
                        string[] arr = new string[2];
                        if (i==0)
                        {
                            arr[0] = "Ngày biên lai";
                            arr[1] = item["ngaybl"].ToString();
                            ListViewItem lvi = new ListViewItem(arr);
                            listView1.Items.Add(lvi);
                            arr[0] = "Số biên lai";
                            arr[1] = item["sobl"].ToString();
                            ListViewItem lvi2 = new ListViewItem(arr);
                            listView1.Items.Add(lvi2);
                            arr[0] = "Dịch vụ";
                            arr[1] = item["tendv"].ToString();
                            ListViewItem lvi3 = new ListViewItem(arr);
                            listView1.Items.Add(lvi3);
                            arr[0] = "Số lượng";
                            arr[1] = item["soluong"].ToString();
                            ListViewItem lvi4 = new ListViewItem(arr);
                            listView1.Items.Add(lvi4);
                            arr[0] = "Đơn giá";
                            arr[1] = item["dongia"].ToString();
                            ListViewItem lvi5 = new ListViewItem(arr);
                            listView1.Items.Add(lvi5);
                            i++;
                        }
                        else
                        {
                             arr[0] = "Dịch vụ";
                            arr[1] = item["tendv"].ToString();
                            ListViewItem lvi3 = new ListViewItem(arr);
                            listView1.Items.Add(lvi3);
                            arr[0] = "Số lượng";
                            arr[1] = item["soluong"].ToString();
                            ListViewItem lvi4 = new ListViewItem(arr);
                            listView1.Items.Add(lvi4);
                            arr[0] = "Đơn giá";
                            arr[1] = item["dongia"].ToString();
                            ListViewItem lvi5 = new ListViewItem(arr);
                            listView1.Items.Add(lvi5);
                        }
                        
                        
                    }

                }    
            }
            catch
            {

            }
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
