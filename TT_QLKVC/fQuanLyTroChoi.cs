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
        public fQuanLyTroChoi()
        {
            InitializeComponent();
            load();
        }
        #region Load
        void load()
        {
            groupBox3.Visible = false;
            textBox1.Visible = false;
            DataTable data = new DataTable();
            data = DataProvider.Instance.ExecuteQuery("Select makhu from khuvuichoi");
            foreach (DataRow item in data.Rows)
            {
                comboBox1.Items.Add(item["makhu"]);
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery("Select * from trochoi");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;


        }

        private void button7_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox2.Text!="" && comboBox1.Text!="")
                {
                    string que = "themTC";
                    int rez = DataProvider.Instance.ExecuteNonQuery(que, new object[] { "" + textBox2.Text + "", "" + comboBox1.Text + "" });
                }    
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
