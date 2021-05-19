using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_QLKVC.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return DataProvider.instance;
            }
            private set => instance = value;
        }

        private DataProvider() { }

        //private string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=KHUVUICHOIGIAITRI;Integrated Security=True";
        private string connectionString = ConnectionString.str;

        #region ExcuteQuery
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] temp = query.Split(' ');
                    List<string> listPara = new List<string>();
                    foreach (string item in temp)
                    {
                        if (item[0] == '@')
                            listPara.Add(item);
                    }
                    for (int i = 0; i < parameter.Length; i++)
                    {
                        command.Parameters.AddWithValue(listPara[i], parameter[i]);
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try { adapter.Fill(data); }
                catch { }


                connection.Close();
            }
            return data;
        }
        #endregion
        #region ExcuteNonQuery
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int acceptedRows = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] temp = query.Split(' ');
                    List<string> listPara = new List<string>();
                    foreach (string item in temp)
                    {
                        if (item[0] == '@')
                            listPara.Add(item);
                    }
                    for (int i = 0; i < parameter.Length; i++)
                    {
                        command.Parameters.AddWithValue(listPara[i], parameter[i]);
                    }
                }
                //thực thi câu query chả về số dòng câu truy vấn thực hiện được
                try
                {
                    acceptedRows = command.ExecuteNonQuery();
                }
                catch { acceptedRows = 0; }
                connection.Close();
            }

            return acceptedRows;
        }
        #endregion
        #region ExecuteScalar
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] temp = query.Split(' ');
                    List<string> listPara = new List<string>();
                    foreach (string item in temp)
                    {
                        if (item[0] == '@')
                            listPara.Add(item);
                    }
                    for (int i = 0; i < parameter.Length; i++)
                    {
                        command.Parameters.AddWithValue(listPara[i], parameter[i]);
                    }
                }
                //thực thi câu query chả về số dòng câu truy vấn thực hiện được
                try
                {
                    data = command.ExecuteScalar();
                }
                catch { }
                connection.Close();
            }

            return data;
        }
        #endregion
    }
}
