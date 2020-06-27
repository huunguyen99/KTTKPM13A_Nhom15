using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public bool Xoa_TaiSuDung_Phong(eVanPhong p, bool tinhTrangPhong)
        {
            var ph = (from n in dt.tblVanPhong
                      where n.MaPhong.Equals(p.MaPhong)
                      select n).FirstOrDefault();
            var hd = (from n in dt.tblHopDong
                      where n.EPhieu.MaPhong.Equals(p.MaPhong) && n.TinhTrangHD == true
                      select n).FirstOrDefault();
            if (hd != null)
                return false;
            try
            {
                ph.TinhTrang = tinhTrangPhong;
                dt.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ThemPhong(eVanPhong p)
        {
            var vp = (from n in dt.tblVanPhong
                      where n.MaPhong.Equals(p.MaPhong)
                      select n).FirstOrDefault();
            if (vp != null)
                return false;
            try
            {
                dt.tblVanPhong.Add(p);
                dt.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool SuaPhong(eVanPhong p, eVanPhong phSua)
        {
            var vp = dt.tblVanPhong.Where(x => x.MaPhong.Equals(p.MaPhong)).FirstOrDefault();
            if (vp == null)
                return false;
            try
            {
                vp.SoBongDen = phSua.SoBongDen;
                vp.SoMayLanh = phSua.SoMayLanh;
                vp.GiaThue = phSua.GiaThue;
                vp.DienTich = phSua.DienTich;
                vp.TangLau = phSua.TangLau;
                vp.TenPhong = phSua.TenPhong;
                dt.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
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
