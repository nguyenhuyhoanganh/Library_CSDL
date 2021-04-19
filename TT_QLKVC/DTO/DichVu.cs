using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_QLKVC.DTO
{
    public class DichVu
    { 
        public DichVu(string mADV, string tENDV, string mALDV, int dONGIA)
        {
            this.MADV = mADV;
            this.TENDV = tENDV;
            this.MALDV = mALDV;
            this.DONGIA = dONGIA;
        }
        public DichVu(DataRow row)
        {
            this.MADV = row["mADV"].ToString();
            this.TENDV = row["tENDV"].ToString();
            this.MALDV = row["mALDV"].ToString();
            this.DONGIA = (int)row["dONGIA"];
        }
        private string mADV;
        public string MADV
        {
            get
            {
                return mADV;
            }

            set
            {
                mADV = value;
            }
        }


        private string tENDV;

        public string TENDV
        {
            get
            {
                return tENDV;
            }

            set
            {
                tENDV = value;
            }
        }
        private string mALDV;

        public string MALDV
        {
            get
            {
                return mALDV;
            }

            set
            {
                mALDV = value;
            }
        }

        private int dONGIA;

        public int DONGIA
        {
            get
            {
                return dONGIA;
            }

            set
            {
                dONGIA = value;
            }
        }
    }
}
