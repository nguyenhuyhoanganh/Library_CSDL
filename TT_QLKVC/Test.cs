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
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString.str))
            {
                sqlcon.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("EXECUTE tongdoanhthunv '" + "1/1/2019" + "','" + "1/1/2022" + "'", sqlcon);
                DataTable dataTable = new DataTable();
                sqlData.Fill(dataTable);
                chart1.DataSource = dataTable;
                //chart1.
                chart1.Series["Series1"].XValueMember = dataTable.Columns[0].ColumnName;
                chart1.Series["Series1"].YValueMembers = dataTable.Columns[1].ColumnName;
                //chart1.Series["Series3"].YValueMembers = dataTable.Columns[2].ColumnName;
                //chart1.Series["Series1"].YValueMembers = dataTable.Columns[3].ColumnName;
            }

            
        }
    }

    
}
