using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_QLKVC.DTO
{
    public class LoaiDV
    {
        public LoaiDV(string mALDV, string tENLDV)
        {
            this.MALDV = mALDV;
            this.TENLDV = tENLDV;
        }

        public LoaiDV(DataRow row)
        {
            this.MALDV = row["mALDV"].ToString();
            this.TENLDV = row["tENLDV"].ToString();
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


        private string tENLDV;

        public string TENLDV
        {
            get
            {
                return tENLDV;
            }

            set
            {
                tENLDV = value;
            }
        }
    }
}
