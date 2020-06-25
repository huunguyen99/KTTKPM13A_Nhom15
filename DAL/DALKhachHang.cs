using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALKhachHang
    {
        VanPhongDbContext dt;
        public DALKhachHang()
        {
            dt = new VanPhongDbContext();
        }
        public List<eKhachHang> LayDSKhachHangDangThue(string maPhong)
        {
            var dskh = (from h in dt.tblHopDong
                        where h.TinhTrangHD == true && h.EPhieu.MaPhong.Equals(maPhong) && h.EKhachHang.Active == true
                        select h.EKhachHang).ToList();
            return dskh;
        }

        public List<eKhachHang> LayDSKhachHangKhongConThue()
        {
            var dskh = (from h in dt.tblHopDong
                        where h.TinhTrangHD == false && h.EKhachHang.Active == true
                        select h.EKhachHang).ToList();
            return dskh;
        }
        public List<eKhachHang> LayDSTatCaKhachHang()
        {
            var ds = from n in dt.tblKhachHang
                     where n.Active == true
                     select n;
            return ds.ToList();
        }

        public eKhachHang KiemTraKH(string soCMND)
        {
            var kh = dt.tblKhachHang.Where(k => k.SoCMND.Equals(soCMND)).FirstOrDefault();
            return kh;
        }

        public bool ThemKH(eKhachHang kh)
        {
            var khang = KiemTraKH(kh.SoCMND);
            if (khang != null)
                return false;
            try
            {
                dt.tblKhachHang.Add(kh);
                dt.SaveChanges();
                return true;
            }    
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool XoaKH(eKhachHang kh)
        {
            var khxoa = (from n in dt.tblKhachHang
                         where n.MaKH.Equals(kh.MaKH)
                         select n).FirstOrDefault();
            if (khxoa == null)
                return false;
            try
            {
                khxoa.Active = false;
                dt.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool SuaKH(eKhachHang khCanSua, eKhachHang khSua)
        {
            var kh = (from n in dt.tblKhachHang
                      where n.MaKH.Equals(khCanSua.MaKH)
                      select n).FirstOrDefault();
            if (kh == null)
                return false;
            try
            {
                kh.TenKH = khSua.TenKH;
                kh.Email = khSua.Email;
                kh.SDT = khSua.SDT;
                kh.SoCMND = khSua.SoCMND;
                kh.NgaySinh = khSua.NgaySinh;
                kh.DiaChi = khSua.DiaChi;
                dt.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
