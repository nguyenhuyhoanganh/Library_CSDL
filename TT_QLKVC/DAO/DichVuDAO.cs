using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_QLKVC.DAO
{
    class DichVuDAO
    {
        private static DichVuDAO instance;

        public static DichVuDAO Instance
        {
            get { if (instance == null) instance = new DichVuDAO(); return DichVuDAO.instance; }
            private set { DichVuDAO.instance = value; }
        }

        private DichVuDAO() { }

        public bool InsertDV(string madv, string tendv, decimal dongia, string maldv)
        {
            string query1 = string.Format("exec THEMDICHVU '{0}',N'{1}',{2},'{3}'", madv,tendv,dongia,maldv);
            int result = DataProvider.Instance.ExecuteNonQuery(query1);
            return result > 0;
        }
        public bool UpdateDV(string madv, string tendv, decimal dongia, string maldv)
        {
            string query1 = string.Format("exec suaDICHVU '{0}',N'{1}',{2},'{3}'", madv, tendv, dongia, maldv);
            int result = DataProvider.Instance.ExecuteNonQuery(query1);
            return result > 0;
        }
        public bool DeleteDV(string madv)
        {
            string query1 = string.Format("exec xoaDICHVU '{0}'", madv);
            int result = DataProvider.Instance.ExecuteNonQuery(query1);
            return result > 0;
        }
    }
}
