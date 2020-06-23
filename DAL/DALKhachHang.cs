using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var dskh = (from k in dt.tblKhachHang
                        join h in dt.tblHopDong on k.MaKH equals h.MaKH
                        join p in dt.tblPhieuYeuCauKiemTraPhong on h.MaPhieuKTra equals p.MaPhieuKTra
                        where h.NgayTraThucTe > DateTime.Now && p.MaPhong == maPhong
                        select k).ToList();
            return dskh;
        }

        public List<eKhachHang> LayDSKhachHangKhongConThue()
        {
            var dskh = (from k in dt.tblKhachHang
                        join h in dt.tblHopDong on k.MaKH equals h.MaKH
                        join p in dt.tblPhieuYeuCauKiemTraPhong on h.MaPhieuKTra equals p.MaPhieuKTra
                        where h.NgayTraThucTe < DateTime.Now
                        select k).ToList();
            return dskh;
        }
        public List<eKhachHang> LayDSTatCaKhachHang()
        {
            var ds = from n in dt.tblKhachHang
                     where n.Active == true
                     select n;
            return ds.ToList();
        }
    }
}
