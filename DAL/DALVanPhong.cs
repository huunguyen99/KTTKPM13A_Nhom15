using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            var hd = (from p in dt.tblPhieuYeuCauKiemTraPhong
                      join h in dt.tblHopDong on p.MaPhieuKTra equals h.MaPhieuKTra
                      where p.MaPhong.Equals(maPhong) && h.TinhTrangHD == true
                      select h).FirstOrDefault();
            if (hd == null)
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

        public bool TraPhong(string maPhong)
        {
            var hd = (from h in dt.tblHopDong
                      where h.EPhieu.MaPhong.Equals(maPhong) && h.TinhTrangHD == true
                      select h).FirstOrDefault();
            if (hd == null)
                return false;
            try
            {
                hd.TinhTrangHD = false;
                hd.NgayTraThucTe = DateTime.Now;
                dt.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
