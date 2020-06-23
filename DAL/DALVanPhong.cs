using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALVanPhong
    {
        VanPhongDbContext dt;
        public DALVanPhong()
        {
            dt = new VanPhongDbContext();
        }
        public bool KiemTraPhongDaThueChua(string maPhong)
        {
            var pkt = dt.tblPhieuYeuCauKiemTraPhong.Where(x => x.MaPhong == maPhong).FirstOrDefault();
            if (pkt == null)
                return false;
            var hd = dt.tblHopDong.Where(x => x.MaPhieuKTra == pkt.MaPhieuKTra).FirstOrDefault();
            if (hd == null)
                return false;
            if (hd.NgayTraThucTe < DateTime.Now)
                return false;
            return true;
        }

        public List<eVanPhong> LayDanhSachPhong()
        {
            var ds = (from n in dt.tblVanPhong
                      select n).ToList();
            return ds;
        }
        public List<eVanPhong> LayDSVanPhongDangChoThue()
        {
            var ds = dt.tblVanPhong.Where(p => p.TinhTrang == true).ToList();
            List<eVanPhong> dsphong = new List<eVanPhong>();
            foreach (var item in ds)
            {
                if (KiemTraPhongDaThueChua(item.MaPhong) == true)
                    dsphong.Add(item);
            }
            return dsphong;
        }
        public List<eVanPhong> LayDSVanPhongTrong()
        {
            var ds = dt.tblVanPhong.Where(x => x.TinhTrang == true).ToList();
            List<eVanPhong> dsphong = new List<eVanPhong>();
            foreach (var item in ds)
            {
                if (KiemTraPhongDaThueChua(item.MaPhong) == false)
                    dsphong.Add(item);
            }
            return dsphong;
        }
    }
}
