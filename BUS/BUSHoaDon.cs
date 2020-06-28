using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities; 

namespace BUS
{
    public class BUSHoaDon
    {
        DALHoaDon dt;
        public BUSHoaDon()
        {
            dt = new DALHoaDon();
        }
        public DateTime LayHDSauCung(string maPhong)
        {
            return dt.LayHDSauCung(maPhong);
        }
        public void ThemHoaDon(eHoaDon hd)
        {
            dt.ThemHoaDon(hd);
        }
    }
}
