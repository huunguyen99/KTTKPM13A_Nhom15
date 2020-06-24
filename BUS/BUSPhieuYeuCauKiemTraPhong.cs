using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BUS
{
    public class BUSPhieuYeuCauKiemTraPhong
    {
        DALPhieuYeuCauKiemTraPhong dt;
        public BUSPhieuYeuCauKiemTraPhong()
        {
            dt = new DALPhieuYeuCauKiemTraPhong();
        }
        public bool KiemTraPhongDaGuiPhieuKiemTraChua(string maPhong)
        {
            return dt.KiemTraPhongDaGuiPhieuKiemTraChua(maPhong);
        }
        public void TaoPhieuKiemTra(ePhieuYeuCauKiemTraPhong p)
        {
            dt.TaoPhieuKiemTra(p);
        }

        public bool CapNhatPhieuKiemTra(ePhieuYeuCauKiemTraPhong p, bool tinhTrangPhong, int maNVKyThuat, string ghiChu)
        {
            return dt.CapNhatPhieuKiemTra(p, tinhTrangPhong,maNVKyThuat, ghiChu);
        }

        public void XoaPhieuKiemTra(int maPhieu)
        {
            dt.XoaPhieuKiemTra(maPhieu);
        }
        public List<ePhieuYeuCauKiemTraPhong> LayDSPhieuDaDuyet(string maPhong)
        {
            return dt.LayDSPhieuDaDuyet(maPhong);
        }

        public List<ePhieuYeuCauKiemTraPhong> LayDSPhieuChuaDuyet()
        {
            return dt.LayDSPhieuChuaDuyet();
        }
    }
}
