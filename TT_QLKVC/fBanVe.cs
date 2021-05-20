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
using System.IO;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System.Diagnostics;

namespace TT_QLKVC
{
    public partial class fBanVe : Form
    {
        public DataTable data;
        decimal nl, te;
        public fBanVe()
        {
            InitializeComponent();
            txbNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        public DataTable loadDL(string a)
        {
            string que = "select * from KHUVUICHOI where MAKHU = N'" + a + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            return tb;
        }
        private void fBanVe_Load(object sender, EventArgs e)
        {
            string a = data.Rows[0]["MAKHU"].ToString();
            if (a != "")
            {
                cbKhu.Visible = false;

                DataTable tb = loadDL(a);

                label3.Text = "Khu: " + tb.Rows[0]["TENKHU"].ToString();
                label4.Text = "Giá Vé Người Lớn: " + tb.Rows[0]["GIAVENL"].ToString();
                label5.Text = "Giá Vé Trẻ Em: " + tb.Rows[0]["GIAVETE"].ToString();
                nl = Convert.ToDecimal(tb.Rows[0]["GIAVENL"]);
                te = Convert.ToDecimal(tb.Rows[0]["GIAVETE"]);
            }
            else
            {
                cbKhu.Visible = true;
                string que = "select * from KHUVUICHOI";
                DataTable tb = DataProvider.Instance.ExecuteQuery(que);
                /*cbKhu.DataSource = tb;
                cbKhu.DisplayMember = "TENKHU";*/
                foreach (DataRow item in tb.Rows)
                {
                    cbKhu.Items.Add(item["TenKHU"]);
                }
                cbKhu.SelectedIndex = 0;
                label4.Text = "Giá Vé Người Lớn: " + tb.Rows[0]["GIAVENL"].ToString();
                label5.Text = "Giá Vé Trẻ Em: " + tb.Rows[0]["GIAVETE"].ToString();
                nl = Convert.ToDecimal(tb.Rows[0]["GIAVENL"]);
                te = Convert.ToDecimal(tb.Rows[0]["GIAVETE"]);
            }

            txbMaVe.Text = loadMV();
        }

        private void cbKhu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string que = "select * from KHUVUICHOI where TENKHU = N'" + cbKhu.Text + "'";
            DataTable tb = DataProvider.Instance.ExecuteQuery(que);
            label4.Text = "Giá Vé Người Lớn: " + tb.Rows[0]["GIAVENL"].ToString();
            label5.Text = "Giá Vé Trẻ Em: " + tb.Rows[0]["GIAVETE"].ToString();
            nl = Convert.ToDecimal(tb.Rows[0]["GIAVENL"]);
            te = Convert.ToDecimal(tb.Rows[0]["GIAVETE"]);

            numericUpDown4_ValueChanged(sender, e);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            decimal tong = (decimal)numericUpDown4.Value * nl + (decimal)numericUpDown3.Value * te;
            txbTong.Text = tong.ToString();
        }
        string maKHU, gVNL, gVTE, maNV;
        private void btnXuatVe_Click(object sender, EventArgs e)
        {
            if(numericUpDown4.Value == 0 && numericUpDown3.Value == 0)
            {
                MessageBox.Show("Chưa Thêm Lượng Khách Vào Vé");
                return;
            }
             maNV = data.Rows[0]["MANV"].ToString();
             maKHU = data.Rows[0]["MAKHU"].ToString();
            DataTable tb = new DataTable();
            if (cbKhu.Visible == true)
            {
                string que = "select * from KHUVUICHOI where TENKHU = N'" + cbKhu.Text + "'";
                tb = DataProvider.Instance.ExecuteQuery(que);
            }
            else
            {
                string que = "select * from KHUVUICHOI where MAKHU = N'" + data.Rows[0]["MAKHU"].ToString() + "'";
                tb = DataProvider.Instance.ExecuteQuery(que);
            }
           
             gVNL = tb.Rows[0]["GIAVENL"].ToString();
             gVTE = tb.Rows[0]["GIAVETE"].ToString();

            if (maKHU == "")
            {
                maKHU = tb.Rows[0]["MAKHU"].ToString();
            }

            string query = @"Insert into VE  (MAVE, SOLUONGNL, SOLUONGTE, MAKHU, MANV, TONGTIEN, NGAYBAN , GIAVENL, GIAVETE) values( N'" 
            + txbMaVe.Text + "', '" + numericUpDown4.Value.ToString() + "', '"
            + numericUpDown3.Value.ToString() + "', N'" + maKHU + "', N'" + maNV + "', "
            + txbTong.Text + ", '" + DateTime.Now.ToString("MM/dd/yyyy") + "', " + gVNL + ", " + gVTE + ")";
            int i = DataProvider.Instance.ExecuteNonQuery(query);
            if (i != 0)
            {
                MessageBox.Show("Thêm vé Thành Công");


                string path = @"C:\Users\minht\source\repos\nguyenhuyhoanganh\NhomLoonf1\TT_QLKVC\Word\ticket - Copy.docx";
                if (Directory.Exists(path))
                {
                    System.IO.File.Delete(path);
                    System.IO.File.Copy(@"C:\Users\minht\source\repos\nguyenhuyhoanganh\NhomLoonf1\TT_QLKVC\Word\ticket.docx", path);
                }
                CreateWordDocument(@"C:\Users\minht\source\repos\nguyenhuyhoanganh\NhomLoonf1\TT_QLKVC\Word\ticket.docx", path);

                //MessageBox.Show(gVNL + " " + gVTE + " " + "1 " + cbKhu.Text + " 2" + namekvc() + " " + " ");
            }
            else
            {
                MessageBox.Show("Thêm Vé Không Thành Công!!!");
            }
            fBanVe_Load(sender, e);
        }

        private string loadMV()
        {
            DataTable b = DataProvider.Instance.ExecuteQuery("select dbo.at_ma_ve() as N'Mã Vé'");
            return b.Rows[0]["Mã Vé"].ToString();
        }
        private void FindAndReplace(Word.Application wordApp, object ToFindText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref ToFindText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundLike,
                ref nmatchAllforms, ref forward,
                ref wrap, ref format, ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }
        string name()
        {
            string name= "";
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString.str))
            {
                sqlcon.Open();
                SqlCommand command = new SqlCommand("select tennv from nhanvien where manv='" + fDangNhap.manv+ "'", sqlcon);
                name = command.ExecuteScalar().ToString();
            }
            return name;

        }
        //string sumnl()
        //{
        //    decimal sum;
        //    sum = Int16.Parse(gVNL) * Int16.Parse(numericUpDown4.Value.ToString());
        //    return sum.ToString();
        //}

        //string sumte()
        //{
        //    decimal sum;
        //    sum = Int16.Parse(gVTE) * Int16.Parse(numericUpDown3.Value.ToString());
        //    return sum.ToString();
        //}
        string namekvc()
        {
            string name="";
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString.str))
            {
                sqlcon.Open();
                SqlCommand command = new SqlCommand("select tenkhu from khuvuichoi where makhu= (select makhu from nhanvien where manv='" + fDangNhap.manv + "')", sqlcon);
                name = command.ExecuteScalar().ToString();
            }
            return name;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text File|*.docx";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = @"C:\Users\minht\source\repos\nguyenhuyhoanganh\NhomLoonf1\TT_QLKVC\Word\ticket - Copy.docx";
                if (Directory.Exists(path))
                    System.IO.File.Delete(path);
                System.IO.File.Copy(@"C:\Users\minht\source\repos\nguyenhuyhoanganh\NhomLoonf1\TT_QLKVC\Word\ticket.docx", path);
                CreateWordDocument(@"C:\Users\minht\source\repos\nguyenhuyhoanganh\NhomLoonf1\TT_QLKVC\Word\ticket.docx",path);
            }

            MessageBox.Show(gVNL + " " + gVTE + " " + " 0" + cbKhu.Text + " 1"+namekvc() + " " + " ");
        }

        //Creeate the Doc Method
        private void CreateWordDocument(object filename, object SaveAs)
        {
            Word.Application wordApp = new Word.Application();
            object missing = Missing.Value;
            Word.Document myWordDoc = null;

            if (File.Exists((string)filename))
            {
                object readOnly = false;
                object isVisible = false;
                wordApp.Visible = false;

                myWordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing, ref missing);
                myWordDoc.Activate();

                //find and replace
                this.FindAndReplace(wordApp, "<sonl>", numericUpDown4.Value.ToString());
                this.FindAndReplace(wordApp, "<sote>", numericUpDown3.Value.ToString());
                this.FindAndReplace(wordApp, "<dg>", gVNL);
                this.FindAndReplace(wordApp, "<dgte>", gVTE);
                //this.FindAndReplace(wordApp, "<sumte>", sumte());
                //this.FindAndReplace(wordApp, "<sumnl>", sumnl());
                this.FindAndReplace(wordApp, "<name>", name());
                this.FindAndReplace(wordApp, "<namekvc>", namekvc());
                this.FindAndReplace(wordApp, "<id>", fDangNhap.manv);
                this.FindAndReplace(wordApp, "<num>", txbMaVe.Text);
                this.FindAndReplace(wordApp, "<sum>", txbTong.Text);
                this.FindAndReplace(wordApp, "<date>", dateTimePicker2.Value.ToString());
            }
            else
            {
                MessageBox.Show("Không tìm thấy file này!");
            }

            //Save as
            myWordDoc.SaveAs2(ref SaveAs, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing);

            myWordDoc.Close();
            wordApp.Quit();
            string path = @"C:\Users\minht\source\repos\nguyenhuyhoanganh\NhomLoonf1\TT_QLKVC\Word\ticket - Copy.docx";
            Process.Start(path);
            //System.IO.File.Open(path, FileMode.Open);
            MessageBox.Show("File Created!");
        }

    }
}
