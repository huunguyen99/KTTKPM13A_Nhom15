using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALChiTietHoaDon
    {
        VanPhongDbContext dt;
        public DALChiTietHoaDon()
        {
            dt = new VanPhongDbContext();
        }
        public void ThemCTHoaDon(eChiTietHoaDon cthd)
        {
            try
            {
                dt.tblCT_HoaDon.Add(cthd);
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<eChiTietHoaDon> LayDSHoaDon(string maPhong)
        {
            var ds = (from n in dt.tblCT_HoaDon
                      where n.EHoaDon.EHopDong.EPhieu.MaPhong.Equals(maPhong)
                      select n).OrderByDescending(n => n.EHoaDon.NgayCanLap).ToList();
            return ds;
        }

        public List<eChiTietHoaDon> LayDSHoaDonChuaThanhToan(string maPhong)
        {
            var ds = (from n in dt.tblCT_HoaDon
                      where n.EHoaDon.EHopDong.EPhieu.MaPhong.Equals(maPhong) && n.EHoaDon.TinhTrangHD == false
                      select n).OrderByDescending(n => n.EHoaDon.NgayCanLap).ToList();
            return ds;
        }
    }
}
