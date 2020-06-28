using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BUS
{
    public class BUSChiTietHoaDon
    {
        DALChiTietHoaDon dt;
        public BUSChiTietHoaDon()
        {
            dt = new DALChiTietHoaDon();
        }
        public void ThemCTHoaDon(eChiTietHoaDon cthd)
        {
            dt.ThemCTHoaDon(cthd);
        }
        public List<eChiTietHoaDon> LayDSHoaDon(string maPhong)
        {
            return dt.LayDSHoaDon(maPhong);
        }
        public List<eChiTietHoaDon> LayDSHoaDonChuaThanhToan(string maPhong)
        {
            return dt.LayDSHoaDonChuaThanhToan(maPhong);
        }
    }
}
