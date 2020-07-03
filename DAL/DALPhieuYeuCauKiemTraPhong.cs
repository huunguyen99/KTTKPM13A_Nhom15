using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPhieuYeuCauKiemTraPhong
    {
        VanPhongDbContext dt;
        public DALPhieuYeuCauKiemTraPhong()
        {
            dt = new VanPhongDbContext();
        }

        public bool KiemTraPhongDaGuiPhieuKiemTraChua(string maPhong)
        {
            var ph = (from n in dt.tblPhieuYeuCauKiemTraPhong
                      where n.MaPhong == maPhong && n.TrangThaiPhieu == false
                      select n).FirstOrDefault();
            if (ph != null)
                return true;
            if (LayDSPhieuDaDuyet(maPhong).Count > 0)
                return true;
            return false;
        }
        public void TaoPhieuKiemTra(ePhieuYeuCauKiemTraPhong p)
        {
            try
            {
                dt.tblPhieuYeuCauKiemTraPhong.Add(p);
                dt.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void XoaPhieuKiemTra(int maPhieu)
        {
            var ph = dt.tblPhieuYeuCauKiemTraPhong.Where(x => x.MaPhieuKTra == maPhieu).FirstOrDefault();
            try
            {
                dt.tblPhieuYeuCauKiemTraPhong.Remove(ph);
                dt.SaveChanges();
            }    
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ePhieuYeuCauKiemTraPhong> LayDSPhieuDaDuyet(string maPhong)
        {
            var dsphieu = dt.tblPhieuYeuCauKiemTraPhong.Where(p => p.MaPhong == maPhong && p.TrangThaiPhieu == true).ToList();
            var dshphieucanlay = new List<ePhieuYeuCauKiemTraPhong>();
            foreach(var item in dsphieu)
            {
                var hd = (from n in dt.tblHopDong
                          where n.MaPhieuKTra == item.MaPhieuKTra
                          select n).FirstOrDefault();
                if (hd == null)
                    dshphieucanlay.Add(item);
            }    
            return dshphieucanlay;
        }
        public List<ePhieuYeuCauKiemTraPhong> LayDSPhieuChuaDuyet()
        {
            var dsphieu = dt.tblPhieuYeuCauKiemTraPhong.Where(x => x.TrangThaiPhieu == false).ToList();
            return dsphieu;
        }

        public void DuyetPhieu(ePhieuYeuCauKiemTraPhong ph, int maNVDuyet, bool tinhTrangPhong, string ghiChu)
        {
            var phieu = (from n in dt.tblPhieuYeuCauKiemTraPhong
                         where n.MaPhieuKTra == ph.MaPhieuKTra
                         select n).FirstOrDefault();
            try
            {
                phieu.GhiChu = ghiChu;
                phieu.MaNVKyThuat = maNVDuyet;
                phieu.TinhTrangPhong = tinhTrangPhong;
                phieu.TrangThaiPhieu = true;
                dt.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
