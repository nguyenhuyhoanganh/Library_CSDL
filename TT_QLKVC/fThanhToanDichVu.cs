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
    public partial class fThanhToanDichVu : Form
    {
        public DataTable dt;
        public fThanhToanDichVu()
        {
            InitializeComponent();
            loadCBB();
        }
        #region Load data
        void loadCBB()
        {
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery("Select tenldv from loaidv");
            foreach (DataRow item in data.Rows)
            {
                comboBox1.Items.Add(item["tenldv"]);

            }
            comboBox1.SelectedIndex = 0;
            
        }
        void loadListView()
        {
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery("Select dbo.at_ma_bl() as SoBL");
            string soBL = "";
            foreach (DataRow item in data.Rows)
            {
                soBL = item["SoBL"].ToString();
            }
            string[] arr = new string[2];
            arr[0] = "Ngày: ";
            arr[1] = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
            ListViewItem it = new ListViewItem(arr);
            listView1.Items.Add(it);
            arr[0] = "Mã biên lai: ";
            if (soBL != "")
            {
                arr[1] = soBL;
            }
            else arr[1] = "Error";
            it = new ListViewItem(arr);
            listView1.Items.Add(it);
        }

        bool Available()
        {
            int rez = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                if(item.SubItems[0].Text == comboBox2.Text)
                {
                    rez += 1;
                }    
            }
            if (rez == 0) return false;
            else return true;
        }

        double total_cost(ListViewItem item)
        {
            double total = 0;
            DataTable data = DataProvider.Instance.ExecuteQuery("Select dongia from dichvu where dichvu.tendv = N'" + item.SubItems[0].Text + "'");
            int SL = Convert.ToInt32(item.SubItems[1].Text);
            double price = 0;
            foreach (DataRow it in data.Rows)
            {
                price = Convert.ToDouble(it["dongia"].ToString());
            }
            total = price * SL;

            return total;
        }
        #endregion

        #region Event
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            DataTable data = DataProvider.Instance.ExecuteQuery("select tendv, dongia from dichvu dv, LOAIDV ldv where ldv.MALDV=dv.MALDV and ldv.TENLDV=N'"+comboBox1.Text+"'  ");
            foreach (DataRow item in data.Rows)
            {
                comboBox2.Items.Add(item["tendv"]);
            }
            comboBox2.SelectedIndex = 0;
            dataGridViewTenDV.DataSource = data;
        }


        private void dataGridViewTenDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox2.SelectedIndex = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value!=0)
            {
                if (listView1.Items.Count > 0)
                {
                    //có sẵn 
                    if (Available())
                    {
                        foreach (ListViewItem item in listView1.Items)
                        {
                            ListViewItem item_temp = new ListViewItem();
                            if (item.SubItems[0].Text == comboBox2.Text)
                            {
                                int val_temp = Convert.ToInt32(item.SubItems[1].Text);
                                int Val_new = val_temp + Convert.ToInt32(numericUpDown1.Value);
                                item.SubItems[1].Text = Val_new.ToString();
                            }
                        }
                        // update database
                    }
                    else
                    {
                        //add item
                        if(numericUpDown1.Value>0)
                        {
                            string[] arr = new string[2];
                            arr[0] = comboBox2.Text;
                            arr[1] = numericUpDown1.Value.ToString();
                            ListViewItem item = new ListViewItem(arr);
                            listView1.Items.Add(item);
                            //+ lưu vào data base
                        }
                        else
                        {
                            MessageBox.Show("Không được để số bé hơn 1");
                        }


                    }
                }
                else
                {
                    //+layout
                    loadListView();
                    if (Available())
                    {
                        //update value
                        foreach (ListViewItem item in listView1.Items)
                        {
                            ListViewItem item_temp = new ListViewItem();
                            if (item.SubItems[0].Text == comboBox2.Text)
                            {
                                int val_temp = Convert.ToInt32(item.SubItems[1]);
                                int Val_new = val_temp + Convert.ToInt32(numericUpDown1.Value);
                                item.SubItems[1].Text = val_temp.ToString();
                            }
                        }
                        // update database


                    }
                    else
                    {
                        //add item
                        if(numericUpDown1.Value>0)
                        {
                            string[] arr = new string[2];
                            arr[0] = comboBox2.Text;
                            arr[1] = numericUpDown1.Value.ToString();
                            ListViewItem item = new ListViewItem(arr);
                            listView1.Items.Add(item);
                            //+ lưu vào data base
                        }
                        else
                        {
                            MessageBox.Show("Không được để số bé hơn 1");
                        }    
                       
                    }
                }
            }    
            else
            {
                MessageBox.Show("Hãy chọn số lượng");
            }    
            
            double total = 0;
            for (int i = 2; i < listView1.Items.Count; i++)
            {
                ListViewItem item = listView1.Items[i];
                if (Convert.ToInt32(item.SubItems[1].Text) <= 0)
                {
                    item.Remove();
                    listView1.Update();
                }
                
                total += total_cost(item);
            }
            textBox2.Text = "";
            textBox2.Text = total.ToString() + " 000vnd";
        }

        #endregion


    }
}
