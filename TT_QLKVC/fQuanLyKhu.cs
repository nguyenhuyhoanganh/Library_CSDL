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

    public partial class fQuanLyKhu : Form
    {
        static String connString = @"Data Source=DESKTOP-Q6S8P58\SQLEXPRESS;Initial Catalog=KHUVUICHOIGIAITRI;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        bool them;
        bool sua;
        bool xoa;
        public fQuanLyKhu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                Sqlxem();
                groupBox3.Visible = false;
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Sqlxem()
        {
            string SqlSELECT = "SELECT MAKHU as N'Mã khu',TENKHU as N'Tên khu',GIAVENL as N'Giá vé người lớn',GIAVETE as N'Giá vé trẻ em' FROM KHUVUICHOI";
            SqlCommand cmd = new SqlCommand(SqlSELECT, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                them = true;
                sua = false;
                xoa = false;
                groupBox3.Visible = true;
                groupBox3.Text = "Thêm";
                textBox8.Enabled = true;
                textBox2.Enabled = true;
                textBox9.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox8.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox9.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }
        private void XoaTextbox()
        {
            textBox1.Clear();
            textBox8.Clear();
            textBox2.Clear();
            textBox9.Clear();
            textBox1.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sua = true;
                them = false;
                xoa = false;
                groupBox3.Visible = true;
                textBox8.Enabled = true;
                textBox2.Enabled = true;
                textBox9.Enabled = true;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                xoa = true;
                sua = false;
                them = false;
                groupBox3.Visible = true;
                groupBox3.Text = "Xóa";
                textBox8.Enabled = false;
                textBox2.Enabled = false;
                textBox9.Enabled = false;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                if (textBox5.Text.Length>0)
            {
                    SqlCommand cmd = new SqlCommand("execute timkiemmaKVC N'" + textBox5.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@search", textBox5.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
            }
            else if(textBox6.Text.Length>0)
            {
                    SqlCommand cmd = new SqlCommand("execute timkiemtenKVC N'" + textBox6.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@search", textBox6.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
            }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = "";
            groupBox3.Visible = false;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox5.Text = "";
            groupBox3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void fQuanLyKhu_Load(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                if (them == true)
                {
                    SqlCommand cmd = new SqlCommand("themKHUVUICHOI", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MAKHU", textBox1.Text);
                    cmd.Parameters.AddWithValue("@TENKHU", textBox8.Text);
                    cmd.Parameters.AddWithValue("@GIAVENL", int.Parse(textBox2.Text));
                    cmd.Parameters.AddWithValue("@GIAVETE", int.Parse(textBox9.Text));
                    cmd.ExecuteNonQuery();
                    button1_Click(sender, e);


                }
                else if (sua == true)
                {
                    SqlCommand cmd = new SqlCommand("CapNhatKhuVuiChoi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MAKHU", textBox1.Text);
                    cmd.Parameters.AddWithValue("@TENKHU", textBox8.Text);
                    cmd.Parameters.AddWithValue("@GIAVENL", int.Parse(textBox2.Text));
                    cmd.Parameters.AddWithValue("@GIAVETE", int.Parse(textBox9.Text));
                    cmd.ExecuteNonQuery();
                    button1_Click(sender, e);
                    XoaTextbox();

                }
                else if (xoa == true)
                {
                    SqlCommand cmd = new SqlCommand("Delete from KHUVUICHOI where MAKHU='" + textBox1.Text + "'", conn);
                    cmd.ExecuteNonQuery();;
                    button1_Click(sender, e);
                    XoaTextbox();

                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
