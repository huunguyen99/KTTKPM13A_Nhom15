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
        public void DuyetPhieu(ePhieuYeuCauKiemTraPhong ph, int maNVDuyet, bool tinhTrangPhong, string ghiChu)
        {
            dt.DuyetPhieu(ph, maNVDuyet, tinhTrangPhong, ghiChu);
        }
    }
}
