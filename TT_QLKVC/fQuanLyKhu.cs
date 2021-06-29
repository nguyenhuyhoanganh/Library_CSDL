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
using Guna.UI2.WinForms;
using System.Text.RegularExpressions;
namespace TT_QLKVC
{

    public partial class fQuanLyKhu : Form
    {
        static String connString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=KHUVUICHOIGIAITRI;Integrated Security=True";
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
            string SqlSELECT = "SELECT MAKHU as N'Mã khu',TENKHU as N'Tên khu',CAST(GIAVENL as int)as N'Giá vé người lớn',Cast(GIAVETE as int) as N'Giá vé trẻ em' FROM KHUVUICHOI";
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
                conn.Open();
                them = true;
                sua = false;
                xoa = false;
                button2.Enabled = false;
                button4.Enabled = false;
                groupBox3.Visible = true;
                groupBox3.Text = "Thêm";
                textBox8.Enabled = true;
                textBox2.Enabled = true;
                textBox9.Enabled = true;
                SqlCommand cmd1 = new SqlCommand("execute auto_makhu", conn);
                textBox1.Text = cmd1.ExecuteScalar().ToString();
                textBox1.Enabled = false;
                textBox1.BackColor = Color.White;
                textBox2.Clear();
                textBox8.Clear();
                textBox9.Clear();
                conn.Close();
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
                conn.Open();
                XoaTextbox();
                sua = true;
                them = false;
                xoa = false;
                button3.Enabled = false;
                button4.Enabled = false;
                groupBox3.Visible = true;
                textBox8.Enabled = true;
                textBox2.Enabled = true;
                textBox9.Enabled = true;
                textBox1.Enabled = false;
                textBox1.BackColor = Color.White;
                conn.Close();
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
                conn.Open();
                XoaTextbox();
                xoa = true;
                sua = false;
                them = false;
                button3.Enabled = false;
                button2.Enabled = false;
                groupBox3.Visible = true;
                groupBox3.Text = "Xóa";
                textBox8.Enabled = false;
                textBox2.Enabled = false;
                textBox9.Enabled = false;
                textBox1.ReadOnly = true;
                textBox1.BackColor = Color.White;
                conn.Close();
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
                    SqlCommand cmd = new SqlCommand("execute timkiemmaKVC N'" + textBox5.Text + "', N'" + textBox6.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@makhu", textBox5.Text);
                    cmd.Parameters.AddWithValue("@tenkhu", textBox6.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
            
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
            groupBox3.Visible = false;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conn.Close();
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button1_Click(sender, e);
        }

        private void fQuanLyKhu_Load(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            button1_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                if (them == true)
                {
                    int i;
                    if (textBox1.Text != "" && textBox8.Text != ""&&textBox9.Text!=""&&textBox8.Text!="")
                    {
                        SqlCommand cmd = new SqlCommand("themKHUVUICHOI", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MAKHU", textBox1.Text);
                        if (Regex.IsMatch(textBox8.Text, "\\d") == true)
                        {
                            MessageBox.Show("Tên khu là 1 chuỗi ký tự từ A-Z", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TENKHU", textBox8.Text);
                        }
                        if (Regex.IsMatch(textBox2.Text, "\\d") == false)
                        {
                            MessageBox.Show("Giá vé người lớn là số tự nhiên lớn hơn 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@GIAVENL", int.Parse(textBox2.Text));
                        }
                        if (Regex.IsMatch(textBox9.Text, "\\d") == false)
                        {
                            MessageBox.Show("Giá vé trẻ em là số tự nhiên lớn hơn 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@GIAVETE", int.Parse(textBox9.Text));
                        }
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm thành công");
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button1_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    conn.Close();


                }
                else if (sua == true)
                {
                    SqlCommand cmd = new SqlCommand("CapNhatKhuVuiChoi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MAKHU", textBox1.Text);
                    if (Regex.IsMatch(textBox8.Text, "\\d") == true)
                    {
                        MessageBox.Show("Tên khu là 1 chuỗi ký tự từ A-Z", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@TENKHU", textBox8.Text);
                    }
                    if (Regex.IsMatch(textBox2.Text, "\\d") == false)
                    {
                        MessageBox.Show("Giá vé người lớn là số tự nhiên lớn hơn 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@GIAVENL", int.Parse(textBox2.Text));
                    }
                    if (Regex.IsMatch(textBox9.Text, "\\d") == false)
                    {
                        MessageBox.Show("Giá vé trẻ em là số tự nhiên lớn hơn 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@GIAVETE", int.Parse(textBox9.Text));
                    }
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thông tin thành công");
                    button1_Click(sender, e);
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    XoaTextbox();
                    conn.Close();

                }
                else if (xoa == true)
                {
                    SqlCommand cmd = new SqlCommand("Delete from KHUVUICHOI where MAKHU='" + textBox1.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công");
                    button1_Click(sender, e);
                    XoaTextbox();
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    conn.Close();

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
