using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entities;
using DAL;
using System.ServiceModel.Configuration;

namespace VanPhongServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChoThueVanPhongService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ChoThueVanPhongService.svc or ChoThueVanPhongService.svc.cs at the Solution Explorer and start debugging.
    public class ChoThueVanPhongService : IChoThueVanPhongService
    {
        VanPhongDbContext dt;
        public ChoThueVanPhongService()
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
            var ds = dt.tblVanPhong.Where(p=>p.TinhTrang == true).ToList();
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


        public eTaiKhoan KiemTraTaiKhoan(string taiKhoan, string matKhau)
        {
            var tk = (from n in dt.tblTaiKhoan
                      where n.TenTK == taiKhoan && n.MatKhau == matKhau
                      select n).FirstOrDefault();
            return tk;
        }
        public bool DangNhap(string taiKhoan, string matKhau)
        {
            var tk = KiemTraTaiKhoan(taiKhoan, matKhau);
            if (tk == null)
                return false;
            return true;
        }
        public eNhanVien LayNhanVienDangNhap(string taiKhoan, string matKhau)
        {
            var tk = KiemTraTaiKhoan(taiKhoan, matKhau);
            if (tk == null)
                return null;
            return tk.ENhanVien;
        }

        public List<eKhachHang> LayDSKhachHangDangThue(string maPhong)
        {
            var dskh = (from k in dt.tblKhachHang
                        join h in dt.tblHopDong on k.MaKH equals h.MaKH
                        join p in dt.tblPhieuYeuCauKiemTraPhong on h.MaPhieuKTra equals p.MaPhieuKTra
                        where h.NgayTraThucTe > DateTime.Now
                        select k).ToList();
            return dskh;

        }

        public void TaoPhieuKiemTra(ePhieuYeuCauKiemTraPhong p)
        {
            try
            {
                dt.tblPhieuYeuCauKiemTraPhong.Add(p);
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool CapNhatPhieuKiemTra(ePhieuYeuCauKiemTraPhong p, bool tinhTrangPhong, int maNVKyThuat, string ghiChu)
        {
            ePhieuYeuCauKiemTraPhong ph = dt.tblPhieuYeuCauKiemTraPhong.Where(x => x.MaPhieuKTra == p.MaPhieuKTra).FirstOrDefault();
            try
            {
                ph.TinhTrangPhong = tinhTrangPhong;
                ph.TrangThaiPhieu = true;
                ph.MaNVKyThuat = maNVKyThuat;
                ph.GhiChu = ghiChu;
                dt.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ePhieuYeuCauKiemTraPhong> LayDSPhieuDaDuyet(string maPhong)
        {
            var dsphieu = dt.tblPhieuYeuCauKiemTraPhong.Where(p => p.MaPhong == maPhong && p.TrangThaiPhieu == true).ToList();
            return dsphieu;
        }

        public List<ePhieuYeuCauKiemTraPhong> LayDSPhieuChuaDuyet()
        {
            var dsphieu = dt.tblPhieuYeuCauKiemTraPhong.Where(x => x.TrangThaiPhieu == false).ToList();
            return dsphieu;
        }
    }
}
