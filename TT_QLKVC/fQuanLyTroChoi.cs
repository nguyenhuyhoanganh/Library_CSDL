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
    public partial class fQuanLyTroChoi : Form
    {
        public int bt2_click = 0;
        public fQuanLyTroChoi()
        {
            InitializeComponent();
            load();
            loadCBbox();
        }
        #region Load
        void load()
        {
            groupBox3.Visible = false;
            dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery("Select * from trochoi");
        }
        void loadCBbox()
        {
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery("Select makhu from khuvuichoi");
            foreach (DataRow item in data.Rows)
            {
                comboBox1.Items.Add(item["makhu"]);
                comboBox2.Items.Add(item["makhu"]);
            }
        }
        void unlock_bt()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery("Select * from trochoi");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            button4.Enabled = false;
            button2.Enabled = false;
            bt2_click = 0;
            textBox1.Clear();
            textBox2.Clear();
           

        }

        private void button7_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            unlock_bt();
            load();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //sửa dữ liệu
            if(bt2_click==1)
            {
                
                try
                {
                    if(textBox2.Text != "" && comboBox1.Text != "")
                    {
                        int rez = 0;
                        string que = "exec dbo.suaTroChoi @a , @b , @c";
                        //MessageBox.Show(que);
                        rez = DataProvider.Instance.ExecuteNonQuery(que, new object[] { textBox1.Text, textBox2.Text, comboBox1.Text });
                        if (rez > 0)
                        {
                            MessageBox.Show("Sửa thành công");
                            bt2_click = 0;
                            unlock_bt();
                            load();
                        }
                        else MessageBox.Show("Sửa không thành công");
                    }
                    else { MessageBox.Show("Thông tin không thể trống"); } 
                }catch(Exception ex)
                { MessageBox.Show(ex.ToString()); }
            }
            else
            {
                int rez = 0;
                try
                {
                    if (textBox2.Text != "" && comboBox1.Text != "")
                    {
                        string que = "exec dbo.themTC @a , @b";
                        rez = DataProvider.Instance.ExecuteNonQuery(que, new object[] { textBox2.Text, comboBox1.Text });
                    }
                    else
                    {
                        MessageBox.Show("Hãy nhập đầy đủ thông tin");
                    }
                    if (rez > 0)
                    {
                        MessageBox.Show("Thêm thành công");
                        load();
                        unlock_bt();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
           
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            MessageBox.Show("Chọn trường dữ liệu muốn sửa");
            groupBox3.Visible = true;
            bt2_click = 1;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(bt2_click==1)
            {
                textBox1.DataBindings.Clear();
                textBox1.DataBindings.Add("text", dataGridView1.DataSource, "matc");
                comboBox1.DataBindings.Clear();
                comboBox1.DataBindings.Add("text", dataGridView1.DataSource, "makhu");
                textBox2.DataBindings.Clear();
                textBox2.DataBindings.Add("text", dataGridView1.DataSource, "tentc");
                //bt2_click = 0;
            }    
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chọn trường dữ liệu muốn xóa");
            bt2_click = 1;
            groupBox3.Visible = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button6.Visible = false;
            button7.Visible = false;
            buttonXoa.Visible = true;
            buttonHuyXoa.Visible = true;
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string que = "exec xoaTroChoi @a";
                int rez = DataProvider.Instance.ExecuteNonQuery(que, new object[] { textBox1.Text });
                if (rez > 0)
                {
                    MessageBox.Show("Xóa thành công");
                    load();
                    unlock_bt();
                    buttonXoa.Visible = false;
                    buttonHuyXoa.Visible = false;
                    button6.Visible = true;
                    button7.Visible = true;
                    groupBox3.Visible = false;
                }
                else MessageBox.Show("Xóa không thành công");
            }catch(Exception ex)
            { }
        }

        private void buttonHuyXoa_Click(object sender, EventArgs e)
        {
            unlock_bt();
            buttonXoa.Visible = false;
            buttonHuyXoa.Visible = false;
            button6.Visible = true;
            button7.Visible = true;
            groupBox3.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //DataTable rez=DataProvider.Instance.ExecuteQuery("Select Count(*) as sl from TROCHOI where TENTC like N'%'+@tenTC+'%' or MAKHU like '%'+@khuTC+'%'");
            dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery("exec timKiemTC @a , @b", new object[]{ textBox6.Text,comboBox2.Text});
            //MessageBox.Show("Có " + kq + " Kết quả");
        }
    }
}
