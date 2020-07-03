using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALHoaDon
    {
        VanPhongDbContext dt;
        public DALHoaDon()
        {
            dt = new VanPhongDbContext();
        }

        public DateTime LayHDSauCung(string maPhong)
        {
            var hd = dt.tblHoaDon.Where(x => x.EHopDong.EPhieu.MaPhong.Equals(maPhong)).Max(h => h.NgayCanLap);
            return hd;
        }

        public void ThemHoaDon(eHoaDon hd)
        {
            try
            {
                dt.tblHoaDon.Add(hd);
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void ThanhToanHoaDon(eHoaDon hdtt, DateTime ngayThanhToan)
        {
            eHoaDon hd = (from n in dt.tblHoaDon
                          where n.MaHoaDon.Equals(hdtt.MaHoaDon)
                          select n).FirstOrDefault();
            try
            {
                hd.NgayThanhToan = ngayThanhToan;
                hd.TinhTrangHD = true;
                dt.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
