using TT_QLKVC.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_QLKVC.DAO
{
    class LoaiDVDAO
    {
        private static LoaiDVDAO instance;

        public static LoaiDVDAO Instance
        {
            get { if (instance == null) instance = new LoaiDVDAO(); return LoaiDVDAO.instance; }
            private set { LoaiDVDAO.instance = value; }
        }

        private LoaiDVDAO() { }


        public List<LoaiDV> GetListLoaiDV()
        {
            List<LoaiDV> list = new List<LoaiDV>();
            string query = "select * from LOAIDV";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                LoaiDV loaiDV = new LoaiDV(item);
                list.Add(loaiDV);
            }
            return list;
        }

        public LoaiDV GetListLoaiDichVuById(string id)
        {
            LoaiDV loaiDV = null;
            string query = string.Format("Select * from LOAIDV where MALDV = '{0}'", id);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                loaiDV = new LoaiDV(item);
                return loaiDV;
            }

            return loaiDV;
        }

        public bool InsertLoaiDV(string maloaidv,string tenloaidv)
        {
            string query1 = string.Format("exec THEMLOAIDICHVU '{0}',N'{1}'", maloaidv,tenloaidv);
            int result = DataProvider.Instance.ExecuteNonQuery(query1);
            return result > 0;
        }

        public bool UpdateLoaiDV(string maloaidv, string tenloaidv)
        {
            string query1 = string.Format("exec suaLOAIDV  '{0}',N'{1}'", maloaidv, tenloaidv);
            int result = DataProvider.Instance.ExecuteNonQuery(query1);
            return result > 0;
        }

        public bool DeleteLoaiDV(string maloaidv)
        {
            string query1 = string.Format("exec xoaLOAIDV N'{0}' ", maloaidv);
            int result = DataProvider.Instance.ExecuteNonQuery(query1);
            return result > 0;
        }
    }

}
